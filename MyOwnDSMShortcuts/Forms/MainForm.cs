using ImageMagick;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZTn.Json.Editor.Forms;

namespace BeatificaBytes.Synology.Mods
{
    public partial class MainForm : Form
    {
        #region Enum --------------------------------------------------------------------------------------------------------------------------------
        public enum State
        {
            None,
            View,
            Edit,
            Add
        }

        public enum UrlType
        {
            Url = 0,
            Script = 1,
            WebApp = 2
        }
        #endregion  --------------------------------------------------------------------------------------------------------------------------------

        #region Declarations -----------------------------------------------------------------------------------------------------------------------
        const string CONFIGFILE = @"package\{0}\config";
        static Regex getPort = new Regex(@"^:(\d*)/.*$", RegexOptions.Compiled);
        static Regex getOldFirmwareVersion = new Regex(@"^\d+.\d+\.\d+$", RegexOptions.Compiled);
        static Regex getShortFirmwareVersion = new Regex(@"^\d+\.\d+$", RegexOptions.Compiled);
        static Regex getFirmwareVersion = new Regex(@"^(\d+\.)*\d-\d+$", RegexOptions.Compiled);

        static Regex getOldVersion = new Regex(@"^((\d+\.)*\d+)\.(\d+)$", RegexOptions.Compiled);
        static Regex getVersion = new Regex(@"^(\d+\.)*\d-\d+$", RegexOptions.Compiled);


        string PackageRootPath = Properties.Settings.Default.LastPackage;
        string PackageRepoPath = Properties.Settings.Default.PackageRepo;

        //4 next vars are a Dirty Hack - move these 4 vars into a class. Replace AppsData with that class, ...
        string webAppFolder = null;
        string webAppIndex = null;
        string currentScript = null;
        string currentRunner = null;

        Dictionary<string, PictureBox> pictureBoxes;
        SortedDictionary<string, string> info;

        Image picturePkg_72 = null;
        Image picturePkg_120 = null;
        Image picturePkg_256 = null;

        KeyValuePair<string, AppsData> current;
        State state;
        Package list;
        bool dirty = false;
        bool wizardExist = false;

        string previousReportUrl;
        string imageDragDropPath;
        protected bool validImage4DragDrop;
        protected string validPackage4DragDrop = null;

        protected List<String> warnings = new List<string>();

        public string CurrentScript
        {
            get
            {
                return currentScript;
            }

            set
            {
                currentScript = value;
            }
        }
        #endregion  --------------------------------------------------------------------------------------------------------------------------------

        #region Initialize -------------------------------------------------------------------------------------------------------------------------
        public MainForm(string package)
        {
            InitializeComponent();

            // Load the default runner script or create one if it does not exist
            var defaultRunnerPath = Path.Combine(Helper.ResourcesDirectory, "default.runner");
            if (!File.Exists(defaultRunnerPath))
                File.WriteAllText(defaultRunnerPath, Properties.Settings.Default.Ps_Exec);

            // Extract the WizardUI background image if it does not exist
            var backWizard = Path.Combine(Helper.ResourcesDirectory, "backwizard.png");
            if (!File.Exists(backWizard))
            {
                var backWizardPng = new Bitmap(Properties.Resources.BackWizard);
                backWizardPng.Save(backWizard);
            }

            groupBoxItem.Enabled = false;
            groupBoxPackage.Enabled = false;
            comboBoxTransparency.SelectedIndex = 0;

            // Prepare the PictureBox for drag&drop, etc...
            PrepareItemPictureBoxes();

            if (!string.IsNullOrEmpty(package))
            {
                // Override the path of the last package;
                PackageRootPath = null;

                if (File.Exists(package))
                {
                    var packageDirectory = Path.GetDirectoryName(package);
                    var spkList = Directory.GetFiles(packageDirectory, "*.spk");
                    if (spkList.Length > 1)
                    {
                        //several SPK in the folder => Import the requested one
                        packageDirectory = ExpandSpkInTempFolder(package);
                    }
                    //Check if the SPK is in a valid Package folder
                    var getPackage = IsPackageFolderEmpty(packageDirectory, true);
                    if (getPackage == DialogResult.No)
                    {
                        PackageRootPath = packageDirectory;
                    }
                }
                else if (Directory.Exists(package))
                {
                    var getPackage = IsPackageFolderEmpty(package);
                    if (getPackage == DialogResult.No)
                    {
                        PackageRootPath = package;
                    }
                }
            }

            if (string.IsNullOrEmpty(PackageRootPath) || !Directory.Exists(PackageRootPath))
            {
                PackageRootPath = "";
            }
            else if (!File.Exists(Path.Combine(PackageRootPath, "INFO")))
            {
                MessageBoxEx.Show(this, "The INFO file of your package does not exist anymore. Reset your package.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PackageRootPath = "";
            }
            else
            {
                LoadPackageInfo(PackageRootPath);
                BindData(list, null);
                CopyPackagingBinaries(PackageRootPath);
            }
            DisplayItem();
            foreach (var control in groupBoxItem.Controls)
            {
                var item = control as Control;
                if (item != null)
                {
                    item.MouseEnter += new System.EventHandler(this.OnMouseEnter);
                    item.Enter += new System.EventHandler(this.OnMouseEnter);
                    item.MouseLeave += new System.EventHandler(this.OnMouseLeave);

                    if (item.Name.StartsWith("textBox") && Helper.IsSubscribed(item, "EventValidating"))
                        item.TextChanged += new System.EventHandler(this.OnTextChanged);
                }
            }
            foreach (var control in groupBoxPackage.Controls)
            {
                var item = control as Control;
                if (item != null)
                {
                    item.MouseEnter += new System.EventHandler(this.OnMouseEnter);
                    item.Enter += new System.EventHandler(this.OnMouseEnter);
                    item.MouseLeave += new System.EventHandler(this.OnMouseLeave);

                    if (item.Name.StartsWith("textBox") && Helper.IsSubscribed(item, "EventValidating"))
                        item.TextChanged += new System.EventHandler(this.OnTextChanged);
                }
            }

            foreach (ToolStripItem control in menuStripMainBar.Items)
            {
                ToolStripDropDownItem mainMenu = control as ToolStripDropDownItem;
                if (mainMenu != null)
                {
                    foreach (ToolStripItem menu in mainMenu.DropDownItems)
                    {
                        menu.MouseEnter += new System.EventHandler(this.OnMouseEnter);
                        menu.MouseLeave += new System.EventHandler(this.OnMouseLeave);
                    }
                }
            }

            ShowAdvancedEditor(Properties.Settings.Default.AdvancedEditor);
            CreateRecentsMenu();
            textBoxDisplay.Focus();
        }

        private void CreateRecentsMenu()
        {
            menuOpenRecentPackage.DropDownItems.Clear();
            if (Properties.Settings.Default.Recents != null)
            {
                var items = new List<ToolStripMenuItem>();
                for (int item = Properties.Settings.Default.Recents.Count - 1; item >= 0; item--)
                {
                    string path = null;
                    bool exist = true;
                    try
                    {
                        path = Properties.Settings.Default.Recents[item];
                        exist = Directory.Exists(path);
                        if (exist)
                        {
                            var entry = new ToolStripMenuItem();
                            entry.Name = "recentItem" + item.ToString();
                            entry.Tag = path;
                            entry.Text = Properties.Settings.Default.RecentsName[item];
                            entry.ToolTipText = path;
                            entry.Click += new EventHandler(MenuItemOpenRecent_ClickHandler);
                            items.Add(entry);
                        }
                    }
                    catch
                    {
                        exist = false;
                    }
                    if (!exist) RemoveRecentPath(path);
                }
                menuOpenPackage.DropDownItems.AddRange(items.ToArray());
            }
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(sender as Control, "");
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            var zone = sender as Control;
            if (zone != null)
            {
                var text = toolTip4Mods.GetToolTip(zone);
                labelToolTip.Text = text;
            }
            var menu = sender as ToolStripItem;
            if (menu != null)
            {
                var text = menu.ToolTipText;
                labelToolTip.Text = text;
            }
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            labelToolTip.Text = "";
        }

        private void PrepareItemPictureBoxes()
        {
            pictureBoxes = new Dictionary<string, PictureBox>();

            foreach (var control in groupBoxItem.Controls)
            {
                var pictureBoxItem = control as PictureBox;
                if (pictureBoxItem != null && pictureBoxItem.Tag != null && pictureBoxItem.Tag.ToString().StartsWith("ITEM"))
                {
                    pictureBoxItem.AllowDrop = true;

                    pictureBoxItem.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBoxItem_DragDrop);
                    pictureBoxItem.DoubleClick += new System.EventHandler(this.pictureBox_DoubleClick);
                    pictureBoxItem.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBoxItem_DragEnter);

                    if (!pictureBoxItem.Tag.ToString().EndsWith("MAX"))
                        pictureBoxItem.Enabled = !checkBoxSize.Checked;

                    pictureBoxes.Add(pictureBoxItem.Tag.ToString().Split(';')[1], pictureBoxItem);
                }
            }

            pictureBoxPkg_72.AllowDrop = true;
            pictureBoxPkg_256.AllowDrop = true;
        }

        private void BindData(Package list, string selection)
        {
            listViewItems.Items.Clear();
            if (list != null)
            {
                foreach (var item in list.items)
                {
                    var uri = item.Value.url;
                    if (uri.StartsWith("/"))
                    {
                        if (item.Value.port != 0)
                            uri = string.Format(":{0}{1}", item.Value.port, uri);
                        uri = string.Format("<Synology>{0}", uri);

                        if (!string.IsNullOrEmpty(item.Value.protocol))
                            uri = string.Format("{0}://{1}", item.Value.protocol, uri);
                    }

                    // Define the list items
                    var lvi = new ListViewItem(item.Value.title);
                    lvi.SubItems.Add(uri);
                    lvi.Tag = item;

                    // Add the list items to the ListView
                    listViewItems.Items.Add(lvi);
                }

                listViewItems.Sort();

                if (!string.IsNullOrEmpty(selection))
                {
                    foreach (ListViewItem item in listViewItems.Items)
                    {
                        if (item.Text == selection)
                        {
                            item.Selected = true;
                            item.Focused = true;
                        }
                    }
                }
            }
            else
            {
                var lvi = new ListViewItem("No Items");
                lvi.SubItems.Add("This package only contains scripts");
                lvi.Tag = new KeyValuePair<string, AppsData>(null, null);

                // Add the list items to the ListView
                listViewItems.Items.Add(lvi);
            }
        }

        private void LoadPackageConfig(string path)
        {
            string ui = null;
            if (info.ContainsKey("dsmuidir"))
                ui = info["dsmuidir"];
            else
            {
                var fileList = new DirectoryInfo(Path.Combine(path, "package")).GetFiles("config", SearchOption.AllDirectories);
                if (fileList.Length > 0)
                {
                    ui = fileList[0].FullName.Replace(Path.Combine(path, "package"), "").Replace("config", "").Trim(new char[] { '\\' });
                    info.Add("dsmuidir", ui);
                }
                else
                    ui = null;

            }

            if (ui != null)
            {
                var config = Path.Combine(path, string.Format(CONFIGFILE, ui));
                if (File.Exists(config))
                {
                    var json = File.ReadAllText(config);
                    list = JsonConvert.DeserializeObject<Package>(json, new KeyValuePairConverter());

                    if (list == null || list.items.Count == 0)
                    {
                        //json = Properties.Settings.Default.Packages;
                        //if (!string.IsNullOrEmpty(json))
                        //{
                        //    list = JsonConvert.DeserializeObject<Package>(json, new KeyValuePairConverter());
                        //}
                        //else
                        //{
                        list = new Package();
                        //}
                    }
                }
                else
                {
                    list = new Package();
                }

                foreach (var item in list.items)
                {
                    if (item.Value.itemType == -1)
                        if (item.Value.type == "legacy") item.Value.itemType = (int)UrlType.WebApp;
                    //if (item.Value.url.StartsWith("/webman/3rdparty/") ) item.Value.itemType = (int)UrlType.WebApp;
                }
            }
            if (!info.ContainsKey("singleApp"))
            {
                if (list != null && list.items.Count == 1)
                {
                    var title = list.items.First().Value.title;
                    title = Helper.CleanUpText(title);

                    if (list.items.First().Value.url.Contains(string.Format("/{0}/", title)))
                    {
                        info.Add("singleApp", "no");
                    }
                    else
                    {
                        info.Add("singleApp", "yes");
                    }
                }
                else
                {
                    info.Add("singleApp", "no");
                }
            }

            var pkg72 = new FileInfo(Path.Combine(path, "PACKAGE_ICON.PNG"));
            if (pkg72.Exists)
            {
                picturePkg_72 = Helper.LoadImageFromFile(pkg72.FullName);
            }
            else
            {
                if (info.ContainsKey("package_icon"))
                {
                    picturePkg_72 = Helper.LoadImageFromBase64(info["package_icon"]);
                }
                else
                {

                    picturePkg_72 = null;
                }
            }
            var pkg120 = new FileInfo(Path.Combine(path, "PACKAGE_ICON_120.PNG"));
            if (pkg120.Exists)
            {
                picturePkg_120 = Helper.LoadImageFromFile(pkg120.FullName);
            }
            else
            {
                if (info.ContainsKey("package_icon_120"))
                {
                    picturePkg_120 = Helper.LoadImageFromBase64(info["package_icon_120"]);
                }
                else if (info.ContainsKey("package_icon120"))
                {
                    picturePkg_120 = Helper.LoadImageFromBase64(info["package_icon120"]);
                }
                else
                {
                    picturePkg_120 = null;
                }
            }
            var pkg256 = new FileInfo(Path.Combine(path, "PACKAGE_ICON_256.PNG"));
            if (pkg256.Exists)
            {
                picturePkg_256 = Helper.LoadImageFromFile(pkg256.FullName);
            }
            else
            {
                if (info.ContainsKey("package_icon_256"))
                {
                    picturePkg_256 = Helper.LoadImageFromBase64(info["package_icon_256"]);
                }
                else if (info.ContainsKey("package_icon256"))
                {
                    picturePkg_256 = Helper.LoadImageFromBase64(info["package_icon256"]);
                }
                else
                {
                    picturePkg_256 = null;
                }
            }

            groupBoxItem.Enabled = false;
        }

        private void LoadPackageInfo(string path)
        {
            info = new SortedDictionary<string, string>();
            state = State.None;

            var file = Path.Combine(path, "INFO");

            if (File.Exists(file))
            {
                var lines = File.ReadAllLines(file);
                foreach (var line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var key = line.Substring(0, line.IndexOf('='));
                        var value = line.Substring(line.IndexOf('=') + 1);
                        value = value.Trim(new char[] { '"' });
                        value = value.Replace("<br>", "\r\n");
                        if (info.ContainsKey(key))
                            info.Remove(key);
                        info.Add(key, value);
                    }
                }

                if (info.ContainsKey("maintainer") && info["maintainer"] == "...")
                    info["maintainer"] = Environment.UserName;
                if (info.ContainsKey("distributor") && info["distributor"] == "...")
                    info["distributor"] = Environment.UserName;

                groupBoxPackage.Enabled = false;

                SavePackageSettings();
                CreateRecentsMenu();

                var wizard = Path.Combine(path, "WIZARD_UIFILES");
                wizardExist = Directory.Exists(wizard);
            }
            else
            {
                MessageBoxEx.Show(this, "The working folder doesn't contain a Package.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadPackageConfig(path);

            FillInfoScreen();
        }

        private string GetUIDir(string path)
        {
            string value = null;

            var file = Path.Combine(path, "INFO");
            if (File.Exists(file))
            {
                var lines = File.ReadAllLines(file);
                foreach (var line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var key = line.Substring(0, line.IndexOf('='));
                        if (key == "dsmuidir")
                        {
                            value = line.Substring(line.IndexOf('=') + 1);
                            value = value.Trim(new char[] { '"' });
                            break;
                        }
                    }
                }
            }
            else
            {
                MessageBoxEx.Show(this, "The working folder doesn't contain a Package.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return value;
        }

        private void FillInfoScreen()
        {
            if (info != null)
            {
                var parent = Path.GetFileName(PackageRootPath);
                Guid tmp;
                bool isTempFolder = Guid.TryParse(parent, out tmp);
                if (isTempFolder)
                {
                    if (groupBoxPackage.BackColor != Color.Salmon)
                    {
                        groupBoxPackage.BackColor = Color.Salmon;
                        warnings.Add("This package has has been opened from a temporary folder. Possibly move it into a traget folder using the menu Package > Move.");
                    }
                }
                else
                {
                    groupBoxPackage.BackColor = SystemColors.Control;
                }

                string subkey;
                var unused = new List<string>(info.Keys);
                foreach (var control in groupBoxPackage.Controls)
                {
                    var textBox = control as TextBox;
                    if (textBox != null && textBox.Tag != null && textBox.Tag.ToString().StartsWith("PKG"))
                    {
                        var keys = textBox.Tag.ToString().Split(';');
                        textBox.Text = "";
                        foreach (var key in keys)
                        {
                            if (key.StartsWith("PKG"))
                            {
                                subkey = key.Substring(3);
                                if (info.Keys.Contains(subkey))
                                {
                                    if (string.IsNullOrEmpty(textBox.Text))
                                        textBox.Text = info[subkey];
                                    unused.Remove(subkey);
                                }
                            }
                        }
                    }
                    var comboBox = control as ComboBox;
                    if (comboBox != null && comboBox.Tag != null && comboBox.Tag.ToString().StartsWith("PKG"))
                    {
                        var keys = comboBox.Tag.ToString().Split(';');
                        comboBox.Text = "";
                        foreach (var key in keys)
                        {
                            if (key.StartsWith("PKG"))
                            {
                                subkey = key.Substring(3);
                                if (info.Keys.Contains(subkey))
                                {
                                    //if (string.IsNullOrEmpty(comboBox.Text))
                                    comboBox.SelectedIndex = comboBox.FindStringExact(info[subkey]);
                                    if (string.IsNullOrEmpty(comboBox.Text))
                                        comboBox.Text = info[subkey];
                                    unused.Remove(subkey);
                                }
                            }
                        }
                    }
                    var checkBox = control as CheckBox;
                    if (checkBox != null && checkBox.Tag != null && checkBox.Tag.ToString().StartsWith("PKG"))
                    {
                        bool tick = false;
                        var keys = checkBox.Tag.ToString().Split(';');
                        foreach (var key in keys)
                        {
                            subkey = key.Substring(3);
                            if (key.StartsWith("PKG"))
                            {
                                if (info.Keys.Contains(subkey))
                                {
                                    unused.Remove(subkey);
                                    tick = (info[subkey] == "yes");
                                }
                            }
                            if (key.StartsWith("DEF"))
                            {
                                tick = (subkey == "yes");
                            }
                        }
                        checkBox.Checked = tick;
                    }
                }

                checkBoxAdminUrl.Checked = !string.IsNullOrEmpty(textBoxAdminPort.Text) || !string.IsNullOrEmpty(textBoxAdminUrl.Text);

                //Remove unused info element that are not to be displayed to the user
                var unusedkeys = new List<String>(unused);
                foreach (var key in unusedkeys)
                {
                    if (key.StartsWith("#"))
                        unused.Remove(key); // commented info
                    if (key.StartsWith("description_"))
                        unused.Remove(key); // multilingual description
                    if (key.StartsWith("displayname_"))
                        unused.Remove(key); // multilingual display name

                }
                unused.Remove("dsmuidir");
                unused.Remove("checksum");
                unused.Remove("reloadui");//Not yet supported but ignored

                unused.Remove("thirdparty");//Not yet supported but ignored as not supported by DSM >= 5.0

                unused.Remove("package_icon");//Not yet supported but ignored
                unused.Remove("package_icon_120");//Not yet supported but ignored
                unused.Remove("package_icon_256");//Not yet supported but ignored
                unused.Remove("package_icon120");//Not yet supported but ignored
                unused.Remove("package_icon256");//Not yet supported but ignored

                var listDependencies = new Dependencies(null);
                listDependencies.RemoveSupported(unused);

                if (unused.Count > 0)
                {
                    warnings.Add("There are a few unsupported info in this package. You can edit those via the 'Advanced' button." + Environment.NewLine + Environment.NewLine + unused.Aggregate((i, j) => i + Environment.NewLine + j + ": " + info[j]));
                }
            }
            else
            {
                groupBoxPackage.BackColor = SystemColors.Control;

                foreach (var control in groupBoxPackage.Controls)
                {
                    var textBox = control as TextBox;
                    if (textBox != null && textBox.Tag != null && textBox.Tag.ToString().StartsWith("PKG"))
                    {
                        textBox.Text = "";
                    }
                    var checkBox = control as CheckBox;
                    if (checkBox != null && checkBox.Tag != null && checkBox.Tag.ToString().StartsWith("PKG"))
                    {
                        checkBox.Checked = false;
                    }
                    var comboBox = control as ComboBox;
                    if (comboBox != null && comboBox.Tag != null && comboBox.Tag.ToString().StartsWith("PKG"))
                    {
                        comboBox.Text = "";
                    }
                }

                //Remove unused info element that are not to be displayed to the user
                checkBoxAdminUrl.Checked = false;
            }

            if (picturePkg_256 == null && picturePkg_120 != null)
            {
                picturePkg_256 = picturePkg_120;
            }

            LoadPictureBox(pictureBoxPkg_256, picturePkg_256);
            LoadPictureBox(pictureBoxPkg_72, picturePkg_72);

            Blink();
        }
        private async void Blink()
        {
            pictureBoxWarning.Visible = true;
            var image = pictureBoxWarning.BackgroundImage;
            while (warnings.Count > 0)
            {
                await Task.Delay(500);
                if (pictureBoxWarning.BackgroundImage == null)
                    pictureBoxWarning.BackgroundImage = image;
                else
                    pictureBoxWarning.BackgroundImage = null;
            }
            pictureBoxWarning.BackgroundImage = image;
            pictureBoxWarning.Visible = false;
        }

        private void InitialConfiguration(string path)
        {
            using (new CWaitCursor())
            {
                Process unzip = new Process();
                unzip.StartInfo.FileName = Path.Combine(Helper.ResourcesDirectory, "7z.exe");
                unzip.StartInfo.Arguments = string.Format("x \"{0}\" -o\"{1}\"", Path.Combine(Helper.ResourcesDirectory, "Package.zip"), path);
                unzip.StartInfo.UseShellExecute = false;
                unzip.StartInfo.RedirectStandardOutput = true;
                unzip.StartInfo.CreateNoWindow = true;
                unzip.Start();
                unzip.WaitForExit(10000);
                if (unzip.StartInfo.RedirectStandardOutput) Console.WriteLine(unzip.StandardOutput.ReadToEnd());

                CopyPackagingBinaries(path);
            }
        }
        #endregion --------------------------------------------------------------------------------------------------------------------------------

        #region Manage Package --------------------------------------------------------------------------------------------------------------------

        // Click on Button Reset Package
        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetPackage();
        }

        // Click on Button Create Package
        private void buttonPublish_Click(object sender, EventArgs e)
        {
            PublishPackage();
        }

        private void PublishPackage()
        {
            warnings.Clear();

            if (ValidateChildren())
            {
                GeneratePackage(PackageRootPath);

                var PackageRepo = Properties.Settings.Default.PackageRepo;
                var DefaultPackageRepo = Properties.Settings.Default.DefaultPackageRepo;
                if (string.IsNullOrEmpty(PackageRepo) || !Directory.Exists(PackageRepo) || !DefaultPackageRepo)
                {

                    SpkRepoBrowserDialog4Mods.Title = "Pick a folder to publish the Package.";
                    if (!string.IsNullOrEmpty(PackageRepoPath))
                        SpkRepoBrowserDialog4Mods.InitialDirectory = PackageRepoPath;
                    else if (string.IsNullOrEmpty(PackageRepo) || !Directory.Exists(PackageRepo))
                        SpkRepoBrowserDialog4Mods.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    else
                        SpkRepoBrowserDialog4Mods.InitialDirectory = PackageRepo;

                    if (SpkRepoBrowserDialog4Mods.ShowDialog())
                        PackageRepoPath = SpkRepoBrowserDialog4Mods.FileName;
                    else
                        PackageRepoPath = null;
                }
                else
                    PackageRepoPath = PackageRepo;

                if (!string.IsNullOrEmpty(PackageRepoPath))
                    PublishPackage(PackageRootPath, PackageRepoPath);
            }
        }

        private void SavePackageInfo(string path)
        {
            using (new CWaitCursor())
            {

                if (info != null)
                {
                    // Collect Package Info from controls tagged like PKG...
                    foreach (var control in groupBoxPackage.Controls)
                    {
                        var textBox = control as TextBox;
                        if (textBox != null && textBox.Tag != null && textBox.Tag.ToString().StartsWith("PKG"))
                        {
                            var keys = textBox.Tag.ToString().Split(';');
                            foreach (var key in keys)
                            {
                                if (key.StartsWith("PKG"))
                                {
                                    var keyId = key.Substring(3);
                                    if (info.Keys.Contains(keyId))
                                        info[keyId] = textBox.Text.Trim();
                                    else
                                        info.Add(keyId, textBox.Text.Trim());
                                }
                            }
                        }
                        var checkBox = control as CheckBox;
                        if (checkBox != null && checkBox.Tag != null && checkBox.Tag.ToString().StartsWith("PKG"))
                        {
                            var keys = checkBox.Tag.ToString().Split(';');
                            foreach (var key in keys)
                            {
                                if (key.StartsWith("PKG"))
                                {
                                    var keyId = key.Substring(3);
                                    var value = checkBox.Checked ? "yes" : "no";
                                    if (info.Keys.Contains(keyId))
                                        info[keyId] = value;
                                    else
                                        info.Add(keyId, value);
                                }
                            }
                        }
                        var comboBox = control as ComboBox;
                        if (comboBox != null && comboBox.Tag != null && comboBox.Tag.ToString().StartsWith("PKG"))
                        {
                            var keys = comboBox.Tag.ToString().Split(';');
                            foreach (var key in keys)
                            {
                                if (key.StartsWith("PKG"))
                                {
                                    var keyId = key.Substring(3);
                                    if (info.Keys.Contains(keyId))
                                        info[keyId] = comboBox.Text.Trim();
                                    else
                                        info.Add(keyId, comboBox.Text.Trim());
                                }
                            }
                        }
                    }

                    // Delete existing INFO file
                    var infoName = Path.Combine(path, "INFO");
                    if (File.Exists(infoName))
                        File.Delete(infoName);

                    // Write the new INFO file
                    using (StreamWriter outputFile = new StreamWriter(infoName))
                    {
                        foreach (var element in info)
                        {
                            if (!string.IsNullOrEmpty(element.Value) && element.Value != "path")
                            {
                                var value = element.Value.Replace("\r\n", "<br>").Replace("\n", "<br>");
                                outputFile.WriteLine("{0}=\"{1}\"", element.Key, value);
                            }
                        }
                    }

                    // Save Package's icons
                    var imageName = Path.Combine(path, "PACKAGE_ICON.PNG");
                    SavePkgImage(pictureBoxPkg_72, imageName);
                    imageName = Path.Combine(path, "PACKAGE_ICON_256.PNG");
                    SavePkgImage(pictureBoxPkg_256, imageName);
                }

                dirty = false;
            }
        }

        // Create the SPK
        private void CreatePackage(string path)
        {
            var packCmd = Path.Combine(path, "Pack.cmd");
            if (File.Exists(packCmd))
            {
                // Delete existing package if any
                var dir = new DirectoryInfo(path);
                foreach (var file in dir.EnumerateFiles("*.spk"))
                {
                    file.Delete();
                }

                // Execute the script to generate the SPK
                Process pack = new Process();
                pack.StartInfo.FileName = packCmd;
                pack.StartInfo.Arguments = "";
                pack.StartInfo.WorkingDirectory = path;
                pack.StartInfo.UseShellExecute = false;
                pack.StartInfo.RedirectStandardOutput = true;
                pack.StartInfo.CreateNoWindow = true;
                pack.Start();
                pack.WaitForExit(10000);
                if (pack.StartInfo.RedirectStandardOutput) Console.WriteLine(pack.StandardOutput.ReadToEnd());
                if (pack.ExitCode == 2)
                    MessageBoxEx.Show(this, "Creation of the package has failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                // Rename the new Package with its target name
                var packName = Path.Combine(path, info["package"] + ".spk");
                File.Move(Path.Combine(path, "mods.spk"), packName);
            }
            else
            {
                MessageBoxEx.Show(this, "For some reason, required resource files are missing. You will have to reconfigure your destination path", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // Save icons of the SPK
        private void SavePkgImage(PictureBox pictureBox, string path)
        {
            var image = pictureBox.Image;
            if (File.Exists(path))
                File.Delete(path);
            if (image != null) image.Save(path, ImageFormat.Png);

            // TODO: check that PKG images are saved when closing Mods
        }
        #endregion --------------------------------------------------------------------------------------------------------------------------------

        #region Manage List of items --------------------------------------------------------------------------------------------------------------
        private void listViewItems_DoubleClick(object sender, EventArgs e)
        {
            if (list != null && listViewItems.SelectedItems.Count == 1)
            {
                buttonEditItem_Click(sender, e);
            }
        }

        private void listViewItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (state != State.Edit)
            {
                if (list != null && listViewItems.SelectedItems.Count > 0)
                {
                    state = State.View;
                    DisplayDetails((KeyValuePair<string, AppsData>)listViewItems.SelectedItems[0].Tag);
                }
                else
                {
                    state = State.None;
                    DisplayDetails(new KeyValuePair<string, AppsData>(null, null));
                }
            }
        }

        // Add an new item
        private void buttonAddItem_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                state = State.Add;
                DisplayDetails(new KeyValuePair<string, AppsData>(Guid.NewGuid().ToString(), new AppsData()));
                textBoxTitle.Focus();
            }
        }

        // Edit the item currently selected
        private void buttonEditItem_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                state = State.Edit;
                EnableItemDetails();
                textBoxTitle.Focus();
            }
        }

        private void buttonCancelItem_Click(object sender, EventArgs e)
        {
            if (state == State.Add)
                current = new KeyValuePair<string, AppsData>(null, null);

            ResetValidateChildren(this); // Reset Error Validation on all controls

            DisplayItem(current);
        }

        private void buttonSaveItem_Click(object sender, EventArgs e)
        {
            if (ValidateChildren()) // Trigger Error Validation on all controls
            {
                DialogResult answer = DialogResult.No;
                if (!IsItemPicturesLoaded())
                {
                    answer = MessageBoxEx.Show(this, "This item has no icon. Do you want to add icons before saving ?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                }
                if (answer == DialogResult.No)
                {
                    var candidate = GetItemDetails();

                    if (state == State.Edit)
                        candidate.Value.guid = current.Value.guid;

                    bool succeed;
                    succeed = CleanupPreviousItem(current, candidate);
                    if (succeed)
                        succeed = RenamePreviousItem(current, candidate);

                    if (succeed)
                    {
                        SaveItemDetails(candidate);
                        SaveItemsConfig();

                        currentScript = null;
                        currentRunner = null;
                        webAppIndex = null;
                        webAppFolder = null;

                        listViewItems.Focus();
                    }
                    else
                    {
                        MessageBoxEx.Show(this, "The changes couldn't be saved", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                MessageBoxEx.Show(this, "The changes may not be saved as long as you don't fix all issues.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonDeleteItem_Click(object sender, EventArgs e)
        {
            if (current.Key != null)
            {
                var answer = MessageBoxEx.Show(this, string.Format("Do you really want to delete the {0} '{1}' and related icons?", GetItemType(current.Value.itemType), current.Value.title), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (answer == DialogResult.Yes)
                {
                    list.items.Remove(current.Key);
                    BindData(list, null);
                    DeleteItemPictures(current.Value.icon);

                    var cleanedCurrent = Helper.CleanUpText(current.Value.title);
                    switch (current.Value.itemType)
                    {
                        case (int)UrlType.WebApp:
                            DeleteWebApp(cleanedCurrent);
                            break;
                        case (int)UrlType.Script:
                            DeleteScript(cleanedCurrent);
                            break;
                    }

                    DisplayItem();

                    SaveItemsConfig();
                }
            }
        }

        private void comboBoxItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxItemType.SelectedIndex)
            {
                case (int)UrlType.Url: // Url                    
                    this.toolTip4Mods.SetToolTip(this.textBoxUrl, "Type here the URL to be opened when clicking the icon on DSM. if not hosted on Synology, it must start with 'http://' or 'https://'. If hosted on Synology, it must start with a '/'.");
                    break;
                case (int)UrlType.Script: // Script
                    this.toolTip4Mods.SetToolTip(this.textBoxUrl, "Type the Script to be executed when clicking the icon on DSM. DoubleClick to edit.");
                    break;
                case (int)UrlType.WebApp: // WebApp
                    this.toolTip4Mods.SetToolTip(this.textBoxUrl, "Here is the url of your own page to be opened when clicking the icon on DMS. DoubleClick to edit.");
                    break;
            }

            if (comboBoxItemType.Focused)
            {
                ChangeItemType(comboBoxItemType.SelectedIndex);
            }
        }

        /// <summary>
        /// Handle a request to close the Main Form of the application.
        /// This will not be possible if the user is editing the config file or if pending changes couldn't be saved or discarded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (state == State.Add || state == State.Edit)
            {
                // User is currenlty editing a config. He must complete this action first.
                MessageBoxEx.Show(this, "Please, Cancel or Save first the current edition.");
                e.Cancel = true;
            }
            else
            {
                // Close the current Package. This is going to prompt the user to save changes if any. 
                var saved = CloseCurrentPackage(true);

                // Do not close the application if saving failed or was cancelled
                e.Cancel = !(saved == DialogResult.Yes || saved == DialogResult.No);
            }
        }

        private bool RenamePreviousItem(KeyValuePair<string, AppsData> current, KeyValuePair<string, AppsData> candidate)
        {
            bool succeed = true;

            var cleanedCurrent = Helper.CleanUpText(current.Value.title);
            var cleanedCandidate = Helper.CleanUpText(candidate.Value.title);

            if (cleanedCurrent != null)
            {
                //Rename a Script
                if (current.Value.itemType == (int)UrlType.Script && candidate.Value.itemType == (int)UrlType.Script && cleanedCurrent != cleanedCandidate)
                {
                    succeed = RenameScript(candidate.Value.title, cleanedCurrent, cleanedCandidate);
                }

                //Rename a WebApp 
                if (current.Value.itemType == (int)UrlType.WebApp && candidate.Value.itemType == (int)UrlType.WebApp && cleanedCurrent != cleanedCandidate)
                {
                    succeed = RenameWebApp(candidate.Value.title, cleanedCurrent, cleanedCandidate);
                }
            }

            return succeed;
        }

        private bool RenameWebApp(string candidate, string cleanedCurrent, string cleanedCandidate)
        {
            bool succeed = false;
            var answer = DialogResult.Yes;

            if (info["singleApp"] != "yes")
            {
                var existingWebAppFolder = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], cleanedCurrent);
                if (Directory.Exists(existingWebAppFolder))
                {
                    var targetWebAppFolder = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], cleanedCandidate);
                    if (Directory.Exists(targetWebAppFolder))
                    {
                        answer = MessageBoxEx.Show(this, string.Format("Your Package '{0}' already contains a WebApp named {1}.\nDo you confirm that you want to replace it?\nIf you answer No, the existing one will be used. Otherwise, it will be replaced.", info["package"], candidate), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (answer == DialogResult.Yes)
                        {
                            var ex = Helper.DeleteDirectory(targetWebAppFolder);
                            if (ex != null)
                            {
                                MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to delete {0}: {1}", candidate, ex.Message));
                                answer = DialogResult.Abort;
                            }
                        }
                    }
                    if (answer == DialogResult.Yes)
                    {
                        bool retry = true;
                        while (retry)
                        {
                            try
                            {
                                Directory.Move(existingWebAppFolder, targetWebAppFolder);
                                retry = false;
                                succeed = true;
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message == "The process cannot access the file because it is being used by another process.")
                                {
                                    answer = MessageBoxEx.Show(this, string.Format("Your WebApp '{0}' cannot be renamed because its folder is in use.\nDo you want to retry (close first any other application using that folder)?", candidate), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                    retry = (answer == DialogResult.Yes);
                                }
                            }
                        }
                    }
                }
                else
                {
                    succeed = true;
                }
            }
            else
            {
                succeed = true;
            }

            return succeed;
        }

        private bool CleanupPreviousItem(KeyValuePair<string, AppsData> current, KeyValuePair<string, AppsData> candidate)
        {
            bool succeed = true;

            var cleanedCurrent = Helper.CleanUpText(current.Value.title);
            var cleanedCandidate = Helper.CleanUpText(candidate.Value.title);

            //Clean Up a WebApp previously defined if replaced by another type
            if (current.Value.itemType == (int)UrlType.WebApp && candidate.Value.itemType != (int)UrlType.WebApp)
            {
                succeed = DeleteWebApp(cleanedCurrent);
            }

            //Clean Up a Script previously defined if replaced by another type
            if (current.Value.itemType == (int)UrlType.Script && candidate.Value.itemType != (int)UrlType.Script)
            {
                succeed = DeleteScript(cleanedCurrent);
            }

            if (succeed && current.Key != null)
            {
                list.items.Remove(current.Key);
                BindData(list, null);
                DeleteItemPictures(current.Value.icon);
            }

            return succeed;
        }

        private void SaveItemDetails(KeyValuePair<string, AppsData> candidate)
        {
            SaveItemPictures(candidate);

            switch (candidate.Value.itemType)
            {
                case (int)UrlType.Url:
                    break;
                case (int)UrlType.WebApp:
                    CreateWebApp(candidate);
                    break;
                case (int)UrlType.Script:
                    CreateScript(candidate, currentScript, currentRunner);
                    break;
            }

            list.items.Add(candidate.Key, candidate.Value);
            BindData(list, candidate.Value.title);

            DisplayItem(candidate);
        }

        private bool RenameScript(string candidate, string cleanedCurrent, string cleanedCandidate)
        {
            bool succeed = false;
            var answer = DialogResult.Yes;

            if (info["singleApp"] != "yes")
            {
                var current = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], cleanedCurrent);
                if (Directory.Exists(current))
                {
                    var target = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], cleanedCandidate);
                    if (Directory.Exists(target))
                    {
                        answer = MessageBoxEx.Show(this, string.Format("Your Package '{0}' already contains a script named {1}.\nDo you confirm that you want to replace it?\nIf you answer No, the existing one will be used. Otherwise, it will be replaced.", info["package"], candidate), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (answer == DialogResult.Yes)
                        {
                            var ex = Helper.DeleteDirectory(target);
                            if (ex != null)
                            {
                                MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to delete {0}: {1}", target, ex.Message));
                                answer = DialogResult.Abort;
                            }
                        }
                    }
                    if (answer == DialogResult.Yes)
                    {
                        bool retry = true;
                        while (retry)
                        {
                            try
                            {
                                Directory.Move(current, target);
                                retry = false;
                                succeed = true;
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message == "The process cannot access the file because it is being used by another process.")
                                {
                                    answer = MessageBoxEx.Show(this, string.Format("Your WevApp '{0}' cannot be renamed because its folder is in use.\nDo you want to retry (close first any other application using that folder)?", candidate), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                    retry = (answer == DialogResult.Yes);
                                }
                            }
                        }
                    }
                }
                else
                {
                    succeed = true;
                }
            }
            else
            {
                succeed = true;
            }

            return succeed;
        }

        private bool DeleteScript(string cleanedCurrent)
        {
            bool succeed = false;

            if (info["singleApp"] == "yes")
                cleanedCurrent = "";

            var targetScript = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], cleanedCurrent);
            if (Directory.Exists(targetScript))
            {
                var ex = Helper.DeleteDirectory(targetScript);
                if (ex != null)
                {
                    MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to delete {0}: {1}", targetScript, ex.Message));
                }
                else
                {
                    if (string.IsNullOrEmpty(cleanedCurrent)) Directory.CreateDirectory(targetScript);
                    succeed = true;
                }
            }
            else
            {
                succeed = true;
            }
            return succeed;
        }

        private bool DeleteWebApp(string cleanedCurrent)
        {
            bool succeed = false;

            if (info["singleApp"] == "yes")
                cleanedCurrent = "";

            var targetWebApp = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], cleanedCurrent);
            if (Directory.Exists(targetWebApp))
            {
                var ex = Helper.DeleteDirectory(targetWebApp);
                if (ex != null)
                {
                    MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to delete {0}: {1}", targetWebApp, ex.Message));
                }
                else
                {
                    if (string.IsNullOrEmpty(cleanedCurrent)) Directory.CreateDirectory(targetWebApp);
                    succeed = true;
                }
            }
            else
            {
                succeed = true;
            }

            return succeed;
        }

        private void ChangeItemType(int selectedIndex)
        {
            var answer = DialogResult.Yes;
            if (current.Value != null)
            {
                if (current.Value.itemType != -1 && current.Value.itemType != (int)UrlType.Url && current.Value.itemType != selectedIndex)
                {
                    var from = GetItemType(current.Value.itemType);
                    var to = GetItemType(selectedIndex);
                    answer = MessageBoxEx.Show(this, string.Format("Your item '{0}' is currently a {1}.\nDo you confirm that you want to replace it by a new {2}?\nIf you answer Yes, your existing {1} will be deleted when you save your changes.", info["package"], from, to), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                }

                if (answer == DialogResult.No)
                {
                    textBoxTitle.Focus();
                    comboBoxItemType.SelectedIndex = current.Value.itemType;
                    checkBoxLegacy.Checked = (current.Value.type == "legacy");
                }
                else
                {
                    //var previous = current.Value.itemType;
                    //current.Value.itemType = selectedIndex;
                    switch (selectedIndex)
                    {
                        case (int)UrlType.Url:
                            textBoxUrl.Enabled = true;
                            textBoxUrl.ReadOnly = false;
                            if (current.Value.itemType != selectedIndex)
                            {
                                textBoxUrl.Text = "";
                            }
                            textBoxUrl.Focus();
                            current.Value.itemType = selectedIndex;
                            break;

                        case (int)UrlType.Script:
                            var cleanedScriptName = Helper.CleanUpText(textBoxTitle.Text);
                            var targetScriptPath = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], cleanedScriptName, "mods.sh");
                            var targetRunnerPath = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], cleanedScriptName, "mods.php");

                            textBoxUrl.Enabled = true;
                            textBoxUrl.ReadOnly = true;

                            EditScript(targetScriptPath, targetRunnerPath);
                            if (string.IsNullOrEmpty(currentScript))
                            {
                                selectedIndex = current.Value.itemType;
                                comboBoxItemType.SelectedIndex = selectedIndex;
                                checkBoxLegacy.Checked = (current.Value.type == "legacy");
                            }
                            else
                            {
                                var url = "";
                                GetDetailsScript(cleanedScriptName, ref url);
                                current.Value.itemType = selectedIndex;
                                textBoxUrl.Text = url;
                            }
                            break;
                        case (int)UrlType.WebApp:
                            var cleanedWebApp = Helper.CleanUpText(textBoxTitle.Text);
                            var targetWebAppFolder = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], cleanedWebApp);
                            if (Directory.Exists(targetWebAppFolder) && Directory.EnumerateFiles(targetWebAppFolder).Count() > 0)
                            {
                                answer = MessageBoxEx.Show(this, string.Format("Your Package '{0}' already contains a WebApp named {1}.\nDo you want to replace it?", info["package"], textBoxTitle.Text), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            }
                            if (answer == DialogResult.Yes)
                            {
                                if (!EditWebApp())
                                {
                                    selectedIndex = current.Value.itemType;
                                    comboBoxItemType.SelectedIndex = selectedIndex;
                                    checkBoxLegacy.Checked = (current.Value.type == "legacy");
                                }
                                else
                                {
                                    current.Value.itemType = selectedIndex;
                                }
                            }
                            break;
                    }
                    //if (current.Value.itemType != selectedIndex)
                    //{
                    //    current.Value.itemType = selectedIndex;
                    //    ChangeItemType(selectedIndex);
                    //    comboBoxItemType.SelectedIndex = selectedIndex;
                    //}
                }
            }
        }

        private DialogResult EditScript(string scriptPath, string runnerPath)
        {
            string inputScript = null;
            string inputRunner = null;
            if (File.Exists(scriptPath))
                inputScript = File.ReadAllText(scriptPath);
            if (File.Exists(runnerPath))
                inputRunner = File.ReadAllText(runnerPath);
            else
                inputRunner = File.ReadAllText(Path.Combine(Helper.ResourcesDirectory, "default.runner"));

            var script = new ScriptDetails(inputScript, "Script Editor", new Uri("https://www.shellscript.sh/"), "Shell Scripting Tutorial");
            var runner = new ScriptDetails(inputRunner, "Runner Editor", new Uri("https://stackoverflow.com/questions/20107147/php-reading-shell-exec-live-output"), "Reading shell_exec live output in PHP");

            DialogResult result = Helper.ScriptEditor(script, runner, GetAllWizardVariables());
            if (result == DialogResult.OK)
            {
                currentScript = script.Code;
                currentRunner = runner.Code;
            }
            else if (!string.IsNullOrEmpty(inputScript))
            {
                result = DialogResult.OK;
            }
            return result;
        }

        private List<Tuple<string, string>> GetAllWizardVariables()
        {
            List<Tuple<string, string>> variables = null;

            var wizard = Path.Combine(PackageRootPath, "WIZARD_UIFILES");

            if (Directory.Exists(wizard))
            {
                variables = new List<Tuple<string, string>>();
                string line;
                foreach (var filename in Directory.GetFiles(wizard))
                {
                    // Read the file and display it line by line.
                    using (var file = new StreamReader(filename))
                    {
                        while ((line = file.ReadLine()) != null)
                        {
                            Match match = Regex.Match(line, @"""key"".*:.*""(.*)""", RegexOptions.IgnoreCase);
                            if (match.Success)
                            {
                                variables.Add(new Tuple<string, string>(match.Groups[1].Value, string.Format("Variables from Wizard file '{0}'", Path.GetFileName(filename))));
                            }
                        }
                    }
                }
            }

            return variables;
        }

        private bool EditWebApp()
        {
            webpageBrowserDialog4Mods.Title = "Pick the folder containing the sources of your WebApp.";
            var result = webpageBrowserDialog4Mods.ShowDialog();
            if (result)
            {
                openFileDialog4Mods.Title = "Pick the index page.";
                openFileDialog4Mods.InitialDirectory = webpageBrowserDialog4Mods.FileName;
                openFileDialog4Mods.Filter = "html (*.html)|*.html|php (*.php)|*.php|cgi (*.cgi)|*.cgi";
                openFileDialog4Mods.FilterIndex = 2;
                openFileDialog4Mods.FileName = null;
                var files = Directory.GetFiles(webpageBrowserDialog4Mods.FileName).Select(path => Path.GetFileName(path)).ToArray();
                openFileDialog4Mods.FileName = Helper.FindFileIndex(files, "index.php") ?? Helper.FindFileIndex(files, "index.html") ?? Helper.FindFileIndex(files, "index.cgi") ?? Helper.FindFileIndex(files, "default.php") ?? Helper.FindFileIndex(files, "default.html") ?? Helper.FindFileIndex(files, "default.cgi") ?? null;
                if (openFileDialog4Mods.FileName.EndsWith("html")) openFileDialog4Mods.FilterIndex = 1;
                if (openFileDialog4Mods.FileName.EndsWith("php")) openFileDialog4Mods.FilterIndex = 2;
                if (openFileDialog4Mods.FileName.EndsWith("cgi")) openFileDialog4Mods.FilterIndex = 3;

                result = (openFileDialog4Mods.ShowDialog() == DialogResult.OK);
                if (result)
                {
                    webAppIndex = openFileDialog4Mods.FileName;
                    webAppFolder = webpageBrowserDialog4Mods.FileName;

                    if (!webAppIndex.StartsWith(webAppFolder))
                    {
                        MessageBoxEx.Show(this, "This file is not in the directory selected previously. Please select a file under that folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        webAppFolder = null;
                        webAppIndex = null;
                        comboBoxItemType.SelectedIndex = (int)UrlType.Url;
                    }
                    else
                    {
                        textBoxUrl.Text = webAppIndex.Remove(0, webAppFolder.Length + 1);
                    }
                }
            }

            return result;
        }

        private bool CreateWebApp(KeyValuePair<string, AppsData> current)
        {
            bool succeed = true;
            if (webAppIndex != null && webAppFolder != null)
            {
                var cleanedCurrent = Helper.CleanUpText(current.Value.title);
                if (info["singleApp"] == "yes")
                {
                    if (Directory.GetDirectories(webAppFolder).Contains(Path.Combine(webAppFolder, "images")))
                    {
                        MessageBoxEx.Show(this, string.Format("You may not use '{0}' for a single app because it contains a folder named 'images'!", webAppFolder), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        succeed = false;
                    }
                    else if (Directory.GetFiles(webAppFolder).Contains(Path.Combine(webAppFolder, "config")))
                    {
                        MessageBoxEx.Show(this, string.Format("You may not use '{0}' for a single app because it contains a file named 'config'!", webAppFolder), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        succeed = false;
                    }

                    cleanedCurrent = "";
                }
                if (succeed)
                {
                    var targetWebAppFolder = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], cleanedCurrent);

                    // Delete the previous version of this WebApp if any
                    if (Directory.Exists(targetWebAppFolder) && Directory.EnumerateFiles(targetWebAppFolder).Count() > 0)
                    {
                        var ex = Helper.DeleteDirectory(targetWebAppFolder);
                        if (ex != null)
                        {
                            MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to delete {0}: {1}", targetWebAppFolder, ex.Message));
                            succeed = false;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(cleanedCurrent)) Directory.CreateDirectory(targetWebAppFolder);
                        }
                    }

                    if (succeed)
                        Helper.CopyDirectory(webAppFolder, targetWebAppFolder);
                }
            }

            return succeed; //TODO: Handle this return value
        }

        private bool CreateScript(KeyValuePair<string, AppsData> current, string script, string runner)
        {
            bool succeed = true;

            if (script != null)
            {
                var cleanedCurrent = Helper.CleanUpText(current.Value.title);
                if (info["singleApp"] == "yes")
                    cleanedCurrent = "";

                var target = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], cleanedCurrent);
                var targetRunner = Path.Combine(target, "mods.php");
                var targetScript = Path.Combine(target, "mods.sh");

                if (Directory.Exists(target))
                {
                    var ex = Helper.DeleteDirectory(target);
                    if (ex != null)
                    {
                        MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to delete {0}: {1}", target, ex.Message));
                        succeed = false;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(cleanedCurrent)) Directory.CreateDirectory(target);
                    }
                }

                if (succeed)
                {
                    Directory.CreateDirectory(target);

                    // Create sh script (ANSI) to be executed by the php runner script
                    using (TextWriter text = new StreamWriter(targetScript, true, Encoding.GetEncoding(1252)))
                    {
                        text.Write(script);
                    }
                    using (TextWriter text = new StreamWriter(targetRunner, true, Encoding.GetEncoding(1252)))
                    {
                        text.Write(runner);
                    }
                }
            }

            return succeed; //TODO: handle this return value
        }

        private void SaveItemsConfig()
        {
            if (!string.IsNullOrEmpty(PackageRootPath))
            {
                dirty = true;
                Regex rgx;

                var json = JsonConvert.SerializeObject(list, Formatting.Indented, new KeyValuePairConverter());

                //remove default port
                rgx = new Regex("\\s*\"port\": 0");
                json = rgx.Replace(json, "");
                rgx = new Regex(",,");
                json = rgx.Replace(json, ",");

                //remove default protocol
                rgx = new Regex("\\s*\"protocol\": \"\"");
                json = rgx.Replace(json, "");
                rgx = new Regex("\\s*\"protocol\": null");
                json = rgx.Replace(json, "");
                rgx = new Regex(",,");
                json = rgx.Replace(json, ",");

                Properties.Settings.Default.Packages = json;
                Properties.Settings.Default.Save();

                var config = Path.Combine(PackageRootPath, string.Format(CONFIGFILE, info["dsmuidir"]));
                if (Directory.Exists(Path.GetDirectoryName(config)))
                {
                    File.WriteAllText(config, json);
                }
                else
                {
                    MessageBoxEx.Show(this, "For some reason, required resource files are missing. You will have to reconfigure your destination path", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void DisplayNone()
        {
            state = State.None;
            DisplayDetails(new KeyValuePair<string, AppsData>(null, null));
        }

        private void DisplayItem()
        {
            DisplayItem(new KeyValuePair<string, AppsData>());
        }

        // Display an item which becomes the current one. If no item is specified, the first one in the list is used, if any.
        private void DisplayItem(KeyValuePair<string, AppsData> item)
        {
            // If no current Url, select the first one in the list
            if (item.Key == null && listViewItems.Items.Count > 0)
            {
                item = (KeyValuePair<string, AppsData>)listViewItems.Items[0].Tag;
            }

            // If no Url in the list, display none
            if (item.Key == null)
            {
                DisplayNone();
            }
            else
            // Otherwise, display the selected one
            {
                state = State.View;
                var currentGuid = item.Value.guid;
                DisplayDetails(item);

                foreach (ListViewItem other in listViewItems.Items)
                {
                    KeyValuePair<string, AppsData> tag = (KeyValuePair<string, AppsData>)other.Tag;
                    other.Selected = (tag.Value.guid == currentGuid);
                    if (other.Selected)
                        listViewItems.EnsureVisible(other.Index);
                }
            }
        }

        private void DisplayDetails(KeyValuePair<string, AppsData> item)
        {
            currentScript = null;
            currentRunner = null;
            current = item;
            var show = item.Key != null;
            var descText = show ? item.Value.desc : "";
            var titleText = show ? item.Value.title : "";
            var protocolText = show ? item.Value.protocol : "";
            var portText = show ? item.Value.port.ToString() : "0";
            var urlText = show ? item.Value.url : "";
            var users = show ? item.Value.allUsers : false;
            var grantPrivilege = show ? item.Value.grantPrivilege : "";
            var advanceGrantPrivilege = show ? item.Value.advanceGrantPrivilege : false;
            var configablePrivilege = show ? item.Value.configablePrivilege : false;

            if (show)
            {
                if (String.IsNullOrEmpty(protocolText))
                    protocolText = "default";
                if (portText == "0")
                    portText = "default";

                foreach (var size in pictureBoxes.Keys)
                {
                    var picture = GetIconFullPath(item.Value.icon, size);

                    if (File.Exists(picture))
                    {
                        LoadPictureBox(Helper.LoadImageFromFile(picture), size);
                    }
                    else
                    {
                        LoadPictureBox(null, size);
                    }
                }
            }
            else
            {
                if (portText == "0")
                    portText = "";
                foreach (var pictureBox in pictureBoxes.Values)
                {
                    LoadPictureBox(pictureBox, null);
                }
                comboBoxItemType.SelectedIndex = (int)UrlType.Url;
            }

            textBoxDesc.Text = descText;
            textBoxTitle.Text = titleText;
            textBoxUrl.Text = urlText;

            if (!show)
            {
                comboBoxProtocol.Visible = true;
                textBoxPort.Visible = true;
            }
            else if (item.Value.itemType == 0)
            {
                comboBoxProtocol.Visible = textBoxUrl.Text.StartsWith("/");
                textBoxPort.Visible = textBoxUrl.Text.StartsWith("/");
            }
            else
            {
                comboBoxProtocol.Visible = false;
                textBoxPort.Visible = false;
            }

            comboBoxProtocol.SelectedIndex = comboBoxProtocol.FindString(protocolText);
            textBoxPort.Text = portText;
            checkBoxAllUsers.Checked = users;
            checkBoxMultiInstance.Checked = show ? item.Value.allowMultiInstance : false;
            checkBoxLegacy.Checked = show ? item.Value.type == "legacy" : false;

            ComboBoxGrantPrivilege.SelectedIndex = string.IsNullOrEmpty(grantPrivilege) ? -1 : ComboBoxGrantPrivilege.FindString(grantPrivilege);
            checkBoxConfigPrivilege.Checked = configablePrivilege;
            checkBoxAdvanceGrantPrivilege.Checked = advanceGrantPrivilege;

            if (show)
            {
                if (item.Value.itemType != -1 && !Enum.IsDefined(typeof(UrlType), item.Value.itemType))
                {
                    MessageBoxEx.Show(this, "The type of this element is obsolete and must be edited to be fixed !!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    item.Value.itemType = (int)UrlType.Script;
                }
                if (item.Value.itemType == -1)
                    item.Value.itemType = 0;
                comboBoxItemType.SelectedIndex = item.Value.itemType;
            }
            else
            {
                comboBoxItemType.SelectedIndex = (int)UrlType.Url;
            }

            EnableItemDetails();

            listViewItems.Focus();
        }

        private void EnableItemDetails()
        {
            bool enabling;
            bool packaging;

            if (info == null)
            {
                // Disable the detail zone if not package defined
                enabling = false;
                packaging = false;

                EnableItemFieldDetails(!enabling && list != null, enabling);
                EnableItemButtonDetails(enabling, enabling, enabling, enabling, enabling, packaging);

                EnableItemMenuDetails(false, false, false, false, true, true, true, false, false);
            }
            else
            {
                bool add;

                // Enable the detail and package zone depending on the current state (view, new, edit or none)
                enabling = (state != State.View && state != State.None);
                packaging = listViewItems.Items.Count > 0 && !enabling;

                EnableItemFieldDetails(!enabling && list != null, enabling);
                switch (state)
                {
                    case State.View:
                        add = list != null && (info["singleApp"] == "no" || list.items.Count == 0);
                        EnableItemButtonDetails(add, true, false, false, true, packaging);
                        EnableItemMenuDetails(!enabling, true, !enabling, !enabling, true, true, true, true, true);
                        break;
                    case State.None:
                        add = list != null && (info["singleApp"] == "no" || list.items.Count == 0);
                        EnableItemButtonDetails(add, false, false, false, false, packaging);
                        EnableItemMenuDetails(!enabling, true, !enabling, !enabling, true, true, true, true, true);
                        break;
                    case State.Add:
                    case State.Edit:
                        EnableItemButtonDetails(false, false, true, true, false, packaging);
                        EnableItemMenuDetails(!enabling, true, !enabling, !enabling, false, false, false, false, false);
                        break;
                }
                textBoxUrl.ReadOnly = !(comboBoxItemType.SelectedIndex == (int)UrlType.Url);
            }

            if (wizardExist)
                menuAddWizard.Text = "Remove Wizard";
            else
                menuAddWizard.Text = "Create Wizard";


            menuWizardInstallUI.Enabled = wizardExist;
            menuWizardUninstallUI.Enabled = wizardExist;
            menuWizardUpgradeUI.Enabled = wizardExist;
        }

        private void EnableItemMenuDetails(bool packageArea, bool itemsArea, bool menuPackage, bool menuSave, bool menuNew, bool menuOpen, bool menuRecent, bool menuEdit, bool menuClose)
        {
            groupBoxPackage.Enabled = packageArea;
            groupBoxItem.Enabled = itemsArea;
            foreach (ToolStripItem menu in this.menuPackage.DropDownItems)
            {
                menu.Enabled = menuPackage;
            }
            this.menuSavePackage.Enabled = menuSave;
            this.menuNewPackage.Enabled = menuNew;
            this.menuOpenPackage.Enabled = menuOpen;
            menuImportPackage.Enabled = menuOpen;
            menuOpenRecentPackage.Enabled = menuRecent;
            this.menuClosePackage.Enabled = menuClose;
            foreach (ToolStripItem menu in this.menuEdit.DropDownItems)
            {
                menu.Enabled = menuEdit;
            }
        }

        private void EnableItemFieldDetails(bool blistViewItems, bool bItemDetails)
        {
            listViewItems.Enabled = blistViewItems;

            textBoxDesc.Enabled = bItemDetails;
            textBoxTitle.Enabled = bItemDetails;
            textBoxUrl.Enabled = bItemDetails;
            checkBoxAllUsers.Enabled = bItemDetails;
            checkBoxMultiInstance.Enabled = bItemDetails;
            checkBoxLegacy.Enabled = bItemDetails;
            comboBoxItemType.Enabled = bItemDetails;
            comboBoxProtocol.Enabled = bItemDetails;
            textBoxPort.Enabled = bItemDetails;
            ComboBoxGrantPrivilege.Enabled = bItemDetails;
            checkBoxAdvanceGrantPrivilege.Enabled = bItemDetails;
            checkBoxConfigPrivilege.Enabled = bItemDetails;
        }

        private void EnableItemButtonDetails(bool bButtonAdd, bool bButtonEdit, bool bButtonSave, bool bButtonCancel, bool bButtonDelete, bool bbuttonPublish)
        {
            buttonAdd.Enabled = bButtonAdd;
            buttonEdit.Enabled = bButtonEdit;
            buttonSave.Enabled = bButtonSave;
            buttonCancel.Enabled = bButtonCancel;
            buttonDelete.Enabled = bButtonDelete;
            buttonPublish.Enabled = bbuttonPublish;
        }

        // Parse the data of the details zone
        private KeyValuePair<string, AppsData> GetItemDetails()
        {
            bool legacy = checkBoxLegacy.Checked;
            bool multiInstance = checkBoxMultiInstance.Checked;
            var allUsers = checkBoxAllUsers.Checked;
            var title = textBoxTitle.Text.Trim();
            //var dsmName = textBoxDsmAppName.Text.Trim();
            var desc = textBoxDesc.Text.Trim();
            string protocol = "";
            int port = 0;
            string grantPrivilege = ComboBoxGrantPrivilege.SelectedItem == null ? "" : ComboBoxGrantPrivilege.SelectedItem.ToString();
            bool advanceGrantPrivilege = checkBoxAdvanceGrantPrivilege.Checked;
            bool configablePrivilege = checkBoxConfigPrivilege.Checked;

            var url = textBoxUrl.Text.Trim();
            var key = string.Format("{0}.{1}", textBoxDsmAppName.Text, Helper.CleanUpText(title));
            //var key = dsmName;

            var urlType = comboBoxItemType.SelectedIndex;
            switch (urlType)
            {
                case (int)UrlType.Url:
                    //GetDetailsUrl(ref protocol, ref port, ref url);
                    if (url.StartsWith("/"))
                    {
                        int.TryParse(textBoxPort.Text, out port);
                        protocol = comboBoxProtocol.SelectedItem.ToString();
                        if (protocol.Equals("default")) protocol = "";
                    }
                    break;
                case (int)UrlType.WebApp:
                    GetDetailsWebApp(title, ref url);
                    break;
                case (int)UrlType.Script:
                    GetDetailsScript(title, ref url);
                    break;
            }
            var appsData = new AppsData()
            {
                allUsers = allUsers,
                title = title,
                desc = desc,
                protocol = protocol,
                url = url,
                port = port,
                type = legacy ? "legacy" : "url",
                itemType = urlType,
                //appWindow = key,
                allowMultiInstance = multiInstance,
                grantPrivilege = grantPrivilege,
                advanceGrantPrivilege = advanceGrantPrivilege,
                configablePrivilege = configablePrivilege
            };

            title = Helper.CleanUpText(title);
            appsData.icon = string.Format("images/{0}_{1}.png", title, "{0}");
            return new KeyValuePair<string, AppsData>(key, appsData);
        }

        private void GetDetailsScript(string title, ref string url)
        {
            var cleanedScript = Helper.CleanUpText(title);
            string actualUrl = null;
            if (info["singleApp"] == "yes")
                actualUrl = string.Format("/webman/3rdparty/{0}/mods.php", info["package"]);
            else
                actualUrl = string.Format("/webman/3rdparty/{0}/{1}/mods.php", info["package"], cleanedScript);
            if (url != actualUrl)
            {
                url = actualUrl;
            }
        }

        private void GetDetailsWebApp(string title, ref string url)
        {
            var cleanedWebApp = Helper.CleanUpText(title);
            string actualUrl = null;
            if (info["singleApp"] == "yes")
                actualUrl = string.Format("/webman/3rdparty/{0}/{1}", info["package"], url);
            else
                actualUrl = string.Format("/webman/3rdparty/{0}/{1}/{2}", info["package"], cleanedWebApp, url);
            if (webAppIndex != null && webAppFolder != null)
            {
                url = actualUrl;
            }
        }

        //private void GetDetailsUrl(ref string protocol, ref int port, ref string url)
        //{
        //    if (url.StartsWith(":"))
        //    {
        //        var portMatch = getPort.Match(url);
        //        if (portMatch.Success)
        //        {
        //            var value = portMatch.Groups[1].Value;
        //            url = url.Substring(value.Length + 1);
        //            port = int.Parse(value);
        //        }
        //    }

        //    if (url.ToLower().StartsWith("https://:") || url.ToLower().StartsWith("http://:"))
        //    {
        //        url = url.Replace("://:", "://0.0.0.0:");
        //    }

        //    Uri uri;
        //    if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri))
        //    {
        //        if (uri.IsAbsoluteUri)
        //        {
        //            protocol = uri.Scheme;
        //            port = uri.Port;
        //            if (uri.Host == "0.0.0.0")
        //                url = uri.AbsolutePath;
        //        }
        //        else
        //        {
        //            protocol = "";
        //            url = uri.OriginalString;
        //            if (!url.StartsWith("/"))
        //                url = string.Format("/{0}", url);
        //        }
        //    }
        //    else
        //    {
        //        port = 0;
        //        if (!url.StartsWith("/"))
        //            url = string.Format("/{0}", url);
        //    }
        //}
        #endregion --------------------------------------------------------------------------------------------------------------------------------

        #region Manage Icons ----------------------------------------------------------------------------------------------------------------------
        private void pictureBoxPkg_DragEnter(object sender, DragEventArgs e)
        {
            string filename;
            validImage4DragDrop = Helper.GetDragDropFilename(out filename, e);
            if (validImage4DragDrop)
            {
                imageDragDropPath = filename;
                e.Effect = DragDropEffects.Copy;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void pictureBoxPkg_DragDrop(object sender, DragEventArgs e)
        {
            if (validImage4DragDrop)
            {
                var pictureBox = sender as PictureBox;
                dirty = ChangePicturePkg(imageDragDropPath, pictureBox.Tag.ToString().Split(';')[1]);
            }
        }

        private void pictureBoxItem_DragEnter(object sender, DragEventArgs e)
        {
            if (state == State.Add || state == State.Edit)
            {
                string filename;
                validImage4DragDrop = Helper.GetDragDropFilename(out filename, e);
                if (validImage4DragDrop)
                {
                    imageDragDropPath = filename;
                    e.Effect = DragDropEffects.Copy;
                }
                else
                    e.Effect = DragDropEffects.None;
            }
            else
            {
                validImage4DragDrop = false;
                e.Effect = DragDropEffects.None;
            }
        }

        private void pictureBoxItem_DragDrop(object sender, DragEventArgs e)
        {
            if (validImage4DragDrop)
            {
                var pictureBox = sender as PictureBox;
                ChangePictureItem(imageDragDropPath, pictureBox.Tag.ToString().Split(';')[1]);
            }
        }

        private void pictureBoxPkg_DoubleClick(object sender, EventArgs e)
        {
            var picture = sender as PictureBox;
            var size = picture.Tag.ToString().Split(';')[1];

            openFileDialog4Mods.Title = string.Format("Pick a png, jpg or bmp of {0}x{0}", size);
            openFileDialog4Mods.Filter = "jpg (*.jpg)|*.jpg|png (*.png)|*.png|bmp (*.bmp)|*.bmp";
            openFileDialog4Mods.FilterIndex = 2;
            openFileDialog4Mods.FileName = null;
            DialogResult result = openFileDialog4Mods.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var file = openFileDialog4Mods.FileName;
                ChangePicturePkg(file, size);

                Properties.Settings.Default.SourceImages = Path.GetDirectoryName(file);
                Properties.Settings.Default.Save();
            }
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (state == State.Add || state == State.Edit)
            {
                var picture = sender as PictureBox;
                var size = picture.Tag.ToString().Split(';')[1];

                openFileDialog4Mods.Title = string.Format("Pick a png, jpg or bmp of {0}x{0}", size);
                openFileDialog4Mods.Filter = "jpg (*.jpg)|*.jpg|png (*.png)|*.png|bmp (*.bmp)|*.bmp";
                openFileDialog4Mods.FilterIndex = 2;
                openFileDialog4Mods.FileName = null;
                DialogResult result = openFileDialog4Mods.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    string file = openFileDialog4Mods.FileName;
                    ChangePictureItem(file, size);

                    Properties.Settings.Default.SourceImages = Path.GetDirectoryName(file);
                    Properties.Settings.Default.Save();
                }
            }
        }

        private bool ChangePicturePkg(string picture, string size)
        {
            bool result = false;

            Image image = LoadImage(picture);
            if (image != null)
            {
                result = true;
                if (size == "256")
                    LoadPictureBox(pictureBoxPkg_256, image);
                if (size == "72")
                    LoadPictureBox(pictureBoxPkg_72, image);
            }

            return result;
        }

        private void ChangePictureItem(string picture, string size)
        {
            Image image = LoadImage(picture);
            if (image != null)
            {
                if (checkBoxSize.Checked)
                    foreach (var boxSize in pictureBoxes.Keys)
                        LoadPictureBox(image, boxSize);
                else
                    LoadPictureBox(image, size);
            }
        }

        private Image LoadImage(string picture)
        {
            var transparency = int.Parse(comboBoxTransparency.SelectedItem.ToString());
            if (MessageBoxEx.Show(this, "Do you want to make this image transparent?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                transparency = 0;
            }
            else if (transparency == 0)
            {
                MessageBoxEx.Show(this, "You need to pick a transparency value");
                comboBoxTransparency.Focus();
                picture = null;
            }
            Image image = null;

            if (!string.IsNullOrEmpty(picture))
            {
                image = LoadImage(picture, transparency, 256);
            }

            return image;
        }

        private Image LoadImage(string picture, int transparency, int size)
        {
            var copy = new Bitmap(size, size);
            labelToolTip.Text = "";

            if (!File.Exists(picture))
            {
                MessageBoxEx.Show(this, string.Format("Picture '{0}' is missing and can therefore not be loaded ?!", picture), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                copy = Helper.LoadImage(picture, transparency, size);
            }

            return copy;
        }


        // Get the path of Item's icon (to be) saved inside the package
        private string GetIconFullPath(string item, string size)
        {
            var icons = string.Format(item, size).Split('/');
            string picture = null;
            if (info.ContainsKey("dsmuidir"))
                if (icons.Length > 1)
                    picture = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], icons[0], icons[1]);
                else
                    picture = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], icons[0]);
            return picture;
        }

        // Physically delete all images of an item
        private void DeleteItemPictures(string item)
        {
            foreach (var size in pictureBoxes.Keys)
            {
                var path = GetIconFullPath(item, size);
                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        // Load an image in the PictureBox of the specified size, resizing the image to match it
        private void LoadPictureBox(Image image, string size)
        {
            var pictureBox = pictureBoxes[size];
            LoadPictureBox(pictureBox, image);
        }

        // Load an image in a pictureBox, resizing the image to match it
        private void LoadPictureBox(PictureBox pictureBox, Image image)
        {
            if (image == null)
            {
                pictureBox.Image = null;
            }
            else
            {
                var size = int.Parse(pictureBox.Tag.ToString().Split(';')[1]);

                Bitmap copy = ResizeImage(image, size);

                pictureBox.Image = copy;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private static Bitmap ResizeImage(Image image, int size)
        {
            var copy = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(copy))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, size, size);
            }

            return copy;
        }

        // Save all the pictures of the current Item
        private void SaveItemPictures(KeyValuePair<string, AppsData> current)
        {
            string iconName = current.Value.icon;

            foreach (var pictureBox in pictureBoxes)
            {
                var picture = pictureBox.Value;
                var size = pictureBox.Key;
                var image = picture.Image;

                if (image != null)
                {
                    var path = GetIconFullPath(iconName, size);
                    if (!Directory.Exists(Path.GetDirectoryName(path)))
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                    if (File.Exists(path))
                        File.Delete(path);
                    image.Save(path, ImageFormat.Png);

                    path = Path.Combine(Helper.ResourcesDirectory, @"recovery", Path.GetFileName(path));
                    if (!Directory.Exists(Path.GetDirectoryName(path)))
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                    if (File.Exists(path))
                        File.Delete(path);
                    image.Save(path, ImageFormat.Png);
                }
            }
        }

        private bool IsItemPicturesLoaded()
        {
            bool hasIcon = false;
            foreach (var pictureBox in pictureBoxes)
            {
                var picture = pictureBox.Value;
                var image = picture.Image;

                if (image != null)
                {
                    hasIcon = true;
                    break;
                }
            }

            return hasIcon;
        }
        #endregion --------------------------------------------------------------------------------------------------------------------------------

        #region Validations -----------------------------------------------------------------------------------------------------------------------
        private void textBoxPackage_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (string.IsNullOrEmpty(textBoxPackage.Text) && !string.IsNullOrEmpty(textBoxDisplay.Text))
                    textBoxPackage.Text = textBoxDisplay.Text.Replace(' ', '_');

                if (!CheckEmpty(textBoxPackage, ref e, ""))
                {
                    textBoxPackage.Text = textBoxPackage.Text.Replace(' ', '_');
                    var name = textBoxPackage.Text;
                    var cleaned = Helper.CleanUpText(textBoxPackage.Text);
                    if (name != cleaned)
                    {
                        e.Cancel = true;
                        textBoxPackage.Select(0, textBoxPackage.Text.Length);
                        errorProvider.SetError(textBoxPackage, "The name of the package may not contain blanks or special characters.");
                    }
                }
            }
        }
        private void textBoxPackage_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxPackage, "");

            if (textBoxPackage.Enabled)
            {
                if (string.IsNullOrEmpty(textBoxDisplay.Text) && !string.IsNullOrEmpty(textBoxPackage.Text))
                    textBoxDisplay.Text = textBoxPackage.Text.Replace('_', ' ');

                var newName = textBoxPackage.Text;
                var oldName = info.Count > 0 && info.ContainsKey("package") ? info["package"] : newName;
                if (newName != oldName && !string.IsNullOrEmpty(oldName))
                {
                    var focused = Helper.FindFocusedControl(this);

                    var copy = new Package();
                    foreach (var item in list.items)
                    {
                        copy.items.Add(item.Key.Replace(oldName, newName), item.Value);
                    }
                    list = copy;
                    if (!string.IsNullOrEmpty(textBoxDsmAppName.Text))
                        textBoxDsmAppName.Text = textBoxDsmAppName.Text.Replace(oldName, newName);

                    //foreach (string oldSnapshot in Directory.GetFiles(PackageRootPath, "*_screen_*.png"))
                    //{
                    //    var newSnapshot = oldSnapshot.Replace(oldName, newName);
                    //    File.Move(oldSnapshot, newSnapshot);
                    //}

                    oldName = string.Format("/webman/3rdparty/{0}", oldName);
                    newName = string.Format("/webman/3rdparty/{0}", newName);

                    foreach (var item in list.items)
                    {
                        if (item.Value.url.StartsWith(oldName))
                        {
                            item.Value.url = item.Value.url.Replace(oldName, newName);
                        }
                    }
                    BindData(list, null);
                    DisplayItem();
                    focused.Focus();

                    info["package"] = textBoxPackage.Text;
                    dirty = true;
                }

            }
        }

        private void textBoxDisplay_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (string.IsNullOrEmpty(textBoxDisplay.Text) && !string.IsNullOrEmpty(textBoxPackage.Text))
                    textBoxDisplay.Text = textBoxPackage.Text.Replace('_', ' ');
                CheckDoubleQuotes(textBoxDisplay, ref e);
            }
        }

        private void textBoxDisplay_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxDisplay, "");
        }

        private void textBoxMaintainer_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (!CheckEmpty(textBoxMaintainer, ref e, ""))
                {
                    CheckDoubleQuotes(textBoxMaintainer, ref e);
                }
            }
        }

        private void textBoxMaintainer_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxMaintainer, "");
        }

        //private void textBoxPublisher_Validated(object sender, EventArgs e)
        //{
        //    errorProvider.SetError(textBoxPublisher, "");
        //}

        //private void textBoxPublisher_Validating(object sender, CancelEventArgs e)
        //{
        //    if (!CheckEmpty(textBoxPublisher, ref e))
        //    {
        //        CheckDoubleQuotes(textBoxPublisher, ref e);
        //    }
        //}

        private void textBoxDescription_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (!CheckEmpty(textBoxDescription, ref e, ""))
                {
                    CheckDoubleQuotes(textBoxDescription, ref e);
                }
            }
        }

        private void textBoxDescription_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxDescription, "");
        }

        private void textBoxMaintainerUrl_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (!string.IsNullOrEmpty(textBoxMaintainerUrl.Text))
                {
                    CheckUrl(textBoxMaintainerUrl, ref e);
                }
            }
        }

        private void textBoxMaintainerUrl_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxMaintainerUrl, "");
        }

        private void textBoxVersion_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                textBoxVersion.Text = textBoxVersion.Text.Replace("_", "-").Replace("b", ".");
                if (!CheckEmpty(textBoxVersion, ref e, ""))
                {
                    var match = getOldVersion.Match(textBoxVersion.Text);
                    if (match.Success)
                    {
                        textBoxVersion.Text = string.Format("{0}-{1:D4}", match.Groups[1], int.Parse(match.Groups[3].ToString()));
                    }
                    if (!getVersion.IsMatch(textBoxVersion.Text))
                    {
                        e.Cancel = true;
                        textBoxVersion.Select(0, textBoxVersion.Text.Length);
                        errorProvider.SetError(textBoxVersion, "The format of a version must be like 0.0-0000");
                    }
                }
            }
        }

        private void textBoxVersion_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxVersion, "");
        }

        private void textBoxDsmAppName_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (string.IsNullOrEmpty(textBoxDsmAppName.Text) && !string.IsNullOrEmpty(textBoxPackage.Text))
                {
                    textBoxDsmAppName.Text = string.Format("SYNO.SDS._ThirdParty.App.{0}", Helper.CleanUpText(textBoxPackage.Text));
                }
                var name = textBoxDsmAppName.Text.Replace(".", "_").Replace("__", "_");
                var cleaned = Helper.CleanUpText(textBoxDsmAppName.Text);
                if (name != cleaned)
                {
                    e.Cancel = true;
                    textBoxDsmAppName.Select(0, textBoxDsmAppName.Text.Length);
                    errorProvider.SetError(textBoxDsmAppName, "The name of the package may not contain blanks or special characters.");
                }
            }
        }

        private void textBoxDsmAppName_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxDsmAppName, "");
        }

        private void textBoxTitle_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (!CheckEmpty(textBoxTitle, ref e, ""))
                {
                    if (textBoxTitle.Text.Equals("images", StringComparison.InvariantCultureIgnoreCase))
                    {
                        e.Cancel = true;
                        textBoxTitle.Select(0, textBoxTitle.Text.Length);
                        errorProvider.SetError(textBoxTitle, "'images' may not be used as a title");
                    }
                    else
                    {
                        foreach (var url in list.items.Values)
                        {
                            var existingTitle = Helper.CleanUpText(url.title);
                            var newTitle = Helper.CleanUpText(textBoxTitle.Text);
                            if (existingTitle.Equals(newTitle, StringComparison.InvariantCultureIgnoreCase) && current.Value.guid != url.guid)
                            {
                                e.Cancel = true;
                                textBoxTitle.Select(0, textBoxTitle.Text.Length);
                                errorProvider.SetError(textBoxTitle, string.Format("This title is already used for another URL: {0}", url.url));
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void textBoxTitle_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxTitle, "");
            if (textBoxTitle.Enabled)
            {
                var oldName = Helper.CleanUpText(current.Value.title);
                var newName = Helper.CleanUpText(textBoxTitle.Text);
                if (newName != oldName)
                {
                    oldName = string.Format("/webman/3rdparty/{0}/{1}", info["package"], oldName);
                    newName = string.Format("/webman/3rdparty/{0}/{1}", info["package"], newName);

                    textBoxUrl.Text = textBoxUrl.Text.Replace(oldName, newName);
                }
            }
        }

        private void textBoxItem_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (!CheckEmpty(textBoxUrl, ref e, ""))
                {
                    if (comboBoxItemType.SelectedIndex == (int)UrlType.Url)
                    {
                        CheckUrl(textBoxUrl, ref e);
                    }
                }
            }
        }

        private void textBoxItem_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxUrl, "");
        }

        private bool CheckEmpty(TextBox textBox, ref CancelEventArgs e, string defaultValue)
        {
            if (textBox.Enabled && string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = defaultValue;
                e.Cancel = true;
                textBox.Select(0, textBox.Text.Length);
                errorProvider.SetError(textBox, "This field may not be empty.");
            }

            return e.Cancel;
        }

        private void CheckDoubleQuotes(TextBox textBox, ref CancelEventArgs e)
        {
            if (textBox.Text.Contains('"'))
            {
                e.Cancel = true;
                textBox.Select(0, textBox.Text.Length);
                errorProvider.SetError(textBox, "You may not use double quotes in this textbox.");
            }
            if (textBox.Text.Contains("\r\n"))
            {
                e.Cancel = true;
                textBox.Select(0, textBox.Text.Length);
                errorProvider.SetError(textBox, "You may not use CRLF in this textbox.");
            }
        }
        private void CheckUrl(TextBox textBox, ref CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox.Text) && !textBox.Text.StartsWith("/") && !Helper.IsValidUrl(textBox.Text))
            {
                e.Cancel = true;
                textBox.Select(0, textBox.Text.Length);
                errorProvider.SetError(textBox, "You didn't type a well formed http(s) absolute Url.");
            }
        }

        // Reset errors possibly displayed on any Control
        private void ResetValidateChildren(Control control)
        {
            errorProvider.SetError(control, "");
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl != null)
                {
                    ResetValidateChildren(ctrl);
                }
            }
        }
        #endregion --------------------------------------------------------------------------------------------------------------------------------

        private void textBoxItem_DoubleClick(object sender, EventArgs e)
        {
            switch (current.Value.itemType)
            {
                case (int)UrlType.Url:
                    break;
                case (int)UrlType.WebApp:
                    EditWebApp();
                    break;
                case (int)UrlType.Script:
                    var cleanedScriptName = Helper.CleanUpText(textBoxTitle.Text);
                    var oldCleanedScriptName = cleanedScriptName;
                    if (info["singleApp"] == "yes")
                        cleanedScriptName = "";

                    var targetScript = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], cleanedScriptName, "mods.sh");
                    var targetRunner = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], cleanedScriptName, "mods.php");

                    //Upgrade an old "script" versions
                    var target = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], oldCleanedScriptName);
                    if (!Directory.Exists(target))
                    {
                        var oldTargetScript = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], oldCleanedScriptName + ".sh");
                        var oldTargetRunner = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], oldCleanedScriptName + ".php");
                        if (File.Exists(oldTargetScript) && File.Exists(oldTargetRunner))
                        {
                            Directory.CreateDirectory(target);
                            File.Move(oldTargetRunner, targetRunner);
                            File.Move(oldTargetScript, targetScript);

                            current.Value.url = targetRunner;
                        }
                    }

                    EditScript(targetScript, targetRunner);
                    break;
            }
        }

        private string GetItemType(int type)
        {
            string typeName = null;
            switch (type)
            {
                case (int)UrlType.Url: // Url
                    typeName = "Url";
                    break;
                case (int)UrlType.WebApp: // WebApp
                    typeName = "WebApp";
                    break;
                case (int)UrlType.Script: // Script
                    typeName = "Script";
                    break;
            }
            return typeName;
        }

        private void checkBoxSize_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var box in pictureBoxes.Values)
            {
                if (!box.Tag.ToString().EndsWith("MAX"))
                    box.Enabled = !checkBoxSize.Checked;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Close the current Package. This is going to prompt the user to save changes if any. 
            var saved = CloseCurrentPackage();

            // Create a new Package if the user saved/discarded explicitly possible changes
            if (saved == DialogResult.Yes || saved == DialogResult.No)
            {
                NewPackage();
            }
        }

        /// <summary>
        /// Prompt the user to save the Package if flagged as dirty or if there are any pending changes.
        /// </summary>
        /// <param name="path">Target save location.</param>
        /// <param name="force">Pending changes are saved without prompting the user if this parameter is true.</param>
        /// <returns>
        /// Yes if the package is successfuly saved.
        /// No if the user discarded the pending changes or if there was no pending changes.
        /// Cancel if the user asked to cancel the operation.
        /// Abort if something went wrong or if the user might not save the package due to invalid data.
        /// </returns>
        private DialogResult SavePackage(string path, bool force = false)
        {
            using (new CWaitCursor(labelToolTip, "PLEASE WAIT WHILE SAVING YOUR PACKAGE..."))
            {
                DialogResult saved = DialogResult.Yes;
                if (info == null)
                {
                    // No package to be saved
                    saved = DialogResult.No;
                }
                else
                {
                    try
                    {
                        if (!ValidateChildren())
                        {
                            // User may not save changes with Invalid data
                            saved = DialogResult.Abort;
                        }
                        else
                        {
                            if (dirty || CheckChanges(null))
                            {
                                // Prompt the user to save pending changes. He may answer Yes, No or Cancel
                                if (!force) saved = MessageBoxEx.Show(this, "Do you want to save pending changes in your package?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                                if (saved == DialogResult.Yes)
                                {
                                    if (info.ContainsKey("checksum")) info.Remove("checksum");
                                    SaveItemsConfig();
                                    SavePackageInfo(path);
                                    SavePackageSettings();
                                    CreateRecentsMenu();
                                    dirty = false;
                                }
                            }
                            else
                            {
                                // Nothing needs to be saved
                                saved = DialogResult.No;
                            }

                            // Pending Changes successfuly saved or discarded
                            if (saved != DialogResult.Cancel) ResetEditScriptMenuIcons();
                        }
                    }
                    catch { saved = DialogResult.Abort; }
                }
                return saved;
            }
        }

        private void SavePackageSettings()
        {
            if (Properties.Settings.Default.Recents != null)
            {
                RemoveRecentPath(PackageRootPath);
                if (Properties.Settings.Default.Recents.Count >= 10)
                {
                    Properties.Settings.Default.Recents.RemoveAt(0);
                    Properties.Settings.Default.RecentsName.RemoveAt(0);
                }
            }
            else
            {
                Properties.Settings.Default.Recents = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.RecentsName = new System.Collections.Specialized.StringCollection();
            }

            Properties.Settings.Default.Recents.Add(PackageRootPath);
            if (info.ContainsKey("displayname") && !string.IsNullOrEmpty(info["displayname"]))
                Properties.Settings.Default.RecentsName.Add(info["displayname"]);
            else if (info.ContainsKey("package") && !string.IsNullOrEmpty(info["package"]))
                Properties.Settings.Default.RecentsName.Add(info["package"]);
            else
                Properties.Settings.Default.RecentsName.Add(Path.GetFileName(PackageRootPath));

            Properties.Settings.Default.LastPackage = PackageRootPath;
            Properties.Settings.Default.Save();
        }

        private void RemoveRecentPath(string path)
        {
            for (var item = 0; item < Properties.Settings.Default.Recents.Count; item++)
            {
                if (Properties.Settings.Default.Recents[item] == path)
                {
                    if (Properties.Settings.Default.Recents.Count > item)
                        Properties.Settings.Default.Recents.RemoveAt(item);
                    if (Properties.Settings.Default.RecentsName.Count > item)
                        Properties.Settings.Default.RecentsName.RemoveAt(item);
                }
            }
        }

        private bool CheckChanges(List<Tuple<string, string, string>> changes)
        {
            bool dirty = false;

            if (info != null)
                foreach (var control in groupBoxPackage.Controls)
                {
                    var textBox = control as TextBox;
                    if (textBox != null && textBox.Tag != null && textBox.Tag.ToString().StartsWith("PKG"))
                    {
                        var keys = textBox.Tag.ToString().Split(';');
                        var key = keys[0].Substring(3);

                        if (info.ContainsKey(key))
                        {
                            dirty = textBox.Text != info[key];
                            if (dirty && changes != null) changes.Add(new Tuple<string, string, string>(key, info[key], textBox.Text));
                        }
                        else
                        {
                            dirty = !string.IsNullOrEmpty(textBox.Text);
                            if (dirty && changes != null) changes.Add(new Tuple<string, string, string>(key, "", textBox.Text));
                        }

                        if (changes != null) dirty = false;

                        if (dirty)
                            break;
                    }
                    var checkBox = control as CheckBox;
                    if (checkBox != null && checkBox.Tag != null && checkBox.Tag.ToString().StartsWith("PKG"))
                    {
                        var keys = checkBox.Tag.ToString().Split(';');
                        var key = keys[0].Substring(3);

                        if (info.ContainsKey(key))
                        {
                            dirty = checkBox.Checked != (info[key] == "yes");
                            if (dirty && changes != null) changes.Add(new Tuple<string, string, string>(key, info[key], checkBox.Checked ? "yes" : "no"));
                        }

                        if (changes != null) dirty = false;

                        if (dirty)
                            break;
                    }
                    var comboBox = control as ComboBox;
                    if (comboBox != null && comboBox.Tag != null && comboBox.Tag.ToString().StartsWith("PKG"))
                    {
                        var keys = comboBox.Tag.ToString().Split(';');
                        var key = keys[0].Substring(3);

                        if (info.ContainsKey(key))
                        {
                            dirty = comboBox.Text != info[key];
                            if (dirty && changes != null) changes.Add(new Tuple<string, string, string>(key, info[key], comboBox.Text));
                        }
                        else
                        {
                            dirty = !string.IsNullOrEmpty(comboBox.Text);
                            if (dirty && changes != null) changes.Add(new Tuple<string, string, string>(key, "", comboBox.Text));
                        }

                        if (changes != null) dirty = false;

                        if (dirty)
                            break;
                    }
                }

            if (changes != null) dirty = changes.Count > 0;

            return dirty;
        }

        private bool ResetPackage()
        {
            bool succeed = true;
            warnings.Clear();

            if (info != null)
            {
                var resetPackage = PackageRootPath;
                var answer = MessageBoxEx.Show(this, "Do you really want to reset the complete Package?\r\n\r\nThis cannot be undone!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (answer == DialogResult.Yes)
                {
                    string packageName = "";
                    if (info.ContainsKey("package"))
                        packageName = info["package"];
                    try
                    {
                        if (string.IsNullOrEmpty(resetPackage))
                            MessageBoxEx.Show(this, "The path of the Package is not defined. Create a new package instead.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else
                        {
                            if (Directory.Exists(resetPackage))
                            {
                                // Close without trying to save any pending changes.
                                CloseCurrentPackage(false);

                                var ex = Helper.DeleteDirectory(resetPackage);
                                if (ex != null)
                                {
                                    MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to delete {0}: {1}", PackageRootPath, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    succeed = false;
                                }
                            }

                            if (succeed)
                            {
                                NewPackage(resetPackage);
                                textBoxPackage.Text = packageName;
                            }
                        }
                    }
                    catch
                    {
                        MessageBoxEx.Show(this, "The destination folder cannot be cleaned. The package file is possibly in use?!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBoxEx.Show(this, "There is no Package loaded.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            textBoxDisplay.Focus();

            return succeed; //TODO: Handle this return value;
        }

        private void NewPackage(string path = null)
        {
            DialogResult result = DialogResult.Cancel;
            warnings.Clear();

            if (path == null)
            {
                if (Properties.Settings.Default.DefaultPackageRoot && Directory.Exists(Properties.Settings.Default.PackageRoot))
                {
                    path = Path.Combine(Properties.Settings.Default.PackageRoot, Guid.NewGuid().ToString());
                    result = DialogResult.Yes;
                }
                else
                {
                    result = GetPackagePath(ref path);
                }
            }
            else
            {
                result = IsPackageFolderEmpty(path);
            }

            if (result == DialogResult.No)
            {
                var answer = MessageBoxEx.Show(this, "This folder already contains a Package. Do you want to load it?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (answer == DialogResult.Yes)
                    OpenPackage(path);
            }
            else if (result == DialogResult.Abort)
            {
                MessageBoxEx.Show(this, "This folder already contains some Files. Please, select an empty Folder.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (result != DialogResult.Cancel && result != DialogResult.Abort)
            {
                PackageRootPath = path;

                ResetValidateChildren(this); // Reset Error Validation on all controls

                InitialConfiguration(path);
                LoadPackageInfo(path);
                BindData(list, null);
                DisplayItem();

                ResetEditScriptMenuIcons();
            }
        }

        private void GeneratePackage(string path)
        {
            warnings.Clear();

            // This is required to insure that changes (mainly icons) are correctly applied when installing the package in DSM
            textBoxVersion.Text = Helper.IncrementVersion(textBoxVersion.Text);

            using (new CWaitCursor(labelToolTip, "PLEASE WAIT WHILE GENERATING YOUR PACKAGE..."))
            {
                SavePackageInfo(path);

                // Create the SPK
                CreatePackage(path);
            }

            ResetEditScriptMenuIcons();
        }

        private void PublishPackage(string PackagePath, string PackageRepo)
        {
            var packName = info["package"];

            try
            {
                publishFile(Path.Combine(PackagePath, packName + ".spk"), Path.Combine(PackageRepo, packName + ".spk"));
                MessageBoxEx.Show(this, "The package has been successfuly published.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to copy files: {0}", ex.Message));
            }
        }
        private void publishFile(string src, string dest)
        {
            if (File.Exists(dest))
                File.Delete(dest);

            File.Copy(src, dest);
        }

        private bool ExtractPackage(string path)
        {
            bool result = true;

            var unpackCmd = Path.Combine(path, "Unpack.cmd");
            if (File.Exists(unpackCmd))
            {
                using (new CWaitCursor(labelToolTip, "PLEASE WAIT WHILE DEFLATING YOUR PACKAGE..."))
                {
                    // Execute the script to generate the SPK
                    Process unpack = new Process();
                    unpack.StartInfo.FileName = unpackCmd;
                    unpack.StartInfo.Arguments = "";
                    unpack.StartInfo.WorkingDirectory = path;
                    unpack.StartInfo.UseShellExecute = true; // required to run as admin
                    unpack.StartInfo.RedirectStandardOutput = false; // may not read from standard output when run as admin
                    unpack.StartInfo.CreateNoWindow = true; // Does not work if UseShellExecute = true
                    unpack.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    unpack.StartInfo.Verb = "runas"; //Required to run as admin as some packages have "symlink" resulting in "ERROR: Can not create symbolic link : Access is denied."
                    unpack.Start();
                    unpack.WaitForExit(30000);
                    if (unpack.StartInfo.RedirectStandardOutput) Console.WriteLine(unpack.StandardOutput.ReadToEnd());
                    if (unpack.ExitCode == 2)
                    {
                        result = false;
                        MessageBoxEx.Show(this, "Extraction of the package has failed. Possibly try to run Unpack.cmd as Administrator in your package folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //As the package has been run as admin, Users don't have full control access
                    GrantAccess(path);
                }
            }
            else
            {
                MessageBoxEx.Show(this, "For some reason, required resource files are missing. Your package can't be extracted", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                result = false;
            }
            return result;
        }

        private void GrantAccess(string fullPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(fullPath);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
        }

        private void ResetEditScriptMenuIcons()
        {
            foreach (ToolStripItem menu in menuEdit.DropDownItems)
            {
                menu.Image = null;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Close the current Package. This is going to prompt the user to save changes if any. 
            var saved = CloseCurrentPackage();

            // Open another Package if the user saved/discarded explicitly pending changes.
            if (saved == DialogResult.Yes || saved == DialogResult.No)
                OpenPackage();
        }

        private bool OpenPackage(string path = null, bool import = false)
        {
            bool succeed = true;

            previousReportUrl = "";

            DialogResult result = DialogResult.Cancel;
            if (path == null)
            {
                folderBrowserDialog4Mods.Title = "Pick a folder to store the new Package or a folder containing an existing Package.";
                if (Properties.Settings.Default.DefaultPackageRoot)
                    folderBrowserDialog4Mods.InitialDirectory = Properties.Settings.Default.PackageRoot;
                else
                    folderBrowserDialog4Mods.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                result = GetPackagePath(ref path);
            }
            else
            {
                result = IsPackageFolderEmpty(path, import);
            }

            if (result == DialogResult.Yes)
            {
                result = MessageBoxEx.Show(this, "The Folder does not contain any valid package.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                result = DialogResult.Cancel;
                succeed = false;
            }
            else if (result == DialogResult.Abort)
            {
                MessageBoxEx.Show(this, "The selected Folder does not contain a valid Package or contains unsupported features.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                succeed = false;
            }
            if (result != DialogResult.Cancel && result != DialogResult.Abort)
            {
                PackageRootPath = path;
                Properties.Settings.Default.LastPackage = PackageRootPath;
                Properties.Settings.Default.Save();

                ResetValidateChildren(this); // Reset Error Validation on all controls

                if (result == DialogResult.Yes)
                {
                    InitialConfiguration(path);
                }

                LoadPackageInfo(path);
                BindData(list, null);
                CopyPackagingBinaries(path);
                DisplayItem();

                ResetEditScriptMenuIcons();
            }

            return succeed;
        }

        private void CopyPackagingBinaries(string path)
        {
            if (File.Exists(Path.Combine(path, "7z.exe")))
                File.Delete(Path.Combine(path, "7z.exe"));
            File.Copy(Path.Combine(Helper.ResourcesDirectory, "7z.exe"), Path.Combine(path, "7z.exe"));
            if (File.Exists(Path.Combine(path, "7z.dll")))
                File.Delete(Path.Combine(path, "7z.dll"));
            File.Copy(Path.Combine(Helper.ResourcesDirectory, "7z.dll"), Path.Combine(path, "7z.dll"));
            if (File.Exists(Path.Combine(path, "Pack.cmd")))
                File.Delete(Path.Combine(path, "Pack.cmd"));
            File.Copy(Path.Combine(Helper.ResourcesDirectory, "Pack.cmd"), Path.Combine(path, "Pack.cmd"));
            if (File.Exists(Path.Combine(path, "Unpack.cmd")))
                File.Delete(Path.Combine(path, "Unpack.cmd"));
            File.Copy(Path.Combine(Helper.ResourcesDirectory, "Unpack.cmd"), Path.Combine(path, "Unpack.cmd"));
            if (File.Exists(Path.Combine(path, "Mods.exe")))
                File.Delete(Path.Combine(path, "Mods.exe"));
            File.Copy(Assembly.GetEntryAssembly().Location, Path.Combine(path, "Mods.exe"));
        }

        // Return Yes to create a new package
        // Return No to load an existing package
        // Return Cancel to neither create nor load a package
        private DialogResult GetPackagePath(ref string path)
        {
            DialogResult getPackage = DialogResult.Cancel;

            var result = folderBrowserDialog4Mods.ShowDialog();
            if (result)
            {
                path = folderBrowserDialog4Mods.FileName;
                if (Directory.Exists(path))
                {
                    getPackage = IsPackageFolderEmpty(path);
                }
                else
                {
                    MessageBoxEx.Show(this, string.Format("Something went wrong when picking the folder {0}", path), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return getPackage;
        }

        private DialogResult IsPackageFolderEmpty(string path, bool import = false)
        {
            DialogResult createNewPackage = DialogResult.Yes; //Folder Empty and ok to create a new package
            if (Directory.Exists(path))
            {
                var spkList = Directory.GetFiles(path, "*.spk");
                if (spkList.Length == 1) // Folder already contains spk.
                {
                    var info = Path.Combine(path, "INFO");
                    if (!File.Exists(info))
                    {
                        // spk found but no INFO file. Ask the user to deflate the spk.
                        if (import || MessageBoxEx.Show(this, "This folder contains a package but no INFO file. Do you want to deflate the package ?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            CopyPackagingBinaries(path);
                            if (ExtractPackage(path))
                                createNewPackage = DialogResult.No;
                            else
                                createNewPackage = DialogResult.Abort;
                        }
                        else
                        {
                            createNewPackage = DialogResult.Abort;
                        }
                    }
                }
                else if (spkList.Length > 1)
                {
                    MessageBoxEx.Show(this, "This folder contains several packages. Operation will abort.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    createNewPackage = DialogResult.Abort;
                }

                if (createNewPackage != DialogResult.Abort)
                {
                    string uiDir = null;
                    FileInfo info = new FileInfo(Path.Combine(path, "INFO"));
                    if (info.Exists)
                    {
                        uiDir = GetUIDir(path);
                    }

                    var content = Directory.GetDirectories(path).ToList();
                    content.AddRange(Directory.GetFiles(path));
                    if (content.Count > 0)
                    {
                        content.Remove(Path.Combine(path, "Mods.exe"));

                        content.Remove(Path.Combine(path, "7z.dll"));
                        content.Remove(Path.Combine(path, "7z.exe"));
                        content.Remove(Path.Combine(path, "Pack.cmd"));
                        content.Remove(Path.Combine(path, "Unpack.cmd"));

                        content.Remove(Path.Combine(path, "INFO"));
                        content.Remove(Path.Combine(path, "PACKAGE_ICON.PNG"));
                        content.Remove(Path.Combine(path, "PACKAGE_ICON_120.PNG")); //?? Found in some packages
                        content.Remove(Path.Combine(path, "PACKAGE_ICON_256.PNG"));
                        content.Remove(Path.Combine(path, "package"));
                        content.Remove(Path.Combine(path, "scripts"));
                        content.Remove(Path.Combine(path, "WIZARD_UIFILES"));

                        string syno_signature = Path.Combine(path, "syno_signature.asc");
                        content.Remove(syno_signature); // Ignored
                        File.Delete(syno_signature);

                        content.Remove(Path.Combine(path, "LICENSE"));
                        content.Remove(Path.Combine(path, "CHANGELOG"));

                        if (!string.IsNullOrEmpty(uiDir))
                        {
                            content.Remove(Path.Combine(path, uiDir));
                        }

                        var remaining = content.ToList();
                        foreach (var other in remaining)
                        {
                            if (other.EndsWith(".spk"))
                            {
                                content.Remove(other);
                            }
                            else if (other.EndsWith(".png") && Path.GetFileNameWithoutExtension(other).StartsWith("screen_"))
                            {
                                content.Remove(other);
                            }
                            else
                            {
                                Debug.Print("Package Folder contains unknow '{0}'", other);
                            }
                        }
                        if (content.Count > 0)
                        {
                            if (info.Exists)
                            {
                                // Folder contains an existing "extracted" Package (not a single spk file) with some unknow files
                                warnings.Add("This Package contains unknown elements:" + Environment.NewLine + content.Aggregate((i, j) => i.Replace(path, "") + Environment.NewLine + j.Replace(path, "")));
                                createNewPackage = DialogResult.No;
                            }
                            else
                            {
                                // Folder contains something else than a package
                                createNewPackage = DialogResult.Abort;
                            }
                        }
                        else
                        {
                            // Folder contains an existing "extracted" Package (not a single spk file)
                            createNewPackage = DialogResult.No;
                        }
                    }
                    else
                    {
                        // Folder is empty
                        createNewPackage = DialogResult.Yes;
                    }
                }
            }
            return createNewPackage;
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetPackage();
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                GeneratePackage(PackageRootPath);
                var answer = MessageBoxEx.Show(this, string.Format("Your Package '{0}' is ready in {1}.\nDo you want to open that folder now?", info["package"], PackageRootPath), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (answer == DialogResult.Yes)
                {
                    Process.Start(PackageRootPath);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!dirty && !CheckChanges(null))
                MessageBoxEx.Show(this, "There is no pending changes to be saved.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Do not prompt the user to save changes and just do it! 
            var saved = SavePackage(PackageRootPath, true);

            // Some Changes have been saved.
            if (saved == DialogResult.Yes)
                MessageBoxEx.Show(this, "Package saved.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MenuItemOpenRecent_ClickHandler(object sender, EventArgs e)
        {
            // Close the current Package. This is going to prompt the user to save changes if any. 
            var saved = CloseCurrentPackage();

            // Open the requested Package if the user saved/discarded explicitly pending changes.
            if (saved == DialogResult.Yes || saved == DialogResult.No)
            {
                ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
                String path = clickedItem.Tag.ToString();
                if (!OpenPackage(path))
                {
                    // if the package cannot be opened, remove it from the menu 'Open Recent'
                    RemoveRecentPath(path);
                    Properties.Settings.Default.Save();
                    CreateRecentsMenu();
                }
            }
        }

        private void scriptRunnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var defaultRunnerPath = Path.Combine(Helper.ResourcesDirectory, "default.runner");

            var content = File.ReadAllText(defaultRunnerPath);
            var runner = new ScriptDetails(content, "Default Runner", new Uri("https://stackoverflow.com/questions/20107147/php-reading-shell-exec-live-output"), "Reading shell_exec live output in PHP");
            DialogResult result = Helper.ScriptEditor(null, runner, null);
            if (result == DialogResult.OK)
            {
                File.WriteAllText(defaultRunnerPath, runner.Code);
                menuScriptRunner.Image = new Bitmap(Properties.Resources.EditedScript);
            }
        }

        private void scriptEditMenuItem_Click(object sender, EventArgs e)
        {
            var menu = (ToolStripMenuItem)sender;
            var scriptName = menu.Tag.ToString();
            var scriptPath = Path.Combine(PackageRootPath, "scripts", scriptName);

            if (!File.Exists(scriptPath))
                MessageBoxEx.Show(this, "This Script cannot be found. Please Reset your Package.");
            else
            {
                var content = File.ReadAllText(scriptPath);
                var script = new ScriptDetails(content, menu.Text, new Uri("https://developer.synology.com/developer-guide/synology_package/scripts.html"), "Details about script files");
                DialogResult result = Helper.ScriptEditor(script, null, GetAllWizardVariables());
                if (result == DialogResult.OK)
                {
                    File.WriteAllText(scriptPath, script.Code);
                    menu.Image = new Bitmap(Properties.Resources.EditedScript);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Trigger the Close event on the Main Form. (See MainForm_FormClosing)
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new AboutBox();
            about.StartPosition = FormStartPosition.CenterParent;
            about.ShowDialog(this);
        }

        private void addWizardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var wizard = Path.Combine(PackageRootPath, "WIZARD_UIFILES");
            if (Directory.Exists(wizard))
            {
                var result = MessageBoxEx.Show(this, "Are you sure you want to delete your wizard?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    wizardExist = false;
                    var ex = Helper.DeleteDirectory(wizard);
                    if (ex != null)
                    {
                        MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to delete {0}: {1}", wizard, ex.Message));
                        wizardExist = true;
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(wizard);
                MessageBoxEx.Show(this, "You can now Edit wizards to install/upgrade/uninstall this package.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                wizardExist = true;
            }
            EnableItemDetails();
        }

        private void wizardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.Cancel;
            var menu = (ToolStripMenuItem)sender;
            var json = menu.Tag.ToString();
            var jsonPath = Path.Combine(PackageRootPath, "WIZARD_UIFILES", json);

            if (!File.Exists(jsonPath))
            {
                jsonPath = jsonPath + ".sh";
                if (!File.Exists(jsonPath))
                    jsonPath = null;
            }

            if (jsonPath == null)
            {
                result = MessageBoxEx.Show(this, "Do you want to create a standard json wizard? (Answering 'No' will create a dynamic wizard using a shell script)", "Type of Wizard", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    jsonPath = Path.Combine(PackageRootPath, "WIZARD_UIFILES", json + ".sh");
                else if (result == DialogResult.Yes)
                    jsonPath = Path.Combine(PackageRootPath, "WIZARD_UIFILES", json);

                if (jsonPath != null)
                    using (File.CreateText(jsonPath))
                    { }
            }

            if (jsonPath != null)
            {
                if (Path.GetExtension(jsonPath) == ".sh")
                {
                    var content = File.ReadAllText(jsonPath);
                    var wizard = new ScriptDetails(content, menu.Text, new Uri("https://developer.synology.com/developer-guide/synology_package/WIZARD_UIFILES.html"), "Details about Wizard File");

                    string outputRunner = string.Empty;
                    result = Helper.ScriptEditor(wizard, null, null);
                    if (result == DialogResult.OK)
                    {
                        File.WriteAllText(jsonPath, wizard.Code);
                        menu.Image = new Bitmap(Properties.Resources.EditedScript);
                    }
                }
                else
                {
                    var jsonEditor = new JsonEditorMainForm();

                    try
                    {
                        jsonEditor.OpenFile(jsonPath);
                        jsonEditor.Title = menu.Text;
                        jsonEditor.StartPosition = FormStartPosition.CenterParent;
                        result = jsonEditor.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            menu.Image = new Bitmap(Properties.Resources.EditedScript);
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBoxEx.Show(this, "You may not open this file due to security restriction. Try to run this app as Administrator or grant 'Modify' on this file to the group USERS.", "Security Issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void textBoxFirmware_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (!CheckEmpty(textBoxFirmware, ref e, ""))
                {
                    if (getOldFirmwareVersion.IsMatch(textBoxFirmware.Text))
                    {
                        var parts = textBoxFirmware.Text.Split('.');
                        textBoxFirmware.Text = string.Format("{0}.{1}-{2:D4}", parts[0], parts[1], int.Parse(parts[2]));
                    }
                    if (getShortFirmwareVersion.IsMatch(textBoxFirmware.Text))
                    {
                        var parts = textBoxFirmware.Text.Split('.');
                        textBoxFirmware.Text = string.Format("{0}.{1}-0000", parts[0], parts[1]);
                    }
                    if (!getFirmwareVersion.IsMatch(textBoxFirmware.Text))
                    {
                        e.Cancel = true;
                        textBoxFirmware.Select(0, textBoxFirmware.Text.Length);
                        errorProvider.SetError(textBoxFirmware, "The format of a firmware must be like 0.0-0000");
                    }
                    else
                    {
                        var parts = textBoxFirmware.Text.Split(new char[] { '.', '-' });
                        if (int.Parse(parts[2]) == 0)
                            textBoxFirmware.Text = string.Format("{0}.{1}", parts[0], parts[1]);
                        else
                            textBoxFirmware.Text = string.Format("{0}.{1}-{2:D4}", parts[0], parts[1], int.Parse(parts[2]));
                    }
                }
            }
        }
        private void textBoxFirmware_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxFirmware, "");
        }

        private void checkBoxBeta_CheckedChanged(object sender, EventArgs e)
        {
            TextBoxReportUrl.Visible = checkBoxBeta.Checked & !checkBoxSupportCenter.Checked;
            labelReportUrl.Visible = TextBoxReportUrl.Visible;
            if (!TextBoxReportUrl.Visible)
            {
                if (TextBoxReportUrl.Text != "")
                    previousReportUrl = TextBoxReportUrl.Text;
                TextBoxReportUrl.Text = "";
            }
            else if (previousReportUrl != null)
                TextBoxReportUrl.Text = previousReportUrl;
        }

        private void TextBoxReportUrl_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (!string.IsNullOrEmpty(TextBoxReportUrl.Text))
                {
                    CheckUrl(TextBoxReportUrl, ref e);
                }
            }
        }

        private void textBoxHelpUrl_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (!string.IsNullOrEmpty(textBoxHelpUrl.Text))
                {
                    CheckUrl(textBoxHelpUrl, ref e);
                }
            }
        }

        private void TextBoxReportUrl_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(TextBoxReportUrl, "");
        }

        private void textBoxHelpUrl_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxHelpUrl, "");
        }

        private void textBoxPublisherUrl_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxPublisherUrl, "");
        }

        private void textBoxPublisherUrl_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (!string.IsNullOrEmpty(textBoxPublisherUrl.Text))
                {
                    CheckUrl(textBoxPublisherUrl, ref e);
                }
            }
        }

        private void buttonAdvanced_Click(object sender, EventArgs e)
        {
            string content = null; ;

            SavePackageInfo(PackageRootPath);
            var infoName = Path.Combine(PackageRootPath, "INFO");
            content = File.ReadAllText(infoName);
            var script = new ScriptDetails(content, "INFO Editor", new Uri("https://developer.synology.com/developer-guide/synology_package/INFO.html"), "Details about INFO settings");

            Properties.Settings.Default.AdvancedEditor = true;
            Properties.Settings.Default.Save();
            ShowAdvancedEditor(true);

            var configName = Path.Combine(PackageRootPath, string.Format(CONFIGFILE, info["dsmuidir"]));
            if (File.Exists(configName))
            {
                content = File.ReadAllText(configName);
                content = Helper.JsonPrettify(content);
            }
            else
                content = null;


            var config = new ScriptDetails(content, "Config Editor", new Uri("https://developer.synology.com/developer-guide/integrate_dsm/config.html"), "Details about Config settings");


            DialogResult result = Helper.ScriptEditor(script, config, null);
            if (result == DialogResult.OK)
            {
                File.WriteAllText(infoName, script.Code);
                File.WriteAllText(configName, config.Code);
                LoadPackageInfo(PackageRootPath);
                BindData(list, null);
                DisplayItem();
            }
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(PackageRootPath)))
                Process.Start(PackageRootPath);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PackageRootPath) && MessageBoxEx.Show(this, "Are you sure that ou want to delete this Package?\r\n\r\nThis cannot be undone!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                RemoveRecentPath(PackageRootPath);
                Properties.Settings.Default.Save();
                CreateRecentsMenu();
                var ex = Helper.DeleteDirectory(PackageRootPath);
                if (ex != null)
                {
                    MessageBoxEx.Show(this, string.Format("A Fatal error occured while trying to delete {0}: {1}", PackageRootPath, ex.Message), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                // Close the current Package without trying to save pending changes. 
                CloseCurrentPackage(false);
            }
        }

        /// <summary>
        /// Prompt the user to save pending changes if any and close the current package.
        /// </summary>
        /// <param name="trySavingPendingChange">Do not try to save any pending changes if this parameter is false.</param>
        /// <param name="forceSavingPendingChange">Pending changes are saved without prompting the user if this parameters is true.</param>
        /// <returns>
        /// Yes if the pending changes were succesfuly saved.
        /// No if the user discarded the pending changes, if there was no pending changes or if the user was not prompted.
        /// Cancel if the user cancelled the saving.
        /// Abort if anything went wrong or if pending changes were not valid and couldn't be saved.
        /// </returns>
        private DialogResult CloseCurrentPackage(bool trySavingPendingChange = true, bool forceSavingPendingChange = false)
        {
            warnings.Clear();

            DialogResult closed = DialogResult.No;
            if (info != null) try
                {
                    // Prompt the user to save changes if required
                    if (trySavingPendingChange) closed = SavePackage(PackageRootPath, forceSavingPendingChange);

                    // Couldn't save some pending changes
                    if (closed == DialogResult.Abort)
                    {
                        closed = MessageBoxEx.Show(this, "Pending changes couldn't be saved!\r\n\r\nDo you want to close your package anyway?\r\nChanges will be lost!", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
                        if (closed != DialogResult.Yes)
                            // User doens't want to close without saving remaining changes
                            closed = DialogResult.Cancel;
                        else
                            // User discards remaining changes
                            closed = DialogResult.No;
                    }

                    // No need to save or user saved/discared explicitly possible changes
                    if (closed == DialogResult.Yes || closed == DialogResult.No)
                    {
                        // Rename the parent folder with the package display name (or the package name)
                        RenamePackageFolder();

                        PackageRootPath = "";
                        textBoxDisplay.Focus();
                        ResetValidateChildren(this); // Reset Error Validation on all controls

                        info = null;
                        list = null;
                        picturePkg_256 = null;
                        picturePkg_120 = null;
                        picturePkg_72 = null;
                        FillInfoScreen();
                        BindData(list, null);
                        DisplayDetails(new KeyValuePair<string, AppsData>(null, null));
                        labelToolTip.Text = "";

                        textBoxDisplay.Focus();
                        closed = DialogResult.Yes;
                    }
                }
                catch { closed = DialogResult.Abort; }

            return closed;
        }

        /// <summary>
        /// Rename the Package folder into its display name or package name if it's not a temporary folder.
        /// </summary>
        /// <remarks>This doesn't rethow any exception if the renaming failed.</remarks>
        private void RenamePackageFolder()
        {
            try
            {
                var parent = Path.GetFileName(PackageRootPath);
                Guid tmp;
                bool isTempFolder = Guid.TryParse(parent, out tmp);
                if (!isTempFolder)
                {
                    string newName = null;
                    if (info.ContainsKey("displayname") && !string.IsNullOrEmpty(info["displayname"]))
                        newName = (info["displayname"]);
                    else if (info.ContainsKey("package") && !string.IsNullOrEmpty(info["package"]))
                        newName = info["package"];

                    if (!string.IsNullOrEmpty(newName) && !newName.Equals(parent, StringComparison.InvariantCultureIgnoreCase))
                    {
                        var RenamedPackageRootPath = PackageRootPath.Replace(parent, newName);
                        var increment = 0;
                        while (Directory.Exists(RenamedPackageRootPath))
                        {
                            increment++;
                            RenamedPackageRootPath = string.Format("{0} ({1})", PackageRootPath.Replace(parent, newName), increment);
                        }
                        RemoveRecentPath(PackageRootPath);
                        // Moving Package results in Windows Explorer locking them => copy into a new folder and delete the old one
                        //Directory.Move(PackageRootPath, RenamedPackageRootPath);
                        var oldPackageRootPath = PackageRootPath;
                        if (Helper.CopyDirectory(PackageRootPath, RenamedPackageRootPath))
                        {
                            PackageRootPath = RenamedPackageRootPath;
                            SavePackageSettings();
                            CreateRecentsMenu();
                            if (Directory.Exists(RenamedPackageRootPath) && RenamedPackageRootPath != oldPackageRootPath)
                                Helper.DeleteDirectory(oldPackageRootPath);
                            else
                            {
                                // Something went wrong while copying the old folder into a renamed one ?! Reuse therefore the old one :(
                                PackageRootPath = oldPackageRootPath;
                                SavePackageSettings();
                                CreateRecentsMenu();
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Close the current Package. This is going to prompt the user to save changes if any. 
            CloseCurrentPackage(true);
        }

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = new ProcessStartInfo("https://github.com/vletroye/Mods/wiki");
            info.UseShellExecute = true;
            Process.Start(info);
        }

        private void supportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = new ProcessStartInfo("https://github.com/vletroye/Mods/issues");
            info.UseShellExecute = true;
            Process.Start(info);
        }

        private void packeDevGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = new ProcessStartInfo("https://developer.synology.com/developer-guide/");
            info.UseShellExecute = true;
            Process.Start(info);
        }

        private void labelMaintainerUrl_Click(object sender, EventArgs e)
        {
            if (Helper.IsValidUrl(textBoxMaintainerUrl.Text))
            {
                var info = new ProcessStartInfo(textBoxMaintainerUrl.Text);
                info.UseShellExecute = true;
                Process.Start(info);
            }
        }

        private void labelPublisherUrl_Click(object sender, EventArgs e)
        {
            if (Helper.IsValidUrl(textBoxPublisherUrl.Text))
            {
                var info = new ProcessStartInfo(textBoxPublisherUrl.Text);
                info.UseShellExecute = true;
                Process.Start(info);
            }
        }

        private void labelHelpUrl_Click(object sender, EventArgs e)
        {
            if (Helper.IsValidUrl(textBoxHelpUrl.Text))
            {
                var info = new ProcessStartInfo(textBoxHelpUrl.Text);
                info.UseShellExecute = true;
                Process.Start(info);
            }
        }

        private void labelReportUrl_Click(object sender, EventArgs e)
        {
            if (Helper.IsValidUrl(TextBoxReportUrl.Text))
            {
                var info = new ProcessStartInfo(TextBoxReportUrl.Text);
                info.UseShellExecute = true;
                Process.Start(info);
            }
        }

        private void checkBoxSingleApp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSingleApp.Checked)
            {
                if (list.items.Count > 1)
                    checkBoxSingleApp.Checked = false;
                else if (list.items.Count == 1)
                {
                    var title = list.items.First().Value.title;
                    var type = list.items.First().Value.itemType;
                    if (type == 0)
                    {
                        MessageBoxEx.Show(this, string.Format("You may not tansform the url '{0}' into a single app!", title), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        checkBoxSingleApp.Checked = false;
                    }
                    else
                    {
                        var cleanTitle = Helper.CleanUpText(title);

                        if (list.items.First().Value.url.Contains(string.Format("/{0}/", cleanTitle)))
                        {
                            if (MessageBoxEx.Show(this, string.Format("Do you want to tansform '{0}' into a single app?", title), "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                var sourceWebAppFolder = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], cleanTitle);
                                var targetWebAppFolder = Path.Combine(PackageRootPath, @"package", info["dsmuidir"]);

                                if (Directory.GetDirectories(sourceWebAppFolder).Contains(Path.Combine(sourceWebAppFolder, "images")))
                                {
                                    MessageBoxEx.Show(this, string.Format("You may not tansform {0} into a single app because it contains a folder named 'images'!", title), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    checkBoxSingleApp.Checked = false;
                                }
                                else if (Directory.GetFiles(sourceWebAppFolder).Contains(Path.Combine(sourceWebAppFolder, "config")))
                                {
                                    MessageBoxEx.Show(this, string.Format("You may not tansform {0} into a single app because it contains a file named 'config'!", title), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    checkBoxSingleApp.Checked = false;
                                }

                                if (checkBoxSingleApp.Checked)
                                {
                                    try
                                    {
                                        Helper.CopyDirectory(sourceWebAppFolder, targetWebAppFolder);
                                        Helper.DeleteDirectory(sourceWebAppFolder);

                                        var focused = Helper.FindFocusedControl(this);

                                        var oldName = string.Format("/webman/3rdparty/{0}/{1}/", info["package"], cleanTitle);
                                        var newName = string.Format("/webman/3rdparty/{0}/", info["package"]);

                                        list.items.First().Value.url = list.items.First().Value.url.Replace(oldName, newName);
                                        dirty = true;

                                        BindData(list, null);
                                        DisplayItem();
                                        focused.Focus();

                                        info["singleApp"] = "yes";

                                        // Force saving changes without prompting the user. This is required has changes have been done in the config file.
                                        SavePackage(PackageRootPath, true);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBoxEx.Show(this, string.Format("A Fatal error occured while trying to move {0} to {1} : {2}", sourceWebAppFolder, targetWebAppFolder, ex.Message), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                }
                            }
                            else
                            {
                                checkBoxSingleApp.Checked = false;
                            }
                        }
                        else
                        if (info != null)
                            info["singleApp"] = "yes";
                    }
                }
            }
            else
            {
                if (list != null && list.items.Count == 1)
                {
                    var title = list.items.First().Value.title;
                    var type = list.items.First().Value.itemType;
                    if (type != 0)
                    {
                        var cleanTitle = Helper.CleanUpText(title);

                        if (!list.items.First().Value.url.Contains(string.Format("/{0}/", cleanTitle)))
                            if (info != null)
                            {
                                if (MessageBoxEx.Show(this, string.Format("Do you want to tansform {0} into a side by side app?", title), "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    var sourceWebAppFolder = Path.Combine(PackageRootPath, @"package", info["dsmuidir"]);
                                    var targetWebAppFolder = Path.Combine(PackageRootPath, @"package", info["dsmuidir"], cleanTitle);

                                    try
                                    {
                                        var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

                                        //Move all files into a the new (sub)folder
                                        Helper.CopyDirectory(sourceWebAppFolder, tempPath);
                                        Helper.DeleteDirectory(sourceWebAppFolder);
                                        Helper.CopyDirectory(tempPath, targetWebAppFolder);
                                        Helper.DeleteDirectory(tempPath);

                                        //Except the images and the config file.
                                        if (Directory.Exists(Path.Combine(targetWebAppFolder, "images")))
                                            Directory.Move(Path.Combine(targetWebAppFolder, "images"), Path.Combine(sourceWebAppFolder, "images"));
                                        if (File.Exists(Path.Combine(targetWebAppFolder, "config")))
                                            File.Move(Path.Combine(targetWebAppFolder, "config"), Path.Combine(sourceWebAppFolder, "config"));

                                        var focused = Helper.FindFocusedControl(this);

                                        var oldName = string.Format("/webman/3rdparty/{0}/", info["package"]);
                                        var newName = string.Format("/webman/3rdparty/{0}/{1}/", info["package"], cleanTitle);

                                        list.items.First().Value.url = list.items.First().Value.url.Replace(oldName, newName);
                                        dirty = true;

                                        BindData(list, null);
                                        DisplayItem();
                                        focused.Focus();

                                        info["singleApp"] = "no";

                                        // Force saving changes without prompting the user. This is required has changes have been done in the config file.
                                        SavePackage(PackageRootPath, true);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBoxEx.Show(this, string.Format("An error occured while trying to move {0} to {1} : {2}", sourceWebAppFolder, targetWebAppFolder, ex.Message), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                }
                                else
                                {
                                    info["singleApp"] = "no";
                                }
                            }
                            else
                                checkBoxSingleApp.Checked = true;
                    }
                }
                else
                    if (info != null)
                    info["singleApp"] = "no";
            }

            EnableItemDetails();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PromptToImportPackage();
        }

        private void PromptToImportPackage()
        {
            openFileDialog4Mods.Title = "Select a package file";
            openFileDialog4Mods.Filter = "spk (*.spk)|*.spk";
            openFileDialog4Mods.FilterIndex = 0;
            openFileDialog4Mods.FileName = null;
            DialogResult result = openFileDialog4Mods.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                PackageRootPath = ImportPackage(openFileDialog4Mods.FileName);
            }
        }

        private string ImportPackage(string spk)
        {
            string path = ExpandSpkInTempFolder(spk);
            OpenPackage(path, true);
            return path;
        }

        private static string ExpandSpkInTempFolder(string spk)
        {
            var spkInfo = new FileInfo(spk);
            var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(path);
            spkInfo.CopyTo(Path.Combine(path, spkInfo.Name));
            return path;
        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PromptToMovePackage();
        }

        private bool PromptToMovePackage(string path = null)
        {
            bool succeed = true;
            warnings.Clear();

            DialogResult result = DialogResult.Cancel;
            if (path == null)
            {
                folderBrowserDialog4Mods.Title = "Pick a target root folder to move the Package currently opened.";
                if (Properties.Settings.Default.DefaultPackageRoot)
                    folderBrowserDialog4Mods.InitialDirectory = Properties.Settings.Default.PackageRoot;
                else
                    folderBrowserDialog4Mods.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                var selection = folderBrowserDialog4Mods.ShowDialog();
                if (selection)
                {
                    path = folderBrowserDialog4Mods.FileName;
                    if (Directory.Exists(path))
                    {
                        var name = info["displayname"];
                        var copies = Directory.GetDirectories(path, name + " (*)");
                        if (copies.Length > 0)
                        {
                            var higher = 0;
                            foreach (var folder in copies)
                            {
                                var counting = Regex.Match(Regex.Match(folder, @"\(\d+\)$").Value, @"\d+").Value;
                                int count = 0;
                                int.TryParse(counting, out count);
                                if (count > higher)
                                    higher = count;
                            }
                            do
                            {
                                higher++;
                                name = string.Format("{0} ({1})", name, higher);
                            } while (Directory.Exists(Path.Combine(path, name)));
                        }
                        else
                        {
                            name = string.Format("{0} ({1})", name, 1);
                        }
                        path = Path.Combine(path, name);
                        result = DialogResult.OK;
                    }
                    else
                        result = DialogResult.Abort;
                }
                else
                    result = DialogResult.Cancel;
            }

            if (result == DialogResult.Abort)
            {
                succeed = false;
            }
            if (result != DialogResult.Cancel && result != DialogResult.Abort)
            {
                try
                {
                    Helper.CopyDirectory(PackageRootPath, path);
                    Helper.DeleteDirectory(PackageRootPath);
                    succeed = OpenPackage(path);
                }
                catch (Exception ex)
                {
                    MessageBoxEx.Show(this, string.Format("A Fatal error occured while trying to move {0} to {1} : {2}", PackageRootPath, path, ex.Message), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    succeed = false;
                }
            }

            return succeed;
        }

        private void textBoxUrl_TextChanged(object sender, EventArgs e)
        {
            var url = textBoxUrl.Text;
            if (url == "" || url == "h" || url == "ht" || url == "htt")
            { }
            else if (url.StartsWith(":"))
            {
                var portMatch = getPort.Match(url);
                if (portMatch.Success)
                {
                    var port = portMatch.Groups[1].Value;
                    url = url.Substring(port.Length + 1);
                    textBoxUrl.Text = url;
                    textBoxUrl.SelectionStart = url.Length;
                    textBoxPort.Text = port;
                }
            }
            else if (!(url.StartsWith("/") || url.StartsWith("http", StringComparison.InvariantCultureIgnoreCase) || url.StartsWith("https", StringComparison.InvariantCultureIgnoreCase)))
            {
                var pos = textBoxUrl.SelectionStart;
                url = string.Format(@"/{0}", url);
                textBoxUrl.Text = url;
                textBoxUrl.SelectionStart = pos + 1;
            }
            textBoxPort.Visible = url.StartsWith("/");
            comboBoxProtocol.Visible = url.StartsWith("/");
        }

        private void textBoxPort_TextChanged(object sender, EventArgs e)
        {
            if ((!(string.IsNullOrEmpty(textBoxPort.Text) || textBoxPort.Text.Equals("default")) && comboBoxProtocol.SelectedIndex == 0))
                comboBoxProtocol.SelectedIndex = 1;
        }

        private void comboBoxProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProtocol.SelectedIndex == 0 && !string.IsNullOrEmpty(textBoxPort.Text) && !textBoxPort.Text.Equals("default"))
                comboBoxProtocol.SelectedIndex = 1;
        }

        private void textBoxPort_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (string.IsNullOrEmpty(textBoxPort.Text))
                    textBoxPort.Text = "default";
                else if (!textBoxPort.Text.Equals("default"))
                {
                    if (!textBoxPort.Text.All(char.IsNumber))
                    {
                        e.Cancel = true;
                        errorProvider.SetError(textBoxPort, "The port must be empty or numeric");
                    }
                }
            }
        }

        private void textBoxPort_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxPort, "");
        }

        private void checkBoxLegacy_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLegacy.Focused && comboBoxItemType.SelectedIndex == (int)UrlType.Url && checkBoxLegacy.Checked)
            {
                if (MessageBoxEx.Show(this, "If you enable this option with an external URL, you will have to disable the option 'Improve security with HTTP Content Security Policy (CSP) header' in the Control Panel > Security !", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    checkBoxLegacy.Checked = false;
                }
            }
        }

        private void advancedEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AdvancedEditor = !Properties.Settings.Default.AdvancedEditor;
            Properties.Settings.Default.Save();
            ShowAdvancedEditor(Properties.Settings.Default.AdvancedEditor);
        }

        private void ShowAdvancedEditor(bool advanced)
        {
            menuAdvancedEditor.Checked = advanced;

            labelVersion.Visible = advanced;
            textBoxVersion.Visible = advanced;
            labelDSMAppName.Visible = advanced;
            textBoxDsmAppName.Visible = advanced;

            labelLatestFirmware.Visible = advanced;
            textBoxLatestFirmware.Visible = advanced;
            labelFirmware.Visible = advanced;
            textBoxFirmware.Visible = advanced;
            checkBoxSingleApp.Visible = advanced;
            checkBoxBeta.Visible = advanced;
            TextBoxReportUrl.Visible = advanced && checkBoxBeta.Checked;

            checkBoxOfflineInstall.Visible = advanced;
            checkBoxSilentInstalll.Visible = advanced;
            checkBoxSilentUpgrade.Visible = advanced;
            checkBoxSilentUninstall.Visible = advanced;
            checkBoxStartable.Visible = advanced;
            checkBoxRemovable.Visible = advanced;
            checkBoxPrecheck.Visible = advanced;
            checkBoxSilentReboot.Visible = advanced;

            checkBoxSupportCenter.Visible = advanced;
            checkBoxLegacy.Visible = advanced;
            labelGrantPrivilege.Visible = advanced;
            ComboBoxGrantPrivilege.Visible = advanced;
            checkBoxAdvanceGrantPrivilege.Visible = advanced;
            checkBoxConfigPrivilege.Visible = advanced;

            checkBoxAdminUrl.Visible = advanced;
            textBoxAdminPort.Visible = advanced && checkBoxAdminUrl.Checked;
            textBoxAdminUrl.Visible = advanced && checkBoxAdminUrl.Checked;
            comboBoxAdminProtocol.Visible = advanced && checkBoxAdminUrl.Checked;

            buttonDependencies.Visible = advanced;
        }

        private void ComboBoxGrantPrivilege_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBoxConfigPrivilege.Checked = true;
        }

        private void checkBoxAdvanceGrantPrivilege_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAdvanceGrantPrivilege.Checked && ComboBoxGrantPrivilege.SelectedIndex == -1 && ComboBoxGrantPrivilege.Enabled)
            {
                ComboBoxGrantPrivilege.SelectedIndex = ComboBoxGrantPrivilege.FindString("local");
            }
        }

        private void checkBoxConfigPrivilege_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxConfigPrivilege.Checked && ComboBoxGrantPrivilege.SelectedIndex == -1 && ComboBoxGrantPrivilege.Enabled)
            {
                ComboBoxGrantPrivilege.SelectedIndex = ComboBoxGrantPrivilege.FindString("local");
            }
        }

        private void checkBoxSupportCenter_CheckedChanged(object sender, EventArgs e)
        {
            //textBoxSupportUrl.Visible = checkBoxSupportCenter.Checked;
            //labelSupportUrl.Visible = textBoxSupportUrl.Visible;
            //if (!textBoxSupportUrl.Visible)
            //{
            //    if (textBoxSupportUrl.Text != "")
            //        previousSupportUrl = textBoxSupportUrl.Text;
            //    textBoxSupportUrl.Text = "";
            //}
            //else if (previousSupportUrl != null)
            //    textBoxSupportUrl.Text = previousSupportUrl;

            checkBoxBeta_CheckedChanged(sender, e);
        }

        private void labelSupportUrl_Click(object sender, EventArgs e)
        {
            if (Helper.IsValidUrl(textBoxSupportUrl.Text))
            {
                var info = new ProcessStartInfo(textBoxSupportUrl.Text);
                info.UseShellExecute = true;
                Process.Start(info);
            }
        }

        private void textBoxSupportUrl_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxSupportUrl, "");
        }

        private void textBoxSupportUrl_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (!string.IsNullOrEmpty(textBoxSupportUrl.Text))
                {
                    CheckUrl(textBoxSupportUrl, ref e);
                }
            }
        }

        private void textBoxLatestFirmware_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxLatestFirmware, "");
        }

        private void textBoxLatestFirmware_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (textBoxLatestFirmware.Text != "")
                {
                    if (getOldFirmwareVersion.IsMatch(textBoxLatestFirmware.Text))
                    {
                        var parts = textBoxLatestFirmware.Text.Split('.');
                        textBoxLatestFirmware.Text = string.Format("{0}.{1}-{2:D4}", parts[0], parts[1], int.Parse(parts[2]));
                    }
                    if (getShortFirmwareVersion.IsMatch(textBoxLatestFirmware.Text))
                    {
                        var parts = textBoxLatestFirmware.Text.Split('.');
                        textBoxLatestFirmware.Text = string.Format("{0}.{1}-0000", parts[0], parts[1]);
                    }
                    if (!getFirmwareVersion.IsMatch(textBoxLatestFirmware.Text))
                    {
                        e.Cancel = true;
                        textBoxLatestFirmware.Select(0, textBoxLatestFirmware.Text.Length);
                        errorProvider.SetError(textBoxLatestFirmware, "The format of a firmware must be like 0.0-0000");
                    }
                    else
                    {
                        var parts = textBoxLatestFirmware.Text.Split(new char[] { '.', '-' });
                        if (int.Parse(parts[2]) == 0)
                            textBoxLatestFirmware.Text = string.Format("{0}.{1}", parts[0], parts[1]);
                        else
                            textBoxLatestFirmware.Text = string.Format("{0}.{1}-{2:D4}", parts[0], parts[1], int.Parse(parts[2]));
                    }
                }
            }
        }

        private void checkBoxAdminUrl_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxAdminProtocol.Visible = checkBoxAdminUrl.Checked;
            textBoxAdminPort.Visible = checkBoxAdminUrl.Checked;
            textBoxAdminUrl.Visible = checkBoxAdminUrl.Checked;
        }

        private void toolStripMenuItemPublish_Click(object sender, EventArgs e)
        {
            PublishPackage();
        }

        private void manageScreenshotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var snapshotManager = new SnapshotManager(PackageRootPath);
            snapshotManager.ShowDialog(this);
        }

        private void toolStripMenuItemChangeLog_Click(object sender, EventArgs e)
        {
            var changelog = textBoxChangeBox.Text;
            var content = new ScriptDetails(changelog, "ChangeLog Editor");

            DialogResult result = Helper.ScriptEditor(content, null, null);
            if (result == DialogResult.OK)
            {
                textBoxChangeBox.Text = content.Code;
            }
        }

        private void buttonDependencies_Click(object sender, EventArgs e)
        {
            var edit = new Dependencies(info);
            edit.ShowDialog(this);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                errorProvider.Tag = new object(); ;
                ResetValidateChildren(this);
                textBoxDisplay.Focus();
                errorProvider.Tag = null;
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var param = new Parameters();
            param.ShowDialog();
        }

        private void pictureBoxWarning_Click(object sender, EventArgs e)
        {
            var message = warnings.Aggregate((i, j) => i + "\r\n_____________________________________________________________\r\n\r\n" + j);
            warnings.Clear();
            MessageBoxEx.Show(this, message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            DragDropEffects effects = DragDropEffects.None;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                validPackage4DragDrop = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                if (Directory.Exists(validPackage4DragDrop))
                    effects = DragDropEffects.Copy;
                else
                    validPackage4DragDrop = null;
            }

            e.Effect = effects;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (validPackage4DragDrop != null)
            {
                // Close the current Package. This is going to prompt the user to save changes if any. 
                var saved = CloseCurrentPackage();

                // Open another Package if the user saved/discarded explicitly pending changes.
                if (saved == DialogResult.Yes || saved == DialogResult.No)
                    OpenPackage(validPackage4DragDrop);

                validPackage4DragDrop = null;
            }
        }

        private void reviewPendingChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValidateChildren();

            var changes = new List<Tuple<string, string, string>>();
            var exist = CheckChanges(changes);
            StringBuilder message = new StringBuilder();
            foreach(var item in changes)
            {
                message.AppendFormat("{0}: {1} => {2}\r\n", item.Item1, item.Item2, item.Item3);
            }

            MessageBoxEx.Show(this, message.ToString(), "Pending Changes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

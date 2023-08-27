using BeatificaBytes.Synology.Mods.Forms;
using BeatificaBytes.Synology.Mods.Helpers;
using IniParser;
using IniParser.Model;
using IniParser.Model.Configuration;
using IniParser.Parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.Remoting.Lifetime;
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
        #endregion  --------------------------------------------------------------------------------------------------------------------------------

        #region Declarations -----------------------------------------------------------------------------------------------------------------------
        const string CONFIGFILE = @"package\{0}\config";

        string SSPKRepository = Properties.Settings.Default.PackageRepo;

        //4 next vars are a Dirty Hack - move these 4 vars into a class. Replace AppsData with that class, ...
        string webAppFolder = null;
        string webAppIndex = null;
        string currentScript = null;
        string currentRunner = null;

        MainHelper mainHelper = null;
        Dictionary<string, PictureBox> pictureBoxes;
        KeyValuePair<string, AppData> current;
        bool dirtyPic256 = false;
        bool dirtyPic72 = false;
        //PackageConfig list;
        State state;

        string previousReportUrl;
        string imageDragDropPath;
        protected bool validImage4DragDrop;
        protected string validPackage4DragDrop = null;
        string[] filesDragDropPath;
        protected bool validFiles4DragDrop;
        private List<string> candidate_preloads;

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
        public MainForm(string packageFolder)
        {
            InitializeComponent();

            // Initialise the MainFrom Helper Intended to manage the Package
            mainHelper = new MainHelper(this);

            // Subscribe on Notification for messages published via Helper.PulishWarning()
            HelperNew.Notify += this.OnNotification;

            // Prepare the PictureBox for drag&drop, etc...
            PreparePictureBoxesEvents();

            // Prepare Events
            PrepareFieldsEvents();

            // Load AutoComplete for Firmware
            Helper.LoadDSMReleases(textBoxMinFirmware);
            Helper.LoadDSMReleases(textBoxMaxFirmware);

            // Reset the UI (enabling/disabling Fields, etc...)
            Reset_UI();

            // If Mods Packager is not called with a Package as input parameters use the last package;
            if (string.IsNullOrWhiteSpace(packageFolder))
                packageFolder = Properties.Settings.Default.LastPackage;

            // Open the required packaged if any
            try
            {
                if (!string.IsNullOrWhiteSpace(packageFolder))
                    mainHelper.OpenExistingPackage(packageFolder);
            }
            catch
            {
                Properties.Settings.Default.LastPackage = null;
                Properties.Settings.Default.Save();
                throw;
            }
        }

        #region Encapsulated Properties
        internal Package CurrentPackage { get { return mainHelper != null ? mainHelper.Package : null; } }
        internal OpenFolderDialog SpkRepoBrowserDialog4Mods { get { return spkRepoBrowserDialog4Mods; } }
        internal OpenFolderDialog FolderBrowserDialog4Mods { get { return folderBrowserDialog4Mods; } }
        internal OpenFolderDialog WebpageBrowserDialog4Mods { get { return webpageBrowserDialog4Mods; } }
        internal Label ToolTip { get { return labelToolTip; } }
        #endregion

        /// <summary>
        /// EvenHandler to process messages sent via Helper.PublishWarnings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnNotification(object sender, MessageEventArgs args)
        {
            PublishWarning(args.Message);
        }

        #region Mods UI Management
        /// <summary>
        /// Initialise the PictureBox Controls to react on user interaction (Support Drag&Drop or doubble click to change their content).
        /// Used to change their current Icon.
        /// </summary>
        private void PreparePictureBoxesEvents()
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

        /// <summary>
        /// Initialise the Controls to react on user interaction (Support MouseEnter, MouseLeave, Enter and TextChanged).
        /// Used to display the tooltips in the Tooltip Bar and to validate the Fields.
        /// </summary>
        private void PrepareFieldsEvents()
        {
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
        }

        internal void PrepareRecentsMenu()
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
                    if (!exist) mainHelper.RemoveRecentPathFromSettings(path);
                }
                menuOpenRecentPackage.DropDownItems.AddRange(items.ToArray());
            }
            menuOpenRecentPackage.Enabled = menuOpenRecentPackage.DropDownItems.Count > 0;
        }

        private void PrepareDataBinding(string selection)
        {
            listViewItems.Items.Clear();
            PackageConfig list = CurrentPackage == null ? null : CurrentPackage.Config;
            if (list != null)
            {
                foreach (var item in list.items)
                {
                    var uri = item.Value.url;
                    if (uri != null && uri.StartsWith("/"))
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
                lvi.Tag = new KeyValuePair<string, AppData>(null, null);

                // Add the list items to the ListView
                listViewItems.Items.Add(lvi);
            }
        }
        #endregion

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
            if (CurrentPackage != null)
                labelToolTip.Text = string.Format("Package loaded from {0}", CurrentPackage.Folder_Root);
            else
                labelToolTip.Text = "";
        }

        //private void LoadResourceConfig(string path)
        //{
        //    this.resource = null;
        //    var resourceDir = Path.Combine(path, "conf");
        //    if (Directory.Exists(resourceDir))
        //    {
        //        var resourceFile = Path.Combine(resourceDir, "resource");

        //        if (File.Exists(resourceFile))
        //        {
        //            var json = File.ReadAllText(resourceFile);
        //            try
        //            {
        //                this.resource = JsonConvert.DeserializeObject<JObject>(json);
        //            }
        //            catch (Exception ex)
        //            {
        //                PublishWarning(string.Format("The resource file '{0}' is corrupted...", resourceFile));
        //            }

        //            if (this.resource != null && this.resource.Count == 0)
        //            {
        //                this.resource = null;
        //            }
        //        }
        //    }
        //}
        private void SaveResourceConfig()
        {
            //    var resourceDir = Path.Combine(path, "conf");

            //    if (this.resource != null)
            //    {
            //        if (!Directory.Exists(resourceDir))
            //            Directory.CreateDirectory(resourceDir);

            //        var resourceFile = Path.Combine(resourceDir, "resource");
            //        try
            //        {
            //            if (File.Exists(resourceFile))
            //            {
            //                File.Delete(resourceFile);
            //            }

            //            if (this.resource.Count > 0)
            //            {
            //                var json = JsonConvert.SerializeObject(this.resource, Formatting.Indented);

            //                Helper.WriteAnsiFile(resourceFile, json);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            PublishWarning(string.Format("The resource file '{0}' can't be updated.", resourceFile));
            //        }
            //    }

            //    if (Directory.EnumerateFileSystemEntries(resourceDir).Count() == 0)
            //        Directory.Delete(resourceDir);

            //    // Required for DSM 4.2 ~ DSM 5.2
            //    if (info.ContainsKey("support_conf_folder"))
            //        info.Remove("support_conf_folder");
            //    if (Directory.Exists(resourceDir))
            //    {
            //        var addKey = true;
            //        if (info.ContainsKey("os_min_ver"))
            //        {
            //            var major = 0;
            //            int.TryParse(info["os_min_ver"].Substring(0, 1), out major);
            //            addKey = major < 6;
            //        }
            //        if (addKey)
            //            info.Add("support_conf_folder", "yes");
            //    }

            CurrentPackage.Save();
            ResetEditScriptMenuIcons();
        }

        //private void LoadPrivilegeConfig(string path)
        //{
        //    this.privilege = null;
        //    var privilegeDir = Path.Combine(path, "conf");
        //    if (Directory.Exists(privilegeDir))
        //    {
        //        var privilegeFile = Path.Combine(privilegeDir, "privilege");

        //        if (File.Exists(privilegeFile))
        //        {
        //            var json = File.ReadAllText(privilegeFile);
        //            try
        //            {
        //                this.privilege = JsonConvert.DeserializeObject<JObject>(json);
        //            }
        //            catch (Exception ex)
        //            {
        //                PublishWarning(string.Format("The privilege file '{0}' is corrupted...", privilegeFile));
        //            }

        //            if (this.privilege != null && this.privilege.Count == 0)
        //            {
        //                this.privilege = null;
        //            }
        //        }
        //    }
        //}
        private void SavePrivilegeConfig()
        {
            //var privilegeDir = Path.Combine(path, "conf");
            //var privilegeFile = Path.Combine(privilegeDir, "privilege");
            //if (File.Exists(privilegeFile))
            //{
            //    File.Delete(privilegeFile);
            //}

            //if (this.privilege != null)
            //{
            //    if (!Directory.Exists(privilegeDir))
            //        Directory.CreateDirectory(privilegeDir);

            //    try
            //    {

            //        if (this.privilege.Count > 0)
            //        {
            //            var json = JsonConvert.SerializeObject(this.privilege, Formatting.Indented);

            //            Helper.WriteAnsiFile(privilegeFile, json);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        PublishWarning(string.Format("The privilege file '{0}' can't be updated.", privilegeFile));
            //    }
            //}

            //if (Directory.EnumerateFileSystemEntries(privilegeDir).Count() == 0)
            //    Directory.Delete(privilegeDir);

            //// Required for DSM 4.2 ~ DSM 5.2
            //if (info.ContainsKey("support_conf_folder"))
            //    info.Remove("support_conf_folder");
            //if (Directory.Exists(privilegeDir))
            //{
            //    var addKey = true;
            //    if (info.ContainsKey("os_min_ver"))
            //    {
            //        var major = 0;
            //        int.TryParse(info["os_min_ver"].Substring(0, 1), out major);
            //        addKey = major < 6;
            //    }
            //    if (addKey)
            //        info.Add("support_conf_folder", "yes");
            //}

            CurrentPackage.Save();
            ResetEditScriptMenuIcons();
        }

        private void ProcessCurrentPackage()
        {
            state = State.None;

            if (CurrentPackage != null)
            {
                mainHelper.ProcessPackageInfo();
                mainHelper.ProcessPackageConfig();
            }

            FillInfoScreen();
        }

        private void FillInfoScreen()
        {
            if (CurrentPackage != null)
            {
                var parent = Path.GetFileName(CurrentPackage.Folder_Root);
                Guid tmp;
                bool isTempFolder = Guid.TryParse(parent, out tmp);
                if (isTempFolder)
                {
                    if (groupBoxPackage.BackColor != Color.Salmon)
                    {
                        groupBoxPackage.BackColor = Color.Salmon;
                        HelperNew.PublishWarning("This package has has been opened from a temporary folder. Possibly move it into a target folder using the menu Package > Move.");
                    }
                }
                else
                {
                    groupBoxPackage.BackColor = SystemColors.Control;
                }

                string subkey;
                var unused = new List<string>(CurrentPackage.INFO.Keys);
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
                                if (CurrentPackage.INFO.Keys.Contains(subkey))
                                {
                                    if (string.IsNullOrEmpty(textBox.Text))
                                        textBox.Text = CurrentPackage.INFO[subkey];
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
                                if (CurrentPackage.INFO.Keys.Contains(subkey))
                                {
                                    //if (string.IsNullOrEmpty(comboBox.Text))
                                    comboBox.SelectedIndex = comboBox.FindStringExact(CurrentPackage.INFO[subkey]);
                                    if (string.IsNullOrEmpty(comboBox.Text))
                                        comboBox.Text = CurrentPackage.INFO[subkey];
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
                                if (CurrentPackage.INFO.Keys.Contains(subkey))
                                {
                                    unused.Remove(subkey);
                                    tick = (CurrentPackage.INFO[subkey] == "yes");
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

                unused.Remove("support_conf_folder");

                var listDependencies = new Dependencies(null);
                listDependencies.RemoveSupported(unused);

                if (unused.Count > 0)
                {
                    HelperNew.PublishWarning("There are a few unsupported info in this package. You can edit those via the 'Advanced' button." + Environment.NewLine + Environment.NewLine + unused.Aggregate((i, j) => i + Environment.NewLine + j + ": " + CurrentPackage.INFO[j]));
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

            if (mainHelper.PicturePkg_256 == null && mainHelper.PicturePkg_120 != null)
            {
                mainHelper.PicturePkg_256 = mainHelper.PicturePkg_120;
            }

            LoadPictureBox(pictureBoxPkg_256, mainHelper.PicturePkg_256);
            dirtyPic256 = false;
            LoadPictureBox(pictureBoxPkg_72, mainHelper.PicturePkg_72);
            dirtyPic72 = false;
        }
        /// <summary>
        /// Publish a Warnings and activate the related blinking icon
        /// </summary>
        /// <param name="message"></param>
        /// <remarks>use Helper.PublishWarning("...") to publish !</remarks>
        private async void PublishWarning(string message)
        {
            if (!mainHelper.Warnings.Contains(message)) mainHelper.Warnings.Add(message);

            pictureBoxWarning.Enabled = true;
            if (!pictureBoxWarning.Visible)
            {
                var watch = new Stopwatch();
                pictureBoxWarning.Visible = true;
                var image = pictureBoxWarning.BackgroundImage;
                watch.Start();
                while (mainHelper != null && mainHelper.Warnings.Count > 0 && watch.ElapsedMilliseconds < 6000)
                {
                    await Task.Delay(500);
                    if (pictureBoxWarning.BackgroundImage == null)
                        pictureBoxWarning.BackgroundImage = image;
                    else
                        pictureBoxWarning.BackgroundImage = null;
                }
                watch.Stop();
                pictureBoxWarning.BackgroundImage = image;
            }

        }

        private void ShowWarnings()
        {
            var message = mainHelper.Warnings.Aggregate((i, j) => i + "\r\n_____________________________________________________________\r\n\r\n" + j);
            mainHelper.Warnings.Clear();
            pictureBoxWarning.Visible = false;
            MessageBoxEx.Show(this, message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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

        //TODO: move this method to the MainHelper
        private void PublishPackage()
        {
            mainHelper.Warnings.Clear();

            if (ValidateChildren())
            {
                BuildPackage();

                var PackageRepo = Properties.Settings.Default.PackageRepo;
                var DefaultPackageRepo = Properties.Settings.Default.DefaultPackageRepo;
                if (string.IsNullOrEmpty(PackageRepo) || !Directory.Exists(PackageRepo) || !DefaultPackageRepo)
                {

                    spkRepoBrowserDialog4Mods.Title = "Pick a folder to publish the Package.";
                    if (!string.IsNullOrEmpty(SSPKRepository))
                        spkRepoBrowserDialog4Mods.InitialDirectory = SSPKRepository;
                    else if (string.IsNullOrEmpty(PackageRepo) || !Directory.Exists(PackageRepo))
                        spkRepoBrowserDialog4Mods.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    else
                        spkRepoBrowserDialog4Mods.InitialDirectory = PackageRepo;

                    if (spkRepoBrowserDialog4Mods.ShowDialog())
                        SSPKRepository = spkRepoBrowserDialog4Mods.FileName;
                    else
                        SSPKRepository = null;
                }
                else
                    SSPKRepository = PackageRepo;

                if (!string.IsNullOrEmpty(SSPKRepository))
                    PublishPackage(CurrentPackage.Folder_Root, SSPKRepository);
            }
        }

        internal void SavePackageInfo()
        {
            using (new CWaitCursor())
            {
                UpdatePackageInfo();

                CurrentPackage.Save();

                // Save Package's icons
                if (dirtyPic72)
                {
                    var imageName = Path.Combine(CurrentPackage.Folder_Root, "PACKAGE_ICON.PNG");
                    SavePkgImage(pictureBoxPkg_72, imageName);
                    dirtyPic72 = false;
                }
                if (dirtyPic256)
                {
                    var imageName = Path.Combine(CurrentPackage.Folder_Root, "PACKAGE_ICON_256.PNG");
                    SavePkgImage(pictureBoxPkg_256, imageName);
                    dirtyPic256 = false;
                }

                mainHelper.Dirty = false;
                ResetEditScriptMenuIcons();
            }
        }

        private void UpdatePackageInfo()
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
                            CurrentPackage.INFO[keyId] = textBox.Text.Trim();
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
                            CurrentPackage.INFO[keyId] = value;
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
                            CurrentPackage.INFO[keyId] = comboBox.Text.Trim();
                        }
                    }
                }
            }

            if (!checkBoxAdminUrl.Checked)
            {
                CurrentPackage.INFO.AdminPort = null;
                CurrentPackage.INFO.AdminProtocol = null;
                CurrentPackage.INFO.AdminUrl = null;
                CurrentPackage.INFO.CheckPort = null;
            }
        }

        // Save icons of the SPK
        private void SavePkgImage(PictureBox pictureBox, string path)
        {
            var image = pictureBox.Image;
            if (File.Exists(path))
                Helper.DeleteFile(path);

            if (image != null) image.Save(path, ImageFormat.Png);

            // TODO: check that PKG images are saved when closing Mods
        }
        #endregion --------------------------------------------------------------------------------------------------------------------------------

        #region Manage List of items --------------------------------------------------------------------------------------------------------------
        private void listViewItems_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentPackage.Config != null && listViewItems.SelectedItems.Count == 1)
            {
                buttonEditItem_Click(sender, e);
            }
        }

        private void listViewItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (state != State.Edit)
            {
                if (CurrentPackage.Config != null && listViewItems.SelectedItems.Count > 0)
                {
                    state = State.View;
                    DisplayDetails((KeyValuePair<string, AppData>)listViewItems.SelectedItems[0].Tag);
                }
                else
                {
                    state = State.None;
                    DisplayDetails(new KeyValuePair<string, AppData>(null, null));
                }
            }
        }

        // Add an new item
        private void buttonAddItem_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                //TODO: do not save anymore the INFO file but only update the CurrentPackage.INFO !
                //var saved = mainHelper.SaveCurrentPackage(true); // Need to save to get the latest value in _package.INFO.Package
                UpdatePackageInfo();

                var prompt = new NewService();
                var response = prompt.ShowDialog(this);
                if (response != DialogResult.Cancel)
                {

                    state = State.Add;
                    currentScript = null;
                    currentRunner = null;

                    var data = new AppData();
                    if (CurrentPackage.INFO.SingleApp == "yes")
                    {
                        data.title = CurrentPackage.INFO.Displayname;
                        data.desc = CurrentPackage.INFO.Description;
                    }
                    DisplayDetails(new KeyValuePair<string, AppData>(Guid.NewGuid().ToString(), data));
                    textBoxTitle.Focus();
                }
            }
        }

        // Edit the item currently selected
        private void buttonEditItem_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                state = State.Edit;
                currentScript = null;
                currentRunner = null;
                EnableItemDetails();
                textBoxTitle.Focus();
            }
        }

        private void buttonCancelItem_Click(object sender, EventArgs e)
        {
            if (state == State.Add)
                current = new KeyValuePair<string, AppData>(null, null);

            currentScript = null;
            currentRunner = null;
            candidate_preloads = null;
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
                    answer = MessageBoxEx.Show(this, "This item has no icon. Do you want to add icons before saving ?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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
                        MessageBoxEx.Show(this, "The changes couldn't be saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
            }
            else
            {
                MessageBoxEx.Show(this, "The changes may not be saved as long as you don't fix all issues.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void buttonDeleteItem_Click(object sender, EventArgs e)
        {
            if (current.Key != null)
            {
                var answer = MessageBoxEx.Show(this, string.Format("Do you really want to delete the {0} '{1}' and related icons?", GetTypeName(current.Value.itemType), current.Value.title), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (answer == DialogResult.Yes)
                {
                    CurrentPackage.Config.items.Remove(current.Key);
                    PrepareDataBinding(null);
                    DeleteItemPictures(current.Value.icon);

                    var cleanedCurrent = Helper.CleanUpText(current.Value.title);
                    switch (current.Value.itemType)
                    {
                        case AppDataType.WebApp:
                            DeleteWebApp(cleanedCurrent);
                            break;
                        case AppDataType.Script:
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
                case (int)AppDataType.Url: // Url                    
                    this.toolTip4Mods.SetToolTip(this.textBoxUrl, "Type here the URL to be opened when clicking the icon on DSM. If not hosted on Synology, it must start with 'http://' or 'https://'. If hosted on Synology, it must start with a '/'.");
                    break;
                case (int)AppDataType.Script: // Script
                    this.toolTip4Mods.SetToolTip(this.textBoxUrl, "Type the Script to be executed when clicking the icon on DSM. DoubleClick to edit.");
                    break;
                case (int)AppDataType.WebApp: // WebApp
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
                MessageBoxEx.Show(this, "Please, Cancel or Save first the current edition.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                e.Cancel = true;
            }
            else
            {
                // Close the current Package. This is going to prompt the user to save changes if any. 
                var saved = mainHelper.CloseCurrentPackage(true);

                // Do not close the application if saving failed or was cancelled
                e.Cancel = !(saved == DialogResult.Yes || saved == DialogResult.No);
            }
        }

        private bool RenamePreviousItem(KeyValuePair<string, AppData> current, KeyValuePair<string, AppData> candidate)
        {
            bool succeed = true;

            var cleanedCurrent = Helper.CleanUpText(current.Value.title);
            var cleanedCandidate = Helper.CleanUpText(candidate.Value.title);

            if (cleanedCurrent != null)
            {
                //Rename a Script
                if (current.Value.itemType == AppDataType.Script && candidate.Value.itemType == AppDataType.Script && cleanedCurrent != cleanedCandidate)
                {
                    succeed = RenameScript(candidate.Value.title, cleanedCurrent, cleanedCandidate);
                }

                //Rename a WebApp 
                if (current.Value.itemType == AppDataType.WebApp && candidate.Value.itemType == AppDataType.WebApp && cleanedCurrent != cleanedCandidate)
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

            if (CurrentPackage.INFO.SingleApp != "yes")
            {
                var existingWebAppFolder = getItemPath(cleanedCurrent);
                if (Directory.Exists(existingWebAppFolder))
                {
                    var targetWebAppFolder = getItemPath(cleanedCandidate);
                    if (Directory.Exists(targetWebAppFolder))
                    {
                        answer = MessageBoxEx.Show(this, string.Format("Your Package '{0}' already contains a WebApp named {1}.\nDo you confirm that you want to replace it?\nIf you answer No, the existing one will be used. Otherwise, it will be replaced.", CurrentPackage.INFO.Package, candidate), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (answer == DialogResult.Yes)
                        {
                            var ex = Helper.DeleteDirectory(targetWebAppFolder);
                            if (ex != null)
                            {
                                MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to delete {0}: {1}", candidate, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
                                    answer = MessageBoxEx.Show(this, string.Format("Your WebApp '{0}' cannot be renamed because its folder is in use.\nDo you want to retry (close first any other application using that folder)?", candidate), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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

        private bool CleanupPreviousItem(KeyValuePair<string, AppData> current, KeyValuePair<string, AppData> candidate)
        {
            bool succeed = true;

            var cleanedCurrent = Helper.CleanUpText(current.Value.title);
            var cleanedCandidate = Helper.CleanUpText(candidate.Value.title);

            //Clean Up a WebApp previously defined if replaced by another type
            if (current.Value.itemType == AppDataType.WebApp && candidate.Value.itemType != AppDataType.WebApp)
            {
                succeed = DeleteWebApp(cleanedCurrent);
            }

            //Clean Up a Script previously defined if replaced by another type
            if (current.Value.itemType == AppDataType.Script && candidate.Value.itemType != AppDataType.Script)
            {
                succeed = DeleteScript(cleanedCurrent);
            }

            if (succeed && current.Key != null)
            {
                CurrentPackage.Config.items.Remove(current.Key);
                PrepareDataBinding(null);
                DeleteItemPictures(current.Value.icon);
            }

            return succeed;
        }

        private void SaveItemDetails(KeyValuePair<string, AppData> candidate)
        {
            SaveItemPictures(candidate);

            switch (candidate.Value.itemType)
            {
                case AppDataType.Url:
                    break;
                case AppDataType.WebApp:
                    CreateWebApp(candidate);
                    break;
                case AppDataType.Script:
                    CreateScript(candidate, currentScript, currentRunner);
                    break;
            }

            CurrentPackage.Config.items.Add(candidate.Key, candidate.Value);
            PrepareDataBinding(candidate.Value.title);

            DisplayItem(candidate);
        }

        private bool RenameScript(string candidate, string cleanedCurrent, string cleanedCandidate)
        {
            bool succeed = false;
            var answer = DialogResult.Yes;

            if (CurrentPackage.INFO.SingleApp != "yes")
            {
                var current = getItemPath(cleanedCurrent);
                if (Directory.Exists(current))
                {
                    var target = getItemPath(cleanedCandidate);
                    if (Directory.Exists(target))
                    {
                        answer = MessageBoxEx.Show(this, string.Format("Your Package '{0}' already contains a script named {1}.\nDo you confirm that you want to replace it?\nIf you answer No, the existing one will be used. Otherwise, it will be replaced.", CurrentPackage.INFO.Package, candidate), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (answer == DialogResult.Yes)
                        {
                            var ex = Helper.DeleteDirectory(target);
                            if (ex != null)
                            {
                                MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to delete {0}: {1}", target, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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

            if (CurrentPackage.INFO.SingleApp == "yes")
                cleanedCurrent = "";

            var targetScript = getItemPath(cleanedCurrent);
            if (Directory.Exists(targetScript))
            {
                var ex = Helper.DeleteDirectory(targetScript);
                if (ex != null)
                {
                    MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to delete {0}: {1}", targetScript, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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

            if (CurrentPackage.INFO.SingleApp == "yes")
                cleanedCurrent = "";

            var targetWebApp = getItemPath(cleanedCurrent);
            if (Directory.Exists(targetWebApp))
            {
                var ex = Helper.DeleteDirectory(targetWebApp);
                if (ex != null)
                {
                    MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to delete {0}: {1}", targetWebApp, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
                if (current.Value.itemType != AppDataType.Undefined && current.Value.itemType != (int)AppDataType.Url && (int)current.Value.itemType != selectedIndex)
                {
                    var from = GetTypeName(current.Value.itemType);
                    var to = GetTypeName(selectedIndex);
                    answer = MessageBoxEx.Show(this, string.Format("Your item '{0}' is currently a {1}.\nDo you confirm that you want to replace it by a new {2}?\nIf you answer Yes, your existing {1} will be deleted when you save your changes.", CurrentPackage.INFO.Package, from, to), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                }

                if (answer == DialogResult.No)
                {
                    textBoxTitle.Focus();
                    comboBoxItemType.SelectedIndex = (int)current.Value.itemType;
                    checkBoxLegacy.Checked = (current.Value.type == "legacy");
                }
                else
                {
                    //var previous = current.Value.itemType;
                    //current.Value.itemType = selectedIndex;
                    switch (selectedIndex)
                    {
                        case (int)AppDataType.Url:
                            textBoxUrl.Enabled = true;
                            textBoxUrl.ReadOnly = false;
                            if ((int)current.Value.itemType != selectedIndex)
                            {
                                textBoxUrl.Text = "";
                            }
                            //textBoxUrl.Focus();
                            current.Value.itemType = GetType(selectedIndex);
                            break;

                        case (int)AppDataType.Script:
                            var cleanedScriptName = Helper.CleanUpText(textBoxTitle.Text);
                            var targetScriptPath = getItemPath(cleanedScriptName, "mods.sh");
                            var targetRunnerPath = getItemPath(cleanedScriptName, "mods.php");

                            textBoxUrl.Enabled = true;
                            textBoxUrl.ReadOnly = true;

                            EditScript(targetScriptPath, targetRunnerPath);
                            if (string.IsNullOrEmpty(currentScript))
                            {
                                selectedIndex = (int)current.Value.itemType;
                                comboBoxItemType.SelectedIndex = selectedIndex;
                                checkBoxLegacy.Checked = (current.Value.type == "legacy");
                            }
                            else
                            {
                                var url = "";
                                GetDetailsScript(cleanedScriptName, ref url);
                                current.Value.itemType = GetType(selectedIndex);
                                textBoxUrl.Text = url;
                            }
                            break;

                        case (int)AppDataType.WebApp:
                            var cleanedWebApp = Helper.CleanUpText(textBoxTitle.Text);
                            var targetWebAppFolder = getItemPath(cleanedWebApp);
                            if (Directory.Exists(targetWebAppFolder) && Directory.EnumerateFiles(targetWebAppFolder).Count() > 0)
                            {
                                answer = MessageBoxEx.Show(this, string.Format("Your Package '{0}' already contains a WebApp named {1}.\nDo you want to replace it?", CurrentPackage.INFO.Package, textBoxTitle.Text), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            }
                            if (answer == DialogResult.Yes)
                            {
                                if (!EditWebApp())
                                {
                                    selectedIndex = (int)current.Value.itemType;
                                    comboBoxItemType.SelectedIndex = selectedIndex;
                                    checkBoxLegacy.Checked = (current.Value.type == "legacy");
                                }
                                else
                                {
                                    current.Value.itemType = GetType(selectedIndex);
                                }
                            }
                            break;
                    }

                    if (current.Value.itemType == 0)
                    {
                        comboBoxProtocol.Visible = textBoxUrl.Text.StartsWith("/");
                        textBoxPort.Visible = textBoxUrl.Text.StartsWith("/");
                    }
                    else
                    {
                        comboBoxProtocol.Visible = false;
                        textBoxPort.Visible = false;
                        labelAddResources.Visible = (current.Value.itemType == AppDataType.Script);
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
            if (currentScript == null && File.Exists(scriptPath))
                inputScript = File.ReadAllText(scriptPath);
            else
            {
                //When creating a new script, the script can be edited several times but is not yet saved in a file.
                inputScript = currentScript;
            }

            if (currentRunner == null && File.Exists(runnerPath))
                inputRunner = File.ReadAllText(runnerPath);
            else if (currentRunner == null)
            {
                inputRunner = File.ReadAllText(Path.Combine(Helper.ResourcesDirectory, "default.runner"));
            }
            else
            {
                inputRunner = currentRunner;
            }

            var script = new ScriptInfo(inputScript, "Script Editor", new Uri("https://www.shellscript.sh/"), "Shell Scripting Tutorial");
            var runner = new ScriptInfo(inputRunner, "Runner Editor", new Uri("https://stackoverflow.com/questions/20107147/php-reading-shell-exec-live-output"), "Reading shell_exec live output in PHP");

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
            return Helpers.Synology.GetAllWizardVariables(CurrentPackage.Folder_Root);
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
                        MessageBoxEx.Show(this, "This file is not in the directory selected previously. Please select a file under that folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        webAppFolder = null;
                        webAppIndex = null;
                        comboBoxItemType.SelectedIndex = (int)AppDataType.Url;
                    }
                    else
                    {
                        textBoxUrl.Text = webAppIndex.Remove(0, webAppFolder.Length + 1);
                    }
                }
            }

            return result;
        }

        private bool CreateWebApp(KeyValuePair<string, AppData> current)
        {
            bool succeed = true;
            if (webAppIndex != null && webAppFolder != null)
            {
                var cleanedCurrent = Helper.CleanUpText(current.Value.title);

                // A Single App is stored in the root of the package app folder. So, the Single App may not have items conflicting with the package items "images" and "config"
                if (CurrentPackage.INFO.SingleApp == "yes")
                {
                    succeed = MayTransformIntoSingleApp(webAppFolder);
                    cleanedCurrent = "";
                }
                if (succeed)
                {
                    // Ask about using the Router CGI technic instead of depending on the ThirdParty package
                    DialogResult transformIntoSingleApp = DialogResult.No;
                    var router = MessageBoxEx.Show(this, "Do you want to use the Router CGI?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (router == DialogResult.Yes && CurrentPackage.INFO.SingleApp != "yes")
                    {
                        {
                            transformIntoSingleApp = MessageBoxEx.Show(this, "Using a Router CGI is only fully supported by MODS with Single App? Do you want to transform the WebApp into a Single App (YES) or are you going to manage yourself the scripts to support the Router CGI (NO)? You may also continue without using the Router CGI (CANCEL).", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (transformIntoSingleApp == DialogResult.Yes && !MayTransformIntoSingleApp(webAppFolder))
                            {
                                transformIntoSingleApp = MessageBoxEx.Show(this, "Are you going to manage yourself the scripts to support the Router CGI (YES) or do you want continue without using the Router CGI (NO).", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                if (transformIntoSingleApp == DialogResult.Yes)
                                    transformIntoSingleApp = DialogResult.No;
                                else
                                    transformIntoSingleApp = DialogResult.Cancel;
                            }
                            if (transformIntoSingleApp == DialogResult.Cancel)
                                router = DialogResult.No;
                        }
                    }

                    var targetWebAppFolder = getItemPath(cleanedCurrent);

                    // Delete the previous version of this WebApp if any
                    if (Directory.Exists(targetWebAppFolder) && Directory.EnumerateFiles(targetWebAppFolder).Count() > 0)
                    {
                        var ex = Helper.DeleteDirectory(targetWebAppFolder);
                        if (ex != null)
                        {
                            MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to delete {0}: {1}", targetWebAppFolder, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            succeed = false;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(cleanedCurrent)) Directory.CreateDirectory(targetWebAppFolder);
                        }
                    }

                    // Copy the files into the target WebApp Folder
                    if (succeed)
                    {
                        DialogResult copy = DialogResult.Yes;
                        if (router == DialogResult.No && CurrentPackage.INFO.SingleApp != "yes")
                            copy = MessageBoxEx.Show(this, "Do you want to copy the files of the webApp into the package Folder (YES) or create a Symbolic Link on the target folder instead (NO)?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                        if (copy == DialogResult.Yes)
                        {
                            succeed = Helper.CopyDirectory(webAppFolder, targetWebAppFolder);

                            if (router == DialogResult.Yes)
                            {
                                var RouterConfig = Path.Combine(Helper.ResourcesDirectory, "dsm.cgi.conf");
                                var targetRouterConfig = Path.Combine(targetWebAppFolder, "dsm.cgi.conf");
                                if (!File.Exists(targetRouterConfig))
                                    File.Copy(RouterConfig, targetRouterConfig);
                                else
                                    MessageBoxEx.Show(this, "A file dsm.cgi.conf already exist. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                                var RouterScript = Path.Combine(Helper.ResourcesDirectory, "router.cgi");
                                var targetRouterScript = Path.Combine(targetWebAppFolder, "router.cgi");
                                if (!File.Exists(targetRouterScript))
                                    File.Copy(RouterScript, targetRouterScript);
                                else
                                    MessageBoxEx.Show(this, "A file router.cgi already exist. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                                var editScript = MessageBoxEx.Show(this, "You must now edit the Post installation script. The code to be added is now in the clipboard. Simply paste it at the appropriate location!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                if (editScript == DialogResult.OK)
                                {
                                    var script = Properties.Settings.Default.router_inst;
                                    if (CurrentPackage.INFO.SingleApp != "yes" && transformIntoSingleApp != DialogResult.Yes)
                                        script = script.Replace("/ui/dsm.cgi.conf", String.Format("/ui/{0}/dsm.cgi.conf", cleanedCurrent));
                                    Clipboard.SetText(script);
                                    if (EditInstallationScript("scripts/postinst", "Post-Install Script"))
                                    {
                                        menuPostInstall.Image = new Bitmap(Properties.Resources.EditedScript);
                                    }

                                    var editDependency = MessageBoxEx.Show(this, "Finally, you must add 'nginx' in the list of 'startstop_restart_services' and remove the dependency on 'Init_3rdparty>=1.5' if any.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                    if (editDependency == DialogResult.OK)
                                    {
                                        var edit = new Dependencies(CurrentPackage.INFO);
                                        edit.ShowDialog(this);
                                    }
                                }
                            }

                            if (transformIntoSingleApp == DialogResult.Yes)
                            {
                                if (!TransformIntoSingleApp(current))
                                {
                                    copy = MessageBoxEx.Show(this, "The transformation into a Single App has failed. Do you want to continue without transforming your WebApp?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                                }
                                else
                                {
                                    checkBoxSingleApp.Checked = true;
                                }
                            }
                        }
                        else if (copy == DialogResult.No)
                            succeed = Helper.CreateSymLink(targetWebAppFolder, webAppFolder, true);
                    }
                }
            }

            return succeed; //TODO: Handle this return value
        }

        private bool MayTransformIntoSingleApp(string folder)
        {
            var succeed = true;
            if (Directory.GetDirectories(folder).Contains(Path.Combine(folder, "images")))
            {
                MessageBoxEx.Show(this, string.Format("You may not use '{0}' for a single app because it contains a folder named 'images'!", folder), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                succeed = false;
            }
            else if (Directory.GetFiles(folder).Contains(Path.Combine(folder, "config")))
            {
                MessageBoxEx.Show(this, string.Format("You may not use '{0}' for a single app because it contains a file named 'config'!", folder), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                succeed = false;
            }
            return succeed;
        }

        private bool CreateScript(KeyValuePair<string, AppData> current, string script, string runner)
        {
            bool succeed = true;

            if (script != null)
            {
                var cleanedCurrent = Helper.CleanUpText(current.Value.title);
                if (CurrentPackage.INFO.SingleApp == "yes")
                    cleanedCurrent = "";

                var target = getItemPath(cleanedCurrent);
                var targetRunner = Path.Combine(target, "mods.php");
                var targetScript = Path.Combine(target, "mods.sh");

                if (Directory.Exists(target))
                {
                    if (File.Exists(targetRunner))
                        Helper.DeleteFile(targetRunner);
                    if (File.Exists(targetScript))
                        Helper.DeleteFile(targetScript);
                }
                else
                {
                    Directory.CreateDirectory(target);
                }

                // Create sh script (ANSI) to be executed by the php runner script
                Helper.WriteAnsiFile(targetScript, script);
                Helper.WriteAnsiFile(targetRunner, runner);
            }

            return succeed; //TODO: handle this return value
        }

        private void SaveItemsConfig()
        {
            if (CurrentPackage != null)
            {
                mainHelper.Dirty = true;
                Regex rgx;

                var json = JsonConvert.SerializeObject(CurrentPackage.Config, Formatting.Indented, new KeyValuePairConverter());

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

                if (!string.IsNullOrWhiteSpace(CurrentPackage.INFO.DsmUiDir))
                {
                    var config = CurrentPackage.Path_Config;
                    if (Directory.Exists(Path.GetDirectoryName(config)))
                    {
                        // Save Config as Ansi
                        Helper.WriteAnsiFile(config, json);
                    }
                    else
                    {
                        MessageBoxEx.Show(this, "For some reason, required resource files are missing. You will have to reconfigure your destination path", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    }
                }
            }
        }

        private void DisplayNone()
        {
            state = State.None;
            DisplayDetails(new KeyValuePair<string, AppData>(null, null));
        }

        private void DisplayItem()
        {
            DisplayItem(new KeyValuePair<string, AppData>());
        }

        // Display an item which becomes the current one. If no item is specified, the first one in the list is used, if any.
        private void DisplayItem(KeyValuePair<string, AppData> item)
        {
            // If no current Url, select the first one in the list
            if (item.Key == null && listViewItems.Items.Count > 0)
            {
                item = (KeyValuePair<string, AppData>)listViewItems.Items[0].Tag;
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
                    KeyValuePair<string, AppData> tag = (KeyValuePair<string, AppData>)other.Tag;
                    other.Selected = (tag.Value.guid == currentGuid);
                    if (other.Selected)
                        listViewItems.EnsureVisible(other.Index);
                }
            }
        }

        private void DisplayDetails(KeyValuePair<string, AppData> item)
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
                comboBoxItemType.SelectedIndex = (int)AppDataType.Url;
            }

            textBoxDesc.Text = descText;
            textBoxTitle.Text = titleText;
            textBoxUrl.Text = urlText;

            if (!show)
            {
                comboBoxProtocol.Visible = true;
                textBoxPort.Visible = true;
                labelAddResources.Visible = false;
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
                labelAddResources.Visible = (item.Value.itemType == AppDataType.Script);
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
                if (item.Value.itemType != AppDataType.Undefined && !Enum.IsDefined(typeof(AppDataType), item.Value.itemType))
                {
                    MessageBoxEx.Show(this, "The type of this element is obsolete and must be edited to be fixed !!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    item.Value.itemType = AppDataType.Script;
                }
                if (item.Value.itemType == AppDataType.Undefined)
                    item.Value.itemType = 0;
                comboBoxItemType.SelectedIndex = (int)item.Value.itemType;
            }
            else
            {
                comboBoxItemType.SelectedIndex = (int)AppDataType.Url;
            }

            EnableItemDetails();

            listViewItems.Focus();
        }

        private void EnableItemDetails()
        {
            bool enabling;
            bool packaging;
            bool itemArea;

            if (CurrentPackage == null)
            {
                // Disable the detail zone if not package defined
                enabling = false;
                packaging = false;

                PackageConfig list = CurrentPackage == null ? null : CurrentPackage.Config;
                EnableItemFieldDetails(!enabling && list != null, enabling);
                EnableItemButtonDetails(enabling, enabling, enabling, enabling, enabling, packaging);

                EnableItemMenuDetails(false, false, false, false, true, true, true, false, false);
            }
            else
            {
                bool add;

                // Enable the detail and package zone depending on the current state (view, new, edit or none)
                enabling = (state != State.View && state != State.None);
                //packaging = listViewItems.Items.Count > 0 && !enabling;
                packaging = true; //A package can be published even without any item

                itemArea = !string.IsNullOrWhiteSpace(CurrentPackage.DsmUiDir);

                EnableItemFieldDetails(!enabling && CurrentPackage.Config != null, enabling);
                switch (state)
                {
                    case State.View:
                        add = CurrentPackage.Config != null && (CurrentPackage.INFO.SingleApp == "no" || CurrentPackage.Config.items.Count == 0);
                        EnableItemButtonDetails(add, true, false, false, true, packaging);
                        EnableItemMenuDetails(!enabling, itemArea, !enabling, !enabling, true, true, true, true, true);
                        break;
                    case State.None:
                        add = CurrentPackage.Config != null && (CurrentPackage.INFO.SingleApp == "no" || CurrentPackage.Config.items.Count == 0);
                        EnableItemButtonDetails(add, false, false, false, false, packaging);
                        EnableItemMenuDetails(!enabling, itemArea, !enabling, !enabling, true, true, true, true, true);
                        break;
                    case State.Add:
                    case State.Edit:
                        EnableItemButtonDetails(false, false, true, true, false, packaging);
                        EnableItemMenuDetails(!enabling, itemArea, !enabling, !enabling, false, false, false, false, false);
                        break;
                }
                textBoxUrl.ReadOnly = !(comboBoxItemType.SelectedIndex == (int)AppDataType.Url);

                if (!string.IsNullOrWhiteSpace(CurrentPackage.INFO.DsmUiDir))
                {
                    var file = CurrentPackage.Folder_UI;
                    if (File.Exists(file))
                    {
                        menuRouterScript.Enabled = File.Exists(file.Replace("config", "router.cgi"));
                        menuRouterConfig.Enabled = File.Exists(file.Replace("config", "dsm.cgi.conf"));
                        menuRouterConfig.Enabled = File.Exists(file.Replace("config", @"texts\enu\strings"));
                    }
                }

                if (mainHelper.WizardExist)
                    menuAddWizard.Text = "Remove Wizard";
                else
                    menuAddWizard.Text = "Create Wizard";


                menuWizardInstallUI.Enabled = mainHelper.WizardExist;
                menuWizardUninstallUI.Enabled = mainHelper.WizardExist;
                menuWizardUpgradeUI.Enabled = mainHelper.WizardExist;
            }
        }

        private void EnableItemMenuDetails(bool packageArea, bool itemsArea, bool menuPackage, bool menuSave, bool menuNew, bool menuOpen, bool menuRecent, bool menuEdit, bool menuClose)
        {
            groupBoxPackage.Enabled = packageArea;
            groupBoxItem.Enabled = itemsArea;
            foreach (ToolStripItem menu in this.menuPackage.DropDownItems)
            {
                menu.Enabled = menuPackage;
            }
            menuSavePackage.Enabled = menuSave;
            menuNewPackage.Enabled = menuNew;
            //menuOpenPackageFolder.Enabled = true;
            menuOpenPackage.Enabled = menuOpen;
            menuImportPackage.Enabled = menuOpen;
            menuOpenRecentPackage.Enabled = menuRecent;
            menuClosePackage.Enabled = menuClose;
            foreach (ToolStripItem menu in this.menuEdit.DropDownItems)
            {
                menu.Enabled = menuEdit;
            }
            foreach (ToolStripItem menu in this.menuWorkers.DropDownItems)
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
            comboBoxProtocol.Enabled = !checkBoxLegacy.Checked;
            textBoxPort.Enabled = !checkBoxLegacy.Checked;
            ComboBoxGrantPrivilege.Enabled = bItemDetails;
            checkBoxAdvanceGrantPrivilege.Enabled = bItemDetails;
            checkBoxConfigPrivilege.Enabled = bItemDetails;
            labelAddResources.Enabled = bItemDetails;
            buttonPreloadTexts.Enabled = bItemDetails;
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
        private KeyValuePair<string, AppData> GetItemDetails()
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
            var preloadTexts = candidate_preloads;
            candidate_preloads = null;

            var url = textBoxUrl.Text.Trim();
            var key = string.Format("{0}.{1}", textBoxDsmAppName.Text, Helper.CleanUpText(title));
            //if (!multiInstance) key = textBoxDsmAppName.Text; // This is recommended otherwise the Task Manager does not find the icon of the service

            var urlType = GetType(comboBoxItemType.SelectedIndex);
            switch (urlType)
            {
                case AppDataType.Url:
                    //GetDetailsUrl(ref protocol, ref port, ref url);
                    if (url.StartsWith("/"))
                    {
                        int.TryParse(textBoxPort.Text, out port);
                        protocol = comboBoxProtocol.SelectedItem.ToString();
                        if (protocol.Equals("default", StringComparison.InvariantCultureIgnoreCase)) protocol = "";
                    }
                    break;
                case AppDataType.WebApp:
                    GetDetailsWebApp(title, ref url);
                    break;
                case AppDataType.Script:
                    GetDetailsScript(title, ref url);
                    break;
            }
            var appsData = new AppData()
            {
                allUsers = allUsers,
                title = title,
                desc = desc,
                protocol = protocol,
                url = url,
                port = port,
                itemType = urlType,
                allowMultiInstance = multiInstance,
                grantPrivilege = grantPrivilege,
                advanceGrantPrivilege = advanceGrantPrivilege,
                configablePrivilege = configablePrivilege,
                preloadTexts = preloadTexts
            };
            if (legacy)
            {
                appsData.type = "legacy";
                appsData.appWindow = key;
            }
            else
            {
                appsData.type = "url";
            }

            title = Helper.CleanUpText(title);
            appsData.icon = string.Format("images/{0}_{1}.png", title, "{0}");
            return new KeyValuePair<string, AppData>(key, appsData);
        }

        private void GetDetailsScript(string title, ref string url)
        {
            var cleanedScript = Helper.CleanUpText(title);
            string actualUrl = null;
            if (CurrentPackage.INFO.SingleApp == "yes")
                actualUrl = string.Format("/webman/3rdparty/{0}/mods.php", CurrentPackage.INFO.Package);
            else
                actualUrl = string.Format("/webman/3rdparty/{0}/{1}/mods.php", CurrentPackage.INFO.Package, cleanedScript);
            if (url != actualUrl && url != actualUrl.Replace(".php", ".cgi"))
            {
                url = actualUrl;
            }
        }

        private void GetDetailsWebApp(string title, ref string url)
        {
            var cleanedWebApp = Helper.CleanUpText(title);
            string actualUrl = null;
            if (CurrentPackage.INFO.SingleApp == "yes")
                actualUrl = string.Format("/webman/3rdparty/{0}/{1}", CurrentPackage.INFO.Package, url).Replace("//", "/");
            else
                actualUrl = string.Format("/webman/3rdparty/{0}/{1}/{2}", CurrentPackage.INFO.Package, cleanedWebApp, url).Replace("//", "/");
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
            validImage4DragDrop = Helper.GetDragDropFilename(out imageDragDropPath, e);

            if (validImage4DragDrop)
            {
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
                mainHelper.Dirty = ChangePicturePkg(imageDragDropPath, pictureBox.Tag.ToString().Split(';')[1]);
            }
        }

        private void pictureBoxItem_DragEnter(object sender, DragEventArgs e)
        {
            if (state == State.Add || state == State.Edit)
            {
                validImage4DragDrop = Helper.GetDragDropFilename(out imageDragDropPath, e);
                if (validImage4DragDrop)
                {
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
                if (size == "256")
                    result = dirtyPic256 = LoadPictureBox(pictureBoxPkg_256, image);

                if (size == "72")
                    result = dirtyPic72 = LoadPictureBox(pictureBoxPkg_72, image);
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
            if (transparency > 0)
                if (MessageBoxEx.Show(this, "Do you want to make this image transparent?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    transparency = 0;
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
                MessageBoxEx.Show(this, string.Format("Picture '{0}' is missing and can therefore not be loaded ?!", picture), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
            if (!string.IsNullOrWhiteSpace(CurrentPackage.INFO.DsmUiDir))
                if (icons.Length > 1)
                    picture = getItemPath(icons[0], icons[1]);
                else
                    picture = getItemPath(icons[0]);
            return picture;
        }

        // Physically delete all images of an item
        private void DeleteItemPictures(string item)
        {
            foreach (var size in pictureBoxes.Keys)
            {
                var path = GetIconFullPath(item, size);
                if (File.Exists(path))
                    Helper.DeleteFile(path);
            }
        }

        // Load an image in the PictureBox of the specified size, resizing the image to match it
        private void LoadPictureBox(Image image, string size)
        {
            var pictureBox = pictureBoxes[size];
            LoadPictureBox(pictureBox, image);
        }

        // Load an image in a pictureBox, resizing the image to match it
        private bool LoadPictureBox(PictureBox pictureBox, Image image)
        {
            var loaded = true;
            if (image == null)
            {
                pictureBox.Image = null;
            }
            else
            {
                var size = int.Parse(pictureBox.Tag.ToString().Split(';')[1]);

                var copy = Helper.ResizeImage(image, size, size);

                if (pictureBox.Image == null || !Helper.CompareImages(new Bitmap(pictureBox.Image), new Bitmap(image)))
                {
                    pictureBox.Image = copy;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                    loaded = false;
            }

            return loaded;
        }

        // Save all the pictures of the current Item
        private void SaveItemPictures(KeyValuePair<string, AppData> current)
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
                        Helper.DeleteFile(path);
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
                    textBoxPackage.Text = textBoxDisplay.Text.Replace(' ', '_').Replace("-", "").Replace("__", "_");

                if (!CheckEmpty(textBoxPackage, ref e, ""))
                {
                    textBoxPackage.Text = textBoxPackage.Text.Replace(' ', '_').Replace("-", "").Replace("__", "_");
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
                    textBoxDisplay.Text = textBoxPackage.Text.Replace('_', ' ').Replace("-", "").Replace("__", "_");

                var newName = textBoxPackage.Text;
                var oldName = !string.IsNullOrWhiteSpace(CurrentPackage.INFO.Package) ? CurrentPackage.INFO.Package : newName;
                if (newName != oldName && !string.IsNullOrEmpty(oldName))
                {
                    var focused = Helper.FindFocusedControl(this);

                    var list = CurrentPackage.Config;
                    if (list != null)
                    {
                        var copy = new PackageConfig();
                        foreach (var item in list.items)
                        {
                            copy.items.Add(item.Key.Replace(oldName, newName), item.Value);
                        }
                        list = copy;
                    }
                    if (!string.IsNullOrEmpty(textBoxDsmAppName.Text))
                        textBoxDsmAppName.Text = textBoxDsmAppName.Text.Replace(oldName, newName);

                    oldName = string.Format("/webman/3rdparty/{0}/", oldName);
                    newName = string.Format("/webman/3rdparty/{0}/", newName);

                    if (list != null)
                    {
                        foreach (var item in list.items)
                        {
                            if (item.Value.url.StartsWith(oldName))
                            {
                                item.Value.url = item.Value.url.Replace(oldName, newName);
                            }
                        }
                        PrepareDataBinding(null);
                    }
                    DisplayItem();
                    focused.Focus();

                    CurrentPackage.INFO.Package = textBoxPackage.Text;
                    mainHelper.Dirty = true;
                }

            }
        }

        private void textBoxDisplay_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (string.IsNullOrEmpty(textBoxDisplay.Text) && !string.IsNullOrEmpty(textBoxPackage.Text))
                    textBoxDisplay.Text = textBoxPackage.Text.Replace('_', ' ').Replace("-", "").Replace("__", "_");
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
                if (string.IsNullOrEmpty(textBoxMaintainer.Text))
                {
                    using (new CWaitCursor())
                    {
                        textBoxMaintainer.Text = mainHelper.user.DisplayName;
                    }
                }

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

        private void textBoxDescription_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {

                if (string.IsNullOrEmpty(textBoxDescription.Text) && !string.IsNullOrEmpty(textBoxDisplay.Text))
                {
                    using (new CWaitCursor())
                    {
                        textBoxDescription.Text = textBoxDisplay.Text;
                    }
                }

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
                textBoxVersion.Text = textBoxVersion.Text.Replace('_', '-').Replace('b', '.');
                if (!CheckEmpty(textBoxVersion, ref e, ""))
                {
                    var match = Helper.getOldVersion.Match(textBoxVersion.Text);
                    if (match.Success)
                    {
                        textBoxVersion.Text = string.Format("{0}-{1:D4}", match.Groups[1], int.Parse(match.Groups[3].ToString()));
                    }
                    if (!Helper.getVersion.IsMatch(textBoxVersion.Text))
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
                var name = textBoxDsmAppName.Text; //.Replace(".", "_").Replace("__", "_");
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
                        if (CurrentPackage.Config != null)
                            foreach (var url in CurrentPackage.Config.items.Values)
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
                    oldName = string.Format("/webman/3rdparty/{0}/{1}/", CurrentPackage.INFO.Package, oldName);
                    newName = string.Format("/webman/3rdparty/{0}/{1}/", CurrentPackage.INFO.Package, newName);

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
                    if (comboBoxItemType.SelectedIndex == (int)AppDataType.Url)
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

        // Reset errors possibly displayed on the Control and any children
        internal void ResetValidateChildren(Control control)
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
                case AppDataType.Url:
                    break;
                case AppDataType.WebApp:
                    EditWebApp();
                    break;
                case AppDataType.Script:
                    var cleanedScriptName = Helper.CleanUpText(textBoxTitle.Text);
                    var oldCleanedScriptName = cleanedScriptName;
                    if (CurrentPackage.INFO.SingleApp == "yes")
                        cleanedScriptName = "";

                    var targetScript = getItemPath(cleanedScriptName, "mods.sh");
                    var targetRunner = getItemPath(cleanedScriptName, "mods.php");

                    //Upgrade an old "script" versions
                    var target = getItemPath(oldCleanedScriptName);
                    if (!Directory.Exists(target))
                    {
                        var oldTargetScript = getItemPath(oldCleanedScriptName + ".sh");
                        var oldTargetRunner = getItemPath(oldCleanedScriptName + ".php");
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

        private string GetTypeName(AppDataType type)
        {
            string typeName = null;
            switch (type)
            {
                case AppDataType.Url: // Url
                    typeName = "Url";
                    break;
                case AppDataType.WebApp: // WebApp
                    typeName = "WebApp";
                    break;
                case AppDataType.Script: // Script
                    typeName = "Script";
                    break;
            }
            return typeName;
        }

        private string GetTypeName(int type)
        {
            string typeName = null;
            switch (type)
            {
                case (int)AppDataType.Url: // Url
                    typeName = "Url";
                    break;
                case (int)AppDataType.WebApp: // WebApp
                    typeName = "WebApp";
                    break;
                case (int)AppDataType.Script: // Script
                    typeName = "Script";
                    break;
            }
            return typeName;
        }

        private AppDataType GetType(int type)
        {
            AppDataType appDataType = AppDataType.Undefined;
            switch (type)
            {
                case (int)AppDataType.Url: // Url
                    appDataType = AppDataType.Url;
                    break;
                case (int)AppDataType.WebApp: // WebApp
                    appDataType = AppDataType.WebApp;
                    break;
                case (int)AppDataType.Script: // Script
                    appDataType = AppDataType.Script;
                    break;
            }
            return appDataType;
        }



        private void checkBoxSize_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var box in pictureBoxes.Values)
            {
                if (!box.Tag.ToString().EndsWith("MAX"))
                    box.Enabled = !checkBoxSize.Checked;
            }
        }

        private void menuNew_Click(object sender, EventArgs e)
        {
            // Close the current Package. This is going to prompt the user to save changes if any. 
            var saved = mainHelper.CloseCurrentPackage();

            // Create a new Package if the user saved/discarded explicitly possible changes
            if (saved == DialogResult.Yes || saved == DialogResult.No)
            {
                mainHelper.OpenNewPackage();
            }
        }

        internal bool CheckChanges(List<Tuple<string, string, string>> changes)
        {
            bool dirty = false;

            if (CurrentPackage != null)
            {
                foreach (var control in groupBoxPackage.Controls)
                {
                    var textBox = control as TextBox;
                    if (textBox != null && textBox.Tag != null && textBox.Tag.ToString().StartsWith("PKG"))
                    {
                        var keys = textBox.Tag.ToString().Split(';');
                        var key = keys[0].Substring(3);

                        if (CurrentPackage.INFO.ContainsKey(key))
                        {
                            dirty = textBox.Text.Trim() != CurrentPackage.INFO[key].Trim();
                            if (dirty && changes != null) changes.Add(new Tuple<string, string, string>(key, CurrentPackage.INFO[key], textBox.Text.Trim()));
                        }
                        else
                        {
                            dirty = !string.IsNullOrEmpty(textBox.Text.Trim());
                            if (dirty && changes != null) changes.Add(new Tuple<string, string, string>(key, "", textBox.Text.Trim()));
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

                        if (CurrentPackage.INFO.ContainsKey(key))
                        {
                            dirty = checkBox.Checked != (CurrentPackage.INFO[key] == "yes");
                            if (dirty && changes != null) changes.Add(new Tuple<string, string, string>(key, CurrentPackage.INFO[key], checkBox.Checked ? "yes" : "no"));
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

                        if (CurrentPackage.INFO.ContainsKey(key))
                        {
                            dirty = comboBox.Text.Trim() != CurrentPackage.INFO[key];
                            if (dirty && changes != null) changes.Add(new Tuple<string, string, string>(key, CurrentPackage.INFO[key], comboBox.Text.Trim()));
                        }
                        else
                        {
                            dirty = !string.IsNullOrEmpty(comboBox.Text.Trim());
                            if (dirty && changes != null) changes.Add(new Tuple<string, string, string>(key, "", comboBox.Text.Trim()));
                        }

                        if (changes != null) dirty = false;

                        if (dirty)
                            break;
                    }
                }
                if (!checkBoxAdminUrl.Checked)
                {
                    if (CurrentPackage.INFO.ContainsKey("adminport"))
                    {
                        dirty = !string.IsNullOrEmpty(CurrentPackage.INFO["adminport"]);
                        if (dirty && changes != null) changes.Add(new Tuple<string, string, string>("adminport", CurrentPackage.INFO["adminport"], ""));
                    }
                    if (CurrentPackage.INFO.ContainsKey("adminurl"))
                    {
                        dirty = !string.IsNullOrEmpty(CurrentPackage.INFO["adminurl"]);
                        if (dirty && changes != null) changes.Add(new Tuple<string, string, string>("adminurl", CurrentPackage.INFO["adminurl"], ""));
                    }
                    if (CurrentPackage.INFO.ContainsKey("adminprotocol"))
                    {
                        dirty = !string.IsNullOrEmpty(CurrentPackage.INFO["adminprotocol"]);
                        if (dirty && changes != null) changes.Add(new Tuple<string, string, string>("adminprotocol", CurrentPackage.INFO["adminprotocol"], ""));
                    }
                    if (CurrentPackage.INFO.ContainsKey("checkport"))
                    {
                        dirty = !string.IsNullOrEmpty(CurrentPackage.INFO["checkport"]);
                        if (dirty && changes != null) changes.Add(new Tuple<string, string, string>("checkport", CurrentPackage.INFO["checkport"], ""));
                    }
                }
            }
            if (changes != null) dirty = changes.Count > 0;

            return dirty;
        }

        private bool ResetPackage()
        {
            bool succeed = true;
            mainHelper.Warnings.Clear();

            if (CurrentPackage != null)
            {
                var resetPackage = CurrentPackage.Folder_Root;
                var answer = MessageBoxEx.Show(this, "Do you really want to reset the complete Package?\r\n\r\nThis cannot be undone!", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
                if (answer == DialogResult.Yes)
                {
                    string packageName = "";
                    if (CurrentPackage.INFO.ContainsKey("package"))
                        packageName = CurrentPackage.INFO.Package;
                    try
                    {
                        if (string.IsNullOrEmpty(resetPackage))
                            MessageBoxEx.Show(this, "The path of the Package is not defined. Create a new package instead.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        else
                        {
                            if (Directory.Exists(resetPackage))
                            {
                                // Close without trying to save any pending changes.
                                mainHelper.CloseCurrentPackage(false);

                                var ex = Helper.DeleteDirectory(resetPackage);
                                if (ex != null)
                                {
                                    MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to delete {0}: {1}", CurrentPackage.Folder_Root, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                    succeed = false;
                                }
                            }

                            if (succeed)
                            {
                                Directory.CreateDirectory(resetPackage);
                                mainHelper.OpenNewPackage(resetPackage);
                                textBoxPackage.Text = packageName;
                            }
                        }
                    }
                    catch
                    {
                        MessageBoxEx.Show(this, "The destination folder cannot be cleaned. The package file is possibly in use?!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }
            }
            else
            {
                MessageBoxEx.Show(this, "There is no Package loaded.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }

            textBoxDisplay.Focus();

            return succeed; //TODO: Handle this return value;
        }

        private void BuildPackage()
        {
            mainHelper.Warnings.Clear();

            // This is required to insure that changes (mainly icons) are correctly applied when installing the package in DSM
            textBoxVersion.Text = Helper.IncrementVersion(textBoxVersion.Text);

            using (new CWaitCursor(labelToolTip, "PLEASE WAIT WHILE GENERATING YOUR PACKAGE..."))
            {
                SavePackageInfo();

                // Create the SPK
                var packCmd = Path.Combine(CurrentPackage.Folder_Root, "Pack.cmd");
                if (File.Exists(packCmd))
                {
                    // Delete existing package if any
                    var dir = new DirectoryInfo(CurrentPackage.Folder_Root);
                    foreach (var file in dir.EnumerateFiles("*.spk"))
                    {
                        Helper.DeleteFile(file.FullName);
                    }

                    // Execute the script to generate the SPK
                    Process pack = new Process();
                    pack.StartInfo.FileName = packCmd;
                    pack.StartInfo.Arguments = "";
                    pack.StartInfo.WorkingDirectory = CurrentPackage.Folder_Root;
                    pack.StartInfo.UseShellExecute = false;
                    pack.StartInfo.RedirectStandardOutput = true;
                    pack.StartInfo.CreateNoWindow = true;
                    pack.Start();
                    pack.WaitForExit(10000);
                    if (pack.StartInfo.RedirectStandardOutput) Console.WriteLine(pack.StandardOutput.ReadToEnd());

                    // Rename the new Package with its target name
                    var packName = Path.Combine(CurrentPackage.Folder_Root, CurrentPackage.INFO.Package + ".spk");
                    var tmpName = Path.Combine(CurrentPackage.Folder_Root, "mods.spk");
                    if (pack.ExitCode == 2 || !File.Exists(tmpName))
                    {
                        MessageBoxEx.Show(this, "Creation of the package has failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }

                    try
                    {
                        File.Move(tmpName, packName);
                        //lastBuild.Text = string.Format("Build on {0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.Show(this, "Creation of the package has failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        Helper.DeleteFile(tmpName);
                    }
                }
                else
                {
                    MessageBoxEx.Show(this, "For some reason, required resource files are missing. You will have to reconfigure your destination path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void PublishPackage(string PackagePath, string PackageRepo)
        {
            var packName = CurrentPackage.INFO.Package;
            var archname = CurrentPackage.INFO.Arch;

            try
            {
                var flavour = archname.Equals("noarch", StringComparison.InvariantCultureIgnoreCase) ? "" : "[" + archname.Replace(" ", "-") + "]";
                publishFile(Path.Combine(PackagePath, packName + ".spk"), Path.Combine(PackageRepo, packName + flavour + ".spk"));
                MessageBoxEx.Show(this, "The package has been successfuly published.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to copy files: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        private void UnpublishPackage(string PackageRepo)
        {
            var packName = CurrentPackage.INFO.Package;

            try
            {
                publishFile(null, Path.Combine(PackageRepo, packName + ".spk"));
                MessageBoxEx.Show(this, "The package has been successfuly removed.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while deleting files: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        private void publishFile(string src, string dest)
        {
            if (!string.IsNullOrEmpty(dest) && File.Exists(dest))
            {
                // try to send the existing SPK to the RecycleBin
                Helper.DeleteFile(dest);
            }

            if (!string.IsNullOrEmpty(src))
            {
                File.Copy(src, dest);
            }
        }


        internal void ResetEditScriptMenuIcons()
        {
            foreach (ToolStripItem menu in menuEdit.DropDownItems)
            {
                ShowIconOnScriptMenu(menu);
            }

            foreach (ToolStripItem menu in menuWorkers.DropDownItems)
            {
                ShowIconOnScriptMenu(menu);
            }

            ShowIconOnScriptMenu(menuManageScreenshots);
            menuChangeLog.Image = null;
            menuReviewPendingChanges.Image = null;
        }

        /// <summary>
        /// Show an icon on the menu for which settings already exists
        /// </summary>
        /// <param name="menu"></param>
        private void ShowIconOnScriptMenu(ToolStripItem menu)
        {
            var file = menu.Tag != null ? menu.Tag.ToString().Split(';')[0] : "";
            if (!string.IsNullOrEmpty(file))
            {
                switch (file)
                {
                    case "!webservice":
                        var webservice = CurrentPackage != null && CurrentPackage.IsLoaded && CurrentPackage.Resource != null ? CurrentPackage.Resource.WebService : null;
                        if (webservice != null)
                            file = CurrentPackage.Path_Info; //Provide a file that exits to trigger the icon on this menu
                        else
                            file = null;
                        break;

                    case "!usr-local-linker":
                        var usrLocaLinker = CurrentPackage != null && CurrentPackage.IsLoaded && CurrentPackage.Resource != null ? CurrentPackage.Resource.UsrLocalLinker : null;
                        if (usrLocaLinker != null)
                            file = CurrentPackage.Path_Info; //Provide a file that exits to trigger the icon on this menu
                        else
                            file = null;
                        break;
                    case "!port-config":
                        file = null;
                        var portConfig = CurrentPackage != null && CurrentPackage.IsLoaded && CurrentPackage.Resource != null ? CurrentPackage.Resource.PortConfig : null;
                        if (portConfig != null)
                        {
                            var protocolFile = CurrentPackage.Resource.PortConfig.ProtocolFile;
                            if (protocolFile == null) // Maybe the old style ?
                            {
                                file = Path.Combine(CurrentPackage.Folder_Root, portConfig.ToString().Replace("/", "\\"));
                            }
                            else
                            {
                                file = Path.Combine(CurrentPackage.Folder_Package, protocolFile.ToString().Replace("/", "\\"));
                            }
                        }
                        break;
                    default:
                        // {0} => dsmuidir
                        if (CurrentPackage != null && CurrentPackage.IsLoaded)
                        {
                            if (file.Contains("*"))
                            {
                                if (Directory.Exists(Path.Combine(CurrentPackage.Folder_Root, file)))
                                {
                                    var files = Directory.GetFiles(CurrentPackage.Folder_Root, file, SearchOption.TopDirectoryOnly);
                                    if (files.Length > 0) file = files[0]; else file = null;
                                }
                            }
                            else
                            {
                                var uidir = CurrentPackage.INFO.DsmUiDir;
                                file = Path.Combine(CurrentPackage.Folder_Root, string.Format(file, uidir));
                            }
                        }
                        break;
                }
                if (!string.IsNullOrEmpty(file) && !File.Exists(file) && !File.Exists(file + ".sh")) file = null;
            }
            if (!string.IsNullOrEmpty(file))
                menu.Image = new Bitmap(Properties.Resources.Script);
            else
                menu.Image = null;
        }

        /// <summary>
        /// Check if a Package folder is ready to be used.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="autoDeflate">deflate the spk file in the provided path if this parameter is true and there is no other file next to it.</param>
        /// <returns>
        /// Yes if the folder contains a valid package.
        /// No if the folder is empty.
        /// Cancel if the user cancelled the request.
        /// Abort otherwise. Ex.: if the folder does not exist.
        /// </returns>
        //private DialogResult IsPackageFolderReady(string path, bool autoDeflate = false)
        //{
        //    DialogResult result = DialogResult.Abort;

        //    if (!Directory.Exists(path))
        //    {
        //        result = DialogResult.Abort;
        //    }
        //    else
        //    {
        //        var content = GetFolderContent(path);
        //        if (content.Count == 0)
        //            // Use the provided folder which is empty
        //            result = DialogResult.No;
        //        else
        //        {
        //            // Check if the provided folder, which is not empty, contains a valid Package or an Spk to possibly be deflated
        //            result = IsPackageFolderValid(path, autoDeflate);

        //            // Accept folder with a valid package possibly next to some other files.
        //            if (result == DialogResult.No) result = DialogResult.Yes;
        //        }
        //    }

        //    return result;
        //}

        /// <summary>
        /// Check is a folder contains a valid deflated Package.
        /// </summary>
        /// <param name="path">Path to be checked. It must exists</param>
        /// <param name="autoDeflate">Deflate the spk found in this folder if this parameter is true and there is no other file next to it.</param>
        /// <param name="forceDeflate">Defate the spk found in this folder if this parameter is true and there is no other spk next to it.</param>
        /// <returns>
        /// Yes if the folder contains a valid deflated Package.
        /// No if the folder contains not only valid deflated Package but also some other files.
        /// Cancel if the user cancelled the request.
        /// Abort if the folder does not exist, does not contain a valid Package or anything went wrong.
        /// </returns>
        //private DialogResult IsPackageFolderValid(string path, bool autoDeflate = false, bool forceDeflate = false)
        //{
        //    DialogResult valid = DialogResult.Yes; //Folder does not contains a valid package
        //    if (!Directory.Exists(path))
        //    {
        //        valid = DialogResult.Abort;
        //    }
        //    else
        //    {
        //        var spkList = Directory.GetFiles(path, "*.spk");
        //        var content = GetFolderContent(path);
        //        if (spkList.Length == 1 && (content.Count == 1 || forceDeflate))
        //        // Folder containing only one spk => deflate
        //        {
        //            if (forceDeflate || autoDeflate || MessageBoxEx.Show(this, "This folder contains a SPK file. Do you want to deflate this package ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
        //            {
        //                if (!DeflatePackage(path))
        //                    valid = DialogResult.Abort;
        //            }
        //            else
        //            {
        //                valid = DialogResult.Cancel;
        //            }
        //        }
        //        else if (spkList.Length > 1)
        //        // Folder containing several spk => abort
        //        {
        //            PublishWarning("The package is in a folder with several SPK files.");
        //            valid = DialogResult.Abort;
        //        }

        //        if (valid == DialogResult.Yes)
        //        {
        //            FileInfo info = new FileInfo(Path.Combine(path, "INFO"));

        //        }
        //    }
        //    return valid;
        //}

        private void menuOpen_Click(object sender, EventArgs e)
        {
            // Close the current Package. This is going to prompt the user to save changes if any. 
            var saved = mainHelper.CloseCurrentPackage();

            // Open another Package if the user saved/discarded explicitly pending changes.
            if (saved == DialogResult.Yes || saved == DialogResult.No)
                if (!mainHelper.OpenExistingPackage())
                {
                    //Main Screen Disabled
                    if (pictureBoxWarning.Visible && !pictureBoxWarning.Enabled)
                        ShowWarnings();
                }
        }
        private void menuReset_Click(object sender, EventArgs e)
        {
            ResetPackage();
        }
        private void menuBuild_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                BuildPackage();
                SystemSounds.Hand.Play();
                if (Properties.Settings.Default.PromptExplorer)
                {
                    var answer = MessageBoxEx.Show(this, string.Format("Your Package '{0}' is ready in {1}.\nDo you want to open that folder now?", CurrentPackage.INFO.Package, CurrentPackage.Folder_Root), "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (answer == DialogResult.Yes)
                    {
                        Process.Start(CurrentPackage.Folder_Root);
                    }
                }
                if (Properties.Settings.Default.CopyPackagePath)
                {
                    Clipboard.SetText(CurrentPackage.Folder_Root);
                }
            }
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            if (!mainHelper.Dirty && !CheckChanges(null))
                MessageBoxEx.Show(this, "There is no pending change to be saved.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            else
            {
                // Do not prompt the user to save changes and just do it!
                var saved = mainHelper.SaveCurrentPackage(true);

                // Some Changes have been saved.
                if (saved == DialogResult.Yes)
                    MessageBoxEx.Show(this, "Package saved.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void MenuItemOpenRecent_ClickHandler(object sender, EventArgs e)
        {
            // Close the current Package. This is going to prompt the user to save changes if any. 
            var saved = mainHelper.CloseCurrentPackage();

            // Open the requested Package if the user saved/discarded explicitly pending changes.
            if (saved == DialogResult.Yes || saved == DialogResult.No)
            {
                ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
                String path = clickedItem.Tag.ToString();
                if (!mainHelper.OpenExistingPackage(path))
                {
                    // if the package cannot be opened, remove it from the menu 'Open Recent'
                    mainHelper.RemoveRecentPathFromSettings(path);
                    Properties.Settings.Default.Save();
                    PrepareRecentsMenu();
                }
            }
        }

        private void scriptEditMenuItem_Click(object sender, EventArgs e)
        {
            var menu = (ToolStripMenuItem)sender;
            var scriptName = menu.Tag.ToString();
            if (EditInstallationScript(scriptName, menu.Text))
            {
                menu.Image = new Bitmap(Properties.Resources.EditedScript);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptName">"scripts/{scriptname}"</param>
        /// <param name="title"></param>
        /// <returns></returns>
        private bool EditInstallationScript(string scriptName, string title)
        {
            var done = false;
            var created = false;
            var scriptPath = Path.Combine(CurrentPackage.Folder_Root, scriptName);

            if (scriptName.EndsWith("*"))
            {
                openFileDialog4Mods.Title = "Pick the script file.";
                openFileDialog4Mods.InitialDirectory = Path.GetDirectoryName(scriptPath);
                openFileDialog4Mods.Filter = "script |*.*";
                openFileDialog4Mods.FileName = null;
                if (openFileDialog4Mods.ShowDialog() == DialogResult.OK)
                {
                    scriptPath = openFileDialog4Mods.FileName;
                }
                else
                {
                    return done;
                }
            }

            if (!File.Exists(scriptPath))
            {
                if (MessageBoxEx.Show(this, "This Script does not yet exist.\r\n\r\nPossibly 'Reset' your Package or click 'OK' to create yourself the script from scratch.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    File.WriteAllLines(scriptPath, new string[] { "#!/bin/sh", "", "exit 0" });
                    created = true;
                }
            }
            if (File.Exists(scriptPath))
            {
                var content = File.ReadAllText(scriptPath);
                var script = new ScriptInfo(content, title, new Uri("https://help.synology.com/developer-guide/synology_package/scripts.html"), "Details about script files");
                DialogResult result;
                if (Path.HasExtension(scriptPath) && Path.GetExtension(scriptPath).Equals(".php", StringComparison.InvariantCultureIgnoreCase))
                {
                    result = Helper.ScriptEditor(null, script, GetAllWizardVariables());
                }
                else
                {
                    result = Helper.ScriptEditor(script, null, GetAllWizardVariables());
                }
                if (result == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(script.Code))
                    {
                        result = MessageBoxEx.Show(this, "This Script is now empty.\r\n\r\nDo you want to delete it ?.", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        switch (result)
                        {
                            case DialogResult.Yes:
                                File.Delete(scriptPath);
                                result = DialogResult.Cancel;
                                break;
                            case DialogResult.No:
                                result = DialogResult.OK;
                                break;
                            default:
                                break;
                        }
                    }

                    if (result == DialogResult.OK)
                        Helper.WriteAnsiFile(scriptPath, script.Code);

                    done = true;
                }
            }
            else if (created)
            {
                File.Delete(scriptPath);
            }

            return done;
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            // Trigger the Close event on the Main Form. (See MainForm_FormClosing)
            //DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            var about = new AboutBox();
            about.StartPosition = FormStartPosition.CenterParent;
            about.ShowDialog(this);
        }

        private void menuAddWizard_Click(object sender, EventArgs e)
        {
            var wizard = CurrentPackage.Folder_WizardUI;
            if (Directory.Exists(wizard))
            {
                var result = MessageBoxEx.Show(this, "Are you sure you want to delete your wizard?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
                if (result == DialogResult.Yes)
                {
                    mainHelper.WizardExist = false;
                    var ex = Helper.DeleteDirectory(wizard);
                    if (ex != null)
                    {
                        MessageBoxEx.Show(this, string.Format("The operation cannot be completed because a fatal error occured while trying to delete {0}: {1}", wizard, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        mainHelper.WizardExist = true;
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(wizard);
                MessageBoxEx.Show(this, "You can now Edit wizards to install/upgrade/uninstall this package.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                mainHelper.WizardExist = true;
            }
            EnableItemDetails();
        }

        private void menuWizard_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.Cancel;
            var menu = (ToolStripMenuItem)sender;
            var info = menu.Tag.ToString().Split(';');
            var json = info[0]; // = "WIZARD_UIFILES\xxx_uifile with xxx = install, uninstall or upgrade"
            var file = info[1]; // = "install, uninstall or upgrade"
            var jsonPath = Path.Combine(CurrentPackage.Folder_Root, json);
            var filePath = Path.Combine(CurrentPackage.Folder_WizardUI, file);

            if (!File.Exists(jsonPath))
            {
                jsonPath = jsonPath + ".sh";
                if (!File.Exists(jsonPath))
                    jsonPath = null;
            }

            if (jsonPath == null)
            {
                result = MessageBoxEx.Show(this, "Do you want to create a script to make your wizard dynamic?", "Type of Wizard", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    jsonPath = Path.Combine(CurrentPackage.Folder_Root, json + ".sh");
                    if (!File.Exists(filePath) && MessageBoxEx.Show(this, "Do you want to create a json wizard for your script ?", "Type of Wizard", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        Helper.WriteAnsiFile(jsonPath, Properties.Settings.Default.wizard.Replace("@MODS_WIZARD@", file));
                        Helper.WriteAnsiFile(filePath, string.Empty);
                    }
                    else
                    {
                        Helper.WriteAnsiFile(jsonPath, string.Empty);
                    }
                }
                else if (result == DialogResult.No)
                {
                    jsonPath = Path.Combine(CurrentPackage.Folder_Root, json);
                    Helper.WriteAnsiFile(jsonPath, string.Empty);
                }
            }

            if (jsonPath != null)
            {
                if (File.Exists(filePath) && MessageBoxEx.Show(this, "Do you want to edit the json wizard (YES) or the script wizard (NO)", "Type of Wizard", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    jsonPath = filePath;
                }

                if (Path.GetExtension(jsonPath) == ".sh")
                {
                    var content = File.ReadAllText(jsonPath);
                    var wizard = new ScriptInfo(content, menu.Text, new Uri("https://help.synology.com/developer-guide/synology_package/WIZARD_UIFILES.html"), "Details about Wizard File");

                    string outputRunner = string.Empty;
                    result = Helper.ScriptEditor(wizard, null, null);
                    if (result == DialogResult.OK)
                    {
                        Helper.WriteAnsiFile(jsonPath, wizard.Code);
                        menu.Image = new Bitmap(Properties.Resources.EditedScript);
                    }
                    if (wizard.Code.Trim() == "")
                        Helper.DeleteFile(jsonPath);
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
                        if (result == DialogResult.Abort)
                        {
                            menu.Image = null;
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBoxEx.Show(this, "You may not open this file due to security restriction. Try to run this app as Administrator or grant 'Modify' on this file to the group USERS.", "Security Issue", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                var dir = Path.GetDirectoryName(jsonPath);
                if (Directory.Exists(dir) && Directory.EnumerateFiles(dir).Count() == 0)
                    Directory.Delete(dir);
            }
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
            //TODO: only update package info and items
            SavePackageInfo();
            SaveItemsConfig();

            var infoName = CurrentPackage.Path_Info;
            string content = File.ReadAllText(infoName);
            var script = new ScriptInfo(content, "INFO Editor", new Uri("https://help.synology.com/developer-guide/synology_package/INFO.html"), "Details about INFO settings");

            Properties.Settings.Default.AdvancedEditor = true;
            Properties.Settings.Default.Save();
            ShowAdvancedEditor(true);

            content = null;
            string configName = null;
            ScriptInfo config = null;
            if (!string.IsNullOrWhiteSpace(CurrentPackage.INFO.DsmUiDir))
            {
                configName = CurrentPackage.Folder_UI;
                if (File.Exists(configName))
                {
                    content = File.ReadAllText(configName);
                    content = Helper.JsonPrettify(content);
                }
                config = new ScriptInfo(content, "Config Editor", new Uri("https://help.synology.com/developer-guide/integrate_dsm/config.html"), "Details about Config settings");
            }

            DialogResult result = Helper.ScriptEditor(script, config, null);
            if (result == DialogResult.OK)
            {
                Helper.WriteAnsiFile(infoName, script.Code);
                if (configName != null) Helper.WriteAnsiFile(configName, config.Code);

                //TODO: this should not be required
                ProcessCurrentPackage();
                PrepareDataBinding(null);
                DisplayItem();
            }
        }

        private void menuOpenPackageFolder_Click(object sender, EventArgs e)
        {
            if (CurrentPackage != null && CurrentPackage.IsLoaded)
            {
                var path = CurrentPackage.Folder_Root;
                if (state == State.Add || state == State.Edit)
                {
                    if (current.Value.title != null)
                    {
                        //In Add/Edit item mode, open its current folder instead of the package folder
                        var cleanedCurrent = Helper.CleanUpText(current.Value.title);
                        if (CurrentPackage.INFO.SingleApp == "yes")
                            cleanedCurrent = "";
                        path = getItemPath(cleanedCurrent);
                    }
                }
                Process.Start(path);
            }
            else if (!(string.IsNullOrEmpty(Properties.Settings.Default.PackageRoot)) && Directory.Exists(Properties.Settings.Default.PackageRoot))
            {
                var path = Properties.Settings.Default.PackageRoot;
                Process.Start(path);
            }
        }

        private void menuDelete_Click(object sender, EventArgs e)
        {
            if (CurrentPackage != null && CurrentPackage.IsLoaded && MessageBoxEx.Show(this, "Are you sure that ou want to delete this Package?\r\n\r\nThis cannot be undone!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                var path = CurrentPackage.Folder_Root;

                // Close the current Package without trying to save pending changes. 
                mainHelper.CloseCurrentPackage(false);

                mainHelper.RemoveRecentPathFromSettings(path);
                Properties.Settings.Default.Save();
                PrepareRecentsMenu();
                var ex = Helper.DeleteDirectory(path);
                if (ex != null)
                {
                    MessageBoxEx.Show(this, string.Format("A Fatal error occured while trying to delete {0}: {1}", path, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            // Close the current Package. This is going to prompt the user to save changes if any. 
            mainHelper.CloseCurrentPackage(true);
        }

        private void menuDocumentation_Click(object sender, EventArgs e)
        {
            var info = new ProcessStartInfo("https://github.com/vletroye/Mods/wiki");
            info.UseShellExecute = true;
            Process.Start(info);
        }

        private void menuSupport_Click(object sender, EventArgs e)
        {
            var info = new ProcessStartInfo("https://github.com/vletroye/Mods/issues");
            info.UseShellExecute = true;
            Process.Start(info);
        }

        private void menuDevGuide_Click(object sender, EventArgs e)
        {
            var info = new ProcessStartInfo("https://help.synology.com/developer-guide/");
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
                if (CurrentPackage.INFO.SingleApp != "yes" && !TransformIntoSingleApp())
                    checkBoxSingleApp.Checked = false;
            }
            else
            {
                if (CurrentPackage != null && CurrentPackage.Config != null && CurrentPackage.Config.items.Count == 1)
                {
                    var title = CurrentPackage.Config.items.First().Value.title;
                    var type = CurrentPackage.Config.items.First().Value.itemType;
                    if (type != 0)
                    {
                        var cleanTitle = Helper.CleanUpText(title);

                        if (!CurrentPackage.Config.items.First().Value.url.Contains(string.Format("/{0}/", cleanTitle)))
                            if (CurrentPackage != null)
                            {
                                if (MessageBoxEx.Show(this, string.Format("Do you want to tansform {0} into a side by side app?", title), "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    var sourceWebAppFolder = getItemPath("");
                                    var targetWebAppFolder = getItemPath(cleanTitle);

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

                                        var oldName = string.Format("/webman/3rdparty/{0}/", CurrentPackage.INFO.Package);
                                        var newName = string.Format("/webman/3rdparty/{0}/{1}/", CurrentPackage.INFO.Package, cleanTitle);

                                        CurrentPackage.Config.items.First().Value.url = CurrentPackage.Config.items.First().Value.url.Replace(oldName, newName);
                                        mainHelper.Dirty = true;

                                        PrepareDataBinding(null);
                                        DisplayItem();
                                        focused.Focus();

                                        CurrentPackage.INFO.SingleApp = "no";

                                        // Force saving changes without prompting the user. This is required has changes have been done in the config file.
                                        mainHelper.SaveCurrentPackage(true);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBoxEx.Show(this, string.Format("An error occured while trying to move {0} to {1} : {2}", sourceWebAppFolder, targetWebAppFolder, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                    }
                                }
                                else
                                {
                                    CurrentPackage.INFO.SingleApp = "no";
                                }
                            }
                            else
                                checkBoxSingleApp.Checked = true;
                    }
                }
                else
                    if (CurrentPackage != null)
                    CurrentPackage.INFO.SingleApp = "no";
            }

            EnableItemDetails();
        }

        private bool TransformIntoSingleApp()
        {
            bool succeed = true;
            var list = CurrentPackage.Config;
            if (list != null)
            {
                if (list.items.Count == 1)
                {
                    var item = list.items.First();
                    if (MessageBoxEx.Show(this, string.Format("Do you want to tansform '{0}' into a single app?", item.Value.title), "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        succeed = TransformIntoSingleApp(item);
                    }
                    else
                    {
                        succeed = false;
                    }
                }
                else if (list.items.Count > 1)
                {
                    MessageBoxEx.Show(this, string.Format("You cannot tansform this package into a single app as it has already {0} items.", list.items.Count), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    succeed = false;
                }
            }


            return succeed;
        }

        private bool TransformIntoSingleApp(KeyValuePair<string, AppData> item)
        {
            var succeed = true;
            var title = item.Value.title;
            var type = item.Value.itemType;
            if (type == 0)
            {
                MessageBoxEx.Show(this, string.Format("You may not tansform the url '{0}' into a single app!", title), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                succeed = false;
            }
            else
            {
                var cleanTitle = Helper.CleanUpText(title);

                if (item.Value.url.Contains(string.Format("/{0}/", cleanTitle)))
                {
                    var sourceWebAppFolder = getItemPath(cleanTitle);
                    var targetWebAppFolder = getItemPath("");

                    succeed = MayTransformIntoSingleApp(sourceWebAppFolder);

                    if (succeed)
                    {
                        try
                        {
                            Helper.CopyDirectory(sourceWebAppFolder, targetWebAppFolder);
                            Helper.DeleteDirectory(sourceWebAppFolder);

                            var focused = Helper.FindFocusedControl(this);

                            var oldName = string.Format("/webman/3rdparty/{0}/{1}/", CurrentPackage.INFO.Package, cleanTitle);
                            var newName = string.Format("/webman/3rdparty/{0}/", CurrentPackage.INFO.Package);

                            item.Value.url = item.Value.url.Replace(oldName, newName);
                            mainHelper.Dirty = true;

                            PrepareDataBinding(null);
                            DisplayItem();
                            focused.Focus();

                            CurrentPackage.INFO.SingleApp = "yes";

                            // Force saving changes without prompting the user. This is required has changes have been done in the config file.
                            mainHelper.SaveCurrentPackage(true);
                        }
                        catch (Exception ex)
                        {
                            MessageBoxEx.Show(this, string.Format("A Fatal error occured while trying to move {0} to {1} : {2}", sourceWebAppFolder, targetWebAppFolder, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            succeed = false;
                        }
                    }
                }
                else if (CurrentPackage != null)
                    CurrentPackage.INFO.SingleApp = "yes";
            }
            return succeed;
        }

        private void menuImport_Click(object sender, EventArgs e)
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
                mainHelper.OpenExistingPackage(openFileDialog4Mods.FileName, true);
            }
        }

        //private string ImportPackage(string spk)
        //{
        //    string path = ExpandSpkInTempFolder(spk);
        //    OpenExistingPackage(path, true);
        //    return path;
        //}

        //private string ExpandSpkInTempFolder(string spk)
        //{
        //    var spkInfo = new FileInfo(spk);
        //    var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        //    Directory.CreateDirectory(path);
        //    spkInfo.CopyTo(Path.Combine(path, spkInfo.Name));

        //    if (!DeflatePackage(path))
        //        path = null;

        //    return path;
        //}

        private void menuMove_Click(object sender, EventArgs e)
        {
            PromptToMovePackage();
        }

        private bool PromptToMovePackage(string path = null)
        {
            bool succeed = true;
            mainHelper.Warnings.Clear();

            DialogResult result = DialogResult.Cancel;
            if (path == null)
            {
                folderBrowserDialog4Mods.Title = "Pick a target Root folder to move the Package currently opened.";
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
                        var name = CurrentPackage.INFO.Displayname;
                        var copies = Directory.GetDirectories(path, name + " (*)");
                        if (copies.Length > 0 || Directory.Exists(Path.Combine(path, name)))
                        {
                            var higher = 0;
                            foreach (var folder in copies)
                            {
                                var counting = Regex.Match(Regex.Match(folder, @"\(\d+\)$").Value, @"\d+").Value;
                                int count = 0;
                                int.TryParse(counting, out count);
                                if (count > higher)
                                    higher = count - 1;
                            }
                            do
                            {
                                higher++;
                                name = string.Format("{0} ({1})", name, higher);
                            } while (Directory.Exists(Path.Combine(path, name)));
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
                    var source = CurrentPackage.Folder_Root;
                    if (mainHelper.CloseCurrentPackage(true) == DialogResult.Yes)
                    {
                        Helper.CopyDirectory(source, path);
                        if (Directory.Exists(path) && !path.Equals(source, StringComparison.InvariantCultureIgnoreCase))
                            succeed = Helper.DeleteDirectory(source) == null;
                        else
                            succeed = path.Equals(source, StringComparison.InvariantCultureIgnoreCase);
                    }
                    else
                        succeed = false;

                    if (succeed) succeed = mainHelper.OpenExistingPackage(path);
                }
                catch (Exception ex)
                {
                    MessageBoxEx.Show(this, string.Format("A Fatal error occured while trying to move {0} to {1} : {2}", CurrentPackage.Folder_Root, path, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
                var portMatch = Helper.getPort.Match(url);
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
            if ((!(string.IsNullOrEmpty(textBoxPort.Text) || textBoxPort.Text.Equals("default", StringComparison.InvariantCultureIgnoreCase)) && comboBoxProtocol.SelectedIndex == 0))
                comboBoxProtocol.SelectedIndex = 1;
        }

        private void comboBoxProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProtocol.SelectedIndex == 0 && !string.IsNullOrEmpty(textBoxPort.Text) && !textBoxPort.Text.Equals("default", StringComparison.InvariantCultureIgnoreCase))
                comboBoxProtocol.SelectedIndex = 1;
        }

        private void textBoxPort_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (string.IsNullOrEmpty(textBoxPort.Text))
                    textBoxPort.Text = "default";
                else if (!textBoxPort.Text.Equals("default", StringComparison.InvariantCultureIgnoreCase))
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
            if (checkBoxLegacy.Focused && comboBoxItemType.SelectedIndex == (int)AppDataType.Url && checkBoxLegacy.Checked)
            {
                if (MessageBoxEx.Show(this, "If you enable this option with an external URL, you will have to disable the option 'Improve security with HTTP Content Security Policy (CSP) header' in the Control Panel > Security !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                {
                    checkBoxLegacy.Checked = false;
                }
                else
                {
                    comboBoxProtocol.SelectedIndex = 0;
                    textBoxPort.Text = "default";
                }
            }

            comboBoxProtocol.Enabled = !checkBoxLegacy.Checked;
            textBoxPort.Enabled = !checkBoxLegacy.Checked;
        }

        private void menuAdvancedEditor_Click(object sender, EventArgs e)
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

            labelMaxFirmware.Visible = advanced;
            textBoxMaxFirmware.Visible = advanced;
            labelMinFirmware.Visible = advanced;
            textBoxMinFirmware.Visible = advanced;
            checkBoxSingleApp.Visible = advanced;
            //checkBoxBeta.Visible = advanced;
            //TextBoxReportUrl.Visible = advanced && checkBoxBeta.Checked;
            //labelReportUrl.Visible = TextBoxReportUrl.Visible;

            checkBoxOfflineInstall.Visible = advanced;
            checkBoxSilentInstalll.Visible = advanced;
            checkBoxSilentUpgrade.Visible = advanced;
            checkBoxSilentUninstall.Visible = advanced;
            checkBoxStartable.Visible = advanced;
            checkBoxRemovable.Visible = advanced;
            checkBoxPrecheck.Visible = advanced;
            checkBoxSilentReboot.Visible = advanced;

            checkBoxSupportCenter.Visible = advanced;
            //checkBoxLegacy.Visible = advanced;
            labelGrantPrivilege.Visible = advanced;
            ComboBoxGrantPrivilege.Visible = advanced;
            checkBoxAdvanceGrantPrivilege.Visible = advanced;
            checkBoxConfigPrivilege.Visible = advanced;

            checkBoxAdminUrl.Visible = advanced;
            textBoxAdminPort.Visible = advanced && checkBoxAdminUrl.Checked;
            textBoxAdminUrl.Visible = advanced && checkBoxAdminUrl.Checked;
            comboBoxAdminProtocol.Visible = advanced && checkBoxAdminUrl.Checked;
            checkBoxCheckPort.Visible = advanced && checkBoxAdminUrl.Checked;

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

        private void textBoxFirmware_Validated(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                errorProvider.SetError(textBox, "");
            }
        }

        private void textBoxFirmware_Validating(object sender, CancelEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                Helper.ValidateFirmware(textBox, e, errorProvider);

                if (mainHelper.IsDSM7x && !string.IsNullOrWhiteSpace(textBox.Text) && !Helper.CheckDSMVersionMin(textBox.Text, 7, 0, 40000))
                {
                    if (MessageBoxEx.Show(this, "Downgrading a package from DSM 7.x is not supported (but it could work if you do manually all the required changes). Do you really want to continue ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        e.Cancel = true;
                        textBox.Text = CurrentPackage.INFO.Os_min_ver;
                    }
                    else
                    {
                        mainHelper.IsDSM7x = false;
                    }
                }
                if (!mainHelper.IsDSM7x && !string.IsNullOrWhiteSpace(textBox.Text) && Helper.CheckDSMVersionMin(textBox.Text, 7, 0, 40000))
                {
                    if (MessageBoxEx.Show(this, "Upgrading a package to DSM 7.x is not supported (but it could work if you do manually all the required changes). Do you really want to continue ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        e.Cancel = true;
                        textBox.Text = CurrentPackage.INFO.Os_min_ver;
                    }
                    else
                    {
                        mainHelper.IsDSM7x = true;
                    }
                }

            }
        }

        private void checkBoxAdminUrl_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxAdminProtocol.Visible = checkBoxAdminUrl.Checked;
            textBoxAdminPort.Visible = checkBoxAdminUrl.Checked;
            textBoxAdminUrl.Visible = checkBoxAdminUrl.Checked;
            checkBoxCheckPort.Visible = checkBoxAdminUrl.Checked;

            if (checkBoxAdminUrl.Checked && CurrentPackage != null && CurrentPackage.INFO.CheckPort != null)
            {
                CurrentPackage.INFO.CheckPort = "yes";
                checkBoxCheckPort.Checked = true;
            }
            if (!checkBoxAdminUrl.Checked && CurrentPackage != null && CurrentPackage.INFO.CheckPort != null)
            {
                CurrentPackage.INFO.CheckPort = null;
                checkBoxCheckPort.Checked = false;
            }
        }

        private void toolStripMenuItemPublish_Click(object sender, EventArgs e)
        {
            PublishPackage();
        }

        private void menuManageScreenshots_Click(object sender, EventArgs e)
        {
            var snapshotManager = new SnapshotManager(CurrentPackage.Folder_Root);
            snapshotManager.ShowDialog(this);
        }

        private void toolStripMenuItemChangeLog_Click(object sender, EventArgs e)
        {
            var changelog = textBoxChangeBox.Text;
            var content = new ScriptInfo(changelog, "ChangeLog Editor");

            DialogResult result = Helper.ScriptEditor(content, null, null);
            if (result == DialogResult.OK)
            {
                if (textBoxChangeBox.Text.Trim() != content.Code.Trim())
                    menuChangeLog.Image = new Bitmap(Properties.Resources.EditedScript);
                textBoxChangeBox.Text = content.Code;
            }
        }

        private void SetInfoTag(Control control)
        {
            var tags = control.Tag.ToString().Split(';');
            foreach (var tag in tags)
            {
                if (tag.StartsWith("PKG"))
                    CurrentPackage.INFO[tag.Substring(3)] = control.Text;
            }
        }

        private void buttonDependencies_Click(object sender, EventArgs e)
        {
            SetInfoTag(textBoxMinFirmware);
            var edit = new Dependencies(CurrentPackage.INFO);
            if (edit.ShowDialog(this) == DialogResult.OK) mainHelper.Dirty = true;
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

        private void menuParameters_Click(object sender, EventArgs e)
        {
            var param = new Parameters();
            param.ShowDialog();
            if (Properties.Settings.Default.Recents != null && Properties.Settings.Default.Recents.Count == 0)
                PrepareRecentsMenu();

            // ReLoad AutoComplete for Firmware
            Helper.LoadDSMReleases(textBoxMinFirmware);
            Helper.LoadDSMReleases(textBoxMaxFirmware);
        }

        private void pictureBoxWarning_Click(object sender, EventArgs e)
        {
            ShowWarnings();
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            DragDropEffects effects = DragDropEffects.None;
            validPackage4DragDrop = null;
            if (state == State.None || state == State.View)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    var paths = ((string[])e.Data.GetData(DataFormats.FileDrop));
                    if (paths.Length == 1)
                    {
                        validPackage4DragDrop = paths[0];
                        if (Directory.Exists(validPackage4DragDrop))
                            effects = DragDropEffects.Copy;
                        else if (File.Exists(validPackage4DragDrop) && (Path.GetExtension(validPackage4DragDrop).Equals(".SPK", StringComparison.InvariantCultureIgnoreCase) || Path.GetFileName(validPackage4DragDrop).Equals("INFO", StringComparison.InvariantCultureIgnoreCase)))
                            effects = DragDropEffects.Copy;
                        else
                            validPackage4DragDrop = null;
                    }
                }
            }
            e.Effect = effects;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (validPackage4DragDrop != null)
            {
                // Close the current Package. This is going to prompt the user to save changes if any. 
                var saved = mainHelper.CloseCurrentPackage();

                if (saved == DialogResult.Yes || saved == DialogResult.No)
                {
                    mainHelper.OpenExistingPackage(validPackage4DragDrop, true);
                }

                validPackage4DragDrop = null;
            }
        }

        private void menuReviewPendingChanges_Click(object sender, EventArgs e)
        {
            ValidateChildren();

            var changes = new List<Tuple<string, string, string>>();
            var exist = CheckChanges(changes);
            if (exist)
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in changes)
                {
                    message.AppendFormat("{0}: {1} => {2}\r\n", item.Item1, item.Item2, item.Item3);
                }

                MessageBoxEx.Show(this, message.ToString(), "Pending Changes", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
                MessageBoxEx.Show(this, "There is no pending change to be saved.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

        }

        private void menuLicense_Click(object sender, EventArgs e)
        {
            var menu = (ToolStripMenuItem)sender;
            var scriptName = menu.Tag.ToString();
            var scriptPath = Path.Combine(CurrentPackage.Folder_Root, scriptName);
            var license = "";

            if (File.Exists(scriptPath))
                license = File.ReadAllText(scriptPath);
            else
                license = Properties.Settings.Default.License;

            var script = new ScriptInfo(license, menu.Text, new Uri("https://help.synology.com/developer-guide/synology_package/package_structure.html"), "Info about LICENSE");
            DialogResult result = Helper.ScriptEditor(script, null, null);
            if (result == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(script.Code))
                {
                    File.Delete(scriptPath);
                }
                else
                {
                    Helper.WriteAnsiFile(scriptPath, script.Code);
                    menu.Image = new Bitmap(Properties.Resources.EditedScript);
                }
            }
        }

        private string getItemPath(string itemName)
        {
            return Path.Combine(CurrentPackage.Folder_UI, itemName);
        }
        private string getItemPath(string itemName, string scriptName)
        {
            return Path.Combine(getItemPath(itemName), scriptName);
        }

        private void labelAddResources_DragEnter(object sender, DragEventArgs e)
        {
            if (state == State.Add || state == State.Edit)
            {
                validFiles4DragDrop = Helper.GetDragDropFilenames(out filesDragDropPath, e);
                if (validFiles4DragDrop)
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                    e.Effect = DragDropEffects.None;
            }
            else
            {
                validFiles4DragDrop = false;
                e.Effect = DragDropEffects.None;
            }
        }

        private void labelAddResources_DragDrop(object sender, DragEventArgs e)
        {
            if (validFiles4DragDrop)
            {
                var cleanedCurrent = Helper.CleanUpText(current.Value.title);
                if (CurrentPackage.INFO.SingleApp == "yes")
                    cleanedCurrent = "";

                var target = getItemPath(cleanedCurrent);
                foreach (var path in filesDragDropPath)
                {
                    if (Helper.IsDirectory(path))
                        Helper.CopyDirectory(path, target);
                    else
                        if (Helper.IsFile(path))
                        File.Copy(path, Path.Combine(target, Path.GetFileName(path)));
                }
            }
        }

        private void menuDefaultRunner_Click(object sender, EventArgs e)
        {
            var file = Path.Combine(Helper.ResourcesDirectory, "default.runner");

            var content = File.ReadAllText(file);
            var runner = new ScriptInfo(content, "Default Runner", new Uri("https://stackoverflow.com/questions/20107147/php-reading-shell-exec-live-output"), "Reading shell_exec live output in PHP");
            DialogResult result = Helper.ScriptEditor(null, runner, null);
            if (result == DialogResult.OK)
            {
                Helper.WriteAnsiFile(file, runner.Code);
                menuDefaultRunner.Image = new Bitmap(Properties.Resources.EditedScript);
            }
        }

        private void menuDefaultRouterConfig_Click(object sender, EventArgs e)
        {
            var file = Path.Combine(Helper.ResourcesDirectory, "dsm.cgi.conf");

            var content = File.ReadAllText(file);
            var routerConfig = new ScriptInfo(content, "Default Router Config", new Uri("https://github.com/vletroye/SynoPackages/wiki/MODS-Advanced-Test-CGI"), "Redirect all calls to a CGI Router");
            DialogResult result = Helper.ScriptEditor(null, routerConfig, null);
            if (result == DialogResult.OK)
            {
                Helper.WriteAnsiFile(file, routerConfig.Code);
                menuDefaultRouterConfig.Image = new Bitmap(Properties.Resources.EditedScript);
            }
        }

        private void menuDefaultRouterScript_Click(object sender, EventArgs e)
        {
            var file = Path.Combine(Helper.ResourcesDirectory, "router.cgi");

            var content = File.ReadAllText(file);
            var routercgi = new ScriptInfo(content, "Default Router Script", new Uri("https://github.com/vletroye/SynoPackages/wiki/MODS-Advanced-Test-CGI"), "CGI Router handling calls to php");
            DialogResult result = Helper.ScriptEditor(routercgi, null, null);
            if (result == DialogResult.OK)
            {
                Helper.WriteAnsiFile(file, routercgi.Code);
                menuDefaultRouterScript.Image = new Bitmap(Properties.Resources.EditedScript);
            }
        }

        private void menuRouterConfig_Click(object sender, EventArgs e)
        {
            string file = null;
            if (CurrentPackage != null && CurrentPackage.IsDmsUiDirDefined)
            {
                file = CurrentPackage.Path_Config;
                if (File.Exists(file))
                {
                    file = file.Replace("config", "dsm.cgi.conf");

                    if (File.Exists(file))
                    {
                        var content = File.ReadAllText(file);
                        var routerConfig = new ScriptInfo(content, "Router Config", new Uri("https://github.com/vletroye/SynoPackages/wiki/MODS-Advanced-Test-CGI"), "Redirect all calls to a CGI Router");
                        DialogResult result = Helper.ScriptEditor(null, routerConfig, null);
                        if (result == DialogResult.OK)
                        {
                            Helper.WriteAnsiFile(file, routerConfig.Code);
                            menuRouterConfig.Image = new Bitmap(Properties.Resources.EditedScript);
                        }
                    }
                }
            }
        }

        private void menuRouterScript_Click(object sender, EventArgs e)
        {
            string file = null;
            if (CurrentPackage != null && CurrentPackage.IsDmsUiDirDefined)
            {
                file = CurrentPackage.Path_Config;
                if (File.Exists(file))
                {
                    file = file.Replace("config", "router.cgi");

                    if (File.Exists(file))
                    {
                        var content = File.ReadAllText(file);
                        var routercgi = new ScriptInfo(content, "Router Script", new Uri("https://github.com/vletroye/SynoPackages/wiki/MODS-Advanced-Test-CGI"), "CGI Router handling calls to php");
                        DialogResult result = Helper.ScriptEditor(routercgi, null, null);
                        if (result == DialogResult.OK)
                        {
                            Helper.WriteAnsiFile(file, routercgi.Code);
                            menuRouterScript.Image = new Bitmap(Properties.Resources.EditedScript);
                        }
                    }
                }
            }
        }

        private void portConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePackageInfo();

            if (!Helper.CheckDSMVersionMin(CurrentPackage.INFO, 6, 0, 5936))
            {
                MessageBoxEx.Show(this, "Port-Config worker is only supported by firmware >= 6.0-5936", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                string protocolFilePath = null;
                IniData portConfigData = null;
                var portConfig = CurrentPackage.Resource.PortConfig;
                if (portConfig != null)
                {
                    var protocolFile = CurrentPackage.Resource.PortConfig.ProtocolFile;
                    if (protocolFile == null) // Maybe the old style ?
                    {
                        //TODO: review why this was coded in the old version ? Does it exist ?
                        protocolFilePath = Path.Combine(CurrentPackage.Folder_Root, portConfig.ToString().Replace("/", "\\"));
                    }
                    else
                    {
                        protocolFilePath = Path.Combine(CurrentPackage.Folder_Package, protocolFile.ToString().Replace("/", "\\"));
                    }
                    if (File.Exists(protocolFilePath))
                    {
                        //Read the INI protocol file (remove space before and after the '=')
                        IniParserConfiguration config = new IniParserConfiguration();
                        config.AssigmentSpacer = "";
                        var parser = new IniDataParser(config);
                        var fileParser = new FileIniDataParser(parser);
                        try
                        {
                            portConfigData = fileParser.ReadFile(protocolFilePath);
                        }
                        catch
                        {
                            MessageBoxEx.Show(this, string.Format("Service Configuration file {0} can't be parsed.", protocolFile.ToString()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        }
                    }
                }

                var worker = new PortConfigWorker(portConfig, portConfigData, GetAllWizardVariables(), CurrentPackage.INFO.Package);
                if (worker.ShowDialog(this) == DialogResult.OK && worker.PendingChanges())
                {
                    var obj = JsonConvert.DeserializeObject<JObject>(worker.PortConfig.ToString());
                    portConfig = obj.ToObject<PortConfig>();
                    portConfigData = worker.SynoConfig;

                    if (portConfig != null)
                    {
                        var protocolFile = CurrentPackage.Resource.PortConfig.ProtocolFile;
                        if (protocolFile == null) // Maybe the old style ?
                        {
                            //TODO: review why this was coded in the old version ? Does it exist ?
                            protocolFilePath = Path.Combine(CurrentPackage.Folder_Root, portConfig.ToString().Replace("/", "\\"));
                        }
                        else
                        {
                            protocolFilePath = Path.Combine(CurrentPackage.Folder_Package, protocolFile.ToString().Replace("/", "\\"));
                        }
                        if (protocolFilePath != null && protocolFilePath.EndsWith(".sc"))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(protocolFilePath));

                            //Read the INI protocol string and remove space before and after the '='
                            IniParserConfiguration config = new IniParserConfiguration();
                            config.AssigmentSpacer = "";
                            var parser = new IniDataParser(config);
                            var streamParser = new StreamIniDataParser(parser);
                            using (var stream = Helper.GetStreamFromString(portConfigData.ToString()))
                            {
                                using (var streamReader = new StreamReader(stream))
                                {
                                    portConfigData = streamParser.ReadData(streamReader);
                                }
                            }

                            //Save the INI protocol file
                            Helper.WriteAnsiFile(protocolFilePath, portConfigData.ToString());
                            //var fileParser = new FileIniDataParser(parser);
                            //fileParser.WriteFile(protocolFile, portConfigData, Encoding.GetEncoding(1252));


                            //Update the Resource file
                            //if (resource == null) resource = JsonConvert.DeserializeObject<JObject>("{}");
                            CurrentPackage.Resource.PortConfig = portConfig;
                        }
                    }
                    else
                    {
                        //Update the Resource file (or delete it) and delete the INI Protocol file
                        if (!string.IsNullOrEmpty(protocolFilePath) && File.Exists(protocolFilePath))
                        {
                            File.Delete(protocolFilePath);
                            var synoConfigDir = Path.GetDirectoryName(protocolFilePath);
                            if (Directory.EnumerateFileSystemEntries(synoConfigDir).Count() == 0)
                                Directory.Delete(synoConfigDir);
                        }
                        //Remove port Config
                        CurrentPackage.Resource.PortConfig = null;
                    }

                    //Save the Resource file
                    SaveResourceConfig();
                }
            }
        }

        private void syslogConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePackageInfo();

            if (!Helper.CheckDSMVersionMin(CurrentPackage.INFO, 6, 0, 7145))
            {
                MessageBoxEx.Show(this, "Syslog config worker is only supported by firmware >= 6.0-7145", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBoxEx.Show(this, "This Worker is not yet implemented.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void pHPINIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePackageInfo();

            if (!Helper.CheckDSMVersionMin(CurrentPackage.INFO, 4, 2, 3160))
            {
                MessageBoxEx.Show(this, "Php INI worker is only supported by firmware >= 4.2-3160", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBoxEx.Show(this, "This Worker is not yet implemented.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void mariaDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePackageInfo();

            if (!Helper.CheckDSMVersionMin(CurrentPackage.INFO, 5, 5, 0062))
            {
                MessageBoxEx.Show(this, "Maria DB worker is only supported by firmware >= 5.5.47-0062", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBoxEx.Show(this, "This Worker is not yet implemented.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void indexDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePackageInfo();

            if (!Helper.CheckDSMVersionMin(CurrentPackage.INFO, 6, 0, 5924))
            {
                MessageBoxEx.Show(this, "index DB worker is only supported by firmware >= 6.0-5924", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBoxEx.Show(this, "This Worker is not yet implemented.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void dataShareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePackageInfo();

            if (!Helper.CheckDSMVersionMin(CurrentPackage.INFO, 6, 0, 5941))
            {
                MessageBoxEx.Show(this, "Data Share worker is only supported by firmware >= 6.0-5941", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBoxEx.Show(this, "This Worker is not yet implemented.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void apacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePackageInfo();

            if (!Helper.CheckDSMVersionMin(CurrentPackage.INFO, 4, 2, 3160))
            {
                MessageBoxEx.Show(this, "Apache worker is only supported by firmware >= 4.2-3160", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBoxEx.Show(this, "This Worker is not yet implemented.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void linkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePackageInfo();

            if (!Helper.CheckDSMVersionMin(CurrentPackage.INFO, 6, 0, 5941))
            {
                MessageBoxEx.Show(this, "Linker worker is only supported by firmware >= 6.0-5941", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                var ui = CurrentPackage.Folder_UI;
                var worker = new Worker_Linker(CurrentPackage);
                if (worker.ShowDialog(this) == DialogResult.OK && worker.PendingChanges())
                {
                    //TODO: Review the Edit and Save of Linker

                    //linkerConfig = worker.Specification;

                    //if (linkerConfig != null && linkerConfig.HasValues)
                    //{
                    //    //Update the Resource file
                    //    if (resource == null) resource = JsonConvert.DeserializeObject<JObject>("{}");
                    //    resource["usr-local-linker"] = linkerConfig;
                    //}
                    //else
                    //{
                    //    if (resource != null)
                    //        resource.Remove("usr-local-linker");
                    //}

                    //Save the Resource file
                    SaveResourceConfig();
                }
            }
        }

        private void pkgDependenciesToolStripMenu_Click(object sender, EventArgs e)
        {
            EditPackageConfig(CurrentPackage.Path_Pkg_Deps, "Define dependency between packages with restrictions of DSM version. Before your package is installed or upgraded, these packages must be installed first. Package Center controls the order of start or stop packages according to the dependency.");
        }


        private void pkgConflictsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditPackageConfig(CurrentPackage.Path_Pkg_Conx, "Define conflicts between packages with restrictions of DSM version. Before your package is installed or upgraded, these conflicting packages cannot be installed.");
        }

        private void EditPackageConfig(string pkgFile, string doc)
        {
            UpdatePackageInfo();

            if (!Helper.CheckDSMVersionMin(CurrentPackage.INFO, 4, 2, 3160))
            {
                MessageBoxEx.Show(this, "PKG_xxxx are only supported by firmware >= 4.2.3160", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                IniData pkgConfig = null;
                if (File.Exists(pkgFile))
                {
                    //Read the INI PKG_DEPS file (remove space before and after the '=')
                    IniParserConfiguration config = new IniParserConfiguration();
                    config.AssigmentSpacer = "";
                    var parser = new IniDataParser(config);
                    var fileParser = new FileIniDataParser(parser);
                    try
                    {
                        pkgConfig = fileParser.ReadFile(pkgFile);
                    }
                    catch
                    {
                        MessageBoxEx.Show(this, string.Format("PKG file {0} can't be parsed.", pkgFile), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                }

                var pkgConfigEditor = new PKG_Conf(pkgConfig, GetAllWizardVariables(), Path.GetFileName(pkgFile), doc);
                if (pkgConfigEditor.ShowDialog(this) == DialogResult.OK && pkgConfigEditor.PendingChanges())
                {
                    pkgConfig = pkgConfigEditor.PkgConf;
                    if (pkgConfig != null)
                    {
                        var pkg_copy = new IniData();
                        foreach (var section in pkgConfig.Sections)
                        {
                            pkg_copy.Sections.AddSection(section.SectionName);
                            var section_copy = pkg_copy.Sections[section.SectionName];
                            foreach (var key in section.Keys)
                            {
                                var value = Helper.Unquote(key.Value);
                                if (!string.IsNullOrEmpty(value))
                                    section_copy.AddKey(key.KeyName, value);
                            }
                        }
                        pkgConfig = pkg_copy;

                        Directory.CreateDirectory(Path.GetDirectoryName(pkgFile));

                        //Read the INI PKG string and remove space before and after the '='
                        IniParserConfiguration config = new IniParserConfiguration();
                        config.AssigmentSpacer = "";
                        var parser = new IniDataParser(config);
                        var streamParser = new StreamIniDataParser(parser);
                        using (var stream = Helper.GetStreamFromString(pkgConfig.ToString()))
                        {
                            using (var streamReader = new StreamReader(stream))
                            {
                                pkgConfig = streamParser.ReadData(streamReader);
                            }
                        }

                        //Save the INI protocol file
                        //var fileParser = new FileIniDataParser(parser);
                        //fileParser.WriteFile(pkgConfigFile, pkgConfig);
                        Helper.WriteAnsiFile(pkgFile, pkgConfig.ToString());
                    }
                    else
                    {
                        //Update the Resource file (or delete it) and delete the INI PKG
                        if (!string.IsNullOrEmpty(pkgFile) && File.Exists(pkgFile))
                        {
                            File.Delete(pkgFile);
                            var synoConfigDir = Path.GetDirectoryName(pkgFile);
                            if (Directory.EnumerateFileSystemEntries(synoConfigDir).Count() == 0)
                                Directory.Delete(synoConfigDir);
                        }
                    }

                    ResetEditScriptMenuIcons();
                }
            }
        }

        private void menuPackage_Click(object sender, EventArgs e)
        {
            var changes = new List<Tuple<string, string, string>>();
            var exist = CheckChanges(changes);
            if (exist)
                menuReviewPendingChanges.Image = new Bitmap(Properties.Resources.EditedScript);
            else
                menuReviewPendingChanges.Image = null;
        }

        private void UnpublishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SSPKRepository))
                UnpublishPackage(SSPKRepository);
        }

        private void privilegeConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePackageInfo();

            if (!Helper.CheckDSMVersionMin(CurrentPackage.INFO, 6, 0, 7145))
            {
                MessageBoxEx.Show(this, "Privilege-Config worker is only supported by firmware >= 6.0-7145", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {

                var privilegeEditor = new Worker_Privilege(CurrentPackage.Privilege, CurrentPackage.INFO);
                if (privilegeEditor.ShowDialog(this) == DialogResult.OK)
                {
                    //privilege = privilegeEditor.Specification as JObject;
                    CurrentPackage.Privilege = PackagePrivilege.Load(CurrentPackage.Path_Privilege, privilegeEditor.GetPrivilege());

                    //Save the Resource file
                    SavePrivilegeConfig();
                }
                else
                {

                }

                ResetEditScriptMenuIcons();
            }
        }

        private void webServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePackageInfo();

            if (!Helper.CheckDSMVersionMin(CurrentPackage.INFO, 6, 0, 5941))
            {
                MessageBoxEx.Show(this, "Web Service worker is only supported by firmware >= 7.0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            else
            {
                //var WebServiceConfig = resource == null ? null : resource.SelectToken("webservice");

                //var test = "{ \"webservice\": { \"services\": [{ \"service\": \"myapplication\", \"display_name\": \"MyApplication\", \"support_alias\": true, \"support_server\": true, \"type\": \"nginx_php\", \"root\": \"myapplication\", \"icon\": \"ui/MyIcon_{0}.png\", \"intercept_errors\": false, \"php\": { \"profile_name\": \"MyApplication Profile\", \"profile_desc\": \"PHP Profile for MyApplication\", \"backend\": 8, \"open_basedir\": \"/var/services/web_packages/myapplication:/tmp:/var/services/tmp\", \"extensions\": [ \"curl\", \"dom\", \"exif\", \"fileinfo\", \"gd\", \"hash\", \"iconv\", \"imagick\", \"json\", \"mbstring\", \"mysql\", \"mysqli\", \"openssl\", \"pcre\", \"pdo_mysql\", \"xml\", \"zlib\", \"zip\" ], \"php_settings\": { \"mysql.default_socket\": \"/run/mysqld/mysqld10.sock\", \"mysqli.default_socket\": \"/run/mysqld/mysqld10.sock\", \"pdo_mysql.default_socket\": \"/run/mysqld/mysqld10.sock\" }, \"user\": \"MyApplication\", \"group\": \"http\" }, \"connect_timeout\": 60, \"read_timeout\": 3600, \"send_timeout\": 60 }], \"portals\": [{ \"service\": \"myapplication\", \"type\": \"alias\", \"name\": \"myapplication1\", \"display_name\": \"My Application 1\", \"alias\": \"myapplication1\", \"app\": \"com.mycompany.app\" }], \"pkg_dir_prepare\": [{ \"source\": \"/var/packages/MODS_DemoUiSpk7/target/src\", \"target\": \"myapplication\", \"mode\": \"0755\", \"group\": \"http\", \"user\": \"MyApplication\" }] }, \"usr-local-linker\": { \"bin\": [\"bin/demouispk7-cli\"] }}";

                var ui = CurrentPackage.Folder_UI;
                var worker = new Worker_WebService(CurrentPackage.Resource);
                if (worker.ShowDialog(this) == DialogResult.OK && worker.PendingChanges())
                {
                    //TODO: Review the edit and save of WebService

                    //WebServiceConfig = worker.Specification;

                    //if (WebServiceConfig != null && WebServiceConfig.HasValues)
                    //{
                    //    //Update the Resource file
                    //    if (resource == null) resource = JsonConvert.DeserializeObject<JObject>("{}");
                    //    resource["webservice"] = WebServiceConfig;
                    //}
                    //else
                    //{
                    //    if (resource != null)
                    //        resource.Remove("webservice");
                    //}

                    //Save the Resource file
                    SaveResourceConfig();
                }
            }
        }

        private void buttonPreloadTexts_Click(object sender, EventArgs e)
        {
            if (CurrentPackage != null && CurrentPackage.IsDmsUiDirDefined)
            {
                var file = CurrentPackage.Path_Strings;
                if (File.Exists(file))
                {
                    var preloadTexts = new PreloadTexts(file, current.Value.preloadTexts);
                    if (preloadTexts.ShowDialog() == DialogResult.OK)
                    {
                        var strings = preloadTexts.strings;
                        candidate_preloads = new List<string>();
                        foreach (var elements in strings)
                        {
                            candidate_preloads.Add(elements[0] + ":" + elements[1]);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, "You must first define Strings for this package.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void toolStripMenuItemStrings_Click(object sender, EventArgs e)
        {
            if (CurrentPackage != null && CurrentPackage.IsDmsUiDirDefined)
            {
                var file = CurrentPackage.Path_Strings;
                string content = "";
                if (File.Exists(file))
                {
                    content = File.ReadAllText(file);
                }

                var strings = new ScriptInfo(content, "Strings", new Uri("https://help.synology.com/developer-guide/integrate_dsm/i18n.html"), "Strings (i18n) referenced by config, help, etc...");
                DialogResult result = Helper.ScriptEditor(strings, null, null);
                if (result == DialogResult.OK)
                {
                    Helper.WriteAnsiFile(file, strings.Code);
                    toolStripMenuItemStrings.Image = new Bitmap(Properties.Resources.EditedScript);
                }
            }
        }

        internal void Reset_UI()
        {
            // Reset Warnings and releated icon
            // TODO: embed warinings and related feeatures in a class
            mainHelper.Warnings.Clear();
            pictureBoxWarning.Visible = false;

            //Disable all items on the Screen
            //groupBoxItem.Enabled = false;
            //groupBoxPackage.Enabled = false;
            comboBoxTransparency.SelectedIndex = 0;
            mainHelper.PicturePkg_256 = null;
            mainHelper.PicturePkg_120 = null;
            mainHelper.PicturePkg_72 = null;

            //Todo: check if this is usefull here (used to be done before refactoring)
            //CurrentPackage.Config = null;

            DisplayPackage();
        }
        internal void DisplayPackage()
        {
            ShowAdvancedEditor(Properties.Settings.Default.AdvancedEditor);

            ResetValidateChildren(this); // Reset Error Validation on all controls
            ResetEditScriptMenuIcons(); // Reset the icon showing that a script has been edited and must be saved
            PrepareRecentsMenu(); // Reset the "Open Recent" menu
            PrepareDataBinding(null);

            ProcessCurrentPackage();

            DisplayItem();
            EnableItemDetails();

            labelToolTip.Text = "";
            textBoxDisplay.Focus();
        }

        internal void Reset_UI_menuReviewPendingChanges()
        {
            menuReviewPendingChanges.Image = null;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mainHelper != null) mainHelper.Close();
            mainHelper = null;
        }
    }
}
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
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
        const string CONFIGFILE = @"package\ui\config";
        static Regex getPort = new Regex(@"^:(\d*).*$", RegexOptions.Compiled);
        static Regex getVersion = new Regex(@"^\d*.\d*.\d*$", RegexOptions.Compiled);

        string defaultRunnerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "default.runner");
        string ResourcesRootPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources");
        string PackageRootPath = Properties.Settings.Default.PackageRoot;
        string WebSynology = Properties.Settings.Default.Synology;
        string WebProtocol = Properties.Settings.Default.Protocol;
        int WebPort = Properties.Settings.Default.Port;

        //3 next vars are a Dirty Hack - move these 3 vars into a class. Replace AppsData with that class, ...
        string webAppFolder = null;
        string webAppIndex = null;
        string scriptValue = null;
        string runnerValue = null;

        Dictionary<string, PictureBox> pictureBoxes;
        Dictionary<string, string> info;

        KeyValuePair<string, AppsData> current;
        State state;
        Package list;
        bool dirty = false;

        string imageDragDropPath;
        protected bool validData;
        #endregion  --------------------------------------------------------------------------------------------------------------------------------

        #region Initialize -------------------------------------------------------------------------------------------------------------------------
        public MainForm()
        {
            InitializeComponent();

            if (!File.Exists(defaultRunnerPath))
                File.WriteAllText(defaultRunnerPath, Properties.Settings.Default.Ps_Exec);

            comboBoxTransparency.SelectedIndex = 0;

            InitListView();

            GetItemPictureBoxes();

            groupBoxItem.Enabled = false;
            groupBoxPackage.Enabled = false;

            if (string.IsNullOrEmpty(PackageRootPath) || !Directory.Exists(PackageRootPath))
            {
                MessageBox.Show(this, "The destination path for your package does not exist anymore. Reconfigure it and possibly 'recover' your icons.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PackageRootPath = "";
                DisplayItem();
            }
            else if (!File.Exists(Path.Combine(PackageRootPath, "INFO")))
            {
                MessageBox.Show(this, "The INFO file for your package does not exist anymore. Reset your package.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PackageRootPath = "";
                DisplayItem();
            }
            else
            {
                InitData();
                BindData(list);
                DisplayItem();
                LoadPackageInfo();
            }
            foreach (var control in groupBoxItem.Controls)
            {
                var item = control as Control;
                if (item != null)
                {
                    item.MouseEnter += new System.EventHandler(this.OnMouseEnter);
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
                    item.MouseLeave += new System.EventHandler(this.OnMouseLeave);

                    if (item.Name.StartsWith("textBox") && Helper.IsSubscribed(item, "EventValidating"))
                        item.TextChanged += new System.EventHandler(this.OnTextChanged);
                }
            }

            var linkTimeLocal = Helper.GetLinkerTime(Assembly.GetExecutingAssembly());
            this.Text += string.Format(" [{0} Build {1}]", Properties.Settings.Default.Version, linkTimeLocal);
            textBoxPackage.Focus();

            CreateRecentsMenu();
        }

        private void CreateRecentsMenu()
        {
            openRecentToolStripMenuItem.DropDownItems.Clear();
            if (Properties.Settings.Default.Recents != null)
            {
                ToolStripMenuItem[] items = new ToolStripMenuItem[Properties.Settings.Default.Recents.Count];
                for (int item = items.Length - 1; item >= 0; item--)
                {
                    items[item] = new ToolStripMenuItem();
                    items[item].Name = "recentItem" + item.ToString();
                    items[item].Tag = Properties.Settings.Default.Recents[item];
                    items[item].Text = Properties.Settings.Default.RecentsName[item];
                    items[item].Click += new EventHandler(MenuItemOpenRecent_ClickHandler);
                }
                openRecentToolStripMenuItem.DropDownItems.AddRange(items);
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
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            labelToolTip.Text = "";
        }

        private void InitData()
        {
            info = new Dictionary<string, string>();
            state = State.None;
            LoadPackageConfig();
        }

        private void InitListView()
        {
            listViewItems.View = View.Details;
            listViewItems.GridLines = true;
            listViewItems.FullRowSelect = true;
            listViewItems.Columns.Add("Name", 200);
            listViewItems.Columns.Add("Uri", 580);
            listViewItems.Sorting = SortOrder.Ascending;
        }

        private void GetItemPictureBoxes()
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

        private void BindData(Package list)
        {
            listViewItems.Items.Clear();

            foreach (var item in list.items)
            {
                var uri = item.Value.url;
                if (item.Value.itemType == (int)UrlType.Url && uri.StartsWith("/"))
                    uri = string.Format("{0}://{1}:{2}{3}", item.Value.protocol, WebSynology, item.Value.port, uri);


                // Define the list items
                var lvi = new ListViewItem(item.Value.title);
                lvi.SubItems.Add(uri);
                lvi.Tag = item;

                // Add the list items to the ListView
                listViewItems.Items.Add(lvi);
            }

            listViewItems.Sort();
        }

        private void LoadPackageConfig()
        {
            var config = Path.Combine(PackageRootPath, CONFIGFILE);
            if (File.Exists(config))
            {
                var json = File.ReadAllText(config);
                list = JsonConvert.DeserializeObject<Package>(json, new KeyValuePairConverter());
            }

            if (list == null || list.items.Count == 0)
            {
                var json = Properties.Settings.Default.Packages;
                if (!string.IsNullOrEmpty(json))
                {
                    list = JsonConvert.DeserializeObject<Package>(json, new KeyValuePairConverter());
                }
                else
                {
                    list = new Package();
                }
            }
            groupBoxItem.Enabled = false;
        }

        private void LoadPackageInfo()
        {
            var file = Path.Combine(PackageRootPath, "INFO");

            if (File.Exists(file))
            {
                var lines = File.ReadAllLines(file);
                foreach (var line in lines)
                {
                    var key = line.Substring(0, line.IndexOf('='));
                    var value = line.Substring(line.IndexOf('=') + 1);
                    value = value.Trim(new char[] { '"' });
                    info.Add(key, value);
                }

                if (info["maintainer"] == "...")
                    info["maintainer"] = Environment.UserName;

                foreach (var control in groupBoxPackage.Controls)
                {
                    var textBox = control as TextBox;
                    if (textBox != null && textBox.Tag != null && textBox.Tag.ToString().StartsWith("PKG"))
                    {
                        var keys = textBox.Tag.ToString().Split(';');
                        var key = keys[0].Substring(3);
                        textBox.Text = info[key];
                    }
                }

                LoadPictureBox(pictureBoxPkg_256, LoadImageFromFile(Path.Combine(PackageRootPath, "PACKAGE_ICON_256.PNG")));
                LoadPictureBox(pictureBoxPkg_72, LoadImageFromFile(Path.Combine(PackageRootPath, "PACKAGE_ICON.PNG")));

                groupBoxPackage.Enabled = false;

                SavePackageSettings();
                CreateRecentsMenu();
            }
            else
            {
                MessageBox.Show(this, "The working folder doesn't contain a Package.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitialConfiguration()
        {
            using (new CWaitCursor())
            {
                Process unzip = new Process();
                unzip.StartInfo.FileName = Path.Combine(ResourcesRootPath, "7z.exe");
                unzip.StartInfo.Arguments = string.Format("x \"{0}\" -o\"{1}\"", Path.Combine(ResourcesRootPath, "Package.zip"), PackageRootPath);
                unzip.StartInfo.UseShellExecute = false;
                unzip.StartInfo.RedirectStandardOutput = true;
                unzip.StartInfo.CreateNoWindow = true;
                unzip.Start();
                Console.WriteLine(unzip.StandardOutput.ReadToEnd());
                unzip.WaitForExit();

                File.Copy(Path.Combine(ResourcesRootPath, "7z.exe"), Path.Combine(PackageRootPath, "7z.exe"));
                File.Copy(Path.Combine(ResourcesRootPath, "7z.dll"), Path.Combine(PackageRootPath, "7z.dll"));
                File.Copy(Path.Combine(ResourcesRootPath, "Pack.cmd"), Path.Combine(PackageRootPath, "Pack.cmd"));
            }

            // Pictures are all saved in the Mods.exe's folder / recovery, just in case one does Reset the package by mistake.

            //var recovery = Path.Combine(ResourcesRootPath, "recovery");
            //if (Directory.Exists(recovery))
            //{
            //    var images = Directory.GetFiles(recovery);
            //    if (images.Length > 0)
            //    {
            //        var answer = MessageBox.Show(this, "Icons from a previous package are available. Do you want to recover them?\nIf you answer 'No', they will be deleted.\nIf you 'Cancel', they will be kept in the recovery folder.", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            //        switch (answer)
            //        {
            //            case DialogResult.Yes:
            //                var target = Path.Combine(PackageRootPath, @"package\ui\images");
            //                foreach (var image in images)
            //                {
            //                    var dest = Path.Combine(target, Path.GetFileName(image));
            //                    if (File.Exists(dest))
            //                        File.Delete(dest);
            //                    File.Move(image, dest);
            //                }
            //                break;
            //            case DialogResult.No:
            //                Helper.DeleteDirectory(recovery);
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //}
        }
        #endregion --------------------------------------------------------------------------------------------------------------------------------

        #region Manage Package --------------------------------------------------------------------------------------------------------------------
        private void pictureBoxSettings_Click(object sender, EventArgs e)
        {

        }

        // Click on Button Reset Package
        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetPackage();
        }

        // Click on Button Create Package
        private void buttonPackage_Click(object sender, EventArgs e)
        {
            GeneratePackage();
        }

        private void SavePackageInfo()
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
                            var keyId = key.Substring(3);
                            info[keyId] = textBox.Text.Trim(); ;
                        }
                    }
                }

                // Delete existing INFO file
                var infoName = Path.Combine(PackageRootPath, "INFO");
                if (File.Exists(infoName))
                    File.Delete(infoName);

                // Write the new INFO file
                using (StreamWriter outputFile = new StreamWriter(infoName))
                {
                    foreach (var element in info)
                    {
                        outputFile.WriteLine("{0}=\"{1}\"", element.Key, element.Value);
                    }
                }

                // Save Package's icons
                var imageName = Path.Combine(PackageRootPath, "PACKAGE_ICON.PNG");
                SavePkgImage(pictureBoxPkg_72, imageName);
                imageName = Path.Combine(PackageRootPath, "PACKAGE_ICON_256.PNG");
                SavePkgImage(pictureBoxPkg_256, imageName);
            }
        }

        // Create the SPK
        private void CreatePackage(string packCmd)
        {
            // Delete existing package if any
            var dir = new DirectoryInfo(PackageRootPath);
            foreach (var file in dir.EnumerateFiles("*.spk"))
            {
                file.Delete();
            }

            // Execute the script to generate the SPK
            Process pack = new Process();
            pack.StartInfo.FileName = packCmd;
            pack.StartInfo.Arguments = "";
            pack.StartInfo.WorkingDirectory = PackageRootPath;
            pack.StartInfo.UseShellExecute = false;
            pack.StartInfo.RedirectStandardOutput = true;
            pack.StartInfo.CreateNoWindow = true;
            pack.Start();
            Console.WriteLine(pack.StandardOutput.ReadToEnd());
            pack.WaitForExit();

            // Rename the new Package with its target name
            var packName = Path.Combine(PackageRootPath, info["package"] + ".spk");
            File.Move(Path.Combine(PackageRootPath, "mods.spk"), packName);

        }

        // Save icons of the SPK
        private void SavePkgImage(PictureBox pictureBox, string path)
        {
            var image = pictureBox.Image;
            if (File.Exists(path))
                File.Delete(path);
            image.Save(path, ImageFormat.Png);

            // TODO: check that PKG images are saved when closing Mods
        }
        #endregion --------------------------------------------------------------------------------------------------------------------------------

        #region Manage List of items --------------------------------------------------------------------------------------------------------------
        private void listViewItems_DoubleClick(object sender, EventArgs e)
        {
            if (listViewItems.SelectedItems.Count == 1)
            {
                buttonEditItem_Click(sender, e);
            }
        }

        private void listViewItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (state != State.Edit)
            {
                if (listViewItems.SelectedItems.Count > 0)
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
            state = State.Add;
            DisplayDetails(new KeyValuePair<string, AppsData>(Guid.NewGuid().ToString(), new AppsData()));
        }

        // Edit the item currently selected
        private void buttonEditItem_Click(object sender, EventArgs e)
        {
            state = State.Edit;
            EnableItemDetails();
            textBoxTitle.Focus();
        }

        private void buttonCancelItem_Click(object sender, EventArgs e)
        {
            if (state == State.Add)
                current = new KeyValuePair<string, AppsData>(null, null);

            ResetValidateChildren(); // Reset Error Validation on all controls

            DisplayItem(current);
        }

        private void buttonSaveItem_Click(object sender, EventArgs e)
        {
            if (ValidateChildren()) // Trigger Error Validation on all controls
            {
                var candidate = GetItemDetails();
                if (state == State.Edit)
                    candidate.Value.guid = current.Value.guid;

                CleanupPreviousItem(current, candidate);
                RenamePreviousItem(current, candidate);
                SaveItemDetails(candidate);

                SaveItemsConfig();

                scriptValue = null;
                runnerValue = null;
                webAppIndex = null;
                webAppFolder = null;
            }
        }

        private void buttonDeleteItem_Click(object sender, EventArgs e)
        {
            if (current.Key != null)
            {
                var answer = MessageBox.Show(this, string.Format("Do you really want to delete the {0} '{1}' and related icons?", GetItemType(current.Value.itemType), current.Value.title), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (answer == DialogResult.Yes)
                {
                    list.items.Remove(current.Key);
                    BindData(list);
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
                    this.toolTip4Mods.SetToolTip(this.textBoxItem, "Type here the URL to be opened when clicking the icon on DSM.");
                    //textBoxItem.Focus();
                    break;
                case (int)UrlType.Script: // Script
                    this.toolTip4Mods.SetToolTip(this.textBoxItem, "Type the Script to be executed when clicking the icon on DSM. DoubleClick to edit.");
                    break;
                case (int)UrlType.WebApp: // WebApp
                    this.toolTip4Mods.SetToolTip(this.textBoxItem, "Here is the url of your own page to be opened when clicking the icon on DMS. DoubleClick to edit.");
                    break;
            }

            if (comboBoxItemType.Focused)
            {
                ChangeItemType(comboBoxItemType.SelectedIndex);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (state == State.Add || state == State.Edit)
                e.Cancel = true;

            if (SavePackage() == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void RenamePreviousItem(KeyValuePair<string, AppsData> current, KeyValuePair<string, AppsData> candidate)
        {
            var cleanedCurrent = Helper.CleanUpText(current.Value.title);
            var cleanedCandidate = Helper.CleanUpText(candidate.Value.title);
            var answer = DialogResult.Yes;

            if (cleanedCurrent != null)
            {
                //Rename a Script
                if (current.Value.itemType == (int)UrlType.Script && candidate.Value.itemType == (int)UrlType.Script && cleanedCurrent != cleanedCandidate)
                {
                    RenameScript(candidate.Value.title, cleanedCurrent, cleanedCandidate);
                }

                //Rename a WebApp 
                if (current.Value.itemType == (int)UrlType.WebApp && candidate.Value.itemType == (int)UrlType.WebApp && cleanedCurrent != cleanedCandidate)
                {
                    var existingWebAppFolder = Path.Combine(PackageRootPath, @"package\ui", cleanedCurrent);
                    if (Directory.Exists(existingWebAppFolder))
                    {
                        var targetWebAppFolder = Path.Combine(PackageRootPath, @"package\ui", cleanedCandidate);
                        if (Directory.Exists(targetWebAppFolder))
                        {
                            answer = MessageBox.Show(this, string.Format("Your Package '{0}' already contains a WebApp named {1}.\nDo you confirm that you want to replace it?\nIf you answer No, the existing one will be used. Otherwise, it will be replaced.", info["package"], candidate.Value.title), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (answer == DialogResult.Yes)
                            {
                                Helper.DeleteDirectory(targetWebAppFolder);
                            }
                        }
                        if (answer == DialogResult.Yes)
                        {
                            Directory.Move(existingWebAppFolder, targetWebAppFolder);
                        }
                    }
                }
            }
        }

        private void CleanupPreviousItem(KeyValuePair<string, AppsData> current, KeyValuePair<string, AppsData> candidate)
        {
            if (current.Key != null)
            {
                list.items.Remove(current.Key);
                BindData(list);
                DeleteItemPictures(current.Value.icon);
            }

            var cleanedCurrent = Helper.CleanUpText(current.Value.title);
            var cleanedCandidate = Helper.CleanUpText(candidate.Value.title);

            //Clean Up a WebApp previously defined if replaced by another type
            if (current.Value.itemType == (int)UrlType.WebApp && candidate.Value.itemType != (int)UrlType.WebApp)
            {
                DeleteWebApp(cleanedCurrent);
            }

            //Clean Up a Script previously defined if replaced by another type
            if (current.Value.itemType == (int)UrlType.Script && candidate.Value.itemType != (int)UrlType.Script)
            {
                DeleteScript(cleanedCurrent);
            }
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
                    CreateScript(candidate, scriptValue, runnerValue);
                    break;
            }

            list.items.Add(candidate.Key, candidate.Value);
            BindData(list);

            DisplayItem(candidate);
        }

        private void RenameScript(string packageName, string cleanedCurrent, string cleanedCandidate)
        {
            var answer = DialogResult.Yes;

            var current = Path.Combine(PackageRootPath, @"package\ui", cleanedCurrent);
            if (Directory.Exists(current))
            {
                var target = Path.Combine(PackageRootPath, @"package\ui", cleanedCandidate);
                if (Directory.Exists(target))
                {
                    answer = MessageBox.Show(this, string.Format("Your Package '{0}' already contains a script named {1}.\nDo you confirm that you want to replace it?\nIf you answer No, the existing one will be used. Otherwise, it will be replaced.", info["package"], packageName), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (answer == DialogResult.Yes)
                    {
                        Helper.DeleteDirectory(target);
                    }
                }
                if (answer == DialogResult.Yes)
                {
                    Directory.Move(current, target);
                }
            }
        }

        private void DeleteScript(string cleanedCurrent)
        {
            var target = Path.Combine(PackageRootPath, @"package\ui", cleanedCurrent);
            if (File.Exists(target))
                Helper.DeleteDirectory(target);
        }

        private void DeleteWebApp(string cleanedCurrent)
        {
            var targetWebAppFolder = Path.Combine(PackageRootPath, @"package\ui", cleanedCurrent);
            if (Directory.Exists(targetWebAppFolder))
                Helper.DeleteDirectory(targetWebAppFolder);
        }

        private void ChangeItemType(int selectedIndex)
        {
            var answer = DialogResult.Yes;

            if (current.Value.itemType != (int)UrlType.Url && (current.Value.itemType != selectedIndex))
            {
                var from = GetItemType(current.Value.itemType);
                var to = GetItemType(selectedIndex);
                answer = MessageBox.Show(this, string.Format("Your Package '{0}' currently contains a {1}.\nDo you confirm that you want to replace it by a new {2}?\nIf you answer Yes, your existing {1} will be deleted when you save your changes.", info["package"], from, to), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }

            if (answer == DialogResult.No)
            {
                textBoxTitle.Focus();
                comboBoxItemType.SelectedIndex = current.Value.itemType;
            }
            else
            {
                current.Value.itemType = selectedIndex;
                switch (selectedIndex)
                {
                    case (int)UrlType.Url:
                        textBoxItem.Enabled = true;
                        textBoxItem.ReadOnly = false;
                        textBoxItem.Text = "";
                        textBoxItem.Focus();
                        break;

                    case (int)UrlType.Script:
                        var cleanedScriptName = Helper.CleanUpText(textBoxTitle.Text);
                        var targetScriptPath = Path.Combine(PackageRootPath, @"package\ui", cleanedScriptName, "mods.sh");
                        var targetRunnerPath = Path.Combine(PackageRootPath, @"package\ui", cleanedScriptName, "mods.php");

                        textBoxItem.Enabled = true;
                        textBoxItem.ReadOnly = true;

                        EditScript(targetScriptPath, targetRunnerPath);
                        if (!string.IsNullOrEmpty(scriptValue))
                        {
                            var url = "";
                            GetDetailsScript(cleanedScriptName, ref url);
                            textBoxItem.Text = url;
                        }
                        else
                        {
                            selectedIndex = (int)UrlType.Url;
                        }
                        break;
                    case (int)UrlType.WebApp:
                        textBoxItem.Enabled = true;
                        textBoxItem.ReadOnly = true;
                        textBoxItem.Text = "";
                        var cleanedWebApp = Helper.CleanUpText(textBoxTitle.Text);
                        var targetWebAppFolder = Path.Combine(PackageRootPath, @"package\ui", cleanedWebApp);
                        if (Directory.Exists(targetWebAppFolder) && Directory.EnumerateFiles(targetWebAppFolder).Count() > 0)
                        {
                            answer = MessageBox.Show(this, string.Format("Your Package '{0}' already contains a WebApp named {1}.\nDo you want to replace it?", info["package"], textBoxTitle.Text), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        }
                        if (answer == DialogResult.Yes)
                        {
                            if (EditWebApp() != DialogResult.OK)
                            {
                                selectedIndex = (int)UrlType.Url;
                            }
                        }
                        break;
                }
                if (current.Value.itemType != selectedIndex)
                {
                    current.Value.itemType = selectedIndex;
                    ChangeItemType(selectedIndex);
                    comboBoxItemType.SelectedIndex = selectedIndex;
                }
            }
        }

        private DialogResult EditScript(string scriptPath, string runnerPath)
        {
            string outputScript = string.Empty;
            string outputRunner = string.Empty;
            var inputScript = string.Empty;
            var inputRunner = string.Empty;
            if (File.Exists(scriptPath))
                inputScript = File.ReadAllText(scriptPath);
            if (File.Exists(runnerPath))
                inputRunner = File.ReadAllText(runnerPath);
            else
                inputRunner = File.ReadAllText(defaultRunnerPath);

            DialogResult result = Helper.ScriptEditor(inputScript, inputRunner, out outputScript, out outputRunner);
            if (result == DialogResult.OK)
            {
                scriptValue = outputScript;
                runnerValue = outputRunner;
            }
            return result;
        }

        private DialogResult EditWebApp()
        {
            webpageBrowserDialog4Mods.Description = "Pick the folder containing the sources of your WebApp.";
            DialogResult result = webpageBrowserDialog4Mods.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                openFileDialog4Mods.Title = "Pick the index page.";
                openFileDialog4Mods.InitialDirectory = webpageBrowserDialog4Mods.SelectedPath;
                openFileDialog4Mods.Filter = "html (*.html)|*.html|php (*.php)|*.php";
                openFileDialog4Mods.FilterIndex = 2;
                openFileDialog4Mods.FileName = null;
                var files = Directory.GetFiles(webpageBrowserDialog4Mods.SelectedPath).Select(path => Path.GetFileName(path)).ToArray();
                openFileDialog4Mods.FileName = Helper.FindFileIndex(files, "index.php") ?? Helper.FindFileIndex(files, "index.html") ?? Helper.FindFileIndex(files, "default.php") ?? Helper.FindFileIndex(files, "default.html") ?? null;

                result = openFileDialog4Mods.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    webAppIndex = openFileDialog4Mods.FileName;
                    webAppFolder = webpageBrowserDialog4Mods.SelectedPath;

                    if (!webAppIndex.StartsWith(webAppFolder))
                    {
                        MessageBox.Show(this, "This file is not in the directory selected previously. Please select a file under that folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        webAppFolder = null;
                        webAppIndex = null;
                        comboBoxItemType.SelectedIndex = (int)UrlType.Url;
                    }
                    else
                    {
                        textBoxItem.Text = webAppIndex.Remove(0, webAppFolder.Length + 1);
                    }
                }
            }

            return result;
        }

        private void CreateWebApp(KeyValuePair<string, AppsData> current)
        {
            if (webAppIndex != null && webAppFolder != null)
            {
                var cleanedCurrent = Helper.CleanUpText(current.Value.title);
                var targetWebAppFolder = Path.Combine(PackageRootPath, @"package\ui", cleanedCurrent);

                if (Directory.Exists(targetWebAppFolder) && Directory.EnumerateFiles(targetWebAppFolder).Count() > 0)
                {
                    Helper.DeleteDirectory(targetWebAppFolder);
                }

                Helper.CopyDirectory(webAppFolder, targetWebAppFolder);
            }
        }

        private void CreateScript(KeyValuePair<string, AppsData> current, string script, string runner)
        {
            if (script != null)
            {
                var cleanedCurrent = Helper.CleanUpText(current.Value.title);
                var target = Path.Combine(PackageRootPath, @"package\ui", cleanedCurrent);
                var targetRunner = Path.Combine(target, "mods.php");
                var targetScript = Path.Combine(target, "mods.sh");

                if (Directory.Exists(target))
                {
                    Helper.DeleteDirectory(target);
                }

                // Remove \r not supported in shell scripts
                script = script.Replace("\r\n", "\n");
                //runner = script.Replace("\r\n", "\n");

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

        private void SaveItemsConfig()
        {
            if (!string.IsNullOrEmpty(PackageRootPath))
            {
                dirty = true;

                var json = JsonConvert.SerializeObject(list, Formatting.Indented, new KeyValuePairConverter());
                Properties.Settings.Default.Packages = json;
                Properties.Settings.Default.Save();

                var config = Path.Combine(PackageRootPath, CONFIGFILE);
                if (Directory.Exists(Path.GetDirectoryName(config)))
                {
                    File.WriteAllText(config, json);
                }
                else
                {
                    MessageBox.Show(this, "For some reason, required resource files are missing. You will have to reconfigure your destination path", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            scriptValue = null;
            runnerValue = null;
            current = item;
            var show = item.Key != null;
            var descText = show ? item.Value.desc : "";
            var titleText = show ? item.Value.title : "";
            var urlText = show ? item.Value.url : "";
            if (urlText != null && urlText.StartsWith("/") && item.Value.port != WebPort)
            {
                if (item.Value.protocol == "https")
                {
                    urlText = string.Format("https://:{0}{1}", item.Value.port, item.Value.url);
                }
                else
                {
                    urlText = string.Format(":{0}{1}", item.Value.port, item.Value.url);
                }
            }
            var users = show ? item.Value.allUsers : false;

            if (show)
            {
                foreach (var size in pictureBoxes.Keys)
                {
                    var picture = GetIconFullPath(item.Value.icon, size);

                    if (File.Exists(picture))
                    {
                        LoadPictureBox(LoadImageFromFile(picture), size);
                    }
                    else
                    {
                        LoadPictureBox(null, size);
                    }
                }
            }
            else
            {
                foreach (var pictureBox in pictureBoxes.Values)
                {
                    LoadPictureBox(pictureBox, null);
                }
                comboBoxItemType.SelectedIndex = (int)UrlType.Url;
            }

            textBoxDesc.Text = descText;
            textBoxTitle.Text = titleText;
            textBoxItem.Text = urlText;
            checkBoxAllUsers.Checked = users;
            checkBoxMultiInstance.Checked = show ? item.Value.allowMultiInstance : false;
            comboBoxItemType.SelectedIndex = show ? item.Value.itemType : (int)UrlType.Url;

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

                EnableItemFieldDetails(enabling, enabling, enabling, enabling, enabling, !enabling, enabling);
                EnableItemButtonDetails(enabling, enabling, enabling, enabling, enabling, packaging);

                EnableItemMenuDetails(false, false, false, false, false, true, true, true);
            }
            else
            {
                // Enable the detail and package zone depending on the current state (view, new, edit or none)
                enabling = (state != State.View && state != State.None);
                packaging = listViewItems.Items.Count > 0 && !enabling;

                EnableItemFieldDetails(enabling, enabling, enabling, enabling, enabling, !enabling, enabling);
                switch (state)
                {
                    case State.View:
                        EnableItemButtonDetails(true, true, false, false, true, packaging);
                        EnableItemMenuDetails(!enabling, true, !enabling, !enabling, !enabling, true, true, true);
                        break;
                    case State.None:
                        EnableItemButtonDetails(true, false, false, false, false, packaging);
                        EnableItemMenuDetails(!enabling, true, !enabling, !enabling, !enabling, true, true, true);
                        break;
                    case State.Add:
                    case State.Edit:
                        EnableItemButtonDetails(false, false, true, true, false, packaging);
                        EnableItemMenuDetails(!enabling, true, !enabling, !enabling, !enabling, false, false, false);
                        break;
                }
                textBoxItem.ReadOnly = !(comboBoxItemType.SelectedIndex == (int)UrlType.Url);

            }
        }

        private void EnableItemMenuDetails(bool packageArea, bool itemsArea, bool menuPackage, bool menuReset, bool menuSave, bool menuNew, bool menuOpen, bool menuRecent)
        {
            groupBoxPackage.Enabled = packageArea;
            groupBoxItem.Enabled = itemsArea;
            generateToolStripMenuItem.Enabled = menuPackage;
            resetToolStripMenuItem.Enabled = menuReset;
            saveToolStripMenuItem.Enabled = menuSave;
            newToolStripMenuItem.Enabled = menuNew;
            openToolStripMenuItem.Enabled = menuOpen;
            openRecentToolStripMenuItem.Enabled = menuRecent;
        }

        private void EnableItemFieldDetails(bool bTextBoxDesc, bool bTextBoxTitle, bool btestBoxItem, bool bCheckBoxAllUsers, bool bCheckBoxMultiInstance, bool blistViewItems, bool bcomboBoxItemType)
        {
            textBoxDesc.Enabled = bTextBoxDesc;
            textBoxTitle.Enabled = bTextBoxTitle;
            textBoxItem.Enabled = btestBoxItem;
            checkBoxAllUsers.Enabled = bCheckBoxAllUsers;
            checkBoxMultiInstance.Enabled = bCheckBoxMultiInstance;
            listViewItems.Enabled = blistViewItems;
            comboBoxItemType.Enabled = bcomboBoxItemType;
        }

        private void EnableItemButtonDetails(bool bButtonAdd, bool bButtonEdit, bool bButtonSave, bool bButtonCancel, bool bButtonDelete, bool bButtonPackage)
        {
            buttonAdd.Enabled = bButtonAdd;
            buttonEdit.Enabled = bButtonEdit;
            buttonSave.Enabled = bButtonSave;
            buttonCancel.Enabled = bButtonCancel;
            buttonDelete.Enabled = bButtonDelete;
            buttonPackage.Enabled = bButtonPackage;
        }

        // Parse the data of the details zone
        private KeyValuePair<string, AppsData> GetItemDetails()
        {
            bool multiInstance = checkBoxMultiInstance.Checked;
            var allUsers = checkBoxAllUsers.Checked;
            var title = textBoxTitle.Text.Trim();
            var desc = textBoxDesc.Text.Trim();

            string protocol = WebProtocol;
            int port = WebPort;

            var url = textBoxItem.Text.Trim();
            var key = string.Format("SYNO.SDS._ThirdParty.App.{0}", title.Replace(" ", ""));

            var urlType = comboBoxItemType.SelectedIndex;
            switch (urlType)
            {
                case (int)UrlType.Url:
                    GetDetailsUrl(ref protocol, ref port, ref url);
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
                type = urlType == (int)UrlType.Url ? "url" : "legacy",
                itemType = urlType,
                appWindow = key,
                allowMultiInstance = multiInstance
            };

            title = Helper.CleanUpText(title);
            appsData.icon = string.Format("images/{0}_{1}.png", title, "{0}");
            return new KeyValuePair<string, AppsData>(key, appsData);
        }

        private void GetDetailsScript(string title, ref string url)
        {
            var cleanedScript = Helper.CleanUpText(title);
            var actualUrl = string.Format("/webman/3rdparty/{0}/{1}/mods.php", info["package"], cleanedScript);
            if (url != actualUrl)
            {
                url = actualUrl;
            }
        }

        private void GetDetailsWebApp(string title, ref string url)
        {
            var cleanedWebApp = Helper.CleanUpText(title);
            var actualUrl = string.Format("/webman/3rdparty/{0}/{1}/{2}", info["package"], cleanedWebApp, url);
            if (webAppIndex != null && webAppFolder != null)
            {
                url = actualUrl;
            }
        }

        private void GetDetailsUrl(ref string protocol, ref int port, ref string url)
        {
            if (url.StartsWith(":"))
            {
                var portMatch = getPort.Match(url);
                if (portMatch.Success)
                {
                    var value = portMatch.Groups[1].Value;
                    url = url.Substring(value.Length + 1);
                    port = int.Parse(value);
                }
            }

            if (url.ToLower().StartsWith("https://:") || url.ToLower().StartsWith("http://:"))
            {
                url = url.Replace("://:", "://0.0.0.0:");
            }

            Uri uri;
            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri))
            {
                if (uri.IsAbsoluteUri)
                {
                    protocol = uri.Scheme;
                    port = uri.Port;
                    if (uri.Host == "0.0.0.0")
                        url = uri.AbsolutePath;
                }
                else
                {
                    protocol = WebProtocol;
                    url = uri.OriginalString;
                    if (!url.StartsWith("/"))
                        url = string.Format("/{0}", url);
                }
            }
            else
            {
                port = WebPort;
                if (!url.StartsWith("/"))
                    url = string.Format("/{0}", url);
            }
        }
        #endregion --------------------------------------------------------------------------------------------------------------------------------

        #region Manage Icons ----------------------------------------------------------------------------------------------------------------------
        private void pictureBoxPkg_DragEnter(object sender, DragEventArgs e)
        {
            string filename;
            validData = Helper.GetDragDropFilename(out filename, e);
            if (validData)
            {
                imageDragDropPath = filename;
                e.Effect = DragDropEffects.Copy;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void pictureBoxPkg_DragDrop(object sender, DragEventArgs e)
        {
            if (validData)
            {
                var pictureBox = sender as PictureBox;
                ChangePicturePkg(imageDragDropPath, pictureBox.Tag.ToString().Split(';')[1]);
            }
        }

        private void pictureBoxItem_DragEnter(object sender, DragEventArgs e)
        {
            if (state == State.Add || state == State.Edit)
            {
                string filename;
                validData = Helper.GetDragDropFilename(out filename, e);
                if (validData)
                {
                    imageDragDropPath = filename;
                    e.Effect = DragDropEffects.Copy;
                }
                else
                    e.Effect = DragDropEffects.None;
            }
            else
            {
                validData = false;
                e.Effect = DragDropEffects.None;
            }
        }

        private void pictureBoxItem_DragDrop(object sender, DragEventArgs e)
        {
            if (validData)
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

        private void ChangePicturePkg(string picture, string size)
        {
            Image image = LoadImage(picture);
            if (image != null)
            {
                if (size == "256")
                    LoadPictureBox(pictureBoxPkg_256, image);
                if (size == "72")
                    LoadPictureBox(pictureBoxPkg_72, image);
            }
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
            if (MessageBox.Show(this, "Do you want to make this image transparent?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                transparency = 0;
            }
            else if (transparency == 0)
            {
                MessageBox.Show(this, "You need to pick a transparency value");
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

            if (!File.Exists(picture))
            {
                MessageBox.Show(this, string.Format("Picture '{0}' is missing and can therefore not be loaded ?!", picture), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Image image = LoadImageFromFile(picture);

                using (Graphics g = Graphics.FromImage(copy))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, 0, 0, copy.Width, copy.Height);

                    var backColor = copy.GetPixel(1, 1);
                    if (transparency > 0)
                    {
                        var Rl = Helper.roundByte(backColor.R, -transparency);
                        var Ru = Helper.roundByte(backColor.R, transparency);
                        var Gl = Helper.roundByte(backColor.G, -transparency);
                        var Gu = Helper.roundByte(backColor.G, transparency);
                        var Bl = Helper.roundByte(backColor.B, -transparency);
                        var Bu = Helper.roundByte(backColor.B, transparency);

                        var steps = (Ru - Rl + 1) * (Gu - Gl + 1) * (Bu - Bl + 1);
                        var total = steps;

                        for (int R = Rl; R <= Ru; R++)
                            for (int G = Gl; G <= Gu; G++)
                                for (int B = Bl; B <= Bu; B++)
                                {
                                    copy.MakeTransparent(Color.FromArgb(R, G, B));
                                    steps--;
                                    if (steps % 100 == 0)
                                    {
                                        labelToolTip.Text = string.Format("MAKING IMAGE TRANSPARENT [{0}%]", Math.Round((double)(total - steps) * 100 / total));
                                        labelToolTip.Invalidate();
                                        labelToolTip.Update();
                                        labelToolTip.Refresh();
                                        Application.DoEvents();
                                    }
                                }
                        g.DrawImage(image, 0, 0, copy.Width, copy.Height);
                    }
                }

                labelToolTip.Text = "";
                image.Dispose();
            }
            return copy;
        }

        private Image LoadImageFromFile(string picture)
        {
            Image image;
            using (FileStream stream = new FileStream(picture, FileMode.Open, FileAccess.Read))
            {
                image = Image.FromStream(stream);
            }
            return image;
        }

        // Get the path of Item's icon (to be) saved inside the package
        private string GetIconFullPath(string item, string size)
        {
            var icons = string.Format(item, size).Split('/');
            var picture = Path.Combine(PackageRootPath, @"package\ui", icons[0], icons[1]);
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

                var copy = new Bitmap(size, size);
                using (Graphics g = Graphics.FromImage(copy))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, 0, 0, size, size);
                }

                pictureBox.Image = copy;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
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
                    if (File.Exists(path))
                        File.Delete(path);
                    image.Save(path, ImageFormat.Png);

                    path = Path.Combine(ResourcesRootPath, @"recovery", Path.GetFileName(path));
                    if (!Directory.Exists(Path.GetDirectoryName(path)))
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                    if (File.Exists(path))
                        File.Delete(path);
                    image.Save(path, ImageFormat.Png);
                }
            }
        }
        #endregion --------------------------------------------------------------------------------------------------------------------------------

        #region Validations -----------------------------------------------------------------------------------------------------------------------
        private void textBoxPackage_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxPackage, ref e))
            {
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

        private void textBoxPackage_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxPackage, "");
            if (textBoxPackage.Enabled)
            {
                var newName = textBoxPackage.Text;
                var oldName = info.Count > 0 ? info["package"] : newName;
                if (newName != oldName)
                {
                    oldName = string.Format("/webman/3rdparty/{0}", oldName);
                    newName = string.Format("/webman/3rdparty/{0}", newName);

                    foreach (var item in list.items)
                    {
                        if (item.Value.url.StartsWith(oldName))
                        {
                            item.Value.url = item.Value.url.Replace(oldName, newName);
                        }
                    }
                    BindData(list);
                    //DisplayItem();
                    info["package"] = textBoxPackage.Text;
                }
            }
        }

        private void textBoxDisplay_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxDisplay, ref e))
            {
                CheckDoubleQuotes(textBoxDisplay, ref e);
            }
        }

        private void textBoxDisplay_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxDisplay, "");
        }

        private void textBoxMaintainer_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxMaintainer, ref e))
            {
                CheckDoubleQuotes(textBoxMaintainer, ref e);
            }
        }

        private void textBoxMaintainer_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxMaintainer, "");
        }

        private void textBoxPublisher_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxPublisher, "");
        }

        private void textBoxPublisher_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxPublisher, ref e))
            {
                CheckDoubleQuotes(textBoxPublisher, ref e);
            }
        }

        private void textBoxDescription_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxPublisher, ref e))
            {
                CheckDoubleQuotes(textBoxDescription, ref e);
            }
        }

        private void textBoxDescription_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxDescription, "");
        }

        private void textBoxMaintainerUrl_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxMaintainerUrl, ref e))
            {
                CheckDoubleQuotes(textBoxMaintainerUrl, ref e);
            }
        }

        private void textBoxMaintainerUrl_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxMaintainerUrl, "");
        }

        private void textBoxVersion_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxVersion, ref e))
            {
                if (!getVersion.IsMatch(textBoxVersion.Text))
                {
                    e.Cancel = true;
                    textBoxVersion.Select(0, textBoxVersion.Text.Length);
                    errorProvider.SetError(textBoxVersion, "The format of a version must be like 0.0.0");
                }
            }
        }

        private void textBoxVersion_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxVersion, "");
        }

        private void textBoxDsmAppName_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxDsmAppName, ref e))
            {
                var name = textBoxPackage.Text.Replace(".", "");
                var cleaned = Helper.CleanUpText(textBoxPackage.Text);
                if (name != cleaned)
                {
                    e.Cancel = true;
                    textBoxPackage.Select(0, textBoxPackage.Text.Length);
                    errorProvider.SetError(textBoxPackage, "The name of the package may not contain blanks or special characters.");
                }
            }
        }

        private void textBoxDsmAppName_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxDsmAppName, "");
        }

        private void textBoxTitle_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxTitle, ref e))
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
                        if (url.title.Equals(textBoxTitle.Text, StringComparison.InvariantCultureIgnoreCase) && current.Value.guid != url.guid)
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

                    textBoxItem.Text = textBoxItem.Text.Replace(oldName, newName);
                }
            }
        }

        private void testBoxItem_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxItem, ref e))
            {
            }
        }

        private void testBoxItem_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxItem, "");
        }

        private bool CheckEmpty(TextBox textBox, ref CancelEventArgs e)
        {
            if (textBox.Enabled && string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Enter_A_Value";
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
        }

        // Reset errors possibly displayed on any Control
        private void ResetValidateChildren()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl != null)
                {
                    errorProvider.SetError(ctrl, "");
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
                    var targetScript = Path.Combine(PackageRootPath, @"package\ui", cleanedScriptName, "mods.sh");
                    var targetRunner = Path.Combine(PackageRootPath, @"package\ui", cleanedScriptName, "mods.php");
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
            if (SavePackage() != DialogResult.Cancel)
            {
                NewPackage(null);
            }
        }

        private DialogResult SavePackage(bool force = false)
        {
            DialogResult response = DialogResult.Yes;
            if (info != null)
            {
                if (!dirty)
                    dirty = CheckChanges();
                if (dirty)
                {
                    if (!force)
                        response = MessageBox.Show(this, "Do you want to save changes done in the current Package", "Warning", MessageBoxButtons.YesNoCancel);

                    if (response == DialogResult.Yes)
                    {
                        SaveItemsConfig();
                        SavePackageInfo();

                        SavePackageSettings();
                        CreateRecentsMenu();
                        dirty = false;
                    }
                }
            }
            else
            {
                response = DialogResult.Cancel;
            }

            return response;
        }

        private void SavePackageSettings()
        {
            Properties.Settings.Default.PackageRoot = PackageRootPath;
            if (Properties.Settings.Default.Recents != null)
            {
                for (var item = 0; item < Properties.Settings.Default.Recents.Count; item++)
                {
                    if (Properties.Settings.Default.Recents[item] == PackageRootPath)
                    {
                        Properties.Settings.Default.Recents.RemoveAt(item);
                        Properties.Settings.Default.RecentsName.RemoveAt(item);
                    }
                }
                if (Properties.Settings.Default.Recents.Count >= 5)
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
            Properties.Settings.Default.RecentsName.Add(info["package"]);
            Properties.Settings.Default.Save();
        }

        private bool CheckChanges()
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

                        dirty = textBox.Text != info[key];

                        if (dirty)
                            break;
                    }
                }

            return dirty;
        }

        private void ResetPackage()
        {
            if (info != null)
            {
                var answer = MessageBox.Show(this, "Do you really want to reset the complete Package to its defaults?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (answer == DialogResult.Yes)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(PackageRootPath))
                            MessageBox.Show(this, "The path of the Package is not defined. Create a new package instead.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else
                        {
                            if (Directory.Exists(PackageRootPath))
                                Helper.DeleteDirectory(PackageRootPath);

                            NewPackage(PackageRootPath);
                        }
                    }
                    catch
                    {
                        MessageBox.Show(this, "The destination folder cannot be cleaned. The package file is possibly in use?!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "There is no Package loaded.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void NewPackage(string path = null)
        {
            DialogResult result = DialogResult.Cancel;

            if (path == null)
            {
                result = GetPackagePath(ref path);
            }
            else
            {
                result = IsPackageFolderEmpty(path);
            }

            if (result == DialogResult.No)
            {
                var answer = MessageBox.Show(this, "This folder already contains a Package. Do you want to load it?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (answer == DialogResult.Yes)
                    OpenPackage(path);
            }
            else if (result != DialogResult.Cancel)
            {
                PackageRootPath = path;

                ResetValidateChildren(); // Reset Error Validation on all controls

                InitialConfiguration();

                InitData();
                LoadPackageInfo();
                BindData(list);
                DisplayItem();
            }
        }

        private void GeneratePackage()
        {
            // This is required to insure that changes (mainly icons) are correctly applied when installing the package in DSM
            textBoxVersion.Text = Helper.IncrementVersion(textBoxVersion.Text);

            var packCmd = Path.Combine(PackageRootPath, "Pack.cmd");
            if (File.Exists(packCmd))
            {
                SavePackageInfo();

                using (new CWaitCursor())
                {
                    // Create the SPK
                    CreatePackage(packCmd);
                }

                var answer = MessageBox.Show(this, string.Format("Your Package '{0}' is ready in {1}.\nDo you want to open that folder now?", info["package"], PackageRootPath), "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (answer == DialogResult.Yes)
                {
                    Process.Start(PackageRootPath);
                }
            }
            else
            {
                MessageBox.Show(this, "For some reason, required resource files are missing. You will have to reconfigure your destination path", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPackage();
        }

        private void OpenPackage(string path = null)
        {
            DialogResult result = DialogResult.Cancel;
            if (path == null)
            {
                folderBrowserDialog4Mods.Description = "Pick a folder to store the new Package or a folder containing an existing Package.";
                if (!string.IsNullOrEmpty(PackageRootPath))
                    folderBrowserDialog4Mods.SelectedPath = PackageRootPath;

                result = GetPackagePath(ref path);
            }
            else
            {
                result = IsPackageFolderEmpty(path);
            }

            if (result == DialogResult.Yes)
            {
                result = MessageBox.Show(this, "The Folder does not contain any package. Do you want to create a new one ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result != DialogResult.Yes)
                {
                    result = DialogResult.Cancel;
                }
            }
            if (result != DialogResult.Cancel)
            {
                PackageRootPath = path;

                ResetValidateChildren(); // Reset Error Validation on all controls

                if (result == DialogResult.Yes)
                {
                    InitialConfiguration();
                }

                InitData();
                LoadPackageInfo();
                BindData(list);
                DisplayItem();
            }
            else
            {
                MessageBox.Show(this, "The selected Folder does not contain a valid Package.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Return Yes to create a new package
        // Return No to load an existing package
        // Return Cancel to neither create nor load a package
        private DialogResult GetPackagePath(ref string path)
        {
            DialogResult createNewPackage = DialogResult.Cancel;

            DialogResult result = folderBrowserDialog4Mods.ShowDialog(this);
            path = folderBrowserDialog4Mods.SelectedPath;
            if (Directory.Exists(path))
            {
                createNewPackage = IsPackageFolderEmpty(path);
            }
            else
            {
                if (path.EndsWith("New folder"))
                    MessageBox.Show(this, "The renaming of the 'New folder' was not yet completed when you clicked 'Ok'. Please, reselect your folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(this, string.Format("Something went wrong when picking the folder {0}", path), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return createNewPackage;
        }

        private DialogResult IsPackageFolderEmpty(string path)
        {
            DialogResult createNewPackage = DialogResult.Yes;
            if (Directory.Exists(path))
            {
                var content = Directory.GetDirectories(path).ToList();
                content.AddRange(Directory.GetFiles(path));
                if (content.Count > 0)
                {
                    if (content.Count >= 8)
                    {
                        content.Remove(Path.Combine(path, "7z.dll"));
                        content.Remove(Path.Combine(path, "7z.exe"));
                        content.Remove(Path.Combine(path, "INFO"));
                        content.Remove(Path.Combine(path, "Pack.cmd"));
                        content.Remove(Path.Combine(path, "PACKAGE_ICON.PNG"));
                        content.Remove(Path.Combine(path, "PACKAGE_ICON_256.PNG"));
                        content.Remove(Path.Combine(path, "package"));
                        content.Remove(Path.Combine(path, "scripts"));
                    }
                    var remaining = content.ToList();
                    foreach (var spk in remaining)
                    {
                        if (spk.EndsWith(".spk"))
                        {
                            content.Remove(spk);
                        }
                    }
                    if (content.Count > 0)
                    {
                        // Folder contains something else than a package
                        createNewPackage = DialogResult.Cancel;
                    }
                    else
                    {
                        // Folder contains an existing Package
                        createNewPackage = DialogResult.No;
                    }
                }
                else
                {
                    // Folder is empty
                    createNewPackage = DialogResult.Yes;
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
            GeneratePackage();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SavePackage() != DialogResult.Cancel)
                MessageBox.Show(this, "Package saved.", "Notification");
            else
                MessageBox.Show(this, "Nothing to save.", "Notification");
        }

        private void MenuItemOpenRecent_ClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            String path = clickedItem.Tag.ToString();
            SavePackage();
            OpenPackage(path);
        }

        private void scriptRunnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var runner = File.ReadAllText(defaultRunnerPath);
            string outputScript = string.Empty;
            string outputRunner = string.Empty;
            DialogResult result = Helper.ScriptEditor(null, runner, out outputScript, out outputRunner);
            if (result == DialogResult.OK)
            {
                File.WriteAllText(defaultRunnerPath, outputRunner);
            }
        }
    }
}

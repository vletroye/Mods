using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace BeatificaBytes.Synology.Mods
{
    public partial class Parameters : Form
    {
        private OpenFolderDialog BrowserDialog4Mods = new OpenFolderDialog();
        public Parameters()
        {
            InitializeComponent();

            foreach (var control in this.Controls)
            {
                var item = control as Control;
                if (item != null)
                {
                    item.MouseEnter += new System.EventHandler(this.OnMouseEnter);
                    item.Enter += new System.EventHandler(this.OnMouseEnter);
                    item.MouseLeave += new System.EventHandler(this.OnMouseLeave);
                }
            }

            ShowParameters();
        }

        private void ShowParameters()
        {
            string command = null;
            string path;
            var key = Registry.ClassesRoot.OpenSubKey(".spk");
            if (key != null && key.GetValue("") != null)
            {
                path = key.GetValue("").ToString();
            }
            else
            {
                path = ".spk";
            }

            var regcmd = Registry.ClassesRoot.OpenSubKey(string.Format(@"{0}\shell\edit", path));
            if (regcmd != null && regcmd.GetValue("") != null)
            {
                command = regcmd.GetValue("").ToString();
            }
            Properties.Settings.Default.OpenWith = command == null ? false : command == "Edit with Mods Packager";

            checkBoxDefaultPackageRepo.Checked = Properties.Settings.Default.DefaultPackageRepo;
            checkBoxOpenWith.Checked = Properties.Settings.Default.OpenWith;
            checkBoxDefaultPackageRoot.Checked = Properties.Settings.Default.DefaultPackageRoot;
            checkBoxPromptExplorer.Checked = Properties.Settings.Default.PromptExplorer;
            checkBoxCopyPackagePath.Checked = Properties.Settings.Default.CopyPackagePath;
            
            buttonDefaultPackageRepo.Visible = checkBoxDefaultPackageRepo.Checked;
            labelDefaultPublishFolder.Visible = checkBoxDefaultPackageRepo.Checked;
            labelDefaultPublishFolder.Text = Properties.Settings.Default.PackageRepo;

            buttonDefaultPackageRoot.Visible = checkBoxDefaultPackageRoot.Checked;
            labelDefaultPackageRoot.Visible = checkBoxDefaultPackageRoot.Checked;
            labelDefaultPackageRoot.Text = Properties.Settings.Default.PackageRoot;
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            var zone = sender as Control;
            if (zone != null)
            {
                var text = toolTipProperties.GetToolTip(zone);
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

        private void checkBoxDefaultPackageRepo_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultPackageRepo = checkBoxDefaultPackageRepo.Checked;
            if (checkBoxDefaultPackageRepo.Checked)
            {
                if (string.IsNullOrEmpty(Properties.Settings.Default.PackageRepo))
                    PickPackageRepo();
            }
            ShowParameters();
        }

        private void PickPackageRepo()
        {
            var path = Properties.Settings.Default.PackageRepo;
            BrowserDialog4Mods.Title = "Pick a folder to publish the Package.";
            if (!string.IsNullOrEmpty(path))
                BrowserDialog4Mods.InitialDirectory = path;
            else
                BrowserDialog4Mods.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (BrowserDialog4Mods.ShowDialog(this.Handle))
            {
                Properties.Settings.Default.PackageRepo = BrowserDialog4Mods.FileName;
            }
            ShowParameters();
        }

        private void PickPackageRoot()
        {
            var path = Properties.Settings.Default.PackageRoot;
            BrowserDialog4Mods.Title = "Pick a folder to store all your Packages under creation.";
            if (!string.IsNullOrEmpty(path))
                BrowserDialog4Mods.InitialDirectory = path;
            else
                BrowserDialog4Mods.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (BrowserDialog4Mods.ShowDialog(this.Handle))
            {
                Properties.Settings.Default.PackageRoot = BrowserDialog4Mods.FileName;
            }
            ShowParameters();
        }

        private void buttonDefaultPackageRepo_Click(object sender, EventArgs e)
        {
            PickPackageRepo();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.DefaultPackageRepo)
                Properties.Settings.Default.PackageRepo = "";

            if (!Properties.Settings.Default.DefaultPackageRoot)
                Properties.Settings.Default.PackageRoot = "";

            Properties.Settings.Default.Save();
            this.Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void labelDefaultPublishFolder_Click(object sender, EventArgs e)
        {
            var path = Properties.Settings.Default.PackageRepo;
            if (Directory.Exists(path))
                Process.Start(path);
            else
                MessageBoxEx.Show(this, "This folder does not exist anymore or cannot be accessed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void checkBoxOpenWith_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOpenWith.Checked && !Properties.Settings.Default.OpenWith)
                AddOpenWith();
            else if (!checkBoxOpenWith.Checked && Properties.Settings.Default.OpenWith)
                RemoveOpenWith();

            ShowParameters();
        }

        private void RemoveOpenWith()
        {
            try
            {
                var key = Registry.ClassesRoot.OpenSubKey(".spk");
                if (key != null)
                    Helper.RunProcessAsAdmin("reg.exe", @"delete HKEY_CLASSES_ROOT\.spk\shell\edit /f");

                if (key != null && key.GetValue("") != null)
                {
                    var path = key.GetValue("").ToString();
                    Helper.RunProcessAsAdmin("reg.exe", string.Format(@"delete HKEY_CLASSES_ROOT\{0}\shell\edit /f", path));
                }

            }
            catch { }
        }

        private void AddOpenWith()
        {
            try
            {
                string path;

                RemoveOpenWith();
                var key = Registry.ClassesRoot.OpenSubKey(".spk");
                //If the user as open SPK with anoher software, the key is redirected to "spk_auto_file"
                if (key != null && key.GetValue("") != null)
                {
                    path = key.GetValue("").ToString(); //Should be "spk_auto_file"
                }
                else
                {
                    path = ".spk";
                }
                var command = string.Format(@"\""{0}\"" edit:\""%1\""", Assembly.GetEntryAssembly().Location);
                Helper.RunProcessAsAdmin("reg.exe", string.Format(@"add HKEY_CLASSES_ROOT\{0}\shell\edit /d ""Edit with Mods Packager"" /t REG_SZ", path));
                Helper.RunProcessAsAdmin("reg.exe", string.Format(@"add HKEY_CLASSES_ROOT\{0}\shell\edit\command /d ""{1}"" /t REG_SZ", path, command));

                //Adding the menu here above does not work if the suer as decided to always "open with" another software. Delete that choice.
                Helper.RunProcessAsAdmin("reg.exe", @"delete HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.spk\UserChoice /f");

            }
            catch { }
        }

        private void buttonDefaultPackageRoot_Click(object sender, EventArgs e)
        {
            PickPackageRoot();
        }

        private void checkBoxDefaultPackageRoot_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultPackageRoot = checkBoxDefaultPackageRoot.Checked;
            if (checkBoxDefaultPackageRoot.Checked)
            {
                if (string.IsNullOrEmpty(Properties.Settings.Default.PackageRoot))
                    PickPackageRoot();
            }
            ShowParameters();
        }

        private void checkBoxPromptExplorer_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PromptExplorer = checkBoxPromptExplorer.Checked;
            ShowParameters();
        }

        private void labelDefaultPackageRoot_Click(object sender, EventArgs e)
        {
            var path = Properties.Settings.Default.PackageRoot;
            if (Directory.Exists(path))
                Process.Start(path);
            else
                MessageBoxEx.Show(this, "This folder does not exist anymore or cannot be accessed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show(this, "Are you sure that you want to reset all user settings?\r\n\r\nThis cannot be undone!", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
            {
                Properties.Settings.Default.DefaultPackageRoot = false;
                Properties.Settings.Default.PackageRoot = "";
                Properties.Settings.Default.DefaultPackageRepo = false;
                Properties.Settings.Default.PackageRepo = "";
                Properties.Settings.Default.PromptExplorer = true;
                Properties.Settings.Default.Recents = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.RecentsName = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.AdvancedEditor = false;
                Properties.Settings.Default.LastPackage = null;
                RemoveOpenWith();
                Properties.Settings.Default.Save();

                ShowParameters();
            }
        }

        private void buttonEditDSMReleases_Click(object sender, EventArgs e)
        {
            var dsmReleases = Path.Combine(Helper.ResourcesDirectory, "dsm_releases");

            var content = File.ReadAllText(dsmReleases);
            var dsmRelease = new ScriptInfo(content, "DSM Releases", new Uri("https://archive.synology.com/download/Os/DSM"), "List of valid DSM releases");
            DialogResult result = Helper.ScriptEditor(null, dsmRelease, null);
            if (result == DialogResult.OK)
            {
                Helper.WriteAnsiFile(dsmReleases, dsmRelease.Code);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CopyPackagePath = checkBoxCopyPackagePath.Checked;
            ShowParameters();
        }

        private void buttonPhpExtensions_Click(object sender, EventArgs e)
        {
            var phpExtensions = Path.Combine(Helper.ResourcesDirectory, "php_extensions");

            var content = File.ReadAllText(phpExtensions);
            var phpExtension = new ScriptInfo(content, "Php Extensions", new Uri("https://en.wikipedia.org/wiki/List_of_PHP_extensions"), "List of Php Extensions");
            DialogResult result = Helper.ScriptEditor(null, phpExtension, null);
            if (result == DialogResult.OK)
            {
                Helper.WriteAnsiFile(phpExtensions, phpExtension.Code);
            }
        }
    }
}

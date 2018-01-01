using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatificaBytes.Synology.Mods
{
    public partial class Parameters : Form
    {
        private OpenFolderDialog SpkRepoBrowserDialog4Mods = new OpenFolderDialog();
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

            checkBoxPublishFolder.Checked = Properties.Settings.Default.DefaultPackageRepo;
            checkBoxOpenWith.Checked = Properties.Settings.Default.OpenWith;
            labelDefaultPublishFolder.Text = Properties.Settings.Default.PackageRepo;
            labelDefaultPublishFolder.Enabled = checkBoxPublishFolder.Checked;
            buttonDefaultPackageRepo.Visible = checkBoxPublishFolder.Checked;
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

        private void checkBoxPublishFolder_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultPackageRepo = checkBoxPublishFolder.Checked;
            if (checkBoxPublishFolder.Checked)
            {
                if (string.IsNullOrEmpty(Properties.Settings.Default.PackageRepo))
                    PickPackageRepo();
            }
            ShowParameters();
        }

        private void PickPackageRepo()
        {
            var path = Properties.Settings.Default.PackageRepo;
            SpkRepoBrowserDialog4Mods.Title = "Pick a folder to publish the Package.";
            if (!string.IsNullOrEmpty(path))
                SpkRepoBrowserDialog4Mods.InitialDirectory = path;
            else
                SpkRepoBrowserDialog4Mods.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (SpkRepoBrowserDialog4Mods.ShowDialog())
            {
                Properties.Settings.Default.PackageRepo = SpkRepoBrowserDialog4Mods.FileName;
            }
        }

        private void buttonDefaultPackageRepo_Click(object sender, EventArgs e)
        {
            PickPackageRepo();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
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
            finally
            {
                ShowParameters();
            }
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
            finally
            {
                ShowParameters();
            }
        }
    }
}

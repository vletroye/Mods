using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BeatificaBytes.Synology.Mods.MainForm;

namespace BeatificaBytes.Synology.Mods
{
    /// <summary>
    /// Edit Linker: https://help.synology.com/developer-guide/resource_acquisition/usrlocal_linker.html
    /// in file /conf/resource
    /// {
    ///  "usr-local-linker": {
    ///   "bin" ["<relpath>", ...],
    ///   "lib" ["<relpath>", ...],
    ///   "etc" ["<relpath>", ...]
    ///   }
    /// }
    /// </summary>
    public partial class Worker_Linker : Form
    {
        private JToken origlinker;
        private JToken linker;
        private State stateLinker = State.None;
        private HelpInfo help = new HelpInfo(new Uri("https://help.synology.com/developer-guide/resource_acquisition/usrlocal_linker.html"), "Details about Linker");
        private string ui = null;

        public JToken Specification
        {
            get
            {
                return linker;
            }

            set
            {
                linker = value;
            }
        }

        internal Worker_Linker(Package package)
        {
            InitializeComponent();
            SetLinker(package.Resource);

            textboxLinkerPath.AutoCompleteCustomSource = new AutoCompleteStringCollection();

            var dir = new DirectoryInfo(package.Folder_UI);
            if (Directory.Exists(Path.Combine(package.Folder_Package, "bin")))
            {
                //structure "old style" with app, bin, etc, lib in the target directory.
                dir = new DirectoryInfo(package.Folder_Package);
            }
            else
            {
                ui = package.INFO.DsmUiDir;
            }


            textboxLinkerPath.AutoCompleteCustomSource.Clear();
            var excludes = new string[] { "images", "config", "dsm.cgi.conf", "router.cgi" };
            foreach (var path in dir.EnumerateFiles("*.*", SearchOption.AllDirectories))
            {
                var file = path.FullName.Remove(0, package.Folder_Package.Length + 1);
                if (!Array.Exists(excludes, element => file.StartsWith(element)))
                {
                    textboxLinkerPath.AutoCompleteCustomSource.Add(file.Replace(@"\", @"/"));
                }
            }
        }

        private void SetLinker(PackageResource linker)
        {
            origlinker = JToken.Parse(JsonConvert.SerializeObject(linker.UsrLocalLinker));
            Specification = linker == null ? JsonConvert.DeserializeObject<JObject>(@"{}") : JToken.Parse(JsonConvert.SerializeObject(linker.UsrLocalLinker));

            DisplayLinker();
        }

        private void DisplayLinker()
        {
            try
            {
                if (linker != null)
                    foreach (JProperty ctrl in linker)
                    {
                        var type = ctrl.Name;
                        foreach (var path in ctrl.Values())
                        {
                            var item = new ListViewItem(type);
                            item.SubItems.Add(path.ToString());
                            listViewLinker.Items.Add(item);
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(this, "The list of linkers can't be parsed.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                SafeClose();
            }

            DisplayLinkerDetails(null, null);
        }

        private void DisplayLinkerDetails(string type, string path)
        {
            if (type == null || path == null)
            {
                comboBoxLinkerType.SelectedIndex = -1;
                textboxLinkerPath.Text = path;
            }
            else
            {
                comboBoxLinkerType.SelectedItem = type;
                textboxLinkerPath.Text = path;
            }

            EnableLinkerItemDetails();
        }

        private void EnableLinkerItemDetails()
        {
            comboBoxLinkerType.Enabled = (stateLinker == State.Edit || stateLinker == State.Add);
            textboxLinkerPath.Enabled = (stateLinker == State.Edit || stateLinker == State.Add);

            buttonAddLinker.Enabled = (stateLinker == State.View || stateLinker == State.None);
            buttonEditLinker.Enabled = (stateLinker == State.View);
            ButtonDeleteLinker.Enabled = (stateLinker == State.View);
            buttonSaveLinker.Enabled = (stateLinker == State.Edit || stateLinker == State.Add);
            buttonCancelLinker.Enabled = (stateLinker == State.Edit || stateLinker == State.Add);

            listViewLinker.Enabled = (stateLinker == State.View || stateLinker == State.None);

            buttonCancel.Enabled = (stateLinker == State.View || stateLinker == State.None);
            buttonOk.Enabled = (stateLinker == State.View || stateLinker == State.None);
            buttonRemove.Enabled = (stateLinker == State.View || stateLinker == State.None);
        }


        private void listViewLinker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stateLinker != State.Edit)
            {
                if (listViewLinker.SelectedItems.Count > 0)
                {
                    stateLinker = State.View;
                    DisplayLinkerDetails(listViewLinker.SelectedItems[0].Text, listViewLinker.SelectedItems[0].SubItems[1].Text);
                }
                else
                {
                    stateLinker = State.None;
                    DisplayLinkerDetails(null, null);
                }
            }
        }

        private void listViewLinker_DoubleClick(object sender, EventArgs e)
        {
            if (listViewLinker.SelectedItems.Count == 1)
            {
                buttonEditLinker_Click(sender, e);
            }
        }

        private void buttonAddLinker_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                stateLinker = State.Add;
                DisplayLinkerDetails(null, null);
                comboBoxLinkerType.Focus();
            }
        }

        private void ButtonDeleteLinker_Click(object sender, EventArgs e)
        {
            if (listViewLinker.SelectedItems.Count > 0)
            {
                stateLinker = State.None;
                DisplayLinkerDetails(null, null);
                listViewLinker.SelectedItems[0].Remove();
            }
        }

        private void buttonEditLinker_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                stateLinker = State.Edit;
                EnableLinkerItemDetails();
                comboBoxLinkerType.Focus();
            }

        }

        private void buttonCancelLinker_Click(object sender, EventArgs e)
        {
            if (stateLinker == State.Add)
            {
                stateLinker = State.None;
                DisplayLinkerDetails(null, null);
            }
            else
            {
                if (listViewLinker.SelectedItems.Count > 0)
                {
                    stateLinker = State.View;
                    DisplayLinkerDetails(listViewLinker.SelectedItems[0].SubItems[0].Text, listViewLinker.SelectedItems[0].SubItems[1].Text);
                }
                else
                {
                    stateLinker = State.None;
                    DisplayLinkerDetails(null, null);
                }
            }
        }

        private void buttonSaveLinker_Click(object sender, EventArgs e)
        {
            if (stateLinker == State.Add)
            {
                var item = new ListViewItem(comboBoxLinkerType.SelectedItem.ToString());
                item.SubItems.Add(textboxLinkerPath.Text);
                listViewLinker.Items.Add(item);
            }
            else
            {
                listViewLinker.SelectedItems[0].SubItems[0].Text = comboBoxLinkerType.Text;
                listViewLinker.SelectedItems[0].SubItems[1].Text = textboxLinkerPath.Text;
            }
            stateLinker = State.None;
            DisplayLinkerDetails(null, null);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            linker = JsonConvert.DeserializeObject<JObject>("{}");
            var linkers = new List<List<string>>();
            foreach (ListViewItem item in listViewLinker.Items)
            {
                var type = item.Text;
                var path = item.SubItems[1].Text;
                if (path.StartsWith("/")) path = path.Remove(0, 1);
                if (!string.IsNullOrEmpty(ui) && !path.StartsWith(ui)) path = string.Format("{0}/{1}", ui, path);
                var node = linker.SelectToken(type);
                if (node == null)
                    linker[type] = new JArray() { path };
                else
                    ((JArray)node).Add(path);
            }

            if (CloseScript(DialogResult.OK)) SafeClose();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            var answer = MessageBoxEx.Show(this, "Do you really want to delete all the linkers?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer == DialogResult.Yes)
            {
                linker = null;
                this.DialogResult = DialogResult.OK;
                SafeClose();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public bool PendingChanges()
        {
            var pendingChanges = !(origlinker == null && linker == null);

            if (pendingChanges)
            {
                pendingChanges = (origlinker == null && linker != null) || (origlinker != null && (linker == null || origlinker.ToString() != linker.ToString()));
            }

            return pendingChanges;
        }

        private bool CloseScript(DialogResult exitMode)
        {

            DialogResult = exitMode;
            if (DialogResult == DialogResult.None)
            {
                DialogResult = MessageBoxEx.Show(this, "Do you want to save your changes?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                switch (DialogResult)
                {
                    case DialogResult.Yes:
                        DialogResult = DialogResult.OK;
                        break;
                    case DialogResult.No:
                        DialogResult = DialogResult.Cancel;
                        break;
                    case DialogResult.Cancel:
                        DialogResult = DialogResult.None;
                        break;
                }
            }

            if (DialogResult != DialogResult.None)
            {
                switch (DialogResult)
                {
                    case DialogResult.OK:

                        break;
                    case DialogResult.Cancel:
                        if (PendingChanges())
                        {
                            DialogResult = MessageBoxEx.Show(this, "Do you want really want to quit without saving your changes?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                            switch (DialogResult)
                            {
                                case DialogResult.Yes:

                                    break;
                                case DialogResult.No:
                                    DialogResult = DialogResult.None;
                                    break;
                                case DialogResult.Cancel:
                                    DialogResult = DialogResult.None;
                                    break;
                            }
                        }
                        break;
                }
            }

            return (DialogResult != DialogResult.None);
        }

        private void Linker_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            var info = new ProcessStartInfo(help.Url.AbsoluteUri);
            info.UseShellExecute = true;
            Process.Start(info);
        }

        private void listViewLinker_KeyUp(object sender, KeyEventArgs e)
        {
            if (stateLinker == State.View && e.KeyCode == Keys.Delete)
                ButtonDeleteLinker_Click(null, null);
            if ((stateLinker == State.None || stateLinker == State.View) && e.KeyCode == Keys.Insert)
                buttonAddLinker_Click(null, null);
        }

        private void Worker_Linker_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !CloseScript(DialogResult.Cancel);
        }

        private void SafeClose()
        {
            this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.Worker_Linker_FormClosing);
            Close();
        }

    }
}

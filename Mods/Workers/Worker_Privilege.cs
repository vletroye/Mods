﻿using BeatificaBytes.Synology.Mods.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BeatificaBytes.Synology.Mods.MainForm;

namespace BeatificaBytes.Synology.Mods
{
    /// <summary>
    /// Edit Privilege: https://help.synology.com/developer-guide/privilege/privilege_specification.html
    /// In file /conf/privilege
    // {
    //  "defaults":{
    //    "run-as": "<run-as>"
    //  },
    //  "username": "<username>",?
    //  "groupname": "<groupname>",
    //  "join-groupname": "<existing groupname>", => use "join-groupname" if it already exist as mentionned (but not explained) in this PDF: https://global.download.synology.com/download/Document/Software/DeveloperGuide/Os/DSM/All/enu/DSM_Developer_Guide_7_enu.pdf
    //  "ctrl-script":[{
    //    "action": "<action>",
    //    "run-as": "<run-as>"
    //  }, ...],
    //  "executable": [{
    //    "relpath": "<relpath>",
    //    "run-as": "<run-as>"
    //  }, ...],
    //  "tool": [{
    //    "relpath": "<relpath>",
    //    "user": "<user>",
    //    "group": "<group>",
    //    "permission": "<mode>"
    //  }, ...]
    // }

    /// </summary>
    public partial class Worker_Privilege : Form
    {
        private PackageINFO info;
        private JToken origPrivilege;
        private JToken privilege;
        private State stateCtrlScript = State.None;
        private State stateExecutable = State.None;
        private List<string> actions = new List<string>() { "start", "stop", "status", "prestart", "prestop", "preinst", "postinst", "preuninst", "postuninst", "preupgrade", "postupgrade" };
        private HelpInfo help = new HelpInfo(new Uri("https://help.synology.com/developer-guide/privilege/privilege_specification.html"), "Details about Privilege");

        public JToken Specification
        {
            get
            {
                return privilege;
            }

            set
            {
                privilege = value;
            }
        }

        public Worker_Privilege(PackagePrivilege privilege, PackageINFO info)
        {
            this.info = info;
            InitializeComponent();
            SetPrivilege(privilege);
        }

        private void SetPrivilege(PackagePrivilege privilege)
        {
            origPrivilege = JToken.Parse(JsonConvert.SerializeObject(privilege));
            Specification = privilege == null ? JsonConvert.DeserializeObject<JObject>(@"{""defaults"":{""run-as"":""package""}}") : JToken.Parse(JsonConvert.SerializeObject(privilege));
            
            DisplayPrivilege();
        }

        internal string  GetPrivilege()
        {
            return JsonConvert.SerializeObject(privilege);
        }

        private void DisplayPrivilege()
        {
            try
            {
                var defaults = privilege.SelectToken("defaults");
                var runAs = defaults.SelectToken("run-as");
                if (runAs == null)
                    comboBoxRunAs.SelectedItem = "package";
                else
                    comboBoxRunAs.SelectedItem = runAs.ToString();

                var username = privilege.SelectToken("username");
                if (username != null) textBoxUsername.Text = username.ToString();

                var groupname = privilege.SelectToken("groupname");
                if (groupname != null) textBoxGroupname.Text = groupname.ToString();

                var joingroupname = privilege.SelectToken("join-groupname");
                if (joingroupname != null) textBoxJoinGroupname.Text = joingroupname.ToString();

                var ctrlScript = privilege.SelectToken("ctrl-script");
                if (ctrlScript != null)
                    foreach (var ctrl in ctrlScript)
                    {
                        var item = new ListViewItem(ctrl.SelectToken("action").ToString());
                        item.SubItems.Add(ctrl.SelectToken("run-as").ToString());
                        listViewCtrlScript.Items.Add(item);
                    }

                var executable = privilege.SelectToken("executable");
                if (executable != null)
                    foreach (var exec in executable)
                    {
                        var item = new ListViewItem(exec.SelectToken("relpath").ToString());
                        item.SubItems.Add(exec.SelectToken("run-as").ToString());
                        listViewExecutable.Items.Add(item);
                    }

                var tool = privilege.SelectToken("tool");
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(this, "The privilege file can't be parsed.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                SafeClose();
            }

            DisplayCtrlScriptDetails(null, null);
            DisplayExecutableDetails(null, null);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            var defaults = JObject.Parse(string.Format("{{\"run-as\": \"{0}\"}}", comboBoxRunAs.SelectedItem));
            var username = textBoxUsername.Text;
            var groupname = textBoxGroupname.Text;
            var joingroupname = textBoxJoinGroupname.Text;
            var package = info.Package;

            if (string.IsNullOrEmpty(username) || username == package) username = null;
            if (string.IsNullOrEmpty(groupname) || groupname == package) groupname = null;
            if (string.IsNullOrEmpty(joingroupname) || joingroupname == package) joingroupname = null;

            //Update the Resource file
            privilege["defaults"] = defaults;


            if (privilege.SelectToken("username") != null) ((JObject)privilege).Remove("username");
            if (username != null)
                privilege["username"] = username;

            if (privilege.SelectToken("groupname") != null) ((JObject)privilege).Remove("groupname");
            if (groupname != null)
                privilege["groupname"] = groupname;

            if (privilege.SelectToken("join-groupname") != null) ((JObject)privilege).Remove("join-groupname");
            if (joingroupname != null)
                privilege["join-groupname"] = joingroupname;

            if (privilege.SelectToken("ctrl-script") != null) ((JObject)privilege).Remove("ctrl-script");
            var ctrlScript = new List<string>();
            foreach (ListViewItem item in listViewCtrlScript.Items)
            {
                var action = item.Text;
                var runAs = item.SubItems[1].Text;
                ctrlScript.Add(string.Format("{{\"action\": \"{0}\",\"run-as\": \"{1}\"}}", action, runAs));
            }
            if (ctrlScript.Count > 0)
            {
                var ctrl = JArray.Parse("[" + ctrlScript.Aggregate((i, j) => i + "," + j) + "]");
                privilege["ctrl-script"] = ctrl;
            }

            if (privilege.SelectToken("executable") != null) ((JObject)privilege).Remove("executable");
            var executable = new List<string>();
            foreach (ListViewItem item in listViewExecutable.Items)
            {
                var relpath = item.Text;
                var runAs = item.SubItems[1].Text;
                executable.Add(string.Format("{{\"relpath\": \"{0}\",\"run-as\": \"{1}\"}}", relpath, runAs));
            }
            if (executable.Count > 0)
            {
                var exec = JArray.Parse("[" + executable.Aggregate((i, j) => i + "," + j) + "]");
                privilege["executable"] = exec;
            }

            if (CloseScript(DialogResult.OK)) SafeClose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public bool PendingChanges()
        {
            var pendingChanges = !(origPrivilege == null && privilege == null);

            if (pendingChanges)
            {
                pendingChanges = (origPrivilege == null && privilege != null) || (origPrivilege != null && (privilege == null || origPrivilege.ToString() != privilege.ToString()));
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

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            var answer = MessageBoxEx.Show(this, "Do you really want to delete the privileges?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer == DialogResult.Yes)
            {
                privilege = null;
                this.DialogResult = DialogResult.OK;
                SafeClose();
            }
        }

        private void comboBoxRunAs_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(comboBoxRunAs, "");
        }

        private void comboBoxRunAs_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                var min = 0;
                var max = 6;
                if (info.ContainsKey("os_min_ver") && info["os_min_ver"] != "")
                {
                    int.TryParse(info["os_min_ver"].Substring(0, 1), out min);
                }
                if (info.ContainsKey("os_max_ver") && info["os_max_ver"] != "")
                {
                    int.TryParse(info["os_max_ver"].Substring(0, 1), out max);
                }

                if (string.IsNullOrEmpty(comboBoxRunAs.Text) || (comboBoxRunAs.Text != "package" && (min >= 7 || max > 6)))
                {
                    e.Cancel = true;
                    errorProvider.SetError(comboBoxRunAs, "Must run as 'package' since targeting DSM >= 7");
                }
            }
        }

        //----------------------------------
        //Tab Ctrl-Script

        private void DisplayCtrlScriptDetails(string action, string runas)
        {
            comboBoxCtrlScriptAction.Text = "";
            comboBoxCtrlScriptRunAs.Text = "";

            if (action == null)
                comboBoxCtrlScriptAction.SelectedIndex = comboBoxCtrlScriptAction.Items.Count == 0 ? -1 : 0;
            else
                comboBoxCtrlScriptAction.SelectedItem = action;

            if (runas == null)
                comboBoxCtrlScriptRunAs.SelectedIndex = comboBoxCtrlScriptRunAs.Items.Count == 0 ? -1 : 0;
            else
                comboBoxCtrlScriptRunAs.SelectedItem = runas;

            EnableCtrlScriptItemDetails();
        }

        private void EnableCtrlScriptItemDetails()
        {
            comboBoxCtrlScriptAction.Enabled = (stateCtrlScript == State.Edit || stateCtrlScript == State.Add);
            comboBoxCtrlScriptRunAs.Enabled = (stateCtrlScript == State.Edit || stateCtrlScript == State.Add);

            buttonCtrlScriptAdd.Enabled = (stateCtrlScript == State.View || stateCtrlScript == State.None);
            buttonCtrlScriptEdit.Enabled = (stateCtrlScript == State.View);
            buttonCtrlScriptDelete.Enabled = (stateCtrlScript == State.View);
            buttonCtrlScriptSave.Enabled = (stateCtrlScript == State.Edit || stateCtrlScript == State.Add);
            buttonCtrlScriptCancel.Enabled = (stateCtrlScript == State.Edit || stateCtrlScript == State.Add);

            listViewCtrlScript.Enabled = (stateCtrlScript == State.View || stateCtrlScript == State.None);

            buttonCancel.Enabled = (stateCtrlScript == State.View || stateCtrlScript == State.None);
            buttonOk.Enabled = (stateCtrlScript == State.View || stateCtrlScript == State.None);
            buttonRemove.Enabled = (stateCtrlScript == State.View || stateCtrlScript == State.None);
        }


        private void listViewCtrlScript_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stateCtrlScript != State.Edit)
            {
                if (listViewCtrlScript.SelectedItems.Count > 0)
                {
                    stateCtrlScript = State.View;
                    comboBoxCtrlScriptAction.Items.Clear();
                    comboBoxCtrlScriptAction.Items.AddRange(actions.ToArray());
                    DisplayCtrlScriptDetails(listViewCtrlScript.SelectedItems[0].SubItems[0].Text, listViewCtrlScript.SelectedItems[0].SubItems[1].Text);
                }
                else
                {
                    stateCtrlScript = State.None;
                    DisplayCtrlScriptDetails(null, null);
                }
            }
        }

        private void listViewCtrlScript_DoubleClick(object sender, EventArgs e)
        {
            if (listViewCtrlScript.SelectedItems.Count == 1)
            {
                buttonCtrlScriptEditItem_Click(sender, e);
            }
        }


        // Add an new item
        private void buttonCtrlScriptAddItem_Click(object sender, EventArgs e)
        {
            stateCtrlScript = State.Add;
            comboBoxCtrlScriptAction.Items.Clear();
            comboBoxCtrlScriptAction.Items.AddRange(actions.ToArray());
            foreach (ListViewItem item in listViewCtrlScript.Items)
            {
                comboBoxCtrlScriptAction.Items.Remove(item.Text);
            }
            DisplayCtrlScriptDetails(null, comboBoxRunAs.SelectedItem == null ? null : comboBoxRunAs.SelectedItem.ToString());
            comboBoxCtrlScriptAction.Focus();
        }

        // Edit the item currently selected
        private void buttonCtrlScriptEditItem_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                stateCtrlScript = State.Edit;
                comboBoxCtrlScriptAction.Items.Clear();
                comboBoxCtrlScriptAction.Items.AddRange(actions.ToArray());
                foreach (ListViewItem item in listViewCtrlScript.Items)
                {
                    if (item.Text != comboBoxCtrlScriptAction.Text)
                        comboBoxCtrlScriptAction.Items.Remove(item.Text);
                }
                EnableCtrlScriptItemDetails();
                comboBoxCtrlScriptAction.Focus();
            }
        }

        private void buttonCtrlScriptCancelItem_Click(object sender, EventArgs e)
        {
            if (stateCtrlScript == State.Add)
            {
                stateCtrlScript = State.None;
                DisplayCtrlScriptDetails(null, null);
            }
            else
            {
                if (listViewCtrlScript.SelectedItems.Count > 0)
                {
                    stateCtrlScript = State.View;
                    DisplayCtrlScriptDetails(listViewCtrlScript.SelectedItems[0].SubItems[0].Text, listViewCtrlScript.SelectedItems[0].SubItems[1].Text);
                }
                else
                {
                    stateCtrlScript = State.None;
                    DisplayCtrlScriptDetails(null, null);
                }
            }

        }

        private void buttonCtrlScriptSaveItem_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                if (stateCtrlScript == State.Add)
                {
                    var item = new ListViewItem(comboBoxCtrlScriptAction.SelectedItem.ToString());
                    item.SubItems.Add(comboBoxCtrlScriptRunAs.SelectedItem.ToString());
                    listViewCtrlScript.Items.Add(item);
                }
                else
                {
                    listViewCtrlScript.SelectedItems[0].SubItems[0].Text = comboBoxCtrlScriptAction.Text;
                    listViewCtrlScript.SelectedItems[0].SubItems[1].Text = comboBoxCtrlScriptRunAs.Text;
                }
                stateCtrlScript = State.None;
                DisplayCtrlScriptDetails(null, null);
            }
        }

        private void buttonCtrlScriptDeleteItem_Click(object sender, EventArgs e)
        {
            if (listViewCtrlScript.SelectedItems.Count > 0)
            {
                stateCtrlScript = State.None;
                DisplayCtrlScriptDetails(null, null);
                listViewCtrlScript.SelectedItems[0].Remove();
            }
        }

        //----------------------------------
        //Tab Executable

        private void DisplayExecutableDetails(string action, string runas)
        {
            if (action == null || runas == null)
            {
                textBoxExecutablePath.Text = "";
                comboBoxExecutableRunAs.SelectedIndex = -1;
            }
            else
            {
                textBoxExecutablePath.Text = action;
                comboBoxExecutableRunAs.SelectedItem = runas;
            }

            EnableExecutableItemDetails();
        }

        private void EnableExecutableItemDetails()
        {
            textBoxExecutablePath.Enabled = (stateExecutable == State.Edit || stateExecutable == State.Add);
            comboBoxExecutableRunAs.Enabled = (stateExecutable == State.Edit || stateExecutable == State.Add);

            buttonExecutableAdd.Enabled = (stateExecutable == State.View || stateExecutable == State.None);
            buttonExecutableEdit.Enabled = (stateExecutable == State.View);
            buttonExecutableDelete.Enabled = (stateExecutable == State.View);
            buttonExecutableSave.Enabled = (stateExecutable == State.Edit || stateExecutable == State.Add);
            buttonExecutableCancel.Enabled = (stateExecutable == State.Edit || stateExecutable == State.Add);

            listViewExecutable.Enabled = (stateExecutable == State.View || stateExecutable == State.None);

            buttonCancel.Enabled = (stateExecutable == State.View || stateExecutable == State.None);
            buttonOk.Enabled = (stateExecutable == State.View || stateExecutable == State.None);
            buttonRemove.Enabled = (stateExecutable == State.View || stateExecutable == State.None);
        }


        private void listViewExecutable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stateExecutable != State.Edit)
            {
                if (listViewExecutable.SelectedItems.Count > 0)
                {
                    stateExecutable = State.View;
                    DisplayExecutableDetails(listViewExecutable.SelectedItems[0].SubItems[0].Text, listViewExecutable.SelectedItems[0].SubItems[1].Text);
                }
                else
                {
                    stateExecutable = State.None;
                    DisplayExecutableDetails(null, null);
                }
            }
        }

        private void listViewExecutable_DoubleClick(object sender, EventArgs e)
        {
            if (listViewExecutable.SelectedItems.Count == 1)
            {
                buttonExecutableEdit_Click(sender, e);
            }
        }


        // Add an new item
        private void buttonExecutableAdd_Click(object sender, EventArgs e)
        {
            stateExecutable = State.Add;
            DisplayExecutableDetails(null, comboBoxRunAs.SelectedItem == null ? null : comboBoxRunAs.SelectedItem.ToString());
            textBoxExecutablePath.Focus();
        }

        // Edit the item currently selected
        private void buttonExecutableEdit_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                EnableExecutableItemDetails();
                textBoxExecutablePath.Focus();
            }
        }

        private void buttonExecutableCancel_Click(object sender, EventArgs e)
        {
            if (stateExecutable == State.Add)
            {
                stateExecutable = State.None;
                DisplayExecutableDetails(null, null);
            }
            else
            {
                if (listViewExecutable.SelectedItems.Count > 0)
                {
                    stateExecutable = State.View;
                    DisplayExecutableDetails(listViewExecutable.SelectedItems[0].SubItems[0].Text, listViewExecutable.SelectedItems[0].SubItems[1].Text);
                }
                else
                {
                    stateExecutable = State.None;
                    DisplayExecutableDetails(null, null);
                }
            }

        }

        private void buttonExecutableSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                if (stateExecutable == State.Add)
                {
                    var item = new ListViewItem(textBoxExecutablePath.Text);
                    item.SubItems.Add(comboBoxExecutableRunAs.SelectedItem.ToString());
                    listViewExecutable.Items.Add(item);
                }
                else
                {
                    listViewExecutable.SelectedItems[0].SubItems[0].Text = textBoxExecutablePath.Text;
                    listViewExecutable.SelectedItems[0].SubItems[1].Text = comboBoxExecutableRunAs.SelectedItem.ToString();
                }
                stateExecutable = State.None;
                DisplayExecutableDetails(null, null);
            }
        }

        private void buttonExecutableDelete_Click(object sender, EventArgs e)
        {
            if (listViewExecutable.SelectedItems.Count > 0)
            {
                stateExecutable = State.None;
                DisplayExecutableDetails(null, null);
                listViewExecutable.SelectedItems[0].Remove();
            }
        }

        private void Privilege_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            var info = new ProcessStartInfo(help.Url.AbsoluteUri);
            info.UseShellExecute = true;
            Process.Start(info);
        }

        private void listViewCtrlScript_KeyUp(object sender, KeyEventArgs e)
        {
            if (stateCtrlScript == State.View && e.KeyCode == Keys.Delete)
                buttonCtrlScriptDeleteItem_Click(null, null);
            if ((stateCtrlScript == State.None || stateCtrlScript == State.View) && e.KeyCode == Keys.Insert)
                buttonCtrlScriptAddItem_Click(null, null);
        }

        private void listViewExecutable_KeyUp(object sender, KeyEventArgs e)
        {
            if (stateExecutable == State.View && e.KeyCode == Keys.Delete)
                buttonExecutableDelete_Click(null, null);
            if ((stateExecutable == State.None || stateExecutable == State.View) && e.KeyCode == Keys.Insert)
                buttonExecutableAdd_Click(null, null);
        }

        private void comboBoxCtrlScriptAction_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (comboBoxCtrlScriptAction.SelectedItem == null && comboBoxCtrlScriptAction.Enabled)
                {
                    e.Cancel = true;
                    errorProvider.SetError(comboBoxCtrlScriptAction, "The Script may not be empty");
                }
            }
        }

        private void comboBoxCtrlScriptAction_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(comboBoxCtrlScriptAction, "");
        }

        private void comboBoxCtrlScriptRunAs_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (comboBoxCtrlScriptRunAs.SelectedItem == null && comboBoxCtrlScriptRunAs.Enabled)
                {
                    e.Cancel = true;
                    errorProvider.SetError(comboBoxCtrlScriptRunAs, "The RunAs may not be empty");
                }
            }
        }

        private void comboBoxCtrlScriptRunAs_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(comboBoxCtrlScriptRunAs, "");
        }


        private void textBoxExecutablePath_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (string.IsNullOrEmpty(textBoxExecutablePath.Text) && textBoxExecutablePath.Enabled)
                {
                    e.Cancel = true;
                    errorProvider.SetError(textBoxExecutablePath, "The Path may not be empty");
                }
            }
        }

        private void textBoxExecutablePath_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxExecutablePath, "");
        }

        private void comboBoxExecutableRunAs_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (comboBoxExecutableRunAs.SelectedItem == null && comboBoxExecutableRunAs.Enabled)
                {
                    e.Cancel = true;
                    errorProvider.SetError(comboBoxExecutableRunAs, "The RunAs may not be empty");
                }
            }
        }

        private void comboBoxExecutableRunAs_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(comboBoxExecutableRunAs, "");
        }

        private void Privilege_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                errorProvider.Tag = new object();
                ResetValidateChildren(this);
                errorProvider.Tag = null;
            }
        }

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

        private void Worker_Privilege_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !CloseScript(DialogResult.Cancel);
        }

        private void SafeClose()
        {
            this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.Worker_Privilege_FormClosing);
            Close();
        }

        private void textBoxGroupname_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxGroupname.Text == "http" || textBoxGroupname.Text == "service")
            {
                textBoxJoinGroupname.Text = textBoxGroupname.Text;
                textBoxGroupname.Text = "";                
            }
        }
    }
}

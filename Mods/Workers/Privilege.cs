using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatificaBytes.Synology.Mods
{
    public partial class Privilege : Form
    {
        SortedDictionary<string, string> info;
        private JToken origPrivilege;
        private JToken privilege;

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

        public Privilege(JToken privilege, SortedDictionary<string, string> info)
        {
            this.info = info;
            InitializeComponent();
            SetPrivilege(privilege);
        }

        private void SetPrivilege(JToken privilege)
        {
            origPrivilege = privilege;
            Specification = privilege == null ? JsonConvert.DeserializeObject<JObject>(@"{""defaults"":{""run-as"":""system""}}") : privilege.DeepClone();

            DisplayPrivilege();
        }

        private void DisplayPrivilege()
        {
            try
            {
                var defaults = privilege.SelectToken("defaults");
                var runAs = defaults.SelectToken("run-as");
                if (runAs == null)
                    comboBoxRunAs.SelectedItem = "system";
                else
                    comboBoxRunAs.SelectedItem = runAs.ToString();

                var username = privilege.SelectToken("username");
                if (username != null) textBoxUsername.Text = username.ToString();

                var groupname = privilege.SelectToken("groupname");
                if (groupname != null) textBoxGroupname.Text = groupname.ToString();

                var ctrlScript = privilege.SelectToken("ctrl-script");

                var executable = privilege.SelectToken("executable");

                var tool = privilege.SelectToken("tool");
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(this, "The privilege file can't be parsed.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

            var defaults = JObject.Parse(string.Format("{{\"run-as\": \"{0}\"}}", comboBoxRunAs.SelectedItem));
            var username = textBoxUsername.Text;
            var groupname = textBoxGroupname.Text;
            var package = info["package"];

            if (string.IsNullOrEmpty(username) || username == package) username = null;
            if (string.IsNullOrEmpty(groupname) || groupname == package) groupname = null;

            //Update the Resource file
            privilege["defaults"] = defaults;
            if (username == null)
            { if (privilege.SelectToken("username") != null) ((JObject)privilege).Remove("username"); }
            else
            { privilege["username"] = username; }
            if (groupname == null)
            { if (privilege.SelectToken("groupname") != null) ((JObject)privilege).Remove("groupname"); }
            else
            { privilege["groupname"] = groupname; }
            CloseScript(DialogResult.OK);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (!PendingChanges())
                DialogResult = DialogResult.Cancel;
            else
                CloseScript(DialogResult.Cancel);
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

        private void CloseScript(DialogResult exitMode)
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

                if (DialogResult != DialogResult.None) Close();
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            var answer = MessageBoxEx.Show(this, "Do you really want to delete the privileges?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer == DialogResult.Yes)
            {
                privilege = null;
                this.DialogResult = DialogResult.OK;
                this.Close();
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
                if (info.ContainsKey("os_max_ver") && info["os_max_ver"] !="")
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
    }
}

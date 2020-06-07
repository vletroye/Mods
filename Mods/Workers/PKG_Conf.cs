using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using IniParser;
using System.IO;
using IniParser.Model;
using System.Data;
using System.ComponentModel;
using System.Text.RegularExpressions;
using IniParser.Model.Configuration;
using IniParser.Parser;

namespace BeatificaBytes.Synology.Mods
{
    public partial class PKG_Conf : Form
    {
        private HelpInfo help = new HelpInfo(new Uri("https://help.synology.com/developer-guide/synology_package/conf.html"), "Details about Port Config");
        List<Tuple<string, string>> variables;
        private IniData pkgConf;
        private IniData origPkgConf;
        private string pkgConfName;

        private List<string> fields;
        private Dictionary<string, string> current;
        private State state = State.None;

        public IniData PkgConf
        {
            get
            {
                return pkgConf;
            }

            set
            {
                pkgConf = value;
            }
        }

        public enum State
        {
            None,
            View,
            Edit,
            Add
        }

        //public PortConfig(JObject resource, string packageFolder)
        public PKG_Conf(IniData pkgConf, List<Tuple<string, string>> variables, string pkgConfName, string toolTip)
        {
            InitializeComponent();

            fields = new List<string>();
            fields.Add("package_name");
            fields.Add("pkg_min_ver");
            fields.Add("pkg_max_ver");
            fields.Add("dsm_min_ver");
            fields.Add("dsm_max_ver");

            this.origPkgConf = pkgConf == null ? null : pkgConf.Clone() as IniData;
            this.pkgConfName = pkgConfName;
            this.Text = this.Text + " - " + pkgConfName;

            this.toolTip.SetToolTip(this.dataGridViewConfig, toolTip);

            this.variables = variables;
            SetPkgConfig(pkgConf);

            foreach (var control in panelPortConfig.Controls)
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

            Helper.LoadDSMReleases(textBoxDsmMinVer);
            Helper.LoadDSMReleases(textBoxDsmMaxVer);
        }

        private void SetPkgConfig(IniData pkgConf)
        {
            this.PkgConf = pkgConf;

            checkBoxConfig.Checked = pkgConf != null;
            panelPortConfig.Visible = pkgConf != null;
            buttonAdvanced.Enabled = pkgConf != null;

            DisplayPkgConf();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (PkgConf != null && PkgConf.Sections.Count == 0)
            {
                MessageBoxEx.Show(this, "You must add at least one Package Configuration!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else
            {
                CloseScript(DialogResult.OK);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (!PendingChanges())
                DialogResult = DialogResult.Cancel;
            else
                CloseScript(DialogResult.Cancel);
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var info = new ProcessStartInfo(help.Url.AbsoluteUri);
            info.UseShellExecute = true;
            Process.Start(info);
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
                        if (true)
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

        private void DisplayPkgConf()
        {
            if (PkgConf != null)
            {
                DataTable dt = new DataTable();
                var index = 0;
                foreach (var field in fields)
                {
                    dt.Columns.Add(field);
                    dataGridViewConfig.Columns[index].DataPropertyName = field;
                    index++;
                }
                foreach (var section in PkgConf.Sections)
                {
                    dt.Rows.Add(
                        section.SectionName,
                        Helper.Unquote(section.Keys["pkg_min_ver"]),
                        Helper.Unquote(section.Keys["pkg_max_ver"]),
                        Helper.Unquote(section.Keys["dsm_min_ver"]),
                        Helper.Unquote(section.Keys["dsm_max_ver"]));
                }
                dataGridViewConfig.DataSource = dt;
                dataGridViewConfig.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                dataGridViewConfig.ClearSelection();
            }
        }

        private void dataGridViewPortConfig_SelectionChanged(object sender, EventArgs e)
        {
            SetCurrent();
        }

        private void SetCurrent()
        {
            if (dataGridViewConfig.SelectedRows.Count > 0)
            {
                var row = dataGridViewConfig.SelectedRows[0];
                current = new Dictionary<string, string>();
                var index = 0;
                foreach (var field in fields)
                {
                    current.Add(field, row.Cells[index].Value as string);
                    index++;
                }
                state = State.View;
            }
            else
            {
                SetEmptyConfig();
                state = State.None;
            }
            DisplayPortConfigDetails();
        }

        private void SetEmptyConfig()
        {
            current = new Dictionary<string, string>();
            int index = 0;
            foreach (var field in fields)
            {
                current.Add(field, "");
                index++;
            }
        }

        private void DisplayPortConfigDetails()
        {
            switch (state)
            {
                case State.None:
                    EnableButtons(true, false, false, false, false, true);
                    break;
                case State.View:
                    EnableButtons(true, true, true, false, false, true);
                    break;
                case State.Add:
                    EnableButtons(false, false, false, true, true, false);
                    break;
                case State.Edit:
                    EnableButtons(false, false, false, true, true, false);
                    break;
            }

            textBoxPackage.Text = current["package_name"];
            textBoxPkgMinVer.Text = current["pkg_min_ver"];
            textBoxPkgMaxVer.Text = current["pkg_max_ver"];
            textBoxDsmMinVer.Text = current["dsm_min_ver"];
            textBoxDsmMaxVer.Text = current["dsm_max_ver"];

            EnableFields();
        }

        private void EnableButtons(bool Add, bool Edit, bool Delete, bool Save, bool Abort, bool Others)
        {
            buttonAdd.Enabled = Add;
            buttonEdit.Enabled = Edit;
            buttonDelete.Enabled = Delete;
            buttonSave.Enabled = Save;
            buttonAbort.Enabled = Abort;

            buttonOk.Visible = Others;
            buttonCancel.Visible = Others;
            buttonAdvanced.Visible = Others;
        }

        private void EnableFields()
        {
            var enabled = state == State.Edit || state == State.Add;

            checkBoxConfig.Enabled = (state != State.Add) && (state != State.Edit);

            if (enabled)
            {
                this.CancelButton = null;
            }
            else
            {
                this.CancelButton = this.buttonCancel;
            }

            textBoxPackage.Enabled = enabled;
            textBoxPkgMinVer.Enabled = enabled;
            textBoxPkgMaxVer.Enabled = enabled;
            textBoxDsmMinVer.Enabled = enabled;
            textBoxPkgMaxVer.Enabled = enabled;
            textBoxDsmMaxVer.Enabled = enabled;
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
                var text = toolTip.GetToolTip(zone);
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            state = State.Add;
            SetEmptyConfig();
            DisplayPortConfigDetails();
            textBoxPackage.Focus();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            state = State.Edit;
            DisplayPortConfigDetails();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBoxEx.Show(this, string.Format("Do you really want to delete the config {0} ?", current["package_name"]), "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            switch (result)
            {
                case DialogResult.Yes:
                    PkgConf.Sections.RemoveSection(current["package_name"]);
                    state = State.None;
                    DisplayPkgConf();
                    break;
                case DialogResult.No:
                case DialogResult.Cancel:

                    break;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var succeed = ValidateChildren();
            if (succeed)
            {
                if (state == State.Edit)
                {
                    var newSc = new IniData();
                    foreach (var section in PkgConf.Sections)
                    {
                        if (section.SectionName != current["package_name"])
                        {
                            newSc.Sections.AddSection(section.SectionName);
                            var newSection = newSc.Sections[section.SectionName];
                            foreach (var key in PkgConf.Sections[section.SectionName])
                            {
                                var value = Helper.Unquote(key.Value.Trim());
                                if (!string.IsNullOrEmpty(value))
                                    newSection.AddKey(key.KeyName, value);
                            }
                        }
                        else
                        {
                            AddNewSection(ref newSc);
                        }
                    }
                    PkgConf = newSc;
                }
                else
                {
                    succeed = AddNewSection(ref pkgConf);
                }
            }

            if (succeed)
            {
                state = State.None;
                DisplayPkgConf();
                buttonAdd.Focus();
            }
        }

        private bool AddNewSection(ref IniData newSc)
        {
            if (newSc == null) newSc = new IniData();
            var succeed = newSc.Sections.AddSection(textBoxPackage.Text);
            if (succeed)
            {
                var newSection = newSc.Sections[textBoxPackage.Text];
                newSection.AddKey("pkg_min_ver", textBoxPkgMinVer.Text.Trim());
                newSection.AddKey("pkg_max_ver", textBoxPkgMaxVer.Text.Trim());
                newSection.AddKey("dsm_min_ver", textBoxDsmMinVer.Text.Trim());
                newSection.AddKey("dsm_max_ver", textBoxDsmMaxVer.Text.Trim());
            }
            else
            {
                errorProvider.SetError(textBoxPackage, string.Format("You may not save this Package Config as another one with the same name '{0}' exists!", textBoxPackage.Text));
            }

            return succeed;
        }

        private void buttonAbort_Click(object sender, EventArgs e)
        {
            state = State.None;
            SetCurrent();
        }

        private void PortConfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                errorProvider.Tag = new object(); ;
                ResetValidateChildren(this);
                checkBoxConfig.Focus();
                errorProvider.Tag = null;
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

        private void textBoxPackage_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxPackage, "");
        }

        private void textBoxPackage_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (!CheckEmpty(textBoxPackage, ref e, ""))
                {
                    if (PkgConf != null)
                    {
                        var newService = Helper.CleanUpText(textBoxPackage.Text);
                        foreach (var section in PkgConf.Sections)
                        {
                            if (section.SectionName == newService && section.SectionName != current["package_name"])
                            {
                                e.Cancel = true;
                                textBoxPackage.Select(0, textBoxPackage.Text.Length);
                                errorProvider.SetError(textBoxPackage, "This Service Name is already used");
                                break;
                            }
                        }
                    }
                }
            }
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
            if (textBox.Text.Contains("\""))
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

        public bool PendingChanges()
        {
            var pendingChanges = !(origPkgConf == null && (pkgConf == null || pkgConf.Sections.Count == 0));

            if (pendingChanges)
            {
                pendingChanges = (origPkgConf == null && pkgConf != null) || (origPkgConf != null && (pkgConf == null || origPkgConf.ToString() != pkgConf.ToString()));
            }

            return pendingChanges;
        }

        private void buttonAdvanced_Click(object sender, EventArgs e)
        {
            var script = new ScriptInfo(pkgConf.ToString(), "Package Configuration", help.Url, "Details about Package Configuration");
            DialogResult result = Helper.ScriptEditor(script, null, variables);
            if (result == DialogResult.OK)
            {
                {
                    IniParserConfiguration config = new IniParserConfiguration();
                    config.AssigmentSpacer = "";
                    var parser = new IniDataParser(config);
                    var streamParser = new StreamIniDataParser(parser);
                    try
                    {
                        using (var stream = Helper.GetStreamFromString(script.Code))
                        {
                            using (var streamReader = new StreamReader(stream))
                            {
                                pkgConf = streamParser.ReadData(streamReader);
                            }
                        }
                    }
                    catch
                    {
                        MessageBoxEx.Show(this, "This Package Configuration can't be parsed.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
                DisplayPkgConf();
            }
        }


        private void textBoxFirmware_Validating(object sender, CancelEventArgs e)
        {
            TextBox firmware = sender as TextBox;
            if (errorProvider.Tag == null && firmware != null)
            {
                //if (!CheckEmpty(textBoxFirmware, ref e, ""))
                if (!string.IsNullOrEmpty(firmware.Text))
                {
                    if (Helper.getOldFirmwareVersion.IsMatch(firmware.Text))
                    {
                        var parts = firmware.Text.Split('.');
                        firmware.Text = string.Format("{0}.{1}-{2:D4}", parts[0], parts[1], int.Parse(parts[2]));
                    }
                    if (Helper.getShortFirmwareVersion.IsMatch(firmware.Text))
                    {
                        var parts = firmware.Text.Split('.');
                        firmware.Text = string.Format("{0}.{1}-0000", parts[0], parts[1]);
                    }
                    if (!Helper.getFirmwareVersion.IsMatch(firmware.Text))
                    {
                        e.Cancel = true;
                        firmware.Select(0, firmware.Text.Length);
                        errorProvider.SetError(firmware, "The format of a firmware must be like 0.0-0000");
                    }
                    else
                    {
                        var parts = firmware.Text.Split(new char[] { '.', '-' });
                        if (int.Parse(parts[2]) == 0)
                            firmware.Text = string.Format("{0}.{1}", parts[0], parts[1]);
                        else
                            firmware.Text = string.Format("{0}.{1}-{2:D4}", parts[0], parts[1], int.Parse(parts[2]));
                    }
                }
            }
        }
        private void textBoxFirmware_Validated(object sender, EventArgs e)
        {
            TextBox firmware = sender as TextBox;
            if (firmware != null)
            {
                errorProvider.SetError(firmware, "");
            }
        }

        private void checkBoxConfig_Click(object sender, EventArgs e)
        {
            if (checkBoxConfig.Checked)
            {
                if (PkgConf != null && PkgConf.Sections.Count > 0)
                {
                    var dialogResult = origPkgConf == null ? DialogResult.Yes : MessageBoxEx.Show(this, "Do you want really want to delete this Package Configuration?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SetPkgConfig(null);
                    }
                }
                else
                {
                    SetPkgConfig(null);
                }
            }
            else if (pkgConf == null)
            {
                if (origPkgConf == null)
                {
                    SetPkgConfig(new IniData());
                }
                else
                {
                    SetPkgConfig(origPkgConf);
                }
            }
            else
            {
                SetPkgConfig(pkgConf);
            }
            SetCurrent();
        }
    }
}

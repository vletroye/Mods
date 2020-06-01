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
    public partial class PortConfigWorker : Form
    {
        //private HelpInfo help = new HelpInfo(new Uri("https://help.synology.com/developer-guide/resource_acquisition/port_config.html"), "Details about Port Config");
        private HelpInfo help = new HelpInfo(new Uri("https://help.synology.com/developer-guide/integrate_dsm/install_ports.html"), "Details about Port Config");
        List<Tuple<string, string>> variables;
        private JToken portConfig;
        private JToken origPortConfig;
        private IniData synoConfig;
        private IniData origSynoConfig;
        private Regex validPorts = new Regex("^(()([1-9]|[1-5]?[0-9]{2,4}|6[1-4][0-9]{3}|65[1-4][0-9]{2}|655[1-2][0-9]|6553[1-5]))([,:](()([1-9]|[1-5]?[0-9]{2,4}|6[1-4][0-9]{3}|65[1-4][0-9]{2}|655[1-2][0-9]|6553[1-5])))*$");
        private string packageName;

        private List<string> fields;
        private Dictionary<string, string> current;
        private State state = State.None;

        public JToken PortConfig
        {
            get
            {
                return portConfig;
            }

            set
            {
                portConfig = value;
            }
        }

        public IniData SynoConfig
        {
            get
            {
                return synoConfig;
            }

            set
            {
                synoConfig = value;
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
        public PortConfigWorker(JToken portConfig, IniData synoConfig, List<Tuple<string, string>> variables, string packageName)
        {
            InitializeComponent();

            fields = new List<string>();
            fields.Add("service_name");
            fields.Add("title");
            fields.Add("desc");
            fields.Add("port_forward");
            fields.Add("src.ports");
            fields.Add("dst.ports");

            this.origPortConfig = portConfig == null ? null : portConfig.DeepClone();
            this.origSynoConfig = synoConfig == null ? null : synoConfig.Clone() as IniData;
            this.packageName = packageName;
            this.variables = variables;
            SetPortConfig(portConfig, synoConfig);

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
        }

        private void SetPortConfig(JToken portConfig, IniData synoConfig)
        {
            PortConfig = portConfig;
            SynoConfig = synoConfig;

            checkBoxPortConfig.Checked = portConfig != null;
            panelPortConfig.Visible = portConfig != null;
            buttonAdvanced.Enabled = portConfig != null;

            DisplayPortConfig();
        }
        private void checkBoxPortConfig_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxPortConfig.Checked && PortConfig != null)
            {
                var dialogResult = origPortConfig == null ? DialogResult.Yes : MessageBoxEx.Show(this, "Do you want really want to delete the Port Config?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                if (dialogResult == DialogResult.Yes)
                {
                    SetPortConfig(null, null);
                }
            }
            else if (checkBoxPortConfig.Checked && PortConfig == null)
            {
                if (origPortConfig == null)
                {
                    SetPortConfig(JObject.Parse(string.Format("{\"protocol-file\": \"etc/{0}.sc\"}", packageName)), null);
                }
                else
                {
                    SetPortConfig(origPortConfig, origSynoConfig);
                }
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (portConfig != null && (SynoConfig == null || SynoConfig.Sections.Count == 0))
            {
                MessageBoxEx.Show(this, "You must add at least one Port Configuration!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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

        private void DisplayPortConfig()
        {
            if (PortConfig != null)
            {
                DataTable dt = new DataTable();
                var index = 0;
                foreach (var field in fields)
                {
                    dt.Columns.Add(field);
                    dataGridViewPortConfig.Columns[index].DataPropertyName = field;
                    index++;
                }
                if (SynoConfig != null)
                {
                    foreach (var section in SynoConfig.Sections)
                    {
                        dt.Rows.Add(
                            section.SectionName,
                            Unquote(section.Keys["title"]),
                            Unquote(section.Keys["desc"]),
                            section.Keys["port_forward"] == "\"yes\"",
                            Unquote(section.Keys["src.ports"]),
                            Unquote(section.Keys["dst.ports"]));
                    }
                    dataGridViewPortConfig.DataSource = dt;
                    dataGridViewPortConfig.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                    dataGridViewPortConfig.ClearSelection();
                }
                else
                {
                    SetCurrent();
                }

            }
        }

        private string Unquote(string text)
        {
            if (text != null)
            {
                if (text.StartsWith("\"")) text = text.Substring(1);
                if (text.EndsWith("\"")) text = text.Substring(0, text.Length - 1);
            }
            return text;
        }

        private void dataGridViewPortConfig_SelectionChanged(object sender, EventArgs e)
        {
            SetCurrent();
        }

        private void SetCurrent()
        {
            if (dataGridViewPortConfig.SelectedRows.Count > 0)
            {
                var row = dataGridViewPortConfig.SelectedRows[0];
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
                SetEmptyPortConfig();
                state = State.None;
            }
            DisplayPortConfigDetails();
        }

        private void SetEmptyPortConfig()
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

            textBoxService.Text = current["service_name"];
            textBoxTitle.Text = current["title"];
            textBoxDescription.Text = current["desc"];
            checkBoxForward.Checked = current["port_forward"] == "True";

            string protocol;
            string ports;
            GetPortProtocol(current["src.ports"], out ports, out protocol);
            textBoxSrcPorts.Text = ports;
            comboBoxSrcProtocol.SelectedItem = protocol;
            GetPortProtocol(current["dst.ports"], out ports, out protocol);
            textBoxDstPorts.Text = ports;
            comboBoxDstProtocol.SelectedItem = protocol;

            EnableFields();
        }

        private void GetPortProtocol(string data, out string ports, out string protocol)
        {
            var parts = (data ?? "").Split('/');
            if (parts.Length == 2)
            {
                protocol = parts[1];
                ports = parts[0];
            }
            else
            {
                protocol = "";
                ports = "";
            }
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

            if (enabled)
            {
                this.CancelButton = null;
            }
            else
            {
                this.CancelButton = this.buttonCancel;
            }

            textBoxService.Enabled = enabled;
            textBoxTitle.Enabled = enabled;
            textBoxDescription.Enabled = enabled;
            checkBoxForward.Enabled = enabled;
            comboBoxSrcProtocol.Enabled = enabled;
            textBoxSrcPorts.Enabled = enabled;
            textBoxDescription.Enabled = enabled;
            comboBoxDstProtocol.Enabled = enabled;
            textBoxDstPorts.Enabled = enabled;
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
            SetEmptyPortConfig();
            DisplayPortConfigDetails();
            textBoxService.Focus();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            state = State.Edit;
            DisplayPortConfigDetails();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBoxEx.Show(this, string.Format("Do you really want to delete the config {0} ?", current["service_name"]), "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            switch (result)
            {
                case DialogResult.Yes:
                    SynoConfig.Sections.RemoveSection(current["service_name"]);
                    state = State.None;
                    DisplayPortConfig();
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
                    foreach (var section in SynoConfig.Sections)
                    {
                        if (section.SectionName != current["service_name"])
                        {
                            newSc.Sections.AddSection(section.SectionName);
                            var newSection = newSc.Sections[section.SectionName];
                            foreach (var key in SynoConfig.Sections[section.SectionName])
                            {
                                newSection.AddKey(key.KeyName, key.Value);
                            }
                        }
                        else
                        {
                            AddNewSection(ref newSc);
                        }
                    }
                    SynoConfig = newSc;
                }
                else
                {
                    succeed = AddNewSection(ref synoConfig);
                }
            }

            if (succeed)
            {
                state = State.None;
                DisplayPortConfig();
                buttonAdd.Focus();
            }
        }

        private bool AddNewSection(ref IniData newSc)
        {
            if (newSc == null) newSc = new IniData();
            var succeed = newSc.Sections.AddSection(textBoxService.Text);
            if (succeed)
            {
                var newSection = newSc.Sections[textBoxService.Text];
                newSection.AddKey("title", string.Format("\"{0}\"", textBoxTitle.Text));
                newSection.AddKey("desc", string.Format("\"{0}\"", textBoxDescription.Text));

                if (checkBoxForward.Checked)
                    newSection.AddKey("port_forward", "\"yes\"");

                if (!string.IsNullOrEmpty(textBoxSrcPorts.Text))
                    newSection.AddKey("src.ports", string.Format("\"{0}/{1}\"", textBoxSrcPorts.Text, comboBoxSrcProtocol.SelectedItem));

                newSection.AddKey("dst.ports", string.Format("\"{0}/{1}\"", textBoxDstPorts.Text, comboBoxDstProtocol.SelectedItem));
            }
            else
            {
                errorProvider.SetError(textBoxService, string.Format("You may not save this Port Config as another one with the same name '{0}' exists!", textBoxService.Text));
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
                checkBoxPortConfig.Focus();
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

        private void textBoxService_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxService, "");
        }

        private void textBoxService_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (!CheckEmpty(textBoxService, ref e, ""))
                {
                    if (SynoConfig != null)
                    {
                        var newService = Helper.CleanUpText(textBoxService.Text);
                        foreach (var section in SynoConfig.Sections)
                        {
                            if (section.SectionName == newService && section.SectionName != current["service_name"])
                            {
                                e.Cancel = true;
                                textBoxService.Select(0, textBoxService.Text.Length);
                                errorProvider.SetError(textBoxService, "This Service Name is already used");
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

        private bool CheckValidPorts(TextBox textBox, ref CancelEventArgs e)
        {
            if (textBox.Enabled && !string.IsNullOrEmpty(textBox.Text) && !validPorts.IsMatch(textBox.Text))
            {
                e.Cancel = true;
                textBox.Select(0, textBox.Text.Length);
                errorProvider.SetError(textBox, "This field must contain valid ports: 1~65535 separated by ‘,’ and using ‘:’ to represent port range.");
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
        private void CheckUrl(TextBox textBox, ref CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox.Text) && !textBox.Text.StartsWith("/") && !Helper.IsValidUrl(textBox.Text))
            {
                e.Cancel = true;
                textBox.Select(0, textBox.Text.Length);
                errorProvider.SetError(textBox, "You didn't type a well formed http(s) absolute Url.");
            }
        }

        private void textBoxSrcPorts_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxSrcPorts, "");
        }

        private void textBoxSrcPorts_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (!string.IsNullOrEmpty(textBoxSrcPorts.Text))
                {
                    if (string.IsNullOrEmpty(comboBoxSrcProtocol.SelectedItem as string))
                        comboBoxSrcProtocol.SelectedItem = "tcp,udp";

                    CheckValidPorts(textBoxSrcPorts, ref e);
                }
            }
        }

        private void textBoxDstPorts_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxDstPorts, "");
        }

        private void textBoxDstPorts_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                if (!CheckEmpty(textBoxDstPorts, ref e, ""))
                {
                    if (string.IsNullOrEmpty(comboBoxDstProtocol.SelectedItem as string))
                        comboBoxDstProtocol.SelectedItem = "tcp,udp";

                    CheckValidPorts(textBoxDstPorts, ref e);
                }
            }
        }

        public bool PendingChanges()
        {
            var pendingChanges = !(origPortConfig == null && (portConfig == null || (synoConfig == null || synoConfig.Sections.Count == 0)));

            if (pendingChanges)
            {
                pendingChanges = (origPortConfig == null && portConfig != null) || (origPortConfig != null && (portConfig == null || origPortConfig.ToString() != portConfig.ToString()));

                if (!pendingChanges)
                {
                    pendingChanges = (origSynoConfig == null && synoConfig != null) || (origSynoConfig != null && (synoConfig == null || origSynoConfig.ToString() != synoConfig.ToString()));
                }
            }

            return pendingChanges;
        }

        private void buttonAdvanced_Click(object sender, EventArgs e)
        {
            var script = new ScriptInfo(synoConfig.ToString(), "Port Config", help.Url, "Details about script files");
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
                                synoConfig = streamParser.ReadData(streamReader);
                            }
                        }
                    }
                    catch
                    {
                        MessageBoxEx.Show(this, "This Service Configuration can't be parsed.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
                DisplayPortConfig();
            }
        }

        private void textBoxTitle_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                CheckEmpty(textBoxTitle, ref e, "");
            }
        }

        private void textBoxTitle_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxTitle, "");
        }

        private void textBoxDescription_Validating(object sender, CancelEventArgs e)
        {
            if (errorProvider.Tag == null)
            {
                CheckEmpty(textBoxDescription, ref e, "");
            }
        }

        private void textBoxDescription_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxDescription, "");
        }
    }
}

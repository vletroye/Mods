using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZTn.Json.Editor.Forms
{
    public partial class SubitemDefinition : Form
    {
        private string subitemType = "";

        private string subitemKey = null;
        private string subitemDescription = null;
        private string subitemDefaultValue = null;
        private string subitemEmptyValue = null;
        private bool subitemDisabled = false;
        private bool subitemHidden = false;
        private bool subitemPreventMark = false;
        private string subitemWidth = null;
        private string subitemHeight = null;
        private string subitemInvalidValue = null;
        private string subitemRoot = null;
        private string subitemApi = null;
        private string subitemValueField = null;
        private string subitemDisplayField = null;
        private bool subitemValueFieldUnique = false;
        private bool subitemDisplayFieldUnique = false;
        private bool subitemStaticCombo = false;

        private List<NameValue> subitemBaseParams = new List<NameValue>();

        private int comboDynamicIndex = -1;

        public SubitemDefinition(string type)
        {
            InitializeComponent();
            subitemType = type;

            switch (subitemType)
            {
                case "singleselect":
                case "multiselect":
                    comboBoxSelect.Visible = true;
                    comboBoxSelect.SelectedIndex = 1;
                    textBoxDefaultValue.Visible = false;
                    labelDefaultValue.Visible = true;
                    textBoxEmptyValue.Visible = false;
                    labelEmptyValue.Visible = false;
                    checkBoxStatic.Visible = false;

                    tabControlDefinition.TabPages.RemoveAt(1);
                    break;
                case "textfield":
                case "password":
                    comboBoxSelect.Visible = false;
                    textBoxDefaultValue.Visible = true;
                    labelDefaultValue.Visible = true;
                    textBoxEmptyValue.Visible = true;
                    labelEmptyValue.Visible = true;
                    textBoxEmptyValue.ReadOnly = false;
                    checkBoxStatic.Visible = false;

                    tabControlDefinition.TabPages.RemoveAt(1);
                    break;
                case "combobox":
                    comboBoxSelect.Visible = false;
                    textBoxDefaultValue.Visible = false;
                    labelDefaultValue.Visible = false;
                    textBoxEmptyValue.Visible = false;
                    labelEmptyValue.Visible = false;
                    labelDefaultValue.Text = "Editable";
                    checkBoxStatic.Visible = true;

                    listBoxBaseParams.Enabled = true;
                    buttonAddParam.Enabled = true;

                    break;
            }

            this.Text = Helper.GetSubItemType(subitemType) + " definition";

            var helpPath = Path.Combine(Helper.AssemblyDirectory, "HelpSynoWizard.csv");
            List<string[]> rows = File.ReadAllLines(helpPath).Select(x => x.Replace("#", Environment.NewLine).Split('|')).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("Property");
            dt.Columns.Add("Supported");
            dt.Columns.Add("Description");
            dt.Columns.Add("DSM Requirement");
            rows.ForEach(x =>
            {
                dt.Rows.Add(x);
            });
            dataGridViewHelp.Columns[0].DataPropertyName = "Property";
            dataGridViewHelp.Columns[1].DataPropertyName = "Supported";
            dataGridViewHelp.Columns[2].DataPropertyName = "Description";
            dataGridViewHelp.Columns[3].DataPropertyName = "DSM Requirement";
            dataGridViewHelp.DataSource = dt;
            dataGridViewHelp.Dock = DockStyle.Fill;
            dataGridViewHelp.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewHelp.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dataGridViewHelp.ClearSelection();
        }

        public string Key { get { return subitemKey; } }
        public string Description { get { return subitemDescription; } }
        public string DefaultValue { get { return subitemDefaultValue; } }

        public string EmptyValue { get { return subitemKey; } }
        public bool Disable { get { return subitemDisabled; } }
        public bool Hidden { get { return subitemHidden; } }
        public bool PreventMark { get { return subitemPreventMark; } }
        public new string Width { get { return subitemWidth; } }
        public new string Height { get { return subitemHeight; } }
        public string InvalidValue { get { return subitemInvalidValue; } }
        public List<NameValue> BaseParams { get { return subitemBaseParams; } }

        public string Root { get { return subitemRoot; } }
        public string Api { get { return subitemApi; } }
        public string ValueField { get { return subitemValueField; } }
        public string DisplayField { get { return subitemDisplayField; } }
        public bool ValueFieldIsUnique { get { return subitemValueFieldUnique; } }
        public bool DisplayFieldIsUnique { get { return subitemDisplayFieldUnique; } }

        public bool StaticCombo { get { return subitemStaticCombo; } }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            subitemKey = textBoxKey.Text;
            subitemDescription = textBoxDescription.Text;

            switch (subitemType)
            {
                case "singleselect":
                case "multiselect":
                    subitemDefaultValue = comboBoxSelect.Text;
                    break;
                case "textfield":
                case "password":
                    subitemDefaultValue = textBoxDefaultValue.Text;
                    subitemEmptyValue = textBoxEmptyValue.Text;
                    break;
                case "combobox":
                    break;
            }

            subitemDisabled = checkBoxDisabled.Checked;
            subitemHidden = checkBoxHidden.Checked;
            subitemPreventMark = checkBoxPreventMark.Checked;
            subitemWidth = textBoxWidth.Text;
            subitemHeight = textBoxHeight.Text;
            subitemInvalidValue = textBoxInvalid.Text;

            subitemStaticCombo = checkBoxStatic.Checked;

            foreach (var item in listBoxBaseParams.Items)
            {
                subitemBaseParams.Add(item as NameValue);
            }
            subitemRoot = textBoxRoot.Text;
            subitemApi = textBoxApiStore.Text;
            subitemValueField = textBoxDynamicValueField.Text;
            subitemDisplayField = textBoxDynamicDisplayField.Text;
            subitemValueFieldUnique = radioButtonDynamicValueField.Checked;
            subitemDisplayFieldUnique = radioButtonDynamicDisplayField.Checked;

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            subitemKey = null;
            this.Close();
        }

        private void textBoxKey_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxKey, "");
            buttonOk.Enabled = true;
        }

        private void textBoxKey_Validating(object sender, CancelEventArgs e)
        {
            buttonOk.Enabled = true;

            var key = Helper.CleanUpText(textBoxKey.Text);
            if (key != textBoxKey.Text)
            {
                errorProvider.SetError(textBoxKey, "You may not use special characters or blanks.");
                e.Cancel = true;
                buttonOk.Enabled = false;
            }
            else if (string.IsNullOrEmpty(key))
            {
                errorProvider.SetError(textBoxKey, "You may not use an empty Key.");
                textBoxKey.Text = "Enter_A_Value";
                e.Cancel = true;
                buttonOk.Enabled = false;
            }
        }

        private void textBoxDefaultValue_TextChanged(object sender, EventArgs e)
        {
            textBoxEmptyValue.ReadOnly = !string.IsNullOrEmpty(textBoxDefaultValue.Text);
        }

        private void buttonAddParam_Click(object sender, EventArgs e)
        {
            var index = listBoxBaseParams.Items.Add(new NameValue("name", "value"));
            listBoxBaseParams.SelectedIndex = index;
        }

        private void buttonRemoveParam_Click(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxParamName, "");

            if (listBoxBaseParams.SelectedItem != null)
                listBoxBaseParams.Items.RemoveAt(listBoxBaseParams.SelectedIndex);
            if (listBoxBaseParams.Items.Count > 0)
                listBoxBaseParams.SelectedIndex = 0;
            else
            {
                textBoxParamName.Enabled = false;
                textBoxParamValue.Enabled = false;
            }
        }

        private void listBoxBaseParams_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = listBoxBaseParams.SelectedItem as NameValue;
            if (selected != null && comboDynamicIndex != listBoxBaseParams.SelectedIndex)
            {
                textBoxParamName.Text = selected.name;
                textBoxParamValue.Text = selected.value;
                textBoxParamName.Enabled = true;
                textBoxParamValue.Enabled = true;
                textBoxParamName.Focus();
            }
            comboDynamicIndex = listBoxBaseParams.SelectedIndex;
            buttonRemoveParam.Enabled = (comboDynamicIndex >= 0);
        }

        private void textBoxParamName_TextChanged(object sender, EventArgs e)
        {
            var selected = listBoxBaseParams.SelectedItem as NameValue;
            if (selected != null)
            {
                selected.name = textBoxParamName.Text;
                listBoxBaseParams.DisplayMember = "";
                listBoxBaseParams.DisplayMember = "-";
            }
        }

        private void textBoxParamValue_TextChanged(object sender, EventArgs e)
        {
            var selected = listBoxBaseParams.SelectedItem as NameValue;
            if (selected != null)
            {
                selected.value = textBoxParamValue.Text;
                listBoxBaseParams.DisplayMember = "";
                listBoxBaseParams.DisplayMember = "-";
            }
        }

        private void checkBoxStatic_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxStatic.Checked)
            {
                labelBaseParams.Text = "Data:";
                labelRoot.Visible = false;
                textBoxRoot.Visible = false;
                labelApiStore.Visible = false;
                textBoxApiStore.Visible = false;
            }
            else
            {
                labelBaseParams.Text = "Base Params:";
                labelRoot.Visible = true;
                textBoxRoot.Visible = true;
                labelApiStore.Visible = true;
                textBoxApiStore.Visible = true;
            }
        }

        private void textBoxParamName_Validating(object sender, CancelEventArgs e)
        {
            foreach (NameValue item in listBoxBaseParams.Items)
            {
                if (item.name == textBoxParamName.Text && listBoxBaseParams.SelectedItem != item)
                {
                    errorProvider.SetError(textBoxParamName, "This value must be unique.");
                    e.Cancel = true;
                }
            }
        }

        private void textBoxParamName_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxParamName, "");
        }

        private void dataGridViewHelp_Paint(object sender, PaintEventArgs e)
        {
            dataGridViewHelp.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewHelp.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewHelp.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewHelp.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            var rowIndex = 0;
            var height = 0;
            if (dataGridViewHelp.Rows.Count > 0)
                height = dataGridViewHelp.Rows[dataGridViewHelp.Rows.Count - 1].Height;
            foreach (DataGridViewRow row in dataGridViewHelp.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    if (row.Cells[0].Value.ToString() == "Property")
                        row.DefaultCellStyle.Font = new Font(dataGridViewHelp.Font, FontStyle.Bold);
                    var text = row.Cells[2].Value.ToString();
                    if (text == "")
                    {
                        text = row.Cells[0].Value.ToString();
                        row.Height = height * 2;
                        Rectangle r0 = dataGridViewHelp.GetCellDisplayRectangle(0, rowIndex, true);
                        int w0 = dataGridViewHelp.GetCellDisplayRectangle(0, rowIndex, true).Width;
                        int w1 = dataGridViewHelp.GetCellDisplayRectangle(1, rowIndex, true).Width;
                        int w2 = dataGridViewHelp.GetCellDisplayRectangle(2, rowIndex, true).Width;
                        int w3 = dataGridViewHelp.GetCellDisplayRectangle(3, rowIndex, true).Width;
                        r0.X += 1;
                        r0.Y += 1;
                        r0.Width = r0.Width + w1 + w2 + w3 - 4;
                        r0.Height = r0.Height - 2;
                        e.Graphics.FillRectangle(new SolidBrush(Color.White), r0);

                        StringFormat format = new StringFormat();
                        e.Graphics.DrawString(text, dataGridViewHelp.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(Color.Black), r0);
                    }
                }
                rowIndex++;
            }
        }

        private void dataGridViewHelp_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewHelp.ClearSelection();
        }
    }
}

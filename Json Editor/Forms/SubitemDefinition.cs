using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZTn.Json.Editor.Forms
{
    public partial class SubitemDefinition : Form
    {
        private string subitemType = "";

        //Subitems
        private string subitemKey = null;
        private string subitemDescription = null;
        private object subitemDefaultValue = null;
        private string subitemEmptyText = null;
        //private ... validator
        private bool subitemDisabled = false;
        private string subitemHeight = null;
        private string subitemWidth = null;
        private bool subitemHidden = false;
        private string subitemInvalidText = null;
        private bool subitemPreventMark = false;

        //Validator
        private bool subitemAllowBlank = true;
        private string subitemMinLength = null;
        private string subitemMaxLength = null;
        private string subitemvType = null;
        private string subitemRegex = null;
        private string subitemFn = null;

        //Others
        private string subitemBlankText = null;
        private bool subitemGrow = false;
        private string subitemGrowMax = null;
        private string subitemGrowMin = null;
        private bool subitemHtmlEncode = false;
        private string subitemMaxLengthText = null;
        private string subitemMinLengthText = null;

        //ComboBox
        private bool subitemStaticCombo = false; //internal variable to fill a 'store' (static combo) or 'api_store' (dynamic combo)
        private string subitemApi = null;
        private List<NameValue> subitemBaseParams = new List<NameValue>();
        private string subitemRoot = null;
        private bool subitemValueFieldUnique = false; //used for idProperty if the ValueField must be used
        private bool subitemDisplayFieldUnique = false; //used for idProperty if the DisplayField must be used
        private bool subitemAutoSelect = true;
        private string subitemDisplayField = null;
        private string subitemValueField = null;
        private bool subitemEditable = true;
        private bool subitemForceSelection = false;
        //private string subitemlistEmptyText = 'to be filled with subitemEmptyText


        private string helpPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources", "HelpSynoWizard.csv");
        private int selectedItem = -1;

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
                    textBoxEmptyText.Visible = false;
                    labelEmptyText.Visible = false;
                    checkBoxStatic.Visible = false;

                    labelMinLength.Visible = false;
                    textBoxMinLength.Visible = false;
                    textBoxMinLengthText.Visible = false;
                    labelMaxLength.Visible = false;
                    textBoxMaxLength.Visible = false;
                    textBoxMaxLengthText.Visible = false;
                    checkBoxAllowBlank.Visible = false;
                    textBoxBlankText.Visible = false;
                    checkBoxHtmlEncode.Visible = false;
                    comboBoxType.Visible = false;
                    labelType.Visible = false;
                    textBoxRegEx.Visible = false;
                    labelRegEx.Visible = false;

                    checkBoxGrow.Visible = false;

                    labelWidth.Visible = false;
                    textBoxWidth.Visible = false;
                    labelHeight.Visible = false;
                    textBoxHeight.Visible = false;

                    tabControlDefinition.TabPages.RemoveAt(1);
                    break;
                case "textfield":
                case "password":
                    comboBoxSelect.Visible = false;
                    textBoxDefaultValue.Visible = true;
                    labelDefaultValue.Visible = true;
                    textBoxEmptyText.Visible = true;
                    labelEmptyText.Visible = true;
                    textBoxEmptyText.ReadOnly = false;
                    checkBoxStatic.Visible = false;

                    tabControlDefinition.TabPages.RemoveAt(1);
                    break;
                case "combobox":
                    comboBoxSelect.Visible = false;
                    textBoxDefaultValue.Visible = false;
                    labelDefaultValue.Visible = false;
                    textBoxEmptyText.Visible = false;
                    labelEmptyText.Visible = false;
                    labelDefaultValue.Text = "Editable";
                    checkBoxStatic.Visible = true;

                    listBoxBaseParams.Enabled = true;
                    buttonAddParam.Enabled = true;

                    tabControlDefinition.TabPages.RemoveAt(2); // It's not clear if there can be validation on comboBox as "validator" apply only on textfield and password...
                    break;
            }

            this.Text = Helper.GetSubItemType(subitemType) + " definition";

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

        public string Key { get { return subitemKey; } set { subitemKey = value; } }
        public string Description { get { return subitemDescription; } set { subitemDescription = value; } }
        public object DefaultValue { get { return subitemDefaultValue; } set { subitemDefaultValue = value; } }

        public string EmptyText { get { return subitemEmptyText; } set { subitemEmptyText = value; } }
        public bool Disable { get { return subitemDisabled; } set { subitemDisabled = value; } }
        public bool Hidden { get { return subitemHidden; } set { subitemHidden = value; } }
        public bool PreventMark { get { return subitemPreventMark; } set { subitemPreventMark = value; } }
        public new string Width { get { return subitemWidth; } set { subitemWidth = value; } }
        public new string Height { get { return subitemHeight; } set { subitemHeight = value; } }
        public string InvalidText { get { return subitemInvalidText; } set { subitemInvalidText = value; } }
        public List<NameValue> BaseParams { get { return subitemBaseParams; } set { subitemBaseParams = value; } }

        public string Root { get { return subitemRoot; } set { subitemRoot = value; } }
        public string Api { get { return subitemApi; } set { subitemApi = value; } }
        public string ValueField { get { return subitemValueField; } set { subitemValueField = value; } }
        public string DisplayField { get { return subitemDisplayField; } set { subitemDisplayField = value; } }
        public bool ValueFieldIsUnique { get { return subitemValueFieldUnique; } set { subitemValueFieldUnique = value; } }
        public bool DisplayFieldIsUnique { get { return subitemDisplayFieldUnique; } set { subitemDisplayFieldUnique = value; } }

        public bool StaticCombo { get { return subitemStaticCombo; } set { subitemStaticCombo = value; } }
        public bool AutoSelect { get { return subitemAutoSelect; } set { subitemAutoSelect = value; } }
        public bool ForceSelection { get { return subitemForceSelection; } set { subitemForceSelection = value; } }
        public bool Editable { get { return subitemEditable; } set { subitemEditable = value; } }

        public bool AllowBlank { get { return subitemAllowBlank; } set { subitemAllowBlank = value; } }
        public string MinLength { get { return subitemMinLength; } set { subitemMinLength = value; } }
        public string MaxLength { get { return subitemMaxLength; } set { subitemMaxLength = value; } }
        public string vType { get { return subitemvType; } set { subitemvType = value; } }
        public string Regex { get { return subitemRegex; } set { subitemRegex = value; } }
        public string Fn { get { return subitemFn; } set { subitemFn = value; } }
        public string BlankText { get { return subitemBlankText; } set { subitemBlankText = value; } }
        public bool Grow { get { return subitemGrow; } set { subitemGrow = value; } }
        public string GrowMax { get { return subitemGrowMax; } set { subitemGrowMax = value; } }
        public string GrowMin { get { return subitemGrowMin; } set { subitemGrowMin = value; } }
        public bool HtmlEncode { get { return subitemHtmlEncode; } set { subitemHtmlEncode = value; } }
        public string MaxLengthText { get { return subitemMaxLengthText; } set { subitemMaxLengthText = value; } }
        public string MinLengthText { get { return subitemMinLengthText; } set { subitemMinLengthText = value; } }

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
                    subitemEmptyText = textBoxEmptyText.Text;
                    break;
                case "combobox":
                    break;
            }

            subitemDisabled = checkBoxDisabled.Checked;
            subitemHidden = checkBoxHidden.Checked;
            subitemPreventMark = checkBoxPreventMark.Checked;
            subitemWidth = textBoxWidth.Text;
            subitemHeight = textBoxHeight.Text;
            subitemInvalidText = textBoxInvalid.Text;

            subitemStaticCombo = checkBoxStatic.Checked;

            if (subitemBaseParams != null)
            {
                subitemBaseParams.Clear();
                foreach (NameValue item in listBoxBaseParams.Items)
                {
                    var clone = new NameValue(item.value, item.name);
                    subitemBaseParams.Add(clone);
                }
            }
            subitemRoot = textBoxRoot.Text;
            subitemApi = textBoxApiStore.Text;
            subitemValueField = textBoxDynamicValueField.Text;
            subitemDisplayField = textBoxDynamicDisplayField.Text;
            subitemValueFieldUnique = radioButtonDynamicValueField.Checked;
            subitemDisplayFieldUnique = radioButtonDynamicDisplayField.Checked;

            subitemEditable = checkBoxEditable.Checked;
            subitemAutoSelect = checkBoxAutoSelect.Checked;
            subitemForceSelection = CheckBoxForceSelection.Checked;

            subitemAllowBlank = checkBoxAllowBlank.Checked;
            subitemMinLength = textBoxMinLength.Text;
            subitemMaxLength = textBoxMaxLength.Text;
            subitemvType = comboBoxType.Text;
            subitemRegex = textBoxRegEx.Text;
            if (!subitemRegex.StartsWith("/")) subitemRegex = "/" + subitemRegex;
            if (!subitemRegex.Substring(subitemRegex.Length-2).StartsWith("/") && 
                !subitemRegex.EndsWith("/")) subitemRegex = subitemRegex + "/";
            textBoxRegEx.Text = subitemRegex;
            subitemFn = textBoxFn.Text;
            subitemFn = subitemFn.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
            if (!string.IsNullOrEmpty(subitemFn) && !(subitemFn.StartsWith("{") || subitemFn.EndsWith("}")))
            {
                subitemFn = string.Format("{{{0}}}", subitemFn);
            }

            subitemBlankText = textBoxBlankText.Text;
            subitemGrow = checkBoxGrow.Checked;
            if (subitemGrow)
            {
                subitemGrowMin = textBoxGrowMin.Text;
                subitemGrowMax = textBoxGrowMax.Text;
            }
            else
            {
                subitemGrowMin = "";
                subitemGrowMax = "";
            }
            subitemHtmlEncode = checkBoxHtmlEncode.Checked;
            if (!string.IsNullOrEmpty(subitemMaxLength))
                subitemMaxLengthText = textBoxMaxLengthText.Text;
            else
                subitemMaxLengthText = "";
            if (!string.IsNullOrEmpty(subitemMinLength))
                subitemMinLengthText = textBoxMinLengthText.Text;
            else
                subitemMinLengthText = "";

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
            textBoxEmptyText.ReadOnly = !string.IsNullOrEmpty(textBoxDefaultValue.Text);
        }

        private void buttonAddParam_Click(object sender, EventArgs e)
        {
            var index = listBoxBaseParams.Items.Add(new NameValue("value", "name"));
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
            if (selected != null && selectedItem != listBoxBaseParams.SelectedIndex)
            {
                textBoxParamName.Text = selected.name;
                textBoxParamValue.Text = selected.value;
                textBoxParamName.Enabled = true;
                textBoxParamValue.Enabled = true;
                textBoxParamName.Focus();
            }
            selectedItem = listBoxBaseParams.SelectedIndex;
            buttonRemoveParam.Enabled = (selectedItem >= 0);
        }

        private void textBoxParamName_TextChanged(object sender, EventArgs e)
        {
            var selected = listBoxBaseParams.SelectedItem as NameValue;
            if (selected != null && selected.name != textBoxParamName.Text)
            {
                selected.name = textBoxParamName.Text;
                listBoxBaseParams.DisplayMember = "";
                listBoxBaseParams.DisplayMember = "-";
            }
        }

        private void textBoxParamValue_TextChanged(object sender, EventArgs e)
        {
            var selected = listBoxBaseParams.SelectedItem as NameValue;
            if (selected != null && selected.value != textBoxParamValue.Text)
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
            var items = listBoxBaseParams.Items.Cast<Object>().ToArray();
            var count = 0;
            foreach (NameValue item in items)
            {
                if (item.name == textBoxParamName.Text)
                    count++;
            }
            if (count > 1)
            {
                errorProvider.SetError(textBoxParamName, "This value must be unique.");
                e.Cancel = true;
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

        private void SubitemDefinition_Load(object sender, EventArgs e)
        {
            textBoxKey.Text = subitemKey;
            buttonOk.Enabled = (!string.IsNullOrEmpty(subitemKey));
            textBoxDescription.Text = subitemDescription;

            switch (subitemType)
            {
                case "singleselect":
                case "multiselect":
                    if (subitemDefaultValue !=null)
                        comboBoxSelect.SelectedIndex = comboBoxSelect.FindString(subitemDefaultValue.ToString());
                    break;
                case "textfield":
                case "password":
                    textBoxDefaultValue.Text = subitemDefaultValue as string;
                    textBoxEmptyText.Text = subitemEmptyText;
                    break;
                case "combobox":
                    break;
            }

            checkBoxDisabled.Checked = subitemDisabled;
            checkBoxHidden.Checked = subitemHidden;
            checkBoxPreventMark.Checked = subitemPreventMark;
            textBoxWidth.Text = subitemWidth;
            textBoxHeight.Text = subitemHeight;
            textBoxInvalid.Text = subitemInvalidText;

            checkBoxStatic.Checked = subitemStaticCombo;
            checkBoxStatic.Enabled = (string.IsNullOrEmpty(subitemKey));
            if (subitemBaseParams != null)
            {
                foreach (NameValue item in subitemBaseParams)
                {
                    var clone = new NameValue(item.value, item.name);
                    listBoxBaseParams.Items.Add(clone);
                }
            }
            textBoxRoot.Text = subitemRoot;
            textBoxApiStore.Text = subitemApi;
            textBoxDynamicValueField.Text = subitemValueField;
            textBoxDynamicDisplayField.Text = subitemDisplayField;
            radioButtonDynamicValueField.Checked = subitemValueFieldUnique;
            radioButtonDynamicDisplayField.Checked = subitemDisplayFieldUnique;

            checkBoxEditable.Checked = subitemEditable;
            checkBoxAutoSelect.Checked = subitemAutoSelect;
            CheckBoxForceSelection.Checked = subitemForceSelection;

            checkBoxAllowBlank.Checked = subitemAllowBlank;
            textBoxMinLength.Text = subitemMinLength;
            textBoxMaxLength.Text = subitemMaxLength;
            comboBoxType.SelectedIndex = comboBoxType.FindString(subitemvType);
            textBoxRegEx.Text = subitemRegex;
            textBoxFn.Text = subitemFn;

            textBoxBlankText.Text = subitemBlankText;
            checkBoxHtmlEncode.Checked = subitemHtmlEncode;
            checkBoxGrow.Checked = subitemGrow;
            textBoxGrowMin.Text = subitemGrowMin;
            textBoxGrowMax.Text = subitemGrowMax;
            textBoxMaxLengthText.Text = subitemMaxLengthText;
            textBoxMinLengthText.Text = subitemMinLengthText;

            foreach (var control in tabControlDefinition.Controls)
            {
                var tab = control as TabPage;
                if (tab != null)
                {
                    foreach (var item in tab.Controls)
                    {
                        var field = item as Control;
                        if (field != null)
                        {
                            field.MouseEnter += new System.EventHandler(this.OnMouseEnter);
                            field.Enter += new System.EventHandler(this.OnMouseEnter);
                            field.MouseLeave += new System.EventHandler(this.OnMouseLeave);
                        }
                    }
                }
            }
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            var zone = sender as Control;
            if (zone != null)
            {
                var text = toolTipSubitemDefinition.GetToolTip(zone);
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

        private void textBoxLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBoxSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBoxRegEx_Validating(object sender, CancelEventArgs e)
        {
            var pattern = textBoxRegEx.Text;
            if (!string.IsNullOrEmpty(Key))
            {
                try
                {
                    var regEx = new Regex(pattern, RegexOptions.Compiled);
                }
                catch
                {
                    errorProvider.SetError(textBoxRegEx, "Invalid Regular Expression");
                    e.Cancel = true;
                }
            }
        }

        private void textBoxRegEx_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxRegEx, "");
        }

        private void textBoxMinLength_TextChanged(object sender, EventArgs e)
        {
            textBoxMinLengthText.Visible = !string.IsNullOrEmpty(textBoxMinLength.Text);
        }

        private void textBoxMaxLength_TextChanged(object sender, EventArgs e)
        {
            textBoxMaxLengthText.Visible = !string.IsNullOrEmpty(textBoxMaxLength.Text);
        }

        private void checkBoxAllowBlank_CheckedChanged(object sender, EventArgs e)
        {
            textBoxBlankText.Visible = !checkBoxAllowBlank.Checked;
        }

        private void checkBoxGrow_CheckedChanged(object sender, EventArgs e)
        {
            labelGrowMin.Visible = checkBoxGrow.Checked;
            textBoxGrowMin.Visible = checkBoxGrow.Checked;
            labelGrowMax.Visible = checkBoxGrow.Checked;
            textBoxGrowMax.Visible = checkBoxGrow.Checked;
        }
    }
}

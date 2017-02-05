using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private List<NameValue> subitemData = new List<NameValue>();
        private List<NameValue> subitemBaseParams = new List<NameValue>();

        private int comboIndex = -1;
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

                    tabControlDefinition.TabPages.RemoveAt(2);
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

                    tabControlDefinition.TabPages.RemoveAt(2);
                    tabControlDefinition.TabPages.RemoveAt(1);
                    break;
                case "combobox":
                    comboBoxSelect.Visible = false;
                    textBoxDefaultValue.Visible = false;
                    labelDefaultValue.Visible = false;
                    textBoxEmptyValue.Visible = false;
                    labelEmptyValue.Visible = false;
                    labelDefaultValue.Text = "Editable";

                    listBoxData.Enabled = true;
                    listBoxBaseParams.Enabled = true;
                    buttonAddData.Enabled = true;
                    buttonAddParam.Enabled = true;

                    break;
            }

            this.Text = Helper.GetSubItemType(subitemType) + " definition";
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
        public List<NameValue> Data { get { return subitemData; } }
        public List<NameValue> BaseParams { get { return subitemBaseParams; } }

        public string Root { get { return subitemRoot; } }
        public string Api { get { return subitemApi; } }
        public string ValueField { get { return subitemValueField; } }
        public string DisplayField { get { return subitemDisplayField; } }
        public bool ValueFieldIsUnique { get { return subitemValueFieldUnique; } }
        public bool DisplayFieldIsUnique { get { return subitemDisplayFieldUnique; } }

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

            if (checkBoxStatic.Checked)
            {
                foreach (var item in listBoxData.Items)
                {
                    subitemData.Add(item as NameValue);
                }
                subitemValueField = textBoxStaticValueField.Text;
                subitemDisplayField = textBoxStaticDisplayField.Text;
                subitemValueFieldUnique = radioButtonStaticValueField.Checked;
                subitemDisplayFieldUnique = radioButtonStaticDisplayField.Checked;

            }
            if (checkBoxDynamic.Checked)
            {
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
            }

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
            buttonOk.Enabled = false;
            var key = Helper.CleanUpText(textBoxKey.Text);
            if (key != textBoxKey.Text)
            {
                errorProvider.SetError(textBoxKey, "You may not use special characters or blanks.");
                e.Cancel = true;
            }
            else if (string.IsNullOrEmpty(key))
            {
                errorProvider.SetError(textBoxKey, "You may not use an empty Key.");
                textBoxKey.Text = "Enter_A_Value";
                e.Cancel = true;
            }
        }

        private void textBoxDefaultValue_TextChanged(object sender, EventArgs e)
        {
            textBoxEmptyValue.ReadOnly = !string.IsNullOrEmpty(textBoxDefaultValue.Text);
        }

        private void listBoxData_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = listBoxData.SelectedItem as NameValue;
            if (selected != null && comboIndex != listBoxData.SelectedIndex)
            {
                textBoxDataValue.Text = selected.value;
                textBoxDataName.Text = selected.name;
                textBoxDataName.Enabled = true;
                textBoxDataValue.Enabled = true;
                textBoxDataName.Focus();
            }
            comboIndex = listBoxData.SelectedIndex;
            buttonRemoveData.Enabled = (comboIndex >= 0);
        }

        private void buttonAddData_Click(object sender, EventArgs e)
        {
            var index = listBoxData.Items.Add(new NameValue("value", "name"));
            listBoxData.SelectedIndex = index;
            checkBoxStatic.Checked = true;
            checkBoxDynamic.Checked = false;
        }

        private void buttonRemoveData_Click(object sender, EventArgs e)
        {
            if (listBoxData.SelectedItem != null)
                listBoxData.Items.RemoveAt(listBoxData.SelectedIndex);
            if (listBoxData.Items.Count > 0)
                listBoxData.SelectedIndex = 0;
            else
            {
                textBoxDataName.Enabled = false;
                textBoxDataValue.Enabled = false;
            }
        }

        private void textBoxDataValue_TextChanged(object sender, EventArgs e)
        {
            var selected = listBoxData.SelectedItem as NameValue;
            if (selected != null)
            {
                selected.value = textBoxDataValue.Text;
                listBoxData.DisplayMember = "";
                listBoxData.DisplayMember = "-";
            }
        }

        private void textBoxDataDisplay_TextChanged(object sender, EventArgs e)
        {
            var selected = listBoxData.SelectedItem as NameValue;
            if (selected != null)
            {
                selected.name = textBoxDataName.Text;
                listBoxData.DisplayMember = "";
                listBoxData.DisplayMember = "-";
            }
        }

        private void buttonAddParam_Click(object sender, EventArgs e)
        {
            var index = listBoxBaseParams.Items.Add(new NameValue("name", "value"));
            listBoxBaseParams.SelectedIndex = index;
            checkBoxDynamic.Checked = true;
            checkBoxStatic.Checked = false;
        }

        private void buttonRemoveParam_Click(object sender, EventArgs e)
        {
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
            if (checkBoxStatic.Checked) checkBoxDynamic.Checked = false;
        }

        private void checkBoxDynamic_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDynamic.Checked) checkBoxStatic.Checked = false;
        }
    }
}

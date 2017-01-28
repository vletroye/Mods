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
                    textBoxEmptyValue.ReadOnly = true;
                    break;
                case "textfield":
                case "password":
                    comboBoxSelect.Visible = false;
                    textBoxDefaultValue.Visible = true;
                    textBoxEmptyValue.ReadOnly = false;
                    break;
                case "combobox":
                    comboBoxSelect.Visible = false;
                    textBoxDefaultValue.Visible = false;
                    textBoxEmptyValue.ReadOnly = true;
                    break;
            }

            labelTypeDesc.Text = Helper.GetSubItemType(subitemType);
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

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            subitemKey = null;
            this.Close();
        }

        private void textBox_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxKey, "");
        }

        private void textBox_Validating(object sender, CancelEventArgs e)
        {
            var key = Helper.CleanUpText(textBoxKey.Text);
            if (key != textBoxKey.Text)
            {
                errorProvider.SetError(textBoxKey, "You may not use special characters or blanks");
            }
        }
    }
}

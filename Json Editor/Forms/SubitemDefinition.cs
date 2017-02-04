﻿using System;
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
        private List<ComboItem> combolist = new List<ComboItem>();
        private int comboIndex = -1;

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
                    break;
                case "textfield":
                case "password":
                    comboBoxSelect.Visible = false;
                    textBoxDefaultValue.Visible = true;
                    labelDefaultValue.Visible = true;
                    textBoxEmptyValue.Visible = true;
                    labelEmptyValue.Visible = true;
                    textBoxEmptyValue.ReadOnly = false;
                    break;
                case "combobox":
                    comboBoxSelect.Visible = false;
                    textBoxDefaultValue.Visible = false;
                    labelDefaultValue.Visible = false;
                    textBoxEmptyValue.Visible = false;
                    labelEmptyValue.Visible = false;

                    listBoxComboValues.Enabled = true;
                    buttonAdd.Enabled = true;

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
        public List<ComboItem> ComboItems { get { return combolist; } }


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

            foreach (var item in listBoxComboValues.Items)
            {
                combolist.Add(item as ComboItem);
            }

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
            buttonOk.Enabled = true;
        }

        private void textBox_Validating(object sender, CancelEventArgs e)
        {
            buttonOk.Enabled = false;
            var key = Helper.CleanUpText(textBoxKey.Text);
            if (key != textBoxKey.Text)
            {
                errorProvider.SetError(textBoxKey, "You may not use special characters or blanks.");
                e.Cancel = true;
            }
            else if (string.IsNullOrEmpty( key ))
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

        private void listBoxComboValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = listBoxComboValues.SelectedItem as ComboItem;
            if (selected != null && comboIndex != listBoxComboValues.SelectedIndex)
            {
                textBoxValue.Text = selected.value;
                textBoxDisplay.Text = selected.display;
                textBoxDisplay.Enabled = true;
                textBoxValue.Enabled = true;
                textBoxValue.Focus();
            }
            comboIndex = listBoxComboValues.SelectedIndex;
            buttonRemove.Enabled = (comboIndex >= 0);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var index = listBoxComboValues.Items.Add(new ComboItem("value", "name"));
            listBoxComboValues.SelectedIndex = index;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxComboValues.SelectedItem != null)
                listBoxComboValues.Items.RemoveAt(listBoxComboValues.SelectedIndex);
            if (listBoxComboValues.Items.Count > 0)
                listBoxComboValues.SelectedIndex = 0;
            else
            {
                textBoxDisplay.Enabled = false;
                textBoxValue.Enabled = false;
            }
        }

        private void textBoxValue_TextChanged(object sender, EventArgs e)
        {
            var selected = listBoxComboValues.SelectedItem as ComboItem;
            if (selected != null)
            {
                selected.value = textBoxValue.Text;
                listBoxComboValues.DisplayMember = "";
                listBoxComboValues.DisplayMember = "-";
            }
        }

        private void textBoxDisplay_TextChanged(object sender, EventArgs e)
        {
            var selected = listBoxComboValues.SelectedItem as ComboItem;
            if (selected != null)
            {
                selected.display = textBoxDisplay.Text;
                listBoxComboValues.DisplayMember = "";
                listBoxComboValues.DisplayMember = "-";
            }
        }
    }
}

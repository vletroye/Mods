namespace ZTn.Json.Editor.Forms
{
    partial class SubitemDefinition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelKey = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBoxDefaultValue = new System.Windows.Forms.TextBox();
            this.labelDefaultValue = new System.Windows.Forms.Label();
            this.comboBoxSelect = new System.Windows.Forms.ComboBox();
            this.textBoxEmptyValue = new System.Windows.Forms.TextBox();
            this.labelEmptyValue = new System.Windows.Forms.Label();
            this.checkBoxDisabled = new System.Windows.Forms.CheckBox();
            this.checkBoxPreventMark = new System.Windows.Forms.CheckBox();
            this.checkBoxHidden = new System.Windows.Forms.CheckBox();
            this.textBoxInvalid = new System.Windows.Forms.TextBox();
            this.labelInvalidText = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listBoxComboValues = new System.Windows.Forms.ListBox();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.textBoxDisplay = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Location = new System.Drawing.Point(55, 9);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(28, 13);
            this.labelKey.TabIndex = 0;
            this.labelKey.Text = "Key:";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(12, 218);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 16;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(604, 218);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(20, 36);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(63, 13);
            this.labelDescription.TabIndex = 4;
            this.labelDescription.Text = "Description:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(86, 33);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(319, 20);
            this.textBoxDescription.TabIndex = 1;
            // 
            // textBoxKey
            // 
            this.textBoxKey.Location = new System.Drawing.Point(86, 6);
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(168, 20);
            this.textBoxKey.TabIndex = 0;
            this.textBoxKey.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            this.textBoxKey.Validated += new System.EventHandler(this.textBox_Validated);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // textBoxDefaultValue
            // 
            this.textBoxDefaultValue.Location = new System.Drawing.Point(86, 59);
            this.textBoxDefaultValue.Name = "textBoxDefaultValue";
            this.textBoxDefaultValue.Size = new System.Drawing.Size(319, 20);
            this.textBoxDefaultValue.TabIndex = 3;
            this.textBoxDefaultValue.TextChanged += new System.EventHandler(this.textBoxDefaultValue_TextChanged);
            // 
            // labelDefaultValue
            // 
            this.labelDefaultValue.AutoSize = true;
            this.labelDefaultValue.Location = new System.Drawing.Point(9, 62);
            this.labelDefaultValue.Name = "labelDefaultValue";
            this.labelDefaultValue.Size = new System.Drawing.Size(74, 13);
            this.labelDefaultValue.TabIndex = 8;
            this.labelDefaultValue.Text = "Default Value:";
            // 
            // comboBoxSelect
            // 
            this.comboBoxSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelect.FormattingEnabled = true;
            this.comboBoxSelect.Items.AddRange(new object[] {
            "true",
            "false"});
            this.comboBoxSelect.Location = new System.Drawing.Point(86, 58);
            this.comboBoxSelect.Name = "comboBoxSelect";
            this.comboBoxSelect.Size = new System.Drawing.Size(57, 21);
            this.comboBoxSelect.TabIndex = 2;
            // 
            // textBoxEmptyValue
            // 
            this.textBoxEmptyValue.Location = new System.Drawing.Point(86, 85);
            this.textBoxEmptyValue.Name = "textBoxEmptyValue";
            this.textBoxEmptyValue.Size = new System.Drawing.Size(319, 20);
            this.textBoxEmptyValue.TabIndex = 4;
            // 
            // labelEmptyValue
            // 
            this.labelEmptyValue.AutoSize = true;
            this.labelEmptyValue.Location = new System.Drawing.Point(14, 88);
            this.labelEmptyValue.Name = "labelEmptyValue";
            this.labelEmptyValue.Size = new System.Drawing.Size(69, 13);
            this.labelEmptyValue.TabIndex = 11;
            this.labelEmptyValue.Text = "Empty Value:";
            // 
            // checkBoxDisabled
            // 
            this.checkBoxDisabled.AutoSize = true;
            this.checkBoxDisabled.Location = new System.Drawing.Point(86, 120);
            this.checkBoxDisabled.Name = "checkBoxDisabled";
            this.checkBoxDisabled.Size = new System.Drawing.Size(67, 17);
            this.checkBoxDisabled.TabIndex = 5;
            this.checkBoxDisabled.Text = "Disabled";
            this.checkBoxDisabled.UseVisualStyleBackColor = true;
            // 
            // checkBoxPreventMark
            // 
            this.checkBoxPreventMark.AutoSize = true;
            this.checkBoxPreventMark.Location = new System.Drawing.Point(86, 143);
            this.checkBoxPreventMark.Name = "checkBoxPreventMark";
            this.checkBoxPreventMark.Size = new System.Drawing.Size(90, 17);
            this.checkBoxPreventMark.TabIndex = 6;
            this.checkBoxPreventMark.Text = "Prevent Mark";
            this.checkBoxPreventMark.UseVisualStyleBackColor = true;
            // 
            // checkBoxHidden
            // 
            this.checkBoxHidden.AutoSize = true;
            this.checkBoxHidden.Location = new System.Drawing.Point(86, 166);
            this.checkBoxHidden.Name = "checkBoxHidden";
            this.checkBoxHidden.Size = new System.Drawing.Size(60, 17);
            this.checkBoxHidden.TabIndex = 7;
            this.checkBoxHidden.Text = "Hidden";
            this.checkBoxHidden.UseVisualStyleBackColor = true;
            // 
            // textBoxInvalid
            // 
            this.textBoxInvalid.Location = new System.Drawing.Point(86, 189);
            this.textBoxInvalid.Name = "textBoxInvalid";
            this.textBoxInvalid.ReadOnly = true;
            this.textBoxInvalid.Size = new System.Drawing.Size(319, 20);
            this.textBoxInvalid.TabIndex = 10;
            // 
            // labelInvalidText
            // 
            this.labelInvalidText.AutoSize = true;
            this.labelInvalidText.Location = new System.Drawing.Point(18, 192);
            this.labelInvalidText.Name = "labelInvalidText";
            this.labelInvalidText.Size = new System.Drawing.Size(65, 13);
            this.labelInvalidText.TabIndex = 16;
            this.labelInvalidText.Text = "Invalid Text:";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(354, 117);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.ReadOnly = true;
            this.textBoxWidth.Size = new System.Drawing.Size(50, 20);
            this.textBoxWidth.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(313, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Width:";
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(354, 143);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.ReadOnly = true;
            this.textBoxHeight.Size = new System.Drawing.Size(50, 20);
            this.textBoxHeight.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(310, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Height:";
            // 
            // listBoxComboValues
            // 
            this.listBoxComboValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxComboValues.Enabled = false;
            this.listBoxComboValues.FormattingEnabled = true;
            this.listBoxComboValues.Location = new System.Drawing.Point(421, 36);
            this.listBoxComboValues.Name = "listBoxComboValues";
            this.listBoxComboValues.Size = new System.Drawing.Size(256, 147);
            this.listBoxComboValues.TabIndex = 11;
            this.listBoxComboValues.SelectedIndexChanged += new System.EventHandler(this.listBoxComboValues_SelectedIndexChanged);
            // 
            // textBoxValue
            // 
            this.textBoxValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxValue.Enabled = false;
            this.textBoxValue.Location = new System.Drawing.Point(487, 189);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(64, 20);
            this.textBoxValue.TabIndex = 12;
            this.textBoxValue.TextChanged += new System.EventHandler(this.textBoxValue_TextChanged);
            // 
            // textBoxDisplay
            // 
            this.textBoxDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxDisplay.Enabled = false;
            this.textBoxDisplay.Location = new System.Drawing.Point(557, 190);
            this.textBoxDisplay.Name = "textBoxDisplay";
            this.textBoxDisplay.Size = new System.Drawing.Size(120, 20);
            this.textBoxDisplay.TabIndex = 13;
            this.textBoxDisplay.TextChanged += new System.EventHandler(this.textBoxDisplay_TextChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Enabled = false;
            this.buttonAdd.Location = new System.Drawing.Point(421, 188);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(25, 23);
            this.buttonAdd.TabIndex = 14;
            this.buttonAdd.Text = "+";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemove.Enabled = false;
            this.buttonRemove.Location = new System.Drawing.Point(452, 188);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(25, 23);
            this.buttonRemove.TabIndex = 15;
            this.buttonRemove.Text = "-";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // SubitemDefinition
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(690, 253);
            this.ControlBox = false;
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textBoxDisplay);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.listBoxComboValues);
            this.Controls.Add(this.textBoxHeight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxInvalid);
            this.Controls.Add(this.labelInvalidText);
            this.Controls.Add(this.checkBoxHidden);
            this.Controls.Add(this.checkBoxPreventMark);
            this.Controls.Add(this.checkBoxDisabled);
            this.Controls.Add(this.textBoxEmptyValue);
            this.Controls.Add(this.labelEmptyValue);
            this.Controls.Add(this.comboBoxSelect);
            this.Controls.Add(this.textBoxDefaultValue);
            this.Controls.Add(this.labelDefaultValue);
            this.Controls.Add(this.textBoxKey);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(706, 292);
            this.MinimizeBox = false;
            this.Name = "SubitemDefinition";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Subitem Definition";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox textBoxDefaultValue;
        private System.Windows.Forms.Label labelDefaultValue;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxInvalid;
        private System.Windows.Forms.Label labelInvalidText;
        private System.Windows.Forms.CheckBox checkBoxHidden;
        private System.Windows.Forms.CheckBox checkBoxPreventMark;
        private System.Windows.Forms.CheckBox checkBoxDisabled;
        private System.Windows.Forms.TextBox textBoxEmptyValue;
        private System.Windows.Forms.Label labelEmptyValue;
        private System.Windows.Forms.ComboBox comboBoxSelect;
        private System.Windows.Forms.ListBox listBoxComboValues;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBoxDisplay;
        private System.Windows.Forms.TextBox textBoxValue;
    }
}
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControlDefinition = new System.Windows.Forms.TabControl();
            this.tabPageDetails = new System.Windows.Forms.TabPage();
            this.comboBoxSelect = new System.Windows.Forms.ComboBox();
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.labelKey = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelDefaultValue = new System.Windows.Forms.Label();
            this.textBoxDefaultValue = new System.Windows.Forms.TextBox();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelEmptyValue = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxEmptyValue = new System.Windows.Forms.TextBox();
            this.labelWidth = new System.Windows.Forms.Label();
            this.checkBoxDisabled = new System.Windows.Forms.CheckBox();
            this.textBoxInvalid = new System.Windows.Forms.TextBox();
            this.checkBoxPreventMark = new System.Windows.Forms.CheckBox();
            this.labelInvalidText = new System.Windows.Forms.Label();
            this.checkBoxHidden = new System.Windows.Forms.CheckBox();
            this.tabPageStaticCombo = new System.Windows.Forms.TabPage();
            this.labelData = new System.Windows.Forms.Label();
            this.radioButtonStaticValueField = new System.Windows.Forms.RadioButton();
            this.radioButtonStaticDisplayField = new System.Windows.Forms.RadioButton();
            this.labelStaticValueField = new System.Windows.Forms.Label();
            this.textBoxStaticValueField = new System.Windows.Forms.TextBox();
            this.labelStaticDisplayField = new System.Windows.Forms.Label();
            this.textBoxStaticDisplayField = new System.Windows.Forms.TextBox();
            this.checkBoxStatic = new System.Windows.Forms.CheckBox();
            this.listBoxData = new System.Windows.Forms.ListBox();
            this.buttonRemoveData = new System.Windows.Forms.Button();
            this.textBoxDataValue = new System.Windows.Forms.TextBox();
            this.buttonAddData = new System.Windows.Forms.Button();
            this.textBoxDataName = new System.Windows.Forms.TextBox();
            this.tabPageDynamicCombo = new System.Windows.Forms.TabPage();
            this.radioButtonDynamicValueField = new System.Windows.Forms.RadioButton();
            this.radioButtonDynamicDisplayField = new System.Windows.Forms.RadioButton();
            this.labelRoot = new System.Windows.Forms.Label();
            this.textBoxRoot = new System.Windows.Forms.TextBox();
            this.labelBaseParams = new System.Windows.Forms.Label();
            this.listBoxBaseParams = new System.Windows.Forms.ListBox();
            this.buttonRemoveParam = new System.Windows.Forms.Button();
            this.textBoxParamName = new System.Windows.Forms.TextBox();
            this.buttonAddParam = new System.Windows.Forms.Button();
            this.textBoxParamValue = new System.Windows.Forms.TextBox();
            this.labelApiStore = new System.Windows.Forms.Label();
            this.textBoxApiStore = new System.Windows.Forms.TextBox();
            this.labelDynamicValueField = new System.Windows.Forms.Label();
            this.textBoxDynamicValueField = new System.Windows.Forms.TextBox();
            this.labelDynamicDisplayField = new System.Windows.Forms.Label();
            this.textBoxDynamicDisplayField = new System.Windows.Forms.TextBox();
            this.checkBoxDynamic = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.tabControlDefinition.SuspendLayout();
            this.tabPageDetails.SuspendLayout();
            this.tabPageStaticCombo.SuspendLayout();
            this.tabPageDynamicCombo.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(12, 292);
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
            this.buttonCancel.Location = new System.Drawing.Point(359, 292);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // tabControlDefinition
            // 
            this.tabControlDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlDefinition.Controls.Add(this.tabPageDetails);
            this.tabControlDefinition.Controls.Add(this.tabPageStaticCombo);
            this.tabControlDefinition.Controls.Add(this.tabPageDynamicCombo);
            this.tabControlDefinition.Location = new System.Drawing.Point(12, 12);
            this.tabControlDefinition.Name = "tabControlDefinition";
            this.tabControlDefinition.SelectedIndex = 0;
            this.tabControlDefinition.Size = new System.Drawing.Size(421, 260);
            this.tabControlDefinition.TabIndex = 0;
            // 
            // tabPageDetails
            // 
            this.tabPageDetails.Controls.Add(this.comboBoxSelect);
            this.tabPageDetails.Controls.Add(this.textBoxKey);
            this.tabPageDetails.Controls.Add(this.labelKey);
            this.tabPageDetails.Controls.Add(this.labelDescription);
            this.tabPageDetails.Controls.Add(this.textBoxDescription);
            this.tabPageDetails.Controls.Add(this.labelDefaultValue);
            this.tabPageDetails.Controls.Add(this.textBoxDefaultValue);
            this.tabPageDetails.Controls.Add(this.textBoxHeight);
            this.tabPageDetails.Controls.Add(this.labelHeight);
            this.tabPageDetails.Controls.Add(this.labelEmptyValue);
            this.tabPageDetails.Controls.Add(this.textBoxWidth);
            this.tabPageDetails.Controls.Add(this.textBoxEmptyValue);
            this.tabPageDetails.Controls.Add(this.labelWidth);
            this.tabPageDetails.Controls.Add(this.checkBoxDisabled);
            this.tabPageDetails.Controls.Add(this.textBoxInvalid);
            this.tabPageDetails.Controls.Add(this.checkBoxPreventMark);
            this.tabPageDetails.Controls.Add(this.labelInvalidText);
            this.tabPageDetails.Controls.Add(this.checkBoxHidden);
            this.tabPageDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageDetails.Name = "tabPageDetails";
            this.tabPageDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDetails.Size = new System.Drawing.Size(413, 234);
            this.tabPageDetails.TabIndex = 0;
            this.tabPageDetails.Text = "Main Settings";
            this.tabPageDetails.UseVisualStyleBackColor = true;
            // 
            // comboBoxSelect
            // 
            this.comboBoxSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelect.FormattingEnabled = true;
            this.comboBoxSelect.Items.AddRange(new object[] {
            "true",
            "false"});
            this.comboBoxSelect.Location = new System.Drawing.Point(82, 58);
            this.comboBoxSelect.Name = "comboBoxSelect";
            this.comboBoxSelect.Size = new System.Drawing.Size(57, 21);
            this.comboBoxSelect.TabIndex = 2;
            // 
            // textBoxKey
            // 
            this.textBoxKey.Location = new System.Drawing.Point(82, 6);
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(168, 20);
            this.textBoxKey.TabIndex = 0;
            this.textBoxKey.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxKey_Validating);
            this.textBoxKey.Validated += new System.EventHandler(this.textBoxKey_Validated);
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Location = new System.Drawing.Point(51, 9);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(28, 13);
            this.labelKey.TabIndex = 0;
            this.labelKey.Text = "Key:";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(16, 36);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(63, 13);
            this.labelDescription.TabIndex = 4;
            this.labelDescription.Text = "Description:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(82, 33);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(319, 20);
            this.textBoxDescription.TabIndex = 1;
            // 
            // labelDefaultValue
            // 
            this.labelDefaultValue.AutoSize = true;
            this.labelDefaultValue.Location = new System.Drawing.Point(5, 62);
            this.labelDefaultValue.Name = "labelDefaultValue";
            this.labelDefaultValue.Size = new System.Drawing.Size(74, 13);
            this.labelDefaultValue.TabIndex = 8;
            this.labelDefaultValue.Text = "Default Value:";
            // 
            // textBoxDefaultValue
            // 
            this.textBoxDefaultValue.Location = new System.Drawing.Point(82, 59);
            this.textBoxDefaultValue.Name = "textBoxDefaultValue";
            this.textBoxDefaultValue.Size = new System.Drawing.Size(319, 20);
            this.textBoxDefaultValue.TabIndex = 3;
            this.textBoxDefaultValue.TextChanged += new System.EventHandler(this.textBoxDefaultValue_TextChanged);
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(350, 143);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.ReadOnly = true;
            this.textBoxHeight.Size = new System.Drawing.Size(50, 20);
            this.textBoxHeight.TabIndex = 9;
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(306, 147);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(41, 13);
            this.labelHeight.TabIndex = 20;
            this.labelHeight.Text = "Height:";
            // 
            // labelEmptyValue
            // 
            this.labelEmptyValue.AutoSize = true;
            this.labelEmptyValue.Location = new System.Drawing.Point(10, 88);
            this.labelEmptyValue.Name = "labelEmptyValue";
            this.labelEmptyValue.Size = new System.Drawing.Size(69, 13);
            this.labelEmptyValue.TabIndex = 11;
            this.labelEmptyValue.Text = "Empty Value:";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(350, 117);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.ReadOnly = true;
            this.textBoxWidth.Size = new System.Drawing.Size(50, 20);
            this.textBoxWidth.TabIndex = 8;
            // 
            // textBoxEmptyValue
            // 
            this.textBoxEmptyValue.Location = new System.Drawing.Point(82, 85);
            this.textBoxEmptyValue.Name = "textBoxEmptyValue";
            this.textBoxEmptyValue.Size = new System.Drawing.Size(319, 20);
            this.textBoxEmptyValue.TabIndex = 4;
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(309, 121);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(38, 13);
            this.labelWidth.TabIndex = 18;
            this.labelWidth.Text = "Width:";
            // 
            // checkBoxDisabled
            // 
            this.checkBoxDisabled.AutoSize = true;
            this.checkBoxDisabled.Location = new System.Drawing.Point(82, 120);
            this.checkBoxDisabled.Name = "checkBoxDisabled";
            this.checkBoxDisabled.Size = new System.Drawing.Size(67, 17);
            this.checkBoxDisabled.TabIndex = 5;
            this.checkBoxDisabled.Text = "Disabled";
            this.checkBoxDisabled.UseVisualStyleBackColor = true;
            // 
            // textBoxInvalid
            // 
            this.textBoxInvalid.Location = new System.Drawing.Point(82, 189);
            this.textBoxInvalid.Name = "textBoxInvalid";
            this.textBoxInvalid.ReadOnly = true;
            this.textBoxInvalid.Size = new System.Drawing.Size(319, 20);
            this.textBoxInvalid.TabIndex = 10;
            // 
            // checkBoxPreventMark
            // 
            this.checkBoxPreventMark.AutoSize = true;
            this.checkBoxPreventMark.Location = new System.Drawing.Point(82, 143);
            this.checkBoxPreventMark.Name = "checkBoxPreventMark";
            this.checkBoxPreventMark.Size = new System.Drawing.Size(90, 17);
            this.checkBoxPreventMark.TabIndex = 6;
            this.checkBoxPreventMark.Text = "Prevent Mark";
            this.checkBoxPreventMark.UseVisualStyleBackColor = true;
            // 
            // labelInvalidText
            // 
            this.labelInvalidText.AutoSize = true;
            this.labelInvalidText.Location = new System.Drawing.Point(14, 192);
            this.labelInvalidText.Name = "labelInvalidText";
            this.labelInvalidText.Size = new System.Drawing.Size(65, 13);
            this.labelInvalidText.TabIndex = 16;
            this.labelInvalidText.Text = "Invalid Text:";
            // 
            // checkBoxHidden
            // 
            this.checkBoxHidden.AutoSize = true;
            this.checkBoxHidden.Location = new System.Drawing.Point(82, 166);
            this.checkBoxHidden.Name = "checkBoxHidden";
            this.checkBoxHidden.Size = new System.Drawing.Size(60, 17);
            this.checkBoxHidden.TabIndex = 7;
            this.checkBoxHidden.Text = "Hidden";
            this.checkBoxHidden.UseVisualStyleBackColor = true;
            // 
            // tabPageStaticCombo
            // 
            this.tabPageStaticCombo.Controls.Add(this.labelData);
            this.tabPageStaticCombo.Controls.Add(this.radioButtonStaticValueField);
            this.tabPageStaticCombo.Controls.Add(this.radioButtonStaticDisplayField);
            this.tabPageStaticCombo.Controls.Add(this.labelStaticValueField);
            this.tabPageStaticCombo.Controls.Add(this.textBoxStaticValueField);
            this.tabPageStaticCombo.Controls.Add(this.labelStaticDisplayField);
            this.tabPageStaticCombo.Controls.Add(this.textBoxStaticDisplayField);
            this.tabPageStaticCombo.Controls.Add(this.checkBoxStatic);
            this.tabPageStaticCombo.Controls.Add(this.listBoxData);
            this.tabPageStaticCombo.Controls.Add(this.buttonRemoveData);
            this.tabPageStaticCombo.Controls.Add(this.textBoxDataValue);
            this.tabPageStaticCombo.Controls.Add(this.buttonAddData);
            this.tabPageStaticCombo.Controls.Add(this.textBoxDataName);
            this.tabPageStaticCombo.Location = new System.Drawing.Point(4, 22);
            this.tabPageStaticCombo.Name = "tabPageStaticCombo";
            this.tabPageStaticCombo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStaticCombo.Size = new System.Drawing.Size(413, 234);
            this.tabPageStaticCombo.TabIndex = 1;
            this.tabPageStaticCombo.Text = "Static Combo";
            this.tabPageStaticCombo.UseVisualStyleBackColor = true;
            // 
            // labelData
            // 
            this.labelData.AutoSize = true;
            this.labelData.Location = new System.Drawing.Point(47, 58);
            this.labelData.Name = "labelData";
            this.labelData.Size = new System.Drawing.Size(33, 13);
            this.labelData.TabIndex = 41;
            this.labelData.Text = "Data:";
            // 
            // radioButtonStaticValueField
            // 
            this.radioButtonStaticValueField.AutoSize = true;
            this.radioButtonStaticValueField.Location = new System.Drawing.Point(188, 33);
            this.radioButtonStaticValueField.Name = "radioButtonStaticValueField";
            this.radioButtonStaticValueField.Size = new System.Drawing.Size(69, 17);
            this.radioButtonStaticValueField.TabIndex = 4;
            this.radioButtonStaticValueField.TabStop = true;
            this.radioButtonStaticValueField.Text = "is Unique";
            this.radioButtonStaticValueField.UseVisualStyleBackColor = true;
            // 
            // radioButtonStaticDisplayField
            // 
            this.radioButtonStaticDisplayField.AutoSize = true;
            this.radioButtonStaticDisplayField.Location = new System.Drawing.Point(188, 8);
            this.radioButtonStaticDisplayField.Name = "radioButtonStaticDisplayField";
            this.radioButtonStaticDisplayField.Size = new System.Drawing.Size(69, 17);
            this.radioButtonStaticDisplayField.TabIndex = 3;
            this.radioButtonStaticDisplayField.TabStop = true;
            this.radioButtonStaticDisplayField.Text = "is Unique";
            this.radioButtonStaticDisplayField.UseVisualStyleBackColor = true;
            // 
            // labelStaticValueField
            // 
            this.labelStaticValueField.AutoSize = true;
            this.labelStaticValueField.Location = new System.Drawing.Point(18, 36);
            this.labelStaticValueField.Name = "labelStaticValueField";
            this.labelStaticValueField.Size = new System.Drawing.Size(62, 13);
            this.labelStaticValueField.TabIndex = 38;
            this.labelStaticValueField.Text = "Value Field:";
            // 
            // textBoxStaticValueField
            // 
            this.textBoxStaticValueField.Location = new System.Drawing.Point(82, 32);
            this.textBoxStaticValueField.Name = "textBoxStaticValueField";
            this.textBoxStaticValueField.Size = new System.Drawing.Size(100, 20);
            this.textBoxStaticValueField.TabIndex = 2;
            // 
            // labelStaticDisplayField
            // 
            this.labelStaticDisplayField.AutoSize = true;
            this.labelStaticDisplayField.Location = new System.Drawing.Point(11, 10);
            this.labelStaticDisplayField.Name = "labelStaticDisplayField";
            this.labelStaticDisplayField.Size = new System.Drawing.Size(69, 13);
            this.labelStaticDisplayField.TabIndex = 36;
            this.labelStaticDisplayField.Text = "Display Field:";
            // 
            // textBoxStaticDisplayField
            // 
            this.textBoxStaticDisplayField.Location = new System.Drawing.Point(82, 6);
            this.textBoxStaticDisplayField.Name = "textBoxStaticDisplayField";
            this.textBoxStaticDisplayField.Size = new System.Drawing.Size(100, 20);
            this.textBoxStaticDisplayField.TabIndex = 1;
            // 
            // checkBoxStatic
            // 
            this.checkBoxStatic.AutoSize = true;
            this.checkBoxStatic.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxStatic.Location = new System.Drawing.Point(287, 6);
            this.checkBoxStatic.Name = "checkBoxStatic";
            this.checkBoxStatic.Size = new System.Drawing.Size(120, 17);
            this.checkBoxStatic.TabIndex = 0;
            this.checkBoxStatic.Text = "Use a Static Combo";
            this.checkBoxStatic.UseVisualStyleBackColor = true;
            this.checkBoxStatic.CheckedChanged += new System.EventHandler(this.checkBoxStatic_CheckedChanged);
            // 
            // listBoxData
            // 
            this.listBoxData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxData.Enabled = false;
            this.listBoxData.FormattingEnabled = true;
            this.listBoxData.Location = new System.Drawing.Point(82, 58);
            this.listBoxData.Name = "listBoxData";
            this.listBoxData.Size = new System.Drawing.Size(255, 121);
            this.listBoxData.TabIndex = 5;
            this.listBoxData.SelectedIndexChanged += new System.EventHandler(this.listBoxData_SelectedIndexChanged);
            // 
            // buttonRemoveData
            // 
            this.buttonRemoveData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemoveData.Enabled = false;
            this.buttonRemoveData.Location = new System.Drawing.Point(113, 195);
            this.buttonRemoveData.Name = "buttonRemoveData";
            this.buttonRemoveData.Size = new System.Drawing.Size(25, 20);
            this.buttonRemoveData.TabIndex = 9;
            this.buttonRemoveData.Text = "-";
            this.buttonRemoveData.UseVisualStyleBackColor = true;
            this.buttonRemoveData.Click += new System.EventHandler(this.buttonRemoveData_Click);
            // 
            // textBoxDataValue
            // 
            this.textBoxDataValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxDataValue.Enabled = false;
            this.textBoxDataValue.Location = new System.Drawing.Point(273, 195);
            this.textBoxDataValue.Name = "textBoxDataValue";
            this.textBoxDataValue.Size = new System.Drawing.Size(64, 20);
            this.textBoxDataValue.TabIndex = 7;
            this.textBoxDataValue.TextChanged += new System.EventHandler(this.textBoxDataValue_TextChanged);
            // 
            // buttonAddData
            // 
            this.buttonAddData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddData.Enabled = false;
            this.buttonAddData.Location = new System.Drawing.Point(82, 195);
            this.buttonAddData.Name = "buttonAddData";
            this.buttonAddData.Size = new System.Drawing.Size(25, 20);
            this.buttonAddData.TabIndex = 8;
            this.buttonAddData.Text = "+";
            this.buttonAddData.UseVisualStyleBackColor = true;
            this.buttonAddData.Click += new System.EventHandler(this.buttonAddData_Click);
            // 
            // textBoxDataName
            // 
            this.textBoxDataName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxDataName.Enabled = false;
            this.textBoxDataName.Location = new System.Drawing.Point(144, 195);
            this.textBoxDataName.Name = "textBoxDataName";
            this.textBoxDataName.Size = new System.Drawing.Size(120, 20);
            this.textBoxDataName.TabIndex = 6;
            this.textBoxDataName.TextChanged += new System.EventHandler(this.textBoxDataDisplay_TextChanged);
            // 
            // tabPageDynamicCombo
            // 
            this.tabPageDynamicCombo.Controls.Add(this.radioButtonDynamicValueField);
            this.tabPageDynamicCombo.Controls.Add(this.radioButtonDynamicDisplayField);
            this.tabPageDynamicCombo.Controls.Add(this.labelRoot);
            this.tabPageDynamicCombo.Controls.Add(this.textBoxRoot);
            this.tabPageDynamicCombo.Controls.Add(this.labelBaseParams);
            this.tabPageDynamicCombo.Controls.Add(this.listBoxBaseParams);
            this.tabPageDynamicCombo.Controls.Add(this.buttonRemoveParam);
            this.tabPageDynamicCombo.Controls.Add(this.textBoxParamName);
            this.tabPageDynamicCombo.Controls.Add(this.buttonAddParam);
            this.tabPageDynamicCombo.Controls.Add(this.textBoxParamValue);
            this.tabPageDynamicCombo.Controls.Add(this.labelApiStore);
            this.tabPageDynamicCombo.Controls.Add(this.textBoxApiStore);
            this.tabPageDynamicCombo.Controls.Add(this.labelDynamicValueField);
            this.tabPageDynamicCombo.Controls.Add(this.textBoxDynamicValueField);
            this.tabPageDynamicCombo.Controls.Add(this.labelDynamicDisplayField);
            this.tabPageDynamicCombo.Controls.Add(this.textBoxDynamicDisplayField);
            this.tabPageDynamicCombo.Controls.Add(this.checkBoxDynamic);
            this.tabPageDynamicCombo.Location = new System.Drawing.Point(4, 22);
            this.tabPageDynamicCombo.Name = "tabPageDynamicCombo";
            this.tabPageDynamicCombo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDynamicCombo.Size = new System.Drawing.Size(413, 234);
            this.tabPageDynamicCombo.TabIndex = 2;
            this.tabPageDynamicCombo.Text = "Dynamic Combo";
            this.tabPageDynamicCombo.UseVisualStyleBackColor = true;
            // 
            // radioButtonDynamicValueField
            // 
            this.radioButtonDynamicValueField.AutoSize = true;
            this.radioButtonDynamicValueField.Location = new System.Drawing.Point(188, 33);
            this.radioButtonDynamicValueField.Name = "radioButtonDynamicValueField";
            this.radioButtonDynamicValueField.Size = new System.Drawing.Size(69, 17);
            this.radioButtonDynamicValueField.TabIndex = 4;
            this.radioButtonDynamicValueField.TabStop = true;
            this.radioButtonDynamicValueField.Text = "is Unique";
            this.radioButtonDynamicValueField.UseVisualStyleBackColor = true;
            // 
            // radioButtonDynamicDisplayField
            // 
            this.radioButtonDynamicDisplayField.AutoSize = true;
            this.radioButtonDynamicDisplayField.Location = new System.Drawing.Point(188, 8);
            this.radioButtonDynamicDisplayField.Name = "radioButtonDynamicDisplayField";
            this.radioButtonDynamicDisplayField.Size = new System.Drawing.Size(69, 17);
            this.radioButtonDynamicDisplayField.TabIndex = 3;
            this.radioButtonDynamicDisplayField.TabStop = true;
            this.radioButtonDynamicDisplayField.Text = "is Unique";
            this.radioButtonDynamicDisplayField.UseVisualStyleBackColor = true;
            // 
            // labelRoot
            // 
            this.labelRoot.AutoSize = true;
            this.labelRoot.Location = new System.Drawing.Point(47, 88);
            this.labelRoot.Name = "labelRoot";
            this.labelRoot.Size = new System.Drawing.Size(33, 13);
            this.labelRoot.TabIndex = 33;
            this.labelRoot.Text = "Root:";
            // 
            // textBoxRoot
            // 
            this.textBoxRoot.Location = new System.Drawing.Point(82, 84);
            this.textBoxRoot.Name = "textBoxRoot";
            this.textBoxRoot.Size = new System.Drawing.Size(100, 20);
            this.textBoxRoot.TabIndex = 6;
            // 
            // labelBaseParams
            // 
            this.labelBaseParams.AutoSize = true;
            this.labelBaseParams.Location = new System.Drawing.Point(8, 110);
            this.labelBaseParams.Name = "labelBaseParams";
            this.labelBaseParams.Size = new System.Drawing.Size(72, 13);
            this.labelBaseParams.TabIndex = 31;
            this.labelBaseParams.Text = "Base Params:";
            // 
            // listBoxBaseParams
            // 
            this.listBoxBaseParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxBaseParams.Enabled = false;
            this.listBoxBaseParams.FormattingEnabled = true;
            this.listBoxBaseParams.Location = new System.Drawing.Point(82, 110);
            this.listBoxBaseParams.Name = "listBoxBaseParams";
            this.listBoxBaseParams.Size = new System.Drawing.Size(256, 69);
            this.listBoxBaseParams.TabIndex = 7;
            this.listBoxBaseParams.SelectedIndexChanged += new System.EventHandler(this.listBoxBaseParams_SelectedIndexChanged);
            // 
            // buttonRemoveParam
            // 
            this.buttonRemoveParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemoveParam.Enabled = false;
            this.buttonRemoveParam.Location = new System.Drawing.Point(113, 195);
            this.buttonRemoveParam.Name = "buttonRemoveParam";
            this.buttonRemoveParam.Size = new System.Drawing.Size(25, 20);
            this.buttonRemoveParam.TabIndex = 11;
            this.buttonRemoveParam.Text = "-";
            this.buttonRemoveParam.UseVisualStyleBackColor = true;
            this.buttonRemoveParam.Click += new System.EventHandler(this.buttonRemoveParam_Click);
            // 
            // textBoxParamName
            // 
            this.textBoxParamName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxParamName.Enabled = false;
            this.textBoxParamName.Location = new System.Drawing.Point(144, 195);
            this.textBoxParamName.Name = "textBoxParamName";
            this.textBoxParamName.Size = new System.Drawing.Size(120, 20);
            this.textBoxParamName.TabIndex = 8;
            this.textBoxParamName.TextChanged += new System.EventHandler(this.textBoxParamName_TextChanged);
            // 
            // buttonAddParam
            // 
            this.buttonAddParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddParam.Enabled = false;
            this.buttonAddParam.Location = new System.Drawing.Point(82, 195);
            this.buttonAddParam.Name = "buttonAddParam";
            this.buttonAddParam.Size = new System.Drawing.Size(25, 20);
            this.buttonAddParam.TabIndex = 10;
            this.buttonAddParam.Text = "+";
            this.buttonAddParam.UseVisualStyleBackColor = true;
            this.buttonAddParam.Click += new System.EventHandler(this.buttonAddParam_Click);
            // 
            // textBoxParamValue
            // 
            this.textBoxParamValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxParamValue.Enabled = false;
            this.textBoxParamValue.Location = new System.Drawing.Point(273, 195);
            this.textBoxParamValue.Name = "textBoxParamValue";
            this.textBoxParamValue.Size = new System.Drawing.Size(65, 20);
            this.textBoxParamValue.TabIndex = 9;
            this.textBoxParamValue.TextChanged += new System.EventHandler(this.textBoxParamValue_TextChanged);
            // 
            // labelApiStore
            // 
            this.labelApiStore.AutoSize = true;
            this.labelApiStore.Location = new System.Drawing.Point(55, 62);
            this.labelApiStore.Name = "labelApiStore";
            this.labelApiStore.Size = new System.Drawing.Size(25, 13);
            this.labelApiStore.TabIndex = 23;
            this.labelApiStore.Text = "Api:";
            // 
            // textBoxApiStore
            // 
            this.textBoxApiStore.Location = new System.Drawing.Point(82, 58);
            this.textBoxApiStore.Name = "textBoxApiStore";
            this.textBoxApiStore.Size = new System.Drawing.Size(325, 20);
            this.textBoxApiStore.TabIndex = 5;
            // 
            // labelDynamicValueField
            // 
            this.labelDynamicValueField.AutoSize = true;
            this.labelDynamicValueField.Location = new System.Drawing.Point(18, 36);
            this.labelDynamicValueField.Name = "labelDynamicValueField";
            this.labelDynamicValueField.Size = new System.Drawing.Size(62, 13);
            this.labelDynamicValueField.TabIndex = 21;
            this.labelDynamicValueField.Text = "Value Field:";
            // 
            // textBoxDynamicValueField
            // 
            this.textBoxDynamicValueField.Location = new System.Drawing.Point(82, 32);
            this.textBoxDynamicValueField.Name = "textBoxDynamicValueField";
            this.textBoxDynamicValueField.Size = new System.Drawing.Size(100, 20);
            this.textBoxDynamicValueField.TabIndex = 2;
            // 
            // labelDynamicDisplayField
            // 
            this.labelDynamicDisplayField.AutoSize = true;
            this.labelDynamicDisplayField.Location = new System.Drawing.Point(11, 10);
            this.labelDynamicDisplayField.Name = "labelDynamicDisplayField";
            this.labelDynamicDisplayField.Size = new System.Drawing.Size(69, 13);
            this.labelDynamicDisplayField.TabIndex = 19;
            this.labelDynamicDisplayField.Text = "Display Field:";
            // 
            // textBoxDynamicDisplayField
            // 
            this.textBoxDynamicDisplayField.Location = new System.Drawing.Point(82, 6);
            this.textBoxDynamicDisplayField.Name = "textBoxDynamicDisplayField";
            this.textBoxDynamicDisplayField.Size = new System.Drawing.Size(100, 20);
            this.textBoxDynamicDisplayField.TabIndex = 1;
            // 
            // checkBoxDynamic
            // 
            this.checkBoxDynamic.AutoSize = true;
            this.checkBoxDynamic.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxDynamic.Location = new System.Drawing.Point(273, 6);
            this.checkBoxDynamic.Name = "checkBoxDynamic";
            this.checkBoxDynamic.Size = new System.Drawing.Size(134, 17);
            this.checkBoxDynamic.TabIndex = 0;
            this.checkBoxDynamic.Text = "Use a Dynamic Combo";
            this.checkBoxDynamic.UseVisualStyleBackColor = true;
            this.checkBoxDynamic.CheckedChanged += new System.EventHandler(this.checkBoxDynamic_CheckedChanged);
            // 
            // SubitemDefinition
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(446, 327);
            this.ControlBox = false;
            this.Controls.Add(this.tabControlDefinition);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubitemDefinition";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Subitem Definition";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.tabControlDefinition.ResumeLayout(false);
            this.tabPageDetails.ResumeLayout(false);
            this.tabPageDetails.PerformLayout();
            this.tabPageStaticCombo.ResumeLayout(false);
            this.tabPageStaticCombo.PerformLayout();
            this.tabPageDynamicCombo.ResumeLayout(false);
            this.tabPageDynamicCombo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TabControl tabControlDefinition;
        private System.Windows.Forms.TabPage tabPageDetails;
        private System.Windows.Forms.TabPage tabPageStaticCombo;
        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelDefaultValue;
        private System.Windows.Forms.TextBox textBoxDefaultValue;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.ComboBox comboBoxSelect;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelEmptyValue;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxEmptyValue;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.CheckBox checkBoxDisabled;
        private System.Windows.Forms.TextBox textBoxInvalid;
        private System.Windows.Forms.CheckBox checkBoxPreventMark;
        private System.Windows.Forms.Label labelInvalidText;
        private System.Windows.Forms.CheckBox checkBoxHidden;
        private System.Windows.Forms.ListBox listBoxData;
        private System.Windows.Forms.Button buttonRemoveData;
        private System.Windows.Forms.TextBox textBoxDataValue;
        private System.Windows.Forms.Button buttonAddData;
        private System.Windows.Forms.TextBox textBoxDataName;
        private System.Windows.Forms.TabPage tabPageDynamicCombo;
        private System.Windows.Forms.CheckBox checkBoxStatic;
        private System.Windows.Forms.CheckBox checkBoxDynamic;
        private System.Windows.Forms.RadioButton radioButtonDynamicValueField;
        private System.Windows.Forms.RadioButton radioButtonDynamicDisplayField;
        private System.Windows.Forms.Label labelRoot;
        private System.Windows.Forms.TextBox textBoxRoot;
        private System.Windows.Forms.Label labelBaseParams;
        private System.Windows.Forms.ListBox listBoxBaseParams;
        private System.Windows.Forms.Button buttonRemoveParam;
        private System.Windows.Forms.TextBox textBoxParamName;
        private System.Windows.Forms.Button buttonAddParam;
        private System.Windows.Forms.TextBox textBoxParamValue;
        private System.Windows.Forms.Label labelApiStore;
        private System.Windows.Forms.TextBox textBoxApiStore;
        private System.Windows.Forms.Label labelDynamicValueField;
        private System.Windows.Forms.TextBox textBoxDynamicValueField;
        private System.Windows.Forms.Label labelDynamicDisplayField;
        private System.Windows.Forms.TextBox textBoxDynamicDisplayField;
        private System.Windows.Forms.Label labelData;
        private System.Windows.Forms.RadioButton radioButtonStaticValueField;
        private System.Windows.Forms.RadioButton radioButtonStaticDisplayField;
        private System.Windows.Forms.Label labelStaticValueField;
        private System.Windows.Forms.TextBox textBoxStaticValueField;
        private System.Windows.Forms.Label labelStaticDisplayField;
        private System.Windows.Forms.TextBox textBoxStaticDisplayField;
    }
}
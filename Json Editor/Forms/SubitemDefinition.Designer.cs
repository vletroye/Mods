using System.Windows.Forms;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubitemDefinition));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControlDefinition = new System.Windows.Forms.TabControl();
            this.tabPageDetails = new System.Windows.Forms.TabPage();
            this.checkBoxGrow = new System.Windows.Forms.CheckBox();
            this.checkBoxHtmlEncode = new System.Windows.Forms.CheckBox();
            this.textBoxGrowMin = new System.Windows.Forms.TextBox();
            this.labelGrowMin = new System.Windows.Forms.Label();
            this.textBoxGrowMax = new System.Windows.Forms.TextBox();
            this.labelGrowMax = new System.Windows.Forms.Label();
            this.checkBoxStatic = new System.Windows.Forms.CheckBox();
            this.comboBoxSelect = new System.Windows.Forms.ComboBox();
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.labelKey = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelDefaultValue = new System.Windows.Forms.Label();
            this.textBoxDefaultValue = new System.Windows.Forms.TextBox();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelEmptyText = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxEmptyText = new System.Windows.Forms.TextBox();
            this.labelWidth = new System.Windows.Forms.Label();
            this.checkBoxDisabled = new System.Windows.Forms.CheckBox();
            this.checkBoxPreventMark = new System.Windows.Forms.CheckBox();
            this.checkBoxHidden = new System.Windows.Forms.CheckBox();
            this.tabPageDynamicCombo = new System.Windows.Forms.TabPage();
            this.CheckBoxForceSelection = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoSelect = new System.Windows.Forms.CheckBox();
            this.checkBoxEditable = new System.Windows.Forms.CheckBox();
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
            this.tabPageValidator = new System.Windows.Forms.TabPage();
            this.textBoxBlankText = new System.Windows.Forms.TextBox();
            this.textBoxMaxLengthText = new System.Windows.Forms.TextBox();
            this.textBoxMinLengthText = new System.Windows.Forms.TextBox();
            this.textBoxInvalid = new System.Windows.Forms.TextBox();
            this.labelInvalidText = new System.Windows.Forms.Label();
            this.textBoxFn = new System.Windows.Forms.TextBox();
            this.labelFn = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.textBoxMaxLength = new System.Windows.Forms.TextBox();
            this.labelMaxLength = new System.Windows.Forms.Label();
            this.textBoxMinLength = new System.Windows.Forms.TextBox();
            this.labelMinLength = new System.Windows.Forms.Label();
            this.checkBoxAllowBlank = new System.Windows.Forms.CheckBox();
            this.textBoxRegEx = new System.Windows.Forms.TextBox();
            this.labelValidator = new System.Windows.Forms.Label();
            this.tabPageHelp = new System.Windows.Forms.TabPage();
            this.dataGridViewHelp = new System.Windows.Forms.DataGridView();
            this.GridProperty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridSupported = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridDSMRequirement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTipSubitemDefinition = new System.Windows.Forms.ToolTip(this.components);
            this.panelToolTip = new System.Windows.Forms.Panel();
            this.labelToolTip = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.tabControlDefinition.SuspendLayout();
            this.tabPageDetails.SuspendLayout();
            this.tabPageDynamicCombo.SuspendLayout();
            this.tabPageValidator.SuspendLayout();
            this.tabPageHelp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHelp)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(12, 259);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(359, 259);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
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
            this.tabControlDefinition.Controls.Add(this.tabPageDynamicCombo);
            this.tabControlDefinition.Controls.Add(this.tabPageValidator);
            this.tabControlDefinition.Controls.Add(this.tabPageHelp);
            this.tabControlDefinition.Location = new System.Drawing.Point(12, 12);
            this.tabControlDefinition.Name = "tabControlDefinition";
            this.tabControlDefinition.SelectedIndex = 0;
            this.tabControlDefinition.Size = new System.Drawing.Size(426, 244);
            this.tabControlDefinition.TabIndex = 0;
            // 
            // tabPageDetails
            // 
            this.tabPageDetails.Controls.Add(this.checkBoxGrow);
            this.tabPageDetails.Controls.Add(this.checkBoxHtmlEncode);
            this.tabPageDetails.Controls.Add(this.textBoxGrowMin);
            this.tabPageDetails.Controls.Add(this.labelGrowMin);
            this.tabPageDetails.Controls.Add(this.textBoxGrowMax);
            this.tabPageDetails.Controls.Add(this.labelGrowMax);
            this.tabPageDetails.Controls.Add(this.checkBoxStatic);
            this.tabPageDetails.Controls.Add(this.comboBoxSelect);
            this.tabPageDetails.Controls.Add(this.textBoxKey);
            this.tabPageDetails.Controls.Add(this.labelKey);
            this.tabPageDetails.Controls.Add(this.labelDescription);
            this.tabPageDetails.Controls.Add(this.textBoxDescription);
            this.tabPageDetails.Controls.Add(this.labelDefaultValue);
            this.tabPageDetails.Controls.Add(this.textBoxDefaultValue);
            this.tabPageDetails.Controls.Add(this.textBoxHeight);
            this.tabPageDetails.Controls.Add(this.labelHeight);
            this.tabPageDetails.Controls.Add(this.labelEmptyText);
            this.tabPageDetails.Controls.Add(this.textBoxWidth);
            this.tabPageDetails.Controls.Add(this.textBoxEmptyText);
            this.tabPageDetails.Controls.Add(this.labelWidth);
            this.tabPageDetails.Controls.Add(this.checkBoxDisabled);
            this.tabPageDetails.Controls.Add(this.checkBoxPreventMark);
            this.tabPageDetails.Controls.Add(this.checkBoxHidden);
            this.tabPageDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageDetails.Name = "tabPageDetails";
            this.tabPageDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDetails.Size = new System.Drawing.Size(418, 218);
            this.tabPageDetails.TabIndex = 0;
            this.tabPageDetails.Text = "Main Settings";
            this.tabPageDetails.UseVisualStyleBackColor = true;
            // 
            // checkBoxGrow
            // 
            this.checkBoxGrow.AutoSize = true;
            this.checkBoxGrow.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxGrow.Location = new System.Drawing.Point(285, 111);
            this.checkBoxGrow.Name = "checkBoxGrow";
            this.checkBoxGrow.Size = new System.Drawing.Size(79, 17);
            this.checkBoxGrow.TabIndex = 12;
            this.checkBoxGrow.Text = "Auto Grow:";
            this.toolTipSubitemDefinition.SetToolTip(this.checkBoxGrow, "Syno Property: grow\r\ntrue if this field should automatically grow and shrink to i" +
        "ts content.");
            this.checkBoxGrow.UseVisualStyleBackColor = true;
            this.checkBoxGrow.CheckedChanged += new System.EventHandler(this.checkBoxGrow_CheckedChanged);
            // 
            // checkBoxHtmlEncode
            // 
            this.checkBoxHtmlEncode.AutoSize = true;
            this.checkBoxHtmlEncode.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxHtmlEncode.Location = new System.Drawing.Point(124, 111);
            this.checkBoxHtmlEncode.Name = "checkBoxHtmlEncode";
            this.checkBoxHtmlEncode.Size = new System.Drawing.Size(90, 17);
            this.checkBoxHtmlEncode.TabIndex = 9;
            this.checkBoxHtmlEncode.Text = "Html Encode:";
            this.toolTipSubitemDefinition.SetToolTip(this.checkBoxHtmlEncode, "Syno Property: htmlEncode\r\nfalse to skip HTML-encoding the text when rendering it" +
        " (defaults to false).");
            this.checkBoxHtmlEncode.UseVisualStyleBackColor = true;
            // 
            // textBoxGrowMin
            // 
            this.textBoxGrowMin.Location = new System.Drawing.Point(351, 131);
            this.textBoxGrowMin.Name = "textBoxGrowMin";
            this.textBoxGrowMin.Size = new System.Drawing.Size(50, 20);
            this.textBoxGrowMin.TabIndex = 13;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxGrowMin, "Syno Property: growMin\r\nThe minimum height to allow when grow is true.");
            this.textBoxGrowMin.Visible = false;
            this.textBoxGrowMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSize_KeyPress);
            // 
            // labelGrowMin
            // 
            this.labelGrowMin.AutoSize = true;
            this.labelGrowMin.Location = new System.Drawing.Point(294, 135);
            this.labelGrowMin.Name = "labelGrowMin";
            this.labelGrowMin.Size = new System.Drawing.Size(55, 13);
            this.labelGrowMin.TabIndex = 24;
            this.labelGrowMin.Text = "Grow Min:";
            this.labelGrowMin.Visible = false;
            // 
            // textBoxGrowMax
            // 
            this.textBoxGrowMax.Location = new System.Drawing.Point(351, 157);
            this.textBoxGrowMax.Name = "textBoxGrowMax";
            this.textBoxGrowMax.Size = new System.Drawing.Size(50, 20);
            this.textBoxGrowMax.TabIndex = 14;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxGrowMax, "Syno Property: growMax\r\nThe maximum height to allow when grow is true.");
            this.textBoxGrowMax.Visible = false;
            this.textBoxGrowMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSize_KeyPress);
            // 
            // labelGrowMax
            // 
            this.labelGrowMax.AutoSize = true;
            this.labelGrowMax.Location = new System.Drawing.Point(291, 161);
            this.labelGrowMax.Name = "labelGrowMax";
            this.labelGrowMax.Size = new System.Drawing.Size(58, 13);
            this.labelGrowMax.TabIndex = 23;
            this.labelGrowMax.Text = "Grow Max:";
            this.labelGrowMax.Visible = false;
            // 
            // checkBoxStatic
            // 
            this.checkBoxStatic.AutoSize = true;
            this.checkBoxStatic.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxStatic.Location = new System.Drawing.Point(4, 180);
            this.checkBoxStatic.Name = "checkBoxStatic";
            this.checkBoxStatic.Size = new System.Drawing.Size(92, 17);
            this.checkBoxStatic.TabIndex = 8;
            this.checkBoxStatic.Text = "Static Combo:";
            this.toolTipSubitemDefinition.SetToolTip(this.checkBoxStatic, resources.GetString("checkBoxStatic.ToolTip"));
            this.checkBoxStatic.UseVisualStyleBackColor = true;
            this.checkBoxStatic.CheckedChanged += new System.EventHandler(this.checkBoxStatic_CheckedChanged);
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
            this.toolTipSubitemDefinition.SetToolTip(this.comboBoxSelect, "Syno Property: defaultValue [Optional]\r\nTrue/false value to initialize “singlesel" +
        "ect” or “multiselect” component.");
            // 
            // textBoxKey
            // 
            this.textBoxKey.Location = new System.Drawing.Point(82, 6);
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(168, 20);
            this.textBoxKey.TabIndex = 0;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxKey, resources.GetString("textBoxKey.ToolTip"));
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
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxDescription, "Syno Property: desc [Optional]\r\nDescribe a component in the label text.");
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
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxDefaultValue, "Syno Property: defaultValue [Optional]\r\nA string value to initialize “textfield” " +
        "or “password” component.");
            this.textBoxDefaultValue.TextChanged += new System.EventHandler(this.textBoxDefaultValue_TextChanged);
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(200, 157);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(50, 20);
            this.textBoxHeight.TabIndex = 11;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxHeight, "Syno Property: height\r\nThe hieght of this component in pixels.");
            this.textBoxHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSize_KeyPress);
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(156, 161);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(41, 13);
            this.labelHeight.TabIndex = 20;
            this.labelHeight.Text = "Height:";
            // 
            // labelEmptyText
            // 
            this.labelEmptyText.AutoSize = true;
            this.labelEmptyText.Location = new System.Drawing.Point(10, 88);
            this.labelEmptyText.Name = "labelEmptyText";
            this.labelEmptyText.Size = new System.Drawing.Size(69, 13);
            this.labelEmptyText.TabIndex = 11;
            this.labelEmptyText.Text = "Empty Value:";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(200, 131);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(50, 20);
            this.textBoxWidth.TabIndex = 10;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxWidth, "Syno Property: width\r\nThe width of this component in pixels.");
            this.textBoxWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSize_KeyPress);
            // 
            // textBoxEmptyText
            // 
            this.textBoxEmptyText.Location = new System.Drawing.Point(82, 85);
            this.textBoxEmptyText.Name = "textBoxEmptyText";
            this.textBoxEmptyText.Size = new System.Drawing.Size(319, 20);
            this.textBoxEmptyText.TabIndex = 4;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxEmptyText, "Syno Property: emptyText [Optional]\r\nThe prompt text to place into an empty “text" +
        "field” or “password” component to prompt the user how to fill in if defaultVaule" +
        " is not set.");
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(159, 135);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(38, 13);
            this.labelWidth.TabIndex = 18;
            this.labelWidth.Text = "Width:";
            // 
            // checkBoxDisabled
            // 
            this.checkBoxDisabled.AutoSize = true;
            this.checkBoxDisabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxDisabled.Location = new System.Drawing.Point(26, 111);
            this.checkBoxDisabled.Name = "checkBoxDisabled";
            this.checkBoxDisabled.Size = new System.Drawing.Size(70, 17);
            this.checkBoxDisabled.TabIndex = 5;
            this.checkBoxDisabled.Text = "Disabled:";
            this.toolTipSubitemDefinition.SetToolTip(this.checkBoxDisabled, "Syno Property: disabled\r\nTrue to disable the field (defaults to false).");
            this.checkBoxDisabled.UseVisualStyleBackColor = true;
            // 
            // checkBoxPreventMark
            // 
            this.checkBoxPreventMark.AutoSize = true;
            this.checkBoxPreventMark.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxPreventMark.Location = new System.Drawing.Point(3, 134);
            this.checkBoxPreventMark.Name = "checkBoxPreventMark";
            this.checkBoxPreventMark.Size = new System.Drawing.Size(93, 17);
            this.checkBoxPreventMark.TabIndex = 6;
            this.checkBoxPreventMark.Text = "Prevent Mark:";
            this.toolTipSubitemDefinition.SetToolTip(this.checkBoxPreventMark, "Syno Property: preventMark\r\nTrue to disable marking the field invalid. Defaults t" +
        "o false.");
            this.checkBoxPreventMark.UseVisualStyleBackColor = true;
            // 
            // checkBoxHidden
            // 
            this.checkBoxHidden.AutoSize = true;
            this.checkBoxHidden.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxHidden.Location = new System.Drawing.Point(33, 157);
            this.checkBoxHidden.Name = "checkBoxHidden";
            this.checkBoxHidden.Size = new System.Drawing.Size(63, 17);
            this.checkBoxHidden.TabIndex = 7;
            this.checkBoxHidden.Text = "Hidden:";
            this.toolTipSubitemDefinition.SetToolTip(this.checkBoxHidden, "Syno Property: hidden\r\nTrue to hide this component.");
            this.checkBoxHidden.UseVisualStyleBackColor = true;
            // 
            // tabPageDynamicCombo
            // 
            this.tabPageDynamicCombo.Controls.Add(this.CheckBoxForceSelection);
            this.tabPageDynamicCombo.Controls.Add(this.checkBoxAutoSelect);
            this.tabPageDynamicCombo.Controls.Add(this.checkBoxEditable);
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
            this.tabPageDynamicCombo.Location = new System.Drawing.Point(4, 22);
            this.tabPageDynamicCombo.Name = "tabPageDynamicCombo";
            this.tabPageDynamicCombo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDynamicCombo.Size = new System.Drawing.Size(418, 218);
            this.tabPageDynamicCombo.TabIndex = 2;
            this.tabPageDynamicCombo.Text = "Combo Settings";
            this.tabPageDynamicCombo.UseVisualStyleBackColor = true;
            // 
            // CheckBoxForceSelection
            // 
            this.CheckBoxForceSelection.AutoSize = true;
            this.CheckBoxForceSelection.Location = new System.Drawing.Point(307, 39);
            this.CheckBoxForceSelection.Name = "CheckBoxForceSelection";
            this.CheckBoxForceSelection.Size = new System.Drawing.Size(100, 17);
            this.CheckBoxForceSelection.TabIndex = 6;
            this.CheckBoxForceSelection.Text = "Force Selection";
            this.CheckBoxForceSelection.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoSelect
            // 
            this.checkBoxAutoSelect.AutoSize = true;
            this.checkBoxAutoSelect.Location = new System.Drawing.Point(307, 21);
            this.checkBoxAutoSelect.Name = "checkBoxAutoSelect";
            this.checkBoxAutoSelect.Size = new System.Drawing.Size(78, 17);
            this.checkBoxAutoSelect.TabIndex = 5;
            this.checkBoxAutoSelect.Text = "AutoSelect";
            this.checkBoxAutoSelect.UseVisualStyleBackColor = true;
            // 
            // checkBoxEditable
            // 
            this.checkBoxEditable.AutoSize = true;
            this.checkBoxEditable.Location = new System.Drawing.Point(307, 3);
            this.checkBoxEditable.Name = "checkBoxEditable";
            this.checkBoxEditable.Size = new System.Drawing.Size(64, 17);
            this.checkBoxEditable.TabIndex = 4;
            this.checkBoxEditable.Text = "Editable";
            this.checkBoxEditable.UseVisualStyleBackColor = true;
            // 
            // radioButtonDynamicValueField
            // 
            this.radioButtonDynamicValueField.AutoSize = true;
            this.radioButtonDynamicValueField.Location = new System.Drawing.Point(188, 33);
            this.radioButtonDynamicValueField.Name = "radioButtonDynamicValueField";
            this.radioButtonDynamicValueField.Size = new System.Drawing.Size(69, 17);
            this.radioButtonDynamicValueField.TabIndex = 3;
            this.radioButtonDynamicValueField.TabStop = true;
            this.radioButtonDynamicValueField.Text = "is Unique";
            this.toolTipSubitemDefinition.SetToolTip(this.radioButtonDynamicValueField, "Syno Property: idProperty\r\nIdentity of the property within data that contains a u" +
        "nique value.\r\nSelect this to use the Value Field as idProperty.");
            this.radioButtonDynamicValueField.UseVisualStyleBackColor = true;
            // 
            // radioButtonDynamicDisplayField
            // 
            this.radioButtonDynamicDisplayField.AutoSize = true;
            this.radioButtonDynamicDisplayField.Location = new System.Drawing.Point(188, 8);
            this.radioButtonDynamicDisplayField.Name = "radioButtonDynamicDisplayField";
            this.radioButtonDynamicDisplayField.Size = new System.Drawing.Size(69, 17);
            this.radioButtonDynamicDisplayField.TabIndex = 1;
            this.radioButtonDynamicDisplayField.TabStop = true;
            this.radioButtonDynamicDisplayField.Text = "is Unique";
            this.toolTipSubitemDefinition.SetToolTip(this.radioButtonDynamicDisplayField, "Syno Property: idProperty\r\nIdentity of the property within data that contains a u" +
        "nique value.\r\nSelect this to use the Display Field as idProperty.");
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
            this.textBoxRoot.TabIndex = 8;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxRoot, "Syno Property: root\r\nThe name of the property which contains the array of data. D" +
        "efaults to undefined.");
            // 
            // labelBaseParams
            // 
            this.labelBaseParams.AutoSize = true;
            this.labelBaseParams.Location = new System.Drawing.Point(8, 110);
            this.labelBaseParams.Name = "labelBaseParams";
            this.labelBaseParams.Size = new System.Drawing.Size(72, 13);
            this.labelBaseParams.TabIndex = 31;
            this.labelBaseParams.Text = "Base Params:";
            this.labelBaseParams.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // listBoxBaseParams
            // 
            this.listBoxBaseParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxBaseParams.Enabled = false;
            this.listBoxBaseParams.FormattingEnabled = true;
            this.listBoxBaseParams.Location = new System.Drawing.Point(82, 110);
            this.listBoxBaseParams.Name = "listBoxBaseParams";
            this.listBoxBaseParams.Size = new System.Drawing.Size(256, 56);
            this.listBoxBaseParams.TabIndex = 9;
            this.toolTipSubitemDefinition.SetToolTip(this.listBoxBaseParams, resources.GetString("listBoxBaseParams.ToolTip"));
            this.listBoxBaseParams.SelectedIndexChanged += new System.EventHandler(this.listBoxBaseParams_SelectedIndexChanged);
            // 
            // buttonRemoveParam
            // 
            this.buttonRemoveParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemoveParam.Enabled = false;
            this.buttonRemoveParam.Location = new System.Drawing.Point(113, 179);
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
            this.textBoxParamName.Location = new System.Drawing.Point(144, 179);
            this.textBoxParamName.Name = "textBoxParamName";
            this.textBoxParamName.Size = new System.Drawing.Size(120, 20);
            this.textBoxParamName.TabIndex = 12;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxParamName, "Name to be added in the list above.");
            this.textBoxParamName.TextChanged += new System.EventHandler(this.textBoxParamName_TextChanged);
            this.textBoxParamName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxParamName_Validating);
            this.textBoxParamName.Validated += new System.EventHandler(this.textBoxParamName_Validated);
            // 
            // buttonAddParam
            // 
            this.buttonAddParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddParam.Enabled = false;
            this.buttonAddParam.Location = new System.Drawing.Point(82, 179);
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
            this.textBoxParamValue.Location = new System.Drawing.Point(273, 179);
            this.textBoxParamValue.Name = "textBoxParamValue";
            this.textBoxParamValue.Size = new System.Drawing.Size(65, 20);
            this.textBoxParamValue.TabIndex = 13;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxParamValue, resources.GetString("textBoxParamValue.ToolTip"));
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
            this.textBoxApiStore.TabIndex = 7;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxApiStore, "Syno Property: api (in  api_store)\r\nName of the WebAPI to be requested to get a r" +
        "esponse in the data strusture for combobox use.");
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
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxDynamicValueField, "Syno Property: valueField\r\nThe underlying data value name to bind to this combobo" +
        "x.");
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
            this.textBoxDynamicDisplayField.TabIndex = 0;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxDynamicDisplayField, "Syno Property: displayField\r\nThe underlying data field name to bind to this combo" +
        "box.");
            // 
            // tabPageValidator
            // 
            this.tabPageValidator.Controls.Add(this.textBoxBlankText);
            this.tabPageValidator.Controls.Add(this.textBoxMaxLengthText);
            this.tabPageValidator.Controls.Add(this.textBoxMinLengthText);
            this.tabPageValidator.Controls.Add(this.textBoxInvalid);
            this.tabPageValidator.Controls.Add(this.labelInvalidText);
            this.tabPageValidator.Controls.Add(this.textBoxFn);
            this.tabPageValidator.Controls.Add(this.labelFn);
            this.tabPageValidator.Controls.Add(this.comboBoxType);
            this.tabPageValidator.Controls.Add(this.labelType);
            this.tabPageValidator.Controls.Add(this.textBoxMaxLength);
            this.tabPageValidator.Controls.Add(this.labelMaxLength);
            this.tabPageValidator.Controls.Add(this.textBoxMinLength);
            this.tabPageValidator.Controls.Add(this.labelMinLength);
            this.tabPageValidator.Controls.Add(this.checkBoxAllowBlank);
            this.tabPageValidator.Controls.Add(this.textBoxRegEx);
            this.tabPageValidator.Controls.Add(this.labelValidator);
            this.tabPageValidator.Location = new System.Drawing.Point(4, 22);
            this.tabPageValidator.Name = "tabPageValidator";
            this.tabPageValidator.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageValidator.Size = new System.Drawing.Size(418, 218);
            this.tabPageValidator.TabIndex = 4;
            this.tabPageValidator.Text = "Validators";
            this.tabPageValidator.UseVisualStyleBackColor = true;
            // 
            // textBoxBlankText
            // 
            this.textBoxBlankText.Location = new System.Drawing.Point(140, 187);
            this.textBoxBlankText.Name = "textBoxBlankText";
            this.textBoxBlankText.Size = new System.Drawing.Size(263, 20);
            this.textBoxBlankText.TabIndex = 9;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxBlankText, "Syno Property: blankText\r\nThe error text to display if the allowBlank validation " +
        "fails.");
            // 
            // textBoxMaxLengthText
            // 
            this.textBoxMaxLengthText.Location = new System.Drawing.Point(140, 161);
            this.textBoxMaxLengthText.Name = "textBoxMaxLengthText";
            this.textBoxMaxLengthText.Size = new System.Drawing.Size(263, 20);
            this.textBoxMaxLengthText.TabIndex = 7;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxMaxLengthText, "Syno Property: maxLengthText\r\nError text to display if the maximum length validat" +
        "ion using maxLength fails.");
            this.textBoxMaxLengthText.Visible = false;
            // 
            // textBoxMinLengthText
            // 
            this.textBoxMinLengthText.Location = new System.Drawing.Point(140, 134);
            this.textBoxMinLengthText.Name = "textBoxMinLengthText";
            this.textBoxMinLengthText.Size = new System.Drawing.Size(263, 20);
            this.textBoxMinLengthText.TabIndex = 5;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxMinLengthText, "Syno Property: minLengthText\r\nError text to display if the minimum length validat" +
        "ion using minLength fails.");
            this.textBoxMinLengthText.Visible = false;
            // 
            // textBoxInvalid
            // 
            this.textBoxInvalid.Location = new System.Drawing.Point(84, 108);
            this.textBoxInvalid.Name = "textBoxInvalid";
            this.textBoxInvalid.Size = new System.Drawing.Size(319, 20);
            this.textBoxInvalid.TabIndex = 3;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxInvalid, "Syno Property: invalidText\r\nThe error text to use when marking a field invalid an" +
        "d no message is provided.");
            // 
            // labelInvalidText
            // 
            this.labelInvalidText.AutoSize = true;
            this.labelInvalidText.Location = new System.Drawing.Point(16, 111);
            this.labelInvalidText.Name = "labelInvalidText";
            this.labelInvalidText.Size = new System.Drawing.Size(65, 13);
            this.labelInvalidText.TabIndex = 36;
            this.labelInvalidText.Text = "Invalid Text:";
            // 
            // textBoxFn
            // 
            this.textBoxFn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFn.Location = new System.Drawing.Point(84, 59);
            this.textBoxFn.Multiline = true;
            this.textBoxFn.Name = "textBoxFn";
            this.textBoxFn.Size = new System.Drawing.Size(319, 45);
            this.textBoxFn.TabIndex = 2;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxFn, resources.GetString("textBoxFn.ToolTip"));
            // 
            // labelFn
            // 
            this.labelFn.AutoSize = true;
            this.labelFn.Location = new System.Drawing.Point(30, 59);
            this.labelFn.Name = "labelFn";
            this.labelFn.Size = new System.Drawing.Size(51, 13);
            this.labelFn.TabIndex = 34;
            this.labelFn.Text = "Function:";
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "",
            "alpha",
            "alphanum",
            "email",
            "url"});
            this.comboBoxType.Location = new System.Drawing.Point(84, 6);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(57, 21);
            this.comboBoxType.TabIndex = 0;
            this.toolTipSubitemDefinition.SetToolTip(this.comboBoxType, "Syno Property: defaultValue [Optional]\r\nTrue/false value to initialize “singlesel" +
        "ect” or “multiselect” component.");
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(47, 10);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(34, 13);
            this.labelType.TabIndex = 32;
            this.labelType.Text = "Type:";
            // 
            // textBoxMaxLength
            // 
            this.textBoxMaxLength.Location = new System.Drawing.Point(84, 161);
            this.textBoxMaxLength.Name = "textBoxMaxLength";
            this.textBoxMaxLength.Size = new System.Drawing.Size(50, 20);
            this.textBoxMaxLength.TabIndex = 6;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxMaxLength, "Syno Property: maxLength\r\nMaximum length of “textfield” or “password” component.");
            this.textBoxMaxLength.TextChanged += new System.EventHandler(this.textBoxMaxLength_TextChanged);
            this.textBoxMaxLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxLength_KeyPress);
            // 
            // labelMaxLength
            // 
            this.labelMaxLength.AutoSize = true;
            this.labelMaxLength.Location = new System.Drawing.Point(15, 165);
            this.labelMaxLength.Name = "labelMaxLength";
            this.labelMaxLength.Size = new System.Drawing.Size(66, 13);
            this.labelMaxLength.TabIndex = 30;
            this.labelMaxLength.Text = "Max Length:";
            // 
            // textBoxMinLength
            // 
            this.textBoxMinLength.Location = new System.Drawing.Point(84, 134);
            this.textBoxMinLength.Name = "textBoxMinLength";
            this.textBoxMinLength.Size = new System.Drawing.Size(50, 20);
            this.textBoxMinLength.TabIndex = 4;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxMinLength, "Syno Property: minLength\r\nMinimum length of “textfield” or “password” component.");
            this.textBoxMinLength.TextChanged += new System.EventHandler(this.textBoxMinLength_TextChanged);
            this.textBoxMinLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxLength_KeyPress);
            // 
            // labelMinLength
            // 
            this.labelMinLength.AutoSize = true;
            this.labelMinLength.Location = new System.Drawing.Point(18, 138);
            this.labelMinLength.Name = "labelMinLength";
            this.labelMinLength.Size = new System.Drawing.Size(63, 13);
            this.labelMinLength.TabIndex = 29;
            this.labelMinLength.Text = "Min Length:";
            // 
            // checkBoxAllowBlank
            // 
            this.checkBoxAllowBlank.AutoSize = true;
            this.checkBoxAllowBlank.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxAllowBlank.Location = new System.Drawing.Point(17, 187);
            this.checkBoxAllowBlank.Name = "checkBoxAllowBlank";
            this.checkBoxAllowBlank.Size = new System.Drawing.Size(81, 17);
            this.checkBoxAllowBlank.TabIndex = 8;
            this.checkBoxAllowBlank.Text = "Allow Blank";
            this.toolTipSubitemDefinition.SetToolTip(this.checkBoxAllowBlank, "Syno Property: allowBlank\r\nSpecify false to validate that the value\'s length of “" +
        "textfield” or “password” component is > 0");
            this.checkBoxAllowBlank.UseVisualStyleBackColor = true;
            this.checkBoxAllowBlank.CheckedChanged += new System.EventHandler(this.checkBoxAllowBlank_CheckedChanged);
            // 
            // textBoxRegEx
            // 
            this.textBoxRegEx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRegEx.Location = new System.Drawing.Point(84, 33);
            this.textBoxRegEx.Name = "textBoxRegEx";
            this.textBoxRegEx.Size = new System.Drawing.Size(319, 20);
            this.textBoxRegEx.TabIndex = 1;
            this.toolTipSubitemDefinition.SetToolTip(this.textBoxRegEx, "Syno Property: regex\r\nDescribe validation Javascript regular expression.");
            this.textBoxRegEx.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxRegEx_Validating);
            this.textBoxRegEx.Validated += new System.EventHandler(this.textBoxRegEx_Validated);
            // 
            // labelValidator
            // 
            this.labelValidator.AutoSize = true;
            this.labelValidator.Location = new System.Drawing.Point(33, 37);
            this.labelValidator.Name = "labelValidator";
            this.labelValidator.Size = new System.Drawing.Size(48, 13);
            this.labelValidator.TabIndex = 25;
            this.labelValidator.Text = "Reg Ex.:";
            // 
            // tabPageHelp
            // 
            this.tabPageHelp.Controls.Add(this.dataGridViewHelp);
            this.tabPageHelp.Location = new System.Drawing.Point(4, 22);
            this.tabPageHelp.Name = "tabPageHelp";
            this.tabPageHelp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHelp.Size = new System.Drawing.Size(418, 218);
            this.tabPageHelp.TabIndex = 3;
            this.tabPageHelp.Text = "Help";
            this.tabPageHelp.UseVisualStyleBackColor = true;
            // 
            // dataGridViewHelp
            // 
            this.dataGridViewHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewHelp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHelp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GridProperty,
            this.GridSupported,
            this.GridDescription,
            this.GridDSMRequirement});
            this.dataGridViewHelp.Location = new System.Drawing.Point(3, 6);
            this.dataGridViewHelp.MultiSelect = false;
            this.dataGridViewHelp.Name = "dataGridViewHelp";
            this.dataGridViewHelp.ReadOnly = true;
            this.dataGridViewHelp.Size = new System.Drawing.Size(557, 238);
            this.dataGridViewHelp.TabIndex = 18;
            this.dataGridViewHelp.SelectionChanged += new System.EventHandler(this.dataGridViewHelp_SelectionChanged);
            this.dataGridViewHelp.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGridViewHelp_Paint);
            // 
            // GridProperty
            // 
            this.GridProperty.HeaderText = "Property";
            this.GridProperty.Name = "GridProperty";
            this.GridProperty.ReadOnly = true;
            this.GridProperty.Width = 80;
            // 
            // GridSupported
            // 
            this.GridSupported.HeaderText = "Supported";
            this.GridSupported.Name = "GridSupported";
            this.GridSupported.ReadOnly = true;
            this.GridSupported.Width = 70;
            // 
            // GridDescription
            // 
            this.GridDescription.HeaderText = "Description";
            this.GridDescription.Name = "GridDescription";
            this.GridDescription.ReadOnly = true;
            this.GridDescription.Width = 300;
            // 
            // GridDSMRequirement
            // 
            this.GridDSMRequirement.HeaderText = "DSM Requirement";
            this.GridDSMRequirement.Name = "GridDSMRequirement";
            this.GridDSMRequirement.ReadOnly = true;
            // 
            // panelToolTip
            // 
            this.panelToolTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolTip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelToolTip.Location = new System.Drawing.Point(12, 289);
            this.panelToolTip.Name = "panelToolTip";
            this.panelToolTip.Size = new System.Drawing.Size(423, 96);
            this.panelToolTip.TabIndex = 19;
            // 
            // labelToolTip
            // 
            this.labelToolTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelToolTip.Location = new System.Drawing.Point(17, 291);
            this.labelToolTip.Name = "labelToolTip";
            this.labelToolTip.Size = new System.Drawing.Size(413, 92);
            this.labelToolTip.TabIndex = 19;
            this.labelToolTip.Text = "Help";
            // 
            // SubitemDefinition
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(446, 397);
            this.ControlBox = false;
            this.Controls.Add(this.labelToolTip);
            this.Controls.Add(this.panelToolTip);
            this.Controls.Add(this.tabControlDefinition);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(462, 413);
            this.Name = "SubitemDefinition";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Subitem Definition";
            this.Load += new System.EventHandler(this.SubitemDefinition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.tabControlDefinition.ResumeLayout(false);
            this.tabPageDetails.ResumeLayout(false);
            this.tabPageDetails.PerformLayout();
            this.tabPageDynamicCombo.ResumeLayout(false);
            this.tabPageDynamicCombo.PerformLayout();
            this.tabPageValidator.ResumeLayout(false);
            this.tabPageValidator.PerformLayout();
            this.tabPageHelp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHelp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TabControl tabControlDefinition;
        private System.Windows.Forms.TabPage tabPageDetails;
        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelDefaultValue;
        private System.Windows.Forms.TextBox textBoxDefaultValue;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.ComboBox comboBoxSelect;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelEmptyText;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxEmptyText;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.CheckBox checkBoxDisabled;
        private System.Windows.Forms.CheckBox checkBoxPreventMark;
        private System.Windows.Forms.CheckBox checkBoxHidden;
        private System.Windows.Forms.TabPage tabPageDynamicCombo;
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
        private System.Windows.Forms.CheckBox checkBoxStatic;
        private System.Windows.Forms.ToolTip toolTipSubitemDefinition;
        private System.Windows.Forms.TabPage tabPageHelp;
        private System.Windows.Forms.DataGridView dataGridViewHelp;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridProperty;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridSupported;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn GridDSMRequirement;
        private CheckBox CheckBoxForceSelection;
        private CheckBox checkBoxAutoSelect;
        private CheckBox checkBoxEditable;
        private TabPage tabPageValidator;
        private TextBox textBoxFn;
        private Label labelFn;
        private ComboBox comboBoxType;
        private Label labelType;
        private TextBox textBoxMaxLength;
        private Label labelMaxLength;
        private TextBox textBoxMinLength;
        private Label labelMinLength;
        private CheckBox checkBoxAllowBlank;
        private TextBox textBoxRegEx;
        private Label labelValidator;
        private Label labelToolTip;
        private Panel panelToolTip;
        private TextBox textBoxInvalid;
        private Label labelInvalidText;
        private TextBox textBoxGrowMin;
        private Label labelGrowMin;
        private TextBox textBoxGrowMax;
        private Label labelGrowMax;
        private CheckBox checkBoxGrow;
        private CheckBox checkBoxHtmlEncode;
        private TextBox textBoxBlankText;
        private TextBox textBoxMaxLengthText;
        private TextBox textBoxMinLengthText;
    }
}
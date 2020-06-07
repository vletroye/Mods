namespace BeatificaBytes.Synology.Mods
{
    partial class PKG_Conf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PKG_Conf));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.linkLabelHelp = new System.Windows.Forms.LinkLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxPackage = new System.Windows.Forms.TextBox();
            this.textBoxPkgMinVer = new System.Windows.Forms.TextBox();
            this.textBoxPkgMaxVer = new System.Windows.Forms.TextBox();
            this.textBoxDsmMinVer = new System.Windows.Forms.TextBox();
            this.textBoxDsmMaxVer = new System.Windows.Forms.TextBox();
            this.dataGridViewConfig = new System.Windows.Forms.DataGridView();
            this.Package = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkgMinVer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PkgMaxVer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DsmMinVer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DsmMaxver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBoxConfig = new System.Windows.Forms.CheckBox();
            this.panelPortConfig = new System.Windows.Forms.Panel();
            this.buttonAbort = new System.Windows.Forms.Button();
            this.groupBoxTip = new System.Windows.Forms.GroupBox();
            this.labelToolTip = new System.Windows.Forms.Label();
            this.labelDsmMaxVer = new System.Windows.Forms.Label();
            this.labelDsmMinVer = new System.Windows.Forms.Label();
            this.labelPkgMaxVer = new System.Windows.Forms.Label();
            this.labelPkgMinVer = new System.Windows.Forms.Label();
            this.labelPackage = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonAdvanced = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConfig)).BeginInit();
            this.panelPortConfig.SuspendLayout();
            this.groupBoxTip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.Location = new System.Drawing.Point(12, 490);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(520, 490);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // linkLabelHelp
            // 
            this.linkLabelHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelHelp.AutoSize = true;
            this.linkLabelHelp.Location = new System.Drawing.Point(575, 3);
            this.linkLabelHelp.Name = "linkLabelHelp";
            this.linkLabelHelp.Size = new System.Drawing.Size(29, 13);
            this.linkLabelHelp.TabIndex = 1;
            this.linkLabelHelp.TabStop = true;
            this.linkLabelHelp.Text = "Help";
            this.linkLabelHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // textBoxPackage
            // 
            this.textBoxPackage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxPackage.Location = new System.Drawing.Point(95, 186);
            this.textBoxPackage.Name = "textBoxPackage";
            this.textBoxPackage.Size = new System.Drawing.Size(156, 20);
            this.textBoxPackage.TabIndex = 4;
            this.toolTip.SetToolTip(this.textBoxPackage, resources.GetString("textBoxPackage.ToolTip"));
            this.textBoxPackage.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxPackage_Validating);
            this.textBoxPackage.Validated += new System.EventHandler(this.textBoxPackage_Validated);
            // 
            // textBoxPkgMinVer
            // 
            this.textBoxPkgMinVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxPkgMinVer.Location = new System.Drawing.Point(95, 216);
            this.textBoxPkgMinVer.Name = "textBoxPkgMinVer";
            this.textBoxPkgMinVer.Size = new System.Drawing.Size(81, 20);
            this.textBoxPkgMinVer.TabIndex = 5;
            this.toolTip.SetToolTip(this.textBoxPkgMinVer, "Minimum version of dependent package. You must install this dependent package wit" +
        "h this version or newer before installing your package.");
            // 
            // textBoxPkgMaxVer
            // 
            this.textBoxPkgMaxVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxPkgMaxVer.Location = new System.Drawing.Point(95, 246);
            this.textBoxPkgMaxVer.Name = "textBoxPkgMaxVer";
            this.textBoxPkgMaxVer.Size = new System.Drawing.Size(81, 20);
            this.textBoxPkgMaxVer.TabIndex = 7;
            this.toolTip.SetToolTip(this.textBoxPkgMaxVer, "Maximum version of dependent package. You must install this dependent package wit" +
        "h the version or older before installing your package.");
            // 
            // textBoxDsmMinVer
            // 
            this.textBoxDsmMinVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxDsmMinVer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxDsmMinVer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxDsmMinVer.Location = new System.Drawing.Point(95, 276);
            this.textBoxDsmMinVer.Name = "textBoxDsmMinVer";
            this.textBoxDsmMinVer.Size = new System.Drawing.Size(81, 20);
            this.textBoxDsmMinVer.TabIndex = 9;
            this.toolTip.SetToolTip(this.textBoxDsmMinVer, resources.GetString("textBoxDsmMinVer.ToolTip"));
            this.textBoxDsmMinVer.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFirmware_Validating);
            this.textBoxDsmMinVer.Validated += new System.EventHandler(this.textBoxFirmware_Validated);
            // 
            // textBoxDsmMaxVer
            // 
            this.textBoxDsmMaxVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxDsmMaxVer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxDsmMaxVer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxDsmMaxVer.Location = new System.Drawing.Point(95, 306);
            this.textBoxDsmMaxVer.Name = "textBoxDsmMaxVer";
            this.textBoxDsmMaxVer.Size = new System.Drawing.Size(81, 20);
            this.textBoxDsmMaxVer.TabIndex = 11;
            this.toolTip.SetToolTip(this.textBoxDsmMaxVer, resources.GetString("textBoxDsmMaxVer.ToolTip"));
            this.textBoxDsmMaxVer.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFirmware_Validating);
            this.textBoxDsmMaxVer.Validated += new System.EventHandler(this.textBoxFirmware_Validated);
            // 
            // dataGridViewConfig
            // 
            this.dataGridViewConfig.AllowUserToAddRows = false;
            this.dataGridViewConfig.AllowUserToDeleteRows = false;
            this.dataGridViewConfig.AllowUserToResizeRows = false;
            this.dataGridViewConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewConfig.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Package,
            this.PkgMinVer,
            this.PkgMaxVer,
            this.DsmMinVer,
            this.DsmMaxver});
            this.dataGridViewConfig.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewConfig.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewConfig.MultiSelect = false;
            this.dataGridViewConfig.Name = "dataGridViewConfig";
            this.dataGridViewConfig.ReadOnly = true;
            this.dataGridViewConfig.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewConfig.ShowEditingIcon = false;
            this.dataGridViewConfig.Size = new System.Drawing.Size(582, 139);
            this.dataGridViewConfig.StandardTab = true;
            this.dataGridViewConfig.TabIndex = 0;
            this.dataGridViewConfig.SelectionChanged += new System.EventHandler(this.dataGridViewPortConfig_SelectionChanged);
            // 
            // Package
            // 
            this.Package.HeaderText = "Package";
            this.Package.Name = "Package";
            this.Package.ReadOnly = true;
            this.Package.Width = 75;
            // 
            // PkgMinVer
            // 
            this.PkgMinVer.HeaderText = "Pkg Min Ver";
            this.PkgMinVer.Name = "PkgMinVer";
            this.PkgMinVer.ReadOnly = true;
            this.PkgMinVer.Width = 90;
            // 
            // PkgMaxVer
            // 
            this.PkgMaxVer.HeaderText = "Pkg Max Ver";
            this.PkgMaxVer.Name = "PkgMaxVer";
            this.PkgMaxVer.ReadOnly = true;
            this.PkgMaxVer.Width = 93;
            // 
            // DsmMinVer
            // 
            this.DsmMinVer.HeaderText = "DSM Min Ver";
            this.DsmMinVer.Name = "DsmMinVer";
            this.DsmMinVer.ReadOnly = true;
            this.DsmMinVer.Width = 95;
            // 
            // DsmMaxver
            // 
            this.DsmMaxver.HeaderText = "DSM Max Ver";
            this.DsmMaxver.Name = "DsmMaxver";
            this.DsmMaxver.ReadOnly = true;
            this.DsmMaxver.Width = 98;
            // 
            // checkBoxConfig
            // 
            this.checkBoxConfig.AutoCheck = false;
            this.checkBoxConfig.AutoSize = true;
            this.checkBoxConfig.Location = new System.Drawing.Point(12, 12);
            this.checkBoxConfig.Name = "checkBoxConfig";
            this.checkBoxConfig.Size = new System.Drawing.Size(92, 17);
            this.checkBoxConfig.TabIndex = 0;
            this.checkBoxConfig.Text = "Enable Config";
            this.checkBoxConfig.UseVisualStyleBackColor = true;
            this.checkBoxConfig.Click += new System.EventHandler(this.checkBoxConfig_Click);
            // 
            // panelPortConfig
            // 
            this.panelPortConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPortConfig.Controls.Add(this.buttonAbort);
            this.panelPortConfig.Controls.Add(this.groupBoxTip);
            this.panelPortConfig.Controls.Add(this.labelDsmMaxVer);
            this.panelPortConfig.Controls.Add(this.textBoxDsmMaxVer);
            this.panelPortConfig.Controls.Add(this.labelDsmMinVer);
            this.panelPortConfig.Controls.Add(this.textBoxDsmMinVer);
            this.panelPortConfig.Controls.Add(this.labelPkgMaxVer);
            this.panelPortConfig.Controls.Add(this.textBoxPkgMaxVer);
            this.panelPortConfig.Controls.Add(this.labelPkgMinVer);
            this.panelPortConfig.Controls.Add(this.textBoxPkgMinVer);
            this.panelPortConfig.Controls.Add(this.labelPackage);
            this.panelPortConfig.Controls.Add(this.textBoxPackage);
            this.panelPortConfig.Controls.Add(this.buttonSave);
            this.panelPortConfig.Controls.Add(this.buttonDelete);
            this.panelPortConfig.Controls.Add(this.buttonEdit);
            this.panelPortConfig.Controls.Add(this.buttonAdd);
            this.panelPortConfig.Controls.Add(this.dataGridViewConfig);
            this.panelPortConfig.Location = new System.Drawing.Point(12, 35);
            this.panelPortConfig.Name = "panelPortConfig";
            this.panelPortConfig.Size = new System.Drawing.Size(583, 449);
            this.panelPortConfig.TabIndex = 2;
            // 
            // buttonAbort
            // 
            this.buttonAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAbort.CausesValidation = false;
            this.buttonAbort.Location = new System.Drawing.Point(505, 145);
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.Size = new System.Drawing.Size(75, 23);
            this.buttonAbort.TabIndex = 13;
            this.buttonAbort.Text = "Cancel";
            this.buttonAbort.UseVisualStyleBackColor = true;
            this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
            // 
            // groupBoxTip
            // 
            this.groupBoxTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTip.Controls.Add(this.labelToolTip);
            this.groupBoxTip.Location = new System.Drawing.Point(3, 355);
            this.groupBoxTip.Name = "groupBoxTip";
            this.groupBoxTip.Size = new System.Drawing.Size(577, 91);
            this.groupBoxTip.TabIndex = 42;
            this.groupBoxTip.TabStop = false;
            this.groupBoxTip.Text = "TIPS";
            // 
            // labelToolTip
            // 
            this.labelToolTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelToolTip.Location = new System.Drawing.Point(10, 16);
            this.labelToolTip.Name = "labelToolTip";
            this.labelToolTip.Size = new System.Drawing.Size(561, 69);
            this.labelToolTip.TabIndex = 24;
            this.labelToolTip.UseMnemonic = false;
            // 
            // labelDsmMaxVer
            // 
            this.labelDsmMaxVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDsmMaxVer.AutoSize = true;
            this.labelDsmMaxVer.Location = new System.Drawing.Point(16, 309);
            this.labelDsmMaxVer.Name = "labelDsmMaxVer";
            this.labelDsmMaxVer.Size = new System.Drawing.Size(73, 13);
            this.labelDsmMaxVer.TabIndex = 16;
            this.labelDsmMaxVer.Text = "DSM Max Ver";
            // 
            // labelDsmMinVer
            // 
            this.labelDsmMinVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDsmMinVer.AutoSize = true;
            this.labelDsmMinVer.Location = new System.Drawing.Point(19, 279);
            this.labelDsmMinVer.Name = "labelDsmMinVer";
            this.labelDsmMinVer.Size = new System.Drawing.Size(70, 13);
            this.labelDsmMinVer.TabIndex = 14;
            this.labelDsmMinVer.Text = "DSM Min Ver";
            // 
            // labelPkgMaxVer
            // 
            this.labelPkgMaxVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPkgMaxVer.AutoSize = true;
            this.labelPkgMaxVer.Location = new System.Drawing.Point(21, 249);
            this.labelPkgMaxVer.Name = "labelPkgMaxVer";
            this.labelPkgMaxVer.Size = new System.Drawing.Size(68, 13);
            this.labelPkgMaxVer.TabIndex = 10;
            this.labelPkgMaxVer.Text = "Pkg Max Ver";
            this.labelPkgMaxVer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPkgMinVer
            // 
            this.labelPkgMinVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPkgMinVer.AutoSize = true;
            this.labelPkgMinVer.Location = new System.Drawing.Point(24, 219);
            this.labelPkgMinVer.Name = "labelPkgMinVer";
            this.labelPkgMinVer.Size = new System.Drawing.Size(65, 13);
            this.labelPkgMinVer.TabIndex = 8;
            this.labelPkgMinVer.Text = "Pkg Min Ver";
            this.labelPkgMinVer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPackage
            // 
            this.labelPackage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPackage.AutoSize = true;
            this.labelPackage.Location = new System.Drawing.Point(8, 189);
            this.labelPackage.Name = "labelPackage";
            this.labelPackage.Size = new System.Drawing.Size(81, 13);
            this.labelPackage.TabIndex = 6;
            this.labelPackage.Text = "Package Name";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(424, 145);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelete.Location = new System.Drawing.Point(165, 145);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEdit.Location = new System.Drawing.Point(84, 145);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Location = new System.Drawing.Point(3, 145);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // buttonAdvanced
            // 
            this.buttonAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdvanced.Location = new System.Drawing.Point(436, 490);
            this.buttonAdvanced.Name = "buttonAdvanced";
            this.buttonAdvanced.Size = new System.Drawing.Size(75, 23);
            this.buttonAdvanced.TabIndex = 5;
            this.buttonAdvanced.Text = "Advanced";
            this.buttonAdvanced.UseVisualStyleBackColor = true;
            this.buttonAdvanced.Click += new System.EventHandler(this.buttonAdvanced_Click);
            // 
            // PKG_Conf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 541);
            this.ControlBox = false;
            this.Controls.Add(this.buttonAdvanced);
            this.Controls.Add(this.panelPortConfig);
            this.Controls.Add(this.checkBoxConfig);
            this.Controls.Add(this.linkLabelHelp);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(623, 580);
            this.Name = "PKG_Conf";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Config Editor";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PortConfig_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConfig)).EndInit();
            this.panelPortConfig.ResumeLayout(false);
            this.panelPortConfig.PerformLayout();
            this.groupBoxTip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.LinkLabel linkLabelHelp;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox checkBoxConfig;
        private System.Windows.Forms.Panel panelPortConfig;
        private System.Windows.Forms.DataGridView dataGridViewConfig;
        private System.Windows.Forms.Label labelDsmMaxVer;
        private System.Windows.Forms.TextBox textBoxDsmMaxVer;
        private System.Windows.Forms.Label labelDsmMinVer;
        private System.Windows.Forms.TextBox textBoxDsmMinVer;
        private System.Windows.Forms.Label labelPkgMaxVer;
        private System.Windows.Forms.TextBox textBoxPkgMaxVer;
        private System.Windows.Forms.Label labelPkgMinVer;
        private System.Windows.Forms.TextBox textBoxPkgMinVer;
        private System.Windows.Forms.Label labelPackage;
        private System.Windows.Forms.TextBox textBoxPackage;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.GroupBox groupBoxTip;
        private System.Windows.Forms.Label labelToolTip;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button buttonAbort;
        private System.Windows.Forms.Button buttonAdvanced;
        private System.Windows.Forms.DataGridViewTextBoxColumn Package;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkgMinVer;
        private System.Windows.Forms.DataGridViewTextBoxColumn PkgMaxVer;
        private System.Windows.Forms.DataGridViewTextBoxColumn DsmMinVer;
        private System.Windows.Forms.DataGridViewTextBoxColumn DsmMaxver;
    }
}
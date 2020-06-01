namespace BeatificaBytes.Synology.Mods
{
    partial class PortConfigWorker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortConfigWorker));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.linkLabelHelp = new System.Windows.Forms.LinkLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewPortConfig = new System.Windows.Forms.DataGridView();
            this.Service = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Forward = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Destination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxService = new System.Windows.Forms.TextBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxSrcPorts = new System.Windows.Forms.TextBox();
            this.textBoxDstPorts = new System.Windows.Forms.TextBox();
            this.checkBoxForward = new System.Windows.Forms.CheckBox();
            this.checkBoxPortConfig = new System.Windows.Forms.CheckBox();
            this.panelPortConfig = new System.Windows.Forms.Panel();
            this.buttonAbort = new System.Windows.Forms.Button();
            this.groupBoxTip = new System.Windows.Forms.GroupBox();
            this.labelToolTip = new System.Windows.Forms.Label();
            this.comboBoxDstProtocol = new System.Windows.Forms.ComboBox();
            this.comboBoxSrcProtocol = new System.Windows.Forms.ComboBox();
            this.labelDstPorts = new System.Windows.Forms.Label();
            this.labelSrcPorts = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelService = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonAdvanced = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPortConfig)).BeginInit();
            this.panelPortConfig.SuspendLayout();
            this.groupBoxTip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.Location = new System.Drawing.Point(12, 605);
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
            this.buttonCancel.Location = new System.Drawing.Point(769, 605);
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
            this.linkLabelHelp.Location = new System.Drawing.Point(824, 3);
            this.linkLabelHelp.Name = "linkLabelHelp";
            this.linkLabelHelp.Size = new System.Drawing.Size(29, 13);
            this.linkLabelHelp.TabIndex = 1;
            this.linkLabelHelp.TabStop = true;
            this.linkLabelHelp.Text = "Help";
            this.linkLabelHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // dataGridViewPortConfig
            // 
            this.dataGridViewPortConfig.AllowUserToAddRows = false;
            this.dataGridViewPortConfig.AllowUserToDeleteRows = false;
            this.dataGridViewPortConfig.AllowUserToResizeRows = false;
            this.dataGridViewPortConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPortConfig.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewPortConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPortConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Service,
            this.Title,
            this.Description,
            this.Forward,
            this.Source,
            this.Destination});
            this.dataGridViewPortConfig.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewPortConfig.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPortConfig.MultiSelect = false;
            this.dataGridViewPortConfig.Name = "dataGridViewPortConfig";
            this.dataGridViewPortConfig.ReadOnly = true;
            this.dataGridViewPortConfig.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPortConfig.ShowEditingIcon = false;
            this.dataGridViewPortConfig.Size = new System.Drawing.Size(831, 254);
            this.dataGridViewPortConfig.StandardTab = true;
            this.dataGridViewPortConfig.TabIndex = 0;
            this.toolTip.SetToolTip(this.dataGridViewPortConfig, "Usually a package only has one unique service name. If your package needs more th" +
        "an one port description, you can define service_name2, service_name3, …");
            this.dataGridViewPortConfig.SelectionChanged += new System.EventHandler(this.dataGridViewPortConfig_SelectionChanged);
            // 
            // Service
            // 
            this.Service.HeaderText = "Service";
            this.Service.Name = "Service";
            this.Service.ReadOnly = true;
            this.Service.Width = 68;
            // 
            // Title
            // 
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Width = 52;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 85;
            // 
            // Forward
            // 
            this.Forward.HeaderText = "Forward";
            this.Forward.Name = "Forward";
            this.Forward.ReadOnly = true;
            this.Forward.Width = 51;
            // 
            // Source
            // 
            this.Source.HeaderText = "Source";
            this.Source.Name = "Source";
            this.Source.ReadOnly = true;
            this.Source.Width = 66;
            // 
            // Destination
            // 
            this.Destination.HeaderText = "Destination";
            this.Destination.Name = "Destination";
            this.Destination.ReadOnly = true;
            this.Destination.Width = 85;
            // 
            // textBoxService
            // 
            this.textBoxService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxService.Location = new System.Drawing.Point(93, 317);
            this.textBoxService.Name = "textBoxService";
            this.textBoxService.Size = new System.Drawing.Size(156, 20);
            this.textBoxService.TabIndex = 4;
            this.toolTip.SetToolTip(this.textBoxService, "A unique Service name. It cannot be empty and can only include characters “a~z”, " +
        "“A~Z”, “0~9”, “-”, “\\”, “.”");
            this.textBoxService.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxService_Validating);
            this.textBoxService.Validated += new System.EventHandler(this.textBoxService_Validated);
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTitle.Location = new System.Drawing.Point(338, 317);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(410, 20);
            this.textBoxTitle.TabIndex = 5;
            this.toolTip.SetToolTip(this.textBoxTitle, "English title which will be shown on field Protocol at firewall build-in selectio" +
        "n menu.\r\nThis Field is Mandatory.");
            this.textBoxTitle.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxTitle_Validating);
            this.textBoxTitle.Validated += new System.EventHandler(this.textBoxTitle_Validated);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(338, 343);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(410, 20);
            this.textBoxDescription.TabIndex = 7;
            this.toolTip.SetToolTip(this.textBoxDescription, "English description which will be shown on field Applications at firewall build-i" +
        "n selection menu.\r\nThis Field is Mandatory.");
            this.textBoxDescription.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDescription_Validating);
            this.textBoxDescription.Validated += new System.EventHandler(this.textBoxDescription_Validated);
            // 
            // textBoxSrcPorts
            // 
            this.textBoxSrcPorts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSrcPorts.Location = new System.Drawing.Point(165, 387);
            this.textBoxSrcPorts.Name = "textBoxSrcPorts";
            this.textBoxSrcPorts.Size = new System.Drawing.Size(583, 20);
            this.textBoxSrcPorts.TabIndex = 9;
            this.toolTip.SetToolTip(this.textBoxSrcPorts, resources.GetString("textBoxSrcPorts.ToolTip"));
            this.textBoxSrcPorts.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSrcPorts_Validating);
            this.textBoxSrcPorts.Validated += new System.EventHandler(this.textBoxSrcPorts_Validated);
            // 
            // textBoxDstPorts
            // 
            this.textBoxDstPorts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDstPorts.Location = new System.Drawing.Point(165, 413);
            this.textBoxDstPorts.Name = "textBoxDstPorts";
            this.textBoxDstPorts.Size = new System.Drawing.Size(583, 20);
            this.textBoxDstPorts.TabIndex = 11;
            this.toolTip.SetToolTip(this.textBoxDstPorts, resources.GetString("textBoxDstPorts.ToolTip"));
            this.textBoxDstPorts.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDstPorts_Validating);
            this.textBoxDstPorts.Validated += new System.EventHandler(this.textBoxDstPorts_Validated);
            // 
            // checkBoxForward
            // 
            this.checkBoxForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxForward.AutoSize = true;
            this.checkBoxForward.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxForward.Location = new System.Drawing.Point(20, 346);
            this.checkBoxForward.Name = "checkBoxForward";
            this.checkBoxForward.Size = new System.Drawing.Size(86, 17);
            this.checkBoxForward.TabIndex = 6;
            this.checkBoxForward.Text = "Port Forward";
            this.toolTip.SetToolTip(this.checkBoxForward, "If set to “yes,” your package service related ports will be listed when users set" +
        " port forwarding rule from build-in applications.\r\nOtherwise they will not be li" +
        "sted.");
            this.checkBoxForward.UseVisualStyleBackColor = true;
            // 
            // checkBoxPortConfig
            // 
            this.checkBoxPortConfig.AutoSize = true;
            this.checkBoxPortConfig.Location = new System.Drawing.Point(12, 12);
            this.checkBoxPortConfig.Name = "checkBoxPortConfig";
            this.checkBoxPortConfig.Size = new System.Drawing.Size(114, 17);
            this.checkBoxPortConfig.TabIndex = 0;
            this.checkBoxPortConfig.Text = "Enable Port Config";
            this.checkBoxPortConfig.UseVisualStyleBackColor = true;
            this.checkBoxPortConfig.CheckedChanged += new System.EventHandler(this.checkBoxPortConfig_CheckedChanged);
            // 
            // panelPortConfig
            // 
            this.panelPortConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPortConfig.Controls.Add(this.buttonAbort);
            this.panelPortConfig.Controls.Add(this.groupBoxTip);
            this.panelPortConfig.Controls.Add(this.comboBoxDstProtocol);
            this.panelPortConfig.Controls.Add(this.comboBoxSrcProtocol);
            this.panelPortConfig.Controls.Add(this.checkBoxForward);
            this.panelPortConfig.Controls.Add(this.labelDstPorts);
            this.panelPortConfig.Controls.Add(this.textBoxDstPorts);
            this.panelPortConfig.Controls.Add(this.labelSrcPorts);
            this.panelPortConfig.Controls.Add(this.textBoxSrcPorts);
            this.panelPortConfig.Controls.Add(this.labelDescription);
            this.panelPortConfig.Controls.Add(this.textBoxDescription);
            this.panelPortConfig.Controls.Add(this.labelTitle);
            this.panelPortConfig.Controls.Add(this.textBoxTitle);
            this.panelPortConfig.Controls.Add(this.labelService);
            this.panelPortConfig.Controls.Add(this.textBoxService);
            this.panelPortConfig.Controls.Add(this.buttonSave);
            this.panelPortConfig.Controls.Add(this.buttonDelete);
            this.panelPortConfig.Controls.Add(this.buttonEdit);
            this.panelPortConfig.Controls.Add(this.buttonAdd);
            this.panelPortConfig.Controls.Add(this.dataGridViewPortConfig);
            this.panelPortConfig.Location = new System.Drawing.Point(12, 35);
            this.panelPortConfig.Name = "panelPortConfig";
            this.panelPortConfig.Size = new System.Drawing.Size(832, 564);
            this.panelPortConfig.TabIndex = 2;
            // 
            // buttonAbort
            // 
            this.buttonAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAbort.CausesValidation = false;
            this.buttonAbort.Location = new System.Drawing.Point(754, 441);
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
            this.groupBoxTip.Location = new System.Drawing.Point(3, 470);
            this.groupBoxTip.Name = "groupBoxTip";
            this.groupBoxTip.Size = new System.Drawing.Size(826, 91);
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
            this.labelToolTip.Size = new System.Drawing.Size(810, 69);
            this.labelToolTip.TabIndex = 24;
            this.labelToolTip.UseMnemonic = false;
            // 
            // comboBoxDstProtocol
            // 
            this.comboBoxDstProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxDstProtocol.FormattingEnabled = true;
            this.comboBoxDstProtocol.Items.AddRange(new object[] {
            "",
            "tcp",
            "udp",
            "tcp,udp"});
            this.comboBoxDstProtocol.Location = new System.Drawing.Point(93, 412);
            this.comboBoxDstProtocol.Name = "comboBoxDstProtocol";
            this.comboBoxDstProtocol.Size = new System.Drawing.Size(66, 21);
            this.comboBoxDstProtocol.TabIndex = 10;
            // 
            // comboBoxSrcProtocol
            // 
            this.comboBoxSrcProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxSrcProtocol.FormattingEnabled = true;
            this.comboBoxSrcProtocol.Items.AddRange(new object[] {
            "",
            "tcp",
            "udp",
            "tcp,udp"});
            this.comboBoxSrcProtocol.Location = new System.Drawing.Point(93, 387);
            this.comboBoxSrcProtocol.Name = "comboBoxSrcProtocol";
            this.comboBoxSrcProtocol.Size = new System.Drawing.Size(66, 21);
            this.comboBoxSrcProtocol.TabIndex = 8;
            // 
            // labelDstPorts
            // 
            this.labelDstPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDstPorts.AutoSize = true;
            this.labelDstPorts.Location = new System.Drawing.Point(2, 416);
            this.labelDstPorts.Name = "labelDstPorts";
            this.labelDstPorts.Size = new System.Drawing.Size(87, 13);
            this.labelDstPorts.TabIndex = 16;
            this.labelDstPorts.Text = "Destination Ports";
            // 
            // labelSrcPorts
            // 
            this.labelSrcPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSrcPorts.AutoSize = true;
            this.labelSrcPorts.Location = new System.Drawing.Point(21, 390);
            this.labelSrcPorts.Name = "labelSrcPorts";
            this.labelSrcPorts.Size = new System.Drawing.Size(68, 13);
            this.labelSrcPorts.TabIndex = 14;
            this.labelSrcPorts.Text = "Source Ports";
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(269, 346);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(60, 13);
            this.labelDescription.TabIndex = 10;
            this.labelDescription.Text = "Description";
            this.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(302, 320);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(27, 13);
            this.labelTitle.TabIndex = 8;
            this.labelTitle.Text = "Title";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelService
            // 
            this.labelService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelService.AutoSize = true;
            this.labelService.Location = new System.Drawing.Point(15, 320);
            this.labelService.Name = "labelService";
            this.labelService.Size = new System.Drawing.Size(74, 13);
            this.labelService.TabIndex = 6;
            this.labelService.Text = "Service Name";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(673, 441);
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
            this.buttonDelete.Location = new System.Drawing.Point(165, 260);
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
            this.buttonEdit.Location = new System.Drawing.Point(84, 260);
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
            this.buttonAdd.Location = new System.Drawing.Point(3, 260);
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
            this.buttonAdvanced.Location = new System.Drawing.Point(685, 605);
            this.buttonAdvanced.Name = "buttonAdvanced";
            this.buttonAdvanced.Size = new System.Drawing.Size(75, 23);
            this.buttonAdvanced.TabIndex = 5;
            this.buttonAdvanced.Text = "Advanced";
            this.buttonAdvanced.UseVisualStyleBackColor = true;
            this.buttonAdvanced.Click += new System.EventHandler(this.buttonAdvanced_Click);
            // 
            // PortConfigWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 640);
            this.ControlBox = false;
            this.Controls.Add(this.buttonAdvanced);
            this.Controls.Add(this.panelPortConfig);
            this.Controls.Add(this.checkBoxPortConfig);
            this.Controls.Add(this.linkLabelHelp);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(601, 531);
            this.Name = "PortConfigWorker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Config Editor";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PortConfig_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPortConfig)).EndInit();
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
        private System.Windows.Forms.CheckBox checkBoxPortConfig;
        private System.Windows.Forms.Panel panelPortConfig;
        private System.Windows.Forms.DataGridView dataGridViewPortConfig;
        private System.Windows.Forms.DataGridViewTextBoxColumn Service;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Forward;
        private System.Windows.Forms.DataGridViewTextBoxColumn Source;
        private System.Windows.Forms.DataGridViewTextBoxColumn Destination;
        private System.Windows.Forms.ComboBox comboBoxDstProtocol;
        private System.Windows.Forms.ComboBox comboBoxSrcProtocol;
        private System.Windows.Forms.CheckBox checkBoxForward;
        private System.Windows.Forms.Label labelDstPorts;
        private System.Windows.Forms.TextBox textBoxDstPorts;
        private System.Windows.Forms.Label labelSrcPorts;
        private System.Windows.Forms.TextBox textBoxSrcPorts;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label labelService;
        private System.Windows.Forms.TextBox textBoxService;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.GroupBox groupBoxTip;
        private System.Windows.Forms.Label labelToolTip;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button buttonAbort;
        private System.Windows.Forms.Button buttonAdvanced;
    }
}
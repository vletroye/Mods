namespace BeatificaBytes.Synology.Mods
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonPackage = new System.Windows.Forms.Button();
            this.listViewUrls = new System.Windows.Forms.ListView();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelDesc = new System.Windows.Forms.Label();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.checkBoxAllUsers = new System.Windows.Forms.CheckBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.toolTip4Mods = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox_256 = new System.Windows.Forms.PictureBox();
            this.pictureBox_128 = new System.Windows.Forms.PictureBox();
            this.pictureBox_96 = new System.Windows.Forms.PictureBox();
            this.pictureBox_72 = new System.Windows.Forms.PictureBox();
            this.pictureBox_64 = new System.Windows.Forms.PictureBox();
            this.pictureBox_48 = new System.Windows.Forms.PictureBox();
            this.pictureBox_32 = new System.Windows.Forms.PictureBox();
            this.pictureBox_24 = new System.Windows.Forms.PictureBox();
            this.pictureBox_16 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPkg_256 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPkg_72 = new System.Windows.Forms.PictureBox();
            this.textBoxMaintainerUrl = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxPackage = new System.Windows.Forms.TextBox();
            this.textBoxDisplay = new System.Windows.Forms.TextBox();
            this.textBoxMaintainer = new System.Windows.Forms.TextBox();
            this.textBoxDsmAppName = new System.Windows.Forms.TextBox();
            this.textBoxVersion = new System.Windows.Forms.TextBox();
            this.openFileDialog4Mods = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog4Mods = new System.Windows.Forms.FolderBrowserDialog();
            this.webpageBrowserDialog4Mods = new System.Windows.Forms.FolderBrowserDialog();
            this.pictureBoxSettings = new System.Windows.Forms.PictureBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.labelMaintainerUrl = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelPackage = new System.Windows.Forms.Label();
            this.labelDisplay = new System.Windows.Forms.Label();
            this.labelMaintainer = new System.Windows.Forms.Label();
            this.groupBoxPackage = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxURL = new System.Windows.Forms.GroupBox();
            this.comboBoxUrlType = new System.Windows.Forms.ComboBox();
            this.checkBoxSize = new System.Windows.Forms.CheckBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelDSMAppName = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.checkBoxMultiInstance = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_256)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_128)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_96)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_72)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_64)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_48)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPkg_256)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPkg_72)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSettings)).BeginInit();
            this.groupBoxPackage.SuspendLayout();
            this.groupBoxURL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPackage
            // 
            this.buttonPackage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPackage.Location = new System.Drawing.Point(707, 159);
            this.buttonPackage.Name = "buttonPackage";
            this.buttonPackage.Size = new System.Drawing.Size(72, 23);
            this.buttonPackage.TabIndex = 18;
            this.buttonPackage.Text = "Package";
            this.buttonPackage.UseVisualStyleBackColor = true;
            this.buttonPackage.Click += new System.EventHandler(this.buttonPackage_Click);
            // 
            // listViewUrls
            // 
            this.listViewUrls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewUrls.Location = new System.Drawing.Point(12, 217);
            this.listViewUrls.Name = "listViewUrls";
            this.listViewUrls.Size = new System.Drawing.Size(770, 130);
            this.listViewUrls.TabIndex = 8;
            this.listViewUrls.UseCompatibleStateImageBehavior = false;
            this.listViewUrls.SelectedIndexChanged += new System.EventHandler(this.listViewUrls_SelectedIndexChanged);
            this.listViewUrls.DoubleClick += new System.EventHandler(this.listViewUrls_DoubleClick);
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxTitle.Location = new System.Drawing.Point(77, 354);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(128, 20);
            this.textBoxTitle.TabIndex = 12;
            this.toolTip4Mods.SetToolTip(this.textBoxTitle, "Enter the title of the URL. It will to be displayed on DSM.");
            this.textBoxTitle.Leave += new System.EventHandler(this.textBoxTitle_Leave);
            this.textBoxTitle.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxTitle_Validating);
            this.textBoxTitle.Validated += new System.EventHandler(this.textBoxTitle_Validated);
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(42, 357);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(30, 13);
            this.labelTitle.TabIndex = 3;
            this.labelTitle.Text = "Title:";
            // 
            // labelDesc
            // 
            this.labelDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDesc.AutoSize = true;
            this.labelDesc.Location = new System.Drawing.Point(9, 383);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Size = new System.Drawing.Size(63, 13);
            this.labelDesc.TabIndex = 5;
            this.labelDesc.Text = "Description:";
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxDesc.Location = new System.Drawing.Point(77, 380);
            this.textBoxDesc.Multiline = true;
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxDesc.Size = new System.Drawing.Size(705, 60);
            this.textBoxDesc.TabIndex = 13;
            this.toolTip4Mods.SetToolTip(this.textBoxDesc, "Enter an optional description. This will not be displayed on DSM.");
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxUrl.Location = new System.Drawing.Point(77, 446);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(704, 20);
            this.textBoxUrl.TabIndex = 14;
            this.toolTip4Mods.SetToolTip(this.textBoxUrl, "Type here the URL to be opened when clicking the icon on DSM.");
            this.textBoxUrl.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxUrl_Validating);
            this.textBoxUrl.Validated += new System.EventHandler(this.textBoxUrl_Validated);
            // 
            // checkBoxAllUsers
            // 
            this.checkBoxAllUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAllUsers.AutoSize = true;
            this.checkBoxAllUsers.Location = new System.Drawing.Point(313, 155);
            this.checkBoxAllUsers.Name = "checkBoxAllUsers";
            this.checkBoxAllUsers.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxAllUsers.Size = new System.Drawing.Size(88, 17);
            this.checkBoxAllUsers.TabIndex = 8;
            this.checkBoxAllUsers.Text = ":For All Users";
            this.checkBoxAllUsers.UseVisualStyleBackColor = true;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Location = new System.Drawing.Point(12, 607);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 9;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEdit.Location = new System.Drawing.Point(172, 407);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 11;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.Location = new System.Drawing.Point(334, 407);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.Location = new System.Drawing.Point(253, 407);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelete.Location = new System.Drawing.Point(91, 407);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 10;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // pictureBox_256
            // 
            this.pictureBox_256.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_256.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_256.Location = new System.Drawing.Point(617, 472);
            this.pictureBox_256.Name = "pictureBox_256";
            this.pictureBox_256.Size = new System.Drawing.Size(164, 164);
            this.pictureBox_256.TabIndex = 22;
            this.pictureBox_256.TabStop = false;
            this.pictureBox_256.Tag = "URL256";
            this.toolTip4Mods.SetToolTip(this.pictureBox_256, "Drop here a logo 256x256");
            // 
            // pictureBox_128
            // 
            this.pictureBox_128.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_128.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_128.Location = new System.Drawing.Point(483, 472);
            this.pictureBox_128.Name = "pictureBox_128";
            this.pictureBox_128.Size = new System.Drawing.Size(128, 128);
            this.pictureBox_128.TabIndex = 21;
            this.pictureBox_128.TabStop = false;
            this.pictureBox_128.Tag = "URL128";
            this.toolTip4Mods.SetToolTip(this.pictureBox_128, "Drop here a logo 128x128");
            // 
            // pictureBox_96
            // 
            this.pictureBox_96.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_96.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_96.Location = new System.Drawing.Point(381, 472);
            this.pictureBox_96.Name = "pictureBox_96";
            this.pictureBox_96.Size = new System.Drawing.Size(96, 96);
            this.pictureBox_96.TabIndex = 20;
            this.pictureBox_96.TabStop = false;
            this.pictureBox_96.Tag = "URL96";
            this.toolTip4Mods.SetToolTip(this.pictureBox_96, "Drop here a logo 96x96");
            // 
            // pictureBox_72
            // 
            this.pictureBox_72.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_72.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_72.Location = new System.Drawing.Point(303, 472);
            this.pictureBox_72.Name = "pictureBox_72";
            this.pictureBox_72.Size = new System.Drawing.Size(72, 72);
            this.pictureBox_72.TabIndex = 19;
            this.pictureBox_72.TabStop = false;
            this.pictureBox_72.Tag = "URL72";
            this.toolTip4Mods.SetToolTip(this.pictureBox_72, "Drop here a logo 72x72");
            // 
            // pictureBox_64
            // 
            this.pictureBox_64.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_64.Location = new System.Drawing.Point(233, 472);
            this.pictureBox_64.Name = "pictureBox_64";
            this.pictureBox_64.Size = new System.Drawing.Size(64, 64);
            this.pictureBox_64.TabIndex = 18;
            this.pictureBox_64.TabStop = false;
            this.pictureBox_64.Tag = "URL64";
            this.toolTip4Mods.SetToolTip(this.pictureBox_64, "Drop here a logo 64x64");
            // 
            // pictureBox_48
            // 
            this.pictureBox_48.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_48.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_48.Location = new System.Drawing.Point(179, 472);
            this.pictureBox_48.Name = "pictureBox_48";
            this.pictureBox_48.Size = new System.Drawing.Size(48, 48);
            this.pictureBox_48.TabIndex = 17;
            this.pictureBox_48.TabStop = false;
            this.pictureBox_48.Tag = "URL48";
            this.toolTip4Mods.SetToolTip(this.pictureBox_48, "Drop here a logo 48x48");
            // 
            // pictureBox_32
            // 
            this.pictureBox_32.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_32.Location = new System.Drawing.Point(141, 472);
            this.pictureBox_32.Name = "pictureBox_32";
            this.pictureBox_32.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_32.TabIndex = 16;
            this.pictureBox_32.TabStop = false;
            this.pictureBox_32.Tag = "URL32";
            this.toolTip4Mods.SetToolTip(this.pictureBox_32, "Drop here a logo 32x32");
            // 
            // pictureBox_24
            // 
            this.pictureBox_24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_24.Location = new System.Drawing.Point(111, 472);
            this.pictureBox_24.Name = "pictureBox_24";
            this.pictureBox_24.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_24.TabIndex = 15;
            this.pictureBox_24.TabStop = false;
            this.pictureBox_24.Tag = "URL24";
            this.toolTip4Mods.SetToolTip(this.pictureBox_24, "Drop here a logo 24x24");
            // 
            // pictureBox_16
            // 
            this.pictureBox_16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_16.Location = new System.Drawing.Point(89, 472);
            this.pictureBox_16.Name = "pictureBox_16";
            this.pictureBox_16.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_16.TabIndex = 14;
            this.pictureBox_16.TabStop = false;
            this.pictureBox_16.Tag = "URL16";
            this.toolTip4Mods.SetToolTip(this.pictureBox_16, "Drop here a logo 16x16");
            // 
            // pictureBoxPkg_256
            // 
            this.pictureBoxPkg_256.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxPkg_256.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPkg_256.Location = new System.Drawing.Point(539, 18);
            this.pictureBoxPkg_256.Name = "pictureBoxPkg_256";
            this.pictureBoxPkg_256.Size = new System.Drawing.Size(164, 164);
            this.pictureBoxPkg_256.TabIndex = 27;
            this.pictureBoxPkg_256.TabStop = false;
            this.pictureBoxPkg_256.Tag = "Pkg256";
            this.toolTip4Mods.SetToolTip(this.pictureBoxPkg_256, "Drop here a logo 256x256");
            this.pictureBoxPkg_256.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBoxPkg_DragDrop);
            this.pictureBoxPkg_256.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBoxPkg_DragEnter);
            this.pictureBoxPkg_256.DoubleClick += new System.EventHandler(this.pictureBoxPkg_DoubleClick);
            // 
            // pictureBoxPkg_72
            // 
            this.pictureBoxPkg_72.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxPkg_72.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPkg_72.Location = new System.Drawing.Point(707, 57);
            this.pictureBoxPkg_72.Name = "pictureBoxPkg_72";
            this.pictureBoxPkg_72.Size = new System.Drawing.Size(72, 72);
            this.pictureBoxPkg_72.TabIndex = 26;
            this.pictureBoxPkg_72.TabStop = false;
            this.pictureBoxPkg_72.Tag = "Pkg72";
            this.toolTip4Mods.SetToolTip(this.pictureBoxPkg_72, "Drop here a logo 72x72");
            this.pictureBoxPkg_72.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBoxPkg_DragDrop);
            this.pictureBoxPkg_72.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBoxPkg_DragEnter);
            this.pictureBoxPkg_72.DoubleClick += new System.EventHandler(this.pictureBoxPkg_DoubleClick);
            // 
            // textBoxMaintainerUrl
            // 
            this.textBoxMaintainerUrl.Location = new System.Drawing.Point(91, 165);
            this.textBoxMaintainerUrl.Name = "textBoxMaintainerUrl";
            this.textBoxMaintainerUrl.Size = new System.Drawing.Size(385, 20);
            this.textBoxMaintainerUrl.TabIndex = 7;
            this.textBoxMaintainerUrl.Tag = "PKGmaintainer_url;PKGdistributor_url";
            this.toolTip4Mods.SetToolTip(this.textBoxMaintainerUrl, "Type here the url of your website if you intend to distribute your package.");
            this.textBoxMaintainerUrl.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxMaintainerUrl_Validating);
            this.textBoxMaintainerUrl.Validated += new System.EventHandler(this.textBoxMaintainerUrl_Validated);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(91, 99);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxDescription.Size = new System.Drawing.Size(385, 60);
            this.textBoxDescription.TabIndex = 6;
            this.textBoxDescription.Tag = "PKGdescription_enu;PKGdescription";
            this.toolTip4Mods.SetToolTip(this.textBoxDescription, "Enter the description of your package. This will be displayed in DSM\'s Package Ce" +
        "nter.");
            this.textBoxDescription.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDescription_Validating);
            this.textBoxDescription.Validated += new System.EventHandler(this.textBoxDescription_Validated);
            // 
            // textBoxPackage
            // 
            this.textBoxPackage.Location = new System.Drawing.Point(91, 21);
            this.textBoxPackage.Name = "textBoxPackage";
            this.textBoxPackage.Size = new System.Drawing.Size(128, 20);
            this.textBoxPackage.TabIndex = 1;
            this.textBoxPackage.Tag = "PKGpackage";
            this.toolTip4Mods.SetToolTip(this.textBoxPackage, "Enter a name for your Package.");
            this.textBoxPackage.Leave += new System.EventHandler(this.textBoxPackage_Leave);
            this.textBoxPackage.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxPackage_Validating);
            this.textBoxPackage.Validated += new System.EventHandler(this.textBoxPackage_Validated);
            // 
            // textBoxDisplay
            // 
            this.textBoxDisplay.Location = new System.Drawing.Point(91, 47);
            this.textBoxDisplay.Name = "textBoxDisplay";
            this.textBoxDisplay.Size = new System.Drawing.Size(128, 20);
            this.textBoxDisplay.TabIndex = 3;
            this.textBoxDisplay.Tag = "PKGdisplayname";
            this.toolTip4Mods.SetToolTip(this.textBoxDisplay, "Enter the name to be displayed on DSM for your package.");
            this.textBoxDisplay.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDisplay_Validating);
            this.textBoxDisplay.Validated += new System.EventHandler(this.textBoxDisplay_Validated);
            // 
            // textBoxMaintainer
            // 
            this.textBoxMaintainer.Location = new System.Drawing.Point(91, 73);
            this.textBoxMaintainer.Name = "textBoxMaintainer";
            this.textBoxMaintainer.Size = new System.Drawing.Size(128, 20);
            this.textBoxMaintainer.TabIndex = 4;
            this.textBoxMaintainer.Tag = "PKGmaintainer;PKGdistributor";
            this.toolTip4Mods.SetToolTip(this.textBoxMaintainer, "Enter the name to be displayed on DSM for your package.");
            this.textBoxMaintainer.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxMaintainer_Validating);
            this.textBoxMaintainer.Validated += new System.EventHandler(this.textBoxMaintainer_Validated);
            // 
            // textBoxDsmAppName
            // 
            this.textBoxDsmAppName.Location = new System.Drawing.Point(306, 72);
            this.textBoxDsmAppName.Name = "textBoxDsmAppName";
            this.textBoxDsmAppName.Size = new System.Drawing.Size(168, 20);
            this.textBoxDsmAppName.TabIndex = 5;
            this.textBoxDsmAppName.Tag = "PKGdsmappname";
            this.toolTip4Mods.SetToolTip(this.textBoxDsmAppName, "Enter an application name for your package. Ex.: com.yourSite.yourPackageName");
            this.textBoxDsmAppName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDsmAppName_Validating);
            this.textBoxDsmAppName.Validated += new System.EventHandler(this.textBoxDsmAppName_Validated);
            // 
            // textBoxVersion
            // 
            this.textBoxVersion.Location = new System.Drawing.Point(306, 21);
            this.textBoxVersion.Name = "textBoxVersion";
            this.textBoxVersion.Size = new System.Drawing.Size(73, 20);
            this.textBoxVersion.TabIndex = 2;
            this.textBoxVersion.Tag = "PKGversion";
            this.toolTip4Mods.SetToolTip(this.textBoxVersion, "Enter the version of your Package.");
            this.textBoxVersion.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxVersion_Validating);
            this.textBoxVersion.Validated += new System.EventHandler(this.textBoxVersion_Validated);
            // 
            // openFileDialog4Mods
            // 
            this.openFileDialog4Mods.Filter = "Png|*.png";
            this.openFileDialog4Mods.RestoreDirectory = true;
            // 
            // pictureBoxSettings
            // 
            this.pictureBoxSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxSettings.Image = global::BeatificaBytes.Synology.Mods.Properties.Resources.settings;
            this.pictureBoxSettings.InitialImage = global::BeatificaBytes.Synology.Mods.Properties.Resources.settings;
            this.pictureBoxSettings.Location = new System.Drawing.Point(742, 11);
            this.pictureBoxSettings.Name = "pictureBoxSettings";
            this.pictureBoxSettings.Size = new System.Drawing.Size(37, 40);
            this.pictureBoxSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSettings.TabIndex = 23;
            this.pictureBoxSettings.TabStop = false;
            this.pictureBoxSettings.Click += new System.EventHandler(this.pictureBoxSettings_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReset.CausesValidation = false;
            this.buttonReset.Location = new System.Drawing.Point(707, 135);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(72, 23);
            this.buttonReset.TabIndex = 17;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // labelMaintainerUrl
            // 
            this.labelMaintainerUrl.AutoSize = true;
            this.labelMaintainerUrl.Location = new System.Drawing.Point(65, 168);
            this.labelMaintainerUrl.Name = "labelMaintainerUrl";
            this.labelMaintainerUrl.Size = new System.Drawing.Size(23, 13);
            this.labelMaintainerUrl.TabIndex = 33;
            this.labelMaintainerUrl.Text = "Url:";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(25, 102);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(63, 13);
            this.labelDescription.TabIndex = 31;
            this.labelDescription.Text = "Description:";
            // 
            // labelPackage
            // 
            this.labelPackage.AutoSize = true;
            this.labelPackage.Location = new System.Drawing.Point(4, 24);
            this.labelPackage.Name = "labelPackage";
            this.labelPackage.Size = new System.Drawing.Size(84, 13);
            this.labelPackage.TabIndex = 29;
            this.labelPackage.Text = "Package Name:";
            // 
            // labelDisplay
            // 
            this.labelDisplay.AutoSize = true;
            this.labelDisplay.Location = new System.Drawing.Point(13, 50);
            this.labelDisplay.Name = "labelDisplay";
            this.labelDisplay.Size = new System.Drawing.Size(75, 13);
            this.labelDisplay.TabIndex = 35;
            this.labelDisplay.Text = "Display Name:";
            // 
            // labelMaintainer
            // 
            this.labelMaintainer.AutoSize = true;
            this.labelMaintainer.Location = new System.Drawing.Point(29, 76);
            this.labelMaintainer.Name = "labelMaintainer";
            this.labelMaintainer.Size = new System.Drawing.Size(59, 13);
            this.labelMaintainer.TabIndex = 37;
            this.labelMaintainer.Text = "Maintainer:";
            // 
            // groupBoxPackage
            // 
            this.groupBoxPackage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPackage.Controls.Add(this.label1);
            this.groupBoxPackage.Controls.Add(this.buttonReset);
            this.groupBoxPackage.Controls.Add(this.buttonPackage);
            this.groupBoxPackage.Controls.Add(this.pictureBoxPkg_72);
            this.groupBoxPackage.Controls.Add(this.pictureBoxSettings);
            this.groupBoxPackage.Controls.Add(this.pictureBoxPkg_256);
            this.groupBoxPackage.Location = new System.Drawing.Point(2, 1);
            this.groupBoxPackage.Name = "groupBoxPackage";
            this.groupBoxPackage.Size = new System.Drawing.Size(801, 196);
            this.groupBoxPackage.TabIndex = 38;
            this.groupBoxPackage.TabStop = false;
            this.groupBoxPackage.Text = "PACKAGE INFORMATION";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(451, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Package Icons:";
            // 
            // groupBoxURL
            // 
            this.groupBoxURL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxURL.Controls.Add(this.checkBoxMultiInstance);
            this.groupBoxURL.Controls.Add(this.comboBoxUrlType);
            this.groupBoxURL.Controls.Add(this.checkBoxSize);
            this.groupBoxURL.Controls.Add(this.buttonSave);
            this.groupBoxURL.Controls.Add(this.buttonCancel);
            this.groupBoxURL.Controls.Add(this.buttonDelete);
            this.groupBoxURL.Controls.Add(this.buttonEdit);
            this.groupBoxURL.Controls.Add(this.checkBoxAllUsers);
            this.groupBoxURL.Location = new System.Drawing.Point(2, 200);
            this.groupBoxURL.Name = "groupBoxURL";
            this.groupBoxURL.Size = new System.Drawing.Size(801, 440);
            this.groupBoxURL.TabIndex = 39;
            this.groupBoxURL.TabStop = false;
            this.groupBoxURL.Text = "URL INFORMATION";
            // 
            // comboBoxUrlType
            // 
            this.comboBoxUrlType.FormattingEnabled = true;
            this.comboBoxUrlType.Items.AddRange(new object[] {
            "Url",
            "Script",
            "WebApp"});
            this.comboBoxUrlType.Location = new System.Drawing.Point(5, 246);
            this.comboBoxUrlType.Name = "comboBoxUrlType";
            this.comboBoxUrlType.Size = new System.Drawing.Size(65, 21);
            this.comboBoxUrlType.TabIndex = 17;
            this.comboBoxUrlType.SelectedIndexChanged += new System.EventHandler(this.comboBoxUrlType_SelectedIndexChanged);
            // 
            // checkBoxSize
            // 
            this.checkBoxSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxSize.AutoSize = true;
            this.checkBoxSize.Checked = true;
            this.checkBoxSize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSize.Location = new System.Drawing.Point(615, 155);
            this.checkBoxSize.Name = "checkBoxSize";
            this.checkBoxSize.Size = new System.Drawing.Size(169, 17);
            this.checkBoxSize.TabIndex = 0;
            this.checkBoxSize.Text = "Use the same Icon for all sizes";
            this.checkBoxSize.UseVisualStyleBackColor = true;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(261, 24);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(45, 13);
            this.labelVersion.TabIndex = 49;
            this.labelVersion.Text = "Version:";
            // 
            // labelDSMAppName
            // 
            this.labelDSMAppName.AutoSize = true;
            this.labelDSMAppName.Location = new System.Drawing.Point(243, 76);
            this.labelDSMAppName.Name = "labelDSMAppName";
            this.labelDSMAppName.Size = new System.Drawing.Size(63, 13);
            this.labelDSMAppName.TabIndex = 53;
            this.labelDSMAppName.Text = "DSM name:";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // checkBoxMultiInstance
            // 
            this.checkBoxMultiInstance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMultiInstance.AutoSize = true;
            this.checkBoxMultiInstance.Checked = true;
            this.checkBoxMultiInstance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMultiInstance.Location = new System.Drawing.Point(205, 155);
            this.checkBoxMultiInstance.Name = "checkBoxMultiInstance";
            this.checkBoxMultiInstance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxMultiInstance.Size = new System.Drawing.Size(95, 17);
            this.checkBoxMultiInstance.TabIndex = 18;
            this.checkBoxMultiInstance.Text = ":Multi Instance";
            this.checkBoxMultiInstance.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 642);
            this.Controls.Add(this.labelDSMAppName);
            this.Controls.Add(this.textBoxDsmAppName);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.textBoxVersion);
            this.Controls.Add(this.labelMaintainer);
            this.Controls.Add(this.textBoxMaintainer);
            this.Controls.Add(this.labelDisplay);
            this.Controls.Add(this.textBoxDisplay);
            this.Controls.Add(this.labelMaintainerUrl);
            this.Controls.Add(this.textBoxMaintainerUrl);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelPackage);
            this.Controls.Add(this.textBoxPackage);
            this.Controls.Add(this.pictureBox_256);
            this.Controls.Add(this.pictureBox_128);
            this.Controls.Add(this.pictureBox_96);
            this.Controls.Add(this.pictureBox_72);
            this.Controls.Add(this.pictureBox_64);
            this.Controls.Add(this.pictureBox_48);
            this.Controls.Add(this.pictureBox_32);
            this.Controls.Add(this.pictureBox_24);
            this.Controls.Add(this.pictureBox_16);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textBoxUrl);
            this.Controls.Add(this.labelDesc);
            this.Controls.Add(this.textBoxDesc);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.listViewUrls);
            this.Controls.Add(this.groupBoxURL);
            this.Controls.Add(this.groupBoxPackage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(826, 681);
            this.Name = "MainForm";
            this.Text = "Mods Package Creator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_256)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_128)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_96)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_72)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_64)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_48)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPkg_256)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPkg_72)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSettings)).EndInit();
            this.groupBoxPackage.ResumeLayout(false);
            this.groupBoxPackage.PerformLayout();
            this.groupBoxURL.ResumeLayout(false);
            this.groupBoxURL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPackage;
        private System.Windows.Forms.ListView listViewUrls;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.TextBox textBoxDesc;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.CheckBox checkBoxAllUsers;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.PictureBox pictureBox_16;
        private System.Windows.Forms.PictureBox pictureBox_24;
        private System.Windows.Forms.PictureBox pictureBox_32;
        private System.Windows.Forms.PictureBox pictureBox_48;
        private System.Windows.Forms.PictureBox pictureBox_64;
        private System.Windows.Forms.PictureBox pictureBox_72;
        private System.Windows.Forms.PictureBox pictureBox_96;
        private System.Windows.Forms.PictureBox pictureBox_128;
        private System.Windows.Forms.PictureBox pictureBox_256;
        private System.Windows.Forms.ToolTip toolTip4Mods;
        private System.Windows.Forms.OpenFileDialog openFileDialog4Mods;
        private System.Windows.Forms.PictureBox pictureBoxSettings;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog4Mods;
        private System.Windows.Forms.FolderBrowserDialog webpageBrowserDialog4Mods;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.PictureBox pictureBoxPkg_256;
        private System.Windows.Forms.PictureBox pictureBoxPkg_72;
        private System.Windows.Forms.Label labelMaintainerUrl;
        private System.Windows.Forms.TextBox textBoxMaintainerUrl;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelPackage;
        private System.Windows.Forms.TextBox textBoxPackage;
        private System.Windows.Forms.Label labelDisplay;
        private System.Windows.Forms.TextBox textBoxDisplay;
        private System.Windows.Forms.Label labelMaintainer;
        private System.Windows.Forms.TextBox textBoxMaintainer;
        private System.Windows.Forms.GroupBox groupBoxPackage;
        private System.Windows.Forms.GroupBox groupBoxURL;
        private System.Windows.Forms.CheckBox checkBoxSize;
        private System.Windows.Forms.TextBox textBoxDsmAppName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TextBox textBoxVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelDSMAppName;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ComboBox comboBoxUrlType;
        private System.Windows.Forms.CheckBox checkBoxMultiInstance;
    }
}


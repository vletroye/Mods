using System.Drawing;

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
            this.toolTip4Mods = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxMaintainerUrl = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxPackage = new System.Windows.Forms.TextBox();
            this.textBoxDisplay = new System.Windows.Forms.TextBox();
            this.textBoxMaintainer = new System.Windows.Forms.TextBox();
            this.textBoxVersion = new System.Windows.Forms.TextBox();
            this.textBoxDsmAppName = new System.Windows.Forms.TextBox();
            this.textBoxItem = new System.Windows.Forms.TextBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.buttonPackage = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.labelTransparency = new System.Windows.Forms.Label();
            this.textBoxPublisher = new System.Windows.Forms.TextBox();
            this.pictureBox_256 = new System.Windows.Forms.PictureBox();
            this.pictureBox_128 = new System.Windows.Forms.PictureBox();
            this.pictureBox_96 = new System.Windows.Forms.PictureBox();
            this.pictureBox_72 = new System.Windows.Forms.PictureBox();
            this.pictureBox_64 = new System.Windows.Forms.PictureBox();
            this.pictureBox_48 = new System.Windows.Forms.PictureBox();
            this.pictureBox_32 = new System.Windows.Forms.PictureBox();
            this.pictureBox_24 = new System.Windows.Forms.PictureBox();
            this.pictureBox_16 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPkg_72 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPkg_256 = new System.Windows.Forms.PictureBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.checkBoxAllUsers = new System.Windows.Forms.CheckBox();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.checkBoxSize = new System.Windows.Forms.CheckBox();
            this.checkBoxMultiInstance = new System.Windows.Forms.CheckBox();
            this.comboBoxTransparency = new System.Windows.Forms.ComboBox();
            this.textBoxPublisherUrl = new System.Windows.Forms.TextBox();
            this.textBoxHelpUrl = new System.Windows.Forms.TextBox();
            this.TextBoxReportUrl = new System.Windows.Forms.TextBox();
            this.textBoxFirmware = new System.Windows.Forms.TextBox();
            this.buttonAdvanced = new System.Windows.Forms.Button();
            this.openFileDialog4Mods = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelMaintainerUrl = new System.Windows.Forms.Label();
            this.labelPackage = new System.Windows.Forms.Label();
            this.labelDisplay = new System.Windows.Forms.Label();
            this.labelMaintainer = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelDSMAppName = new System.Windows.Forms.Label();
            this.groupBoxPackage = new System.Windows.Forms.GroupBox();
            this.labelFirmware = new System.Windows.Forms.Label();
            this.checkBoxBeta = new System.Windows.Forms.CheckBox();
            this.labelHelpUrl = new System.Windows.Forms.Label();
            this.labelReportUrl = new System.Windows.Forms.Label();
            this.labelPublisherUrl = new System.Windows.Forms.Label();
            this.labelPublisher = new System.Windows.Forms.Label();
            this.listViewItems = new System.Windows.Forms.ListView();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelDesc = new System.Windows.Forms.Label();
            this.comboBoxItemType = new System.Windows.Forms.ComboBox();
            this.groupBoxItem = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addWizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptRunnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.startScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postInstallScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preUninstallScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postUninstallScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preUpgradeScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postUpgradeScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.wizardInstallUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wizardUninstallUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wizardUpgradeUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.packeDevGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxTip = new System.Windows.Forms.GroupBox();
            this.labelToolTip = new System.Windows.Forms.Label();
            //this.folderBrowserDialog4Mods = new Ionic.Utils.FolderBrowserDialogEx();
            //this.webpageBrowserDialog4Mods = new Ionic.Utils.FolderBrowserDialogEx();
            this.folderBrowserDialog4Mods = new OpenFolderDialog();
            this.webpageBrowserDialog4Mods = new OpenFolderDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_256)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_128)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_96)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_72)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_64)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_48)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPkg_72)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPkg_256)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBoxPackage.SuspendLayout();
            this.groupBoxItem.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBoxTip.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxMaintainerUrl
            // 
            this.textBoxMaintainerUrl.Location = new System.Drawing.Point(330, 72);
            this.textBoxMaintainerUrl.Name = "textBoxMaintainerUrl";
            this.textBoxMaintainerUrl.Size = new System.Drawing.Size(209, 20);
            this.textBoxMaintainerUrl.TabIndex = 6;
            this.textBoxMaintainerUrl.Tag = "PKGmaintainer_url";
            this.toolTip4Mods.SetToolTip(this.textBoxMaintainerUrl, "Optional: Type here the url of developer\'s website.\r\nThis url will be accessible " +
        "by clicking on developer\'s name in DSM\'s Package Center.");
            this.textBoxMaintainerUrl.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxMaintainerUrl_Validating);
            this.textBoxMaintainerUrl.Validated += new System.EventHandler(this.textBoxMaintainerUrl_Validated);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(102, 124);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxDescription.Size = new System.Drawing.Size(437, 60);
            this.textBoxDescription.TabIndex = 9;
            this.textBoxDescription.Tag = "PKGdescription_enu;PKGdescription";
            this.toolTip4Mods.SetToolTip(this.textBoxDescription, "Enter a description of your package.\r\nThis will be displayed in DSM\'s Package Cen" +
        "ter.");
            this.textBoxDescription.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDescription_Validating);
            this.textBoxDescription.Validated += new System.EventHandler(this.textBoxDescription_Validated);
            // 
            // textBoxPackage
            // 
            this.textBoxPackage.Location = new System.Drawing.Point(102, 20);
            this.textBoxPackage.Name = "textBoxPackage";
            this.textBoxPackage.Size = new System.Drawing.Size(141, 20);
            this.textBoxPackage.TabIndex = 1;
            this.textBoxPackage.Tag = "PKGpackage";
            this.toolTip4Mods.SetToolTip(this.textBoxPackage, "Enter a name for your Package. This name may not include any special character or" +
        " blanks.\r\nPackage Center will create a /var/packages/[package identity] folder t" +
        "o put package files.");
            this.textBoxPackage.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxPackage_Validating);
            this.textBoxPackage.Validated += new System.EventHandler(this.textBoxPackage_Validated);
            // 
            // textBoxDisplay
            // 
            this.textBoxDisplay.Location = new System.Drawing.Point(102, 46);
            this.textBoxDisplay.Name = "textBoxDisplay";
            this.textBoxDisplay.Size = new System.Drawing.Size(141, 20);
            this.textBoxDisplay.TabIndex = 3;
            this.textBoxDisplay.Tag = "PKGdisplayname";
            this.toolTip4Mods.SetToolTip(this.textBoxDisplay, "Enter the name to be displayed on DSM for your package.\r\nIf you don\'t ener a Disp" +
        "lay name, the Package name will be displayed.");
            this.textBoxDisplay.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDisplay_Validating);
            this.textBoxDisplay.Validated += new System.EventHandler(this.textBoxDisplay_Validated);
            // 
            // textBoxMaintainer
            // 
            this.textBoxMaintainer.Location = new System.Drawing.Point(102, 72);
            this.textBoxMaintainer.Name = "textBoxMaintainer";
            this.textBoxMaintainer.Size = new System.Drawing.Size(141, 20);
            this.textBoxMaintainer.TabIndex = 5;
            this.textBoxMaintainer.Tag = "PKGmaintainer";
            this.toolTip4Mods.SetToolTip(this.textBoxMaintainer, "Enter the name of the person who developped the items in your package.\r\nThis name" +
        " will be displayed in DSM\'s Package Center.");
            this.textBoxMaintainer.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxMaintainer_Validating);
            this.textBoxMaintainer.Validated += new System.EventHandler(this.textBoxMaintainer_Validated);
            // 
            // textBoxVersion
            // 
            this.textBoxVersion.Location = new System.Drawing.Point(330, 20);
            this.textBoxVersion.Name = "textBoxVersion";
            this.textBoxVersion.Size = new System.Drawing.Size(73, 20);
            this.textBoxVersion.TabIndex = 2;
            this.textBoxVersion.Tag = "PKGversion";
            this.toolTip4Mods.SetToolTip(this.textBoxVersion, resources.GetString("textBoxVersion.ToolTip"));
            this.textBoxVersion.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxVersion_Validating);
            this.textBoxVersion.Validated += new System.EventHandler(this.textBoxVersion_Validated);
            // 
            // textBoxDsmAppName
            // 
            this.textBoxDsmAppName.Location = new System.Drawing.Point(330, 46);
            this.textBoxDsmAppName.Name = "textBoxDsmAppName";
            this.textBoxDsmAppName.Size = new System.Drawing.Size(209, 20);
            this.textBoxDsmAppName.TabIndex = 4;
            this.textBoxDsmAppName.Tag = "PKGdsmappname";
            this.toolTip4Mods.SetToolTip(this.textBoxDsmAppName, "Enter an application name for your package. Ex.: com.yourSite.yourPackageName\r\nTh" +
        "is name must be unique in the universe and will be used behind the scene when in" +
        "stalling your package on DSM.");
            this.textBoxDsmAppName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDsmAppName_Validating);
            this.textBoxDsmAppName.Validated += new System.EventHandler(this.textBoxDsmAppName_Validated);
            // 
            // textBoxItem
            // 
            this.textBoxItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItem.Location = new System.Drawing.Point(91, 217);
            this.textBoxItem.Name = "textBoxItem";
            this.textBoxItem.Size = new System.Drawing.Size(286, 20);
            this.textBoxItem.TabIndex = 5;
            this.toolTip4Mods.SetToolTip(this.textBoxItem, "Select a Type first");
            this.textBoxItem.DoubleClick += new System.EventHandler(this.textBoxItem_DoubleClick);
            this.textBoxItem.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxItem_Validating);
            this.textBoxItem.Validated += new System.EventHandler(this.textBoxItem_Validated);
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxTitle.Location = new System.Drawing.Point(91, 123);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(108, 20);
            this.textBoxTitle.TabIndex = 1;
            this.toolTip4Mods.SetToolTip(this.textBoxTitle, "Enter the title of the Item. It will to be displayed on DSM.");
            this.textBoxTitle.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxTitle_Validating);
            this.textBoxTitle.Validated += new System.EventHandler(this.textBoxTitle_Validated);
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDesc.Location = new System.Drawing.Point(91, 149);
            this.textBoxDesc.Multiline = true;
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxDesc.Size = new System.Drawing.Size(388, 62);
            this.textBoxDesc.TabIndex = 3;
            this.toolTip4Mods.SetToolTip(this.textBoxDesc, "Enter an optional description. This will not be displayed on DSM.");
            // 
            // buttonPackage
            // 
            this.buttonPackage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPackage.Location = new System.Drawing.Point(715, 160);
            this.buttonPackage.Name = "buttonPackage";
            this.buttonPackage.Size = new System.Drawing.Size(72, 23);
            this.buttonPackage.TabIndex = 16;
            this.buttonPackage.Text = "Package";
            this.toolTip4Mods.SetToolTip(this.buttonPackage, "Generate the Package.\r\nAll changes done in the \"Package Information\" pane will be" +
        " saved.");
            this.buttonPackage.UseVisualStyleBackColor = true;
            this.buttonPackage.Click += new System.EventHandler(this.buttonPackage_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReset.CausesValidation = false;
            this.buttonReset.Location = new System.Drawing.Point(715, 129);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(72, 23);
            this.buttonReset.TabIndex = 15;
            this.buttonReset.Text = "Reset";
            this.toolTip4Mods.SetToolTip(this.buttonReset, "Reset the Package to Dummy values.");
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // labelTransparency
            // 
            this.labelTransparency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTransparency.AutoSize = true;
            this.labelTransparency.Location = new System.Drawing.Point(654, 321);
            this.labelTransparency.Name = "labelTransparency";
            this.labelTransparency.Size = new System.Drawing.Size(75, 13);
            this.labelTransparency.TabIndex = 25;
            this.labelTransparency.Text = "Transparency:";
            this.toolTip4Mods.SetToolTip(this.labelTransparency, "Select a range of color around the color of pixel (1,1) to be made transparent.");
            // 
            // textBoxPublisher
            // 
            this.textBoxPublisher.Location = new System.Drawing.Point(102, 98);
            this.textBoxPublisher.Name = "textBoxPublisher";
            this.textBoxPublisher.Size = new System.Drawing.Size(141, 20);
            this.textBoxPublisher.TabIndex = 7;
            this.textBoxPublisher.Tag = "PKGdistributor";
            this.toolTip4Mods.SetToolTip(this.textBoxPublisher, "Enter the name of the person who create the package.\r\nThis name will be displayed" +
        " in DSM\'s Package Center.");
            this.textBoxPublisher.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxPublisher_Validating);
            this.textBoxPublisher.Validated += new System.EventHandler(this.textBoxPublisher_Validated);
            // 
            // pictureBox_256
            // 
            this.pictureBox_256.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_256.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_256.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_256.Location = new System.Drawing.Point(619, 149);
            this.pictureBox_256.Name = "pictureBox_256";
            this.pictureBox_256.Size = new System.Drawing.Size(164, 164);
            this.pictureBox_256.TabIndex = 22;
            this.pictureBox_256.TabStop = false;
            this.pictureBox_256.Tag = "ITEM;256;MAX";
            this.toolTip4Mods.SetToolTip(this.pictureBox_256, "Drop here a logo 256x256. If you drop a larger or small logo, it will be resized " +
        "automatically. \r\nPay attention that dropping a smaller logo can result in a poor" +
        " quality image.");
            // 
            // pictureBox_128
            // 
            this.pictureBox_128.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_128.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_128.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_128.Location = new System.Drawing.Point(485, 185);
            this.pictureBox_128.Name = "pictureBox_128";
            this.pictureBox_128.Size = new System.Drawing.Size(128, 128);
            this.pictureBox_128.TabIndex = 21;
            this.pictureBox_128.TabStop = false;
            this.pictureBox_128.Tag = "ITEM;128";
            this.toolTip4Mods.SetToolTip(this.pictureBox_128, "Drop here a logo 128x128. If you drop a larger or small logo, it will be resized " +
        "automatically. \r\nPay attention that dropping a smaller logo can result in a poor" +
        " quality image.");
            // 
            // pictureBox_96
            // 
            this.pictureBox_96.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_96.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_96.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_96.Location = new System.Drawing.Point(383, 217);
            this.pictureBox_96.Name = "pictureBox_96";
            this.pictureBox_96.Size = new System.Drawing.Size(96, 96);
            this.pictureBox_96.TabIndex = 20;
            this.pictureBox_96.TabStop = false;
            this.pictureBox_96.Tag = "ITEM;96";
            this.toolTip4Mods.SetToolTip(this.pictureBox_96, "Drop here a logo 96x96. If you drop a larger or small logo, it will be resized au" +
        "tomatically. \r\nPay attention that dropping a smaller logo can result in a poor q" +
        "uality image.");
            // 
            // pictureBox_72
            // 
            this.pictureBox_72.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_72.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_72.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_72.Location = new System.Drawing.Point(305, 241);
            this.pictureBox_72.Name = "pictureBox_72";
            this.pictureBox_72.Size = new System.Drawing.Size(72, 72);
            this.pictureBox_72.TabIndex = 19;
            this.pictureBox_72.TabStop = false;
            this.pictureBox_72.Tag = "ITEM;72";
            this.toolTip4Mods.SetToolTip(this.pictureBox_72, "Drop here a logo 72x7200. If you drop a larger or small logo, it will be resized " +
        "automatically. \r\nPay attention that dropping a smaller logo can result in a poor" +
        " quality image.");
            // 
            // pictureBox_64
            // 
            this.pictureBox_64.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_64.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_64.Location = new System.Drawing.Point(235, 249);
            this.pictureBox_64.Name = "pictureBox_64";
            this.pictureBox_64.Size = new System.Drawing.Size(64, 64);
            this.pictureBox_64.TabIndex = 18;
            this.pictureBox_64.TabStop = false;
            this.pictureBox_64.Tag = "ITEM;64";
            this.toolTip4Mods.SetToolTip(this.pictureBox_64, "Drop here a logo 64x64. If you drop a larger or small logo, it will be resized au" +
        "tomatically. \r\nPay attention that dropping a smaller logo can result in a poor q" +
        "uality image.");
            // 
            // pictureBox_48
            // 
            this.pictureBox_48.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_48.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_48.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_48.Location = new System.Drawing.Point(181, 265);
            this.pictureBox_48.Name = "pictureBox_48";
            this.pictureBox_48.Size = new System.Drawing.Size(48, 48);
            this.pictureBox_48.TabIndex = 17;
            this.pictureBox_48.TabStop = false;
            this.pictureBox_48.Tag = "ITEM;48";
            this.toolTip4Mods.SetToolTip(this.pictureBox_48, "Drop here a logo 48x48. If you drop a larger or small logo, it will be resized au" +
        "tomatically. \r\nPay attention that dropping a smaller logo can result in a poor q" +
        "uality image.");
            // 
            // pictureBox_32
            // 
            this.pictureBox_32.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_32.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_32.Location = new System.Drawing.Point(143, 281);
            this.pictureBox_32.Name = "pictureBox_32";
            this.pictureBox_32.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_32.TabIndex = 16;
            this.pictureBox_32.TabStop = false;
            this.pictureBox_32.Tag = "ITEM;32";
            this.toolTip4Mods.SetToolTip(this.pictureBox_32, "Drop here a logo 32x32. If you drop a larger or small logo, it will be resized au" +
        "tomatically. \r\nPay attention that dropping a smaller logo can result in a poor q" +
        "uality image.");
            // 
            // pictureBox_24
            // 
            this.pictureBox_24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_24.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_24.Location = new System.Drawing.Point(113, 289);
            this.pictureBox_24.Name = "pictureBox_24";
            this.pictureBox_24.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_24.TabIndex = 15;
            this.pictureBox_24.TabStop = false;
            this.pictureBox_24.Tag = "ITEM;24";
            this.toolTip4Mods.SetToolTip(this.pictureBox_24, "Drop here a logo 24x24. If you drop a larger or small logo, it will be resized au" +
        "tomatically. \r\nPay attention that dropping a smaller logo can result in a poor q" +
        "uality image.");
            // 
            // pictureBox_16
            // 
            this.pictureBox_16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_16.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_16.Location = new System.Drawing.Point(91, 297);
            this.pictureBox_16.Name = "pictureBox_16";
            this.pictureBox_16.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_16.TabIndex = 14;
            this.pictureBox_16.TabStop = false;
            this.pictureBox_16.Tag = "ITEM;16";
            this.toolTip4Mods.SetToolTip(this.pictureBox_16, "Drop here a logo 16x16. If you drop a larger or small logo, it will be resized au" +
        "tomatically. \r\nPay attention that dropping a smaller logo can result in a poor q" +
        "uality image.");
            // 
            // pictureBoxPkg_72
            // 
            this.pictureBoxPkg_72.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxPkg_72.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPkg_72.Location = new System.Drawing.Point(715, 20);
            this.pictureBoxPkg_72.Name = "pictureBoxPkg_72";
            this.pictureBoxPkg_72.Size = new System.Drawing.Size(72, 72);
            this.pictureBoxPkg_72.TabIndex = 26;
            this.pictureBoxPkg_72.TabStop = false;
            this.pictureBoxPkg_72.Tag = "Pkg;72";
            this.toolTip4Mods.SetToolTip(this.pictureBoxPkg_72, "Drop here a logo 72x72 to be used for this package when listed by DMS\'s Package C" +
        "enter.");
            this.pictureBoxPkg_72.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBoxPkg_DragDrop);
            this.pictureBoxPkg_72.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBoxPkg_DragEnter);
            this.pictureBoxPkg_72.DoubleClick += new System.EventHandler(this.pictureBoxPkg_DoubleClick);
            // 
            // pictureBoxPkg_256
            // 
            this.pictureBoxPkg_256.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxPkg_256.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPkg_256.Location = new System.Drawing.Point(545, 20);
            this.pictureBoxPkg_256.Name = "pictureBoxPkg_256";
            this.pictureBoxPkg_256.Size = new System.Drawing.Size(164, 164);
            this.pictureBoxPkg_256.TabIndex = 27;
            this.pictureBoxPkg_256.TabStop = false;
            this.pictureBoxPkg_256.Tag = "Pkg;256";
            this.toolTip4Mods.SetToolTip(this.pictureBoxPkg_256, "Drop here a logo 256x256 to be used for this package when DMS\'s Package Center di" +
        "splays its details.");
            this.pictureBoxPkg_256.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBoxPkg_DragDrop);
            this.pictureBoxPkg_256.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBoxPkg_DragEnter);
            this.pictureBoxPkg_256.DoubleClick += new System.EventHandler(this.pictureBoxPkg_DoubleClick);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Location = new System.Drawing.Point(6, 318);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 8;
            this.buttonAdd.Text = "Add";
            this.toolTip4Mods.SetToolTip(this.buttonAdd, "Add a new item in the package.");
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAddItem_Click);
            // 
            // checkBoxAllUsers
            // 
            this.checkBoxAllUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxAllUsers.AutoSize = true;
            this.checkBoxAllUsers.Location = new System.Drawing.Point(20, 267);
            this.checkBoxAllUsers.Name = "checkBoxAllUsers";
            this.checkBoxAllUsers.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxAllUsers.Size = new System.Drawing.Size(85, 17);
            this.checkBoxAllUsers.TabIndex = 7;
            this.checkBoxAllUsers.Text = "For All Users";
            this.toolTip4Mods.SetToolTip(this.checkBoxAllUsers, "If you select this item, all users defined on your NAS will have access to the it" +
        "em.\r\nIf you don\'t select this item, only the user who installed the package will" +
        " have access to the item.");
            this.checkBoxAllUsers.UseVisualStyleBackColor = true;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEdit.Location = new System.Drawing.Point(168, 318);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 10;
            this.buttonEdit.Text = "Edit";
            this.toolTip4Mods.SetToolTip(this.buttonEdit, "Edit the item currently selected in the list above.");
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEditItem_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelete.Location = new System.Drawing.Point(87, 318);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 9;
            this.buttonDelete.Text = "Delete";
            this.toolTip4Mods.SetToolTip(this.buttonDelete, "Delete the item currently selected in the list above.");
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDeleteItem_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.Location = new System.Drawing.Point(249, 318);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.toolTip4Mods.SetToolTip(this.buttonCancel, "Cancel all changes done on the item currently edited.");
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancelItem_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.Location = new System.Drawing.Point(330, 318);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Save";
            this.toolTip4Mods.SetToolTip(this.buttonSave, "Save all changes done on the item currently edited.\r\nNB.: This is not saving the " +
        "changes done in the \'Package Information\' pane.");
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSaveItem_Click);
            // 
            // checkBoxSize
            // 
            this.checkBoxSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxSize.AutoSize = true;
            this.checkBoxSize.Checked = true;
            this.checkBoxSize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSize.Location = new System.Drawing.Point(619, 122);
            this.checkBoxSize.Name = "checkBoxSize";
            this.checkBoxSize.Size = new System.Drawing.Size(131, 17);
            this.checkBoxSize.TabIndex = 2;
            this.checkBoxSize.Text = "Same Icon for all sizes";
            this.toolTip4Mods.SetToolTip(this.checkBoxSize, resources.GetString("checkBoxSize.ToolTip"));
            this.checkBoxSize.UseVisualStyleBackColor = true;
            this.checkBoxSize.CheckedChanged += new System.EventHandler(this.checkBoxSize_CheckedChanged);
            // 
            // checkBoxMultiInstance
            // 
            this.checkBoxMultiInstance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxMultiInstance.AutoSize = true;
            this.checkBoxMultiInstance.Checked = true;
            this.checkBoxMultiInstance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMultiInstance.Location = new System.Drawing.Point(13, 244);
            this.checkBoxMultiInstance.Name = "checkBoxMultiInstance";
            this.checkBoxMultiInstance.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxMultiInstance.Size = new System.Drawing.Size(92, 17);
            this.checkBoxMultiInstance.TabIndex = 6;
            this.checkBoxMultiInstance.Text = "Multi Instance";
            this.toolTip4Mods.SetToolTip(this.checkBoxMultiInstance, "When you select this option, the item can be opened several times simulteanously " +
        "in DSM.\r\nIf you don\'t select this option, you will only ba able to run once inst" +
        "ance of the item.");
            this.checkBoxMultiInstance.UseVisualStyleBackColor = true;
            // 
            // comboBoxTransparency
            // 
            this.comboBoxTransparency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxTransparency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTransparency.FormattingEnabled = true;
            this.comboBoxTransparency.Items.AddRange(new object[] {
            "0",
            "1",
            "5",
            "10",
            "15",
            "20",
            "25",
            "30"});
            this.comboBoxTransparency.Location = new System.Drawing.Point(735, 318);
            this.comboBoxTransparency.Name = "comboBoxTransparency";
            this.comboBoxTransparency.Size = new System.Drawing.Size(48, 21);
            this.comboBoxTransparency.TabIndex = 13;
            this.toolTip4Mods.SetToolTip(this.comboBoxTransparency, "When importing a new image, you will be asked if it must be made transparent. Ans" +
        "wering \'yes\', all colors equal to image\'s pixel (1,1) +/- this value will be set" +
        " as transparent.");
            // 
            // textBoxPublisherUrl
            // 
            this.textBoxPublisherUrl.Location = new System.Drawing.Point(330, 98);
            this.textBoxPublisherUrl.Name = "textBoxPublisherUrl";
            this.textBoxPublisherUrl.Size = new System.Drawing.Size(209, 20);
            this.textBoxPublisherUrl.TabIndex = 8;
            this.textBoxPublisherUrl.Tag = "PKGdistributor_url";
            this.toolTip4Mods.SetToolTip(this.textBoxPublisherUrl, "Optional: Type here the url of publisher\'s website.\r\nThis url will be accessible " +
        "by clicking on publisher\'s name in DSM\'s Package Center.\r\n");
            this.textBoxPublisherUrl.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxPublisherUrl_Validating);
            this.textBoxPublisherUrl.Validated += new System.EventHandler(this.textBoxPublisherUrl_Validated);
            // 
            // textBoxHelpUrl
            // 
            this.textBoxHelpUrl.Location = new System.Drawing.Point(334, 190);
            this.textBoxHelpUrl.Name = "textBoxHelpUrl";
            this.textBoxHelpUrl.Size = new System.Drawing.Size(453, 20);
            this.textBoxHelpUrl.TabIndex = 11;
            this.textBoxHelpUrl.Tag = "PKGhelpurl";
            this.toolTip4Mods.SetToolTip(this.textBoxHelpUrl, "Optional: Type here the url of publisher\'s website.\r\nThis url will be accessible " +
        "by clicking on publisher\'s name in DSM\'s Package Center.\r\n");
            this.textBoxHelpUrl.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxHelpUrl_Validating);
            this.textBoxHelpUrl.Validated += new System.EventHandler(this.textBoxHelpUrl_Validated);
            // 
            // TextBoxReportUrl
            // 
            this.TextBoxReportUrl.Location = new System.Drawing.Point(334, 219);
            this.TextBoxReportUrl.Name = "TextBoxReportUrl";
            this.TextBoxReportUrl.Size = new System.Drawing.Size(453, 20);
            this.TextBoxReportUrl.TabIndex = 13;
            this.TextBoxReportUrl.Tag = "PKGreport_url";
            this.toolTip4Mods.SetToolTip(this.TextBoxReportUrl, "Optional: Type here the url of developer\'s website.\r\nThis url will be accessible " +
        "by clicking on developer\'s name in DSM\'s Package Center.");
            this.TextBoxReportUrl.Visible = false;
            this.TextBoxReportUrl.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxReportUrl_Validating);
            this.TextBoxReportUrl.Validated += new System.EventHandler(this.TextBoxReportUrl_Validated);
            // 
            // textBoxFirmware
            // 
            this.textBoxFirmware.Location = new System.Drawing.Point(102, 190);
            this.textBoxFirmware.Name = "textBoxFirmware";
            this.textBoxFirmware.Size = new System.Drawing.Size(73, 20);
            this.textBoxFirmware.TabIndex = 10;
            this.textBoxFirmware.Tag = "PKGfirmware";
            this.toolTip4Mods.SetToolTip(this.textBoxFirmware, "Earliest version of DSM firmware that is required to run the package.\r\nValue: X.Y" +
        "-Z DSM major number, DSM minor number, DSM build number");
            this.textBoxFirmware.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxFirmware_Validating);
            this.textBoxFirmware.Validated += new System.EventHandler(this.textBoxFirmware_Validated);
            // 
            // buttonAdvanced
            // 
            this.buttonAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdvanced.Location = new System.Drawing.Point(715, 98);
            this.buttonAdvanced.Name = "buttonAdvanced";
            this.buttonAdvanced.Size = new System.Drawing.Size(72, 23);
            this.buttonAdvanced.TabIndex = 14;
            this.buttonAdvanced.Text = "Advanced";
            this.toolTip4Mods.SetToolTip(this.buttonAdvanced, "Edit Advanced Optional Fields.");
            this.buttonAdvanced.UseVisualStyleBackColor = true;
            this.buttonAdvanced.Click += new System.EventHandler(this.buttonAdvanced_Click);
            // 
            // openFileDialog4Mods
            // 
            this.openFileDialog4Mods.Filter = "Png|*.png";
            this.openFileDialog4Mods.RestoreDirectory = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(36, 127);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(63, 13);
            this.labelDescription.TabIndex = 31;
            this.labelDescription.Text = "Description:";
            // 
            // labelMaintainerUrl
            // 
            this.labelMaintainerUrl.AutoSize = true;
            this.labelMaintainerUrl.Location = new System.Drawing.Point(245, 75);
            this.labelMaintainerUrl.Name = "labelMaintainerUrl";
            this.labelMaintainerUrl.Size = new System.Drawing.Size(82, 13);
            this.labelMaintainerUrl.TabIndex = 33;
            this.labelMaintainerUrl.Text = "Developer\'s Url:";
            this.labelMaintainerUrl.Click += new System.EventHandler(this.labelMaintainerUrl_Click);
            // 
            // labelPackage
            // 
            this.labelPackage.AutoSize = true;
            this.labelPackage.Location = new System.Drawing.Point(15, 23);
            this.labelPackage.Name = "labelPackage";
            this.labelPackage.Size = new System.Drawing.Size(84, 13);
            this.labelPackage.TabIndex = 29;
            this.labelPackage.Text = "Package Name:";
            // 
            // labelDisplay
            // 
            this.labelDisplay.AutoSize = true;
            this.labelDisplay.Location = new System.Drawing.Point(24, 49);
            this.labelDisplay.Name = "labelDisplay";
            this.labelDisplay.Size = new System.Drawing.Size(75, 13);
            this.labelDisplay.TabIndex = 35;
            this.labelDisplay.Text = "Display Name:";
            // 
            // labelMaintainer
            // 
            this.labelMaintainer.AutoSize = true;
            this.labelMaintainer.Location = new System.Drawing.Point(2, 75);
            this.labelMaintainer.Name = "labelMaintainer";
            this.labelMaintainer.Size = new System.Drawing.Size(97, 13);
            this.labelMaintainer.TabIndex = 37;
            this.labelMaintainer.Text = "Developer\'s Name:";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(282, 23);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(45, 13);
            this.labelVersion.TabIndex = 49;
            this.labelVersion.Text = "Version:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(457, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Package Icons:";
            // 
            // labelDSMAppName
            // 
            this.labelDSMAppName.AutoSize = true;
            this.labelDSMAppName.Location = new System.Drawing.Point(264, 50);
            this.labelDSMAppName.Name = "labelDSMAppName";
            this.labelDSMAppName.Size = new System.Drawing.Size(63, 13);
            this.labelDSMAppName.TabIndex = 53;
            this.labelDSMAppName.Text = "DSM name:";
            // 
            // groupBoxPackage
            // 
            this.groupBoxPackage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPackage.Controls.Add(this.buttonAdvanced);
            this.groupBoxPackage.Controls.Add(this.labelFirmware);
            this.groupBoxPackage.Controls.Add(this.textBoxFirmware);
            this.groupBoxPackage.Controls.Add(this.checkBoxBeta);
            this.groupBoxPackage.Controls.Add(this.labelHelpUrl);
            this.groupBoxPackage.Controls.Add(this.textBoxHelpUrl);
            this.groupBoxPackage.Controls.Add(this.labelReportUrl);
            this.groupBoxPackage.Controls.Add(this.TextBoxReportUrl);
            this.groupBoxPackage.Controls.Add(this.labelPublisherUrl);
            this.groupBoxPackage.Controls.Add(this.textBoxPublisherUrl);
            this.groupBoxPackage.Controls.Add(this.labelPublisher);
            this.groupBoxPackage.Controls.Add(this.textBoxPublisher);
            this.groupBoxPackage.Controls.Add(this.labelDSMAppName);
            this.groupBoxPackage.Controls.Add(this.label1);
            this.groupBoxPackage.Controls.Add(this.textBoxDsmAppName);
            this.groupBoxPackage.Controls.Add(this.buttonReset);
            this.groupBoxPackage.Controls.Add(this.labelVersion);
            this.groupBoxPackage.Controls.Add(this.buttonPackage);
            this.groupBoxPackage.Controls.Add(this.textBoxVersion);
            this.groupBoxPackage.Controls.Add(this.pictureBoxPkg_72);
            this.groupBoxPackage.Controls.Add(this.labelMaintainer);
            this.groupBoxPackage.Controls.Add(this.textBoxMaintainer);
            this.groupBoxPackage.Controls.Add(this.pictureBoxPkg_256);
            this.groupBoxPackage.Controls.Add(this.labelDisplay);
            this.groupBoxPackage.Controls.Add(this.labelPackage);
            this.groupBoxPackage.Controls.Add(this.textBoxDisplay);
            this.groupBoxPackage.Controls.Add(this.textBoxPackage);
            this.groupBoxPackage.Controls.Add(this.labelMaintainerUrl);
            this.groupBoxPackage.Controls.Add(this.textBoxDescription);
            this.groupBoxPackage.Controls.Add(this.textBoxMaintainerUrl);
            this.groupBoxPackage.Controls.Add(this.labelDescription);
            this.groupBoxPackage.Location = new System.Drawing.Point(2, 37);
            this.groupBoxPackage.Name = "groupBoxPackage";
            this.groupBoxPackage.Size = new System.Drawing.Size(801, 275);
            this.groupBoxPackage.TabIndex = 38;
            this.groupBoxPackage.TabStop = false;
            this.groupBoxPackage.Text = "PACKAGE INFORMATION";
            // 
            // labelFirmware
            // 
            this.labelFirmware.AutoSize = true;
            this.labelFirmware.Location = new System.Drawing.Point(10, 194);
            this.labelFirmware.Name = "labelFirmware";
            this.labelFirmware.Size = new System.Drawing.Size(89, 13);
            this.labelFirmware.TabIndex = 64;
            this.labelFirmware.Text = "Earliest Firmware:";
            // 
            // checkBoxBeta
            // 
            this.checkBoxBeta.AutoSize = true;
            this.checkBoxBeta.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxBeta.Location = new System.Drawing.Point(32, 219);
            this.checkBoxBeta.Name = "checkBoxBeta";
            this.checkBoxBeta.Size = new System.Drawing.Size(86, 17);
            this.checkBoxBeta.TabIndex = 12;
            this.checkBoxBeta.Tag = "PKGbeta";
            this.checkBoxBeta.Text = "Beta Version";
            this.checkBoxBeta.UseVisualStyleBackColor = true;
            this.checkBoxBeta.CheckedChanged += new System.EventHandler(this.checkBoxBeta_CheckedChanged);
            // 
            // labelHelpUrl
            // 
            this.labelHelpUrl.AutoSize = true;
            this.labelHelpUrl.Location = new System.Drawing.Point(229, 194);
            this.labelHelpUrl.Name = "labelHelpUrl";
            this.labelHelpUrl.Size = new System.Drawing.Size(98, 13);
            this.labelHelpUrl.TabIndex = 61;
            this.labelHelpUrl.Text = "Help && How-To Url:";
            this.labelHelpUrl.Click += new System.EventHandler(this.labelHelpUrl_Click);
            // 
            // labelReportUrl
            // 
            this.labelReportUrl.AutoSize = true;
            this.labelReportUrl.Location = new System.Drawing.Point(271, 219);
            this.labelReportUrl.Name = "labelReportUrl";
            this.labelReportUrl.Size = new System.Drawing.Size(58, 13);
            this.labelReportUrl.TabIndex = 59;
            this.labelReportUrl.Text = "Report Url:";
            this.labelReportUrl.Visible = false;
            this.labelReportUrl.Click += new System.EventHandler(this.labelReportUrl_Click);
            // 
            // labelPublisherUrl
            // 
            this.labelPublisherUrl.AutoSize = true;
            this.labelPublisherUrl.Location = new System.Drawing.Point(251, 101);
            this.labelPublisherUrl.Name = "labelPublisherUrl";
            this.labelPublisherUrl.Size = new System.Drawing.Size(76, 13);
            this.labelPublisherUrl.TabIndex = 57;
            this.labelPublisherUrl.Text = "Publisher\'s Url:";
            this.labelPublisherUrl.Click += new System.EventHandler(this.labelPublisherUrl_Click);
            // 
            // labelPublisher
            // 
            this.labelPublisher.AutoSize = true;
            this.labelPublisher.Location = new System.Drawing.Point(8, 101);
            this.labelPublisher.Name = "labelPublisher";
            this.labelPublisher.Size = new System.Drawing.Size(91, 13);
            this.labelPublisher.TabIndex = 55;
            this.labelPublisher.Text = "Publisher\'s Name:";
            // 
            // listViewItems
            // 
            this.listViewItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewItems.Location = new System.Drawing.Point(14, 19);
            this.listViewItems.Name = "listViewItems";
            this.listViewItems.Size = new System.Drawing.Size(773, 98);
            this.listViewItems.TabIndex = 0;
            this.listViewItems.UseCompatibleStateImageBehavior = false;
            this.listViewItems.SelectedIndexChanged += new System.EventHandler(this.listViewItems_SelectedIndexChanged);
            this.listViewItems.DoubleClick += new System.EventHandler(this.listViewItems_DoubleClick);
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(52, 126);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(30, 13);
            this.labelTitle.TabIndex = 3;
            this.labelTitle.Text = "Title:";
            // 
            // labelDesc
            // 
            this.labelDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDesc.AutoSize = true;
            this.labelDesc.Location = new System.Drawing.Point(19, 149);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Size = new System.Drawing.Size(63, 13);
            this.labelDesc.TabIndex = 5;
            this.labelDesc.Text = "Description:";
            // 
            // comboBoxItemType
            // 
            this.comboBoxItemType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItemType.FormattingEnabled = true;
            this.comboBoxItemType.Items.AddRange(new object[] {
            "Url",
            "Script",
            "WebApp"});
            this.comboBoxItemType.Location = new System.Drawing.Point(24, 217);
            this.comboBoxItemType.Name = "comboBoxItemType";
            this.comboBoxItemType.Size = new System.Drawing.Size(58, 21);
            this.comboBoxItemType.TabIndex = 4;
            this.comboBoxItemType.SelectedIndexChanged += new System.EventHandler(this.comboBoxItemType_SelectedIndexChanged);
            // 
            // groupBoxItem
            // 
            this.groupBoxItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxItem.Controls.Add(this.labelTransparency);
            this.groupBoxItem.Controls.Add(this.comboBoxTransparency);
            this.groupBoxItem.Controls.Add(this.pictureBox_256);
            this.groupBoxItem.Controls.Add(this.checkBoxMultiInstance);
            this.groupBoxItem.Controls.Add(this.pictureBox_128);
            this.groupBoxItem.Controls.Add(this.comboBoxItemType);
            this.groupBoxItem.Controls.Add(this.pictureBox_96);
            this.groupBoxItem.Controls.Add(this.checkBoxSize);
            this.groupBoxItem.Controls.Add(this.pictureBox_72);
            this.groupBoxItem.Controls.Add(this.buttonSave);
            this.groupBoxItem.Controls.Add(this.pictureBox_64);
            this.groupBoxItem.Controls.Add(this.buttonCancel);
            this.groupBoxItem.Controls.Add(this.pictureBox_48);
            this.groupBoxItem.Controls.Add(this.buttonDelete);
            this.groupBoxItem.Controls.Add(this.pictureBox_32);
            this.groupBoxItem.Controls.Add(this.buttonEdit);
            this.groupBoxItem.Controls.Add(this.pictureBox_24);
            this.groupBoxItem.Controls.Add(this.checkBoxAllUsers);
            this.groupBoxItem.Controls.Add(this.pictureBox_16);
            this.groupBoxItem.Controls.Add(this.buttonAdd);
            this.groupBoxItem.Controls.Add(this.labelDesc);
            this.groupBoxItem.Controls.Add(this.textBoxDesc);
            this.groupBoxItem.Controls.Add(this.labelTitle);
            this.groupBoxItem.Controls.Add(this.textBoxTitle);
            this.groupBoxItem.Controls.Add(this.listViewItems);
            this.groupBoxItem.Controls.Add(this.textBoxItem);
            this.groupBoxItem.Location = new System.Drawing.Point(2, 318);
            this.groupBoxItem.Name = "groupBoxItem";
            this.groupBoxItem.Size = new System.Drawing.Size(801, 347);
            this.groupBoxItem.TabIndex = 39;
            this.groupBoxItem.TabStop = false;
            this.groupBoxItem.Text = "ITEM INFORMATION";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesToolStripMenuItem,
            this.packageToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(810, 24);
            this.menuStrip1.TabIndex = 40;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.openRecentToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.filesToolStripMenuItem.Text = "Files";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openRecentToolStripMenuItem
            // 
            this.openRecentToolStripMenuItem.Name = "openRecentToolStripMenuItem";
            this.openRecentToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openRecentToolStripMenuItem.Text = "Open Recent";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // packageToolStripMenuItem
            // 
            this.packageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.resetToolStripMenuItem,
            this.toolStripMenuItem1,
            this.generateToolStripMenuItem,
            this.addWizardToolStripMenuItem,
            this.openFolderToolStripMenuItem});
            this.packageToolStripMenuItem.Name = "packageToolStripMenuItem";
            this.packageToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.packageToolStripMenuItem.Text = "Package";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItem1.Text = "Delete";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.generateToolStripMenuItem.Text = "Generate";
            this.generateToolStripMenuItem.Click += new System.EventHandler(this.generateToolStripMenuItem_Click);
            // 
            // addWizardToolStripMenuItem
            // 
            this.addWizardToolStripMenuItem.Name = "addWizardToolStripMenuItem";
            this.addWizardToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.addWizardToolStripMenuItem.Text = "Create Wizard";
            this.addWizardToolStripMenuItem.Click += new System.EventHandler(this.addWizardToolStripMenuItem_Click);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.openFolderToolStripMenuItem.Text = "Open Folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scriptRunnerToolStripMenuItem,
            this.toolStripSeparator1,
            this.startScriptToolStripMenuItem,
            this.stopScriptToolStripMenuItem,
            this.postInstallScriptToolStripMenuItem,
            this.preUninstallScriptToolStripMenuItem,
            this.postUninstallScriptToolStripMenuItem,
            this.preUpgradeScriptToolStripMenuItem,
            this.postUpgradeScriptToolStripMenuItem,
            this.toolStripSeparator2,
            this.wizardInstallUIToolStripMenuItem,
            this.wizardUninstallUIToolStripMenuItem,
            this.wizardUpgradeUIToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // scriptRunnerToolStripMenuItem
            // 
            this.scriptRunnerToolStripMenuItem.Name = "scriptRunnerToolStripMenuItem";
            this.scriptRunnerToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.scriptRunnerToolStripMenuItem.Text = "Default Runner";
            this.scriptRunnerToolStripMenuItem.Click += new System.EventHandler(this.scriptRunnerToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // startScriptToolStripMenuItem
            // 
            this.startScriptToolStripMenuItem.Name = "startScriptToolStripMenuItem";
            this.startScriptToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.startScriptToolStripMenuItem.Tag = "start-stop-status";
            this.startScriptToolStripMenuItem.Text = "Start-Stop Script";
            this.startScriptToolStripMenuItem.Click += new System.EventHandler(this.scriptEditMenuItem_Click);
            // 
            // stopScriptToolStripMenuItem
            // 
            this.stopScriptToolStripMenuItem.Name = "stopScriptToolStripMenuItem";
            this.stopScriptToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.stopScriptToolStripMenuItem.Tag = "preinst";
            this.stopScriptToolStripMenuItem.Text = "Pre-Install Script";
            this.stopScriptToolStripMenuItem.Click += new System.EventHandler(this.scriptEditMenuItem_Click);
            // 
            // postInstallScriptToolStripMenuItem
            // 
            this.postInstallScriptToolStripMenuItem.Name = "postInstallScriptToolStripMenuItem";
            this.postInstallScriptToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.postInstallScriptToolStripMenuItem.Tag = "postinst";
            this.postInstallScriptToolStripMenuItem.Text = "Post-Install Script";
            this.postInstallScriptToolStripMenuItem.Click += new System.EventHandler(this.scriptEditMenuItem_Click);
            // 
            // preUninstallScriptToolStripMenuItem
            // 
            this.preUninstallScriptToolStripMenuItem.Name = "preUninstallScriptToolStripMenuItem";
            this.preUninstallScriptToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.preUninstallScriptToolStripMenuItem.Tag = "preuninst";
            this.preUninstallScriptToolStripMenuItem.Text = "Pre-Uninstall Script";
            this.preUninstallScriptToolStripMenuItem.Click += new System.EventHandler(this.scriptEditMenuItem_Click);
            // 
            // postUninstallScriptToolStripMenuItem
            // 
            this.postUninstallScriptToolStripMenuItem.Name = "postUninstallScriptToolStripMenuItem";
            this.postUninstallScriptToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.postUninstallScriptToolStripMenuItem.Tag = "postuninst";
            this.postUninstallScriptToolStripMenuItem.Text = "Post-Uninstall Script";
            this.postUninstallScriptToolStripMenuItem.Click += new System.EventHandler(this.scriptEditMenuItem_Click);
            // 
            // preUpgradeScriptToolStripMenuItem
            // 
            this.preUpgradeScriptToolStripMenuItem.Name = "preUpgradeScriptToolStripMenuItem";
            this.preUpgradeScriptToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.preUpgradeScriptToolStripMenuItem.Tag = "preupgrade";
            this.preUpgradeScriptToolStripMenuItem.Text = "Pre-Upgrade Script";
            this.preUpgradeScriptToolStripMenuItem.Click += new System.EventHandler(this.scriptEditMenuItem_Click);
            // 
            // postUpgradeScriptToolStripMenuItem
            // 
            this.postUpgradeScriptToolStripMenuItem.Name = "postUpgradeScriptToolStripMenuItem";
            this.postUpgradeScriptToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.postUpgradeScriptToolStripMenuItem.Tag = "postupgrade";
            this.postUpgradeScriptToolStripMenuItem.Text = "Post-Upgrade Script";
            this.postUpgradeScriptToolStripMenuItem.Click += new System.EventHandler(this.scriptEditMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(178, 6);
            // 
            // wizardInstallUIToolStripMenuItem
            // 
            this.wizardInstallUIToolStripMenuItem.Name = "wizardInstallUIToolStripMenuItem";
            this.wizardInstallUIToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.wizardInstallUIToolStripMenuItem.Tag = "install_uifile";
            this.wizardInstallUIToolStripMenuItem.Text = "Wizard-Install UI";
            this.wizardInstallUIToolStripMenuItem.Click += new System.EventHandler(this.wizardToolStripMenuItem_Click);
            // 
            // wizardUninstallUIToolStripMenuItem
            // 
            this.wizardUninstallUIToolStripMenuItem.Name = "wizardUninstallUIToolStripMenuItem";
            this.wizardUninstallUIToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.wizardUninstallUIToolStripMenuItem.Tag = "uninstall_uifile";
            this.wizardUninstallUIToolStripMenuItem.Text = "Wizard-Uninstall UI";
            this.wizardUninstallUIToolStripMenuItem.Click += new System.EventHandler(this.wizardToolStripMenuItem_Click);
            // 
            // wizardUpgradeUIToolStripMenuItem
            // 
            this.wizardUpgradeUIToolStripMenuItem.Name = "wizardUpgradeUIToolStripMenuItem";
            this.wizardUpgradeUIToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.wizardUpgradeUIToolStripMenuItem.Tag = "upgrade_uifile";
            this.wizardUpgradeUIToolStripMenuItem.Text = "Wizard-Upgrade UI";
            this.wizardUpgradeUIToolStripMenuItem.Click += new System.EventHandler(this.wizardToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.documentationToolStripMenuItem,
            this.supportToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.packeDevGuideToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // documentationToolStripMenuItem
            // 
            this.documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            this.documentationToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.documentationToolStripMenuItem.Text = "Documentation";
            this.documentationToolStripMenuItem.Click += new System.EventHandler(this.documentationToolStripMenuItem_Click);
            // 
            // supportToolStripMenuItem
            // 
            this.supportToolStripMenuItem.Name = "supportToolStripMenuItem";
            this.supportToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.supportToolStripMenuItem.Text = "Support";
            this.supportToolStripMenuItem.Click += new System.EventHandler(this.supportToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // packeDevGuideToolStripMenuItem
            // 
            this.packeDevGuideToolStripMenuItem.Name = "packeDevGuideToolStripMenuItem";
            this.packeDevGuideToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.packeDevGuideToolStripMenuItem.Text = "Package DevGuide";
            this.packeDevGuideToolStripMenuItem.Click += new System.EventHandler(this.packeDevGuideToolStripMenuItem_Click);
            // 
            // groupBoxTip
            // 
            this.groupBoxTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTip.Controls.Add(this.labelToolTip);
            this.groupBoxTip.Location = new System.Drawing.Point(3, 671);
            this.groupBoxTip.Name = "groupBoxTip";
            this.groupBoxTip.Size = new System.Drawing.Size(800, 56);
            this.groupBoxTip.TabIndex = 41;
            this.groupBoxTip.TabStop = false;
            this.groupBoxTip.Text = "TIPS";
            // 
            // labelToolTip
            // 
            this.labelToolTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelToolTip.Location = new System.Drawing.Point(10, 18);
            this.labelToolTip.Name = "labelToolTip";
            this.labelToolTip.Size = new System.Drawing.Size(772, 31);
            this.labelToolTip.TabIndex = 24;
            this.labelToolTip.UseMnemonic = false;
            // 
            // folderBrowserDialog4Mods
            // 
            //this.folderBrowserDialog4Mods.Description = "";
            //this.folderBrowserDialog4Mods.DontIncludeNetworkFoldersBelowDomainLevel = false;
            //this.folderBrowserDialog4Mods.NewStyle = true;
            //this.folderBrowserDialog4Mods.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.folderBrowserDialog4Mods.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            //this.folderBrowserDialog4Mods.ShowBothFilesAndFolders = false;
            //this.folderBrowserDialog4Mods.ShowEditBox = true;
            //this.folderBrowserDialog4Mods.ShowFullPathInEditBox = true;
            //this.folderBrowserDialog4Mods.ShowNewFolderButton = true;
            // 
            // webpageBrowserDialog4Mods
            // 
            //this.webpageBrowserDialog4Mods.Description = "";
            //this.webpageBrowserDialog4Mods.DontIncludeNetworkFoldersBelowDomainLevel = false;
            //this.webpageBrowserDialog4Mods.NewStyle = true;
            //this.webpageBrowserDialog4Mods.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.webpageBrowserDialog4Mods.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            //this.webpageBrowserDialog4Mods.ShowBothFilesAndFolders = false;
            //this.webpageBrowserDialog4Mods.ShowEditBox = true;
            //this.webpageBrowserDialog4Mods.ShowFullPathInEditBox = true;
            //this.webpageBrowserDialog4Mods.ShowNewFolderButton = true;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 729);
            this.Controls.Add(this.groupBoxTip);
            this.Controls.Add(this.groupBoxItem);
            this.Controls.Add(this.groupBoxPackage);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(826, 768);
            this.Name = "MainForm";
            this.Text = "Mods Packager for Synology";
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPkg_72)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPkg_256)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBoxPackage.ResumeLayout(false);
            this.groupBoxPackage.PerformLayout();
            this.groupBoxItem.ResumeLayout(false);
            this.groupBoxItem.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxTip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip4Mods;
        private System.Windows.Forms.OpenFileDialog openFileDialog4Mods;
        //private Ionic.Utils.FolderBrowserDialogEx folderBrowserDialog4Mods;
        //private Ionic.Utils.FolderBrowserDialogEx webpageBrowserDialog4Mods;
        private OpenFolderDialog folderBrowserDialog4Mods;
        private OpenFolderDialog webpageBrowserDialog4Mods;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox groupBoxPackage;
        private System.Windows.Forms.Label labelDSMAppName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDsmAppName;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Button buttonPackage;
        private System.Windows.Forms.TextBox textBoxVersion;
        private System.Windows.Forms.PictureBox pictureBoxPkg_72;
        private System.Windows.Forms.Label labelMaintainer;
        private System.Windows.Forms.TextBox textBoxMaintainer;
        private System.Windows.Forms.PictureBox pictureBoxPkg_256;
        private System.Windows.Forms.Label labelDisplay;
        private System.Windows.Forms.Label labelPackage;
        private System.Windows.Forms.TextBox textBoxDisplay;
        private System.Windows.Forms.TextBox textBoxPackage;
        private System.Windows.Forms.Label labelMaintainerUrl;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxMaintainerUrl;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.GroupBox groupBoxItem;
        private System.Windows.Forms.PictureBox pictureBox_256;
        private System.Windows.Forms.CheckBox checkBoxMultiInstance;
        private System.Windows.Forms.PictureBox pictureBox_128;
        private System.Windows.Forms.ComboBox comboBoxItemType;
        private System.Windows.Forms.PictureBox pictureBox_96;
        private System.Windows.Forms.CheckBox checkBoxSize;
        private System.Windows.Forms.PictureBox pictureBox_72;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.PictureBox pictureBox_64;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.PictureBox pictureBox_48;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.PictureBox pictureBox_32;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.PictureBox pictureBox_24;
        private System.Windows.Forms.CheckBox checkBoxAllUsers;
        private System.Windows.Forms.PictureBox pictureBox_16;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.TextBox textBoxDesc;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.ListView listViewItems;
        private System.Windows.Forms.TextBox textBoxItem;
        private System.Windows.Forms.Label labelTransparency;
        private System.Windows.Forms.ComboBox comboBoxTransparency;
        private System.Windows.Forms.Label labelPublisher;
        private System.Windows.Forms.TextBox textBoxPublisher;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRecentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptRunnerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem postInstallScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preUninstallScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem postUninstallScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preUpgradeScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem postUpgradeScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem packeDevGuideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addWizardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wizardInstallUIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wizardUninstallUIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wizardUpgradeUIToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox groupBoxTip;
        private System.Windows.Forms.Label labelToolTip;
        private System.Windows.Forms.Label labelPublisherUrl;
        private System.Windows.Forms.TextBox textBoxPublisherUrl;
        private System.Windows.Forms.Label labelHelpUrl;
        private System.Windows.Forms.TextBox textBoxHelpUrl;
        private System.Windows.Forms.Label labelReportUrl;
        private System.Windows.Forms.TextBox TextBoxReportUrl;
        private System.Windows.Forms.Label labelFirmware;
        private System.Windows.Forms.TextBox textBoxFirmware;
        private System.Windows.Forms.CheckBox checkBoxBeta;
        private System.Windows.Forms.Button buttonAdvanced;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}


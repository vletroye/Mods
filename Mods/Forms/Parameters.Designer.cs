namespace BeatificaBytes.Synology.Mods
{
    partial class Parameters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Parameters));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxDefaultPackageRepo = new System.Windows.Forms.CheckBox();
            this.labelToolTip = new System.Windows.Forms.Label();
            this.toolTipProperties = new System.Windows.Forms.ToolTip(this.components);
            this.labelDefaultPublishFolder = new System.Windows.Forms.Label();
            this.checkBoxOpenWith = new System.Windows.Forms.CheckBox();
            this.labelDefaultPackageRoot = new System.Windows.Forms.Label();
            this.checkBoxDefaultPackageRoot = new System.Windows.Forms.CheckBox();
            this.buttonEditDSMReleases = new System.Windows.Forms.Button();
            this.checkBoxPromptExplorer = new System.Windows.Forms.CheckBox();
            this.checkBoxCopyPackagePath = new System.Windows.Forms.CheckBox();
            this.buttonDefaultPackageRepo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonDefaultPackageRoot = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonPhpExtensions = new System.Windows.Forms.Button();
            this.groupBoxTips = new System.Windows.Forms.GroupBox();
            this.groupBoxTips.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.Location = new System.Drawing.Point(12, 261);
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
            this.buttonCancel.Location = new System.Drawing.Point(443, 261);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxDefaultPackageRepo
            // 
            this.checkBoxDefaultPackageRepo.AutoSize = true;
            this.checkBoxDefaultPackageRepo.Location = new System.Drawing.Point(12, 12);
            this.checkBoxDefaultPackageRepo.Name = "checkBoxDefaultPackageRepo";
            this.checkBoxDefaultPackageRepo.Size = new System.Drawing.Size(160, 17);
            this.checkBoxDefaultPackageRepo.TabIndex = 2;
            this.checkBoxDefaultPackageRepo.Text = "Use a Default Publish Folder";
            this.toolTipProperties.SetToolTip(this.checkBoxDefaultPackageRepo, resources.GetString("checkBoxDefaultPackageRepo.ToolTip"));
            this.checkBoxDefaultPackageRepo.UseVisualStyleBackColor = true;
            this.checkBoxDefaultPackageRepo.CheckedChanged += new System.EventHandler(this.checkBoxDefaultPackageRepo_CheckedChanged);
            // 
            // labelToolTip
            // 
            this.labelToolTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelToolTip.Location = new System.Drawing.Point(6, 16);
            this.labelToolTip.Name = "labelToolTip";
            this.labelToolTip.Size = new System.Drawing.Size(493, 66);
            this.labelToolTip.TabIndex = 22;
            // 
            // labelDefaultPublishFolder
            // 
            this.labelDefaultPublishFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelDefaultPublishFolder.Location = new System.Drawing.Point(93, 35);
            this.labelDefaultPublishFolder.Name = "labelDefaultPublishFolder";
            this.labelDefaultPublishFolder.Size = new System.Drawing.Size(411, 19);
            this.labelDefaultPublishFolder.TabIndex = 24;
            this.labelDefaultPublishFolder.Text = "...";
            this.toolTipProperties.SetToolTip(this.labelDefaultPublishFolder, "Path of the repository where packages will be published. Click here to open that " +
        "folder.");
            this.labelDefaultPublishFolder.Click += new System.EventHandler(this.labelDefaultPublishFolder_Click);
            // 
            // checkBoxOpenWith
            // 
            this.checkBoxOpenWith.AutoSize = true;
            this.checkBoxOpenWith.Location = new System.Drawing.Point(12, 77);
            this.checkBoxOpenWith.Name = "checkBoxOpenWith";
            this.checkBoxOpenWith.Size = new System.Drawing.Size(315, 17);
            this.checkBoxOpenWith.TabIndex = 26;
            this.checkBoxOpenWith.Text = "Add \'Edit\' context-menu on SPK files within Windows Explorer";
            this.toolTipProperties.SetToolTip(this.checkBoxOpenWith, "If you select this option, you will be able to open SPK files with Mods Packager " +
        "from Windows Explorer");
            this.checkBoxOpenWith.UseVisualStyleBackColor = true;
            this.checkBoxOpenWith.CheckedChanged += new System.EventHandler(this.checkBoxOpenWith_CheckedChanged);
            // 
            // labelDefaultPackageRoot
            // 
            this.labelDefaultPackageRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDefaultPackageRoot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelDefaultPackageRoot.Location = new System.Drawing.Point(94, 203);
            this.labelDefaultPackageRoot.Name = "labelDefaultPackageRoot";
            this.labelDefaultPackageRoot.Size = new System.Drawing.Size(423, 19);
            this.labelDefaultPackageRoot.TabIndex = 31;
            this.labelDefaultPackageRoot.Text = "...";
            this.toolTipProperties.SetToolTip(this.labelDefaultPackageRoot, "Path of the repository where new packages will be created before being published." +
        " Click here to open that folder.");
            this.labelDefaultPackageRoot.Click += new System.EventHandler(this.labelDefaultPackageRoot_Click);
            // 
            // checkBoxDefaultPackageRoot
            // 
            this.checkBoxDefaultPackageRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxDefaultPackageRoot.AutoSize = true;
            this.checkBoxDefaultPackageRoot.Location = new System.Drawing.Point(14, 180);
            this.checkBoxDefaultPackageRoot.Name = "checkBoxDefaultPackageRoot";
            this.checkBoxDefaultPackageRoot.Size = new System.Drawing.Size(169, 17);
            this.checkBoxDefaultPackageRoot.TabIndex = 32;
            this.checkBoxDefaultPackageRoot.Text = "Use a Default Package Folder";
            this.toolTipProperties.SetToolTip(this.checkBoxDefaultPackageRoot, "If you select this option, you will have to provide the path of a folder where al" +
        "l your packages will be created before being published. Each Package will be loc" +
        "ated in its own subfolder.");
            this.checkBoxDefaultPackageRoot.UseVisualStyleBackColor = true;
            this.checkBoxDefaultPackageRoot.CheckedChanged += new System.EventHandler(this.checkBoxDefaultPackageRoot_CheckedChanged);
            // 
            // buttonEditDSMReleases
            // 
            this.buttonEditDSMReleases.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditDSMReleases.Location = new System.Drawing.Point(412, 228);
            this.buttonEditDSMReleases.Name = "buttonEditDSMReleases";
            this.buttonEditDSMReleases.Size = new System.Drawing.Size(105, 23);
            this.buttonEditDSMReleases.TabIndex = 36;
            this.buttonEditDSMReleases.Text = "Edit DSM versions";
            this.toolTipProperties.SetToolTip(this.buttonEditDSMReleases, "Edit the list of official DSM releases. It can be created by parsing the release " +
        "notes like https://www.synology.com/en-global/releaseNote/DS1815+");
            this.buttonEditDSMReleases.UseVisualStyleBackColor = true;
            this.buttonEditDSMReleases.Click += new System.EventHandler(this.buttonEditDSMReleases_Click);
            // 
            // checkBoxPromptExplorer
            // 
            this.checkBoxPromptExplorer.AutoSize = true;
            this.checkBoxPromptExplorer.Location = new System.Drawing.Point(12, 114);
            this.checkBoxPromptExplorer.Name = "checkBoxPromptExplorer";
            this.checkBoxPromptExplorer.Size = new System.Drawing.Size(226, 17);
            this.checkBoxPromptExplorer.TabIndex = 37;
            this.checkBoxPromptExplorer.Text = "Prompt to open Package Folder after Build";
            this.toolTipProperties.SetToolTip(this.checkBoxPromptExplorer, "If you select this option, you will be able to open SPK files with Mods Packager " +
        "from Windows Explorer");
            this.checkBoxPromptExplorer.UseVisualStyleBackColor = true;
            this.checkBoxPromptExplorer.CheckedChanged += new System.EventHandler(this.checkBoxPromptExplorer_CheckedChanged);
            // 
            // checkBoxCopyPackagePath
            // 
            this.checkBoxCopyPackagePath.AutoSize = true;
            this.checkBoxCopyPackagePath.Location = new System.Drawing.Point(12, 137);
            this.checkBoxCopyPackagePath.Name = "checkBoxCopyPackagePath";
            this.checkBoxCopyPackagePath.Size = new System.Drawing.Size(236, 17);
            this.checkBoxCopyPackagePath.TabIndex = 38;
            this.checkBoxCopyPackagePath.Text = "Copy Package Folder in Clipboard after Build";
            this.toolTipProperties.SetToolTip(this.checkBoxCopyPackagePath, "If you select this option, you will be able to open SPK files with Mods Packager " +
        "from Windows Explorer");
            this.checkBoxCopyPackagePath.UseVisualStyleBackColor = true;
            this.checkBoxCopyPackagePath.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // buttonDefaultPackageRepo
            // 
            this.buttonDefaultPackageRepo.Location = new System.Drawing.Point(12, 35);
            this.buttonDefaultPackageRepo.Name = "buttonDefaultPackageRepo";
            this.buttonDefaultPackageRepo.Size = new System.Drawing.Size(75, 19);
            this.buttonDefaultPackageRepo.TabIndex = 25;
            this.buttonDefaultPackageRepo.Text = "Select";
            this.buttonDefaultPackageRepo.UseVisualStyleBackColor = true;
            this.buttonDefaultPackageRepo.Visible = false;
            this.buttonDefaultPackageRepo.Click += new System.EventHandler(this.buttonDefaultPackageRepo_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(506, 2);
            this.label1.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(13, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(504, 2);
            this.label2.TabIndex = 30;
            // 
            // buttonDefaultPackageRoot
            // 
            this.buttonDefaultPackageRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDefaultPackageRoot.Location = new System.Drawing.Point(13, 203);
            this.buttonDefaultPackageRoot.Name = "buttonDefaultPackageRoot";
            this.buttonDefaultPackageRoot.Size = new System.Drawing.Size(75, 19);
            this.buttonDefaultPackageRoot.TabIndex = 29;
            this.buttonDefaultPackageRoot.Text = "Select";
            this.buttonDefaultPackageRoot.UseVisualStyleBackColor = true;
            this.buttonDefaultPackageRoot.Visible = false;
            this.buttonDefaultPackageRoot.Click += new System.EventHandler(this.buttonDefaultPackageRoot_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReset.Location = new System.Drawing.Point(362, 261);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 33;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(12, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(506, 2);
            this.label3.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(13, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(504, 2);
            this.label4.TabIndex = 35;
            // 
            // buttonPhpExtensions
            // 
            this.buttonPhpExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPhpExtensions.Location = new System.Drawing.Point(13, 229);
            this.buttonPhpExtensions.Name = "buttonPhpExtensions";
            this.buttonPhpExtensions.Size = new System.Drawing.Size(112, 23);
            this.buttonPhpExtensions.TabIndex = 39;
            this.buttonPhpExtensions.Text = "Edit Php Extensions";
            this.toolTipProperties.SetToolTip(this.buttonPhpExtensions, "Edit the list of php Extensions.");
            this.buttonPhpExtensions.UseVisualStyleBackColor = true;
            this.buttonPhpExtensions.Click += new System.EventHandler(this.buttonPhpExtensions_Click);
            // 
            // groupBoxTips
            // 
            this.groupBoxTips.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTips.Controls.Add(this.labelToolTip);
            this.groupBoxTips.Location = new System.Drawing.Point(12, 291);
            this.groupBoxTips.Name = "groupBoxTips";
            this.groupBoxTips.Size = new System.Drawing.Size(505, 85);
            this.groupBoxTips.TabIndex = 40;
            this.groupBoxTips.TabStop = false;
            this.groupBoxTips.Text = "TIPS";
            // 
            // Parameters
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(530, 388);
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxTips);
            this.Controls.Add(this.buttonPhpExtensions);
            this.Controls.Add(this.checkBoxCopyPackagePath);
            this.Controls.Add(this.checkBoxPromptExplorer);
            this.Controls.Add(this.buttonEditDSMReleases);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.checkBoxDefaultPackageRoot);
            this.Controls.Add(this.labelDefaultPackageRoot);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonDefaultPackageRoot);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxOpenWith);
            this.Controls.Add(this.buttonDefaultPackageRepo);
            this.Controls.Add(this.labelDefaultPublishFolder);
            this.Controls.Add(this.checkBoxDefaultPackageRepo);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Name = "Parameters";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Properties";
            this.groupBoxTips.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxDefaultPackageRepo;
        private System.Windows.Forms.Label labelToolTip;
        private System.Windows.Forms.ToolTip toolTipProperties;
        private System.Windows.Forms.Label labelDefaultPublishFolder;
        private System.Windows.Forms.Button buttonDefaultPackageRepo;
        private System.Windows.Forms.CheckBox checkBoxOpenWith;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonDefaultPackageRoot;
        private System.Windows.Forms.Label labelDefaultPackageRoot;
        private System.Windows.Forms.CheckBox checkBoxDefaultPackageRoot;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonEditDSMReleases;
        private System.Windows.Forms.CheckBox checkBoxPromptExplorer;
        private System.Windows.Forms.CheckBox checkBoxCopyPackagePath;
        private System.Windows.Forms.Button buttonPhpExtensions;
        private System.Windows.Forms.GroupBox groupBoxTips;
    }
}
namespace MyWebApps
{
    partial class FormMyWebApps
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
            this.buttonPackage = new System.Windows.Forms.Button();
            this.listViewUrls = new System.Windows.Forms.ListView();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelDesc = new System.Windows.Forms.Label();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.labelUrl = new System.Windows.Forms.Label();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.checkBoxAllUsers = new System.Windows.Forms.CheckBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.toolTipMyWebApps = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialogMyWebApps = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialogMyWebApps = new System.Windows.Forms.FolderBrowserDialog();
            this.pictureBoxSettings = new System.Windows.Forms.PictureBox();
            this.pictureBox_256 = new System.Windows.Forms.PictureBox();
            this.pictureBox_128 = new System.Windows.Forms.PictureBox();
            this.pictureBox_96 = new System.Windows.Forms.PictureBox();
            this.pictureBox_72 = new System.Windows.Forms.PictureBox();
            this.pictureBox_64 = new System.Windows.Forms.PictureBox();
            this.pictureBox_48 = new System.Windows.Forms.PictureBox();
            this.pictureBox_32 = new System.Windows.Forms.PictureBox();
            this.pictureBox_24 = new System.Windows.Forms.PictureBox();
            this.pictureBox_16 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_256)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_128)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_96)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_72)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_64)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_48)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_16)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPackage
            // 
            this.buttonPackage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPackage.Location = new System.Drawing.Point(722, 607);
            this.buttonPackage.Name = "buttonPackage";
            this.buttonPackage.Size = new System.Drawing.Size(75, 23);
            this.buttonPackage.TabIndex = 0;
            this.buttonPackage.Text = "Package";
            this.buttonPackage.UseVisualStyleBackColor = true;
            this.buttonPackage.Click += new System.EventHandler(this.buttonPackage_Click);
            // 
            // listViewUrls
            // 
            this.listViewUrls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewUrls.Location = new System.Drawing.Point(12, 66);
            this.listViewUrls.Name = "listViewUrls";
            this.listViewUrls.Size = new System.Drawing.Size(785, 240);
            this.listViewUrls.TabIndex = 1;
            this.listViewUrls.UseCompatibleStateImageBehavior = false;
            this.listViewUrls.SelectedIndexChanged += new System.EventHandler(this.listViewUrls_SelectedIndexChanged);
            this.listViewUrls.DoubleClick += new System.EventHandler(this.listViewUrls_DoubleClick);
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxTitle.Location = new System.Drawing.Point(77, 319);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(128, 20);
            this.textBoxTitle.TabIndex = 2;
            this.toolTipMyWebApps.SetToolTip(this.textBoxTitle, "Enter the titleof the RUL. It will to be displayed on DSM.");
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(42, 322);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(30, 13);
            this.labelTitle.TabIndex = 3;
            this.labelTitle.Text = "Title:";
            // 
            // labelDesc
            // 
            this.labelDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDesc.AutoSize = true;
            this.labelDesc.Location = new System.Drawing.Point(9, 348);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Size = new System.Drawing.Size(63, 13);
            this.labelDesc.TabIndex = 5;
            this.labelDesc.Text = "Description:";
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxDesc.Location = new System.Drawing.Point(77, 345);
            this.textBoxDesc.Multiline = true;
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxDesc.Size = new System.Drawing.Size(720, 60);
            this.textBoxDesc.TabIndex = 4;
            this.toolTipMyWebApps.SetToolTip(this.textBoxDesc, "Enter an optional description. This will however not be displayed on DSM.");
            // 
            // labelUrl
            // 
            this.labelUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelUrl.AutoSize = true;
            this.labelUrl.Location = new System.Drawing.Point(49, 414);
            this.labelUrl.Name = "labelUrl";
            this.labelUrl.Size = new System.Drawing.Size(23, 13);
            this.labelUrl.TabIndex = 7;
            this.labelUrl.Text = "Url:";
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxUrl.Location = new System.Drawing.Point(77, 411);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(720, 20);
            this.textBoxUrl.TabIndex = 6;
            this.toolTipMyWebApps.SetToolTip(this.textBoxUrl, "Type here the url to be opened when clicking the icon on DSM.");
            // 
            // checkBoxAllUsers
            // 
            this.checkBoxAllUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxAllUsers.AutoSize = true;
            this.checkBoxAllUsers.Location = new System.Drawing.Point(709, 318);
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
            this.buttonEdit.Location = new System.Drawing.Point(93, 607);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 10;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.Location = new System.Drawing.Point(174, 607);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 11;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.Location = new System.Drawing.Point(255, 607);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelete.Location = new System.Drawing.Point(336, 607);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 13;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // openFileDialogMyWebApps
            // 
            this.openFileDialogMyWebApps.Filter = "Png|*.png";
            this.openFileDialogMyWebApps.RestoreDirectory = true;
            // 
            // pictureBoxSettings
            // 
            this.pictureBoxSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxSettings.Image = global::MyWebApps.Properties.Resources.settings;
            this.pictureBoxSettings.InitialImage = global::MyWebApps.Properties.Resources.settings;
            this.pictureBoxSettings.Location = new System.Drawing.Point(749, 12);
            this.pictureBoxSettings.Name = "pictureBoxSettings";
            this.pictureBoxSettings.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSettings.TabIndex = 23;
            this.pictureBoxSettings.TabStop = false;
            this.pictureBoxSettings.Click += new System.EventHandler(this.pictureBoxSettings_Click);
            // 
            // pictureBox_256
            // 
            this.pictureBox_256.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_256.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_256.Location = new System.Drawing.Point(633, 437);
            this.pictureBox_256.Name = "pictureBox_256";
            this.pictureBox_256.Size = new System.Drawing.Size(164, 164);
            this.pictureBox_256.TabIndex = 22;
            this.pictureBox_256.TabStop = false;
            this.pictureBox_256.Tag = "256";
            this.toolTipMyWebApps.SetToolTip(this.pictureBox_256, "Drop here a logo 256x256");
            // 
            // pictureBox_128
            // 
            this.pictureBox_128.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_128.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_128.Location = new System.Drawing.Point(499, 437);
            this.pictureBox_128.Name = "pictureBox_128";
            this.pictureBox_128.Size = new System.Drawing.Size(128, 128);
            this.pictureBox_128.TabIndex = 21;
            this.pictureBox_128.TabStop = false;
            this.pictureBox_128.Tag = "128";
            this.toolTipMyWebApps.SetToolTip(this.pictureBox_128, "Drop here a logo 128x128");
            // 
            // pictureBox_96
            // 
            this.pictureBox_96.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_96.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_96.Location = new System.Drawing.Point(397, 437);
            this.pictureBox_96.Name = "pictureBox_96";
            this.pictureBox_96.Size = new System.Drawing.Size(96, 96);
            this.pictureBox_96.TabIndex = 20;
            this.pictureBox_96.TabStop = false;
            this.pictureBox_96.Tag = "96";
            this.toolTipMyWebApps.SetToolTip(this.pictureBox_96, "Drop here a logo 96x96");
            // 
            // pictureBox_72
            // 
            this.pictureBox_72.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_72.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_72.Location = new System.Drawing.Point(319, 437);
            this.pictureBox_72.Name = "pictureBox_72";
            this.pictureBox_72.Size = new System.Drawing.Size(72, 72);
            this.pictureBox_72.TabIndex = 19;
            this.pictureBox_72.TabStop = false;
            this.pictureBox_72.Tag = "72";
            this.toolTipMyWebApps.SetToolTip(this.pictureBox_72, "Drop here a logo 72x72");
            // 
            // pictureBox_64
            // 
            this.pictureBox_64.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_64.Location = new System.Drawing.Point(249, 437);
            this.pictureBox_64.Name = "pictureBox_64";
            this.pictureBox_64.Size = new System.Drawing.Size(64, 64);
            this.pictureBox_64.TabIndex = 18;
            this.pictureBox_64.TabStop = false;
            this.pictureBox_64.Tag = "64";
            this.toolTipMyWebApps.SetToolTip(this.pictureBox_64, "Drop here a logo 64x64");
            // 
            // pictureBox_48
            // 
            this.pictureBox_48.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_48.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_48.Location = new System.Drawing.Point(195, 437);
            this.pictureBox_48.Name = "pictureBox_48";
            this.pictureBox_48.Size = new System.Drawing.Size(48, 48);
            this.pictureBox_48.TabIndex = 17;
            this.pictureBox_48.TabStop = false;
            this.pictureBox_48.Tag = "48";
            this.toolTipMyWebApps.SetToolTip(this.pictureBox_48, "Drop here a logo 48x48");
            // 
            // pictureBox_32
            // 
            this.pictureBox_32.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_32.Location = new System.Drawing.Point(157, 437);
            this.pictureBox_32.Name = "pictureBox_32";
            this.pictureBox_32.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_32.TabIndex = 16;
            this.pictureBox_32.TabStop = false;
            this.pictureBox_32.Tag = "32";
            this.toolTipMyWebApps.SetToolTip(this.pictureBox_32, "Drop here a logo 32x32");
            // 
            // pictureBox_24
            // 
            this.pictureBox_24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_24.Location = new System.Drawing.Point(127, 437);
            this.pictureBox_24.Name = "pictureBox_24";
            this.pictureBox_24.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_24.TabIndex = 15;
            this.pictureBox_24.TabStop = false;
            this.pictureBox_24.Tag = "24";
            this.toolTipMyWebApps.SetToolTip(this.pictureBox_24, "Drop here a logo 24x24");
            // 
            // pictureBox_16
            // 
            this.pictureBox_16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_16.Location = new System.Drawing.Point(105, 437);
            this.pictureBox_16.Name = "pictureBox_16";
            this.pictureBox_16.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_16.TabIndex = 14;
            this.pictureBox_16.TabStop = false;
            this.pictureBox_16.Tag = "16";
            this.toolTipMyWebApps.SetToolTip(this.pictureBox_16, "Drop here a logo 16x16");
            // 
            // FormMyWebApps
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 642);
            this.Controls.Add(this.pictureBoxSettings);
            this.Controls.Add(this.pictureBox_256);
            this.Controls.Add(this.pictureBox_128);
            this.Controls.Add(this.pictureBox_96);
            this.Controls.Add(this.pictureBox_72);
            this.Controls.Add(this.pictureBox_64);
            this.Controls.Add(this.pictureBox_48);
            this.Controls.Add(this.pictureBox_32);
            this.Controls.Add(this.pictureBox_24);
            this.Controls.Add(this.pictureBox_16);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.checkBoxAllUsers);
            this.Controls.Add(this.labelUrl);
            this.Controls.Add(this.textBoxUrl);
            this.Controls.Add(this.labelDesc);
            this.Controls.Add(this.textBoxDesc);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.listViewUrls);
            this.Controls.Add(this.buttonPackage);
            this.MinimumSize = new System.Drawing.Size(632, 600);
            this.Name = "FormMyWebApps";
            this.Text = "MyWebApps Package Creator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_256)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_128)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_96)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_72)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_64)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_48)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_16)).EndInit();
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
        private System.Windows.Forms.Label labelUrl;
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
        private System.Windows.Forms.ToolTip toolTipMyWebApps;
        private System.Windows.Forms.OpenFileDialog openFileDialogMyWebApps;
        private System.Windows.Forms.PictureBox pictureBoxSettings;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogMyWebApps;
    }
}


namespace BeatificaBytes.Synology.Mods
{
    partial class Linker
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
            this.comboBoxLinkerType = new System.Windows.Forms.ComboBox();
            this.buttonSaveLinker = new System.Windows.Forms.Button();
            this.buttonCancelLinker = new System.Windows.Forms.Button();
            this.buttonEditLinker = new System.Windows.Forms.Button();
            this.ButtonDeleteLinker = new System.Windows.Forms.Button();
            this.buttonAddLinker = new System.Windows.Forms.Button();
            this.listViewLinker = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textboxLinkerPath = new System.Windows.Forms.TextBox();
            this.linkLabelHelp = new System.Windows.Forms.LinkLabel();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // comboBoxLinkerType
            // 
            this.comboBoxLinkerType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxLinkerType.AutoCompleteCustomSource.AddRange(new string[] {
            "bin",
            "lib",
            "etc",
            "relpath"});
            this.comboBoxLinkerType.FormattingEnabled = true;
            this.comboBoxLinkerType.Items.AddRange(new object[] {
            "bin",
            "lib",
            "etc"});
            this.comboBoxLinkerType.Location = new System.Drawing.Point(12, 149);
            this.comboBoxLinkerType.Name = "comboBoxLinkerType";
            this.comboBoxLinkerType.Size = new System.Drawing.Size(66, 21);
            this.comboBoxLinkerType.TabIndex = 2;
            this.toolTip1.SetToolTip(this.comboBoxLinkerType, "Use bin, lib or etc to link files respectively under /usr/local/bin/, /usr/local/" +
        "lib/ or /usr/local/etc/");
            // 
            // buttonSaveLinker
            // 
            this.buttonSaveLinker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveLinker.Location = new System.Drawing.Point(300, 176);
            this.buttonSaveLinker.Name = "buttonSaveLinker";
            this.buttonSaveLinker.Size = new System.Drawing.Size(66, 28);
            this.buttonSaveLinker.TabIndex = 8;
            this.buttonSaveLinker.Text = "Save";
            this.buttonSaveLinker.UseVisualStyleBackColor = true;
            this.buttonSaveLinker.Click += new System.EventHandler(this.buttonSaveLinker_Click);
            // 
            // buttonCancelLinker
            // 
            this.buttonCancelLinker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancelLinker.Location = new System.Drawing.Point(228, 176);
            this.buttonCancelLinker.Name = "buttonCancelLinker";
            this.buttonCancelLinker.Size = new System.Drawing.Size(66, 28);
            this.buttonCancelLinker.TabIndex = 7;
            this.buttonCancelLinker.Text = "Cancel";
            this.buttonCancelLinker.UseVisualStyleBackColor = true;
            this.buttonCancelLinker.Click += new System.EventHandler(this.buttonCancelLinker_Click);
            // 
            // buttonEditLinker
            // 
            this.buttonEditLinker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEditLinker.Location = new System.Drawing.Point(156, 176);
            this.buttonEditLinker.Name = "buttonEditLinker";
            this.buttonEditLinker.Size = new System.Drawing.Size(66, 28);
            this.buttonEditLinker.TabIndex = 6;
            this.buttonEditLinker.Text = "Edit";
            this.buttonEditLinker.UseVisualStyleBackColor = true;
            this.buttonEditLinker.Click += new System.EventHandler(this.buttonEditLinker_Click);
            // 
            // ButtonDeleteLinker
            // 
            this.ButtonDeleteLinker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonDeleteLinker.Location = new System.Drawing.Point(84, 176);
            this.ButtonDeleteLinker.Name = "ButtonDeleteLinker";
            this.ButtonDeleteLinker.Size = new System.Drawing.Size(66, 28);
            this.ButtonDeleteLinker.TabIndex = 5;
            this.ButtonDeleteLinker.Text = "Delete";
            this.ButtonDeleteLinker.UseVisualStyleBackColor = true;
            this.ButtonDeleteLinker.Click += new System.EventHandler(this.ButtonDeleteLinker_Click);
            // 
            // buttonAddLinker
            // 
            this.buttonAddLinker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddLinker.Location = new System.Drawing.Point(12, 176);
            this.buttonAddLinker.Name = "buttonAddLinker";
            this.buttonAddLinker.Size = new System.Drawing.Size(66, 28);
            this.buttonAddLinker.TabIndex = 4;
            this.buttonAddLinker.Text = "Add";
            this.buttonAddLinker.UseVisualStyleBackColor = true;
            this.buttonAddLinker.Click += new System.EventHandler(this.buttonAddLinker_Click);
            // 
            // listViewLinker
            // 
            this.listViewLinker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewLinker.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewLinker.FullRowSelect = true;
            this.listViewLinker.GridLines = true;
            this.listViewLinker.Location = new System.Drawing.Point(12, 23);
            this.listViewLinker.Name = "listViewLinker";
            this.listViewLinker.Size = new System.Drawing.Size(534, 120);
            this.listViewLinker.TabIndex = 1;
            this.listViewLinker.UseCompatibleStateImageBehavior = false;
            this.listViewLinker.View = System.Windows.Forms.View.Details;
            this.listViewLinker.SelectedIndexChanged += new System.EventHandler(this.listViewLinker_SelectedIndexChanged);
            this.listViewLinker.DoubleClick += new System.EventHandler(this.listViewLinker_DoubleClick);
            this.listViewLinker.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listViewLinker_KeyUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Type";
            this.columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Relative Path";
            this.columnHeader2.Width = 140;
            // 
            // textboxLinkerPath
            // 
            this.textboxLinkerPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxLinkerPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textboxLinkerPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textboxLinkerPath.Location = new System.Drawing.Point(84, 150);
            this.textboxLinkerPath.Name = "textboxLinkerPath";
            this.textboxLinkerPath.Size = new System.Drawing.Size(462, 20);
            this.textboxLinkerPath.TabIndex = 3;
            this.toolTip1.SetToolTip(this.textboxLinkerPath, "String, target file\'s relative path under /var/packages/${package}/target/");
            // 
            // linkLabelHelp
            // 
            this.linkLabelHelp.Location = new System.Drawing.Point(0, 0);
            this.linkLabelHelp.Name = "linkLabelHelp";
            this.linkLabelHelp.Size = new System.Drawing.Size(100, 23);
            this.linkLabelHelp.TabIndex = 12;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Location = new System.Drawing.Point(395, 251);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 10;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(476, 251);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.Location = new System.Drawing.Point(4, 251);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 9;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // Linker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 286);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.linkLabelHelp);
            this.Controls.Add(this.textboxLinkerPath);
            this.Controls.Add(this.comboBoxLinkerType);
            this.Controls.Add(this.buttonSaveLinker);
            this.Controls.Add(this.buttonCancelLinker);
            this.Controls.Add(this.buttonEditLinker);
            this.Controls.Add(this.ButtonDeleteLinker);
            this.Controls.Add(this.buttonAddLinker);
            this.Controls.Add(this.listViewLinker);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Linker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Linker Editor";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Linker_HelpButtonClicked);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxLinkerType;
        private System.Windows.Forms.Button buttonSaveLinker;
        private System.Windows.Forms.Button buttonCancelLinker;
        private System.Windows.Forms.Button buttonEditLinker;
        private System.Windows.Forms.Button ButtonDeleteLinker;
        private System.Windows.Forms.Button buttonAddLinker;
        private System.Windows.Forms.ListView listViewLinker;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox textboxLinkerPath;
        private System.Windows.Forms.LinkLabel linkLabelHelp;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
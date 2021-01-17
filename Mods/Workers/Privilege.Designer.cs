namespace BeatificaBytes.Synology.Mods
{
    partial class Privilege
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
            this.groupBoxDefaults = new System.Windows.Forms.GroupBox();
            this.textBoxGroupname = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxRunAs = new System.Windows.Forms.ComboBox();
            this.labelRunAs = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCtrlScript = new System.Windows.Forms.TabPage();
            this.tabPageExecutable = new System.Windows.Forms.TabPage();
            this.tabPageTool = new System.Windows.Forms.TabPage();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonRemove = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.listViewCtrlScript = new System.Windows.Forms.ListView();
            this.buttonCtrlScriptAdd = new System.Windows.Forms.Button();
            this.buttonCtrlScriptDelete = new System.Windows.Forms.Button();
            this.buttonCtrlScriptEdit = new System.Windows.Forms.Button();
            this.buttonCtrlScriptCancel = new System.Windows.Forms.Button();
            this.buttonCtrlScriptSave = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxDefaults.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageCtrlScript.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.Location = new System.Drawing.Point(12, 432);
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
            this.buttonCancel.Location = new System.Drawing.Point(431, 432);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBoxDefaults
            // 
            this.groupBoxDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDefaults.Controls.Add(this.textBoxGroupname);
            this.groupBoxDefaults.Controls.Add(this.textBoxUsername);
            this.groupBoxDefaults.Controls.Add(this.label2);
            this.groupBoxDefaults.Controls.Add(this.label1);
            this.groupBoxDefaults.Controls.Add(this.comboBoxRunAs);
            this.groupBoxDefaults.Controls.Add(this.labelRunAs);
            this.groupBoxDefaults.Location = new System.Drawing.Point(12, 12);
            this.groupBoxDefaults.Name = "groupBoxDefaults";
            this.groupBoxDefaults.Size = new System.Drawing.Size(494, 113);
            this.groupBoxDefaults.TabIndex = 2;
            this.groupBoxDefaults.TabStop = false;
            this.groupBoxDefaults.Text = "Defaults";
            // 
            // textBoxGroupname
            // 
            this.textBoxGroupname.Location = new System.Drawing.Point(75, 82);
            this.textBoxGroupname.Name = "textBoxGroupname";
            this.textBoxGroupname.Size = new System.Drawing.Size(100, 20);
            this.textBoxGroupname.TabIndex = 5;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(75, 60);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(100, 20);
            this.textBoxUsername.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Groupname:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username:";
            // 
            // comboBoxRunAs
            // 
            this.comboBoxRunAs.FormattingEnabled = true;
            this.comboBoxRunAs.Items.AddRange(new object[] {
            "package",
            "system",
            "root"});
            this.comboBoxRunAs.Location = new System.Drawing.Point(75, 26);
            this.comboBoxRunAs.Name = "comboBoxRunAs";
            this.comboBoxRunAs.Size = new System.Drawing.Size(100, 21);
            this.comboBoxRunAs.TabIndex = 1;
            this.comboBoxRunAs.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxRunAs_Validating);
            this.comboBoxRunAs.Validated += new System.EventHandler(this.comboBoxRunAs_Validated);
            // 
            // labelRunAs
            // 
            this.labelRunAs.AutoSize = true;
            this.labelRunAs.Location = new System.Drawing.Point(6, 29);
            this.labelRunAs.Name = "labelRunAs";
            this.labelRunAs.Size = new System.Drawing.Size(45, 13);
            this.labelRunAs.TabIndex = 0;
            this.labelRunAs.Text = "Run-As:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageCtrlScript);
            this.tabControl.Controls.Add(this.tabPageExecutable);
            this.tabControl.Controls.Add(this.tabPageTool);
            this.tabControl.Location = new System.Drawing.Point(12, 143);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(494, 283);
            this.tabControl.TabIndex = 3;
            // 
            // tabPageCtrlScript
            // 
            this.tabPageCtrlScript.Controls.Add(this.comboBox2);
            this.tabPageCtrlScript.Controls.Add(this.comboBox1);
            this.tabPageCtrlScript.Controls.Add(this.buttonCtrlScriptSave);
            this.tabPageCtrlScript.Controls.Add(this.buttonCtrlScriptCancel);
            this.tabPageCtrlScript.Controls.Add(this.buttonCtrlScriptEdit);
            this.tabPageCtrlScript.Controls.Add(this.buttonCtrlScriptDelete);
            this.tabPageCtrlScript.Controls.Add(this.buttonCtrlScriptAdd);
            this.tabPageCtrlScript.Controls.Add(this.listViewCtrlScript);
            this.tabPageCtrlScript.Location = new System.Drawing.Point(4, 22);
            this.tabPageCtrlScript.Name = "tabPageCtrlScript";
            this.tabPageCtrlScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCtrlScript.Size = new System.Drawing.Size(486, 257);
            this.tabPageCtrlScript.TabIndex = 0;
            this.tabPageCtrlScript.Text = "Ctrl-Script";
            this.tabPageCtrlScript.UseVisualStyleBackColor = true;
            // 
            // tabPageExecutable
            // 
            this.tabPageExecutable.Location = new System.Drawing.Point(4, 22);
            this.tabPageExecutable.Name = "tabPageExecutable";
            this.tabPageExecutable.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExecutable.Size = new System.Drawing.Size(486, 219);
            this.tabPageExecutable.TabIndex = 1;
            this.tabPageExecutable.Text = "Executable";
            this.tabPageExecutable.UseVisualStyleBackColor = true;
            // 
            // tabPageTool
            // 
            this.tabPageTool.Location = new System.Drawing.Point(4, 22);
            this.tabPageTool.Name = "tabPageTool";
            this.tabPageTool.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTool.Size = new System.Drawing.Size(486, 219);
            this.tabPageTool.TabIndex = 2;
            this.tabPageTool.Text = "Tool";
            this.tabPageTool.UseVisualStyleBackColor = true;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Location = new System.Drawing.Point(350, 432);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 4;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // listViewCtrlScript
            // 
            this.listViewCtrlScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewCtrlScript.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewCtrlScript.FullRowSelect = true;
            this.listViewCtrlScript.GridLines = true;
            this.listViewCtrlScript.Location = new System.Drawing.Point(6, 6);
            this.listViewCtrlScript.Name = "listViewCtrlScript";
            this.listViewCtrlScript.Size = new System.Drawing.Size(474, 184);
            this.listViewCtrlScript.TabIndex = 0;
            this.listViewCtrlScript.UseCompatibleStateImageBehavior = false;
            this.listViewCtrlScript.View = System.Windows.Forms.View.Details;
            this.listViewCtrlScript.SelectedIndexChanged += new System.EventHandler(this.listViewCtrlScript_SelectedIndexChanged);
            this.listViewCtrlScript.DoubleClick += new System.EventHandler(this.listViewCtrlScript_DoubleClick);
            // 
            // buttonCtrlScriptAdd
            // 
            this.buttonCtrlScriptAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCtrlScriptAdd.Location = new System.Drawing.Point(6, 223);
            this.buttonCtrlScriptAdd.Name = "buttonCtrlScriptAdd";
            this.buttonCtrlScriptAdd.Size = new System.Drawing.Size(66, 28);
            this.buttonCtrlScriptAdd.TabIndex = 1;
            this.buttonCtrlScriptAdd.Text = "Add";
            this.buttonCtrlScriptAdd.UseVisualStyleBackColor = true;
            this.buttonCtrlScriptAdd.Click += new System.EventHandler(this.buttonAddItem_Click);
            // 
            // buttonCtrlScriptDelete
            // 
            this.buttonCtrlScriptDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCtrlScriptDelete.Location = new System.Drawing.Point(78, 223);
            this.buttonCtrlScriptDelete.Name = "buttonCtrlScriptDelete";
            this.buttonCtrlScriptDelete.Size = new System.Drawing.Size(66, 28);
            this.buttonCtrlScriptDelete.TabIndex = 2;
            this.buttonCtrlScriptDelete.Text = "Delete";
            this.buttonCtrlScriptDelete.UseVisualStyleBackColor = true;
            this.buttonCtrlScriptDelete.Click += new System.EventHandler(this.buttonDeleteItem_Click);
            // 
            // buttonCtrlScriptEdit
            // 
            this.buttonCtrlScriptEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCtrlScriptEdit.Location = new System.Drawing.Point(150, 223);
            this.buttonCtrlScriptEdit.Name = "buttonCtrlScriptEdit";
            this.buttonCtrlScriptEdit.Size = new System.Drawing.Size(66, 28);
            this.buttonCtrlScriptEdit.TabIndex = 3;
            this.buttonCtrlScriptEdit.Text = "Edit";
            this.buttonCtrlScriptEdit.UseVisualStyleBackColor = true;
            this.buttonCtrlScriptEdit.Click += new System.EventHandler(this.buttonEditItem_Click);
            // 
            // buttonCtrlScriptCancel
            // 
            this.buttonCtrlScriptCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCtrlScriptCancel.Location = new System.Drawing.Point(222, 223);
            this.buttonCtrlScriptCancel.Name = "buttonCtrlScriptCancel";
            this.buttonCtrlScriptCancel.Size = new System.Drawing.Size(66, 28);
            this.buttonCtrlScriptCancel.TabIndex = 4;
            this.buttonCtrlScriptCancel.Text = "Cancel";
            this.buttonCtrlScriptCancel.UseVisualStyleBackColor = true;
            this.buttonCtrlScriptCancel.Click += new System.EventHandler(this.buttonCancelItem_Click);
            // 
            // buttonCtrlScriptSave
            // 
            this.buttonCtrlScriptSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCtrlScriptSave.Location = new System.Drawing.Point(294, 223);
            this.buttonCtrlScriptSave.Name = "buttonCtrlScriptSave";
            this.buttonCtrlScriptSave.Size = new System.Drawing.Size(66, 28);
            this.buttonCtrlScriptSave.TabIndex = 5;
            this.buttonCtrlScriptSave.Text = "Save";
            this.buttonCtrlScriptSave.UseVisualStyleBackColor = true;
            this.buttonCtrlScriptSave.Click += new System.EventHandler(this.buttonSaveItem_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 196);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(138, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "root",
            "system",
            "package"});
            this.comboBox2.Location = new System.Drawing.Point(150, 196);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(138, 21);
            this.comboBox2.TabIndex = 7;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Action";
            this.columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Run-as";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 140;
            // 
            // Privilege
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 467);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.groupBoxDefaults);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Name = "Privilege";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure Privilege";
            this.groupBoxDefaults.ResumeLayout(false);
            this.groupBoxDefaults.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageCtrlScript.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBoxDefaults;
        private System.Windows.Forms.TextBox textBoxGroupname;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxRunAs;
        private System.Windows.Forms.Label labelRunAs;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageCtrlScript;
        private System.Windows.Forms.TabPage tabPageExecutable;
        private System.Windows.Forms.TabPage tabPageTool;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ListView listViewCtrlScript;
        private System.Windows.Forms.Button buttonCtrlScriptSave;
        private System.Windows.Forms.Button buttonCtrlScriptCancel;
        private System.Windows.Forms.Button buttonCtrlScriptEdit;
        private System.Windows.Forms.Button buttonCtrlScriptDelete;
        private System.Windows.Forms.Button buttonCtrlScriptAdd;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}
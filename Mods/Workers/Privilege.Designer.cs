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
            this.groupBoxDefaults.SuspendLayout();
            this.tabControl.SuspendLayout();
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
            this.tabControl.Size = new System.Drawing.Size(494, 245);
            this.tabControl.TabIndex = 3;
            // 
            // tabPageCtrlScript
            // 
            this.tabPageCtrlScript.Location = new System.Drawing.Point(4, 22);
            this.tabPageCtrlScript.Name = "tabPageCtrlScript";
            this.tabPageCtrlScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCtrlScript.Size = new System.Drawing.Size(486, 219);
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
    }
}
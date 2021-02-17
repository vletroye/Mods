namespace BeatificaBytes.Synology.Mods
{
    partial class ScriptForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageScript1 = new System.Windows.Forms.TabPage();
            this.tabPageScript2 = new System.Windows.Forms.TabPage();
            this.tabPageVariables = new System.Windows.Forms.TabPage();
            this.listBoxVariables = new System.Windows.Forms.ListBox();
            this.linkLabelHelp = new System.Windows.Forms.LinkLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonSpellCheck = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tabControl.SuspendLayout();
            this.tabPageVariables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.Location = new System.Drawing.Point(12, 694);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(921, 694);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageScript1);
            this.tabControl.Controls.Add(this.tabPageScript2);
            this.tabControl.Controls.Add(this.tabPageVariables);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(494, 667);
            this.tabControl.TabIndex = 4;
            this.tabControl.Tag = "";
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageScript1
            // 
            this.tabPageScript1.Location = new System.Drawing.Point(4, 22);
            this.tabPageScript1.Name = "tabPageScript1";
            this.tabPageScript1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScript1.Size = new System.Drawing.Size(486, 641);
            this.tabPageScript1.TabIndex = 0;
            this.tabPageScript1.Tag = "TabScript1";
            this.tabPageScript1.Text = "Script1 Editor";
            this.tabPageScript1.UseVisualStyleBackColor = true;
            // 
            // tabPageScript2
            // 
            this.tabPageScript2.Location = new System.Drawing.Point(4, 22);
            this.tabPageScript2.Name = "tabPageScript2";
            this.tabPageScript2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScript2.Size = new System.Drawing.Size(486, 641);
            this.tabPageScript2.TabIndex = 1;
            this.tabPageScript2.Tag = "TabScript2";
            this.tabPageScript2.Text = "Script2 Editor";
            this.tabPageScript2.UseVisualStyleBackColor = true;
            // 
            // tabPageVariables
            // 
            this.tabPageVariables.Controls.Add(this.listBoxVariables);
            this.tabPageVariables.Location = new System.Drawing.Point(4, 22);
            this.tabPageVariables.Name = "tabPageVariables";
            this.tabPageVariables.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVariables.Size = new System.Drawing.Size(486, 641);
            this.tabPageVariables.TabIndex = 2;
            this.tabPageVariables.Tag = "TabScript3";
            this.tabPageVariables.Text = "Variables";
            this.tabPageVariables.UseVisualStyleBackColor = true;
            // 
            // listBoxVariables
            // 
            this.listBoxVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxVariables.FormattingEnabled = true;
            this.listBoxVariables.HorizontalScrollbar = true;
            this.listBoxVariables.Location = new System.Drawing.Point(6, 6);
            this.listBoxVariables.Name = "listBoxVariables";
            this.listBoxVariables.Size = new System.Drawing.Size(64, 524);
            this.listBoxVariables.TabIndex = 0;
            this.toolTip.SetToolTip(this.listBoxVariables, "Double click a Variable to copy it into the Clipboard.");
            this.listBoxVariables.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxVariables_MouseDoubleClick);
            // 
            // linkLabelHelp
            // 
            this.linkLabelHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelHelp.AutoSize = true;
            this.linkLabelHelp.Location = new System.Drawing.Point(977, 3);
            this.linkLabelHelp.Name = "linkLabelHelp";
            this.linkLabelHelp.Size = new System.Drawing.Size(29, 13);
            this.linkLabelHelp.TabIndex = 5;
            this.linkLabelHelp.TabStop = true;
            this.linkLabelHelp.Text = "Help";
            this.linkLabelHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // buttonSpellCheck
            // 
            this.buttonSpellCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSpellCheck.Location = new System.Drawing.Point(93, 694);
            this.buttonSpellCheck.Name = "buttonSpellCheck";
            this.buttonSpellCheck.Size = new System.Drawing.Size(83, 23);
            this.buttonSpellCheck.TabIndex = 6;
            this.buttonSpellCheck.Text = "Shell Checker";
            this.buttonSpellCheck.UseVisualStyleBackColor = true;
            this.buttonSpellCheck.Click += new System.EventHandler(this.buttonSpellCheck_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(12, 21);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tabControl);
            this.splitContainer.Size = new System.Drawing.Size(984, 667);
            this.splitContainer.SplitterDistance = 497;
            this.splitContainer.TabIndex = 7;
            this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer_SplitterMoved);
            // 
            // ScriptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1009, 729);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.buttonSpellCheck);
            this.Controls.Add(this.linkLabelHelp);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(614, 648);
            this.Name = "ScriptForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Advanced Editor - Powered by ScintillaNet";
            this.Activated += new System.EventHandler(this.ScriptForm_Activated);
            this.Load += new System.EventHandler(this.ScriptForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageVariables.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageScript1;
        private System.Windows.Forms.TabPage tabPageScript2;
        private System.Windows.Forms.TabPage tabPageVariables;
        private System.Windows.Forms.ListBox listBoxVariables;
        private System.Windows.Forms.LinkLabel linkLabelHelp;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonSpellCheck;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}
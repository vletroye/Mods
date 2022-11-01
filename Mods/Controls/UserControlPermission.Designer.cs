namespace BeatificaBytes.Synology.Mods.Controls
{
    partial class UserControlPermission
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxUser = new System.Windows.Forms.GroupBox();
            this.checkedListBoxUser = new System.Windows.Forms.CheckedListBox();
            this.groupBoxGroup = new System.Windows.Forms.GroupBox();
            this.checkedListBoxGroup = new System.Windows.Forms.CheckedListBox();
            this.groupBoxOther = new System.Windows.Forms.GroupBox();
            this.checkedListBoxOthers = new System.Windows.Forms.CheckedListBox();
            this.groupBoxUser.SuspendLayout();
            this.groupBoxGroup.SuspendLayout();
            this.groupBoxOther.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxUser
            // 
            this.groupBoxUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxUser.Controls.Add(this.checkedListBoxUser);
            this.groupBoxUser.Location = new System.Drawing.Point(0, 0);
            this.groupBoxUser.Name = "groupBoxUser";
            this.groupBoxUser.Size = new System.Drawing.Size(113, 77);
            this.groupBoxUser.TabIndex = 0;
            this.groupBoxUser.TabStop = false;
            this.groupBoxUser.Text = "User";
            // 
            // checkedListBoxUser
            // 
            this.checkedListBoxUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxUser.CausesValidation = false;
            this.checkedListBoxUser.CheckOnClick = true;
            this.checkedListBoxUser.FormattingEnabled = true;
            this.checkedListBoxUser.Items.AddRange(new object[] {
            "Read",
            "Write",
            "Execute"});
            this.checkedListBoxUser.Location = new System.Drawing.Point(6, 19);
            this.checkedListBoxUser.Name = "checkedListBoxUser";
            this.checkedListBoxUser.Size = new System.Drawing.Size(101, 49);
            this.checkedListBoxUser.TabIndex = 0;
            // 
            // groupBoxGroup
            // 
            this.groupBoxGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGroup.Controls.Add(this.checkedListBoxGroup);
            this.groupBoxGroup.Location = new System.Drawing.Point(113, 0);
            this.groupBoxGroup.Name = "groupBoxGroup";
            this.groupBoxGroup.Size = new System.Drawing.Size(113, 77);
            this.groupBoxGroup.TabIndex = 1;
            this.groupBoxGroup.TabStop = false;
            this.groupBoxGroup.Text = "Group";
            // 
            // checkedListBoxGroup
            // 
            this.checkedListBoxGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxGroup.CausesValidation = false;
            this.checkedListBoxGroup.CheckOnClick = true;
            this.checkedListBoxGroup.FormattingEnabled = true;
            this.checkedListBoxGroup.Items.AddRange(new object[] {
            "Read",
            "Write",
            "Execute"});
            this.checkedListBoxGroup.Location = new System.Drawing.Point(6, 19);
            this.checkedListBoxGroup.Name = "checkedListBoxGroup";
            this.checkedListBoxGroup.Size = new System.Drawing.Size(101, 49);
            this.checkedListBoxGroup.TabIndex = 1;
            // 
            // groupBoxOther
            // 
            this.groupBoxOther.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOther.Controls.Add(this.checkedListBoxOthers);
            this.groupBoxOther.Location = new System.Drawing.Point(226, 0);
            this.groupBoxOther.Name = "groupBoxOther";
            this.groupBoxOther.Size = new System.Drawing.Size(113, 77);
            this.groupBoxOther.TabIndex = 2;
            this.groupBoxOther.TabStop = false;
            this.groupBoxOther.Text = "Others";
            // 
            // checkedListBoxOthers
            // 
            this.checkedListBoxOthers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxOthers.CausesValidation = false;
            this.checkedListBoxOthers.CheckOnClick = true;
            this.checkedListBoxOthers.FormattingEnabled = true;
            this.checkedListBoxOthers.Items.AddRange(new object[] {
            "Read",
            "Write",
            "Execute"});
            this.checkedListBoxOthers.Location = new System.Drawing.Point(6, 19);
            this.checkedListBoxOthers.Name = "checkedListBoxOthers";
            this.checkedListBoxOthers.Size = new System.Drawing.Size(101, 49);
            this.checkedListBoxOthers.TabIndex = 2;
            // 
            // UserControlPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxOther);
            this.Controls.Add(this.groupBoxGroup);
            this.Controls.Add(this.groupBoxUser);
            this.Name = "UserControlPermission";
            this.Size = new System.Drawing.Size(339, 80);
            this.groupBoxUser.ResumeLayout(false);
            this.groupBoxGroup.ResumeLayout(false);
            this.groupBoxOther.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxUser;
        private System.Windows.Forms.CheckedListBox checkedListBoxGroup;
        private System.Windows.Forms.CheckedListBox checkedListBoxUser;
        private System.Windows.Forms.GroupBox groupBoxGroup;
        private System.Windows.Forms.GroupBox groupBoxOther;
        private System.Windows.Forms.CheckedListBox checkedListBoxOthers;
    }
}

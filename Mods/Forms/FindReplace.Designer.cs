namespace ScintillaFindReplaceControl
{
    partial class FindReplace
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
            this.tabControlFindReplace = new System.Windows.Forms.TabControl();
            this.tabPageFind = new System.Windows.Forms.TabPage();
            this.buttonFindNext = new System.Windows.Forms.Button();
            this.textBoxFind = new System.Windows.Forms.TextBox();
            this.labelFind = new System.Windows.Forms.Label();
            this.tabPageReplace = new System.Windows.Forms.TabPage();
            this.buttonFindNextToReplace = new System.Windows.Forms.Button();
            this.buttonReplaceAll = new System.Windows.Forms.Button();
            this.textBoxReplace = new System.Windows.Forms.TextBox();
            this.labelReplace = new System.Windows.Forms.Label();
            this.buttonReplaceNext = new System.Windows.Forms.Button();
            this.textBoxFindRep = new System.Windows.Forms.TextBox();
            this.labelFindRep = new System.Windows.Forms.Label();
            this.checkBoxRegEx = new System.Windows.Forms.CheckBox();
            this.checkBoxWholeWord = new System.Windows.Forms.CheckBox();
            this.checkBoxMatchCase = new System.Windows.Forms.CheckBox();
            this.checkBoxWordStart = new System.Windows.Forms.CheckBox();
            this.checkBoxBackward = new System.Windows.Forms.CheckBox();
            this.tabControlFindReplace.SuspendLayout();
            this.tabPageFind.SuspendLayout();
            this.tabPageReplace.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlFindReplace
            // 
            this.tabControlFindReplace.Controls.Add(this.tabPageFind);
            this.tabControlFindReplace.Controls.Add(this.tabPageReplace);
            this.tabControlFindReplace.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlFindReplace.Location = new System.Drawing.Point(0, 0);
            this.tabControlFindReplace.Name = "tabControlFindReplace";
            this.tabControlFindReplace.SelectedIndex = 0;
            this.tabControlFindReplace.Size = new System.Drawing.Size(449, 120);
            this.tabControlFindReplace.TabIndex = 0;
            this.tabControlFindReplace.SelectedIndexChanged += new System.EventHandler(this.tabControlFindReplace_TabIndexChanged);
            // 
            // tabPageFind
            // 
            this.tabPageFind.Controls.Add(this.buttonFindNext);
            this.tabPageFind.Controls.Add(this.textBoxFind);
            this.tabPageFind.Controls.Add(this.labelFind);
            this.tabPageFind.Location = new System.Drawing.Point(4, 22);
            this.tabPageFind.Name = "tabPageFind";
            this.tabPageFind.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFind.Size = new System.Drawing.Size(441, 94);
            this.tabPageFind.TabIndex = 0;
            this.tabPageFind.Text = "Find";
            this.tabPageFind.UseVisualStyleBackColor = true;
            // 
            // buttonFindNext
            // 
            this.buttonFindNext.Location = new System.Drawing.Point(344, 11);
            this.buttonFindNext.Name = "buttonFindNext";
            this.buttonFindNext.Size = new System.Drawing.Size(89, 23);
            this.buttonFindNext.TabIndex = 1;
            this.buttonFindNext.Text = "Find Next";
            this.buttonFindNext.UseVisualStyleBackColor = true;
            this.buttonFindNext.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // textBoxFind
            // 
            this.textBoxFind.Location = new System.Drawing.Point(86, 11);
            this.textBoxFind.Name = "textBoxFind";
            this.textBoxFind.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFind.Size = new System.Drawing.Size(252, 20);
            this.textBoxFind.TabIndex = 0;
            this.textBoxFind.TextChanged += new System.EventHandler(this.textBoxFind_TextChanged);
            // 
            // labelFind
            // 
            this.labelFind.AutoSize = true;
            this.labelFind.Location = new System.Drawing.Point(10, 14);
            this.labelFind.Name = "labelFind";
            this.labelFind.Size = new System.Drawing.Size(56, 13);
            this.labelFind.TabIndex = 0;
            this.labelFind.Text = "Find what:";
            // 
            // tabPageReplace
            // 
            this.tabPageReplace.Controls.Add(this.buttonFindNextToReplace);
            this.tabPageReplace.Controls.Add(this.buttonReplaceAll);
            this.tabPageReplace.Controls.Add(this.textBoxReplace);
            this.tabPageReplace.Controls.Add(this.labelReplace);
            this.tabPageReplace.Controls.Add(this.buttonReplaceNext);
            this.tabPageReplace.Controls.Add(this.textBoxFindRep);
            this.tabPageReplace.Controls.Add(this.labelFindRep);
            this.tabPageReplace.Location = new System.Drawing.Point(4, 22);
            this.tabPageReplace.Name = "tabPageReplace";
            this.tabPageReplace.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReplace.Size = new System.Drawing.Size(441, 94);
            this.tabPageReplace.TabIndex = 1;
            this.tabPageReplace.Text = "Replace";
            this.tabPageReplace.UseVisualStyleBackColor = true;
            // 
            // buttonFindNextToReplace
            // 
            this.buttonFindNextToReplace.Location = new System.Drawing.Point(344, 11);
            this.buttonFindNextToReplace.Name = "buttonFindNextToReplace";
            this.buttonFindNextToReplace.Size = new System.Drawing.Size(89, 23);
            this.buttonFindNextToReplace.TabIndex = 2;
            this.buttonFindNextToReplace.Text = "Find Next";
            this.buttonFindNextToReplace.UseVisualStyleBackColor = true;
            this.buttonFindNextToReplace.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // buttonReplaceAll
            // 
            this.buttonReplaceAll.Location = new System.Drawing.Point(344, 63);
            this.buttonReplaceAll.Name = "buttonReplaceAll";
            this.buttonReplaceAll.Size = new System.Drawing.Size(89, 23);
            this.buttonReplaceAll.TabIndex = 4;
            this.buttonReplaceAll.Text = "Replace &All";
            this.buttonReplaceAll.UseVisualStyleBackColor = true;
            this.buttonReplaceAll.Click += new System.EventHandler(this.buttonReplaceAll_Click);
            // 
            // textBoxReplace
            // 
            this.textBoxReplace.Location = new System.Drawing.Point(86, 37);
            this.textBoxReplace.Name = "textBoxReplace";
            this.textBoxReplace.Size = new System.Drawing.Size(252, 20);
            this.textBoxReplace.TabIndex = 1;
            // 
            // labelReplace
            // 
            this.labelReplace.AutoSize = true;
            this.labelReplace.Location = new System.Drawing.Point(10, 37);
            this.labelReplace.Name = "labelReplace";
            this.labelReplace.Size = new System.Drawing.Size(72, 13);
            this.labelReplace.TabIndex = 6;
            this.labelReplace.Text = "Replace with:";
            // 
            // buttonReplaceNext
            // 
            this.buttonReplaceNext.Location = new System.Drawing.Point(344, 37);
            this.buttonReplaceNext.Name = "buttonReplaceNext";
            this.buttonReplaceNext.Size = new System.Drawing.Size(89, 23);
            this.buttonReplaceNext.TabIndex = 3;
            this.buttonReplaceNext.Text = "&Replace";
            this.buttonReplaceNext.UseVisualStyleBackColor = true;
            this.buttonReplaceNext.Click += new System.EventHandler(this.buttonReplace_Click);
            // 
            // textBoxFindRep
            // 
            this.textBoxFindRep.Location = new System.Drawing.Point(86, 11);
            this.textBoxFindRep.Name = "textBoxFindRep";
            this.textBoxFindRep.Size = new System.Drawing.Size(252, 20);
            this.textBoxFindRep.TabIndex = 0;
            this.textBoxFindRep.TextChanged += new System.EventHandler(this.textBoxFindRep_TextChanged);
            // 
            // labelFindRep
            // 
            this.labelFindRep.AutoSize = true;
            this.labelFindRep.Location = new System.Drawing.Point(10, 14);
            this.labelFindRep.Name = "labelFindRep";
            this.labelFindRep.Size = new System.Drawing.Size(56, 13);
            this.labelFindRep.TabIndex = 3;
            this.labelFindRep.Text = "Find what:";
            // 
            // checkBoxRegEx
            // 
            this.checkBoxRegEx.AutoSize = true;
            this.checkBoxRegEx.Location = new System.Drawing.Point(90, 149);
            this.checkBoxRegEx.Name = "checkBoxRegEx";
            this.checkBoxRegEx.Size = new System.Drawing.Size(117, 17);
            this.checkBoxRegEx.TabIndex = 4;
            this.checkBoxRegEx.Text = "Regular Expression";
            this.checkBoxRegEx.UseVisualStyleBackColor = true;
            // 
            // checkBoxWholeWord
            // 
            this.checkBoxWholeWord.AutoSize = true;
            this.checkBoxWholeWord.Location = new System.Drawing.Point(217, 149);
            this.checkBoxWholeWord.Name = "checkBoxWholeWord";
            this.checkBoxWholeWord.Size = new System.Drawing.Size(86, 17);
            this.checkBoxWholeWord.TabIndex = 6;
            this.checkBoxWholeWord.Text = "Whole Word";
            this.checkBoxWholeWord.UseVisualStyleBackColor = true;
            // 
            // checkBoxMatchCase
            // 
            this.checkBoxMatchCase.AutoSize = true;
            this.checkBoxMatchCase.Location = new System.Drawing.Point(90, 126);
            this.checkBoxMatchCase.Name = "checkBoxMatchCase";
            this.checkBoxMatchCase.Size = new System.Drawing.Size(83, 17);
            this.checkBoxMatchCase.TabIndex = 3;
            this.checkBoxMatchCase.Text = "Match Case";
            this.checkBoxMatchCase.UseVisualStyleBackColor = true;
            // 
            // checkBoxWordStart
            // 
            this.checkBoxWordStart.AutoSize = true;
            this.checkBoxWordStart.Location = new System.Drawing.Point(217, 126);
            this.checkBoxWordStart.Name = "checkBoxWordStart";
            this.checkBoxWordStart.Size = new System.Drawing.Size(77, 17);
            this.checkBoxWordStart.TabIndex = 5;
            this.checkBoxWordStart.Text = "Word Start";
            this.checkBoxWordStart.UseVisualStyleBackColor = true;
            // 
            // checkBoxBackward
            // 
            this.checkBoxBackward.AutoSize = true;
            this.checkBoxBackward.Location = new System.Drawing.Point(4, 126);
            this.checkBoxBackward.Name = "checkBoxBackward";
            this.checkBoxBackward.Size = new System.Drawing.Size(74, 17);
            this.checkBoxBackward.TabIndex = 7;
            this.checkBoxBackward.Text = "Backward";
            this.checkBoxBackward.UseVisualStyleBackColor = true;
            // 
            // FindReplace
            // 
            this.AcceptButton = this.buttonFindNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 172);
            this.Controls.Add(this.checkBoxBackward);
            this.Controls.Add(this.tabControlFindReplace);
            this.Controls.Add(this.checkBoxWordStart);
            this.Controls.Add(this.checkBoxMatchCase);
            this.Controls.Add(this.checkBoxWholeWord);
            this.Controls.Add(this.checkBoxRegEx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FindReplace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find and Replace";
            this.Activated += new System.EventHandler(this.FindReplace_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindReplace_FormClosing);
            this.tabControlFindReplace.ResumeLayout(false);
            this.tabPageFind.ResumeLayout(false);
            this.tabPageFind.PerformLayout();
            this.tabPageReplace.ResumeLayout(false);
            this.tabPageReplace.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlFindReplace;
        private System.Windows.Forms.TabPage tabPageFind;
        private System.Windows.Forms.TabPage tabPageReplace;
        private System.Windows.Forms.TextBox textBoxFind;
        private System.Windows.Forms.Label labelFind;
        private System.Windows.Forms.Button buttonFindNext;
        private System.Windows.Forms.TextBox textBoxReplace;
        private System.Windows.Forms.Label labelReplace;
        private System.Windows.Forms.Button buttonReplaceNext;
        private System.Windows.Forms.TextBox textBoxFindRep;
        private System.Windows.Forms.Label labelFindRep;
        private System.Windows.Forms.Button buttonReplaceAll;
        private System.Windows.Forms.Button buttonFindNextToReplace;
        private System.Windows.Forms.CheckBox checkBoxRegEx;
        private System.Windows.Forms.CheckBox checkBoxWholeWord;
        private System.Windows.Forms.CheckBox checkBoxMatchCase;
        private System.Windows.Forms.CheckBox checkBoxWordStart;
        private System.Windows.Forms.CheckBox checkBoxBackward;
    }
}
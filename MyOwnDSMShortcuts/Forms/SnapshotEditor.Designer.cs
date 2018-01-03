namespace BeatificaBytes.Synology.Mods
{
    partial class SnapshotEditor
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.pictureBoxSnapshot = new System.Windows.Forms.PictureBox();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxAutoResize = new System.Windows.Forms.CheckBox();
            this.labelOriginalSize = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxOriginalHeight = new System.Windows.Forms.TextBox();
            this.textBoxOriginalWidth = new System.Windows.Forms.TextBox();
            this.labelNewSize = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSnapshot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(245, 377);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.Location = new System.Drawing.Point(12, 377);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // pictureBoxSnapshot
            // 
            this.pictureBoxSnapshot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxSnapshot.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxSnapshot.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxSnapshot.Name = "pictureBoxSnapshot";
            this.pictureBoxSnapshot.Size = new System.Drawing.Size(308, 308);
            this.pictureBoxSnapshot.TabIndex = 4;
            this.pictureBoxSnapshot.TabStop = false;
            this.pictureBoxSnapshot.SizeChanged += new System.EventHandler(this.pictureBoxSnapshot_SizeChanged);
            this.pictureBoxSnapshot.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBoxSnapshot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBoxSnapshot.MouseEnter += new System.EventHandler(this.picture_MouseEnter);
            this.pictureBoxSnapshot.MouseLeave += new System.EventHandler(this.picture_MouseLeave);
            this.pictureBoxSnapshot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBoxSnapshot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxWidth.Location = new System.Drawing.Point(80, 351);
            this.textBoxWidth.MaxLength = 4;
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(50, 20);
            this.textBoxWidth.TabIndex = 0;
            this.textBoxWidth.TextChanged += new System.EventHandler(this.textBoxWidth_TextChanged);
            this.textBoxWidth.Leave += new System.EventHandler(this.textBoxSize_Leave);
            this.textBoxWidth.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxWidth_Validating);
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxHeight.Location = new System.Drawing.Point(148, 351);
            this.textBoxHeight.MaxLength = 4;
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(50, 20);
            this.textBoxHeight.TabIndex = 1;
            this.textBoxHeight.TextChanged += new System.EventHandler(this.textBoxHeight_TextChanged);
            this.textBoxHeight.Leave += new System.EventHandler(this.textBoxSize_Leave);
            this.textBoxHeight.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxWidth_Validating);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 355);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "X";
            // 
            // checkBoxAutoResize
            // 
            this.checkBoxAutoResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxAutoResize.AutoSize = true;
            this.checkBoxAutoResize.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxAutoResize.Location = new System.Drawing.Point(93, 383);
            this.checkBoxAutoResize.Name = "checkBoxAutoResize";
            this.checkBoxAutoResize.Size = new System.Drawing.Size(83, 17);
            this.checkBoxAutoResize.TabIndex = 4;
            this.checkBoxAutoResize.Text = "Auto Resize";
            this.checkBoxAutoResize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxAutoResize.UseVisualStyleBackColor = true;
            this.checkBoxAutoResize.CheckedChanged += new System.EventHandler(this.checkBoxAutoResize_CheckedChanged);
            // 
            // labelOriginalSize
            // 
            this.labelOriginalSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelOriginalSize.AutoSize = true;
            this.labelOriginalSize.Location = new System.Drawing.Point(12, 329);
            this.labelOriginalSize.Name = "labelOriginalSize";
            this.labelOriginalSize.Size = new System.Drawing.Size(66, 13);
            this.labelOriginalSize.TabIndex = 11;
            this.labelOriginalSize.Text = "Original size:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 329);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "X";
            // 
            // textBoxOriginalHeight
            // 
            this.textBoxOriginalHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxOriginalHeight.Location = new System.Drawing.Point(148, 325);
            this.textBoxOriginalHeight.MaxLength = 4;
            this.textBoxOriginalHeight.Name = "textBoxOriginalHeight";
            this.textBoxOriginalHeight.ReadOnly = true;
            this.textBoxOriginalHeight.Size = new System.Drawing.Size(50, 20);
            this.textBoxOriginalHeight.TabIndex = 13;
            // 
            // textBoxOriginalWidth
            // 
            this.textBoxOriginalWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxOriginalWidth.Location = new System.Drawing.Point(80, 325);
            this.textBoxOriginalWidth.MaxLength = 4;
            this.textBoxOriginalWidth.Name = "textBoxOriginalWidth";
            this.textBoxOriginalWidth.ReadOnly = true;
            this.textBoxOriginalWidth.Size = new System.Drawing.Size(50, 20);
            this.textBoxOriginalWidth.TabIndex = 12;
            // 
            // labelNewSize
            // 
            this.labelNewSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelNewSize.AutoSize = true;
            this.labelNewSize.Location = new System.Drawing.Point(12, 355);
            this.labelNewSize.Name = "labelNewSize";
            this.labelNewSize.Size = new System.Drawing.Size(53, 13);
            this.labelNewSize.TabIndex = 15;
            this.labelNewSize.Text = "New size:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // SnapshotEditor
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(332, 412);
            this.Controls.Add(this.labelNewSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxOriginalHeight);
            this.Controls.Add(this.textBoxOriginalWidth);
            this.Controls.Add(this.labelOriginalSize);
            this.Controls.Add(this.checkBoxAutoResize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxHeight);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.pictureBoxSnapshot);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(290, 370);
            this.Name = "SnapshotEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Snapshot Editor";
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.SnapshotEditor_Scroll);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SnapshotEditor_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SnapshotEditor_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSnapshot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.PictureBox pictureBoxSnapshot;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxAutoResize;
        private System.Windows.Forms.Label labelOriginalSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxOriginalHeight;
        private System.Windows.Forms.TextBox textBoxOriginalWidth;
        private System.Windows.Forms.Label labelNewSize;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
namespace BeatificaBytes.Synology.Mods
{
    partial class Dependencies
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listViewDependencies = new System.Windows.Forms.ListView();
            this.columnHeaderLabel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDependencies = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxDependencies = new System.Windows.Forms.TextBox();
            this.labelToolTip = new System.Windows.Forms.Label();
            this.panelToolTip = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.Location = new System.Drawing.Point(12, 318);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(567, 318);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // listViewDependencies
            // 
            this.listViewDependencies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDependencies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLabel,
            this.columnHeaderDependencies});
            this.listViewDependencies.FullRowSelect = true;
            this.listViewDependencies.GridLines = true;
            this.listViewDependencies.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDependencies.HideSelection = false;
            this.listViewDependencies.Location = new System.Drawing.Point(12, 12);
            this.listViewDependencies.MultiSelect = false;
            this.listViewDependencies.Name = "listViewDependencies";
            this.listViewDependencies.Size = new System.Drawing.Size(630, 272);
            this.listViewDependencies.TabIndex = 0;
            this.listViewDependencies.UseCompatibleStateImageBehavior = false;
            this.listViewDependencies.View = System.Windows.Forms.View.Details;
            this.listViewDependencies.SelectedIndexChanged += new System.EventHandler(this.listViewDependencies_SelectedIndexChanged);
            this.listViewDependencies.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewDependencies_MouseDoubleClick);
            this.listViewDependencies.Resize += new System.EventHandler(this.listViewDependencies_Resize);
            // 
            // columnHeaderLabel
            // 
            this.columnHeaderLabel.Text = "Dependency Type";
            this.columnHeaderLabel.Width = 150;
            // 
            // columnHeaderDependencies
            // 
            this.columnHeaderDependencies.Text = "Packages or Services";
            this.columnHeaderDependencies.Width = 400;
            // 
            // textBoxDependencies
            // 
            this.textBoxDependencies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDependencies.Location = new System.Drawing.Point(12, 292);
            this.textBoxDependencies.Name = "textBoxDependencies";
            this.textBoxDependencies.Size = new System.Drawing.Size(630, 20);
            this.textBoxDependencies.TabIndex = 1;
            this.textBoxDependencies.TextChanged += new System.EventHandler(this.textBoxDependencies_TextChanged);
            this.textBoxDependencies.DoubleClick += new System.EventHandler(this.textBoxDependencies_DoubleClick);
            // 
            // labelToolTip
            // 
            this.labelToolTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelToolTip.Location = new System.Drawing.Point(21, 349);
            this.labelToolTip.Name = "labelToolTip";
            this.labelToolTip.Size = new System.Drawing.Size(616, 112);
            this.labelToolTip.TabIndex = 20;
            this.labelToolTip.Text = "Help";
            // 
            // panelToolTip
            // 
            this.panelToolTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolTip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelToolTip.Location = new System.Drawing.Point(16, 347);
            this.panelToolTip.Name = "panelToolTip";
            this.panelToolTip.Size = new System.Drawing.Size(626, 116);
            this.panelToolTip.TabIndex = 21;
            // 
            // Dependencies
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(654, 475);
            this.ControlBox = false;
            this.Controls.Add(this.labelToolTip);
            this.Controls.Add(this.panelToolTip);
            this.Controls.Add(this.textBoxDependencies);
            this.Controls.Add(this.listViewDependencies);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.MinimumSize = new System.Drawing.Size(670, 514);
            this.Name = "Dependencies";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit dependencies on other services & packages";
            this.Load += new System.EventHandler(this.Dependencies_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListView listViewDependencies;
        private System.Windows.Forms.ColumnHeader columnHeaderLabel;
        private System.Windows.Forms.ColumnHeader columnHeaderDependencies;
        private System.Windows.Forms.TextBox textBoxDependencies;
        private System.Windows.Forms.Label labelToolTip;
        private System.Windows.Forms.Panel panelToolTip;
    }
}
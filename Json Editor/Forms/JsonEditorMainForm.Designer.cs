namespace ZTn.Json.Editor.Forms
{
    partial class JsonEditorMainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JsonEditorMainForm));
            this.jsonTreeViewSplitContainer = new System.Windows.Forms.SplitContainer();
            this.buttonOk = new System.Windows.Forms.Button();
            this.jsonTreeView = new ZTn.Json.Editor.Forms.JTokenTreeView();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.jsonTypeComboBox = new System.Windows.Forms.ComboBox();
            this.jsonValueTextBox = new System.Windows.Forms.TextBox();
            this.jsonValueLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.newtonsoftJsonTypeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.formMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutJsonEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guiStatusStrip = new System.Windows.Forms.StatusStrip();
            this.actionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.jsonStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.jsonTreeViewSplitContainer)).BeginInit();
            this.jsonTreeViewSplitContainer.Panel1.SuspendLayout();
            this.jsonTreeViewSplitContainer.Panel2.SuspendLayout();
            this.jsonTreeViewSplitContainer.SuspendLayout();
            this.formMenuStrip.SuspendLayout();
            this.guiStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // jsonTreeViewSplitContainer
            // 
            this.jsonTreeViewSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jsonTreeViewSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.jsonTreeViewSplitContainer.Name = "jsonTreeViewSplitContainer";
            // 
            // jsonTreeViewSplitContainer.Panel1
            // 
            this.jsonTreeViewSplitContainer.Panel1.Controls.Add(this.buttonOk);
            this.jsonTreeViewSplitContainer.Panel1.Controls.Add(this.jsonTreeView);
            this.jsonTreeViewSplitContainer.Panel1MinSize = 200;
            // 
            // jsonTreeViewSplitContainer.Panel2
            // 
            this.jsonTreeViewSplitContainer.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.jsonTreeViewSplitContainer.Panel2.Controls.Add(this.buttonCancel);
            this.jsonTreeViewSplitContainer.Panel2.Controls.Add(this.jsonTypeComboBox);
            this.jsonTreeViewSplitContainer.Panel2.Controls.Add(this.jsonValueTextBox);
            this.jsonTreeViewSplitContainer.Panel2.Controls.Add(this.jsonValueLabel);
            this.jsonTreeViewSplitContainer.Panel2.Controls.Add(this.label2);
            this.jsonTreeViewSplitContainer.Panel2.Controls.Add(this.newtonsoftJsonTypeTextBox);
            this.jsonTreeViewSplitContainer.Panel2.Controls.Add(this.label1);
            this.jsonTreeViewSplitContainer.Panel2MinSize = 320;
            this.jsonTreeViewSplitContainer.Size = new System.Drawing.Size(820, 584);
            this.jsonTreeViewSplitContainer.SplitterDistance = 496;
            this.jsonTreeViewSplitContainer.TabIndex = 8;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.Location = new System.Drawing.Point(3, 558);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // jsonTreeView
            // 
            this.jsonTreeView.AllowDrop = true;
            this.jsonTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jsonTreeView.HideSelection = false;
            this.jsonTreeView.Location = new System.Drawing.Point(3, 3);
            this.jsonTreeView.Name = "jsonTreeView";
            this.jsonTreeView.Size = new System.Drawing.Size(490, 549);
            this.jsonTreeView.TabIndex = 0;
            this.jsonTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.jsonTreeView_AfterSelect);
            this.jsonTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.jsonTreeView_NodeMouseClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(242, 558);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // jsonTypeComboBox
            // 
            this.jsonTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.jsonTypeComboBox.Enabled = false;
            this.jsonTypeComboBox.FormattingEnabled = true;
            this.jsonTypeComboBox.Location = new System.Drawing.Point(3, 59);
            this.jsonTypeComboBox.Name = "jsonTypeComboBox";
            this.jsonTypeComboBox.Size = new System.Drawing.Size(154, 21);
            this.jsonTypeComboBox.TabIndex = 7;
            // 
            // jsonValueTextBox
            // 
            this.jsonValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jsonValueTextBox.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.jsonValueTextBox.Location = new System.Drawing.Point(3, 97);
            this.jsonValueTextBox.Multiline = true;
            this.jsonValueTextBox.Name = "jsonValueTextBox";
            this.jsonValueTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.jsonValueTextBox.Size = new System.Drawing.Size(311, 455);
            this.jsonValueTextBox.TabIndex = 6;
            this.jsonValueTextBox.Enter += new System.EventHandler(this.jsonValueTextBox_Enter);
            this.jsonValueTextBox.Leave += new System.EventHandler(this.jsonValueTextBox_Leave);
            // 
            // jsonValueLabel
            // 
            this.jsonValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.jsonValueLabel.AutoSize = true;
            this.jsonValueLabel.Location = new System.Drawing.Point(3, 81);
            this.jsonValueLabel.Name = "jsonValueLabel";
            this.jsonValueLabel.Size = new System.Drawing.Size(65, 13);
            this.jsonValueLabel.TabIndex = 5;
            this.jsonValueLabel.Text = "JSON Value";
            this.jsonValueLabel.TextChanged += new System.EventHandler(this.jsonValueTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "NewtonSoft.Json Type";
            // 
            // newtonsoftJsonTypeTextBox
            // 
            this.newtonsoftJsonTypeTextBox.Location = new System.Drawing.Point(3, 19);
            this.newtonsoftJsonTypeTextBox.Name = "newtonsoftJsonTypeTextBox";
            this.newtonsoftJsonTypeTextBox.ReadOnly = true;
            this.newtonsoftJsonTypeTextBox.Size = new System.Drawing.Size(154, 20);
            this.newtonsoftJsonTypeTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "JSON Type";
            // 
            // formMenuStrip
            // 
            this.formMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.formMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.formMenuStrip.Name = "formMenuStrip";
            this.formMenuStrip.Size = new System.Drawing.Size(820, 24);
            this.formMenuStrip.TabIndex = 0;
            this.formMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importToolStripMenuItem.Text = "&Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutJsonEditorToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(24, 20);
            this.aboutToolStripMenuItem.Text = "?";
            // 
            // aboutJsonEditorToolStripMenuItem
            // 
            this.aboutJsonEditorToolStripMenuItem.Name = "aboutJsonEditorToolStripMenuItem";
            this.aboutJsonEditorToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.aboutJsonEditorToolStripMenuItem.Text = "About Json Editor";
            this.aboutJsonEditorToolStripMenuItem.Click += new System.EventHandler(this.aboutJsonEditorToolStripMenuItem_Click);
            // 
            // guiStatusStrip
            // 
            this.guiStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionStatusLabel,
            this.toolStripStatusLabel1,
            this.jsonStatusLabel});
            this.guiStatusStrip.Location = new System.Drawing.Point(0, 611);
            this.guiStatusStrip.Name = "guiStatusStrip";
            this.guiStatusStrip.Size = new System.Drawing.Size(820, 22);
            this.guiStatusStrip.TabIndex = 9;
            this.guiStatusStrip.Text = "statusStrip";
            // 
            // actionStatusLabel
            // 
            this.actionStatusLabel.Name = "actionStatusLabel";
            this.actionStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.actionStatusLabel.Text = "Status";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(704, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // jsonStatusLabel
            // 
            this.jsonStatusLabel.Name = "jsonStatusLabel";
            this.jsonStatusLabel.Size = new System.Drawing.Size(62, 17);
            this.jsonStatusLabel.Text = "JsonStatus";
            // 
            // JsonEditorMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(820, 633);
            this.ControlBox = false;
            this.Controls.Add(this.guiStatusStrip);
            this.Controls.Add(this.jsonTreeViewSplitContainer);
            this.Controls.Add(this.formMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.formMenuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JsonEditorMainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit your wizard - Powered by ZTn Json Editor";
            this.jsonTreeViewSplitContainer.Panel1.ResumeLayout(false);
            this.jsonTreeViewSplitContainer.Panel2.ResumeLayout(false);
            this.jsonTreeViewSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jsonTreeViewSplitContainer)).EndInit();
            this.jsonTreeViewSplitContainer.ResumeLayout(false);
            this.formMenuStrip.ResumeLayout(false);
            this.formMenuStrip.PerformLayout();
            this.guiStatusStrip.ResumeLayout(false);
            this.guiStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip formMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem newJsonObjectToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem newJsonArrayToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newtonsoftJsonTypeTextBox;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label jsonValueLabel;
        private System.Windows.Forms.SplitContainer jsonTreeViewSplitContainer;
        private System.Windows.Forms.TextBox jsonValueTextBox;
        private System.Windows.Forms.ComboBox jsonTypeComboBox;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutJsonEditorToolStripMenuItem;
        public JTokenTreeView jsonTreeView;
        private System.Windows.Forms.StatusStrip guiStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel actionStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel jsonStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}


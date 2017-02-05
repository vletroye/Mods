namespace ZTn.Json.Editor.Forms
{
    partial class ItemDefinition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemDefinition));
            this.labelValidate = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.comboBoxItemType = new System.Windows.Forms.ComboBox();
            this.toolTipItemDefinition = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // labelValidate
            // 
            this.labelValidate.AutoSize = true;
            this.labelValidate.Location = new System.Drawing.Point(14, 9);
            this.labelValidate.Name = "labelValidate";
            this.labelValidate.Size = new System.Drawing.Size(69, 13);
            this.labelValidate.TabIndex = 0;
            this.labelValidate.Text = "Type of Item:";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(12, 64);
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
            this.buttonCancel.Location = new System.Drawing.Point(339, 64);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(25, 36);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(86, 13);
            this.labelDescription.TabIndex = 4;
            this.labelDescription.Text = "Item Description:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(111, 33);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(303, 20);
            this.textBoxDescription.TabIndex = 1;
            this.toolTipItemDefinition.SetToolTip(this.textBoxDescription, "Syno Property: desc [Optional]\r\nDescribe a component in the label text.\r\n");
            // 
            // comboBoxItemType
            // 
            this.comboBoxItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItemType.FormattingEnabled = true;
            this.comboBoxItemType.Items.AddRange(new object[] {
            "singleselect",
            "multiselect",
            "textfield",
            "password",
            "combobox"});
            this.comboBoxItemType.Location = new System.Drawing.Point(111, 6);
            this.comboBoxItemType.Name = "comboBoxItemType";
            this.comboBoxItemType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxItemType.TabIndex = 0;
            this.toolTipItemDefinition.SetToolTip(this.comboBoxItemType, resources.GetString("comboBoxItemType.ToolTip"));
            this.comboBoxItemType.SelectedIndexChanged += new System.EventHandler(this.comboBoxItemType_SelectedIndexChanged);
            // 
            // ItemDefinition
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(422, 95);
            this.ControlBox = false;
            this.Controls.Add(this.comboBoxItemType);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelValidate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(442, 138);
            this.MinimizeBox = false;
            this.Name = "ItemDefinition";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Item Definition";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelValidate;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.ComboBox comboBoxItemType;
        private System.Windows.Forms.ToolTip toolTipItemDefinition;
    }
}
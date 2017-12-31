namespace ZTn.Json.Editor.Forms
{
    partial class StepDefinition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StepDefinition));
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelValidate = new System.Windows.Forms.Label();
            this.checkBoxValidate = new System.Windows.Forms.CheckBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxActivation = new System.Windows.Forms.TextBox();
            this.textBoxDeactivation = new System.Windows.Forms.TextBox();
            this.toolTipStepDefinition = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(26, 42);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(81, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Step Validation:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(111, 6);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 20);
            this.textBoxName.TabIndex = 0;
            this.toolTipStepDefinition.SetToolTip(this.textBoxName, "Syno Property: step_title [Optional]\r\nDescribes the title of the current step per" +
        "formed in the wizard.");
            this.textBoxName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxName_Validating);
            this.textBoxName.Validated += new System.EventHandler(this.textBoxName_Validated);
            // 
            // labelValidate
            // 
            this.labelValidate.AutoSize = true;
            this.labelValidate.Location = new System.Drawing.Point(14, 9);
            this.labelValidate.Name = "labelValidate";
            this.labelValidate.Size = new System.Drawing.Size(93, 13);
            this.labelValidate.TabIndex = 0;
            this.labelValidate.Text = "Name of the Step:";
            // 
            // checkBoxValidate
            // 
            this.checkBoxValidate.AutoSize = true;
            this.checkBoxValidate.Location = new System.Drawing.Point(111, 42);
            this.checkBoxValidate.Name = "checkBoxValidate";
            this.checkBoxValidate.Size = new System.Drawing.Size(189, 17);
            this.checkBoxValidate.TabIndex = 1;
            this.checkBoxValidate.Text = "[All items must be valid to proceed]";
            this.toolTipStepDefinition.SetToolTip(this.checkBoxValidate, resources.GetString("checkBoxValidate.ToolTip"));
            this.checkBoxValidate.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(12, 150);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(217, 150);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Step Activation:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Step Deactivation:";
            // 
            // textBoxActivation
            // 
            this.textBoxActivation.Location = new System.Drawing.Point(111, 72);
            this.textBoxActivation.Name = "textBoxActivation";
            this.textBoxActivation.ReadOnly = true;
            this.textBoxActivation.Size = new System.Drawing.Size(181, 20);
            this.textBoxActivation.TabIndex = 2;
            this.textBoxActivation.Text = "Not Yet Supported";
            this.toolTipStepDefinition.SetToolTip(this.textBoxActivation, "Syno Property: activate\r\nJSON-style string to describe a function which is run af" +
        "ter the step of the wizard has been visually activated.");
            // 
            // textBoxDeactivation
            // 
            this.textBoxDeactivation.Location = new System.Drawing.Point(111, 105);
            this.textBoxDeactivation.Name = "textBoxDeactivation";
            this.textBoxDeactivation.ReadOnly = true;
            this.textBoxDeactivation.Size = new System.Drawing.Size(181, 20);
            this.textBoxDeactivation.TabIndex = 3;
            this.textBoxDeactivation.Text = "Not Yet Supported";
            this.toolTipStepDefinition.SetToolTip(this.textBoxDeactivation, "Syno Property: deactivate\r\nJSON-style string to describe a function which is run " +
        "after the step of the wizard has been visually deactivated.");
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // StepDefinition
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(300, 181);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxDeactivation);
            this.Controls.Add(this.textBoxActivation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.checkBoxValidate);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelValidate);
            this.Controls.Add(this.labelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StepDefinition";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Step Definition";
            this.Load += new System.EventHandler(this.StepDefinition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelValidate;
        private System.Windows.Forms.CheckBox checkBoxValidate;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxActivation;
        private System.Windows.Forms.TextBox textBoxDeactivation;
        private System.Windows.Forms.ToolTip toolTipStepDefinition;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
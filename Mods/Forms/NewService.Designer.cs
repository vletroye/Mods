namespace BeatificaBytes.Synology.Mods.Forms
{
    partial class NewService
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
            this.radioButtonUrl = new System.Windows.Forms.RadioButton();
            this.radioButtonWeb = new System.Windows.Forms.RadioButton();
            this.radioButtonScript = new System.Windows.Forms.RadioButton();
            this.groupBoxTips = new System.Windows.Forms.GroupBox();
            this.labelToolTip = new System.Windows.Forms.Label();
            this.toolTipNewService = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxService = new System.Windows.Forms.GroupBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxTips.SuspendLayout();
            this.groupBoxService.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonUrl
            // 
            this.radioButtonUrl.AutoSize = true;
            this.radioButtonUrl.Location = new System.Drawing.Point(6, 13);
            this.radioButtonUrl.Name = "radioButtonUrl";
            this.radioButtonUrl.Size = new System.Drawing.Size(69, 17);
            this.radioButtonUrl.TabIndex = 0;
            this.radioButtonUrl.TabStop = true;
            this.radioButtonUrl.Text = "Add URL";
            this.toolTipNewService.SetToolTip(this.radioButtonUrl, "Used to add a shorcut in the DSM menu to open an URL. It can be a website on Inte" +
        "rnet or a website hosted on your Synology.");
            this.radioButtonUrl.UseVisualStyleBackColor = true;
            // 
            // radioButtonWeb
            // 
            this.radioButtonWeb.AutoSize = true;
            this.radioButtonWeb.Location = new System.Drawing.Point(6, 59);
            this.radioButtonWeb.Name = "radioButtonWeb";
            this.radioButtonWeb.Size = new System.Drawing.Size(92, 17);
            this.radioButtonWeb.TabIndex = 3;
            this.radioButtonWeb.TabStop = true;
            this.radioButtonWeb.Text = "Add Web App";
            this.toolTipNewService.SetToolTip(this.radioButtonWeb, "Use to deploy a website on the Synology and open it from the DSM menu.");
            this.radioButtonWeb.UseVisualStyleBackColor = true;
            // 
            // radioButtonScript
            // 
            this.radioButtonScript.AutoSize = true;
            this.radioButtonScript.Location = new System.Drawing.Point(6, 36);
            this.radioButtonScript.Name = "radioButtonScript";
            this.radioButtonScript.Size = new System.Drawing.Size(74, 17);
            this.radioButtonScript.TabIndex = 4;
            this.radioButtonScript.TabStop = true;
            this.radioButtonScript.Text = "Add Script";
            this.toolTipNewService.SetToolTip(this.radioButtonScript, "Used to run a Shell Script. The output of the Script is displayed in a DSM window" +
        ".");
            this.radioButtonScript.UseVisualStyleBackColor = true;
            // 
            // groupBoxTips
            // 
            this.groupBoxTips.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTips.Controls.Add(this.labelToolTip);
            this.groupBoxTips.Location = new System.Drawing.Point(12, 189);
            this.groupBoxTips.Name = "groupBoxTips";
            this.groupBoxTips.Size = new System.Drawing.Size(776, 85);
            this.groupBoxTips.TabIndex = 41;
            this.groupBoxTips.TabStop = false;
            this.groupBoxTips.Text = "TIPS";
            // 
            // labelToolTip
            // 
            this.labelToolTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelToolTip.Location = new System.Drawing.Point(6, 16);
            this.labelToolTip.Name = "labelToolTip";
            this.labelToolTip.Size = new System.Drawing.Size(764, 66);
            this.labelToolTip.TabIndex = 22;
            // 
            // groupBoxService
            // 
            this.groupBoxService.Controls.Add(this.radioButtonUrl);
            this.groupBoxService.Controls.Add(this.radioButtonWeb);
            this.groupBoxService.Controls.Add(this.radioButtonScript);
            this.groupBoxService.Location = new System.Drawing.Point(12, 12);
            this.groupBoxService.Name = "groupBoxService";
            this.groupBoxService.Size = new System.Drawing.Size(106, 86);
            this.groupBoxService.TabIndex = 42;
            this.groupBoxService.TabStop = false;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(12, 151);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 43;
            this.buttonOk.Text = "&Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(713, 151);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 44;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // NewService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 286);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBoxService);
            this.Controls.Add(this.groupBoxTips);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewService";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add a New Application";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.NewService_HelpButtonClicked);
            this.groupBoxTips.ResumeLayout(false);
            this.groupBoxService.ResumeLayout(false);
            this.groupBoxService.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonUrl;
        private System.Windows.Forms.RadioButton radioButtonWeb;
        private System.Windows.Forms.RadioButton radioButtonScript;
        private System.Windows.Forms.GroupBox groupBoxTips;
        private System.Windows.Forms.Label labelToolTip;
        private System.Windows.Forms.ToolTip toolTipNewService;
        private System.Windows.Forms.GroupBox groupBoxService;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}
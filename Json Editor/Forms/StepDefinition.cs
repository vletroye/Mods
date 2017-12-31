using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZTn.Json.Editor.Forms
{
    public partial class StepDefinition : Form
    {
        private string stepTitle = null;
        private bool stepValidation = false;
        private string stepActivation = null;
        private string stepDeactivation = null;

        public StepDefinition()
        {
            InitializeComponent();
        }

        public string Title { get { return stepTitle; } set { stepTitle = value; } }
        public bool Validation { get { return stepValidation; } set { stepValidation = value; } }
        public string Activation { get { return stepActivation; } set { stepActivation = value; } }
        public string Deactivation { get { return stepDeactivation; } set { stepDeactivation = value; } }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            stepTitle = textBoxName.Text;
            stepValidation = checkBoxValidate.Checked;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            stepTitle = null;
            this.Close();
        }

        private void StepDefinition_Load(object sender, EventArgs e)
        {
            textBoxName.Text = stepTitle;
            buttonOk.Enabled = (!string.IsNullOrEmpty(stepTitle));

            checkBoxValidate.Checked = stepValidation;
            textBoxActivation.Text = stepActivation;
            textBoxDeactivation.Text = stepDeactivation;
        }

        private void textBoxName_Validating(object sender, CancelEventArgs e)
        {
            buttonOk.Enabled = true;

            var key = textBoxName.Text;
            if (string.IsNullOrEmpty(key))
            {
                errorProvider.SetError(textBoxName, "You may not use an empty Key.");
                textBoxName.Text = "Enter_A_Value";
                e.Cancel = true;
                buttonOk.Enabled = false;
            }
        }

        private void textBoxName_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxName, "");
            buttonOk.Enabled = true;
        }
    }
}

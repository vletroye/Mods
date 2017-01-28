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

        public string Title { get { return stepTitle; } }
        public bool Validation { get { return stepValidation; } }
        public string Activation { get { return stepActivation; } }
        public string Deactivation { get { return stepDeactivation; } }

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
    }
}

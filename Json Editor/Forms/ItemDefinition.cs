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
    public partial class ItemDefinition : Form
    {
        private string itemType = null;
        private string itemDescription = null;

        public ItemDefinition()
        {
            InitializeComponent();
        }

        public string Type { get { return itemType; } }
        public string Description { get { return itemDescription; } }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            itemType = comboBoxItemType.Text;
            itemDescription= textBoxDescription.Text;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            itemType = null;
            this.Close();
        }
    }
}

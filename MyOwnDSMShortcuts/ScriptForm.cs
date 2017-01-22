using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatificaBytes.Synology.Mods
{
    public partial class ScriptForm : Form
    {
        public ScriptForm()
        {
            InitializeComponent();
        }

        public string Script
        {
            get
            {
                var value = scintilla.Text;
                value.Replace("\r\n", "\n");
                return value;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    value = "#!/bin/sh\n";
                scintilla.Text = value;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            scintilla.Text = null;
            this.Close();
        }

        private void ScriptForm_Load(object sender, EventArgs e)
        {
            scintilla.ConfigurationManager.Language = "bash";
        }
    }
}

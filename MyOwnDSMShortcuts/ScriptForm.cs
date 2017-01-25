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
        public enum Lang
        {
            Php,
            Bash
        }

        private Lang lang;

        public ScriptForm(Lang lang)
        {
            InitializeComponent();
            this.lang = lang;
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
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ScriptForm_Load(object sender, EventArgs e)
        {
            switch (lang)
            {
                case Lang.Bash:
                    scintilla.ConfigurationManager.Language = "bash";
                    this.scintilla.Lexing.Lexer = ScintillaNET.Lexer.Bash;
                    this.scintilla.Lexing.LexerName = "bash";
                    break;
                case Lang.Php:
                    scintilla.ConfigurationManager.Language = "php";
                    this.scintilla.Lexing.Lexer = ScintillaNET.Lexer.Php;
                    this.scintilla.Lexing.LexerName = "php";
                    break;
            }

        }
    }
}

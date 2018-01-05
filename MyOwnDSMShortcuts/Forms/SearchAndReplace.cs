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
    public partial class SearchAndReplace : Form
    {
        public delegate void FindTextEvent(string text);
        public FindTextEvent FindText;

        public delegate void ReplaceTextEvent(string oldText, string newTextox, bool replaceAll);
        public ReplaceTextEvent ReplaceText;


        public SearchAndReplace()
        {
            InitializeComponent();
        }

        public void Init(string text)
        {
            if (!string.IsNullOrEmpty(text))
                textBoxFind.Text = text;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            var text = textBoxFind.Text;
            if (!string.IsNullOrEmpty(text) && FindText != null)
                FindText(text);
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            var oldText = textBoxFind.Text;
            var newText = textBoxReplace.Text;
            if (!string.IsNullOrEmpty(oldText) && ReplaceText != null)
                ReplaceText(oldText, newText, false);
        }

        private void buttonReplaceAll_Click(object sender, EventArgs e)
        {
            var oldText = textBoxFind.Text;
            var newText = textBoxReplace.Text;
            if (!string.IsNullOrEmpty(oldText) && ReplaceText != null)
                ReplaceText(oldText, newText, true);
        }
        

        private void SearchAndReplace_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}

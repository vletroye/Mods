using Newtonsoft.Json.Linq;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatificaBytes.Synology.Mods.Forms
{
    public partial class PreloadTexts : Form
    {
        IniParser _texts;

        public PreloadTexts(string stringsPath, List<string> preloadTexts)
        {
            InitializeComponent();

            editListViewPreloadTexts.Columns.Add(columnHeaderSection);
            editListViewPreloadTexts.Columns.Add(columnHeaderKey);

            List<List<String>> lines = new List<List<string>>();
            if (preloadTexts != null)
            {
                foreach (var preloadText in preloadTexts)
                {
                    var elements = preloadText.Split(':');
                    lines.Add(new List<String>() { elements[0], elements[1] });
                }
                editListViewPreloadTexts.Lines = lines;
            }

            if (File.Exists(stringsPath))
            {
                _texts = new IniParser(stringsPath);
                columnHeaderSection.Tag = _texts.EnumSections();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            editListViewPreloadTexts.AddNewItem();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            editListViewPreloadTexts.DeleteSelectedItem();
        }

        private void editListViewPreloadTexts_Field_Validated(object sender, EventArgs e)
        {
            var line = editListViewPreloadTexts.EditingLine;
            var column = editListViewPreloadTexts.EditingColumn;
            var section = sender as ComboBox;
            if (sender != null & column == 0)
            {
                var value = section.Text;
                columnHeaderKey.Tag = _texts.EnumSection(value);
            }
        }

        private void editListViewPreloadTexts_Field_Validating(object sender, CancelEventArgs e)
        {
            var combo = sender as ComboBox;
            if (combo != null && !string.IsNullOrEmpty(combo.Text))
            {
                if (!combo.Items.Contains(combo.Text))
                {
                    combo.Text = "";
                    e.Cancel = true;
                }
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
                DialogResult = DialogResult.OK;
        }

        public List<List<string>> strings
        {
            get
            {
                return editListViewPreloadTexts.Lines;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

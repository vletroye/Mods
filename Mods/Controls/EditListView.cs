using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BeatificaBytes.Synology.Mods.Controls
{
    public partial class EditListView : UserControl
    {
        private bool Resizing = false;

        private ListViewItem li;
        private int X = 0;
        private int Y = 0;
        private string subItemText;
        private int subItemSelected = 0;
        private TextBox textBoxEditor = new TextBox();
        private ComboBox comboBoxEditor = new ComboBox();
        private int _editingLine = -1;
        private int _editingcolumn = -1;

        private EventHandler field_Validated;
        private CancelEventHandler field_Validating;

        public event EventHandler Field_Validated
        {
            add
            {
                field_Validated += value;
            }
            remove
            {
                field_Validated -= value;
            }
        }
        public event CancelEventHandler Field_Validating
        {
            add
            {
                field_Validating += value;
            }
            remove
            {
                field_Validating -= value;
            }
        }

        public EditListView()
        {
            InitializeComponent();

            //Prepare the ComboBox for Editing
            comboBoxEditor.Size = new Size(0, 0);
            comboBoxEditor.Location = new Point(0, 0);
            listView.Controls.AddRange(new Control[] { this.comboBoxEditor });
            comboBoxEditor.SelectedIndexChanged += new EventHandler(this.CmbSelected);
            comboBoxEditor.LostFocus += new EventHandler(this.CmbFocusOver);
            comboBoxEditor.KeyPress += new KeyPressEventHandler(this.CmbKeyPress);
            comboBoxEditor.Validated += new EventHandler(this.OnField_Validated);
            comboBoxEditor.Validating += new CancelEventHandler(this.OnField_Validating);
            //comboBoxEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            comboBoxEditor.BackColor = Color.SkyBlue;
            comboBoxEditor.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEditor.Hide();
            //Add values dynamically in the Combo cmbBox.Items.Add ...

            //Prepare the TextBox for Editing
            textBoxEditor.Size = new Size(0, 0);
            textBoxEditor.Location = new Point(0, 0);
            listView.Controls.AddRange(new Control[] { this.textBoxEditor });
            textBoxEditor.KeyPress += new KeyPressEventHandler(this.EditOver);
            textBoxEditor.LostFocus += new EventHandler(this.FocusOver);
            textBoxEditor.Validated += new EventHandler(this.OnField_Validated);
            textBoxEditor.Validating += new CancelEventHandler(this.OnField_Validating);

            //textBoxEditor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            textBoxEditor.BackColor = Color.LightYellow;
            textBoxEditor.BorderStyle = BorderStyle.Fixed3D;
            textBoxEditor.Hide();
            textBoxEditor.Text = "";

            listView.MouseDown += new MouseEventHandler(this.EditListView_MouseDown);
            listView.DoubleClick += new EventHandler(this.EditListView_DoubleClick);
        }

        private void CmbKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 27)
            {
                comboBoxEditor.Hide();
            }
        }

        private void CmbSelected(object sender, EventArgs e)
        {
            int sel = comboBoxEditor.SelectedIndex;
            if (sel >= 0)
            {
                string itemSel = comboBoxEditor.Items[sel].ToString();
                li.SubItems[subItemSelected].Text = itemSel;
            }
        }

        private void CmbFocusOver(object sender, EventArgs e)
        {
            comboBoxEditor.Hide();
        }

        private void EditOver(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                li.SubItems[subItemSelected].Text = textBoxEditor.Text;
                textBoxEditor.Hide();
            }

            if (e.KeyChar == 27)
                textBoxEditor.Hide();
        }

        private void OnField_Validated(object sender, EventArgs e)
        {
            field_Validated?.Invoke(sender, e);
        }

        private void OnField_Validating(object sender, CancelEventArgs e)
        {
            field_Validating?.Invoke(sender, e);
        }

        private void FocusOver(object sender, System.EventArgs e)
        {
            li.SubItems[subItemSelected].Text = textBoxEditor.Text;
            textBoxEditor.Hide();
        }

        public void EditListView_DoubleClick(object sender, System.EventArgs e)
        {
            // Check the subitem clicked .
            int nStart = X;
            int spos = 0;
            int epos = listView.Columns[0].Width;
            for (int i = 0; i < listView.Columns.Count; i++)
            {
                if (nStart > spos && nStart < epos)
                {
                    subItemSelected = i;
                    break;
                }

                spos = epos;
                epos += listView.Columns[i].Width;
            }

            subItemText = li.SubItems[subItemSelected].Text;
            _editingLine = li.Index;
            _editingcolumn = subItemSelected;

            var colTag = listView.Columns[subItemSelected].Tag;
            if (colTag is List<String>)
            {
                var list = colTag as List<String>;
                comboBoxEditor.Items.Clear();
                comboBoxEditor.Items.AddRange(list.ToArray());
                Rectangle r = new Rectangle(spos, li.Bounds.Y, epos, li.Bounds.Bottom);
                comboBoxEditor.Size = new System.Drawing.Size(epos - spos, li.Bounds.Bottom - li.Bounds.Top - 2);
                comboBoxEditor.Location = new System.Drawing.Point(spos, li.Bounds.Y - 2);
                comboBoxEditor.Show();
                comboBoxEditor.Text = subItemText;
                comboBoxEditor.SelectAll();
                comboBoxEditor.Focus();
            }
            else
            {
                Rectangle r = new Rectangle(spos, li.Bounds.Y, epos, li.Bounds.Bottom);
                textBoxEditor.Size = new System.Drawing.Size(epos - spos, li.Bounds.Bottom - li.Bounds.Top - 2);
                textBoxEditor.Location = new System.Drawing.Point(spos, li.Bounds.Y - 2);
                textBoxEditor.Show();
                textBoxEditor.Visible = true;
                textBoxEditor.Text = subItemText;
                textBoxEditor.SelectAll();
                textBoxEditor.Focus();
            }
        }

        public void EditListView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            li = listView.GetItemAt(e.X, e.Y);
            X = e.X;
            Y = e.Y;
        }

        public ListView.ColumnHeaderCollection Columns
        {
            get { return listView.Columns; }
            //set { listView.Columns = value; }
        }

        public void DeleteSelectedItem()
        {
            foreach (ListViewItem eachItem in listView.SelectedItems)
            {
                listView.Items.Remove(eachItem);
            }
        }

        public void AddNewItem()
        {
            var items = new string[listView.Columns.Count];
            for (var i = 0; i < items.Length; i++)
            {
                items[i] = "<EDIT>";
            }

            var newItem = new ListViewItem(items);

            listView.Items.Add(newItem).Selected = true;
        }

        public List<List<string>> Lines
        {
            get
            {
                return listView.GetListViewData();
            }
            set
            {
                foreach (var line in value)
                {
                    if (line.Count == listView.Columns.Count)
                        listView.Items.Add(new ListViewItem(line.ToArray()));
                    else
                    {
                        MessageBox.Show(this, string.Format("Too many values ({0}) passed for this EditListView, which only has {1} columns", line.Count, listView.Columns.Count), "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    }
                }
            }
        }

        private void listView_SizeChanged(object sender, EventArgs e)
        {
            // Don't allow overlapping of SizeChanged calls
            if (!Resizing)
            {
                // Set the resizing flag
                Resizing = true;

                ListView listView = sender as ListView;
                if (listView != null)
                {
                    float totalColumnWidth = 0;

                    // Get the sum of all column tags
                    for (int i = 0; i < listView.Columns.Count; i++)
                        totalColumnWidth += 1;
                    //totalColumnWidth += Convert.ToInt32(listView.Columns[i].Tag);

                    // Calculate the percentage of space each column should 
                    // occupy in reference to the other columns and then set the 
                    // width of the column to that percentage of the visible space.
                    for (int i = 0; i < listView.Columns.Count; i++)
                    {
                        //float colPercentage = (Convert.ToInt32(listView.Columns[i].Tag) / totalColumnWidth);
                        float colPercentage = 1 / totalColumnWidth;
                        listView.Columns[i].Width = (int)(colPercentage * listView.ClientRectangle.Width);
                    }
                }
            }

            // Clear the resizing flag
            Resizing = false;
        }

        public int EditingLine
        {
            get { return _editingLine; }
        }
        public int EditingColumn
        {
            get { return _editingcolumn; }
        }

    }
}

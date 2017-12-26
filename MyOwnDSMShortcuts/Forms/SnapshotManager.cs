using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatificaBytes.Synology.Mods
{
    public partial class SnapshotManager : Form
    {
        private PictureBox selectedPicture;
        protected bool validData;
        string imageDragDropPath;
        string path;

        public SnapshotManager(string path)
        {
            InitializeComponent();
            this.path = path;
        }

        private void SnapshotManager_Load(object sender, EventArgs e)
        {
            foreach (string snapshot in Directory.GetFiles(path, "screen_*.png"))
            {
                AddPicture(snapshot);
            }
        }

        private void AddPicture(string snapshot)
        {
            var image = Helper.LoadImage(snapshot, 0, 0);
            AddPicture(image);
        }

        private void AddPicture(Image image)
        {
            var pb = new PictureBox();
            pb.BackColor = Color.Transparent;
            pb.Image = image;
            if (pb.Image != null)
            {
                pb.Size = new Size(250, 250);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                flowLayoutPanelSnapshot.Controls.Add(pb);
                pb.Click += new EventHandler(picture_click);
            }
        }

        private void picture_click(object sender, EventArgs e)
        {
            if (selectedPicture != null)
                selectedPicture.BorderStyle = BorderStyle.None;
            selectedPicture = (PictureBox)sender;
            selectedPicture.BorderStyle = BorderStyle.FixedSingle;

            EnableButtons();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeletePicture();
        }

        private void DeletePicture()
        {
            flowLayoutPanelSnapshot.Controls.Remove(selectedPicture);
            selectedPicture = null;

            EnableButtons();
        }

        private void flowLayoutPanelSnapshot_DragDrop(object sender, DragEventArgs e)
        {
            if (validData)
            {
                AddPicture(imageDragDropPath);
            }
        }

        private void flowLayoutPanelSnapshot_DragEnter(object sender, DragEventArgs e)
        {
            validData = Helper.GetDragDropFilename(out imageDragDropPath, e);
            if (validData)
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void EnableButtons()
        {
            buttonDelete.Enabled = (selectedPicture != null);
            buttonMoveLeft.Enabled = (selectedPicture != null) && flowLayoutPanelSnapshot.Controls.IndexOf(selectedPicture) > 0;
            buttonMoveRight.Enabled = (selectedPicture != null) && flowLayoutPanelSnapshot.Controls.IndexOf(selectedPicture) < flowLayoutPanelSnapshot.Controls.Count - 1;
        }

        private void buttonMoveLeft_Click(object sender, EventArgs e)
        {
            var index = flowLayoutPanelSnapshot.Controls.IndexOf(selectedPicture);
            flowLayoutPanelSnapshot.Controls.SetChildIndex(selectedPicture, index - 1);
            EnableButtons();
        }

        private void buttonMoveRight_Click(object sender, EventArgs e)
        {
            var index = flowLayoutPanelSnapshot.Controls.IndexOf(selectedPicture);
            flowLayoutPanelSnapshot.Controls.SetChildIndex(selectedPicture, index + 1);
            EnableButtons();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            foreach (string snapshot in Directory.GetFiles(path, "screen_*.png"))
            {
                File.Delete(snapshot);
            }

            var index = 1;
            string imagepath;
            foreach (PictureBox pb in flowLayoutPanelSnapshot.Controls)
            {
                Image image = pb.Image;
                imagepath = string.Format("{0}/screen_{1}.png", path, index);
                image.Save(imagepath, ImageFormat.Png);
                index++;
            }
        }

        private void SnapshotManager_KeyUp(object sender, KeyEventArgs e)
        {
            if (selectedPicture != null && e.KeyCode == Keys.Delete)
            {
                DeletePicture();
            }
        }

        private void SnapshotManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Control)
            {
                Image image = Helper.GetImageFromClipboard();
                if (image != null)
                {
                    AddPicture(image);
                }
            }
        }
    }
}

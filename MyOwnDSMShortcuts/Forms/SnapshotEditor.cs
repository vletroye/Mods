using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatificaBytes.Synology.Mods
{
    public partial class SnapshotEditor : Form
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11;

        private Image originalImage;
        private Rectangle selection;
        private double scale;
        private bool preview = false;
        private bool cropping = false;
        private bool selecting = false;
        private bool inPicture = false;
        private bool editing = false;

        public SnapshotEditor(Image image, bool preview = false)
        {
            InitializeComponent();

            this.preview = preview;

            if (image != null)
            {
                originalImage = image.Clone() as Image;
                scale = Helper.SetSizedImage(pictureBoxSnapshot, originalImage);
                DisplayOriginalSize();
                FitToImage();
            }

            if (preview)
            {
                checkBoxAutoResize.Visible = false;
                labelNewSize.Text = "Click 'OK' to confirm.";
                textBoxWidth.Visible = false;
                textBoxHeight.Visible = false;
                labelOriginalSize.Text = "Preview of the changes...";
                textBoxOriginalWidth.Visible = false;
                textBoxOriginalHeight.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
            }

        }

        private void DisplayOriginalSize()
        {
            editing = true;
            textBoxWidth.Text = originalImage.Width.ToString();
            textBoxHeight.Text = originalImage.Height.ToString();
            textBoxOriginalWidth.Text = originalImage.Width.ToString();
            textBoxOriginalHeight.Text = originalImage.Height.ToString();
            editing = false;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Starting point of the selection:
            if (e.Button == MouseButtons.Left && !preview && cropping)
            {
                selecting = true;
                selection = new Rectangle(new Point(e.X, e.Y), new Size());
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            // Update the actual size of the selection:
            if (selecting)
            {
                selection.Width = e.X - selection.X;
                selection.Height = e.Y - selection.Y;

                // Redraw the picturebox:
                pictureBoxSnapshot.Refresh();
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (selecting)
            {
                // Draw a rectangle displaying the current selection
                Pen pen = Pens.MediumVioletRed;
                var zone = selection;
                if (zone.Width < 0)
                    zone = new Rectangle(zone.X + zone.Width, zone.Y, Math.Abs(zone.Width), zone.Height);
                if (zone.Height < 0)
                    zone = new Rectangle(zone.X, zone.Y + zone.Height, zone.Width, Math.Abs(zone.Height));

                e.Graphics.DrawRectangle(pen, zone);
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && selecting && selection.Size != new Size())
            {
                pictureBoxSnapshot.Cursor = Cursors.Default;
                cropping = false;
                selecting = false;

                // Redraw the picturebox:
                pictureBoxSnapshot.Refresh();

                var zone = selection;
                if (zone.Width < 0)
                    zone = new Rectangle(zone.X + zone.Width, zone.Y, Math.Abs(zone.Width), zone.Height);
                if (zone.Height < 0)
                    zone = new Rectangle(zone.X, zone.Y + zone.Height, zone.Width, Math.Abs(zone.Height));

                zone = new Rectangle((int)((double)zone.X * scale) + 1, (int)((double)zone.Y * scale) + 1, (int)((double)zone.Width * scale), (int)((double)zone.Height * scale));

                // Create cropped image:
                Image img = pictureBoxSnapshot.Tag as Image;
                if (img != null)
                {
                    img = img.Crop(zone);

                    // preview to accept:
                    var preview = new SnapshotEditor(img, true);
                    var accept = preview.ShowDialog(this);
                    if (accept == DialogResult.OK)
                    {
                        // Fit image to the picturebox:
                        scale = Helper.SetSizedImage(pictureBoxSnapshot, img);
                        originalImage = img;
                        DisplayOriginalSize();
                    }
                }
            }
            else
                selecting = false;
        }

        internal Image GetImage()
        {
            return pictureBoxSnapshot.Tag as Image;
        }

        private void picture_MouseLeave(object sender, EventArgs e)
        {
            inPicture = false;
        }

        private void picture_MouseEnter(object sender, EventArgs e)
        {
            inPicture = true;
        }

        private void pictureBoxSnapshot_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                scale = Helper.SetSizedImage(pictureBoxSnapshot, pictureBoxSnapshot.Tag as Image);
                if (checkBoxAutoResize.Checked)
                {
                    textBoxWidth.Text = pictureBoxSnapshot.Image.Width.ToString();
                    textBoxHeight.Text = pictureBoxSnapshot.Image.Height.ToString();
                }
            }
        }

        private void checkBoxAutoResize_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoResize.Checked)
            {
                textBoxWidth.Text = pictureBoxSnapshot.Image.Width.ToString();
                textBoxHeight.Text = pictureBoxSnapshot.Image.Height.ToString();
            }
            else
            {
                var img = pictureBoxSnapshot.Tag as Image;
                if (img != null)
                {
                    textBoxWidth.Text = img.Width.ToString();
                    textBoxHeight.Text = img.Height.ToString();
                }
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void SnapshotEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && inPicture)
            {
                pictureBoxSnapshot.Cursor = Cursors.Cross;
                GraphicsUnit units = GraphicsUnit.Point;
                Cursor.Clip = pictureBoxSnapshot.RectangleToScreen(Rectangle.Round(pictureBoxSnapshot.Image.GetBounds(ref units)));
                cropping = true;

                pictureBoxSnapshot.Refresh();
            }
        }

        private void SnapshotEditor_KeyUp(object sender, KeyEventArgs e)
        {
            if (Cursor.Clip != Rectangle.Empty)
            {
                pictureBoxSnapshot.Cursor = Cursors.Default;
                Cursor.Clip = Rectangle.Empty;
                selecting = false;
                cropping = false;

                pictureBoxSnapshot.Refresh();
            }
        }

        private void FitToImage()
        {
            //SendMessage(this.Handle, WM_SETREDRAW, false, 0); // suspend drawing on this form
            var screen = Screen.FromControl(this);
            var screenBounds = Screen.FromControl(this).Bounds;
            using (var temp = new SnapshotEditor(null))
            {
                temp.Width = screenBounds.Width;
                temp.Height = screenBounds.Height;

                var img = pictureBoxSnapshot.Tag as Image;

                if (img != null)
                {
                    var width = temp.pictureBoxSnapshot.Width - img.Width;
                    var height = temp.pictureBoxSnapshot.Height - img.Height;
                    if (width > 0) this.Width = screenBounds.Width - width;
                    if (height > 0) this.Height = screenBounds.Height - height;
                    if (width > 0 || height > 0)
                    {
                        WindowState = FormWindowState.Normal;
                        var formRectangle = new Rectangle(this.Left, this.Top, this.Width, this.Height);

                        if (!screen.WorkingArea.Contains(formRectangle))
                        {
                            this.CenterToScreen();
                        }
                    }
                }
            }

            //SendMessage(this.Handle, WM_SETREDRAW, true, 0); // suspend drawing on this form
            //this.Refresh();
        }

        protected override void WndProc(ref Message m)
        {
            //base.wndproc(ref m); // call the base class to get the new width/height
            if (m.Msg == 0x0112) // wm_syscommand
            {
                var wparam = new IntPtr(m.WParam.ToInt32() & 0xfff0); //wparam & a mask to supprt maximizing by double clicking the titlebar

                // check your window state here
                if (wparam == new IntPtr(0xf030)) //sc_maximize is 0xf030 (from winuser.h)
                                                  //sc_restore is 0xf120, 
                                                  //sc_minimize is 0xf020
                {
                    // the window is being maximized
                    FitToImage();
                }
                else
                {
                    base.WndProc(ref m);
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void textBoxWidth_TextChanged(object sender, EventArgs e)
        {
            int width;
            if (!editing && int.TryParse(textBoxWidth.Text, out width) && width > 0)
            {
                editing = true;
                double ratio = (double)originalImage.Width / (double)width;
                int height = (int)(originalImage.Height / ratio);

                if (width > 1024) errorProvider1.SetError(textBoxWidth, "Max size: 1024x768");
                if (height > 768) errorProvider1.SetError(textBoxHeight, "Max size: 1024x768");
                if (width < 20) errorProvider1.SetError(textBoxWidth, "Min size: 20x20");
                if (height < 20) errorProvider1.SetError(textBoxHeight, "Min size: 20x20");

                textBoxHeight.Text = height.ToString();
                editing = false;
            }
        }

        private void textBoxHeight_TextChanged(object sender, EventArgs e)
        {
            int height;
            if (!editing && int.TryParse(textBoxHeight.Text, out height) && height > 0)
            {
                editing = true;
                double ratio = (double)originalImage.Height / (double)height;
                int width = (int)(originalImage.Width / ratio);

                if (width > 1024) errorProvider1.SetError(textBoxWidth, "Max size: 1024x768");
                if (height > 768) errorProvider1.SetError(textBoxHeight, "Max size: 1024x768");
                if (width < 20) errorProvider1.SetError(textBoxWidth, "Min size: 20x20");
                if (height < 20) errorProvider1.SetError(textBoxHeight, "Min size: 20x20");

                textBoxWidth.Text = width.ToString();
                editing = false;
            }
        }

        private void textBoxSize_Leave(object sender, EventArgs e)
        {
            ApplyResizing();
        }

        private void ApplyResizing()
        {
            int width;
            int.TryParse(textBoxWidth.Text, out width);
            int height;
            int.TryParse(textBoxHeight.Text, out height);

            if (textBoxWidth.Text != textBoxOriginalWidth.Text && height > 0 && width > 0)
            {
                var img = Helper.ResizeImage(originalImage, width, height);
                scale = Helper.SetSizedImage(pictureBoxSnapshot, img);
            }
        }

        private void textBoxWidth_Validating(object sender, CancelEventArgs e)
        {
            int width;
            int height;
            errorProvider1.SetError(textBoxWidth, "");
            errorProvider1.SetError(textBoxHeight, "");
            if (int.TryParse(textBoxWidth.Text, out width) && int.TryParse(textBoxHeight.Text, out height) && width > 0 && height > 0)
            {
                if (width > 1024) errorProvider1.SetError(textBoxWidth, "Max size: 1024x768");
                if (height > 768) errorProvider1.SetError(textBoxHeight, "Max size: 1024x768");
                if (width < 20) errorProvider1.SetError(textBoxWidth, "Min size: 20x20");
                if (height < 20) errorProvider1.SetError(textBoxHeight, "Min size: 20x20");
            }
        }        

        private void SnapshotEditor_Scroll(object sender, MouseEventArgs e)
        {
            if (!preview && !cropping)
            {
                var delta = Math.Sign(e.Delta) * 20;
                int width;
                if (int.TryParse(textBoxWidth.Text, out width))
                {
                    width = width + delta;

                    var previous = textBoxWidth.Text;
                    textBoxWidth.Text = width.ToString();
                    var widthError = errorProvider1.GetError(textBoxWidth) ?? "";
                    var heightError = errorProvider1.GetError(textBoxHeight) ?? "";
                    if (widthError.StartsWith("Min") && delta > 0) widthError = "";
                    if (heightError.StartsWith("Min") && delta > 0) heightError = "";
                    if (widthError.StartsWith("Max") && delta < 0) widthError = "";
                    if (heightError.StartsWith("Max") && delta < 0) heightError = "";

                    if ((string.IsNullOrEmpty(widthError) && string.IsNullOrEmpty(heightError)))
                        ApplyResizing();
                    else
                        textBoxWidth.Text = previous;
                }
                ValidateChildren();
            }
        }
    }
}

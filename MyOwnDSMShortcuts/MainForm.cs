using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatificaBytes.Synology.Mods
{
    public partial class MainForm : Form
    {
        public enum State
        {
            None,
            View,
            Edit,
            Add
        }

        const string CONFIGFILE = @"package\ui\config";

        string ResourcesRootPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources");
        string PackageRootPath = Properties.Settings.Default.PackageRoot;
        string WebSynology = Properties.Settings.Default.Synology;
        string WebProtocol = Properties.Settings.Default.Protocol;
        int WebPort = Properties.Settings.Default.Port;

        Dictionary<string, PictureBox> pictureBoxes = new Dictionary<string, PictureBox>();
        KeyValuePair<string, AppsData> current;
        State state = State.None;
        urls list = null;

        string imageDragDropPath;
        protected bool validData;

        public MainForm()
        {
            InitializeComponent();

            InitListView();
            GetPictureBoxes();

            list = LoadData();
            BindData(list);

            if (string.IsNullOrEmpty(PackageRootPath))
            {
                MessageBox.Show(this, "The destination path for your package does not exist anymore. Reconfigure it and possibly 'recover' your icons.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PackageRootPath = "";
            }
            else if (!Directory.Exists(PackageRootPath))
            {
                MessageBox.Show(this, "The destination path for your package does not exist anymore. Reconfigure it and possibly 'recover' your icons.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PackageRootPath = "";
            }

            Display(new KeyValuePair<string, AppsData>(null, null));
        }

        private void InitListView()
        {
            listViewUrls.View = View.Details;
            listViewUrls.GridLines = true;
            listViewUrls.FullRowSelect = true;
            listViewUrls.Columns.Add("Name", 200);
            listViewUrls.Columns.Add("Uri", 800);
            listViewUrls.Sorting = SortOrder.Ascending;
        }

        private void GetPictureBoxes()
        {
            foreach (var control in this.Controls)
            {
                var pictureBox = control as PictureBox;
                if (pictureBox != null && pictureBox.Tag != null)
                {
                    pictureBox.AllowDrop = true;

                    pictureBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragDrop);
                    pictureBox.DoubleClick += new System.EventHandler(this.pictureBox_DoubleClick);
                    pictureBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragEnter);

                    pictureBoxes.Add(pictureBox.Tag.ToString(), pictureBox);
                }
            }
        }

        private void BindData(urls list)
        {
            listViewUrls.Items.Clear();

            foreach (var url in list.url)
            {
                var uri = url.Value.url;
                if (uri.StartsWith("/"))
                    uri = string.Format("{0}://{1}:{2}{3}", url.Value.protocol, WebSynology, url.Value.port, uri);

                // Define the list items
                var lvi = new ListViewItem(url.Value.title);
                lvi.SubItems.Add(uri);
                lvi.Tag = url;

                // Add the list items to the ListView
                listViewUrls.Items.Add(lvi);
            }

            listViewUrls.Sort();
        }

        private urls LoadData()
        {
            urls list = null;

            var config = Path.Combine(PackageRootPath, CONFIGFILE);
            if (File.Exists(config))
            {
                var json = File.ReadAllText(config);
                list = JsonConvert.DeserializeObject<urls>(json, new KeyValuePairConverter());
            }

            if (list == null || list.url.Count == 0)
            {
                var json = Properties.Settings.Default.Packages;
                if (!string.IsNullOrEmpty(json))
                {
                    list = JsonConvert.DeserializeObject<urls>(json, new KeyValuePairConverter());
                }
                else
                {
                    list = new urls();
                }
            }
            return list;
        }

        private void buttonPackage_Click(object sender, EventArgs e)
        {
            var packCmd = Path.Combine(PackageRootPath, "Pack.cmd");
            if (File.Exists(packCmd))
            {
                using (new CWaitCursor())
                {
                    PersistUrlsConfig();

                    Process pack = new Process();
                    pack.StartInfo.FileName = packCmd;
                    pack.StartInfo.Arguments = "";
                    pack.StartInfo.WorkingDirectory = PackageRootPath;
                    pack.StartInfo.UseShellExecute = false;
                    pack.StartInfo.RedirectStandardOutput = true;
                    pack.StartInfo.CreateNoWindow = true;
                    pack.Start();
                    Console.WriteLine(pack.StandardOutput.ReadToEnd());
                    pack.WaitForExit();
                }
                var answer = MessageBox.Show(this, string.Format("Your Package 'Mods.spk' is ready in {0}.\nDo you want to open that folder now?", PackageRootPath), "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (answer == DialogResult.Yes)
                {
                    Process.Start(PackageRootPath);
                }
            }
            else
            {
                MessageBox.Show(this, "For some reason, required resource files are missing. You will have to reconfigure your destination path", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PersistUrlsConfig()
        {
            if (!string.IsNullOrEmpty(PackageRootPath))
            {
                var json = JsonConvert.SerializeObject(list, Formatting.Indented, new KeyValuePairConverter());
                Properties.Settings.Default.Packages = json;
                Properties.Settings.Default.Save();

                var config = Path.Combine(PackageRootPath, CONFIGFILE);
                File.WriteAllText(config, json);
            }
        }

        private void listViewUrls_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (state != State.Edit)
            {
                if (listViewUrls.SelectedItems.Count > 0)
                {
                    var url = (KeyValuePair<string, AppsData>)listViewUrls.SelectedItems[0].Tag;
                    state = State.View;
                    Display(url);
                }
                else
                {
                    state = State.None;
                    Display(new KeyValuePair<string, AppsData>(null, null));
                }
            }
        }

        private void Display(KeyValuePair<string, AppsData> url)
        {
            current = url;
            var show = url.Key != null;
            var descText = show ? url.Value.desc : "";
            var titleText = show ? url.Value.title : "";
            var urlText = show ? url.Value.url : "";
            var users = show ? url.Value.allUsers : false;

            if (show)
            {
                foreach (var size in pictureBoxes.Keys)
                {
                    var icons = string.Format(url.Value.icon, size).Split('/');
                    var picture = Path.Combine(PackageRootPath, @"package\ui", icons[0], icons[1]);

                    if (File.Exists(picture))
                    {
                        LoadPicture(picture, size);
                    }
                    else
                    {
                        LoadPicture(null, size);
                    }
                }
            }
            else
            {
                foreach (var pictureBox in pictureBoxes.Values)
                {
                    LoadPictureBox(pictureBox, null);
                }
            }

            textBoxDesc.Text = descText;
            textBoxTitle.Text = titleText;
            textBoxUrl.Text = urlText;
            checkBoxAllUsers.Checked = users;

            EnableDetail();
        }

        private void LoadPicture(string picture, string size)
        {
            if (picture == null)
            {
                var pictureBox = pictureBoxes[size];
                pictureBox.Image = null;
            }
            else if (File.Exists(picture))
            {
                var pictureBox = pictureBoxes[size];
                LoadPictureBox(pictureBox, picture);
            }
            else
            {
                MessageBox.Show(this, string.Format("Missing picture '{0}' ?!", picture));
            }
        }

        private void LoadPictureBox(PictureBox pictureBox, string picture)
        {
            if (string.IsNullOrEmpty(picture))
            {
                pictureBox.Image = null;
            }
            else
            {
                Image image;
                using (FileStream stream = new FileStream(picture, FileMode.Open, FileAccess.Read))
                {
                    image = Image.FromStream(stream);
                }

                var width = image.Width;
                var height = image.Height;
                var size = pictureBox.Tag.ToString();
                var picSize = int.Parse(size);

                if (width != height || height != picSize)
                {
                    if (DialogResult.OK == MessageBox.Show(this, string.Format("The selected picture is not {0}x{0} but {1}x{2}. If you continue, it will be resized.", size, width, height), "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
                    {
                        Bitmap copy = new Bitmap(picSize, picSize);
                        using (Graphics g = Graphics.FromImage(copy))
                        {
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.DrawImage(image, 0, 0, copy.Width, copy.Height);
                        }

                        image.Dispose();
                        image = null;
                        pictureBox.Image = copy;
                        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                else
                {
                    pictureBox.Image = image;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private void EnableDetail()
        {
            if (string.IsNullOrEmpty(PackageRootPath))
            {
                textBoxDesc.Enabled = false;
                textBoxTitle.Enabled = false;
                textBoxUrl.Enabled = false;
                checkBoxAllUsers.Enabled = false;
                listViewUrls.Enabled = !false;
                buttonAdd.Enabled = false;
                buttonEdit.Enabled = false;
                buttonSave.Enabled = false;
                buttonCancel.Enabled = false;
                buttonDelete.Enabled = false;
                buttonPackage.Enabled = false;
            }
            else
            {
                bool enable = (state != State.View && state != State.None);

                buttonPackage.Enabled = listViewUrls.Items.Count > 0 && !enable;

                textBoxDesc.Enabled = enable;
                textBoxTitle.Enabled = enable;
                textBoxUrl.Enabled = enable;
                checkBoxAllUsers.Enabled = enable;
                listViewUrls.Enabled = !enable;

                switch (state)
                {
                    case State.View:
                        buttonAdd.Enabled = true;
                        buttonEdit.Enabled = true;
                        buttonSave.Enabled = false;
                        buttonCancel.Enabled = false;
                        buttonDelete.Enabled = true;
                        foreach (var picturebox in pictureBoxes)
                        {
                            picturebox.Value.BackColor = this.BackColor;
                        }

                        break;
                    case State.None:
                        buttonAdd.Enabled = true;
                        buttonEdit.Enabled = false;
                        buttonSave.Enabled = false;
                        buttonCancel.Enabled = false;
                        buttonDelete.Enabled = false;
                        foreach (var picturebox in pictureBoxes)
                        {
                            picturebox.Value.BackColor = this.BackColor;
                        }

                        break;
                    case State.Add:
                    case State.Edit:
                        buttonAdd.Enabled = false;
                        buttonEdit.Enabled = false;
                        buttonSave.Enabled = true;
                        buttonCancel.Enabled = true;
                        buttonDelete.Enabled = false;
                        foreach (var picturebox in pictureBoxes)
                        {
                            picturebox.Value.BackColor = Color.White;
                        }

                        break;
                }
            }
        }

        private KeyValuePair<string, AppsData> GetDetail()
        {
            var allUsers = checkBoxAllUsers.Checked;
            var title = textBoxTitle.Text.Trim();
            var desc = textBoxDesc.Text.Trim();

            string protocol;
            int port;

            var url = textBoxUrl.Text.Trim();
            Uri uri;
            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri))
            {
                if (uri.IsAbsoluteUri)
                {
                    protocol = uri.Scheme;
                    port = uri.Port;
                }
                else
                {
                    protocol = WebProtocol;
                    port = WebPort;
                    url = uri.OriginalString;
                    if (!url.StartsWith("/"))
                        url = string.Format("/{0}", url);
                }
            }
            else
            {
                protocol = WebProtocol;
                port = WebPort;
                if (!url.StartsWith("/"))
                    url = string.Format("/{0}", url);
            }

            var key = string.Format("be.beatificabytes.{0}", title.Replace(" ", ""));

            var appsData = new AppsData()
            {
                allUsers = allUsers,
                title = title,
                desc = desc,
                protocol = protocol,
                url = url,
                port = port
            };

            appsData.icon = string.Format("images/{0}_{1}.png", title, "{0}");
            foreach (var pictureBox in pictureBoxes)
            {
                var picture = pictureBox.Value;
                var size = pictureBox.Key;
                var image = picture.Image;

                if (image != null)
                {
                    var icons = string.Format(appsData.icon, size).Split('/');
                    var path = Path.Combine(PackageRootPath, @"package\ui", icons[0], icons[1]);
                    if (File.Exists(path))
                        File.Delete(path);
                    image.Save(path, ImageFormat.Png);

                    path = Path.Combine(ResourcesRootPath, @"recovery", icons[1]);
                    if (!Directory.Exists(Path.GetDirectoryName(path)))
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                    if (File.Exists(path))
                        File.Delete(path);
                    image.Save(path, ImageFormat.Png);
                }
            }

            return new KeyValuePair<string, AppsData>(key, appsData);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            current = new KeyValuePair<string, AppsData>(Guid.NewGuid().ToString(), new AppsData());

            state = State.Add;
            Display(current);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            state = State.Edit;
            EnableDetail();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var candidate = GetDetail();
            if (Validate(candidate.Value))
            {
                if (current.Key != null)
                {
                    list.url.Remove(current.Key);
                }
                current = candidate;
                list.url.Add(current.Key, current.Value);
                BindData(list);

                DisplayCurrent();

                PersistUrlsConfig();
            }
        }

        private bool Validate(AppsData candidate)
        {
            var message = new StringBuilder();

            if (string.IsNullOrEmpty(candidate.title))
            {
                message.AppendLine("The title may not be empty.");
            }
            if (string.IsNullOrEmpty(candidate.url))
            {
                message.AppendLine("The Url may not be empty.");
            }

            var error = message.ToString();
            var valid = error.Length > 0;
            if (valid)
            {
                MessageBox.Show(this, error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return !valid;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (state == State.Edit)
                DisplayCurrent();
            else
                DisplayNone();
        }

        private void DisplayNone()
        {
            current = new KeyValuePair<string, AppsData>(null, null);

            state = State.None;
            Display(current);
        }

        private void DisplayCurrent()
        {
            if (current.Key == null)
                DisplayNone();
            else
            {
                state = State.View;
                Display(current);
                foreach (ListViewItem item in listViewUrls.Items)
                {
                    if (item.Tag.Equals(current))
                        item.Selected = true;
                }
                listViewUrls.Focus();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (current.Key != null)
            {
                var answer = MessageBox.Show(this, string.Format("Do you really want to delete the Url '{0}'?", current.Value.title), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (answer == DialogResult.Yes)
                {
                    list.url.Remove(current.Key);
                    BindData(list);

                    DisplayNone();

                    PersistUrlsConfig();
                }
            }
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (state == State.Add || state == State.Edit)
            {
                var picture = sender as PictureBox;
                var size = picture.Tag.ToString();

                openFileDialog4Mods.Title = string.Format("Pick a png of {0}x{0}", size);
                DialogResult result = openFileDialog4Mods.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    string file = openFileDialog4Mods.FileName;
                    LoadPicture(openFileDialog4Mods.FileName, size);

                    Properties.Settings.Default.SourceImages = Path.GetDirectoryName(file);
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void pictureBoxSettings_Click(object sender, EventArgs e)
        {
            folderBrowserDialog4Mods.Description = "Pick the folder where the package must be stored.";
            if (!string.IsNullOrEmpty(PackageRootPath))
                folderBrowserDialog4Mods.SelectedPath = PackageRootPath;

            DialogResult result = folderBrowserDialog4Mods.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                PackageRootPath = folderBrowserDialog4Mods.SelectedPath;

                if (Directory.GetFiles(PackageRootPath).Length > 0)
                {
                    MessageBox.Show(this, "The Folder where the package will be created must be empty.");
                }
                else
                {
                    InitialConfiguration();

                    Display(new KeyValuePair<string, AppsData>(null, null));
                }
            }
        }

        private void InitialConfiguration()
        {
            using (new CWaitCursor())
            {
                Properties.Settings.Default.PackageRoot = PackageRootPath;
                Properties.Settings.Default.Save();

                Process unzip = new Process();
                unzip.StartInfo.FileName = Path.Combine(ResourcesRootPath, "7z.exe");
                unzip.StartInfo.Arguments = string.Format("x {0} -o{1}", Path.Combine(ResourcesRootPath, "Package.zip"), PackageRootPath);
                unzip.StartInfo.UseShellExecute = false;
                unzip.StartInfo.RedirectStandardOutput = true;
                unzip.StartInfo.CreateNoWindow = true;
                unzip.Start();
                Console.WriteLine(unzip.StandardOutput.ReadToEnd());
                unzip.WaitForExit();

                File.Copy(Path.Combine(ResourcesRootPath, "7z.exe"), Path.Combine(PackageRootPath, "7z.exe"));
                File.Copy(Path.Combine(ResourcesRootPath, "7z.dll"), Path.Combine(PackageRootPath, "7z.dll"));
                File.Copy(Path.Combine(ResourcesRootPath, "Pack.cmd"), Path.Combine(PackageRootPath, "Pack.cmd"));
            }

            var recovery = Path.Combine(ResourcesRootPath, "recovery");
            if (Directory.Exists(recovery))
            {
                var images = Directory.GetFiles(recovery);
                if (images.Length > 0)
                {
                    var answer = MessageBox.Show(this, "Icons from a previous package are still available do you want to recover them?\nIf you answer 'No', they will be deleted.\nIf you 'Cancel', they will be kept in the recovery folder.", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    switch (answer)
                    {
                        case DialogResult.Yes:
                            var target = Path.Combine(PackageRootPath, @"package\ui\images");
                            foreach (var image in images)
                            {
                                var dest = Path.Combine(target, Path.GetFileName(image));
                                if (File.Exists(dest))
                                    File.Delete(dest);
                                File.Move(image, dest);
                            }
                            break;
                        case DialogResult.No:
                            Directory.Delete(recovery);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (state == State.Add || state == State.Edit)
            {
                string filename;
                validData = GetFilename(out filename, e);
                if (validData)
                {
                    imageDragDropPath = filename;
                    e.Effect = DragDropEffects.Copy;
                }
                else
                    e.Effect = DragDropEffects.None;
            }
            else
            {
                validData = false;
            }
        }

        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            var pictureBox = sender as PictureBox;

            if (validData)
            {
                LoadPictureBox(pictureBox, imageDragDropPath);
            }
        }

        private bool GetFilename(out string filename, DragEventArgs e)
        {
            bool ret = false;
            filename = String.Empty;
            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                Array data = ((IDataObject)e.Data).GetData("FileDrop") as Array;
                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is String))
                    {
                        filename = ((string[])data)[0];
                        string ext = Path.GetExtension(filename).ToLower();
                        if (ext == ".png")
                        {
                            ret = true;
                        }
                    }
                }
            }
            return ret;
        }

        private void listViewUrls_DoubleClick(object sender, EventArgs e)
        {
            if (listViewUrls.SelectedItems.Count == 1)
            {
                buttonEdit_Click(sender, e);
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            var answer = MessageBox.Show(this, "Do you really want to clean the destination folder?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (answer == DialogResult.Yes)
            {
                try
                {
                    DeleteDirectory(PackageRootPath);

                    InitialConfiguration();
                }
                catch
                {
                    MessageBox.Show(this, "The destination folder cannot be cleaned. The package file is possibly in use?!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public static void DeleteDirectory(string path)
        {
            foreach (string directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }

            try
            {
                Directory.Delete(path, true);
            }
            catch (IOException)
            {
                Directory.Delete(path, true);
            }
            catch (UnauthorizedAccessException)
            {
                Directory.Delete(path, true);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PersistUrlsConfig();
        }
    }
}

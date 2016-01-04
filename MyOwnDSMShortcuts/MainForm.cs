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
using System.Text.RegularExpressions;
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
        static Regex getPort = new Regex(@"^:(\d*).*$", RegexOptions.Compiled);
        static Regex cleanChar = new Regex(@"[^a-zA-Z0-9]", RegexOptions.Compiled);
        static Regex getVersion = new Regex(@"^\d*.\d*.\d*$", RegexOptions.Compiled);

        string ResourcesRootPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources");
        string PackageRootPath = Properties.Settings.Default.PackageRoot;
        string WebSynology = Properties.Settings.Default.Synology;
        string WebProtocol = Properties.Settings.Default.Protocol;
        int WebPort = Properties.Settings.Default.Port;

        Dictionary<string, PictureBox> pictureBoxes;
        Dictionary<string, string> info;

        KeyValuePair<string, AppsData> current;
        State state;
        Package list;

        string imageDragDropPath;
        protected bool validData;

        public MainForm()
        {
            InitializeComponent();

            InitListView();

            GetPictureBoxes();

            if (string.IsNullOrEmpty(PackageRootPath) || !Directory.Exists(PackageRootPath))
            {
                MessageBox.Show(this, "The destination path for your package does not exist anymore. Reconfigure it and possibly 'recover' your icons.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PackageRootPath = "";
            }
            else if (!File.Exists(Path.Combine(PackageRootPath, "INFO")))
            {
                MessageBox.Show(this, "The INFO file for your package does not exist anymore. Reset your package.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PackageRootPath = "";
            }
            else
            {
                InitData();
                BindData(list);
                DisplayCurrent();
                LoadPackageInfo();
            }

            //Display(new KeyValuePair<string, AppsData>(null, null));
        }

        private void InitData()
        {
            info = new Dictionary<string, string>();
            state = State.None;
            list = LoadData();
        }

        private void InitListView()
        {
            listViewUrls.View = View.Details;
            listViewUrls.GridLines = true;
            listViewUrls.FullRowSelect = true;
            listViewUrls.Columns.Add("Name", 200);
            listViewUrls.Columns.Add("Uri", 580);
            listViewUrls.Sorting = SortOrder.Ascending;
        }

        private void GetPictureBoxes()
        {
            pictureBoxes = new Dictionary<string, PictureBox>();

            foreach (var control in this.Controls)
            {
                var pictureBox = control as PictureBox;
                if (pictureBox != null && pictureBox.Tag != null && pictureBox.Tag.ToString().StartsWith("URL"))
                {
                    pictureBox.AllowDrop = true;

                    pictureBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragDrop);
                    pictureBox.DoubleClick += new System.EventHandler(this.pictureBox_DoubleClick);
                    pictureBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragEnter);

                    pictureBoxes.Add(pictureBox.Tag.ToString().Substring(3), pictureBox);
                }
            }

            pictureBoxPkg_72.AllowDrop = true;
            pictureBoxPkg_256.AllowDrop = true;
        }

        private void BindData(Package list)
        {
            listViewUrls.Items.Clear();

            foreach (var url in list.urls)
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

        private Package LoadData()
        {
            Package list = null;

            var config = Path.Combine(PackageRootPath, CONFIGFILE);
            if (File.Exists(config))
            {
                var json = File.ReadAllText(config);
                list = JsonConvert.DeserializeObject<Package>(json, new KeyValuePairConverter());
            }

            if (list == null || list.urls.Count == 0)
            {
                var json = Properties.Settings.Default.Packages;
                if (!string.IsNullOrEmpty(json))
                {
                    list = JsonConvert.DeserializeObject<Package>(json, new KeyValuePairConverter());
                }
                else
                {
                    list = new Package();
                }
            }
            return list;
        }

        private void buttonPackage_Click(object sender, EventArgs e)
        {
            //Collect Package Info
            foreach (var control in this.Controls)
            {
                var textBox = control as TextBox;
                if (textBox != null && textBox.Tag != null && textBox.Tag.ToString().StartsWith("PKG"))
                {
                    var keys = textBox.Tag.ToString().Split(';');
                    foreach (var key in keys)
                    {
                        var keyId = key.Substring(3);
                        info[keyId] = textBox.Text.Trim();
                    }
                }
            }

            var packCmd = Path.Combine(PackageRootPath, "Pack.cmd");
            if (File.Exists(packCmd))
            {
                //Delete existing package if any
                var dir = new DirectoryInfo(PackageRootPath);
                foreach (var file in dir.EnumerateFiles("*.spk"))
                {
                    file.Delete();
                }

                //Delete existing INFO file
                var infoName = Path.Combine(PackageRootPath, "INFO");
                if (File.Exists(infoName))
                    File.Delete(infoName);

                //Write the new INFO file
                using (StreamWriter outputFile = new StreamWriter(infoName))
                {
                    foreach (var element in info)
                    {
                        outputFile.WriteLine("{0}=\"{1}\"", element.Key, element.Value);
                    }
                }

                //Save Package's icons
                var imageName = Path.Combine(PackageRootPath, "PACKAGE_ICON.PNG");
                SaveImage(pictureBoxPkg_72, imageName);
                imageName = Path.Combine(PackageRootPath, "PACKAGE_ICON_256.PNG");
                SaveImage(pictureBoxPkg_256, imageName);

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

                //Rename the new Package
                var packName = Path.Combine(PackageRootPath, info["package"] + ".spk");
                File.Move(Path.Combine(PackageRootPath, "mods.spk"), packName);

                var answer = MessageBox.Show(this, string.Format("Your Package '{0}' is ready in {1}.\nDo you want to open that folder now?", info["package"], PackageRootPath), "Done", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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

        private void SaveImage(PictureBox pictureBox, string path)
        {
            var image = pictureBox.Image;
            if (File.Exists(path))
                File.Delete(path);
            image.Save(path, ImageFormat.Png);
        }

        private void PersistUrlsConfig()
        {
            if (!string.IsNullOrEmpty(PackageRootPath))
            {
                var json = JsonConvert.SerializeObject(list, Formatting.Indented, new KeyValuePairConverter());
                Properties.Settings.Default.Packages = json;
                Properties.Settings.Default.Save();

                var config = Path.Combine(PackageRootPath, CONFIGFILE);
                if (Directory.Exists(Path.GetDirectoryName(config)))
                {
                    File.WriteAllText(config, json);
                }
                else
                {
                    MessageBox.Show(this, "For some reason, required resource files are missing. You will have to reconfigure your destination path", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
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
            if (urlText != null && urlText.StartsWith("/") && url.Value.port != WebPort)
            {
                urlText = string.Format(":{0}{1}", url.Value.port, url.Value.url);
            }
            var users = show ? url.Value.allUsers : false;

            if (show)
            {
                foreach (var size in pictureBoxes.Keys)
                {
                    var picture = GetIconFullPath(url.Value.icon, size);

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

        private string GetIconFullPath(string icon, string size)
        {
            var icons = string.Format(icon, size).Split('/');
            var picture = Path.Combine(PackageRootPath, @"package\ui", icons[0], icons[1]);
            return picture;
        }

        private void LoadPicture(string picture, string size, bool forceResize = false)
        {
            if (picture == null)
            {
                var pictureBox = pictureBoxes[size];
                pictureBox.Image = null;
            }
            else if (File.Exists(picture))
            {
                var pictureBox = pictureBoxes[size];
                LoadPictureBox(pictureBox, picture, forceResize);
            }
            else
            {
                MessageBox.Show(this, string.Format("Missing picture '{0}' ?!", picture));
            }
        }

        private void DeletePictures(string icon)
        {
            foreach (var size in pictureBoxes.Keys)
            {
                var path = GetIconFullPath(icon, size);
                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        private void LoadPictureBox(PictureBox pictureBox, string picture, bool forceResize = false)
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
                var size = pictureBox.Tag.ToString().Substring(3);
                var picSize = int.Parse(size);

                if (width != height || height != picSize)
                {
                    if (forceResize || DialogResult.OK == MessageBox.Show(this, string.Format("The selected picture is not {0}x{0} but {1}x{2}. If you continue, it will be resized.", size, width, height), "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
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
            int port = WebPort;

            var url = textBoxUrl.Text.Trim();

            if (url.StartsWith(":"))
            {
                var portMatch = getPort.Match(url);
                if (portMatch.Success)
                {
                    var value = portMatch.Groups[1].Value;
                    url = url.Substring(value.Length + 1);
                    port = int.Parse(value);
                }
            }

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

            title = CleanUpText(title);
            appsData.icon = string.Format("images/{0}_{1}.png", title, "{0}");
            return new KeyValuePair<string, AppsData>(key, appsData);
        }

        private static string CleanUpText(string text)
        {
            var icon = cleanChar.Replace(text, "_");
            while (text != icon)
            {
                text = icon.Trim(new[] { '_' });
                icon = text.Replace("__", "_");
            }
            return text;
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
            textBoxTitle.Focus();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                var candidate = GetDetail();
                if (state == State.Edit)
                    candidate.Value.guid = current.Value.guid;

                if (Validate(candidate.Value))
                {
                    if (current.Key != null)
                    {
                        list.urls.Remove(current.Key);
                        DeletePictures(current.Value.icon);
                    }
                    current = candidate;
                    list.urls.Add(current.Key, current.Value);
                    BindData(list);
                    DisplayCurrent();
                    SavePictures(candidate.Value.icon);

                    PersistUrlsConfig();
                }
            }
        }

        private void SavePictures(string icon)
        {
            foreach (var pictureBox in pictureBoxes)
            {
                var picture = pictureBox.Value;
                var size = pictureBox.Key;
                var image = picture.Image;

                if (image != null)
                {
                    var path = GetIconFullPath(icon, size);
                    if (File.Exists(path))
                        File.Delete(path);
                    image.Save(path, ImageFormat.Png);

                    path = Path.Combine(ResourcesRootPath, @"recovery", Path.GetFileName(path));
                    if (!Directory.Exists(Path.GetDirectoryName(path)))
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                    if (File.Exists(path))
                        File.Delete(path);
                    image.Save(path, ImageFormat.Png);
                }
            }
        }

        private bool Validate(AppsData candidate)
        {
            var message = new StringBuilder();

            //if (string.IsNullOrEmpty(candidate.title))
            //{
            //    message.AppendLine("The title may not be empty.");
            //}
            //foreach (var url in list.url.Values)
            //{
            //    if (url.title.Equals(candidate.title, StringComparison.InvariantCultureIgnoreCase) && candidate.guid != url.guid)
            //    {
            //        message.AppendLine("The title must be unique in your package.");
            //        break;
            //    }
            //}

            //if (string.IsNullOrEmpty(candidate.url))
            //{
            //    message.AppendLine("The Url may not be empty.");
            //}

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
            if (state == State.Add)
                current = new KeyValuePair<string, AppsData>(null, null);

            DisplayCurrent();
        }

        private void DisplayNone()
        {
            current = new KeyValuePair<string, AppsData>(null, null);

            state = State.None;
            Display(current);
        }

        private void DisplayCurrent()
        {
            if (current.Key == null && listViewUrls.Items.Count > 0)
            {
                current = (KeyValuePair<string, AppsData>)listViewUrls.Items[0].Tag;
            }

            if (current.Key == null)
            {
                DisplayNone();
            }
            else
            {
                var currentGuid = current.Value.guid;
                state = State.View;
                Display(current);

                foreach (ListViewItem item in listViewUrls.Items)
                {
                    KeyValuePair<string, AppsData> tag = (KeyValuePair<string, AppsData>)item.Tag;
                    item.Selected = (tag.Value.guid == currentGuid);
                }
                listViewUrls.Focus();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (current.Key != null)
            {
                var answer = MessageBox.Show(this, string.Format("Do you really want to delete the Url '{0}' and related icons?", current.Value.title), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (answer == DialogResult.Yes)
                {
                    DeletePictures(list.urls[current.Key].icon);
                    list.urls.Remove(current.Key);

                    current = new KeyValuePair<string, AppsData>(null, null);
                    BindData(list);
                    DisplayCurrent();

                    PersistUrlsConfig();
                }
            }
        }

        private void pictureBoxPkg_DoubleClick(object sender, EventArgs e)
        {
            openFileDialog4Mods.Title = "Pick a png";
            DialogResult result = openFileDialog4Mods.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string file = openFileDialog4Mods.FileName;
                LoadPictureBox(pictureBoxPkg_72, file, true);
                LoadPictureBox(pictureBoxPkg_256, file, true);

                IncrementVersion();

                Properties.Settings.Default.SourceImages = Path.GetDirectoryName(file);
                Properties.Settings.Default.Save();
            }
        }

        private void IncrementVersion()
        {
            var version = textBoxVersion.Text;
            var versions = version.Split('.');
            var minor = int.Parse(versions[2]) + 1;
            textBoxVersion.Text = string.Format("{0}.{1}.{2}", versions[0], versions[1], minor);
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (state == State.Add || state == State.Edit)
            {
                var picture = sender as PictureBox;
                var size = picture.Tag.ToString().Substring(3);

                openFileDialog4Mods.Title = string.Format("Pick a png of {0}x{0}", size);
                DialogResult result = openFileDialog4Mods.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    string file = openFileDialog4Mods.FileName;
                    ChangePicture(file, size);

                    Properties.Settings.Default.SourceImages = Path.GetDirectoryName(file);
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void ChangePicturePackage(string picture)
        {
            LoadPictureBox(pictureBoxPkg_72, picture, true);
            LoadPictureBox(pictureBoxPkg_256, picture, true);
        }

        private void ChangePictureBox(PictureBox pictureBox, string picture)
        {
            var response = DialogResult.No;
            if (checkBoxSize.Checked)
                response = MessageBox.Show("Do you really want to use this image for all sizes?", "Please Confirm", MessageBoxButtons.YesNoCancel);

            if (response == DialogResult.Yes)
            {
                foreach (var boxSize in pictureBoxes.Keys)
                {
                    LoadPicture(picture, boxSize, true);
                }
            }
            else if (response == DialogResult.No)
            {
                LoadPictureBox(pictureBox, picture);
            }
        }

        private void ChangePicture(string filename, string size)
        {
            var response = DialogResult.No;
            if (checkBoxSize.Checked)
                response = MessageBox.Show("Do you really want to use this image for all sizes?", "Please Confirm", MessageBoxButtons.YesNoCancel);

            if (response == DialogResult.Yes)
            {
                foreach (var boxSize in pictureBoxes.Keys)
                {
                    LoadPicture(openFileDialog4Mods.FileName, boxSize, true);
                }
            }
            else if (response == DialogResult.No)
            {
                LoadPicture(openFileDialog4Mods.FileName, size);
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
                    InitData();
                    LoadPackageInfo();
                    Display(new KeyValuePair<string, AppsData>(null, null));
                }
            }
        }

        private void LoadPackageInfo()
        {
            var file = Path.Combine(PackageRootPath, "INFO");

            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                var key = line.Substring(0, line.IndexOf('='));
                var value = line.Substring(line.IndexOf('=') + 1);
                value = value.Trim(new char[] { '"' });
                info.Add(key, value);
            }

            foreach (var control in this.Controls)
            {
                var textBox = control as TextBox;
                if (textBox != null && textBox.Tag != null && textBox.Tag.ToString().StartsWith("PKG"))
                {
                    var keys = textBox.Tag.ToString().Split(';');
                    var key = keys[0].Substring(3);
                    textBox.Text = info[key];
                }
            }

            LoadPictureBox(pictureBoxPkg_72, Path.Combine(PackageRootPath, "PACKAGE_ICON.PNG"), true);
            LoadPictureBox(pictureBoxPkg_256, Path.Combine(PackageRootPath, "PACKAGE_ICON_256.PNG"), true);
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
                            DeleteDirectory(recovery);
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
                e.Effect = DragDropEffects.None;
            }
        }

        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            var pictureBox = sender as PictureBox;

            if (validData)
            {
                ChangePictureBox(pictureBox, imageDragDropPath);
            }
        }

        private void pictureBoxPkg_DragEnter(object sender, DragEventArgs e)
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

        private void pictureBoxPkg_DragDrop(object sender, DragEventArgs e)
        {
            if (validData)
            {
                ChangePicturePackage(imageDragDropPath);
                IncrementVersion();
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
            if (string.IsNullOrEmpty(PackageRootPath))
            {
                MessageBox.Show(this, "Please reconfigure the destination path of your package first", "Warning");
            }
            else
            {
                var answer = MessageBox.Show(this, "Do you really want to reset the complete Package to its defaults?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (answer == DialogResult.Yes)
                {
                    try
                    {
                        if (Directory.Exists(PackageRootPath))
                            DeleteDirectory(PackageRootPath);

                        InitialConfiguration();
                        InitData();
                        BindData(list);
                        DisplayCurrent();

                        LoadPackageInfo();

                        //Display(new KeyValuePair<string, AppsData>(null, null));
                    }
                    catch
                    {
                        MessageBox.Show(this, "The destination folder cannot be cleaned. The package file is possibly in use?!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
        private void textBoxPackage_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxPackage, ref e))
            {
                var name = textBoxPackage.Text;
                var cleaned = CleanUpText(textBoxPackage.Text).Replace(" ", "");
                if (name != cleaned)
                {
                    e.Cancel = true;
                    textBoxPackage.Select(0, textBoxPackage.Text.Length);
                    errorProvider.SetError(textBoxPackage, "The name of the package may not contain blanks or special characters.");
                }
            }
        }

        private void textBoxPackage_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxPackage, "");
        }
        private void textBoxDisplay_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxDisplay, ref e))
            {
                CheckDoubleQuotes(textBoxDisplay, ref e);
            }
        }
        private void textBoxDisplay_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxDisplay, "");
        }

        private void textBoxMaintainer_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxMaintainer, ref e))
            {
                CheckDoubleQuotes(textBoxMaintainer, ref e);
            }
        }

        private void textBoxMaintainer_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxMaintainer, "");
        }

        private void textBoxDescription_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxDescription, ref e))
            {
                CheckDoubleQuotes(textBoxDescription, ref e);
            }
        }

        private void textBoxDescription_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxDescription, "");
        }

        private void textBoxMaintainerUrl_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxMaintainerUrl, ref e))
            {
                CheckDoubleQuotes(textBoxMaintainerUrl, ref e);
            }
        }

        private void textBoxMaintainerUrl_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxMaintainerUrl, "");
        }

        private void textBoxVersion_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxVersion, ref e))
            {
                if (!getVersion.IsMatch(textBoxVersion.Text))
                {
                    e.Cancel = true;
                    textBoxVersion.Select(0, textBoxVersion.Text.Length);
                    errorProvider.SetError(textBoxVersion, "The format of a version must be like 0.0.0");
                }
            }
        }

        private void textBoxVersion_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxVersion, "");
        }

        private void textBoxDsmAppName_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxDsmAppName, ref e))
            {
                var name = textBoxPackage.Text.Replace(".", "");
                var cleaned = CleanUpText(textBoxPackage.Text).Replace(" ", "");
                if (name != cleaned)
                {
                    e.Cancel = true;
                    textBoxPackage.Select(0, textBoxPackage.Text.Length);
                    errorProvider.SetError(textBoxPackage, "The name of the package may not contain blanks or special characters.");
                }
            }
        }

        private void textBoxDsmAppName_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxDsmAppName, "");
        }

        private void textBoxTitle_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxTitle, ref e))
            {
                foreach (var url in list.urls.Values)
                {
                    if (url.title.Equals(textBoxTitle.Text, StringComparison.InvariantCultureIgnoreCase) && current.Value.guid != url.guid)
                    {
                        e.Cancel = true;
                        textBoxTitle.Select(0, textBoxTitle.Text.Length);
                        errorProvider.SetError(textBoxTitle, string.Format("This title is already used for another URL: {0}", url.url));
                        break;
                    }
                }
            }
        }

        private void textBoxTitle_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxTitle, "");
        }

        private void textBoxUrl_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckEmpty(textBoxUrl, ref e))
            {
            }
        }

        private void textBoxUrl_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxUrl, "");
        }

        private bool CheckEmpty(TextBox textBox, ref CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Enter_A_Value";
                e.Cancel = true;
                textBox.Select(0, textBox.Text.Length);
                errorProvider.SetError(textBox, "This field may not be empty.");
            }

            return e.Cancel;
        }

        private void CheckDoubleQuotes(TextBox textBox, ref CancelEventArgs e)
        {
            if (textBox.Text.Contains('"'))
            {
                e.Cancel = true;
                textBox.Select(0, textBox.Text.Length);
                errorProvider.SetError(textBox, "You may not use double quotes in this textbox.");
            }
        }
    }
}

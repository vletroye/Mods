using BeatificaBytes.Synology.Mods.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;
using Microsoft.VisualBasic.ApplicationServices;

namespace BeatificaBytes.Synology.Mods.Helpers
{
    internal class MainHelper
    {
        private MainForm _this;
        private readonly object _lock = new object();
        private List<String> _warnings = new List<string>();
        private UserPrincipal _user = null;

        internal MainHelper(MainForm main)
        {
            _this = main;
            Task.Factory.StartNew(() => LoadAsyncInfo());
        }

        internal void Close()
        {
            _this = null;
        }

        internal void LoadAsyncInfo()
        {
            var name = user.DisplayName;
        }

        internal List<String> Warnings { get { return _warnings; } }
        internal Image PicturePkg_256 { get; set; }
        internal Image PicturePkg_120 { get; set; }
        internal Image PicturePkg_72 { get; set; }
        internal bool WizardExist { get; set; }
        internal Package Package { get; set; }
        internal bool IsDSM7x { get; set; }
        internal bool Dirty { get; set; }
        internal UserPrincipal user
        {
            get
            {
                lock (_lock)
                {
                    if (_user == null) _user = UserPrincipal.Current;
                    return _user;
                }
            }
            set
            {
                _user = value;
            }
        }

        /// <summary>
        /// Create a new Package in the provided folder.
        /// If no folder is provided but a default Package Root folder is configured, a temporary folder is created there.
        /// If no folder is provided and not default Package Root folder is configured, the user is prompted to pick a folder.
        /// If the folder contains a package, the user is prompted to open this one.
        /// </summary>
        /// <param name="targetFolder"></param>
        /// <remarks>Any previously opened package must be closed before creating/loading a new one</remarks>
        internal bool OpenNewPackage(string targetFolder = null)
        {
            bool succeed = true;

            if (Package != null)
            {
                MessageBoxEx.Show(_this, "A Package is currently opened. Close this one before creating or opening a new one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                succeed = false;
            }
            else
            {
                DialogResult ready = DialogResult.Cancel;
                Warnings.Clear();

                if (string.IsNullOrWhiteSpace(targetFolder))
                {
                    if (Properties.Settings.Default.DefaultPackageRoot && Directory.Exists(Properties.Settings.Default.PackageRoot))
                    {
                        // Use a temporary folder in the "default Package Root Folder" defined within MODS' properties (don't use a GUID as name. This is used only for spk "imported", and trigger a "warning message")
                        targetFolder = Path.Combine(Properties.Settings.Default.PackageRoot, "NEW-" + Guid.NewGuid().ToString());
                        ready = DialogResult.No;
                    }
                    else
                    {
                        // Prompt the user to pick a target Folder
                        FolderBrowserDialog4Mods.Title = "Pick a folder where your package can be created";
                        if (string.IsNullOrEmpty(FolderBrowserDialog4Mods.InitialDirectory))
                            FolderBrowserDialog4Mods.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                        ready = PromptForPath(out targetFolder);
                    }
                }
                else
                {
                    // Check if the provided folder can be used (is empty or contains either a SPK alone or a deflated package)
                    ready = PackageHelper.IsPackageFolderReady(targetFolder);
                }

                if (ready == DialogResult.Abort)
                {
                    if (Warnings.Count > 0)
                    {
                        var message = Warnings.Aggregate((i, j) => i + "\r\n_____________________________________________________________\r\n\r\n" + j);
                        MessageBoxEx.Show(_this, "The Folder does not contain a valid package.\r\n\r\n" + message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                    succeed = false;
                }
                else if (ready == DialogResult.Yes)
                {
                    var answer = MessageBoxEx.Show(_this, "This folder already contains a Package. Do you want to load it?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (answer != DialogResult.Yes) answer = DialogResult.Cancel;
                }
                if (ready == DialogResult.No)
                {
                    // The folder is empty => a new package needs to be initialize
                    InitializeNewPackage(targetFolder);
                    DisplayPackage(Package);
                }
                else
                {
                    // The folder contains an existing package. Open .t
                    if ((ready == DialogResult.No || ready == DialogResult.Yes) && (!string.IsNullOrEmpty(targetFolder)))
                    {
                        var package = new Package(targetFolder, null);
                        DisplayPackage(package);
                        succeed = true;
                    }
                }
            }

            return succeed;
        }

        /// <summary>
        /// Open an existing Package. If no Package is provided, the user will be prompted to pick a folder containing a valid Package.
        /// If the provided or selected Folder contains a spk file and nothing else, the user will be prompted to deflate that spk.
        /// If the provided or selected folder is empty, an new package is created.
        /// If the provided package is a SPK, it will be imported
        /// </summary>
        /// <param name="targetPackage">A folder containing a package or a SPK to be imported</param>
        /// <param name="import">Import the spk file found in the selected or provided folder, without prompting the user, if this parameter is true and there is no other files next to it.</param>
        /// <returns>
        /// True if a package has been opened successfuly
        /// False otherwise
        /// </returns>
        /// <remarks>
        /// Any previously opened package must be closed before creating/loading a new one
        /// if import is null, does not check if the provided folder is valid. It is assumed to be prepared (via PreparePackageFolder)
        /// </remarks>
        internal bool OpenExistingPackage(string targetPackage = null, bool import = false)
        {
            bool succeed = true;

            if (Package != null)
            {
                MessageBoxEx.Show(_this, "A Package is currently opened. Close this one before opening another one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                succeed = false;
            }
            else
            {
                //TODO: check what is this usefull for ?!
                //previousReportUrl = "";

                DialogResult ready = DialogResult.Cancel;
                FileInfo spk = null;
                Warnings.Clear();

                if (string.IsNullOrWhiteSpace(targetPackage))
                {
                    FolderBrowserDialog4Mods.Title = "Pick a folder containing an existing Package or a spk file to deflated.";
                    if (Properties.Settings.Default.DefaultPackageRoot)
                        FolderBrowserDialog4Mods.InitialDirectory = Properties.Settings.Default.PackageRoot;
                    else
                        FolderBrowserDialog4Mods.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    ready = PromptForPath(out targetPackage);
                }

                if (string.IsNullOrWhiteSpace(targetPackage))
                {
                    succeed = false;
                }
                else
                {
                    spk = new FileInfo(targetPackage);
                    if (spk.Exists)
                    {
                        targetPackage = spk.DirectoryName;

                        if (spk.IsExtension("SPK"))
                            ready = import ? DialogResult.Yes : DialogResult.Abort;
                        if (ready != DialogResult.Yes)
                            spk = null;
                        else
                        {
                            // Check if the SPK is in its own folder (alone or deflated)
                            if (!PackageHelper.SeemsDeflated(new DirectoryInfo(targetPackage)))
                                targetPackage = null;
                        }
                    }
                    else
                    {
                        spk = null;
                        ready = PackageHelper.IsPackageFolderReady(targetPackage);
                    }
                }

                if (ready == DialogResult.Abort)
                {
                    if (Warnings.Count > 0)
                    {
                        var message = Warnings.Aggregate((i, j) => i + "\r\n_____________________________________________________________\r\n\r\n" + j);
                        MessageBoxEx.Show(_this, "The Folder does not contain a valid package.\r\n\r\n" + message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                    succeed = false;
                }
                else if (ready == DialogResult.Yes || ready == DialogResult.No)
                {
                    if (ready == DialogResult.No)
                    {
                        // The selected folder is empty => Create a new package
                        ready = MessageBoxEx.Show(_this, "The Folder is empty. Do you want to create a new package?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (ready == DialogResult.Yes)
                            InitializeNewPackage(targetPackage);
                        else
                            ready = DialogResult.Cancel;
                    }
                    else
                    {
                        // The selected folder contains an existing empty => open it
                        if (Properties.Settings.Default.DefaultPackageRoot && Directory.Exists(Properties.Settings.Default.PackageRoot))
                        {
                            if (!import && !targetPackage.StartsWith(Properties.Settings.Default.PackageRoot))
                                HelperNew.PublishWarning(string.Format("This package is not stored in the default Package Root Folder defined in your Parameters...\r\n\r\n[{0}]", targetPackage));
                        }

                        if (ready == DialogResult.Yes)
                        {
                            var targetSPK = spk != null ? spk.FullName : null;
                            var package = new Package(targetPackage, targetSPK, true);
                            DisplayPackage(package);

                            //Refresh the binairies
                            PackageHelper.CopyPackagingBinaries(targetPackage);
                        }
                    }
                }
            }

            return succeed;
        }


        /// <summary>
        /// Prompt the user to save pending changes if any and close the current package.
        /// </summary>
        /// <param name="trySavingPendingChange">Do not try to save any pending changes if this parameter is false.</param>
        /// <param name="forceSavingPendingChange">Pending changes are saved without prompting the user if this parameters is true.</param>
        /// <returns>
        /// Yes if the pending changes were succesfuly saved.
        /// No if the user discarded the pending changes, if there was no pending changes or if the user was not prompted.
        /// Cancel if the user cancelled the saving.
        /// Abort if anything went wrong or if pending changes were not valid and couldn't be saved.
        /// </returns>
        internal DialogResult CloseCurrentPackage(bool trySavingPendingChange = true, bool forceSavingPendingChange = false)
        {
            DialogResult closed = DialogResult.No;
            if (Package != null && Package.IsLoaded)
                try
                {
                    // Prompt the user to save changes if required
                    if (trySavingPendingChange) closed = SaveCurrentPackage(forceSavingPendingChange);

                    // Couldn't save some pending changes
                    if (closed == DialogResult.Abort)
                    {
                        closed = MessageBoxEx.Show(_this, "Pending changes couldn't be saved!\r\n(Check that all fields are valid)\r\n\r\nDo you want to close your package anyway?\r\nChanges will be lost!", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3);
                        if (closed != DialogResult.Yes)
                            // User doesn't want to close without saving remaining changes
                            closed = DialogResult.Cancel;
                        else
                            // User discards remaining changes
                            closed = DialogResult.No;
                    }

                    // No need to save or user saved/discarded explicitly possible changes
                    if (closed == DialogResult.Yes || closed == DialogResult.No)
                    {
                        // Rename the parent folder with the package display name (or the package name) if other changes had to be done
                        if (trySavingPendingChange) RenameCurrentPackage();

                        SavePackageSettings();
                        _this.PrepareRecentsMenu();

                        Package = null;
                        _this.Reset_UI();

                        closed = DialogResult.Yes;
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxEx.Show(_this, string.Format("A Fatal error occured while trying to close the current package: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    closed = DialogResult.Abort;
                }

            return closed;
        }


        /// <summary>
        /// Prompt the user to save the Package if flagged as dirty or if there are any pending changes.
        /// </summary>
        /// <param name="path">Target save location.</param>
        /// <param name="force">Pending changes are saved without prompting the user if this parameter is true.</param>
        /// <returns>
        /// Yes if the package is successfuly saved.
        /// No if the user discarded the pending changes or if there was no pending changes.
        /// Cancel if the user asked to cancel the operation.
        /// Abort if something went wrong or if the user might not save the package due to invalid data.
        /// </returns>
        internal DialogResult SaveCurrentPackage(bool force = false)
        {
            using (new CWaitCursor(_this.ToolTip, "PLEASE WAIT WHILE SAVING YOUR PACKAGE..."))
            {
                DialogResult saved = DialogResult.Yes;
                if (Package == null)
                {
                    // No package to be saved
                    saved = DialogResult.No;
                }
                else
                {
                    try
                    {
                        if (!_this.ValidateChildren())
                        {
                            // User may not save changes with Invalid data
                            saved = DialogResult.Abort;
                        }
                        else
                        {
                            if (force || Dirty || _this.CheckChanges(null))
                            {
                                // Prompt the user to save pending changes. He may answer Yes, No or Cancel
                                if (!force) saved = MessageBoxEx.Show(_this, "Do you want to save pending changes in your package?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                                if (saved == DialogResult.Yes)
                                {
                                    _this.SavePackageInfo();
                                    Package.INFO.Checksum = null;
                                    Package.Save();
                                    SavePackageSettings();
                                    _this.PrepareRecentsMenu();
                                    Dirty = false;
                                }
                            }
                            else
                            {
                                // Nothing needs to be saved
                                saved = DialogResult.No;
                            }
                        }
                    }
                    catch (Exception ex) { saved = DialogResult.Abort; }
                }
                return saved;
            }
        }


        /// <summary>
        /// Rename the Package folder into its display name or package name if it's not a temporary folder.
        /// </summary>
        /// <remarks>This doesn't rethow any exception if the renaming failed.</remarks>
        internal void RenameCurrentPackage()
        {
            try
            {
                var parent = Path.GetFileName(Package.Folder_Root);
                Guid tmp;
                var isTempFolder = Guid.TryParse(parent, out tmp);
                if (!isTempFolder)
                {
                    var newName = GetCurrentPackageFolderName();
                    if (!string.IsNullOrEmpty(newName) && !newName.Equals(parent, StringComparison.InvariantCultureIgnoreCase))
                    {
                        var RenamedPackageRootPath = Package.Folder_Root.Replace(parent, newName);
                        var increment = 0;
                        while (Directory.Exists(RenamedPackageRootPath))
                        {
                            increment++;
                            RenamedPackageRootPath = string.Format("{0} ({1})", Package.Folder_Root.Replace(parent, newName), increment);
                        }
                        RemoveRecentPathFromSettings(Package.Folder_Root);
                        // Moving Package results in Windows Explorer locking them => copy into a new folder and delete the old one
                        // Directory.Move(PackageRootPath, RenamedPackageRootPath);
                        var oldPackageRootPath = Package.Folder_Root;
                        if (Helper.CopyDirectory(Package.Folder_Root, RenamedPackageRootPath))
                        {
                            if (Directory.Exists(RenamedPackageRootPath) && RenamedPackageRootPath != oldPackageRootPath)
                            {
                                Package.Folder_Root = RenamedPackageRootPath;
                                Helper.DeleteDirectory(oldPackageRootPath);
                            }
                            else
                            {
                                // Something went wrong while copying the old folder into a renamed one ?! Reuse therefore the old one :(
                                Package.Folder_Root = oldPackageRootPath;
                            }
                        }
                        else
                        {
                            // Something wrong happened while renaming. Delete the target folder and keep the original one
                            if (Directory.Exists(RenamedPackageRootPath) && RenamedPackageRootPath != oldPackageRootPath)
                            {
                                Package.Folder_Root = oldPackageRootPath;
                                Helper.DeleteDirectory(RenamedPackageRootPath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        internal string GetCurrentPackageFolderName()
        {
            string name = "";

            if (!string.IsNullOrEmpty(Package.INFO.Displayname))
                name = (Package.INFO.Displayname);
            else if (!string.IsNullOrEmpty(Package.INFO.Package))
                name = Package.INFO.Package;

            if (!string.IsNullOrEmpty(name))
            {
                foreach (char c in Path.GetInvalidFileNameChars())
                {
                    name = name.Replace(c, '_');
                }
            }

            return name;
        }

        private void InitializeNewPackage(string path)
        {
            using (new CWaitCursor())
            {
                var answer = MessageBoxEx.Show(_this, "Do you target DSM 7.x (YES) or a previous version (NO)?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (answer != DialogResult.Cancel)
                {
                    Process unzip = new Process();
                    unzip.StartInfo.FileName = Path.Combine(Helper.ResourcesDirectory, "7z.exe");
                    unzip.StartInfo.Arguments = string.Format("x \"{0}\" -o\"{1}\"", Path.Combine(Helper.ResourcesDirectory, "Package.zip"), path);
                    unzip.StartInfo.UseShellExecute = false;
                    unzip.StartInfo.RedirectStandardOutput = true;
                    unzip.StartInfo.CreateNoWindow = true;
                    unzip.Start();
                    unzip.WaitForExit(10000);
                    if (unzip.StartInfo.RedirectStandardOutput) Console.WriteLine(unzip.StandardOutput.ReadToEnd());

                    PackageHelper.CopyPackagingBinaries(path);
                    Package = new Package(path, null);

                    if (answer == DialogResult.Yes)
                    {
                        Package.INFO.Os_min_ver = "7.0-40000";
                        Package.INFO.Firmware = null;
                        Package.INFO.InstallDepPackages = "";
                        Package.INFO.StartstopRestartServices = "nginx.service";

                        Package.Privilege = PackagePrivilege.Load(Package.Path_Privilege, @"{""defaults"":{""run-as"":""package""}}");
                        Package.Privilege.Save();
                    }
                    else if (answer == DialogResult.No)
                    {
                        Package.INFO.Os_min_ver = "6.1-14715";
                        Package.INFO.InstallDepPackages = "Init_3rdparty>=1.5";
                        Package.INFO.StartstopRestartServices = "apache-sys apache-web";
                    }
                }
            }
        }

        private void DisplayPackage(Package package)
        {
            Package = package;
            _this.DisplayPackage();
        }

        internal void ProcessPackageConfig()
        {
            string ui = Package.INFO.DsmUiDir;

            //If no UI directory is defined, search for the config file under the <SPK>\\package folder (there should be only one)
            if (String.IsNullOrWhiteSpace(ui))
            {
                var fileList = new DirectoryInfo(Package.Folder_Package).GetFiles("config", System.IO.SearchOption.AllDirectories);
                if (fileList.Length > 0)
                {
                    ui = fileList[0].FullName.Replace(Package.Folder_Package, "").Replace("config", "").Trim(new char[] { '\\' });
                    Package.INFO.DsmUiDir = ui;

                    if (fileList.Length > 1)
                        HelperNew.PublishWarning(string.Format("More than one config file has been found. Only '{0}' will be used", ui));
                }
            }

            if (!string.IsNullOrWhiteSpace(ui))
            {
                var configFile = Package.Config;
                if (configFile != null)
                    foreach (var item in configFile.items)
                    {
                        if (item.Value.itemType == AppDataType.Undefined)
                            if (item.Value.type == "legacy") item.Value.itemType = AppDataType.WebApp;
                        if (item.Value.url == null)
                        {
                            //if (!string.IsNullOrEmpty(item.Value.port))
                            item.Value.url = "/";
                        }
                        var webapp = item.Value.url;
                        if (webapp.StartsWith("/webman/3rdparty/"))
                        {
                            //item.Value.itemType = (int)AppDataType.WebApp;
                            webapp = webapp.Replace("/webman/3rdparty/", "");
                            if (!webapp.StartsWith(Package.INFO.Package))
                                HelperNew.PublishWarning(string.Format("Menu item '{0}' is not referencing an element of your package...", item.Value.title));
                            else
                            {
                                if (webapp.StartsWith(Package.INFO.Package))
                                {
                                    if (webapp.Length > Package.INFO.Package.Length)
                                        webapp = webapp.Remove(0, Package.INFO.Package.Length + 1);
                                    webapp = Path.Combine(Path.GetDirectoryName(configFile.Path), webapp);
                                }
                                if (!File.Exists(webapp))
                                    HelperNew.PublishWarning(string.Format("Menu item '{0}' is not located in the right subfolder. Check your package...", item.Value.title));
                            }
                        }
                    }

                if (File.Exists(configFile.Path.Replace("config", "index.conf")))
                {
                    HelperNew.PublishWarning("This package contains a file 'index.conf' whose creation and edition are not yet supported by Mods Packager.");
                }

            }
            if (!string.IsNullOrEmpty(Package.INFO.SingleApp))
            {
                if (Package.Config != null && Package.Config.items.Count == 1)
                {
                    var title = Package.Config.items.First().Value.title;
                    title = Helper.CleanUpText(title);

                    if (Package.Config.items.First().Value.url.Contains(string.Format("/{0}/", title)))
                    {
                        Package.INFO.SingleApp = "no";
                    }
                    else
                    {
                        Package.INFO.SingleApp = "yes";
                    }
                }
                else
                {
                    Package.INFO.SingleApp = "no";
                }
            }

            var pkg72 = new FileInfo(Path.Combine(Package.Folder_Root, "PACKAGE_ICON.PNG"));
            if (pkg72.Exists)
            {
                PicturePkg_72 = Helper.LoadImageFromFile(pkg72.FullName);
            }
            else
            {
                if (Package.INFO.ContainsKey("package_icon"))
                {
                    PicturePkg_72 = Helper.LoadImageFromBase64(Package.INFO["package_icon"]);
                }
                else
                {
                    PicturePkg_72 = null;
                }
            }
            var pkg120 = new FileInfo(Path.Combine(Package.Folder_Root, "PACKAGE_ICON_120.PNG"));
            if (pkg120.Exists)
            {
                PicturePkg_120 = Helper.LoadImageFromFile(pkg120.FullName);
            }
            else
            {
                if (Package.INFO.ContainsKey("package_icon_120"))
                {
                    PicturePkg_120 = Helper.LoadImageFromBase64(Package.INFO["package_icon_120"]);
                }
                else
                {
                    PicturePkg_120 = null;
                }
            }
            var pkg256 = new FileInfo(Path.Combine(Package.Folder_Root, "PACKAGE_ICON_256.PNG"));
            if (pkg256.Exists)
            {
                PicturePkg_256 = Helper.LoadImageFromFile(pkg256.FullName);
            }
            else
            {
                if (Package.INFO.ContainsKey("package_icon_256"))
                {
                    PicturePkg_256 = Helper.LoadImageFromBase64(Package.INFO["package_icon_256"]);
                }
                else
                {
                    PicturePkg_256 = null;
                }
            }

            //TODO: I removed this as don't see aymorethe purpose here
            //groupBoxItem.Enabled = false;
        }

        internal void ProcessPackageInfo()
        {
            if (Package != null && Package.IsLoaded)
            {
                //TODO: I removed this as don't see aymore the purpose here
                //groupBoxPackage.Enabled = false;
                //SavePackageSettings();

                var wizard = Package.Folder_WizardUI;
                WizardExist = Directory.Exists(wizard);
            }
            else
            {
                MessageBoxEx.Show(_this, "No valid Package loaded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            if (!String.IsNullOrWhiteSpace(Package.INFO.Displayname))
            {
                var folder = Path.GetFileName(Package.Folder_Root);
                var package = GetCurrentPackageFolderName();

                if (!package.Equals(folder, StringComparison.InvariantCultureIgnoreCase))
                {
                    HelperNew.PublishWarning(string.Format("The working folder '{0}' is not named like your package '{1}'. It will be renamed automatically once your close the package!", folder, package));
                }
            }

            // Check if DSM Min is 7.0-40000
            IsDSM7x = Helper.CheckDSMVersionMin(Package.INFO, 7, 0, 40000);

            Properties.Settings.Default.LastPackage = Package.Folder_Root;
            Properties.Settings.Default.Save();
        }
        #region Folder and File Selection
        private OpenFolderDialog SpkRepoBrowserDialog4Mods { get { return _this.SpkRepoBrowserDialog4Mods; } }
        private OpenFolderDialog FolderBrowserDialog4Mods { get { return _this.FolderBrowserDialog4Mods; } }
        private OpenFolderDialog WebpageBrowserDialog4Mods { get { return _this.WebpageBrowserDialog4Mods; } }

        /// <summary>
        /// Prompt the user to pick a folder.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>
        /// Yes if the selected folder contains a valid package.
        /// No if the selected folder is empty.
        /// Cancel if the user cancelled the request.
        ///</returns>
        private DialogResult PromptForPath(out string path)
        {
            DialogResult getPackage = DialogResult.Abort;

            if (!FolderBrowserDialog4Mods.ShowDialog())
            {
                path = null;
                getPackage = DialogResult.Cancel;
            }
            else
            {
                path = FolderBrowserDialog4Mods.FileName;
                getPackage = PackageHelper.IsPackageFolderReady(path);
            }

            return getPackage;
        }
        #endregion

        #region Mods Settings
        internal void SavePackageSettings()
        {
            if (Properties.Settings.Default.Recents != null)
            {
                RemoveRecentPathFromSettings(Package.Folder_Root);
                if (Properties.Settings.Default.Recents.Count >= 10)
                {
                    Properties.Settings.Default.Recents.RemoveAt(0);
                    Properties.Settings.Default.RecentsName.RemoveAt(0);
                }
            }
            else
            {
                Properties.Settings.Default.Recents = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.RecentsName = new System.Collections.Specialized.StringCollection();
            }

            Properties.Settings.Default.Recents.Add(Package.Folder_Root);
            if (!string.IsNullOrEmpty(Package.INFO.Displayname))
                Properties.Settings.Default.RecentsName.Add(Package.INFO.Displayname);
            else if (!string.IsNullOrEmpty(Package.INFO.Package))
                Properties.Settings.Default.RecentsName.Add(Package.INFO.Package);
            else
                Properties.Settings.Default.RecentsName.Add(Path.GetFileName(Package.Folder_Root));

            Properties.Settings.Default.LastPackage = Package.Folder_Root;
            Properties.Settings.Default.Save();

            _this.Reset_UI_menuReviewPendingChanges();
        }

        internal void RemoveRecentPathFromSettings(string path)
        {
            for (var item = 0; item < Properties.Settings.Default.Recents.Count; item++)
            {
                if (Properties.Settings.Default.Recents[item] == path)
                {
                    if (Properties.Settings.Default.Recents.Count > item)
                        Properties.Settings.Default.Recents.RemoveAt(item);
                    if (Properties.Settings.Default.RecentsName.Count > item)
                        Properties.Settings.Default.RecentsName.RemoveAt(item);
                }
            }
        }
        #endregion
    }
}

using BeatificaBytes.Synology.Mods.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatificaBytes.Synology.Mods.Data
{
    internal class PackageFolder : BasePackage
    {
        public PackageFolder(string packageFolder)
        {
            Folder_Root = packageFolder;
        }

        internal override string DsmUiDir
        {
            get
            {
                return Helper.GetUIDir(Path_Info);
            }
        }
    }

    internal static class PackageHelper
    {
        /// <summary>
        /// Check if a folder possibly contains a deflated package
        /// </summary>
        /// <param name="packageFolder">folder to be validated</param>
        /// <returns>
        /// True if the folde contains the basic items of a package: INFO file, package folder and scripts folder. Call IsValidDeflated to validate the full content.
        /// </returns>
        internal static bool SeemsDeflated(DirectoryInfo packageFolder)
        {
            var targetFolder = new PackageFolder(packageFolder.FullName);
            return File.Exists(targetFolder.Path_Info) &&
                Directory.Exists(targetFolder.Folder_Package) &&
                Directory.Exists(targetFolder.Folder_Scripts) &&
                Directory.GetFiles(targetFolder.Folder_Root, "*.spk").Count() <= 1;
        }

        /// <summary>
        /// Deflate a SPK if required
        /// </summary>
        /// <param name="forceDeflate">force the SPK to be deflated</param>
        /// <param name="targetFolder">Target folder where the SPK will be deflated. This folder will be created if required. If not specified, the folder of the SPK will be used as target.</param>
        /// <param name="spk">SPK to be deflated. It must exists or the target folder must contains one and only one SPK.</param>
        /// <returns>
        /// Yes if a package was deflated. 
        /// Cancel if the deflation was required but cancelled. 
        /// No if a Deflation was not done.
        /// If the spk is deflated into a temporary folder, this one is returned into targetFolder.
        /// </returns>
        internal static DialogResult DeflatePackageIfRequired(bool forceDeflate, ref DirectoryInfo targetFolder, FileInfo spk)
        {
            DialogResult answer = DialogResult.No;
            bool spkToBeDeflated = true;

            // If no SPK specified, check if the targetFolder contains a package alreadry deflated or a SPK to be deflated
            if ((spk == null || !spk.Exists) && targetFolder != null && targetFolder.Exists)
            {
                spkToBeDeflated = !PackageHelper.SeemsDeflated(targetFolder);

                // Look for a SPK of in the target folder
                if (spkToBeDeflated && targetFolder.GetFiles("*.spk").Length == 1)
                    spk = targetFolder.GetFiles("*.spk")[0];
            }
            else
            // There must be a SPK
            {
                // If no SPK to be tested, throw an exception
                if (spk == null)
                    throw new ArgumentNullException("Application Error: A SPK must be passed as input.");
                else if (!spk.Exists)
                    throw new ArgumentException(String.Format("Application Error: The SPK {0} does not exist.", spk.FullName));

                // If not target folder to deflate, use the folder of the SPK
                if (targetFolder == null || !targetFolder.Exists)
                    if (SeemsDeflated(spk.Directory))
                    {
                        targetFolder = spk.Directory;
                        spkToBeDeflated = false;
                    }
                    else
                    {
                        targetFolder = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));
                        spkToBeDeflated = true;
                    }
            }

            if (spkToBeDeflated)
            {
                //Check if the SPK is in the target directory
                if (!targetFolder.Exists)
                {
                    answer = DialogResult.Yes;
                    if (!forceDeflate)
                        answer = MessageBoxEx.Show(String.Format("Do you want to deflate the package {0} into the new folder {1} ?", spk.Name, targetFolder.FullName), "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    switch (answer)
                    {
                        case DialogResult.Yes:
                            targetFolder = Directory.CreateDirectory(targetFolder.FullName);
                            forceDeflate = true;
                            break;
                        case DialogResult.No:
                            targetFolder = spk.Directory;
                            spkToBeDeflated = !SeemsDeflated(targetFolder);
                            break;
                        default:
                            spkToBeDeflated = false;
                            break;
                    }
                }

                //Ask if the spk must be deflated
                if (spkToBeDeflated)
                {
                    answer = DialogResult.Yes;
                    if (!forceDeflate)
                        answer = MessageBoxEx.Show("Do you want to deflate the package {0} into its own folder {1} ?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    switch (answer)
                    {
                        case DialogResult.Yes:
                            if (targetFolder.FullName != spk.DirectoryName)
                            {
                                spk = spk.CopyTo(Path.Combine(targetFolder.FullName, spk.Name));
                            }
                            DeflatePackage(spk);
                            break;
                        case DialogResult.No:
                            break;
                        default:
                            break;
                    }
                }
            }

            return answer;
        }

        /// <summary>
        /// Check if a folder contains a SPK ready to be deflated (supported by MODS)
        /// </summary>
        /// <param name="packageFolder">Folder to be validated</param>
        /// <returns>
        /// Yes if the folder contains only one SPK
        /// No if the folder seems to contain a deflated SPK
        /// Abort otherwise
        /// </returns>
        internal static DialogResult IsValidSPK(DirectoryInfo packageFolder)
        {
            var valid = DialogResult.Abort;

            var content = packageFolder.GetContent();
            if (content.Count == 1 && new FileInfo(content[0]).IsExtension("SPK"))
                valid = DialogResult.Yes;
            else if (content.Count > 1)
            {
                valid = IsValidDeflated(packageFolder);
            }

            return valid;
        }

        /// <summary>
        /// Check if a folder contains a valid deflated package (supported by MODS)
        /// </summary>
        /// <param name="packageFolder">Folder to be validated</param>
        /// <returns>
        /// Yes if all the files are supported by MODS
        /// No if some files are not supported by MODS by won't block it
        /// Abort if the folder cannot be used by MODS
        /// </returns>
        internal static DialogResult IsValidDeflated(DirectoryInfo packageFolder)
        {
            var targetFolder = new PackageFolder(packageFolder.FullName);
            var valid = DialogResult.Yes;

            if (!SeemsDeflated(packageFolder))
            {
                HelperNew.PublishWarning("This Package does not seem to be valid. It is missing the basic INFO, package and scripts.");
                valid = DialogResult.Abort;
            }
            else
            {

                // Get All files and Subdirectoris in the packageFolder.
                // Each one that is "supported/valid" will be removed from that list.
                var content = packageFolder.GetContent();

                // Look for SPK in the packageFolder. There may be only one. Remove it from the list.
                var spkList = packageFolder.GetFiles("*.spk");
                if (spkList.Length > 1)
                {
                    HelperNew.PublishWarning("This Package contains several SPK");
                    valid = DialogResult.Abort;
                }
                else if (spkList.Length == 1)
                    content.Remove(spkList[0].FullName);

                // Ignore the folder Conf but display a warning as it's not yet fully supported.
                var conf = targetFolder.Folder_Conf;
                if (content.Contains(conf))
                {
                    content.Remove(conf);
                    content.AddRange(Directory.GetFileSystemEntries(conf).ToList());

                    //HelperNew.PublishWarning("This Package contains a 'conf' folder. So far, only the Port-Config worker, PKG_DEPS and PKG_CONX are fully supported. Other workers are not supported. Privileges are partially supported.");
                    //valid = DialogResult.No;
                }

                // Ignore all "supported/valid" files (remove them from the list).
                IgnoreValidPackageFiles(targetFolder, content);

                if (content.Count > 0)
                {
                    // Folder contains an existing "deflated" Package with some unknown files
                    HelperNew.PublishWarning("This Package contains unknown elements:"
                        + Environment.NewLine
                        + content.Aggregate((i, j) => i.Replace(targetFolder.Folder_Root, "")
                        + Environment.NewLine + j.Replace(targetFolder.Folder_Root, "")));
                    valid = DialogResult.Cancel;
                }
            }
            return valid;
        }

        internal static void IgnoreValidPackageFiles(PackageFolder targetFolder, List<string> content)
        {
            // Get the UI dir from the INFO file (the package is not yet loaded)
            var uiDir = targetFolder.DsmUiDir;
            if (!string.IsNullOrEmpty(uiDir)) content.Remove(targetFolder.Folder_UI);

            content.Remove(Path.Combine(targetFolder.Folder_Root, "Hash.exe"));

            content.Remove(Path.Combine(targetFolder.Folder_Root, "7z.dll"));
            content.Remove(Path.Combine(targetFolder.Folder_Root, "7z.exe"));
            content.Remove(Path.Combine(targetFolder.Folder_Root, "Pack.cmd"));
            content.Remove(Path.Combine(targetFolder.Folder_Root, "Unpack.cmd"));

            content.Remove(Path.Combine(targetFolder.Folder_Root, "INFO"));
            content.Remove(Path.Combine(targetFolder.Folder_Root, "PACKAGE_ICON.PNG"));
            content.Remove(Path.Combine(targetFolder.Folder_Root, "PACKAGE_ICON_120.PNG")); //?? Found in some packages
            content.Remove(Path.Combine(targetFolder.Folder_Root, "PACKAGE_ICON_256.PNG"));
            content.Remove(Path.Combine(targetFolder.Folder_Root, "package"));
            content.Remove(Path.Combine(targetFolder.Folder_Root, "scripts"));
            content.Remove(Path.Combine(targetFolder.Folder_Root, "WIZARD_UIFILES"));

            content.Remove(Path.Combine(targetFolder.Folder_Root, "LICENSE"));
            content.Remove(Path.Combine(targetFolder.Folder_Root, "CHANGELOG"));

            content.Remove(Path.Combine(targetFolder.Folder_Conf, "privilege"));
            content.Remove(Path.Combine(targetFolder.Folder_Conf, "resource"));

            // signature are ignored and deleted (they are invalid as soon as the package is modified)
            string syno_signature = Path.Combine(targetFolder.Folder_Root, "syno_signature.asc");
            if (content.Remove(syno_signature))
                File.Delete(syno_signature);

            var remaining = content.ToList();
            foreach (var other in remaining)
            {
                // Remove images and screenshots
                if (other.EndsWith(".png") && Path.GetFileNameWithoutExtension(other).StartsWith("screen_")) content.Remove(other);
            }
        }

        internal static bool DeflatePackage(FileInfo spk)
        {
            CopyPackagingBinaries(spk.DirectoryName);
            bool result = true;

            var unpackCmd = Path.Combine(spk.DirectoryName, "Unpack.cmd");
            if (File.Exists(unpackCmd))
            {
                //using (new CWaitCursor(labelToolTip, "PLEASE WAIT WHILE DEFLATING YOUR PACKAGE..."))
                using (new CWaitCursor())
                {
                    // Execute the script to generate the SPK
                    Process unpack = new Process();
                    unpack.StartInfo.FileName = unpackCmd;
                    unpack.StartInfo.Arguments = "";
                    unpack.StartInfo.WorkingDirectory = spk.DirectoryName;
                    unpack.StartInfo.UseShellExecute = true; // required to run as admin
                    unpack.StartInfo.RedirectStandardOutput = false; // may not read from standard output when run as admin
                    unpack.StartInfo.CreateNoWindow = true; // Does not work if UseShellExecute = true
                    unpack.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    unpack.StartInfo.Verb = "runas"; //Required to run as admin as some packages have "symlink" resulting in "ERROR: Can not create symbolic link : Access is denied."
                    unpack.Start();
                    unpack.WaitForExit(30000);
                    if (unpack.StartInfo.RedirectStandardOutput) Console.WriteLine(unpack.StandardOutput.ReadToEnd());
                    if (unpack.ExitCode == 2)
                    {
                        result = false;
                        MessageBoxEx.Show("Extraction of the package has failed. Possibly try to run Unpack.cmd as Administrator in your package folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }

                    //As the package has been run as admin, Users don't have full control access
                    Helper.GrantAccess(spk.DirectoryName);
                }
            }
            else
            {
                MessageBoxEx.Show("For some reason, required resource files are missing. Your package can't be extracted", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                result = false;
            }

            return result;
        }

        internal static void CopyPackagingBinaries(string path)
        {
            if (File.Exists(Path.Combine(path, "7z.exe")))
                File.Delete(Path.Combine(path, "7z.exe"));
            File.Copy(Path.Combine(Helper.ResourcesDirectory, "7z.exe"), Path.Combine(path, "7z.exe"));
            if (File.Exists(Path.Combine(path, "7z.dll")))
                File.Delete(Path.Combine(path, "7z.dll"));
            File.Copy(Path.Combine(Helper.ResourcesDirectory, "7z.dll"), Path.Combine(path, "7z.dll"));
            if (File.Exists(Path.Combine(path, "Pack.cmd")))
                File.Delete(Path.Combine(path, "Pack.cmd"));
            File.Copy(Path.Combine(Helper.ResourcesDirectory, "Pack.cmd"), Path.Combine(path, "Pack.cmd"));
            if (File.Exists(Path.Combine(path, "Unpack.cmd")))
                File.Delete(Path.Combine(path, "Unpack.cmd"));
            File.Copy(Path.Combine(Helper.ResourcesDirectory, "Unpack.cmd"), Path.Combine(path, "Unpack.cmd"));

            var binFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var hash = Path.Combine(binFolder, "Hash.exe");
            if (File.Exists(hash))
            {
                if (File.Exists(Path.Combine(path, "Hash.exe")))
                    File.Delete(Path.Combine(path, "Hash.exe"));
                File.Copy(hash, Path.Combine(path, "Hash.exe"));
            }
        }
        /// <summary>
        /// Prompt the user to pick a folder.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>
        /// Yes if the selected folder contains a valid package.
        /// No if the selected folder is empty.
        /// Cancel if the user cancelled the request.
        /// Abort is anything went wrong.
        ///</returns>

        /// <summary>
        /// Check if a Package folder is ready to be used (contains a SPK of a deflated SPK).
        /// </summary>
        /// <param name="targetFolder"></param>
        /// <returns>
        /// Yes if the folder contains a valid package.
        /// No if the folder is empty.
        /// Abort otherwise. Ex.: if the folder does not exist.
        /// </returns>
        internal static DialogResult IsPackageFolderReady(string targetFolder)
        {
            return PackageHelper.IsPackageFolderReady(new DirectoryInfo(targetFolder));
        }
        /// <summary>
        /// Check if a Package folder is ready to be used.
        /// </summary>
        /// <param name="targetFolder"></param>
        /// <returns>
        /// Yes if the folder contains a valid package (a SPK or a deflated SPK).
        /// No if the folder is empty.
        /// Abort otherwise. Ex.: if the folder does not exist.
        /// </returns>
        internal static DialogResult IsPackageFolderReady(DirectoryInfo targetFolder)
        {
            DialogResult result = DialogResult.Abort;

            if (!targetFolder.Exists)
            {
                result = DialogResult.Abort;
            }
            else
            {
                var content = targetFolder.GetContent();
                if (content.Count == 0)
                    // Use the provided folder which is empty
                    result = DialogResult.No;
                else
                {
                    // Check if the provided folder, which is not empty, contains a valid Package or an Spk to possibly be deflated
                    result = PackageHelper.IsValidDeflated(targetFolder);

                    // Accept folder with a valid package possibly next to some other files.
                    if (result == DialogResult.No) result = DialogResult.Yes;

                    if (result != DialogResult.Yes) result = PackageHelper.IsValidDeflated(targetFolder);
                }
            }

            return result;
        }
    }
}

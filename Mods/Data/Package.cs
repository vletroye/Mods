using System;
using System.IO;
using System.Windows.Forms;
using BeatificaBytes.Synology.Mods.Helpers;
using BeatificaBytes.Synology.Mods.Data;

namespace BeatificaBytes.Synology.Mods
{
    internal class Package : BasePackage
    {
        /// <summary>
        /// A Package Synology.
        /// </summary>
        /// <param name="targetFolder">The target directory where the deflated package is (to be) stored.</param>
        /// <param name="spk">The fullname of a spk, to be deflated.</param>
        /// <exception cref="ArgumentNullException">targetFolder and spk may not both be null or empty.</exception>
        public Package(string targetFolder, string spk, bool forceDeflate)
        {
            // First thing is to set the package is not yet loaded
            IsLoaded = false;

            if (string.IsNullOrWhiteSpace(targetFolder) && string.IsNullOrWhiteSpace(spk))
                throw new ArgumentNullException("Application Error: A path or a spk must be specified to instanciate a Package.");

            DirectoryInfo dirInfo = string.IsNullOrWhiteSpace(targetFolder) ? null : new DirectoryInfo(targetFolder);
            FileInfo fileInfo = string.IsNullOrWhiteSpace(spk) ? null : new FileInfo(spk);

            //if a file is passed as input, it must be the SPK to be loaded or deflated
            if (fileInfo != null && fileInfo.Exists && !fileInfo.IsExtension("SPK"))
            {
                MessageBoxEx.Show("The file to be opened is not an SPK.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsLoaded = false;
            }
            else
            {
                //Check if a package is to be deflated
                if (PackageHelper.DeflatePackageIfRequired(forceDeflate, ref dirInfo, fileInfo) == DialogResult.Cancel)
                    IsLoaded = false;
                else
                {
                    if (dirInfo != null)
                        IsLoaded = Load(dirInfo);
                    else
                    {
                        MessageBoxEx.Show("The package couldn't be opened for an unknown reason.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsLoaded = false;
                    }
                }
            }
        }

        public Package(string targetFolder, string spk) : this(targetFolder, spk, false) { }

        public bool IsLoaded { get; set; }

        /// <summary>
        /// Load the Package
        /// </summary>
        /// <param name="dirInfo">Directory where the package is deflated</param>
        /// <returns></returns>
        private bool Load(DirectoryInfo dirInfo)
        {
            bool loaded = true;
            if (dirInfo == null)
                throw new ArgumentNullException("Application Error: A directory with the package must be passed as input.");

            if (!dirInfo.Exists)
                throw new ArgumentException(String.Format("Application Error: The directoy {0} assumed to contain a package does not exist.", dirInfo.FullName));

            if (!PackageHelper.SeemsDeflated(dirInfo))
                throw new Exception(String.Format("Application Error: The directoy {0} does not contain the INFO of a deflated package.", dirInfo.FullName));

            Folder_Root = dirInfo.FullName;

            //Load INFO file in from <SPK_folder>//INFO
            INFO = PackageINFO.Load(Path_Info);

            //Load Config file from <SPK_folder>//package//ui//config
            Config = PackageConfig.Load(Path_Config);

            //Load resource file from <SPK_folder>//conf//resource
            Resource = PackageResource.Load(Path_Resource);

            //Load privilege file from <SPK_folder>//conf//privilege
            Privilege = PackagePrivilege.Load(Path_Privilege);

            // Check if everything is valid

            return loaded;
        }

        public bool Save()
        {
            if (Privilege != null) Privilege.Save();

            if (Resource != null) Resource.Save();

            if (Config != null) Config.Save();

            INFO.WriteInfoFile(Folder_Conf);

            return true;
        }

        public bool IsDmsUiDirDefined { get { return !string.IsNullOrWhiteSpace(INFO.DsmUiDir); } }

        #region Package Components
        public PackageINFO INFO { get; set; }

        public PackageConfig Config { get; set; }

        public PackageResource Resource { get; set; }

        public PackagePrivilege Privilege { get; set; }

        internal override string DsmUiDir
        {
            get
            {
                return INFO != null ? INFO.DsmUiDir : Helper.GetUIDir(Path_Info);
            }
        }
        #endregion
    }
}

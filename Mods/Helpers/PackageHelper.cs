using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatificaBytes.Synology.Mods.Helpers
{
    internal abstract class BasePackage
    {
        #region Standard SPK files et folders
        protected  const string file_INFO = "INFO";
        protected const string folder_Conf = "conf";
        protected const string folder_Package = "package";
        protected const string folder_WizardUI = "WIZARD_UIFILES";
        protected const string folder_Scripts = "scripts";
        protected const string file_Conf_Resource = folder_Conf + "\\resource";
        protected const string file_Conf_Privilege = folder_Conf + "\\privilege";
        protected const string file_Conf_PKG_DEPS = folder_Conf + "\\PKG_DEPS";
        protected const string file_Conf_PKG_CONX = folder_Conf + "\\PKG_CONX";
        protected const string file_Config = folder_Package + "\\{0}\\config"; //{0} is to be replaced with the value of INFO's "dsmuidir"
        #endregion

        #region Standard SPK Files
        internal string Path_Resource
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Folder_Root))
                    throw new ArgumentNullException("Application error: Package folder is not set.");

                return Path.Combine(Folder_Root, file_Conf_Resource);
            }
        }
        internal string Path_Privilege
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Folder_Root))
                    throw new ArgumentNullException("Application error: Package folder is not set.");

                return Path.Combine(Folder_Root, file_Conf_Privilege);
            }
        }
        internal string Path_Pkg_Conx
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Folder_Root))
                    throw new ArgumentNullException("Application error: Package folder is not set.");

                return Path.Combine(Folder_Root, file_Conf_PKG_CONX);
            }
        }
        internal string Path_Pkg_Deps
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Folder_Root))
                    throw new ArgumentNullException("Application error: Package folder is not set.");

                return Path.Combine(Folder_Root, file_Conf_PKG_DEPS);
            }
        }
        internal string Path_Config
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Folder_Root))
                    throw new ArgumentNullException("Application error: Package folder is not set.");

                if (string.IsNullOrWhiteSpace(DsmUiDir))
                    throw new ArgumentNullException("Application error: INFO file has not been loaded.");

                return Path.Combine(Folder_Root, String.Format(file_Config, DsmUiDir));
            }
        }
        internal string Path_Info
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Folder_Root))
                    throw new ArgumentNullException("Application error: Package folder is not set.");

                return Path.Combine(Folder_Root, file_INFO);
            }
        }
        internal string Folder_Package
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Folder_Root))
                    throw new ArgumentNullException("Application error: Package folder is not set.");

                return Path.Combine(Folder_Root, folder_Package);
            }
        }
        internal string Folder_WizardUI
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Folder_Root))
                    throw new ArgumentNullException("Application error: Package folder is not set.");

                return Path.Combine(Folder_Root, folder_WizardUI);
            }
        }
        internal string Folder_Scripts
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Folder_Root))
                    throw new ArgumentNullException("Application error: Package folder is not set.");

                return Path.Combine(Folder_Root, folder_Scripts);
            }
        }
        internal string Folder_Conf
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Folder_Root))
                    throw new ArgumentNullException("Application error: Package folder is not set.");

                return Path.Combine(Folder_Root, folder_Conf);
            }
        }
        internal string Folder_UI
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Folder_Root))
                    throw new ArgumentNullException("Application error: Package folder is not set.");

                if (String.IsNullOrWhiteSpace(DsmUiDir))
                    throw new ArgumentNullException("Application error: UI folder is not defined in INFO file.");

                return Path.Combine(Folder_Root, folder_Package, DsmUiDir);
            }
        }
        internal string Path_Strings
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Folder_Root))
                    throw new ArgumentNullException("Application error: Package folder is not set.");

                return Path.Combine(Folder_UI, "texts", "enu", "strings");
            }
        }
        internal string Folder_Root { get; set; }

        internal abstract string DsmUiDir { get; }
        #endregion
    }
}

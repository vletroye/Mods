using System;
using System.Collections.Generic;
using System.IO;
using System.Dynamic;
using System.Linq;
using BeatificaBytes.Synology.Mods.Helpers;
using System.Reflection;
using System.Text;

namespace BeatificaBytes.Synology.Mods
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class PackageInfoProperty : Attribute
    {
        public PackageInfoProperty(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }

    [Serializable()]
    public class PackageINFO : Dictionary<String, String>
    {
        internal FileInfo FilePath { get; set; }

        internal static PackageINFO Load(string infoPath)
        {
            return new PackageINFO(infoPath);
        }

        public PackageINFO(string INFO_path)
        {
            FilePath = new FileInfo(INFO_path);

            if (!ReadInfoFile())
                InitDefaultInfo();
        }

        private void InitDefaultInfo()
        {
            //TODO: Code This method to set default value for an INFO file
        }

        #region INFO properties

        internal void SetMyValue(MethodBase method, string value)
        {
            var propertyName = method.Name.Substring(4);
            var property = typeof(PackageINFO).GetProperty(propertyName);
            PackageInfoProperty attr = property.GetCustomAttribute<PackageInfoProperty>();
            var tmp = (PackageInfoProperty)Attribute.GetCustomAttribute(method, typeof(PackageInfoProperty), false);
            propertyName = attr.Name;
            if (value == null)
                this.Remove(propertyName);
            else
                this[propertyName] = value;
        }

        internal string GetMyValue(MethodBase method)
        {
            string value;

            var propertyName = method.Name.Substring(4);
            var property = typeof(PackageINFO).GetProperty(propertyName);
            PackageInfoProperty attr = property.GetCustomAttribute<PackageInfoProperty>();
            var tmp = (PackageInfoProperty)Attribute.GetCustomAttribute(method, typeof(PackageInfoProperty), false);
            propertyName = attr.Name;

            if (this.ContainsKey(propertyName))
                value = this[propertyName];
            else
                value = null;

            return value;
        }

        [PackageInfoProperty("arch")]
        public string Arch
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }

        [PackageInfoProperty("beta")]
        public string Beta
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }

        [PackageInfoProperty("changelog")]
        public string ChangeLog
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("dsmuidir")]
        public string DsmUiDir
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("maintainer")]
        public string Maintainer
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("distributor")]
        public string Distributor
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("ctl_stop")]
        public string CtlStop
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("ctl_uninstall")]
        public string CtlUninstall
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("description")]
        public string Description
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("description_enu")]
        public string DescriptionEnu
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("displayname")]
        public string Displayname
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("displayname_enu")]
        public string DisplaynameEnu
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("Distributor_url")]
        public string DistributorUrl
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("dsmappname")]
        public string DsmAppName
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [Obsolete]
        [PackageInfoProperty("firmware")]
        public string Firmware
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("helpurl")]
        public string HelpUrl
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("install_dep_packages")]
        public string InstallDepPackages
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("install_reboot")]
        public string InstallReboot
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("maintainer_url")]
        public string MaintainerUrl
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("offline_install")]
        public string OfflineInstall
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("os_min_ver")]
        public string Os_min_ver
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("package")]
        public string Package
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("precheckstartstop")]
        public string PrecheckStartStop
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("reloadui")]
        public string ReloadUi
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("report_url")]
        public string ReportUrl
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("silent_install")]
        public string SilentInstal
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("silent_uninstall")]
        public string SilentUninstall
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("silent_upgrade")]
        public string SilentUpgrade
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("singleApp")]
        public string SingleApp
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("startable")]
        public string Startable
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("startstop_restart_services")]
        public string StartstopRestartServices
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("support_center")]
        public string SupportCenter
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("support_url")]
        public string SupportUrl
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("thirdparty")]
        public string ThirdParty
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("version")]
        public string Version
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("checksum")]
        public string Checksum
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("support_conf_folder")]
        public string SupportConfFolder
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("checkport")]
        public string CheckPort
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("adminurl")]
        public string AdminUrl
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("adminprotocol")]
        public string AdminProtocol
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        [PackageInfoProperty("adminport")]
        public string AdminPort
        {
            get { return GetMyValue(MethodBase.GetCurrentMethod()); }
            set { SetMyValue(MethodBase.GetCurrentMethod(), value); }
        }
        #endregion

        public bool IsDsm7x()
        {
            var os_min = this.ContainsKey("os_min_ver") ? this["os_min_ver"] : "";

            return Helper.CheckDSMVersionMin(os_min, 7, 0, 40000);
        }

        private bool ReadInfoFile()
        {
            var loaded = false;

            if (FilePath.Exists)
            {                
                this.Clear();
                var lines = File.ReadAllLines(FilePath.FullName);//, Encoding.Default);
                foreach (var line in lines)
                {
                    var lineText = line.Trim();
                    if (!string.IsNullOrEmpty(lineText))
                    {
                        if (lineText.StartsWith("#"))
                        {
                            //Comments will be lost as not supported by MODS
                        }
                        else if (lineText.Contains('='))
                        {

                            var key = lineText.Substring(0, lineText.IndexOf('='));
                            var value = lineText.Substring(lineText.IndexOf('=') + 1);
                            value = value.Trim(new char[] { '"' });
                            value = value.Replace("<br>", "\r\n");
                            if (key != "checksum")
                                this[key] = value;
                        }
                    }
                }
                loaded = true;

                if (Maintainer == "...")
                    Maintainer = Environment.UserName;
                if (Distributor == "...")
                    Distributor = Environment.UserName;
                if (DsmUiDir == null)
                {
                    DsmUiDir = "";
                    HelperNew.PublishWarning("This Package has no UI. Adding/Editing Items will be disabled.");
                }
                    
            }

            return loaded;
        }

        internal void WriteInfoFile(string confFolder)
        {
            // Required for DSM 4.2 ~ DSM 5.2
            SupportConfFolder = null;
            if (Directory.Exists(confFolder) && Directory.EnumerateFileSystemEntries(confFolder).Count() > 0)
            {
                if (!Helper.CheckDSMVersionMin(Os_min_ver, 6, 0, 0))
                    SupportConfFolder = "yes";
            }

            // Delete existing INFO file (try to send it to the RecycleBin
            if (FilePath.Exists)
                Helper.DeleteFile(FilePath.FullName);

            // Write the new INFO file
            //using (StreamWriter outputFile = new StreamWriter(FilePath.FullName, false, Encoding.GetEncoding(1252)))
            using (StreamWriter outputFile = new StreamWriter(FilePath.FullName))
            {
                foreach (var element in this)
                {
                    if (!string.IsNullOrEmpty(element.Value) && element.Value != "path")
                    {
                        var value = element.Value.Replace("\r\n", "<br>").Replace("\n", "<br>");
                        outputFile.WriteLine("{0}=\"{1}\"", element.Key, value);
                    }
                }
            }
        }
    }
}

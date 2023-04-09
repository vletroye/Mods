using BeatificaBytes.Synology.Mods.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using static ScintillaNET.Style;

// Created using https://json2csharp.com/
namespace BeatificaBytes.Synology.Mods
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class PackagePrivilege
    {
        internal FileInfo FilePath { get; set; }
        internal string Folder { get { return Path.GetDirectoryName(FilePath.FullName); } }

        internal static PackagePrivilege Load(string privilegePath, string jsonPrivilege)
        {
            PackagePrivilege obj = null;
            try
            {
                obj = JsonConvert.DeserializeObject<PackagePrivilege>(jsonPrivilege);
                obj.FilePath = new FileInfo(privilegePath);
            }
            catch (Exception ex)
            {
                //PublishWarning(string.Format("The resource file '{0}' is corrupted...", resourceFile));
            }
            return obj;
        }
        internal static PackagePrivilege Load(string privilegePath)
        {
            PackagePrivilege obj = null;
            if (File.Exists(privilegePath))
            {
                var json = File.ReadAllText(privilegePath);
                try
                {
                    var privilege = JsonConvert.DeserializeObject<JObject>(json);
                    obj = privilege.ToObject<PackagePrivilege>();
                    obj.FilePath = new FileInfo(privilegePath);
                }
                catch (Exception ex)
                {
                    //PublishWarning(string.Format("The resource file '{0}' is corrupted...", resourceFile));
                }
            }
            return obj;
        }

        internal void Save()
        {
            if (FilePath != null)
            {
                if (FilePath.Exists)
                    Helper.DeleteFile(FilePath.FullName);

                if (!Directory.Exists(Folder))
                    Directory.CreateDirectory(Folder);

                try
                {
                    var json = JsonConvert.SerializeObject(this, Formatting.Indented);
                    if (json.Count() > 0)
                        Helper.WriteAnsiFile(FilePath.FullName, json);
                }
                catch (Exception ex)
                {
                    //TODO: throw an exception
                    //PublishWarning(string.Format("The privilege file '{0}' can't be updated.", privilegeFile));
                }

                if (Directory.EnumerateFileSystemEntries(Folder).Count() == 0)
                    Directory.Delete(Folder);
            }
        }

        [JsonProperty("defaults")]
        public Defaults Defaults { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("groupname")]
        public string Groupname { get; set; }
        [JsonProperty("join-groupname", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string JoinGroupname { get; set; }
        [JsonProperty("ctrl-script", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<CtrlScript> CtrlScript { get; set; }
        [JsonProperty("tool", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Tool> Tool { get; set; }
        [JsonProperty("executable", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<Executable> Executable { get; set; }
    }

    public class Executable
    {
        public string relpath { get; set; }

        [JsonProperty("run-as")]
        public string RunAs { get; set; }
    }

    public class CtrlScript
    {
        public string action { get; set; }

        [JsonProperty("run-as")]
        public string RunAs { get; set; }
    }

    public class Defaults
    {
        [JsonProperty("run-as")]
        public string RunAs { get; set; }
    }
    public class Tool
    {
        public string relpath { get; set; }
        public string user { get; set; }
        public string group { get; set; }
        public string capabilities { get; set; }
        public string permission { get; set; }
    }
}

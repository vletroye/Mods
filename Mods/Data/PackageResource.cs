using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Created using https://json2csharp.com/
namespace BeatificaBytes.Synology.Mods
{
    // PackageResource  myDeserializedClass = JsonConvert.DeserializeObject<PackageResource>(myJsonResponse);

    public class PackageResource
    {
        internal FileInfo FilePath { get; set; }
        internal string Folder { get { return Path.GetDirectoryName(FilePath.FullName); } }

        internal static PackageResource Load(JObject resource)
        {
            return resource.ToObject<PackageResource>();
        }
        public static PackageResource Load(string resourcePath)
        {
            PackageResource obj = null;
            if (File.Exists(resourcePath))
            {
                var json = File.ReadAllText(resourcePath);
                try
                {
                    var resource = JsonConvert.DeserializeObject<JObject>(json);
                    obj = resource.ToObject<PackageResource>();
                    obj.FilePath = new FileInfo(resourcePath);
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

        [JsonProperty("webservice")]
        public WebService WebService { get; set; }
        [JsonProperty("usr-local-linker")]
        public UsrLocalLinker UsrLocalLinker { get; set; }
        [JsonProperty("port-config")]
        public PortConfig PortConfig { get; set; }
        [JsonProperty("syslog-config")]
        public SyslogConfig SyslogConfig { get; set; }
    }

    public class WebService
    {
        public List<Service> services { get; set; }
        [DefaultValue(null)]
        public List<Portal> portals { get; set; }
        public List<PkgDirPrepare> pkg_dir_prepare { get; set; }
    }
    public class Service
    {
        public string service { get; set; }
        public string display_name { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool support_alias { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool support_server { get; set; }
        public string type { get; set; }
        public string root { get; set; }
        [DefaultValue(null)]
        public string[] index { get; set; }
        [DefaultValue("")]
        public string icon { get; set; }
        public int backend { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool intercept_errors { get; set; }
        public Php php { get; set; }
        [DefaultValue(0)]
        public int connect_timeout { get; set; }
        [DefaultValue(0)]
        public int read_timeout { get; set; }
        [DefaultValue(0)]
        public int send_timeout { get; set; }
    }

    public class Php
    {
        public string profile_name { get; set; }
        public string profile_desc { get; set; }
        public int backend { get; set; }
        [DefaultValue("")]
        public string open_basedir { get; set; }
        [DefaultValue(null)]
        public List<string> extensions { get; set; }
        [DefaultValue(null)]
        public Dictionary<string, string> php_settings { get; set; }
        public string user { get; set; }
        public string group { get; set; }
    }

    public class PkgDirPrepare
    {
        [DefaultValue("")]
        public string source { get; set; }
        public string target { get; set; }
        public string mode { get; set; }
        public string group { get; set; }
        public string user { get; set; }
    }

    public class Portal
    {
        public string service { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        [DefaultValue("")]
        public string display_name { get; set; }
        [DefaultValue("")]
        public string alias { get; set; }
        [DefaultValue("")]
        public string app { get; set; }
        [DefaultValue(null)]
        public int[] http_port { get; set; }
        [DefaultValue(null)]
        public int[] https_port { get; set; }
        [DefaultValue("")]
        public string host { get; set; }
    }

    public class UsrLocalLinker
    {
        public List<string> bin { get; set; }
    }

    public class SyslogConfig
    {
        [JsonProperty("patterndb-relpath")]
        public string PatterndbRelpath { get; set; }

        [JsonProperty("logrotate-relpath")]
        public string LogrotateRelpath { get; set; }
    }

    public class PortConfig
    {
        [JsonProperty("protocol-file")]
        public string ProtocolFile { get; set; }
    }
}
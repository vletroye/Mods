using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

// Created using https://json2csharp.com/
namespace BeatificaBytes.Synology.Mods.Json.SpkResource
{
    // WebserviceRoot  myDeserializedClass = JsonConvert.DeserializeObject<WebserviceRoot>(myJsonResponse);

    public class Root
    {
        public Webservice webservice { get; set; }

        [JsonProperty("usr-local-linker")]
        public UsrLocalLinker UsrLocalLinker { get; set; }
    }
    public class Webservice
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
        [DefaultValue("")]
        public string index { get; set; }
        [DefaultValue("")]
        public string icon { get; set; }
        public int backend { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)] 
        public bool intercept_errors { get; set; }
        public Php php { get; set; }
        public int connect_timeout { get; set; }
        public int read_timeout { get; set; }
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
        [DefaultValue("")]
        public string http_port { get; set; }
        [DefaultValue("")]
        public string https_port { get; set; }
        [DefaultValue("")]
        public string host { get; set; }
    }

    public class UsrLocalLinker
    {
        public List<string> bin { get; set; }
    }

}
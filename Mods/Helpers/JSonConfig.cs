using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

// Created using https://json2csharp.com/
namespace BeatificaBytes.Synology.Mods.Json.SpkConfig
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Root
    {
        [JsonProperty(".url")]
        public Url Url { get; set; }
    }

    public class SynoApplication
    {
        public int itemType { get; set; }
        public string type { get; set; }
        public bool allUsers { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string icon { get; set; }
        public string appWindow { get; set; }
        public string url { get; set; }
        public bool allowMultiInstance { get; set; }
        public bool configablePrivilege { get; set; }
        public bool advanceGrantPrivilege { get; set; }
        public string grantPrivilege { get; set; }
        public List<string> preloadTexts { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Url
    {
        public List<SynoApplication> application { get; set; }
    }
}

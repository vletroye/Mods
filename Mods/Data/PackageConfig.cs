using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BeatificaBytes.Synology.Mods
{
    public class PackageConfig
    {
        internal string Path { get; set; }

        public PackageConfig()
        {
            items = new Dictionary<string, AppData>();
        }

        [JsonProperty(PropertyName = ".url")]
        public Dictionary<string, AppData> items { get; set; }

        internal static PackageConfig Load(string configPath)
        {
            PackageConfig obj = null;
            if (File.Exists(configPath))
            {
                var json = File.ReadAllText(configPath);
                try
                {
                    var privilege = JsonConvert.DeserializeObject<JObject>(json);
                    obj = privilege.ToObject<PackageConfig>();
                    obj.Path = configPath;
                }
                catch (Exception ex)
                {
                    //TODO: publish an error message
                    //PublishWarning(string.Format("The resource file '{0}' is corrupted...", resourceFile));
                    throw;
                }
            }
            else {
                obj = new PackageConfig();
                obj.Path = configPath;
            }

            return obj;
        }

        internal void Save()
        {
            try
            {
                var json = JsonConvert.SerializeObject(this, Formatting.Indented);
                Helper.WriteAnsiFile(Path, json);
            }
            catch (Exception ex)
            {
                //TODO: throw an exception
                //PublishWarning(string.Format("The privilege file '{0}' can't be updated.", privilegeFile));
            }
        }
    }
}

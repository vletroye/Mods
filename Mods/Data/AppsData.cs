using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace BeatificaBytes.Synology.Mods
{
    public class Package
    {
        public Package()
        {
            items = new Dictionary<string, AppsData>();
        }

        [JsonProperty(PropertyName = ".url")]
        public Dictionary<string, AppsData> items { get; set; }
    }

    public class AppsData
    {
        public AppsData()
        {
            type = "url";
            icon = "images/default_{0}.png";
            guid = Guid.NewGuid();
            itemType = -1;
        }


        // Custom MODS' parameters
        internal Guid guid { get; set; }
        //-1: undefined
        //0: url
        //1: script
        //2: webapp
        public int itemType { get; set; }

        // Synology url's parameters

        /*
        {
            ".url": {
                "<unique_name>": {
                    "type": "legacy",
                    "allUsers": false,
                    "allowMultiInstance": false,
                    "title": "DDNS updater",
                    "icon": "images / ddnsupdater_ {0} .png",
                    "appWindow": "<unique_name>",
                    "width": 960,
                    "height": 550,
                    "url": "/webman/3rdparty/ddnsupdater/ddnsupdater.php"
                }
            }
        }

        Parameter                   Description;                                                        Mandatory  Documented  Value / Type / Example
        <Unique_Name>:              Unique name, eg SYNO.SDS._ThirdParty.App.<application name>,            ✔         ✔      alphanumeric (without the - )
                                    without minus characters, otherwise the legacy type will not work.;
        type:                       For the window (embedded) solution, the key word ""legacy"" is used.    ✔         ✔      legacy, url (there are more values, but these are not suitable for use with 3rdparty applications)
                                    If the application is to be called up in a new window, ""url"" must
                                    be entered.
        allusers:                   Defines who is allowed to call the application                          -          ✔      only for admin (false), for all users (true)
        allowMultiInstance:         Allows the multiple opening of the application,                         -          -       false, true
                                    but there are still no empirical values
        grant privilege:            With this setting, the authorization to start the application can       -          -       all, local, ldap, domain
                                    be regulated. If this option is enabled, the application will appear
                                    under "Control Panel - Users - Applications" or 
                                    "Control Panel - Permissions". Important: To make this work for
                                    3rdparty apps, please read this post before http://www.synology-wiki.de/index.php/Aufbau_der_Datei_%27config%27 

        title:                      Yhe title of the application                                            ✔         ✔     alphanumeric
        desc:                       Text that appears in a mouse-over over the icon in the DSM              -          ✔     alphanumeric, possibly also UTF-8 text
        texts:                      Specifies the folder within the application directory in which the      -          ✔     alphanumeric
                                    files with the texts of the corresponding languages ​​are located. 
                                    If the option is available, the desired text can be accessed 
                                    using <section>: <variable>. (eg for title or desc)
        icon:                       The icon of the application for the 4 resolutions (16,24,32,48px),      ✔         ✔     16, 24, 32, 48
                                    the variable {0} is automatically filled with one of the 4 resolutions
        appWindow:                  The name of the application must be entered here, as in the parameter   -          -      alphanumeric, necessary for type legacy?
                                    <unique_name> above
        width / height:             The width / height of the application window when called in pixels      -          -      numeric
        url:                        The call address of the application, absolute URL or relative to        ✔         ✔     relative path ( must then start with a / ) or absolute URL
                                    the DocumentRoot of the port listening server 
                                    (usually / usr / syno / synoman or / var / services / web)
        protocol:                   URL protocol to the application. If this parameter is not specified,    -          -      Values ​​are eg "http", "https", "ftp". 
                                    the current protocol of the DSM is used. protocol and port must always                    If an absolute address is entered in url, this value is ignored
                                    be specified together
        port:                       The port of the link to be invoked on the DS. If this parameter is      -          -      numeric with quotation marks, eg "port": "80" (for http), "port": "443" (https), "port": "21" (ftp)
                                    not specified, default uses the current port of the DSM
        advance grant privilege:    An extended possibility to limit the users who are allowed to call      -          -      false, true
                                    this application (in addition to "grantPrivilege" groups and IP's)
        configablePrivilege:        To disable the ability to set the permission, even though the           -          -      false, true
                                    grantPrivilege option is enabled.
                            
        */

        public string type { get; set; }
        public bool allUsers { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string icon { get; set; }
        public string appWindow { get; set; }
        public string protocol { get; set; }
        public string url { get; set; }
        public int port { get; set; }
        public bool allowMultiInstance { get; set; }
        public bool configablePrivilege { get; set; }
        public bool advanceGrantPrivilege { get; set; }
        public string grantPrivilege { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public List<string> preloadTexts { get; set; }
    }
}

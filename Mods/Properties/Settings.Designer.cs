﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeatificaBytes.Synology.Mods.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Packages {
            get {
                return ((string)(this["Packages"]));
            }
            set {
                this["Packages"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string PackageRoot {
            get {
                return ((string)(this["PackageRoot"]));
            }
            set {
                this["PackageRoot"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SourceImages {
            get {
                return ((string)(this["SourceImages"]));
            }
            set {
                this["SourceImages"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<script type=\"text/javascript\">\r\n function autoScrolling() { window.scrollTo(0,do" +
            "cument.body.scrollHeight); }\r\n</script>\r\n<?php\r\n/**\r\n * Execute the given comman" +
            "d by displaying console output live to the user.\r\n *  @param  string  cmd       " +
            "   :  command to be executed\r\n *  @return array   exit_status  :  exit status of" +
            " the executed command\r\n *                  output       :  console output of the" +
            " executed command\r\n */\r\nfunction liveExecuteCommand($cmd)\r\n{\r\n\r\n    while (@ ob_" +
            "end_flush()); // end all output buffers if any\r\n\r\n    $proc = popen(\"$cmd 2>&1 ;" +
            " echo Exit status : $?\", \'r\');\r\n\r\n    $live_output     = \"\";\r\n    $complete_outp" +
            "ut = \"\";\r\n\r\n    while (!feof($proc))\r\n    {\r\n        $live_output     = fread($p" +
            "roc, 4096);\r\n        $complete_output = $complete_output . $live_output;\r\n      " +
            "  echo \"$live_output\";\r\n\t\t\r\n\t\techo \"<script type=\\\"text/javascript\\\">\";\r\n\t\techo " +
            "\"autoScrolling();\";\r\n\t\techo \"</script>\";\r\n\r\n        @ flush();\r\n    }\r\n\r\n    pcl" +
            "ose($proc);\r\n\r\n    // get exit status\r\n    preg_match(\'/[0-9]+$/\', $complete_out" +
            "put, $matches);\r\n\r\n    // return exit status and intended output\r\n    return arr" +
            "ay (\r\n                    \'exit_status\'  => intval($matches[0]),\r\n              " +
            "      \'output\'       => str_replace(\"Exit status : \" . $matches[0], \'\', $complet" +
            "e_output)\r\n                 );\r\n}\r\necho \"<pre>\";\r\n\r\n$result = liveExecuteCommand" +
            "(\"mods.sh\");\r\n\r\nif($result[\'exit_status\'] === 0){\r\n   // do something if command" +
            " execution succeeds\r\n} else {\r\n    // do something on failure\r\n}\r\necho \"</pre>\";" +
            "\r\necho \"<script type=\\\"text/javascript\\\">\";\r\necho \"autoScrolling();\";\r\necho \"</s" +
            "cript>\";\r\n?>")]
        public string Ps_Exec {
            get {
                return ((string)(this["Ps_Exec"]));
            }
            set {
                this["Ps_Exec"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Specialized.StringCollection Recents {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["Recents"]));
            }
            set {
                this["Recents"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Specialized.StringCollection RecentsName {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["RecentsName"]));
            }
            set {
                this["RecentsName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UpgradeRequired {
            get {
                return ((bool)(this["UpgradeRequired"]));
            }
            set {
                this["UpgradeRequired"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("CTP 10.0")]
        public string Version {
            get {
                return ((string)(this["Version"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string PackageRepo {
            get {
                return ((string)(this["PackageRepo"]));
            }
            set {
                this["PackageRepo"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool AdvancedEditor {
            get {
                return ((bool)(this["AdvancedEditor"]));
            }
            set {
                this["AdvancedEditor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DefaultPackageRepo {
            get {
                return ((bool)(this["DefaultPackageRepo"]));
            }
            set {
                this["DefaultPackageRepo"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool OpenWith {
            get {
                return ((bool)(this["OpenWith"]));
            }
            set {
                this["OpenWith"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DefaultPackageRoot {
            get {
                return ((bool)(this["DefaultPackageRoot"]));
            }
            set {
                this["DefaultPackageRoot"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string LastPackage {
            get {
                return ((string)(this["LastPackage"]));
            }
            set {
                this["LastPackage"] = value;
            }
        }
    }
}
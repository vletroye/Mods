using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZTn.Json.Editor
{
    public class Helper
    {
        static Regex cleanChar = new Regex(@"[^a-zA-Z0-9]", RegexOptions.Compiled);

        internal static string CleanUpText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var icon = cleanChar.Replace(text, "_");
                while (text != icon)
                {
                    text = icon.Trim(new[] { '_' });
                    icon = text.Replace("__", "_");
                }
            }

            return text;
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static string ResourcesDirectory
        {
            get
            {
                return Path.Combine(Helper.AssemblyDirectory, "Resources");                
            }
        }

        internal static string GetSubItemType(string type)
        {
            string subitemType = "";
            switch (type)
            {
                case "singleselect":
                    subitemType = "Radio Button";
                    break;
                case "multiselect":
                    subitemType = "Check Box";
                    break;
                case "textfield":
                    subitemType = "Text Box";
                    break;
                case "password":
                    subitemType = "Password";
                    break;
                case "combobox":
                    subitemType = "Combo Box";
                    break;
            }
            return subitemType;
        }
    }
}

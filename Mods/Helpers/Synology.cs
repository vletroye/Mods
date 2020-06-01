using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BeatificaBytes.Synology.Mods.Helpers
{
    class Synology
    {
        public static List<Tuple<string, string>> GetAllWizardVariables(string packageFolder)
        {
            List<Tuple<string, string>> variables = new List<Tuple<string, string>>();

            var wizard = Path.Combine(packageFolder, "WIZARD_UIFILES");

            if (Directory.Exists(wizard))
            {
                string line;
                foreach (var filename in Directory.GetFiles(wizard))
                {

                    // Read the file and display it line by line.
                    using (var file = new StreamReader(filename))
                    {
                        while ((line = file.ReadLine()) != null)
                        {
                            Match match = Regex.Match(line, @"""key"".*:.*""(.*)""", RegexOptions.IgnoreCase);
                            if (match.Success)
                            {
                                variables.Add(new Tuple<string, string>(match.Groups[1].Value, string.Format("Variables from Wizard file '{0}'", Path.GetFileName(filename))));
                            }
                        }
                    }

                    // Add a separator
                    if (variables.Count > 0) variables.Add(new Tuple<string, string>("_________________________________________________________", null));
                }
            }

            return variables;
        }
    }
}

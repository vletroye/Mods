using System;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;

static class Win32
{

    [DllImport("kernel32.dll")]
    public static extern IntPtr LoadLibrary(string dllToLoad);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

    [DllImport("kernel32.dll")]
    public static extern bool FreeLibrary(IntPtr hModule);

}

namespace BeatificaBytes.Synology.Mods
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                var hash = args.SingleOrDefault(arg => arg.StartsWith("hash:"));
                if (!string.IsNullOrEmpty(hash))
                {
                    var path = hash.Replace("hash:", "");
                    if (path == ".") path = AppDomain.CurrentDomain.BaseDirectory;
                    Helper.ComputeMD5Hash(path);
                }
                else
                {
                    string open = null;
                    var edit = args.SingleOrDefault(arg => arg.StartsWith("edit:"));
                    if (!string.IsNullOrEmpty(edit))
                    {
                        open = edit.Replace("edit:", "");
                    }
                    else if (Properties.Settings.Default.UpgradeRequired)
                    {
                        Properties.Settings.Default.Upgrade();
                        Properties.Settings.Default.UpgradeRequired = false;
                        Properties.Settings.Default.Save();
                    }

                    // Load the default runner script or create one if it does not exist
                    var defaultRunnerPath = Path.Combine(Helper.ResourcesDirectory, "default.runner");
                    if (!File.Exists(defaultRunnerPath))
                        File.WriteAllText(defaultRunnerPath, Properties.Settings.Default.Ps_Exec);

                    // Extract the WizardUI background image if it does not exist
                    var backWizard = Path.Combine(Helper.ResourcesDirectory, "backwizard.png");
                    if (!File.Exists(backWizard))
                    {
                        var backWizardPng = new Bitmap(Properties.Resources.BackWizard);
                        backWizardPng.Save(backWizard);
                    }

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm(open));
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(string.Format("Mods Packager failed to run due to a fatal error : {0}\r\n\r\nIt will now stop.", ex.Message), "Fatal Error");
                throw;
            }
        }
    }
}

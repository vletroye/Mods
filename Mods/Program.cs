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
                string open = null;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

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
                    Helper.WriteAnsiFile(defaultRunnerPath, Properties.Settings.Default.Ps_Exec);

                // Load the Router Config or create one if it does not exist
                var defaultRouterConfigPath = Path.Combine(Helper.ResourcesDirectory, "dsm.cgi.conf");
                if (!File.Exists(defaultRouterConfigPath))
                    Helper.WriteAnsiFile(defaultRouterConfigPath, Properties.Settings.Default.dsm_cgi);

                // Load the Router Script or create one if it does not exist
                var defaultRouterScriptPath = Path.Combine(Helper.ResourcesDirectory, "router.cgi");
                if (!File.Exists(defaultRouterScriptPath))
                    Helper.WriteAnsiFile(defaultRouterScriptPath, Properties.Settings.Default.router_cgi);

                // Load the default DSM release list or create one if it does not exist
                var defaultDSMReleases = Path.Combine(Helper.ResourcesDirectory, "dsm_releases");
                if (!File.Exists(defaultDSMReleases))
                    Helper.WriteAnsiFile(defaultDSMReleases, Properties.Settings.Default.dsm_releases);

                // Load the default php Extension list or create one if it does not exist
                var defaultPhpExtensions= Path.Combine(Helper.ResourcesDirectory, "php_Extensions");
                if (!File.Exists(defaultPhpExtensions))
                    Helper.WriteAnsiFile(defaultPhpExtensions, Properties.Settings.Default.php_extensions);

                // Extract the WizardUI background image if it does not exist
                var backWizard = Path.Combine(Helper.ResourcesDirectory, "backwizard.png");
                if (!File.Exists(backWizard))
                {
                    var backWizardPng = new Bitmap(Properties.Resources.BackWizard);
                    backWizardPng.Save(backWizard);
                }

                Application.Run(new MainForm(open));
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(string.Format("Mods Packager failed to run due to a fatal error : {0}\r\n\r\nIt will now stop.", ex.Message), "Fatal Error");
            }
        }
    }
}

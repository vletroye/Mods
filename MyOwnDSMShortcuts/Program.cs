using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

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
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var path = AppDomain.CurrentDomain.BaseDirectory;
            var sciLexer = Path.Combine(path, "Resources", "sciLexer.dll");
            if (!File.Exists(sciLexer))
                MessageBox.Show("sciLexer.dll is missing. Mods cannot run without that dll.");
            Win32.LoadLibrary(sciLexer);

            Application.Run(new MainForm());
        }
    }
}

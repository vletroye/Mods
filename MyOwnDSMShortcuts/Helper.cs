using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace BeatificaBytes.Synology.Mods
{
    public class Helper
    {
        static Regex cleanChar = new Regex(@"[^a-zA-Z0-9]", RegexOptions.Compiled);

        // Remove all characters that cannot be used in names stored in a package
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

        internal static byte roundByte(byte code, int range)
        {
            var value = (int)code + range;
            if (value < 0) value = 0;
            if (value > 255) value = 255;
            return (byte)value;
        }

        internal static bool GetDragDropFilename(out string filename, DragEventArgs e)
        {
            bool ret = false;
            filename = String.Empty;
            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                Array data = ((IDataObject)e.Data).GetData("FileDrop") as Array;
                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is String))
                    {
                        filename = ((string[])data)[0];
                        string ext = Path.GetExtension(filename).ToLower();
                        if (ext == ".png" || ext == ".jpg" || ext == ".bmp")
                        {
                            ret = true;
                        }
                    }
                }
            }
            return ret;
        }

        internal static string IncrementVersion(string version)
        {
            var versions = version.Split('.');
            var minor = int.Parse(versions[2]) + 1;
            return string.Format("{0}.{1}.{2}", versions[0], versions[1], minor);
        }

        internal static void DeleteDirectory(string path)
        {
            foreach (string directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }

            try
            {
                Directory.Delete(path, true);
                while (Directory.Exists(path)) { }
            }
            catch (IOException)
            {
                Directory.Delete(path, true);
            }
            catch (UnauthorizedAccessException)
            {
                Directory.Delete(path, true);
            }
        }

        internal static void CopyDirectory(string strSource, string strDestination)
        {
            if (!Directory.Exists(strDestination))
            {
                Directory.CreateDirectory(strDestination);
            }

            DirectoryInfo dirInfo = new DirectoryInfo(strSource);
            FileInfo[] files = dirInfo.GetFiles();
            foreach (FileInfo tempfile in files)
            {
                tempfile.CopyTo(Path.Combine(strDestination, tempfile.Name));
            }

            DirectoryInfo[] directories = dirInfo.GetDirectories();
            foreach (DirectoryInfo tempdir in directories)
            {
                CopyDirectory(Path.Combine(strSource, tempdir.Name), Path.Combine(strDestination, tempdir.Name));
            }
        }

        internal static string FindFileIndex(string[] files, string filename)
        {
            string file = null;
            var index = Array.FindIndex(files, p => p.Equals(filename, StringComparison.CurrentCultureIgnoreCase));
            if (index >= 0)
            {
                file = files[index];
            }
            return file;
        }

        //internal static DateTime GetLinkerTime(Assembly assembly, TimeZoneInfo target = null)
        //{
        //    var filePath = assembly.Location;
        //    const int c_PeHeaderOffset = 60;
        //    const int c_LinkerTimestampOffset = 8;

        //    var buffer = new byte[2048];

        //    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //        stream.Read(buffer, 0, 2048);

        //    var offset = BitConverter.ToInt32(buffer, c_PeHeaderOffset);
        //    var secondsSince1970 = BitConverter.ToInt32(buffer, offset + c_LinkerTimestampOffset);
        //    var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        //    var linkTimeUtc = epoch.AddSeconds(secondsSince1970);

        //    var tz = target ?? TimeZoneInfo.Local;
        //    var localTime = TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, tz);

        //    return localTime;
        //}

        internal static string PickFolder(string path)
        {
            var folderDialogEx = new Ionic.Utils.FolderBrowserDialogEx();
            folderDialogEx.Description = "Select a folder to extract to:";
            folderDialogEx.ShowNewFolderButton = true;
            folderDialogEx.ShowEditBox = true;
            //dlg1.NewStyle = false;
            folderDialogEx.SelectedPath = path;
            folderDialogEx.ShowFullPathInEditBox = true;
            folderDialogEx.RootFolder = System.Environment.SpecialFolder.MyComputer;

            // Show the FolderBrowserDialog.
            DialogResult result = folderDialogEx.ShowDialog();
            if (result == DialogResult.OK)
            {
                path = folderDialogEx.SelectedPath;
            }
            return path;
        }

        internal static bool IsSubscribed(Control controlObject, string eventName)
        {
            // Check all possible eventName by debugging: typeof(Control).GetFields(BindingFlags.Static | BindingFlags.NonPublic)
            FieldInfo event_visible_field_info = typeof(Control).GetField(eventName, BindingFlags.Static | BindingFlags.NonPublic);
            object object_value = event_visible_field_info.GetValue(controlObject);
            PropertyInfo events_property_info = controlObject.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
            EventHandlerList event_list = (EventHandlerList)events_property_info.GetValue(controlObject, null);
            return (event_list[object_value] != null);
        }

        internal static DialogResult ScriptEditor(string inputScript, string inputRunner, out string outputScript, out string outputRunner)
        {
            var editScript = new ScriptForm();
            editScript.Script = inputScript;
            editScript.Runner = inputRunner;
            editScript.StartPosition = FormStartPosition.CenterParent;
            var result = editScript.ShowDialog(Application.OpenForms[0]);
            outputScript = editScript.Script;
            if (outputScript != null)
            {
                // Remove \r not supported in shell scripts
                outputScript = outputScript.Replace("\r\n", "\n");
            }

            outputRunner = editScript.Runner;

            if (inputScript == outputScript && inputRunner == outputRunner)
                result = DialogResult.Cancel;

            return result;

        }

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        internal static DialogResult ScriptEditor(string inputScript, string inputRunner, out string output)
        {
            var outputFake = "";
            DialogResult result;
            if (inputScript == null)
            {
                result = ScriptEditor(null, inputRunner, out outputFake, out output);
            }
            else
            {
                result = ScriptEditor(inputScript, null, out output, out outputFake);
            }

            return result;
        }
    }
}

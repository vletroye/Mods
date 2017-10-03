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
using System.Security.Cryptography;

namespace BeatificaBytes.Synology.Mods
{
    public class Helper
    {
        static Regex cleanChar = new Regex(@"[^a-zA-Z0-9\-]", RegexOptions.Compiled);

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
            var versions = version.Replace("b", ".").Split(new char[] { '.', '-' });
            var major = 0;
            var minor = 0;
            var build = 1;
            if (versions.Length > 2)
                build = int.Parse(versions[2]) + 1;
            if (versions.Length > 1)
                minor = int.Parse(versions[1]);
            if (versions.Length > 0)
                major = int.Parse(versions[0]);
            return string.Format("{0}.{1}.{2:0000}", major, minor, build);
        }

        internal static Exception DeleteDirectory(string path)
        {
            Exception succeed = null;

            if (path == null) path = "";

            if (!Directory.Exists(path))
            {
                succeed = new FileNotFoundException(string.Format("'{0}' does not exist.", path), path);
            }
            else
            {
                foreach (string directory in Directory.GetDirectories(path))
                {
                    succeed = DeleteDirectory(directory);
                    if (succeed != null)
                        break;
                }

                if (succeed == null)
                {
                    try
                    {
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
                    catch (Exception ex)
                    {
                        succeed = ex;
                    }
                }
            }

            return succeed;
        }

        internal static void CopyDirectory(string strSource, string strDestination)
        {
            using (new CWaitCursor())
            {

                if (strDestination.StartsWith(strSource))
                {
                    MessageBox.Show(string.Format("'{0}' cannot be copied into '{1}'", strSource, strDestination), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
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

        internal static string PickFolder(string path)
        {
            var folderDialogEx = new OpenFolderDialog();
            folderDialogEx.Title = "Select a folder to extract to:";
            folderDialogEx.InitialDirectory = Environment.GetFolderPath(System.Environment.SpecialFolder.MyComputer);

            // Show the FolderBrowserDialog.
            bool result = folderDialogEx.ShowDialog();
            if (result)
            {
                path = folderDialogEx.FileName;
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

        internal static DialogResult ScriptEditor(string inputScript, string inputRunner, List<Tuple<string, string>> variables, out string outputScript, out string outputRunner)
        {
            var editScript = new ScriptForm();
            editScript.Script = inputScript;
            editScript.Runner = inputRunner;
            editScript.Variables = variables;
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

        internal static DialogResult ScriptEditor(string inputScript, string inputRunner, List<Tuple<string, string>> variables, out string output)
        {
            var outputFake = "";
            DialogResult result;
            if (inputScript == null)
            {
                result = ScriptEditor(null, inputRunner, null, out outputFake, out output);
            }
            else
            {
                result = ScriptEditor(inputScript, null, variables, out output, out outputFake);
            }

            return result;
        }

        internal static bool IsValidUrl(string Url)
        {
            Uri outUri;
            return (Uri.TryCreate(Url, UriKind.Absolute, out outUri)
                        && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps));
        }


        public static void EncryptAndSign(string sourcePath, string targetPath, string passPhrase, string publicKeyFileName, string privateKeyFileName)
        {
            PgpEncryptionKeys encryptionKeys = new PgpEncryptionKeys(publicKeyFileName, privateKeyFileName, passPhrase);
            PgpEncrypt encrypter = new PgpEncrypt(encryptionKeys);
            using (Stream outputStream = File.Create(targetPath))
            {
                encrypter.EncryptAndSign(outputStream, new FileInfo(sourcePath));
            }
        }

        public static void ComputeMD5Hash(string path)
        {
            string hash = "";
            var package = Path.Combine(path, "package.tgz");
            var info = Path.Combine(path, "INFO");

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(package))
                {
                    var hashByte = md5.ComputeHash(stream);
                    hash = BitConverter.ToString(hashByte).Replace("-", "").ToLower();
                }
            }

            if (File.Exists(info))
            {
                var lines = File.ReadAllLines(info);
                using (StreamWriter outputFile = new StreamWriter(info))
                {
                    bool check = false;
                    foreach (var line in lines)
                    {
                        var key = line.Substring(0, line.IndexOf('='));
                        var value = line.Substring(line.IndexOf('=') + 1);
                        value = value.Trim(new char[] { '"' });

                        if (key == "checksum")
                        {
                            value = hash;
                            check = true;
                        }

                        outputFile.WriteLine("{0}=\"{1}\"", key, value);
                    }
                    if (!check)
                        outputFile.WriteLine("checksum=\"{0}\"", hash);
                }
            }
        }
    }
}

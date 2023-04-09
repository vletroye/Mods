using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hash
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = ".";
            if (args.Length > 0)
                path = args[0];
            if (path == ".") path = AppDomain.CurrentDomain.BaseDirectory;
            ComputeMD5Hash(path);
        }

        static void ComputeMD5Hash(string path)
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
                //var lines = File.ReadAllLines(info, Encoding.Default);
                //using (StreamWriter outputFile = new StreamWriter(info, false, Encoding.GetEncoding(1252)))
                var lines = File.ReadAllLines(info);
                using (StreamWriter outputFile = new StreamWriter(info, false))
                {
                    foreach (var line in lines)
                    {
                        var key = line.Substring(0, line.IndexOf('='));
                        var value = line.Substring(line.IndexOf('=') + 1);
                        value = value.Trim(new char[] { '"' });

                        if (key != "checksum")
                            outputFile.WriteLine("{0}=\"{1}\"", key, value);
                    }
                    outputFile.WriteLine("checksum=\"{0}\"", hash);
                }
            }
        }
    }
}

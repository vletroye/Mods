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
using ImageMagick;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using static System.Environment;

namespace BeatificaBytes.Synology.Mods
{
    public static class Helper
    {
        public static Regex getPort = new Regex(@"^:(\d*)/.*$", RegexOptions.Compiled);
        public static Regex getOldFirmwareVersion = new Regex(@"^\d+.\d+\.\d+$", RegexOptions.Compiled);
        public static Regex getShortFirmwareVersion = new Regex(@"^\d+\.\d+$", RegexOptions.Compiled);
        public static Regex getFirmwareVersion = new Regex(@"^(\d+)\.(\d+)(\.[^-]*){0,1}-(\d+)(.*)$", RegexOptions.Compiled); //new Regex(@"^(\d+\.)*\d-\d+$", RegexOptions.Compiled);

        public static Regex getOldVersion = new Regex(@"^((\d+\.)*\d+)\.(\d+)$", RegexOptions.Compiled);
        public static Regex getVersion = new Regex(@"^(\d+\.)*\d+-\d+$", RegexOptions.Compiled);

        [DllImport("kernel32.dll")]
        static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, SymLinkFlag dwFlags);
        internal enum SymLinkFlag
        {
            File = 0,
            Directory = 1
        }

        static Regex cleanChar = new Regex(@"[^a-zA-Z0-9\-]", RegexOptions.Compiled);
        static string resourcesDirectory = null;

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
                if (string.IsNullOrEmpty(resourcesDirectory))
                {
                    resourcesDirectory = Path.Combine(Helper.AssemblyDirectory, "Resources");
                    if (!Directory.Exists(resourcesDirectory))
                    {
                        throw new Exception("MODs cannot run because its Ressource folder is missing. There is probably an issue with its setup.");
                    }

                    // Check if the folder can be written
                    try
                    {
                        using (var lastRun = File.CreateText(Path.Combine(resourcesDirectory, "LastRun")))
                        {
                            lastRun.Write(DateTime.Now);
                        }
                    }
                    catch
                    {
                        var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
                        var companyName = versionInfo.CompanyName;
                        var productName = versionInfo.ProductName;
                        var appData = Path.Combine(Environment.GetFolderPath(SpecialFolder.CommonApplicationData), companyName, productName);

                        Helper.CopyDirectory(resourcesDirectory, appData, true);
                        resourcesDirectory = appData;

                        using (var lastRun = File.CreateText(Path.Combine(resourcesDirectory, "LastRun")))
                        {
                            lastRun.Write(DateTime.Now);
                        }
                    }

                }
                return resourcesDirectory;
            }
        }

        public static Image LoadImageFromFile(string picture)
        {
            Image image = null;
            if (File.Exists(picture))
                using (FileStream stream = new FileStream(picture, FileMode.Open, FileAccess.Read))
                {
                    image = Image.FromStream(stream);
                }
            return image;
        }

        public static Bitmap LoadImage(string picture, int transparency, int size)
        {
            Bitmap copy = null;
            try
            {
                if (transparency > 0)
                {
                    using (MagickImage image = new MagickImage(picture))
                    {
                        if (size == 0)
                        {
                            size = image.Width > image.Height ? image.Width : image.Height;
                        }
                        copy = new Bitmap(size, size);

                        image.Format = MagickFormat.Png;

                        var backColor = image.GetPixels().GetPixel(1, 1).ToColor();

                        image.ColorFuzz = new Percentage(transparency);
                        image.BackgroundColor = MagickColors.None;
                        image.Transparent(backColor);
                        //image.TransparentChroma(new ColorRGB(Rl, Gl, Bl), new ColorRGB(Ru, Gu, Bu));

                        using (Graphics g = Graphics.FromImage(copy))
                        {
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.DrawImage(image.ToBitmap(), 0, 0, copy.Width, copy.Height);
                        }
                    }
                }
                else
                {
                    using (Image image = Helper.LoadImageFromFile(picture))
                    {
                        if (size == 0)
                        {
                            copy = new Bitmap(image.Width, image.Height);
                        }
                        else
                        {
                            copy = new Bitmap(size, size);
                        }

                        using (Graphics g = Graphics.FromImage(copy))
                        {
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.DrawImage(image, 0, 0, copy.Width, copy.Height);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(Application.OpenForms[0], string.Format("The image {0} connot be loaded due to: {1}", picture, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (size == 0) size = 1;
            }

            return copy;
        }

        public static Image GetImageFromClipboard()
        {
            Image img = null;
            DataObject clipboard = Clipboard.GetDataObject() as DataObject;
            if (clipboard != null)
            {
                img = GetClipboardImage(clipboard);
            }

            return img;
        }

        /// <summary>
        /// Retrieves an image from the given clipboard data object, in the order PNG, DIB, Bitmap, Image object.
        /// </summary>
        /// <param name="retrievedData">The clipboard data.</param>
        /// <returns>The extracted image, or null if no supported image type was found.</returns>
        public static Bitmap GetClipboardImage(DataObject retrievedData)
        {
            Bitmap clipboardimage = null;
            // Order: try PNG, move on to try 32-bit ARGB DIB, then try the normal Bitmap and Image types.
            if (retrievedData.GetDataPresent("PNG"))
            {
                MemoryStream png_stream = retrievedData.GetData("PNG") as MemoryStream;
                if (png_stream != null)
                    using (Bitmap bm = new Bitmap(png_stream))
                        clipboardimage = CloneImage(bm);
            }
            if (clipboardimage == null && retrievedData.GetDataPresent(DataFormats.Dib))
            {
                MemoryStream dib = retrievedData.GetData(DataFormats.Dib) as MemoryStream;
                if (dib != null)
                    clipboardimage = ImageFromClipboardDib(dib.ToArray());
            }
            if (clipboardimage == null && retrievedData.GetDataPresent(DataFormats.Bitmap))
                clipboardimage = new Bitmap(retrievedData.GetData(DataFormats.Bitmap) as Image);
            if (clipboardimage == null && retrievedData.GetDataPresent(typeof(Image)))
                clipboardimage = new Bitmap(retrievedData.GetData(typeof(Image)) as Image);
            return clipboardimage;
        }

        private static Bitmap CloneImage(Bitmap bm)
        {
            int stride;
            return BuildImage(GetImageData(bm, out stride), bm.Width, bm.Height, stride, bm.PixelFormat, null, null);
        }

        /// <summary>
        /// Copies the given image to the clipboard as PNG, DIB and standard Bitmap format.
        /// </summary>
        /// <param name="image">Image to put on the clipboard.</param>
        /// <param name="imageNoTr">Optional specifically nontransparent version of the image to put on the clipboard.</param>
        /// <param name="data">Clipboard data object to put the image into. Might already contain other stuff. Leave null to create a new one.</param>
        public static void SetClipboardImage(Bitmap image, Bitmap imageNoTr, DataObject data)
        {
            Clipboard.Clear();
            if (data == null)
                data = new DataObject();
            if (imageNoTr == null)
                imageNoTr = image;
            using (MemoryStream pngMemStream = new MemoryStream())
            using (MemoryStream dibMemStream = new MemoryStream())
            {
                // As standard bitmap, without transparency support
                data.SetData(DataFormats.Bitmap, true, imageNoTr);
                // As PNG. Gimp will prefer this over the other two.
                image.Save(pngMemStream, ImageFormat.Png);
                data.SetData("PNG", false, pngMemStream);
                // As DIB. This is (wrongly) accepted as ARGB by many applications.
                Byte[] dibData = ConvertToDib(image);
                dibMemStream.Write(dibData, 0, dibData.Length);
                data.SetData(DataFormats.Dib, false, dibMemStream);
                // The 'copy=true' argument means the MemoryStreams can be safely disposed after the operation.
                Clipboard.SetDataObject(data, true);
            }
        }

        /// <summary>
        /// Converts the image to Device Independent Bitmap format of type BITFIELDS.
        /// This is (wrongly) accepted by many applications as containing transparency,
        /// so I'm abusing it for that.
        /// </summary>
        /// <param name="image">Image to convert to DIB</param>
        /// <returns>The image converted to DIB, in bytes.</returns>
        public static Byte[] ConvertToDib(Image image)
        {
            Byte[] bm32bData;
            Int32 width = image.Width;
            Int32 height = image.Height;
            // Ensure image is 32bppARGB by painting it on a new 32bppARGB image.
            using (Bitmap bm32b = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(bm32b))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, new Rectangle(0, 0, bm32b.Width, bm32b.Height));
                }
                // Bitmap format has its lines reversed.
                bm32b.RotateFlip(RotateFlipType.Rotate180FlipX);
                Int32 stride;
                bm32bData = GetImageData(bm32b, out stride);
            }
            // BITMAPINFOHEADER struct for DIB.
            Int32 hdrSize = 0x28;
            Byte[] fullImage = new Byte[hdrSize + 12 + bm32bData.Length];
            //Int32 biSize;
            WriteIntToByteArray(fullImage, 0x00, 4, true, (UInt32)hdrSize);
            //Int32 biWidth;
            WriteIntToByteArray(fullImage, 0x04, 4, true, (UInt32)width);
            //Int32 biHeight;
            WriteIntToByteArray(fullImage, 0x08, 4, true, (UInt32)height);
            //Int16 biPlanes;
            WriteIntToByteArray(fullImage, 0x0C, 2, true, 1);
            //Int16 biBitCount;
            WriteIntToByteArray(fullImage, 0x0E, 2, true, 32);
            //BITMAPCOMPRESSION biCompression = BITMAPCOMPRESSION.BITFIELDS;
            WriteIntToByteArray(fullImage, 0x10, 4, true, 3);
            //Int32 biSizeImage;
            WriteIntToByteArray(fullImage, 0x14, 4, true, (UInt32)bm32bData.Length);
            // These are all 0. Since .net clears new arrays, don't bother writing them.
            //Int32 biXPelsPerMeter = 0;
            //Int32 biYPelsPerMeter = 0;
            //Int32 biClrUsed = 0;
            //Int32 biClrImportant = 0;

            // The aforementioned "BITFIELDS": colour masks applied to the Int32 pixel value to get the R, G and B values.
            WriteIntToByteArray(fullImage, hdrSize + 0, 4, true, 0x00FF0000);
            WriteIntToByteArray(fullImage, hdrSize + 4, 4, true, 0x0000FF00);
            WriteIntToByteArray(fullImage, hdrSize + 8, 4, true, 0x000000FF);
            Array.Copy(bm32bData, 0, fullImage, hdrSize + 12, bm32bData.Length);
            return fullImage;
        }

        public static Bitmap ImageFromClipboardDib(Byte[] dibBytes)
        {
            if (dibBytes == null || dibBytes.Length < 4)
                return null;
            try
            {
                Int32 headerSize = (Int32)ReadIntFromByteArray(dibBytes, 0, 4, true);
                // Only supporting 40-byte DIB from clipboard
                if (headerSize != 40)
                    return null;
                Byte[] header = new Byte[40];
                Array.Copy(dibBytes, header, 40);
                Int32 imageIndex = headerSize;
                Int32 width = (Int32)ReadIntFromByteArray(header, 0x04, 4, true);
                Int32 height = (Int32)ReadIntFromByteArray(header, 0x08, 4, true);
                Int16 planes = (Int16)ReadIntFromByteArray(header, 0x0C, 2, true);
                Int16 bitCount = (Int16)ReadIntFromByteArray(header, 0x0E, 2, true);
                //Compression: 0 = RGB; 3 = BITFIELDS.
                Int32 compression = (Int32)ReadIntFromByteArray(header, 0x10, 4, true);
                // Not dealing with non-standard formats.
                if (planes != 1 || (compression != 0 && compression != 3))
                    return null;
                PixelFormat fmt;
                switch (bitCount)
                {
                    case 32:
                        fmt = PixelFormat.Format32bppRgb;
                        break;
                    case 24:
                        fmt = PixelFormat.Format24bppRgb;
                        break;
                    case 16:
                        fmt = PixelFormat.Format16bppRgb555;
                        break;
                    default:
                        return null;
                }
                if (compression == 3)
                    imageIndex += 12;
                if (dibBytes.Length < imageIndex)
                    return null;
                Byte[] image = new Byte[dibBytes.Length - imageIndex];
                Array.Copy(dibBytes, imageIndex, image, 0, image.Length);
                // Classic stride: fit within blocks of 4 bytes.
                Int32 stride = (((((bitCount * width) + 7) / 8) + 3) / 4) * 4;
                if (compression == 3)
                {
                    UInt32 redMask = ReadIntFromByteArray(dibBytes, headerSize + 0, 4, true);
                    UInt32 greenMask = ReadIntFromByteArray(dibBytes, headerSize + 4, 4, true);
                    UInt32 blueMask = ReadIntFromByteArray(dibBytes, headerSize + 8, 4, true);
                    // Fix for the undocumented use of 32bppARGB disguised as BITFIELDS. Despite lacking an alpha bit field,
                    // the alpha bytes are still filled in, without any header indication of alpha usage.
                    // Pure 32-bit RGB: check if a switch to ARGB can be made by checking for non-zero alpha.
                    // Admitted, this may give a mess if the alpha bits simply aren't cleared, but why the hell wouldn't it use 24bpp then?
                    if (bitCount == 32 && redMask == 0xFF0000 && greenMask == 0x00FF00 && blueMask == 0x0000FF)
                    {
                        // Stride is always a multiple of 4; no need to take it into account for 32bpp.
                        for (Int32 pix = 3; pix < image.Length; pix += 4)
                        {
                            // 0 can mean transparent, but can also mean the alpha isn't filled in, so only check for non-zero alpha,
                            // which would indicate there is actual data in the alpha bytes.
                            if (image[pix] == 0)
                                continue;
                            fmt = PixelFormat.Format32bppPArgb;
                            break;
                        }
                    }
                    else
                        // Could be supported with a system that parses the colour masks,
                        // but I don't think the clipboard ever uses these anyway.
                        return null;
                }
                Bitmap bitmap = BuildImage(image, width, height, stride, fmt, null, null);
                // This is bmp; reverse image lines.
                bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
                return bitmap;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the raw bytes from an image.
        /// </summary>
        /// <param name="sourceImage">The image to get the bytes from.</param>
        /// <param name="stride">Stride of the retrieved image data.</param>
        /// <returns>The raw bytes of the image</returns>
        public static Byte[] GetImageData(Bitmap sourceImage, out Int32 stride)
        {
            BitmapData sourceData = sourceImage.LockBits(new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), ImageLockMode.ReadOnly, sourceImage.PixelFormat);
            stride = sourceData.Stride;
            Byte[] data = new Byte[stride * sourceImage.Height];
            Marshal.Copy(sourceData.Scan0, data, 0, data.Length);
            sourceImage.UnlockBits(sourceData);
            return data;
        }

        /// <summary>
        /// Creates a bitmap based on data, width, height, stride and pixel format.
        /// </summary>
        /// <param name="sourceData">Byte array of raw source data</param>
        /// <param name="width">Width of the image</param>
        /// <param name="height">Height of the image</param>
        /// <param name="stride">Scanline length inside the data</param>
        /// <param name="pixelFormat">Pixel format</param>
        /// <param name="palette">Color palette</param>
        /// <param name="defaultColor">Default color to fill in on the palette if the given colors don't fully fill it.</param>
        /// <returns>The new image</returns>
        public static Bitmap BuildImage(Byte[] sourceData, Int32 width, Int32 height, Int32 stride, PixelFormat pixelFormat, Color[] palette, Color? defaultColor)
        {
            Bitmap newImage = new Bitmap(width, height, pixelFormat);
            BitmapData targetData = newImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, newImage.PixelFormat);
            CopyToMemory(targetData.Scan0, sourceData, 0, sourceData.Length, stride, targetData.Stride);
            newImage.UnlockBits(targetData);
            // For indexed images, set the palette.
            if ((pixelFormat & PixelFormat.Indexed) != 0 && palette != null)
            {
                ColorPalette pal = newImage.Palette;
                for (Int32 i = 0; i < pal.Entries.Length; i++)
                {
                    if (i < palette.Length)
                        pal.Entries[i] = palette[i];
                    else if (defaultColor.HasValue)
                        pal.Entries[i] = defaultColor.Value;
                    else
                        break;
                }
                newImage.Palette = pal;
            }
            return newImage;
        }
        public static void CopyToMemory(IntPtr target, Byte[] bytes, Int32 startPos, Int32 length, Int32 origStride, Int32 targetStride)
        {
            Int32 sourcePos = startPos;
            IntPtr destPos = target;
            Int32 minStride = Math.Min(origStride, targetStride);
            while (length >= targetStride)
            {
                Marshal.Copy(bytes, sourcePos, destPos, minStride);
                length -= origStride;
                sourcePos += origStride;
                destPos = new IntPtr(destPos.ToInt64() + targetStride);
            }
            if (length > 0)
            {
                Marshal.Copy(bytes, sourcePos, destPos, length);
            }
        }

        public static void WriteIntToByteArray(Byte[] data, Int32 startIndex, Int32 bytes, Boolean littleEndian, UInt32 value)
        {
            Int32 lastByte = bytes - 1;
            if (data.Length < startIndex + bytes)
                throw new ArgumentOutOfRangeException("startIndex", "Data array is too small to write a " + bytes + "-byte value at offset " + startIndex + ".");
            for (Int32 index = 0; index < bytes; index++)
            {
                Int32 offs = startIndex + (littleEndian ? index : lastByte - index);
                data[offs] = (Byte)(value >> (8 * index) & 0xFF);
            }
        }

        public static UInt32 ReadIntFromByteArray(Byte[] data, Int32 startIndex, Int32 bytes, Boolean littleEndian)
        {
            Int32 lastByte = bytes - 1;
            if (data.Length < startIndex + bytes)
                throw new ArgumentOutOfRangeException("startIndex", "Data array is too small to read a " + bytes + "-byte value at offset " + startIndex + ".");
            UInt32 value = 0;
            for (Int32 index = 0; index < bytes; index++)
            {
                Int32 offs = startIndex + (littleEndian ? index : lastByte - index);
                value += (UInt32)(data[offs] << (8 * index));
            }
            return value;
        }

        public static Image LoadImageFromBase64(string picture)
        {
            byte[] base64 = Convert.FromBase64String(picture);

            Image image = null;
            using (var ms = new MemoryStream(base64, 0, base64.Length))
            {
                image = Image.FromStream(ms, true);
            }
            return image;
        }
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

        internal static byte roundShort(ushort code, int range)
        {
            var value = (int)code + range;
            if (value < 0) value = 0;
            if (value > 255) value = 255;
            return (byte)value;
        }

        internal static bool GetDragDropFilename(out string filename, DragEventArgs e)
        {
            string[] filenames;
            if (GetDragDropFilenames(out filenames, e))
                filename = GetOneValidImage(filenames);
            else
                filename = null;
            return !String.IsNullOrEmpty(filename);
        }


        internal static bool GetDragDropFilenames(out string[] filenames, DragEventArgs e)
        {
            var files = new List<String>();
            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                Array data = ((IDataObject)e.Data).GetData("FileDrop") as Array;
                if (data != null)
                {
                    foreach (var item in data)
                        if (item is String)
                        {
                            if (File.Exists(item as string) || Directory.Exists(item as string))
                                files.Add(item as String);
                        }
                }
            }
            filenames = files.ToArray();
            return filenames.Length > 0;
        }

        internal static string GetOneValidImage(string[] filenames)
        {
            String filename = null;
            if (filenames.Length == 1)
            {
                filename = filenames[0];
                if (File.Exists(filename))
                {
                    string ext = Path.GetExtension(filename).ToLower();
                    if (ext != ".png" && ext != ".jpg" && ext != ".bmp")
                    {
                        filename = null;
                    }
                }
                else { filename = null; }
            }
            return filename;
        }

        internal static string IncrementVersion(string version)
        {
            if (!string.IsNullOrEmpty(version))
            {
                var versions = version.Split(new char[] { '-', '_', '.' });

                int build = -1;
                int.TryParse(versions.Last(), out build);

                version = string.Format("{0}-{1:D4}", string.Join(".", versions, 0, versions.Length - 1), build + 1);
            }
            else
                version = "";

            return version;
        }

        internal static Exception DeleteDirectory(string path)
        {
            Exception succeed = null;

            try
            {
                // Try first a VB.Net delete
                FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
            }
            catch (Exception)
            {
                succeed = DeleteDirectoryRecursive(path);
            }

            return succeed;
        }

        internal static Exception DeleteDirectoryRecursive(string path)
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

        internal static bool CopyDirectory(string strSource, string strDestination, bool overwrite = false)
        {
            var copied = true;
            try
            {
                using (new CWaitCursor())
                {
                    strDestination += "\\";
                    strSource += "\\";
                    if (strDestination.StartsWith(strSource))
                    {
                        MessageBoxEx.Show(Application.OpenForms[0], string.Format("'{0}' cannot be copied into '{1}'", strSource, strDestination), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                            tempfile.CopyTo(Path.Combine(strDestination, tempfile.Name), overwrite);
                        }

                        DirectoryInfo[] directories = dirInfo.GetDirectories();
                        foreach (DirectoryInfo tempdir in directories)
                        {
                            var subfolder = Path.Combine(strSource, tempdir.Name);
                            if (strDestination != subfolder)
                                copied = CopyDirectory(subfolder, Path.Combine(strDestination, tempdir.Name), overwrite);
                            if (!copied) break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                copied = false;
            }

            return copied;
        }

        internal static string FindFileIndex(string[] files, string filename)
        {
            string file = null;
            var index = Array.FindIndex(files, p => p.Equals(filename, StringComparison.CurrentCultureIgnoreCase));
            if (index == 1)
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

        internal static DialogResult ScriptEditor(ScriptInfo script1, ScriptInfo script2, List<Tuple<string, string>> variables)
        {
            string originalScript1 = script1 == null ? null : script1.Code;
            string originalScript2 = script2 == null ? null : script2.Code;

            var editScript = new ScriptForm(script1, script2, variables);
            editScript.StartPosition = FormStartPosition.CenterParent;
            var result = editScript.ShowDialog(Application.OpenForms[0]);

            if ((script1 == null || (script1.Code == originalScript1)) && (script2 == null || (script2.Code == originalScript2)))
                result = DialogResult.Cancel;

            return result;
        }

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        internal static bool IsValidUrl(string Url)
        {
            Uri outUri;
            return (Uri.TryCreate(Url, UriKind.Absolute, out outUri)
                        && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps));
        }

        public static Control FindFocusedControl(Control control)
        {
            var container = control as IContainerControl;
            while (container != null)
            {
                control = container.ActiveControl;
                container = control as IContainerControl;
            }
            return control;
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

        public static string JsonPrettify(string json)
        {
            using (var stringReader = new StringReader(json))
            using (var stringWriter = new StringWriter())
            {
                var jsonReader = new JsonTextReader(stringReader);
                var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                jsonWriter.WriteToken(jsonReader);
                return stringWriter.ToString();
            }
        }
        public static int RunProcessAsAdmin(string command, string parameters)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true; //Must be true to be used with verb = runas
                startInfo.WorkingDirectory = Environment.SystemDirectory;
                startInfo.FileName = command;
                startInfo.Verb = "runas"; //runas is used to be run as administrator
                startInfo.Arguments = parameters;
                startInfo.ErrorDialog = true;
                //startInfo.CreateNoWindow = true; //Require UseShellExecute = false
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                Process process = Process.Start(startInfo);
                process.WaitForExit();
                return process.ExitCode;
            }
            catch (Win32Exception ex)
            {
                switch (ex.NativeErrorCode)
                {
                    case 1223:
                        return ex.NativeErrorCode;
                    default:
                        return -1;
                }

            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Gets whether the specified path is a valid absolute file path.
        /// </summary>
        /// <param name="path">Any path. OK if null or empty.</param>
        static public bool IsValidPath(string path)
        {
            Regex r = new Regex(@"^(([a-zA-Z]:)|(\))(\{1}|((\{1})[^\]([^/:*?<>""|]*))+)$");
            return r.IsMatch(path);
        }

        internal static void DeleteFile(string fullName)
        {
            try
            {
                if (File.Exists(fullName))
                    // Try to send deleted SPK to RecycleBin 
                    FileSystem.DeleteFile(fullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
            }
            catch
            {
                File.Delete(fullName);
            }
        }

        public static double SetSizedImage(PictureBox picture, Image image)
        {
            Bitmap bmp = null;
            double scale = 0;
            Graphics g;

            if (picture.Tag == null || (picture.Tag as Image) != null)
                picture.Tag = image;

            if (image != null)
            {
                // Scale:
                double scaleY = (double)image.Width / picture.Width;
                double scaleX = (double)image.Height / picture.Height;
                scale = scaleY < scaleX ? scaleX : scaleY;

                if (scale < 1) scale = 1;

                // Create new bitmap:
                bmp = new Bitmap(
                    (int)((double)image.Width / scale),
                    (int)((double)image.Height / scale));

                // Set resolution of the new image:
                bmp.SetResolution(
                    image.HorizontalResolution,
                    image.VerticalResolution);

                // Create graphics:
                g = Graphics.FromImage(bmp);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Draw the new image:
                g.DrawImage(
                    image,
                    new Rectangle(            // Destination
                        0, 0,
                        bmp.Width, bmp.Height),
                    new Rectangle(            // Source
                        0, 0,
                        image.Width, image.Height),
                    GraphicsUnit.Pixel);

                // Release the resources of the graphics:
                g.Dispose();

                picture.Image = bmp;
            }

            return scale;
        }


        /// <summary>
        /// Crops an image according to a selection rectangel
        /// </summary>
        /// <param name="image">
        /// the image to be cropped
        /// </param>
        /// <param name="selection">
        /// the selection
        /// </param>
        /// <returns>
        /// cropped image
        /// </returns>
        public static Image Crop(this Image source, Rectangle selection)
        {
            Bitmap bmp = source as Bitmap;
            Bitmap copy = null;

            // Check if it is a bitmap:
            if (bmp != null)
            {
                if (selection.Width < 0)
                    selection = new Rectangle(selection.X + selection.Width, selection.Y, Math.Abs(selection.Width), selection.Height);
                if (selection.Height < 0)
                    selection = new Rectangle(selection.X, selection.Y + selection.Height, selection.Width, Math.Abs(selection.Height));

                using (MagickImage image = new MagickImage(bmp))
                {
                    copy = new Bitmap(selection.Width, selection.Height);

                    image.Format = MagickFormat.Png;
                    image.Crop(selection.X, selection.Y, selection.Width, selection.Height);

                    using (Graphics g = Graphics.FromImage(copy))
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(image.ToBitmap(), 0, 0, copy.Width, copy.Height);
                    }
                }
            }

            return copy;
        }

        public static Image ResizeImage(Image image, int width, int height)
        {
            var copy = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(copy))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, width, height);
            }

            return copy;
        }

        public static bool CreateSymLink(string link, string target, bool isDirectory)
        {
            return RunProcessAsAdmin("cmd.exe", string.Format(@"/c mklink {0}""{1}"" ""{2}""", isDirectory ? "/D " : "", link, target)) == 0;
        }

        public static bool IsFile(string path)
        {
            var isFile = false;
            if (File.Exists(path))
            {
                // get the file attributes for file or directory
                FileAttributes attr = File.GetAttributes(path);

                //detect whether its a directory or file
                isFile = !((attr & FileAttributes.Directory) == FileAttributes.Directory);
            }

            return isFile;
        }

        public static bool IsDirectory(string path)
        {
            var isDirectory = false;
            if (Directory.Exists(path))
            {
                // get the file attributes for file or directory
                FileAttributes attr = File.GetAttributes(path);

                //detect whether its a directory or file
                isDirectory = ((attr & FileAttributes.Directory) == FileAttributes.Directory);
            }

            return isDirectory;
        }

        public static void LoadDSMReleases(TextBox box)
        {
            var dsmReleases = Path.Combine(Helper.ResourcesDirectory, "dsm_releases");
            var content = File.ReadAllText(dsmReleases);
            var versions = Regex.Split(content, "\r\n|\r|\n");

            box.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            foreach (var version in versions)
            {
                if (getFirmwareVersion.IsMatch(version))
                {
                    var match = getFirmwareVersion.Match(version);
                    var firmware = String.Format("{0}.{1}-{2}", match.Groups[1].Value, match.Groups[2].Value, match.Groups[4].Value);
                    if (!box.AutoCompleteCustomSource.Contains(firmware))
                        box.AutoCompleteCustomSource.Add(firmware);
                }
            }
        }

        public static Stream GetStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static bool CheckDSMVersionMin(SortedDictionary<string, string> info, int minMajor, int minMinor, int minBuild)
        {
            int major, minor, build;
            GetFirmwareMajorMinor(info, out major, out minor, out build);

            return (major > minMajor || (major == minMajor && minor > minMinor) || (major == minMajor && minor == minMinor && build >= minBuild));
        }

        private static void GetFirmwareMajorMinor(SortedDictionary<string, string> info, out int major, out int minor, out int build)
        {
            string firmware = "2.0";
            if (info.ContainsKey("os_min_ver"))
                firmware = info["os_min_ver"];
            else
            if (info.ContainsKey("firmware"))
                firmware = info["firmware"];

            if (!getFirmwareVersion.IsMatch(firmware))
                firmware = "2.0";
            var match = getFirmwareVersion.Match(firmware);

            major = int.Parse(match.Groups[1].Value);
            minor = int.Parse(match.Groups[2].Value);
            build = int.Parse(match.Groups[4].Value);
        }

        public static bool CheckDSMVersionMax(SortedDictionary<string, string> info, int maxMajor, int maxMinor, int maxBuild)
        {
            int major, minor, build;
            GetFirmwareMajorMinor(info, out major, out minor, out build);

            return (major < maxMajor || (major == maxMajor && minor < maxMinor) || (major == maxMajor && minor == maxMinor && build <= maxBuild));
        }

        public static void ValidateFirmware(TextBox sender, CancelEventArgs e, ErrorProvider errorProvider)
        {
            if (errorProvider.Tag == null && sender != null)
            {
                if (sender.Text != "")
                {
                    if (Helper.getOldFirmwareVersion.IsMatch(sender.Text))
                    {
                        var parts = sender.Text.Split('.');
                        sender.Text = string.Format("{0}.{1}-{2:D4}", parts[0], parts[1], int.Parse(parts[2]));
                    }
                    if (Helper.getShortFirmwareVersion.IsMatch(sender.Text))
                    {
                        var parts = sender.Text.Split('.');
                        sender.Text = string.Format("{0}.{1}-0000", parts[0], parts[1]);
                    }
                    if (!Helper.getFirmwareVersion.IsMatch(sender.Text))
                    {
                        e.Cancel = true;
                        sender.Select(0, sender.Text.Length);
                        errorProvider.SetError(sender, "The format of a firmware must be like 0.0-0000");
                    }
                    else
                    {
                        var parts = sender.Text.Split(new char[] { '.', '-' });
                        if (int.Parse(parts[2]) == 0)
                            sender.Text = string.Format("{0}.{1}", parts[0], parts[1]);
                        else
                            sender.Text = string.Format("{0}.{1}-{2:D4}", parts[0], parts[1], int.Parse(parts[2]));
                    }
                }
            }
        }

        public static string Unquote(string text)
        {
            if (text != null)
            {
                if (text.StartsWith("\"")) text = text.Substring(1);
                if (text.EndsWith("\"")) text = text.Substring(0, text.Length - 1);
            }
            return text;
        }
    }
}

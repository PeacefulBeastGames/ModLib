using System;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using Tomlet;

namespace PeacefulBeast.ModLib.Zip
{
    public static class Zip
    {
        /// <summary>
        /// Load a .toml file from a zip file
        /// </summary>
        /// <param name="zipPath">zip file path</param>
        /// <param name="tomlPath">path to the .toml inside the zip file</param>
        /// <exception cref="FileNotFoundException"></exception>
        public static T LoadToml<T>(string zipPath, string tomlPath)
        {
            using var zipFile = new ZipFile(zipPath);
            var entry = zipFile.GetEntry(tomlPath);
            
            if (entry == null) 
                throw new FileNotFoundException("Could not find specified file in zip archive", tomlPath);

            using var zipStream = zipFile.GetInputStream(entry);
            using var reader = new StreamReader(zipStream);
            return TomletMain.To<T>(reader.ReadToEnd());
        }
        
        
        
        
        
        
        
        
        /// <summary>
        /// GitHub copilot output
        /// </summary>
        public static void ZipFile(string sourceFile, string destinationFile)
        {
            using (var zipStream = new ZipOutputStream(System.IO.File.Create(destinationFile)))
            {
                zipStream.SetLevel(9);
                var buffer = new byte[4096];
                var entry = new ZipEntry(Path.GetFileName(sourceFile));
                zipStream.PutNextEntry(entry);
                using (var fileStream = System.IO.File.OpenRead(sourceFile))
                {
                    int sourceBytes;
                    do
                    {
                        sourceBytes = fileStream.Read(buffer, 0, buffer.Length);
                        zipStream.Write(buffer, 0, sourceBytes);
                    } while (sourceBytes > 0);
                }
            }
        }
        
        /// <summary>
        /// Extracts the content from a .zip file inside an specific folder.
        /// </summary>
        public static void ExtractZipContent(string FileZipPath, string password, string OutputFolder)
        {
            ZipFile file = null;
            try
            {
                FileStream fs = System.IO.File.OpenRead(FileZipPath);
                file = new ZipFile(fs);
        
                if (!string.IsNullOrEmpty(password))
                {
                    // AES encrypted entries are handled automatically
                    file.Password = password;
                }
        
                foreach (ZipEntry zipEntry in file)
                {
                    if (!zipEntry.IsFile)
                    {
                        // Ignore directories
                        continue;           
                    }
        
                    string entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.
        
                    // 4K is optimum
                    byte[] buffer = new byte[4096];     
                    Stream zipStream = file.GetInputStream(zipEntry);
        
                    // Manipulate the output filename here as desired.
                    string fullZipToPath = Path.Combine(OutputFolder, entryFileName);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);
        
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }
        
                    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                    // of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using (FileStream streamWriter = System.IO.File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            }
            finally
            {
                if (file != null)
                {
                    file.IsStreamOwner = true; // Makes close also shut the underlying stream
                    file.Close(); // Ensure we release resources
                }
            }
        }
    }
}
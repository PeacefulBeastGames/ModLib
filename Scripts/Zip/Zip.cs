using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace PeacefulBeast.ModLib.Zip
{
    public static class Zip
    {
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
    }
}
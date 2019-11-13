using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace P6.ZippingSlicedFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceFile = "..//..//..//source//sliceMe.mp4";
            string destinationDir = @"..//..//..//destination\";
            double parts = 5;
            Slice(sourceFile, destinationDir, parts);

            List<string> files = new List<string>();
            for (int i = 0; i < parts; i++)
            {
                files.Add(destinationDir + $"Part-{i}.gz");
            }
            Assemble(files, destinationDir);
        }

        public static void Assemble(List<string> files, string destination)
        {
            var writer = new FileStream(destination + "assembled.mp4", FileMode.Create);
            using (writer)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    var reader = new FileStream(files[i], FileMode.Open);
                    using (reader)
                    {
                        byte[] buffer = new byte[4096];
                        using (GZipStream gz = new GZipStream(reader, CompressionMode.Decompress, false))
                        {
                            while (true)
                            {
                                int readBytes = gz.Read(buffer, 0, buffer.Length);
                                if (readBytes == 0)
                                {
                                    break;
                                }
                                writer.Write(buffer, 0, readBytes);
                            }
                        }
                    }
                }
            }
        }

        public static void Slice(string source, string destination, double parts)
        {
            var reader = new FileStream(source, FileMode.Open);
            using (reader)
            {
                for (int i = 0; i < parts; i++)
                {
                    byte[] buffer = new byte[(int)Math.Ceiling(reader.Length / parts)];
                    int readBytes = reader.Read(buffer, 0, buffer.Length);

                    using (GZipStream gz = new GZipStream(new FileStream(destination + $"Part-{i}.gz", FileMode.Create), CompressionMode.Compress, false))
                    {
                        gz.Write(buffer, 0, readBytes);
                    }
                }
            }
        }
    }
}



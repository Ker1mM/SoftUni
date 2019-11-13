using System;
using System.IO;

namespace P4.CopyBinaryFile
{
    class Program
    {
        static void Main(string[] args)
        {
            var originalFile =
                new FileStream(@"D:\Projects\csharp-fundamentals\CSharpAdvanced\StreamsAndFilesExercises\copyMe.png", FileMode.Open);

            using (originalFile)
            {
                var copy = new FileStream(
                    @"D:\Projects\csharp-fundamentals\CSharpAdvanced\StreamsAndFilesExercises\P4.CopyBinaryFile\copyOfcopyMe.png", FileMode.Create);
                using (copy)
                {
                    byte[] buffer = new byte[4096];

                    while (true)
                    {
                        int readBytes = originalFile.Read(buffer, 0, buffer.Length);
                        if (readBytes == 0)
                        {
                            break;
                        }
                        copy.Write(buffer, 0, readBytes);
                    }
                }
            }
        }
    }
}

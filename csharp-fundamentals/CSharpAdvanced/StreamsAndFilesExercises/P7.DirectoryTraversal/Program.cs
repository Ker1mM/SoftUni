using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace P7.DirectoryTraversal
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = "../../../../";

            string[] filePaths = Directory.GetFiles(directory, "*.*",
                                         SearchOption.TopDirectoryOnly);
            filePaths = filePaths.Select(x => x.Replace(directory, "")).ToArray();

            Dictionary<string, List<KeyValuePair<string, double>>> files = new Dictionary<string, List<KeyValuePair<string, double>>>();

            foreach (var file in filePaths)
            {
                int dotIndex = file.LastIndexOf('.');
                string extension = file.Substring(dotIndex, file.Length - dotIndex);
                double size = 0;
                var reader = new FileStream(directory + file, FileMode.Open);
                using (reader)
                {
                    size = reader.Length / 1024.0;
                }

                if (!files.ContainsKey(extension))
                {
                    files.Add(extension, new List<KeyValuePair<string, double>>());
                }
                files[extension].Add(new KeyValuePair<string, double>(file, size));
            }

            files = files
                .OrderByDescending(x => x.Value.Count)
                .ThenBy(x => x.Key)
                .ToList()
                .ToDictionary(x => x.Key, x => x.Value);

            var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            StreamWriter writer = new StreamWriter(desktopFolder + @"\result.txt");
            using (writer)
            {
                foreach (var item in files)
                {
                    writer.WriteLine(item.Key);
                    //Console.WriteLine(item.Key);
                    foreach (var fileName in item.Value.OrderBy(x => x.Value))
                    {
                        writer.WriteLine("--{0} - {1:0.###}kb", fileName.Key, fileName.Value);
                        //Console.WriteLine("--{0} - {1:0.###}kb", fileName.Key, fileName.Value);
                    }
                }
            }
        }
    }
}

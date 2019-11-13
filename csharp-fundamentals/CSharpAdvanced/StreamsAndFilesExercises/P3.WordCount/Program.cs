using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace P3.WordCount
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] words;
            using (var reader = new StreamReader("../../../../words.txt"))
            {
                words = reader.ReadToEnd().Split("\r\n");
            }

            string text;
            using (var reader = new StreamReader("../../../../text.txt"))
            {
                text = reader.ReadToEnd();
            }

            Dictionary<string, int> count = new Dictionary<string, int>();

            foreach (var word in words)
            {
                string pattern = @"(?<=\W)(" + word.ToLower() + @")(?=\W)";
                if (!count.ContainsKey(word))
                {
                    count.Add(word, 0);
                }
                count[word] += Regex.Matches(text.ToLower(), pattern).Count;
            }

            count = count
                .OrderByDescending(x => x.Value)
                .ToList()
                .ToDictionary(x => x.Key, x => x.Value);

            using (var writer = new StreamWriter("../../../result.txt"))
            {
                foreach (var item in count)
                {
                    writer.WriteLine("{0} - {1}", item.Key, item.Value);
                }
            }
        }
    }
}

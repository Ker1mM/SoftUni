using System;
using System.IO;

namespace P01.OddLines
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new StreamReader("../../../text.txt");

            using (reader)
            {
                var text = reader.ReadToEnd();
                var tokens = text.Split("\n");
                for (int i = 0; i < tokens.Length; i++)
                {
                    if (i % 2 != 0)
                    {
                        Console.WriteLine(tokens[i]);
                    }
                }
                reader.Close();
            }
        }
    }
}

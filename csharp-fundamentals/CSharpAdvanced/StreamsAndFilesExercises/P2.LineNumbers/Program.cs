using System;
using System.IO;

namespace P2.LineNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader reader = new StreamReader("../../../../text.txt"))
            {
                string nextLine;
                var writer = new StreamWriter("../../../output.txt");
                int counter = 1;
                while ((nextLine = reader.ReadLine()) != null)
                {
                    writer.WriteLine("Line {0}:{1}", counter, nextLine);
                    counter++;
                }
                reader.Close(); //I know "using" does this, but I do it anyway.
                writer.Close();
            }
        }
    }
}

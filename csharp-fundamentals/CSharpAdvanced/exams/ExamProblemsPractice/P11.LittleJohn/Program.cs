using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace P11.LittleJohn
{
    class Program
    {
        static void Main(string[] args)
        {
            string largePattern = @">>>----->>";
            string mediumPattern = @">>----->";
            string smallPattern = @">----->";

            int smallCount = 0;
            int mediumCount = 0;
            int largeCount = 0;

            for (int i = 0; i < 4; i++)
            {
                string input = Console.ReadLine();

                MatchCollection large = Regex.Matches(input, largePattern);
                input = Regex.Replace(input, largePattern, " ");
                MatchCollection medium = Regex.Matches(input, mediumPattern);
                input = Regex.Replace(input, mediumPattern, " ");
                MatchCollection small = Regex.Matches(input, smallPattern);

                smallCount += small.Count;
                mediumCount += medium.Count;
                largeCount += large.Count;
            }

            string initialNumber = smallCount + "" + mediumCount + largeCount;
            string initialBinary = Convert.ToString(int.Parse(initialNumber), 2);
            string reversedBinary = new string(initialBinary.ToCharArray().Reverse().ToArray());
            string finalBinary = initialBinary + reversedBinary;
            long result = Convert.ToInt64(finalBinary, 2);
            Console.WriteLine(result);
        }
    }
}

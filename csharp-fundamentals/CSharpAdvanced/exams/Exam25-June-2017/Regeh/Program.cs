using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Regeh
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = @"\[[^\[]+<([0-9]+)REGEH([0-9]+)>[^\]]+\]";

            string input = Console.ReadLine();

            MatchCollection rgx = Regex.Matches(input, pattern);
            StringBuilder sb = new StringBuilder();
            int index = 0;
            foreach (Match match in rgx)
            {
                int index1 = int.Parse(match.Groups[1].ToString());
                index += index1;
                if (index >= input.Length)
                {
                    sb.Append(input[index % input.Length]);
                }
                else
                {
                    sb.Append(input[index]);
                }
                int index2 = int.Parse(match.Groups[2].ToString());
                index += index2;
                if (index >= input.Length)
                {
                    sb.Append(input[index % input.Length]);
                }
                else
                {
                    sb.Append(input[index]);
                }
            }

            Console.WriteLine(sb);
        }
    }
}

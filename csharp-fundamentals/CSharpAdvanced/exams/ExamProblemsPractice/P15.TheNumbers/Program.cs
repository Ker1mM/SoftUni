using System;
using System.Text;
using System.Text.RegularExpressions;

namespace P15.TheNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            MatchCollection numbers = Regex.Matches(input, @"[0-9]+");
            StringBuilder sb = new StringBuilder();

            foreach (Match number in numbers)
            {
                if (sb.Length != 0)
                {
                    sb.Append("-");
                }
                string hexValue = int.Parse(number.ToString()).ToString("X");
                sb.Append("0x");
                if (hexValue.Length < 4)
                {
                    sb.Append(new string('0', 4 - hexValue.Length));
                }
                sb.Append(hexValue);
            }
            Console.WriteLine(sb);
        }
    }
}

using System;
using System.Text.RegularExpressions;

namespace P13.SumOfAllValues
{
    class Program
    {
        static void Main(string[] args)
        {
            string startKeyPattern = @"(^[a-zA-Z_]+)(?=[0-9])";
            string endKeyPattern = @"(?<=[0-9])([a-zA-Z_]+$)";

            string keys = Console.ReadLine();
            string text = Console.ReadLine();

            Match startKey = Regex.Match(keys, startKeyPattern);
            Match endKey = Regex.Match(keys, endKeyPattern);

            if (startKey.Success && endKey.Success)
            {
                double totalValue = 0;
                string textPattern = "(" + startKey.ToString() + ")(?<number>.*?)(" + endKey.ToString() + ")";

                MatchCollection numbers = Regex.Matches(text, textPattern);

                foreach (Match number in numbers)
                {
                    if (Double.TryParse(number.Groups["number"].ToString(), out double result))
                    {
                        totalValue += result;
                    }
                }
                if (totalValue > 0)
                {
                    Console.WriteLine("<p>The total value is: <em>{0}</em></p>", totalValue);
                }
                else
                {
                    Console.WriteLine("<p>The total value is: <em>nothing</em></p>");
                }
            }
            else
            {
                Console.WriteLine("<p>A key is missing</p>");
            }
        }
    }
}

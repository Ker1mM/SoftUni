using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace P12.PhoneNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = @"(?<name>(?<=[^a-zA-Z]|^)[A-Z][a-zA-Z]*)([^a-zA-Z\+]*?)(?<number>\+?[0-9]+[()/.\-\s]*[0-9]+[()/.\- 0-9]*)";
            List<KeyValuePair<string, string>> numbers = new List<KeyValuePair<string, string>>();
            string input;
            StringBuilder sb = new StringBuilder();
            while ((input = Console.ReadLine()) != "END")
            {
                sb.Append(input);
            }
            MatchCollection matches = Regex.Matches(sb.ToString(), pattern);

            foreach (Match item in matches)
            {
                string name = item.Groups["name"].ToString();
                string number = new string(
                    item.Groups["number"]
                    .ToString()
                    .ToCharArray()
                    .Where(x => Char.IsDigit(x) || x == '+')
                    .ToArray());

                numbers.Add(new KeyValuePair<string, string>(name, number));
            }

            if (numbers.Count > 0)
            {
                Console.Write("<ol>");
                for (int i = 0; i < numbers.Count; i++)
                {
                    Console.Write("<li><b>{0}:</b> {1}</li>", numbers[i].Key, numbers[i].Value);
                }
                Console.Write("</ol>");
            }
            else
            {
                Console.WriteLine("<p>No matches!</p>");
            }
        }
    }
}

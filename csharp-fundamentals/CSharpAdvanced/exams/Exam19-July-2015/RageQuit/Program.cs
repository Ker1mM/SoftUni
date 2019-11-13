using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RageQuit
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine().ToUpper();
            string pattern = @"(?<text>[^0-9]{1,20})(?<count>[1-9][0-9]{0,1}|0[1-9])";
            MatchCollection messages = Regex.Matches(input, pattern);
            StringBuilder result = new StringBuilder();
            StringBuilder symbols = new StringBuilder();
            foreach (Match message in messages)
            {
                string text = message.Groups["text"].ToString();
                int count = int.Parse(message.Groups["count"].ToString());
                symbols.Append(text);
                result.Append(new StringBuilder().Insert(0, text, count));
            }

            int uniqueSymbols = UniqueSymbolCount(symbols.ToString());
            Console.WriteLine("Unique symbols used: {0}", uniqueSymbols);
            Console.WriteLine(result);
        }

        static int UniqueSymbolCount(string text)
        {
            int result = 0;
            while (text.Length > 0)
            {
                if (!char.IsDigit(text[0]))
                    result++;
                text = text.Replace(text[0].ToString(), string.Empty);
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TextTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            string input;
            while ((input = Console.ReadLine()) != "burp")
            {
                sb.Append(input);
            }
            string text = sb.ToString();
            text = Regex.Replace(text, @"\s+", " ");

            string pattern = @"(\$|%|&|')([^$%&']+)\1";
            List<string> correctedWords = new List<string>();
            MatchCollection matches = Regex.Matches(text, pattern);
            foreach (Match item in matches)
            {
                string word = item.Groups[2].ToString();
                char[] wordArray = word.ToCharArray();
                int symbol = GetCharWeight(item.Groups[1].ToString()[0]);
                for (int i = 0; i < wordArray.Length; i++)
                {
                    {
                        if (i % 2 == 0)
                        {
                            wordArray[i] = (char)(wordArray[i] + symbol);
                        }
                        else
                        {
                            wordArray[i] = (char)(wordArray[i] - symbol);
                        }
                    }
                }
                correctedWords.Add(new string(wordArray));
            }
            Console.WriteLine(String.Join(" ", correctedWords));
        }

        public static int GetCharWeight(char symbol)
        {
            switch (symbol)
            {
                case '$':
                    return 1;
                case '%':
                    return 2;
                case '&':
                    return 3;
                case '\'':
                    return 4;
                default:
                    return 0;

            }
        }
    }
}

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace P20.OhMyGirl_
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = Console.ReadLine();
            StringBuilder sb = new StringBuilder();
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                sb.Append(input);
            }

            string pattern = KeyPatternBuilder(key);
            string matchPattern = pattern + "(.{2,6})" + pattern;

            MatchCollection matches = Regex.Matches(sb.ToString(), matchPattern);

            sb.Clear();
            foreach (Match item in matches)
            {
                sb.Append(item.Groups[1].ToString());
            }
            Console.WriteLine(sb.ToString());
        }

        public static string KeyPatternBuilder(string key)
        {
            StringBuilder keyPattern = new StringBuilder();
            keyPattern.Append(Regex.Escape(key[0].ToString()));
            for (int i = 1; i < key.Length - 1; i++)
            {
                if (Char.IsLetter(key[i]))
                {
                    if (Char.IsUpper(key[i]))
                    {
                        keyPattern.Append("[A-Z]*");
                    }
                    else
                    {
                        keyPattern.Append("[a-z]*");
                    }
                }
                else if (Char.IsDigit(key[i]))
                {
                    keyPattern.Append("[0-9]*");
                }
                else
                {
                    keyPattern.Append(Regex.Escape(key[i].ToString()));
                }
            }
            keyPattern.Append(Regex.Escape(key[key.Length - 1].ToString()));
            return keyPattern.ToString();
        }
    }
}

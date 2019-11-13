using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace P09.UppercaseWords
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = @"(?<=[^A-Za-z]|^)[A-Z]+(?=[^A-Za-z]|$)";

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                MatchCollection matches = Regex.Matches(input, pattern);

                foreach (Match item in matches)
                {
                    string currentWord = item.ToString();
                    string reversedWord = GetReversed(currentWord);
                    string replacePattern = "(?<=[^a-zA-Z]|^)(" + currentWord + ")(?=[^a-zA-Z]|$)";
                    input = Regex.Replace(input, replacePattern, reversedWord);
                }
                Console.WriteLine(System.Security.SecurityElement.Escape(input));
            }
        }

        public static string GetReversed(string word)
        {
            char[] temp = word.ToCharArray();
            temp = temp.Reverse().ToArray();
            string reversedWord = new string(temp);

            if (word == reversedWord)
            {
                StringBuilder sb = new StringBuilder();
                foreach (char letter in word)
                {
                    sb.Append(letter, 2);
                }
                reversedWord = sb.ToString();
            }
            return reversedWord;
        }
    }
}

using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BasicMarkUpLanguage
{
    class Program
    {
        static void Main(string[] args)
        {
            string patttern = @"<\s*([a-z]*)\s+.*?\s*content\s*=\s*\""([^\""]+)\""";

            string input;
            int counter = 1;
            while ((input = Console.ReadLine()) != "<stop/>")
            {
                Match rgx = Regex.Match(input, patttern);
                if (rgx.Success)
                {
                    string command = rgx.Groups[1].ToString();
                    string word = rgx.Groups[2].ToString();

                    switch (command)
                    {
                        case "inverse":
                            Console.WriteLine("{0}. {1}", counter, Inverse(word));
                            counter++;
                            break;
                        case "reverse":
                            Console.WriteLine("{0}. {1}", counter, Reverse(word));
                            counter++;
                            break;
                        case "repeat":
                            string valuePattern = @"value\s*=\s*""([0-9]+)""";
                            string temp = Regex.Match(input, valuePattern).Groups[1].ToString();
                            int value = int.Parse(temp);
                            for (int i = 0; i < value; i++)
                            {
                                Console.WriteLine("{0}. {1}", counter, word);
                                counter++;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static string Reverse(string word)
        {
            string result = new string(word.ToCharArray().Reverse().ToArray());
            return result;
        }

        public static string Inverse(string word)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var letter in word)
            {
                if (Char.IsLower(letter))
                {
                    sb.Append(Char.ToUpper(letter));
                }
                else if (Char.IsUpper(letter))
                {
                    sb.Append(Char.ToLower(letter));
                }
                else
                {
                    sb.Append(letter);
                }
            }
            return sb.ToString();
        }
    }
}

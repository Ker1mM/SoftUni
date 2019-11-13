using System;
using System.Text;
using System.Text.RegularExpressions;

namespace P03.ExtractHyperlinks
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            StringBuilder sb = new StringBuilder();
            while ((input = Console.ReadLine()) != "END")
            {
                sb.Append(input);
            }

            string pattern = @"(?<=<a[^<>]*?href\s*=)([^<>]+)(?=\s|>)";

            MatchCollection matches = Regex.Matches(sb.ToString(), pattern);

            foreach (Match item in matches)
            {
                string current = item.ToString().Trim();
                StringBuilder result = new StringBuilder();
                if (current[0] == '"')
                {
                    for (int i = 1; i < current.Length; i++)
                    {
                        if (current[i] != '"')
                        {
                            result.Append(current[i]);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else if (current[0] == '\'')
                {
                    for (int i = 1; i < current.Length; i++)
                    {
                        if (current[i] != '\'')
                        {
                            result.Append(current[i]);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < current.Length; i++)
                    {
                        if (current[i] != ' ')
                        {
                            result.Append(current[i]);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                Console.WriteLine(result.ToString());
            }
        }
    }
}

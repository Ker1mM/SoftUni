using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace P05.SemanticHTML
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;

            List<string> result = new List<string>();

            while ((input = Console.ReadLine()) != "END")
            {
                string pattern = "";
                if (Regex.IsMatch(input, @"<\s*div"))
                {
                    pattern = "((\\s*(class|id)\\s*=\\s*\"\\s*(?<header>[a-z]+)\\s*\"))";
                    Match toReplace = Regex.Match(input, pattern);
                    input = Regex.Replace(input, pattern, "");
                    input = input.Replace("div", toReplace.Groups["header"].ToString());
                    input = TrimExcessSpaces(input);
                }
                else if (Regex.IsMatch(input, @"<\s*/\s*div"))
                {
                    pattern = "\\s*<\\s*!--\\s*(?<header>[a-z]+)\\s*--\\s*>\\s*";
                    Match toReplace = Regex.Match(input, pattern);
                    input = Regex.Replace(input, pattern, "");
                    input = input.Replace("div", toReplace.Groups["header"].ToString());
                    input = TrimExcessSpaces(input);
                }

                result.Add(input);
            }

            Console.WriteLine(string.Join("\n", result));
        }

        public static string TrimExcessSpaces(string line)
        {
            line = Regex.Replace(line, "(?<=<.*)(\\s{2,})", " ");
            line = Regex.Replace(line, "(?<=\")(\\s +)(?=[a - z] +\\s * \")|(?<=\"\\s *[a - z] +)(\\s +)(?= \")", "");
            line = Regex.Replace(line, "(?<=<)(\\s+)|(\\s+)(?=>)", "");

            return line;
        }
    }
}

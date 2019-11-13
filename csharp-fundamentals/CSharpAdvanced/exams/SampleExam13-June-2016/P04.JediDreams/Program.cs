using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace P04.JediDreams
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputLines = int.Parse(Console.ReadLine());

            Dictionary<string, List<string>> methods = new Dictionary<string, List<string>>();

            string methodPattern = @"(?<=static\s+.*?\s+)([a-zA-Z]*[A-Z][a-zA-Z]*)(?=\s*\()";
            string invokedMethodPattern = @"([a-zA-Z]*[A-Z][a-zA-Z]*)(?=\s*\()";

            Regex mRgx = new Regex(methodPattern);
            Regex imRgx = new Regex(invokedMethodPattern);

            string lastMethodName = "";
            for (int i = 0; i < inputLines; i++)
            {
                string input = Console.ReadLine();

                Match methodMatch = mRgx.Match(input);
                MatchCollection invokedMatches = imRgx.Matches(input);

                if (methodMatch.Success)
                {
                    lastMethodName = methodMatch.ToString();
                    methods.Add(lastMethodName, new List<string>());
                }
                else if (invokedMatches.Count > 0)
                {
                    foreach (Match currentMatch in invokedMatches)
                    {
                        methods[lastMethodName].Add(currentMatch.ToString());
                    }
                }
            }

            methods = methods
                .OrderByDescending(x => x.Value.Count)
                .ThenBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var item in methods)
            {
                Console.Write("{0} -> ", item.Key);
                if (item.Value.Count > 0)
                {
                    Console.WriteLine("{0} -> {1}", item.Value.Count, String.Join(", ", item.Value.OrderBy(x => x)));
                }
                else
                {
                    Console.WriteLine("None");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OlympicsAreComing
{
    class Program
    {
        static void Main(string[] args)
        {

            var wins = new Dictionary<string, int>();
            var participants = new Dictionary<string, HashSet<string>>();
            string input;
            while ((input = Console.ReadLine()) != "report")
            {
                input = Regex.Replace(input, @"\s{2,}", " ");
                string[] tokens = input.Split('|');
                string name = tokens[0].Trim();
                string country = tokens[1].Trim();

                if (!wins.ContainsKey(country))
                {
                    wins.Add(country, 0);
                    participants.Add(country, new HashSet<string>());
                }

                wins[country]++;
                participants[country].Add(name);
            }

            wins = wins
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var country in wins)
            {
                Console.WriteLine($"{country.Key} ({participants[country.Key].Count} participants): {country.Value} wins");
            }
        }
    }
}

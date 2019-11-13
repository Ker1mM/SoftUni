using System;
using System.Collections.Generic;
using System.Linq;

namespace Ranking
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> passwords = new Dictionary<string, string>();

            string input;
            while ((input = Console.ReadLine()) != "end of contests")
            {
                string[] tokens = input.Split(":");

                if (!passwords.ContainsKey(tokens[0]))
                {
                    passwords.Add(tokens[0], tokens[1]);
                }
            }

            Dictionary<string, Dictionary<string, int>> candidates = new Dictionary<string, Dictionary<string, int>>();

            while ((input = Console.ReadLine()) != "end of submissions")
            {
                string[] tokens = input.Split("=>");
                string contest = tokens[0];
                string password = tokens[1];
                string username = tokens[2];
                int points = int.Parse(tokens[3]);

                if (passwords.ContainsKey(contest) && passwords[contest] == password)
                {
                    if (!candidates.ContainsKey(username))
                    {
                        candidates.Add(username, new Dictionary<string, int>());
                    }

                    if (!candidates[username].ContainsKey(contest))
                    {
                        candidates[username].Add(contest, points);
                    }
                    else
                    {
                        if (points > candidates[username][contest])
                        {
                            candidates[username][contest] = points;
                        }
                    }
                }
            }

            var bestCandidate = candidates
                .OrderByDescending(x => x.Value.Values.Sum())
                .First();

            Console.WriteLine("Best candidate is {0} with total {1} points.",
                bestCandidate.Key, bestCandidate.Value.Values.Sum());
            Console.WriteLine("Ranking:");
            foreach (var current in candidates.OrderBy(x => x.Key))
            {
                Console.WriteLine(current.Key);
                foreach (var currentUser in current.Value.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine("#  {0} -> {1}", currentUser.Key, currentUser.Value);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace ChampionsLeague
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            var teams = new Dictionary<string, int>();
            var opponents = new Dictionary<string, List<string>>();
            while ((input = Console.ReadLine()) != "stop")
            {
                string[] tokens = input.Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);

                string team1 = tokens[0];
                string team2 = tokens[1];

                if (!opponents.ContainsKey(team1))
                {
                    opponents.Add(team1, new List<string>());
                }
                opponents[team1].Add(team2);
                if (!opponents.ContainsKey(team2))
                {
                    opponents.Add(team2, new List<string>());
                }
                opponents[team2].Add(team1);
                if (!teams.ContainsKey(team1))
                {
                    teams.Add(team1, 0);
                }
                if (!teams.ContainsKey(team2))
                {
                    teams.Add(team2, 0);
                }

                int[] firstMatchGoals = tokens[2].Split(':').Select(int.Parse).ToArray();
                int[] secondMatchGoals = tokens[3].Split(':').Select(int.Parse).ToArray();

                int team1Goals = firstMatchGoals[0] + secondMatchGoals[1];
                int team2Goals = firstMatchGoals[1] + secondMatchGoals[0];

                if (team1Goals == team2Goals)
                {
                    if (firstMatchGoals[1] > secondMatchGoals[1])
                    {
                        teams[team2]++;
                    }
                    else
                    {
                        teams[team1]++;
                    }
                }
                else
                {
                    if (team1Goals > team2Goals)
                    {
                        teams[team1]++;
                    }
                    else
                    {
                        teams[team2]++;
                    }
                }
            }

            teams = teams
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var team in teams)
            {
                Console.WriteLine($"{team.Key}");
                Console.WriteLine($"- Wins: {team.Value}");
                Console.WriteLine("- Opponents: {0}", String.Join(", ", opponents[team.Key].OrderBy(x => x)));
            }
        }
    }
}

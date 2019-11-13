using System;
using System.Collections.Generic;
using System.Linq;

namespace P6_FootballTeamGenerator
{
    public class Team
    {
        private string name;
        private List<Player> players;

        public IReadOnlyCollection<Player> Players
        {
            get
            {
                return this.players.AsReadOnly();
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value == null || value == "" || value == " ")
                {
                    throw new ArgumentException("A name should not be empty.");
                }
                else
                {
                    this.name = value;
                }
            }
        }

        public Team(string name)
        {
            this.Name = name;
            this.players = new List<Player>();
        }

        public int Rating
        {
            get
            {
                if (this.players.Count == 0)
                {
                    return 0;
                }
                return (int)Math.Round(this.players.Average(x => x.Skills.GetAverageSkill()));
            }
        }

        public void AddPlayer(Player player)
        {
            this.players.Add(player);
        }

        public void RemovePlayer(string playerName)
        {
            if (this.players.Any(x => x.Name == playerName))
            {
                this.players.RemoveAll(x => x.Name == playerName);
            }
            else
            {
                throw new ArgumentException($"Player {playerName} is not in {this.Name} team.");
            }
        }

        public static void ExecuteCommand(string inputCommand, Dictionary<string, Team> teams)
        {
            string[] args = inputCommand.Split(";");
            string command = args[0];
            string teamName = args[1];
            switch (command)
            {
                case "Team":
                    if (!teams.ContainsKey(teamName))
                    {
                        teams.Add(teamName, new Team(teamName));
                    }
                    break;
                case "Add":
                    if (teams.ContainsKey(teamName))
                    {
                        teams[teamName].AddPlayer(new Player(
                            args[2],
                            int.Parse(args[3]),
                            int.Parse(args[4]),
                            int.Parse(args[5]),
                            int.Parse(args[6]),
                            int.Parse(args[7])));
                    }
                    else
                    {
                        Console.WriteLine($"Team {teamName} does not exist.");
                    }
                    break;
                case "Remove":
                    if (teams.ContainsKey(teamName))
                    {
                        string playerName = args[2];
                        if (teams[teamName].Players.Any(x => x.Name == playerName))
                        {
                            teams[teamName].RemovePlayer(playerName);
                        }
                        else
                        {
                            Console.WriteLine($"Player {playerName} is not in {teamName} team.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Team {teamName} does not exist.");
                    }
                    break;
                case "Rating":
                    if (teams.ContainsKey(teamName))
                    {
                        Console.WriteLine("{0} - {1}", teamName, teams[teamName].Rating);
                    }
                    else
                    {
                        Console.WriteLine($"Team {teamName} does not exist.");
                    }
                    break;
                default:
                    break;
            }
        }

    }
}

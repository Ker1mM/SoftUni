using System;
using System.Collections.Generic;
using System.Linq;

namespace P07.VladkosNotebook
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Page> opponents = new Dictionary<string, Page>();
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] tokens = input.Split('|');
                if (tokens.Length == 3)
                {
                    string color = tokens[0];
                    string varOne = tokens[1];
                    string varTwo = tokens[2];

                    if (!opponents.ContainsKey(color))
                    {
                        opponents.Add(color, new Page());
                    }
                    switch (varOne)
                    {
                        case "win":
                            opponents[color].Wins++;
                            opponents[color].Opponents.Add(varTwo);
                            break;
                        case "loss":
                            opponents[color].Losesses++;
                            opponents[color].Opponents.Add(varTwo);
                            break;
                        case "name":
                            opponents[color].Name = varTwo;
                            break;
                        case "age":
                            opponents[color].Age = int.Parse(varTwo);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (opponents.Where(x => x.Value.Age > 0 && x.Value.Name != null).ToList().Count > 0)
            {
                opponents = opponents
                    .OrderBy(x => x.Key)
                    .ToList()
                    .ToDictionary(x => x.Key, x => x.Value);

                foreach (var item in opponents)
                {
                    if (item.Value.Name != null && item.Value.Age >= 0)
                    {

                        Console.WriteLine("Color: {0}", item.Key);
                        Console.WriteLine("-age: {0}", item.Value.Age);
                        Console.WriteLine("-name: {0}", item.Value.Name);
                        if (item.Value.Opponents.Count > 0)
                        {
                            item.Value.Opponents.Sort(StringComparer.Ordinal);
                            Console.WriteLine("-opponents: {0}", String.Join(", ", item.Value.Opponents));
                        }
                        else
                        {
                            Console.WriteLine("-opponents: (empty)");
                        }
                        Console.WriteLine("-rank: {0:f2}", item.Value.GetRank());
                    }

                }
            }
            else
            {
                Console.WriteLine("No data recovered.");
            }
        }

        public class Page
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public double Wins { get; set; }
            public double Losesses { get; set; }
            public List<string> Opponents { get; set; }

            public Page()
            {
                this.Opponents = new List<string>();
                this.Wins = 0;
                this.Losesses = 0;
                this.Age = -1;
            }

            public double GetRank()
            {
                double result = (this.Wins + 1) / (this.Losesses + 1);
                return result;
            }
        }
    }
}

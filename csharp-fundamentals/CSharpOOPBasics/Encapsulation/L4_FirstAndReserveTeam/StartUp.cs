using System;
using System.Collections.Generic;

namespace PersonsInfo
{
    public class StartUp
    {
        public static void Main()
        {
            var lines = int.Parse(Console.ReadLine());
            var persons = new List<Person>();
            for (int i = 0; i < lines; i++)
            {
                var cmdArgs = Console.ReadLine().Split();
                var person = new Person(cmdArgs[0],
                                        cmdArgs[1],
                                        int.Parse(cmdArgs[2]),
                                        decimal.Parse(cmdArgs[3]));

                persons.Add(person);
            }

            Team team = new Team("SoftUni");
            foreach (var player in persons)
            {
                team.AddPlayer(player);
            }

            Console.WriteLine("First team has {0} players.", team.FirstTeam.Count);
            Console.WriteLine("Reserve team has {0} players.", team.ReserveTeam.Count);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace P6_FootballTeamGenerator
{
    public class StartUp
    {
        static void Main()
        {
            var teams = new Dictionary<string, Team>();
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                try
                {
                    Team.ExecuteCommand(input, teams);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

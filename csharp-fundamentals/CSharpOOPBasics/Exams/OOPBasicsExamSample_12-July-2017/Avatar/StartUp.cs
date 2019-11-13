using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        NationsBuilder nations = new NationsBuilder();
        string input;
        while ((input = Console.ReadLine()) != "Quit")
        {
            List<string> args = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            string command = args[0];
            switch (command)
            {
                case "Bender":
                    nations.AssignBender(args);
                    break;
                case "Monument":
                    nations.AssignMonument(args);
                    break;
                case "Status":
                    Console.WriteLine(nations.GetStatus(args[1]));
                    break;
                case "War":
                    nations.IssueWar(args[1]);
                    break;
                default:
                    break;
            }
        }

        Console.WriteLine(nations.GetWarsRecord());
    }
}

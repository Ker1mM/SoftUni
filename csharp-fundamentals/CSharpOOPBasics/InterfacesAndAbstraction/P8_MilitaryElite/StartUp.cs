using P8_MilitaryElite.Miscellaneous;
using P8_MilitaryElite.Soldiers;
using System;
using System.Collections.Generic;

namespace P8_MilitaryElite
{
    public class StartUp
    {
        static void Main()
        {
            List<ISoldier> soldiers = new List<ISoldier>();
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                try
                {
                    string[] args = input.Split();
                    string type = args[0];

                    var tempSoldier = SoldierFactory.GetSoldier(args, soldiers);
                    soldiers.Add(tempSoldier);
                }
                catch (ArgumentException) { };
            }
            foreach (var soldier in soldiers)
            {
                Console.WriteLine(soldier.ToString());
            }
        }
    }
}

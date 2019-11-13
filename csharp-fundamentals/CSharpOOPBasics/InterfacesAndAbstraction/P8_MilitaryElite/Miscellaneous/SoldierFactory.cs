using P8_MilitaryElite.Soldiers;
using System;
using System.Collections.Generic;

namespace P8_MilitaryElite.Miscellaneous
{
    public static class SoldierFactory
    {

        public static ISoldier GetSoldier(string[] args, List<ISoldier> soldiers)
        {
            string type = args[0];
            int id = int.Parse(args[1]);
            string firstName = args[2];
            string lastName = args[3];
            decimal salary = decimal.Parse(args[4]);
            switch (type)
            {
                case "Private":
                    return new Private(id, firstName, lastName, salary);

                case "Spy":
                    int codeNumber = (int)salary;
                    return new Spy(id, firstName, lastName, codeNumber);

                case "LieutenantGeneral":
                    var tempSoldier = new LieutenantGeneral(id, firstName, lastName, salary);
                    tempSoldier.AddPrivates(args, soldiers);
                    return tempSoldier;

                case "Engineer":
                    var tempEngineer = new Engineer(id, firstName, lastName, salary, args[5]);
                    tempEngineer.Add(args);
                    return tempEngineer;

                case "Commando":
                    var tempCommando = new Commando(id, firstName, lastName, salary, args[5]);
                    tempCommando.Add(args);
                    return tempCommando;

                default:
                    throw new ArgumentException("Invalid input");
            }
        }
    }
}

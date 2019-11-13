using InfernoInfinity.Data;
using InfernoInfinity.Data.CustomAttributes;
using System;
using System.Linq;

namespace InfernoInfinity.Core
{
    public class Engine
    {
        private WeaponRepository weaponRep;

        public Engine()
        {
            this.weaponRep = new WeaponRepository();
        }

        public void Run()
        {
            string input = string.Empty;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] args = input.Split(";");
                string[] commandArgs = args.Skip(1).ToArray();
                string command = args[0];
                Type type = weaponRep.GetType();
                var customAttribute = (InfoAttribute)Attribute.GetCustomAttribute(type, typeof(InfoAttribute));

                switch (command)
                {
                    case "Create":
                        this.weaponRep.AddWeapon(commandArgs);
                        break;
                    case "Add":
                        this.weaponRep.AddGem(commandArgs);
                        break;
                    case "Remove":
                        this.weaponRep.RemoveGem(commandArgs);
                        break;
                    case "Print":
                        //Print should do nothing, printing happens after the END command.
                        //Console.WriteLine(this.weaponRep.GetWeaponInfo(commandArgs));
                        break;
                    case "Author":
                        Console.WriteLine($"Author: {customAttribute.Author}");
                        break;
                    case "Revision":
                        Console.WriteLine($"Revision: {customAttribute.Revision}");
                        break;
                    case "Description":
                        Console.WriteLine($"Class description: {customAttribute.Description}");
                        break;
                    case "Reviewers":
                        Console.WriteLine($"Reviewers: {string.Join(", ", customAttribute.Reviewers)}");
                        break;
                    default:
                        break;
                }
            }

            foreach (var weapon in weaponRep)
            {
                Console.WriteLine($"{weapon.Name}: {weapon.GetMinDamage()}-{weapon.GetMaxDamage()} Damage, " +
                    $"+{weapon.GetStrength()} Strength, +{weapon.GetAgility()} Agility, +{weapon.GetVitality()} Vitality");
            }
        }
    }
}

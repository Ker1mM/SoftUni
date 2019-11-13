using System;
using System.Linq;
using System.Text;

namespace DungeonsAndCodeWizards.Functionality
{
    public class Engine
    {
        private DungeonMaster dm;

        public Engine(DungeonMaster dungeon)
        {
            this.dm = dungeon;
        }

        internal void Run()
        {
            StringBuilder sb = new StringBuilder();
            string input;
            while (!dm.IsGameOver() && !string.IsNullOrEmpty((input = Console.ReadLine())))
            {

                try
                {
                    string[] args = input.Split();
                    string command = args[0];
                    string[] commandArgs = args.Skip(1).ToArray();

                    switch (command)
                    {
                        case "JoinParty":
                            sb.AppendLine(dm.JoinParty(commandArgs));
                            break;
                        case "AddItemToPool":
                            sb.AppendLine(dm.AddItemToPool(commandArgs));
                            break;
                        case "PickUpItem":
                            sb.AppendLine(dm.PickUpItem(commandArgs));
                            break;
                        case "UseItem":
                            sb.AppendLine(dm.UseItem(commandArgs));
                            break;
                        case "UseItemOn":
                            sb.AppendLine(dm.UseItemOn(commandArgs));
                            break;
                        case "GiveCharacterItem":
                            sb.AppendLine(dm.GiveCharacterItem(commandArgs));
                            break;
                        case "Attack":
                            sb.AppendLine(dm.Attack(commandArgs));
                            break;
                        case "Heal":
                            sb.AppendLine(dm.Heal(commandArgs));
                            break;
                        case "GetStats":
                            sb.AppendLine(dm.GetStats());
                            break;
                        case "EndTurn":
                            sb.AppendLine(dm.EndTurn(commandArgs));
                            break;
                        default:
                            break;
                    }
                }
                catch (ArgumentException ae)
                { sb.AppendLine($"Parameter Error: {ae.Message}"); }
                catch (InvalidOperationException ioe)
                {
                    sb.AppendLine($"Invalid Operation: {ioe.Message}");
                }
            }
            sb.AppendLine("Final stats:");
            sb.AppendLine(dm.GetStats());
            Console.Write(sb.ToString());
        }
    }
}

using PetClinics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetClinics
{
    public class StartUp
    {
        static void Main()
        {
            Controller controller = new Controller();

            int commandCount = int.Parse(Console.ReadLine());
            StringBuilder sb = new StringBuilder();
            while (commandCount-- > 0)
            {
                string[] args = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string command = args[0];

                try
                {
                    switch (command)
                    {
                        case "Create":
                            controller.Create(args.Skip(1).ToArray());
                            break;
                        case "Add":
                            sb.AppendLine(controller.Add(args.Skip(1).ToArray()).ToString());
                            break;
                        case "Release":
                            sb.AppendLine(controller.Release(args.Skip(1).ToArray()).ToString());
                            break;
                        case "HasEmptyRooms":
                            sb.AppendLine(controller.HasEmptyRooms(args.Skip(1).ToArray()).ToString());
                            break;
                        case "Print":
                            sb.AppendLine(controller.Print(args.Skip(1).ToArray()));
                            break;
                        default:
                            break;
                    }
                }
                catch (ArgumentException)
                {
                    sb.AppendLine("Invalid Operation!");
                }
            }


            Console.WriteLine(sb.ToString().TrimEnd());
        }
    }
}

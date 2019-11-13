using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Engine
{
    public static void Run()
    {
        DraftManager dm = new DraftManager();

        string input;

        StringBuilder sb = new StringBuilder();
        while ((input = Console.ReadLine()) != "Shutdown")
        {
            List<string> args = input.Split().ToList();
            List<string> arguments = args.Skip(1).ToList();
            string command = args[0];

            switch (command)
            {
                case "RegisterHarvester":
                    sb.AppendLine(dm.RegisterHarvester(arguments));
                    break;
                case "RegisterProvider":
                    sb.AppendLine(dm.RegisterProvider(arguments));
                    break;
                case "Day":
                    sb.AppendLine(dm.Day());
                    break;
                case "Mode":
                    sb.AppendLine(dm.Mode(arguments));
                    break;
                case "Check":
                    sb.AppendLine(dm.Check(arguments));
                    break;
                default:
                    sb.AppendLine("Invalid Command!");
                    break;
            }
        }
        sb.Append(dm.ShutDown());

        Console.WriteLine(sb.ToString());
    }
}

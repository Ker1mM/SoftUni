using System;
using System.Text;


public class Engine
{

    public static void Run()
    {
        AnimalCentre.Logic.AnimalCentre veterinarian = new AnimalCentre.Logic.AnimalCentre();
        StringBuilder sb = new StringBuilder();
        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            try
            {

                string[] args = input.Split();
                string command = args[0];
                switch (command)
                {
                    case "RegisterAnimal":
                        sb.AppendLine(veterinarian.RegisterAnimal(args[1], args[2], int.Parse(args[3]), int.Parse(args[4]), int.Parse(args[5])));
                        break;
                    case "Chip":
                        sb.AppendLine(veterinarian.Chip(args[1], int.Parse(args[2])));
                        break;
                    case "Fitness":
                        sb.AppendLine(veterinarian.Fitness(args[1], int.Parse(args[2])));
                        break;
                    case "Play":
                        sb.AppendLine(veterinarian.Play(args[1], int.Parse(args[2])));
                        break;
                    case "DentalCare":
                        sb.AppendLine(veterinarian.DentalCare(args[1], int.Parse(args[2])));
                        break;
                    case "NailTrim":
                        sb.AppendLine(veterinarian.NailTrim(args[1], int.Parse(args[2])));
                        break;
                    case "Adopt":
                        sb.AppendLine(veterinarian.Adopt(args[1], args[2]));
                        break;
                    case "Vaccinate":
                        sb.AppendLine(veterinarian.Vaccinate(args[1], int.Parse(args[2])));
                        break;
                    case "History":
                        sb.AppendLine(veterinarian.History(args[1]));
                        break;
                    default:
                        break;
                }
            }
            catch (ArgumentException ae)
            {
                sb.AppendLine($"ArgumentException: {ae.Message}");
            }
            catch (InvalidOperationException ioe)
            {
                sb.AppendLine($"InvalidOperationException: {ioe.Message}");

            }
        }

        sb.AppendLine(veterinarian.End());

        Console.WriteLine(sb.ToString().Trim());

    }
}


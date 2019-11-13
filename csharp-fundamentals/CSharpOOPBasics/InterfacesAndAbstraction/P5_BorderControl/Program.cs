using System;
using System.Collections.Generic;

namespace P5_BorderControl
{
    public class Program
    {
        static void Main()
        {

            var travellers = new List<IDontKnow>();
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] args = input.Split();

                if (args.Length == 3)
                {
                    travellers.Add(new Citizen(args[0], int.Parse(args[1]), args[2]));
                }
                else if (args.Length == 2)
                {
                    travellers.Add(new Robot(args[0], args[1]));
                }
            }

            string contolrId = Console.ReadLine();

            foreach (var traveller in travellers)
            {
                if (traveller.Check(contolrId))
                {
                    Console.WriteLine(traveller.Id);
                }
            }
        }
    }
}

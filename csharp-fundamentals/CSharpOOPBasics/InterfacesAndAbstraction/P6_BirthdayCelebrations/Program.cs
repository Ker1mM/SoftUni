using System;
using System.Collections.Generic;

namespace P6_BirthdayCelebrations
{
    public class Program
    {
        static void Main()
        {
            var birthdayBoys = new List<IMatch>();
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] args = input.Split();
                string type = args[0];

                if (type == "Citizen")
                {
                    birthdayBoys.Add(new Citizen(args[1], int.Parse(args[2]), args[3], args[4]));
                }
                else if (type == "Pet")
                {
                    birthdayBoys.Add(new Pet(args[1], args[2]));
                }
            }

            string controlDate = Console.ReadLine();

            foreach (var boy in birthdayBoys)
            {
                if (boy.IsMatching(controlDate))
                {
                    Console.WriteLine(boy.BirthDate);
                }
            }
        }
    }
}

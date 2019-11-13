using System;
using System.Collections.Generic;
using System.Linq;

namespace P7_FoodShortage
{
    public class StartUp
    {
        static void Main()
        {
            var people = new Dictionary<string, IBuyer>();
            int count = int.Parse(Console.ReadLine());
            string input;
            while (count-- > 0)
            {
                input = Console.ReadLine();
                string[] args = input.Split();
                string name = args[0];
                int age = int.Parse(args[1]);
                if (args.Length == 4)
                {
                    string id = args[2];
                    string birthDate = args[3];
                    if (!people.ContainsKey(name))
                    {
                        people.Add(name, new Citizen(name, age, id, birthDate));
                    }
                }
                else if (args.Length == 3)
                {
                    string group = args[2];
                    if (!people.ContainsKey(name))
                    {
                        people.Add(name, new Rebel(name, age, group));
                    }
                }
            }

            while ((input = Console.ReadLine()) != "End")
            {
                if (people.ContainsKey(input))
                {
                    people[input].BuyFood();
                }
            }

            int totalFood = people.Values.Sum(x => x.Food);
            Console.WriteLine(totalFood);
        }
    }
}

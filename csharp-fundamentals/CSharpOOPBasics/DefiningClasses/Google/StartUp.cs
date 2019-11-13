using System;
using System.Collections.Generic;

namespace Google
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var people = new Dictionary<string, Person>();
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens =
                    input
                    .Trim()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string name = tokens[0];

                if (!people.ContainsKey(name))
                {
                    people.Add(name, new Person(name));
                }
                people[name].SetInfo(tokens);
            }

            string personToPrint = Console.ReadLine();
            Console.Write(people[personToPrint].ToString());
        }
    }
}

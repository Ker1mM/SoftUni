using System;
using System.Collections.Generic;
using System.Linq;

namespace FamilyTree
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string initialPerson = Console.ReadLine();

            List<Person> people = new List<Person>();
            List<string> relations = new List<string>();
            string input;

            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens =
                    input
                    .Trim()
                    .Split(" - ", StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Length == 1)
                {
                    string name = String.Join(" ", input.Split(" "), 0, 2);
                    string birthday = input.Split(" ")[2];

                    people.Add(new Person(name, birthday));
                }
                else if (tokens.Length == 2)
                {
                    relations.Add(input);
                }
            }

            foreach (var relation in relations)
            {
                string[] tokens = relation.Split(" - ");

                int parentIndex = people.FindIndex(x => x.Name == tokens[0] || x.Birthday == tokens[0]);
                int childIndex = people.FindIndex(x => x.Name == tokens[1] || x.Birthday == tokens[1]);

                people[parentIndex].Children.Add(people[childIndex]);
                people[childIndex].Parents.Add(people[parentIndex]);
            }

            int neededPersonIndex = people.FindIndex(x => x.Name == initialPerson || x.Birthday == initialPerson);
            if (neededPersonIndex >= 0)
            {
                Person person = people[neededPersonIndex];
                Console.WriteLine(person.ToString());
                Console.WriteLine("Parents:");
                foreach (var parent in person.Parents)
                {
                    Console.WriteLine(parent.ToString());
                }
                Console.WriteLine("Children:");
                foreach (var child in person.Children)
                {
                    Console.WriteLine(child.ToString());
                }
            }
        }
    }
}

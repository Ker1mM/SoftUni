using System;
using System.Collections.Generic;

namespace IteratorsAndComparators
{
    public class StartUp
    {
        static void Main()
        {
            var people = new List<Person>();

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] args = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                people.Add(new Person(args[0], args[1], args[2]));
            }
            int totalPeople = people.Count;

            int specialPersonIndex = int.Parse(Console.ReadLine());
            var specialPerson = people[specialPersonIndex - 1];

            int equalPeopleCount = 0;
            foreach (var person in people)
            {
                if (specialPerson.CompareTo(person) == 0)
                {
                    equalPeopleCount++;
                }
            }

            if (equalPeopleCount <= 1)
            {
                Console.WriteLine("No matches");
            }
            else
            {
                Console.WriteLine($"{equalPeopleCount} {Math.Abs(equalPeopleCount - totalPeople)} {totalPeople}");
            }
        }
    }
}

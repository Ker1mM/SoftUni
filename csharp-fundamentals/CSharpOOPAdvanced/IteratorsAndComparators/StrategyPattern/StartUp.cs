using System;
using System.Collections.Generic;

namespace StrategyPattern
{
    public class StartUp
    {
        static void Main()
        {
            int count = int.Parse(Console.ReadLine());

            SortedSet<Person> sortedByName = new SortedSet<Person>(new NameComparator());
            SortedSet<Person> sortedByAge = new SortedSet<Person>(new AgeComparator());
            while (count-- > 0)
            {
                var args = Console.ReadLine().Split();
                var currentPerson = new Person(args[0], int.Parse(args[1]));

                sortedByName.Add(currentPerson);
                sortedByAge.Add(currentPerson);
            }

            Console.WriteLine(string.Join(Environment.NewLine, sortedByName));
            Console.WriteLine(string.Join(Environment.NewLine, sortedByAge));
        }
    }
}

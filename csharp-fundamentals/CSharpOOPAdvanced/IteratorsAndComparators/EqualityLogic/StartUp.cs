using System;
using System.Collections.Generic;

namespace EqualityLogic
{
    public class StartUp
    {
        static void Main()
        {
            SortedSet<Person> sortedSet = new SortedSet<Person>();
            HashSet<Person> hashSet = new HashSet<Person>();
            int count = int.Parse(Console.ReadLine());
            while (count-- > 0)
            {
                var args = Console.ReadLine().Split();
                var currentPerson = new Person(args[0], int.Parse(args[1]));

                sortedSet.Add(currentPerson);
                hashSet.Add(currentPerson);
            }

            Console.WriteLine(sortedSet.Count);
            Console.WriteLine(hashSet.Count);
        }
    }
}

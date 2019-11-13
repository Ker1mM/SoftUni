using System;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            Family myFamiliy = new Family();
            for (int i = 0; i < count; i++)
            {
                string[] tokens = Console.ReadLine().Split();
                string name = tokens[0];
                int age = int.Parse(tokens[1]);
                Person currentPerson = new Person(name, age);
                myFamiliy.AddMember(currentPerson);
            }

            Person oldestPerson = myFamiliy.GetOldestMember();
            Console.WriteLine($"{oldestPerson.Name} {oldestPerson.Age}");
        }
    }
}

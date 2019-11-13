using System;

namespace P10_ExplicitInterfaces
{
    class Program
    {
        static void Main()
        {
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] args = input.Split();
                string name = args[0];
                string country = args[1];
                int age = int.Parse(args[2]);

                IResident resident = new Citizen(name, country, age);
                IPerson person = new Citizen(name, country, age);

                Console.WriteLine(person.GetName());
                Console.WriteLine(resident.GetName());
            }
        }
    }
}

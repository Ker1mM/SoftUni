using System;

namespace P6_Animals
{
    public class StartUp
    {
        static void Main()
        {
            string input;
            while ((input = Console.ReadLine()) != "Beast!")
            {
                try
                {
                    string type = input;
                    string[] args = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string name = args[0];
                    int age = int.Parse(args[1]);
                    string gender = args[2];

                    var animal = AnimalFactory.GetAnimal(name, age, gender, type);
                    animal.Print();
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
        }
    }
}

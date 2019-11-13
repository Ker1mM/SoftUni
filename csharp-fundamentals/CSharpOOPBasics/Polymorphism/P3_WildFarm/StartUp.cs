using System;
using System.Collections.Generic;

class StartUp
{
    static void Main()
    {
        var animals = new List<Animal>();
        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            try
            {
                string[] animalArgs = input.Split();
                input = Console.ReadLine();
                string[] foodArgs = input.Split();

                Animal animal = AnimalFactory.GetAnimal(animalArgs);
                animals.Add(animal);
                Food food = FoodFactory.GetFood(foodArgs);

                Console.WriteLine(animal.MakeSound());
                animal.Eat(food);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        foreach (var nextAnimal in animals)
        {
            Console.WriteLine(nextAnimal.ToString());
        }
    }
}

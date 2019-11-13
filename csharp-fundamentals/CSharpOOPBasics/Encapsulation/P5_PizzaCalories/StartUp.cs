using System;

namespace P5_PizzaCalories
{
    class StartUp
    {
        static void Main()
        {
            try
            {
                string input = Console.ReadLine(); //Add pizza name
                string[] args = input.Split(" ");
                Pizza pizza = new Pizza(args[1]);

                input = Console.ReadLine(); //Add dough
                args = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                Dough dough = new Dough(args[1], args[2], double.Parse(args[3]));
                pizza.AddDough(dough);

                while ((input = Console.ReadLine()) != "END") //Add toppings
                {
                    args = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    Topping topping = new Topping(args[1], double.Parse(args[2]));
                    pizza.AddTopping(topping);
                }

                Console.WriteLine($"{pizza.Name} - {pizza.GetCalories:F2} Calories.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

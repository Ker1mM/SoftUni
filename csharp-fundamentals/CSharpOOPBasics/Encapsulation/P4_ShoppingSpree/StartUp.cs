using System;
using System.Linq;

namespace P4_ShoppingSpree
{
    public class StartUp
    {
        static void Main()
        {
            try
            {
                var people = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries).Select(Person.Parse).ToList();
                var products = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries).Select(Product.Parse).ToList();

                string input;
                while ((input = Console.ReadLine()) != "END")
                {
                    string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string person = tokens[0];
                    string product = tokens[1];

                    int personIndex = people.FindIndex(x => x.Name == person);
                    int productIndex = products.FindIndex(x => x.Name == product);

                    people[personIndex].AddProduct(products[productIndex]);
                }

                people.ForEach(x => Console.WriteLine(x.ToString()));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

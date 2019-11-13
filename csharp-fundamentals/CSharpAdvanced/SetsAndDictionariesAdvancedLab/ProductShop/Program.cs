using System;
using System.Collections.Generic;

namespace ProductShop
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary<string, Dictionary<string, double>> shops = new SortedDictionary<string, Dictionary<string, double>>();


            string input;
            while ((input = Console.ReadLine()) != "Revision")
            {
                string[] tokens = input.Split(", ");
                string store = tokens[0];
                string product = tokens[1];
                double price = double.Parse(tokens[2]);

                if (shops.ContainsKey(store) == false)
                {
                    shops.Add(store, new Dictionary<string, double>());
                }

                if (shops[store].ContainsKey(product) == false)
                {
                    shops[store].Add(product, 0);
                }

                shops[store][product] = price;
            }

            foreach (var shop in shops)
            {
                Console.WriteLine("{0}->", shop.Key);
                foreach (var product in shop.Value)
                {
                    Console.WriteLine("Product: {0}, Price: {1}", product.Key, product.Value);
                }
            }
        }
    }
}

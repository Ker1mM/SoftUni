using System;
using System.Collections.Generic;

namespace CitiesByContinentAndCountry
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputCount = int.Parse(Console.ReadLine());

            Dictionary<string, Dictionary<string, List<string>>> continents = new Dictionary<string, Dictionary<string, List<string>>>();


            while (inputCount-- > 0)
            {
                string input = Console.ReadLine();
                string[] tokens = input.Split(" ");
                string continent = tokens[0];
                string country = tokens[1];
                string city = tokens[2];

                if (!continents.ContainsKey(continent))
                {
                    continents.Add(continent, new Dictionary<string, List<string>>());
                }

                if (!continents[continent].ContainsKey(country))
                {
                    continents[continent].Add(country, new List<string>());
                }

                continents[continent][country].Add(city);
            }

            foreach (var continent in continents)
            {
                Console.WriteLine("{0}:", continent.Key);
                foreach (var country in continent.Value)
                {
                    Console.WriteLine("  {0} -> {1}", country.Key, String.Join(", ", country.Value));
                }
            }
        }
    }
}

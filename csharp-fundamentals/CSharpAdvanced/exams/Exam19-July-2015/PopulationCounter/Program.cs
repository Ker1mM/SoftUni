using System;
using System.Collections.Generic;
using System.Linq;

namespace PopulationCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            var totalPopulation = new Dictionary<string, long>();
            var citiesPopulation = new Dictionary<string, Dictionary<string, int>>();

            while (input.Equals("report") == false)
            {
                string city = input.Split('|')[0];
                string country = input.Split('|')[1];
                int population = int.Parse(input.Split('|')[2]);

                if (totalPopulation.ContainsKey(country) == false)
                {
                    totalPopulation.Add(country, 0);
                    citiesPopulation.Add(country, new Dictionary<string, int>());
                }
                if (citiesPopulation[country].ContainsKey(city) == false)
                {
                    citiesPopulation[country].Add(city, population);
                }

                totalPopulation[country] += population;
                input = Console.ReadLine();
            }

            var sortedTotalPopulation = totalPopulation.ToList();
            sortedTotalPopulation.Sort((y, x) => x.Value.CompareTo(y.Value));

            foreach (var outer in sortedTotalPopulation)
            {
                Console.WriteLine("{0} (total population: {1})", outer.Key, outer.Value);
                var sortedCitiesPopulation = citiesPopulation[outer.Key].ToList();
                sortedCitiesPopulation.Sort((y, x) => x.Value.CompareTo(y.Value));
                foreach (var inner in sortedCitiesPopulation)
                {
                    Console.WriteLine("=>{0}: {1}", inner.Key, inner.Value);
                }
            }
        }
    }
}

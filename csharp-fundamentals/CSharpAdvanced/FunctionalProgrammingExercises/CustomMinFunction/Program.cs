using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomMinFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .Distinct()
                .ToList();

            Func<List<int>, int> GetMinimum = x =>
            {
                int minimum = x[0];
                foreach (int current in x)
                {
                    if (current < minimum)
                    {
                        minimum = current;
                    }
                }

                return minimum;
            };

            Console.WriteLine(GetMinimum(numbers));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace P01_Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            input = Quicksort.Sort(input);

            Console.WriteLine(string.Join(" ", input));
        }
    }
}

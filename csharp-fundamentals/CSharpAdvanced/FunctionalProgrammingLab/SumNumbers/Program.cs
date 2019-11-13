using System;
using System.Collections.Generic;
using System.Linq;

namespace SumNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<List<int>> printSumCount = n => Console.WriteLine("{0}\n{1}", n.Count, n.Sum());
            printSumCount(
                Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToList()
                );
        }
    }
}

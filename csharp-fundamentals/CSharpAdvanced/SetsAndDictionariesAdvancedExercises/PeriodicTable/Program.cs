using System;
using System.Collections.Generic;
using System.Linq;

namespace PeriodicTable
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputCount = int.Parse(Console.ReadLine());


            HashSet<string> uniqueElements = new HashSet<string>();
            while (inputCount-- > 0)
            {
                string input = Console.ReadLine();
                uniqueElements.UnionWith(new HashSet<string>(input.Split(" ")));
            }

            Console.WriteLine(String.Join(" ", uniqueElements.OrderBy(x => x)));
        }
    }
}

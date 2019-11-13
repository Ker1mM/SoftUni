using System;
using System.Collections.Generic;
using System.Linq;

namespace SetsOfElements
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] lengths = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

            HashSet<int> nSet = new HashSet<int>();
            HashSet<int> mSet = new HashSet<int>();

            for (int i = 0; i < lengths[0]; i++)
            {
                int input = int.Parse(Console.ReadLine());
                nSet.Add(input);
            }

            for (int i = 0; i < lengths[1]; i++)
            {
                int input = int.Parse(Console.ReadLine());
                mSet.Add(input);
            }

            foreach (var number in nSet)
            {
                if (mSet.Contains(number))
                {
                    Console.Write("{0} ", number);
                }
            }
            Console.WriteLine();
        }
    }
}

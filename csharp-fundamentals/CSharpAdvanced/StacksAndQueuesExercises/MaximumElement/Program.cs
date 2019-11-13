using System;
using System.Collections.Generic;
using System.Linq;

namespace MaximumElement
{
    class Program
    {
        static void Main(string[] args)
        {
            int queriesCount = int.Parse(Console.ReadLine());

            Stack<int> elements = new Stack<int>();
            while (queriesCount-- > 0)
            {
                string input = Console.ReadLine();

                int[] tokens = input.Split(" ").Select(int.Parse).ToArray();

                if (tokens[0] == 1)
                {
                    elements.Push(tokens[1]);
                }
                else if (tokens[0] == 2 && elements.Count > 0)
                {
                    elements.Pop();
                }
                else if (tokens[0] == 3)
                {
                    Console.WriteLine(elements.ToArray().Max());
                }

            }
        }
    }
}

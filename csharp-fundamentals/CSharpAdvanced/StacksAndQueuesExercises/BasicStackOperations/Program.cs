using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicStackOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputParameters = Console.ReadLine();

            int[] tokens = inputParameters.Split(" ").Select(int.Parse).ToArray();
            int elementsToPush = tokens[0];
            int elemetsToPop = tokens[1];
            int elementToFind = tokens[2];

            if (elementsToPush == 0 || elementsToPush <= elemetsToPop)
            {
                Console.WriteLine("0");
            }
            else
            {
                string elements = Console.ReadLine();

                Stack<int> stack = new Stack<int>(elements.Split(" ").Select(int.Parse));

                while (elemetsToPop > 0 && stack.Count > 0)
                {
                    stack.Pop();
                    elemetsToPop--;
                }

                if (stack.Contains(elementToFind))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine(stack.ToArray().Min());
                }
            }
        }
    }
}

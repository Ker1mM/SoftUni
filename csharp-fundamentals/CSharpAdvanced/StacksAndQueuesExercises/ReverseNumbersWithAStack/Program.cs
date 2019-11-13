using System;
using System.Collections.Generic;
using System.Linq;

namespace ReverseNumbersWithAStack
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            Stack<string> numbers = new Stack<string>(input.Split(" "));

            while (numbers.Count > 0)
            {
                Console.Write(numbers.Pop() + " ");
            }
            Console.WriteLine();
        }
    }
}

using System;
using System.Collections.Generic;

namespace RecursiveFibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            long input = long.Parse(Console.ReadLine());

            Stack<long> fibonacci = new Stack<long>();

            fibonacci.Push(0);
            fibonacci.Push(1);

            long result = 0;
            if (input == 0)
            {
                result = 0;
            }
            else if (input == 1)
            {
                result = 1;
            }
            else
            {
                for (int i = 1; i < input; i++)
                {
                    long temp = fibonacci.Pop();
                    long newNumber = temp + fibonacci.Peek();
                    fibonacci.Push(temp);
                    fibonacci.Push(newNumber);
                }
                result = fibonacci.Peek();
            }

            Console.WriteLine(result);
        }
    }
}

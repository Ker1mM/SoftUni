using System;

namespace Fibonacci
{
    class Program
    {
        private static long[] fibResults;

        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());

            fibResults = new long[number + 1];


            var result = FibonacciSequence(number);
            Console.WriteLine(result);
        }

        private static long FibonacciSequence(long number)
        {
            if (number == 0 || number == 1)
            {
                return number;
            }
            else
            {
                if (fibResults[number] == 0)
                {
                    fibResults[number] = FibonacciSequence(number - 1) + FibonacciSequence(number - 2);
                }

                return fibResults[number];
            }
        }
    }
}

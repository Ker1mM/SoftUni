using System;

namespace L02_RecursiveFactorial
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());
            long result = Factorial(number);

            Console.WriteLine(result);
        }

        private static long Factorial(int n)
        {
            if (n == 1 || n == 0)
            {
                return 1;
            }
            else
            {
                return n * Factorial(n - 1);
            }
        }
    }
}

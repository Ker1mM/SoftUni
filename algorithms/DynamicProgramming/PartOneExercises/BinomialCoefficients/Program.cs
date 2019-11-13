using System;
using System.Numerics;

namespace BinomialCoefficients
{
    class Program
    {

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());

            BigInteger num = GetFactorial(k + 1, n);
            BigInteger den = GetFactorial(1, n - k);
            BigInteger result = num / den;

            Console.WriteLine(result);
        }

        private static BigInteger GetFactorial(int start, int end)
        {

            if (start == end)
            {
                return start;
            }
            else
            {
                return end * GetFactorial(start, end - 1);
            }
        }
    }
}

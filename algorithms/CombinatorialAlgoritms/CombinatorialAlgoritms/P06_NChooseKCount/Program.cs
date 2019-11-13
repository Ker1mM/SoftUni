using System;
using System.Numerics;

namespace P06_NChooseKCount
{
    class Program
    {
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            long k = long.Parse(Console.ReadLine());

            BigInteger result = CalculateBinomial(n, k);
            Console.WriteLine(result);
        }

        private static BigInteger CalculateBinomial(long n, long k)
        {
            BigInteger nFactoriel = CalculateFactorial(n);
            BigInteger kFactorial = CalculateFactorial(k);
            BigInteger nMinusKFactorial = CalculateFactorial(n - k);

            return nFactoriel / (kFactorial * nMinusKFactorial);
        }

        private static BigInteger CalculateFactorial(long n)
        {
            if (n == 1 || n == 0)
            {
                return 1;
            }
            else
            {
                return n * CalculateFactorial(n - 1);
            }
        }
    }
}

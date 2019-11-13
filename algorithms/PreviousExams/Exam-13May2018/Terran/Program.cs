using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Terran
{
    class Program
    {
        static void Main(string[] args)
        {
            var marines = Console.ReadLine().ToCharArray();
            var lineLen = marines.Length;
            var marineCount = new Dictionary<char, int>();

            foreach (var marine in marines)
            {
                if (!marineCount.ContainsKey(marine))
                {
                    marineCount.Add(marine, 0);
                }

                marineCount[marine]++;
            }

            BigInteger den = new BigInteger(1);
            foreach (var item in marineCount)
            {
                den *= (Factoriel(item.Value));
            }

            var nom = Factoriel(marines.Length);
            var res = nom / den;
            Console.WriteLine(res);
        }

        static BigInteger Factoriel(int n)
        {
            BigInteger fac = new BigInteger(1);

            for (int i = 1; i <= n; i++)
            {
                fac *= i;
            }

            return fac;
        }
    }
}

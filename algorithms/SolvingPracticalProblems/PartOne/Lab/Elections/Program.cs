using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Elections
{
    class Program
    {
        static void Main(string[] args)
        {
            int minSeats = int.Parse(Console.ReadLine());
            int partyCount = int.Parse(Console.ReadLine());

            var parties = new int[partyCount];
            int totalSum = 0;
            for (int i = 0; i < partyCount; i++)
            {
                parties[i] = (int.Parse(Console.ReadLine()));
                totalSum += parties[i];
            }

            BigInteger[] sums = new BigInteger[totalSum + 1];

            sums[0] = 1;
            foreach (var party in parties.OrderBy(x => x))
            {
                for (long i = sums.Length - 1; i >= 0; i--)
                {
                    if (sums[i] != 0)
                    {
                        sums[i + party] += sums[i];
                    }
                }
            }

            BigInteger totalCount = 0;

            for (int i = minSeats; i < sums.Length; i++)
            {
                totalCount += sums[i];
            }

            Console.WriteLine(totalCount);
        }
    }
}


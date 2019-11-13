using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ConsoleApp
{
    class Program
    {
        static int pCount;
        static BigInteger[] sum;
        static List<int>[] referrals;

        static void Main()
        {
            pCount = int.Parse(Console.ReadLine());
            sum = new BigInteger[pCount];
            referrals = new List<int>[pCount];
            ReadReferrals();
            for (int i = 0; i < pCount; i++)
            {
                FindSum(i);
            }

            BigInteger result = new BigInteger();
            foreach (var item in sum)
            {
                result += item;
            }
            Console.WriteLine($"${result:f2}");
        }

        static void FindSum(int index)
        {
            int referralCount = referrals[index].Count;
            if (referralCount == 0)
            {
                sum[index] = 1;
                return;
            }
            else if (sum[index] == 0)
            {
                BigInteger totalSum = 0;
                for (int i = 0; i < referrals[index].Count; i++)
                {
                    if (sum[referrals[index][i]] == 0)
                    {
                        FindSum(referrals[index][i]);
                    }
                    totalSum += sum[referrals[index][i]];
                }
                sum[index] = totalSum * referralCount;
            }
        }

        static void ReadReferrals()
        {
            for (int i = 0; i < pCount; i++)
            {
                referrals[i] = new List<int>();
                char[] input = Console.ReadLine().ToCharArray();
                for (int j = 0; j < pCount; j++)
                {
                    if (input[j] == 'R')
                    {
                        referrals[i].Add(j);
                    }
                }
            }
        }
    }
}

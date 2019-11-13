using System;
using System.Collections.Generic;
using System.Linq;

namespace SumWithLimitedAmountOfCoins
{
    class Program
    {
        private static bool[,] SubsetSums;

        static void Main(string[] args)
        {
            var coins = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var sum = int.Parse(Console.ReadLine());

            Console.WriteLine(GetSumCount(coins, sum));
        }

        private static int GetSumCount(int[] coins, int sum)
        {
            var orderedCoins = coins
                .Where(x => x <= sum)
                .OrderBy(x => x)
                .ToArray();

            PopulateSubset(orderedCoins, sum);

            int row = orderedCoins.Length;
            int counter = 0;
            while (SubsetSums[row, sum])
            {
                counter++;
                row--;
            }

            return counter;
        }

        private static void PopulateSubset(int[] coins, int sum)
        {

            int rowCount = coins.Length;
            int colCount = sum;
            SubsetSums = new bool[rowCount + 1, colCount + 1];

            int col = colCount;
            int row = rowCount;

            for (int i = 0; i < coins.Length; i++)
            {
                SubsetSums[i, 0] = true;
            }

            for (int i = 1; i <= sum; i++)
            {
                for (int j = 1; j <= coins.Length; j++)
                {
                    if (i < coins[j - 1])
                    {
                        SubsetSums[j, i] = SubsetSums[j - 1, i];
                    }
                    else
                    {
                        SubsetSums[j, i] = SubsetSums[j - 1, i] || SubsetSums[j - 1, i - coins[j - 1]];
                    }
                }
            }
        }
    }
}

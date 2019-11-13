using System;
using System.Linq;

namespace SumWithUnlimitedAmountOfCoins
{
    class Program
    {
        static void Main(string[] args)
        {
            var coins = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var sum = int.Parse(Console.ReadLine());


            Console.WriteLine(GetSum(coins, coins.Length, sum));
        }

        private static int GetSum(int[] coins, int coinCount, int sum)
        {
            if (sum == 0)
            {
                return 1;
            }

            if (coinCount <= 0 && sum > 0)
            {
                return 0;
            }

            if (sum < 0)
            {
                return 0;
            }

            return GetSum(coins, coinCount - 1, sum) + GetSum(coins, coinCount, sum - coins[coinCount - 1]);
        }
    }
}

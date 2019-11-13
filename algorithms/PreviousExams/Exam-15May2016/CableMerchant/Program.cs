using System;
using System.Linq;

namespace CableMerchant
{
    class Program
    {
        static void Main(string[] args)
        {
            var prices = Console.ReadLine().Split().Select(int.Parse).ToList();
            prices.Insert(0, 0);

            var connectorPrice = int.Parse(Console.ReadLine());

            var sums = new bool[prices.Count];
            int maxLength = prices.Count - 1;

            sums[0] = true;

            for (int len = 1; len <= maxLength; len++)
            {
                sums[len] = true;
                for (int sum = maxLength; sum >= 0; sum--)
                {
                    if (sums[sum] && sum + len <= maxLength)
                    {
                        sums[len + sum] = true;
                        int newPrice = prices[len] + prices[sum] - (2 * connectorPrice);

                        if (prices[len + sum] < newPrice)
                        {
                            prices[len + sum] = newPrice;
                        }
                    }
                }
            }

            prices.RemoveAt(0);
            Console.WriteLine(string.Join(" ", prices));
        }
    }
}

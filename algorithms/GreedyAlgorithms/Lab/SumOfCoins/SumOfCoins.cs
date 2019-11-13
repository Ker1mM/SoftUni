
using System;
using System.Collections.Generic;
using System.Linq;

public class SumOfCoins
{
    public static void Main(string[] args)
    {
        var availableCoins = new[] { 1, 2, 5, 10, 20, 50 };
        var targetSum = 923;

        var selectedCoins = ChooseCoins(availableCoins, targetSum);

        Console.WriteLine($"Number of coins to take: {selectedCoins.Values.Sum()}");
        foreach (var selectedCoin in selectedCoins)
        {
            Console.WriteLine($"{selectedCoin.Value} coin(s) with value {selectedCoin.Key}");
        }
    }

    public static Dictionary<int, int> ChooseCoins(IList<int> coins, int targetSum)
    {
        var sortedCoins = coins
             .OrderByDescending(c => c)
             .ToList();

        var chosenCoins = new Dictionary<int, int>();

        int index = 0;
        while (targetSum > 0 && index < coins.Count)
        {
            int current = sortedCoins[index];
            int coinsToTakeCount = targetSum / current;

            if (coinsToTakeCount > 0)
            {
                chosenCoins.Add(current, coinsToTakeCount);
                targetSum -= current * coinsToTakeCount;
            }
            index++;
        }

        if (targetSum > 0)
        {
            throw new InvalidOperationException(
                "Greedy algorithm cannot produce desired sum with specified coins.");
        }

        return chosenCoins;
    }
}

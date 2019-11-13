using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DividingPresents
{
    class Program
    {

        private static bool[,] SubsetSum;

        static void Main(string[] args)
        {
            int[] presents = Console.ReadLine().Split().Select(int.Parse).ToArray();

            var totalPresentSum = presents.Sum();
            var presentsCount = presents.Length;
            int targetSum = (totalPresentSum) / 2;

            SubsetSum = new bool[presentsCount + 1, targetSum + 1];
            PrintResult(presents, targetSum, totalPresentSum);
        }

        private static void PrintResult(int[] presents, int targetSum, int totalSum)
        {
            PopulateSubset(targetSum, presents);
            int[] result = GetBestCombination(presents);
            var resultedSum = result.Sum();

            var sb = new StringBuilder();
            sb.AppendLine($"Difference: {totalSum - (2 * resultedSum)}");
            sb.AppendLine($"Alan:{resultedSum} Bob:{totalSum - resultedSum}");
            sb.AppendLine($"Alan takes: {string.Join(" ", result)}");
            sb.Append($"Bob takes the rest.");

            Console.WriteLine(sb.ToString());
        }

        private static int[] GetBestCombination(int[] presents)
        {
            int rowCount = SubsetSum.GetLength(0) - 1;
            int colCount = SubsetSum.GetLength(1) - 1;

            int col = colCount;
            int row = rowCount;

            var result = new List<int>();

            while (!SubsetSum[row, col] && col > 0)
            {
                col--;
            }

            while (row > 0 && col > 0)
            {
                if (SubsetSum[row - 1, col])
                {
                    row--;
                }
                else
                {
                    var value = presents[row - 1];
                    result.Add(value);
                    col -= value;
                }
            }
            ;
            return result.ToArray();
        }

        private static void PopulateSubset(int targetsum, int[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                SubsetSum[i, 0] = true;
            }

            for (int i = 1; i <= targetsum; i++)
            {
                for (int j = 1; j <= values.Length; j++)
                {
                    if (i < values[j - 1])
                    {
                        SubsetSum[j, i] = SubsetSum[j - 1, i];
                    }
                    else
                    {
                        SubsetSum[j, i] = SubsetSum[j - 1, i] || SubsetSum[j - 1, i - values[j - 1]];
                    }
                }
            }
        }
    }
}

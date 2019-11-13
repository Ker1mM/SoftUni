using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestIncreasingSubsequence
{
    class Program
    {
        private static List<int>[] bestSolutions;

        static void Main(string[] args)
        {
            int[] sequence = Console.ReadLine().Split().Select(int.Parse).ToArray();

            bestSolutions = new List<int>[sequence.Length];

            var result = FindLIS(sequence);

            Console.WriteLine(string.Join(" ", result));
        }

        private static int[] FindLIS(int[] sequence)
        {
            bestSolutions[0] = new List<int>() { sequence[0] };

            for (int i = 1; i < bestSolutions.Length; i++)
            {
                int currentMax = 0;
                for (int j = 0; j < i; j++)
                {
                    if (sequence[j] < sequence[i] && bestSolutions[j].Count + 1 > currentMax)
                    {
                        bestSolutions[i] = new List<int>(bestSolutions[j]);
                        bestSolutions[i].Add(sequence[i]);
                        currentMax = bestSolutions[i].Count;
                    }
                }

                if (bestSolutions[i] == null)
                {
                    bestSolutions[i] = new List<int>() { sequence[i] };
                }
            }

            return bestSolutions.OrderByDescending(x => x.Count).FirstOrDefault().ToArray();
        }
    }
}

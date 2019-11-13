using System;
using System.Collections.Generic;
using System.Linq;

namespace Towns
{
    class Program
    {
        private static int[] lisResult;

        static void Main(string[] args)
        {
            int townCount = int.Parse(Console.ReadLine());
            var towns = new int[townCount];
            lisResult = new int[townCount];

            for (int i = 0; i < townCount; i++)
            {
                towns[i] = int.Parse(Console.ReadLine().Split()[0]);
            }

            var increasingSeq = FindLIS(towns).ToArray();
            var decreasingSeq = FindLIS(towns.Reverse().ToArray()).Reverse().ToArray();

            int max = 0;
            for (int i = 0; i < increasingSeq.Length; i++)
            {
                int current = increasingSeq[i] + decreasingSeq[i];
                if (current > max)
                {
                    max = current;
                }
            }

            Console.WriteLine(max - 1);
            ;
        }

        private static int[] FindLIS(int[] sequence)
        {
            var bestSolutions = new int[sequence.Length];
            bestSolutions[0] = 1;

            for (int i = 1; i < bestSolutions.Length; i++)
            {
                int currentMax = 0;
                for (int j = 0; j < i; j++)
                {
                    if (sequence[j] < sequence[i] && bestSolutions[j] + 1 > currentMax)
                    {
                        bestSolutions[i] = bestSolutions[j] + 1;
                        currentMax = bestSolutions[i];
                    }
                }

                if (bestSolutions[i] == 0)
                {
                    bestSolutions[i] = 1;
                }
            }

            return bestSolutions;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Guitar
{
    class Program
    {
        static int[] intervals;
        static int max;
        static int maxReachable;
        static int intervalLength;

        static void Main(string[] args)
        {
            intervals = Console.ReadLine()
                .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            intervalLength = intervals.Length;
            int totalSum = intervals.Sum();
            int initial = int.Parse(Console.ReadLine());
            max = int.Parse(Console.ReadLine());
            maxReachable = -1;

            if (totalSum + initial <= max)
            {
                maxReachable = totalSum + initial;
            }
            else
            {
                //AddOrSubtract(initial, 0);
                maxReachable = Hehe(initial);
            }

            Console.WriteLine(maxReachable);
        }

        private static int Hehe(int initial)
        {
            var sums = new int[max + 1];
            sums[initial] = 1;

            foreach (var item in intervals)
            {
                var newSums = new int[max + 1];
                for (int i = 0; i <= max; i++)
                {
                    if (sums[i] == 1)
                    {
                        if (i + item <= max)
                        {
                            newSums[i + item] = 1;
                        }

                        if (i - item >= 0)
                        {
                            newSums[i - item] = 1;
                        }
                    }
                }
                sums = newSums;
            }


            int result = -1;
            for (int i = max; i >= 0; i--)
            {
                if (sums[i] == 1)
                {
                    result = i;
                    break;
                }
            }
            ;
            return result;
        }


    }
}

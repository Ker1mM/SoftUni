using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestZigzagSubsequence
{
    class Program
    {
        private static List<int>[] biggerLastElement;
        private static List<int>[] smallerLastElement;

        static void Main(string[] args)
        {
            int[] sequence = Console.ReadLine().Split().Select(int.Parse).ToArray();

            biggerLastElement = new List<int>[sequence.Length];
            smallerLastElement = new List<int>[sequence.Length];

            var result = GetLZS(sequence);
            Console.WriteLine(string.Join(" ", result));
        }

        private static int[] GetLZS(int[] sequence)
        {
            biggerLastElement[0] = new List<int> { sequence[0] };
            smallerLastElement[0] = new List<int> { sequence[0] };

            for (int i = 1; i < sequence.Length; i++)
            {
                int element = sequence[i];
                int currentBiggerElementMax = 0;
                int currentSmallerElementMax = 0;


                for (int j = i - 1; j >= 0; j--)
                {
                    if (element > sequence[j] && currentBiggerElementMax <= smallerLastElement[j].Count + 1)
                    {
                        biggerLastElement[i] = new List<int>(smallerLastElement[j]);
                        biggerLastElement[i].Add(element);
                        currentBiggerElementMax = biggerLastElement[i].Count;
                    }

                    if (element < sequence[j] && currentSmallerElementMax <= biggerLastElement[j].Count + 1)
                    {
                        smallerLastElement[i] = new List<int>(biggerLastElement[j]);
                        smallerLastElement[i].Add(element);
                        currentSmallerElementMax = smallerLastElement[i].Count;
                    }
                }

                if (smallerLastElement[i] == null)
                {
                    smallerLastElement[i] = new List<int> { element };
                }

                if (biggerLastElement[i] == null)
                {
                    biggerLastElement[i] = new List<int> { element };
                }
            }


            var solutionOne = biggerLastElement.OrderByDescending(x => x.Count).FirstOrDefault();
            var solutionTwo = smallerLastElement.OrderByDescending(x => x.Count).FirstOrDefault();

            if (solutionOne.Count > solutionTwo.Count)
            {
                return solutionOne.ToArray();
            }
            else
            {
                return solutionTwo.ToArray();
            }
        }
    }
}

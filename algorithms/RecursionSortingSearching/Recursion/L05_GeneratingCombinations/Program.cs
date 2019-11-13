using System;
using System.Linq;

namespace L05_GeneratingCombinations
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] set = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int n = int.Parse(Console.ReadLine());
            int[] vector = new int[n];

            GenCombs(set, vector, 0, 0);

        }

        private static void GenCombs(int[] set, int[] vector, int index, int border)
        {
            if (index >= vector.Length)
            {
                Console.WriteLine(string.Join(" ", vector));
                return;
            }
            else
            {
                for (int i = border; i < set.Length; i++)
                {
                    vector[index] = set[i];
                    GenCombs(set, vector, index + 1, i + 1);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingRange
{
    class Program
    {
        static int[] permutation;
        static bool[] marked;
        static int sumToReach;

        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            sumToReach = int.Parse(Console.ReadLine());
            marked = new bool[numbers.Length];
            permutation = numbers;

            PermuteRep(0);
        }

        private static int GetSum()
        {
            int sum = 0;
            int multiplier = 1;
            for (int i = 0; i < permutation.Length; i++)
            {
                if (marked[i])
                {
                    sum += permutation[i] * multiplier;
                    multiplier++;
                }
            }

            return sum;
        }

        private static void Print()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < permutation.Length; i++)
            {
                if (marked[i])
                {
                    sb.Append(permutation[i] + " ");
                }
            }

            Console.WriteLine(sb.ToString().TrimEnd());
        }

        private static void PermuteRep(int start)
        {
            var sum = GetSum();

            if (sum == sumToReach)
            {
                Print();
            }

            if (start >= permutation.Length || sum >= sumToReach)
            {
                return;
            }

            var used = new HashSet<int>();

            for (var i = start; i < permutation.Length; i++)
            {
                if (!used.Contains(permutation[i]))
                {
                    Swap(start, i);
                    marked[start] = true;
                    PermuteRep(start + 1);

                    Swap(start, i);
                    marked[start] = false;
                    used.Add(permutation[i]);
                }
            }

        }

        private static void Swap(int i, int j)
        {
            var temp = permutation[i];
            permutation[i] = permutation[j];
            permutation[j] = temp;
        }
    }
}

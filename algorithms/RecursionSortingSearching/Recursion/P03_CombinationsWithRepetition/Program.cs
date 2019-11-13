using System;

namespace P03_CombinationsWithRepetition
{
    class Program
    {
        private static int[] combination;
        static void Main(string[] args)
        {
            int setCount = int.Parse(Console.ReadLine());
            int duplicateCount = int.Parse(Console.ReadLine());
            combination = new int[duplicateCount];
            Combinations(setCount, 0, 1);
        }

        static void Combinations(int setCount, int index, int element)
        {
            if (index >= combination.Length)
            {
                Print();
                return;
            }

            for (int i = element; i <= setCount; i++)
            {
                combination[index] = i;
                Combinations(setCount, index + 1, i);
            }
        }

        private static void Print()
        {
            Console.WriteLine(string.Join(" ", combination));
        }
    }
}

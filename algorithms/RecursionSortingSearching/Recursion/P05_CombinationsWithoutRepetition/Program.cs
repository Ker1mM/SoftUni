using System;

namespace P05_CombinationsWithoutRepetition
{
    class Program
    {
        static int set;
        static int length;
        static int[] combination;

        static void Main(string[] args)
        {
            set = int.Parse(Console.ReadLine());
            length = int.Parse(Console.ReadLine());
            combination = new int[length];

            Combination(0, 1);
        }

        private static void Combination(int index, int element)
        {
            if (index == length)
            {
                Console.WriteLine(string.Join(" ", combination));
            }
            else
            {
                for (int i = element; i <= set; i++)
                {
                    combination[index] = i;
                    Combination(index + 1, i + 1);
                }
            }
        }
    }
}

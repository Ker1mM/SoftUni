using System;
using System.Linq;

namespace P05_VariationsWithoutRepetitions
{
    class Program
    {
        private static int varLength;
        private static char[] nextVariation;
        private static bool[] used;

        static void Main(string[] args)
        {
            char[] input = Console.ReadLine().Split().Select(char.Parse).ToArray();
            varLength = int.Parse(Console.ReadLine());
            nextVariation = new char[varLength];
            used = new bool[input.Length];
            Variation(0, input);
        }

        static void Variation(int index, char[] input)
        {
            if (index >= varLength)
            {
                Console.WriteLine(string.Join(" ", nextVariation));
            }
            else
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (!used[i])
                    {
                        used[i] = true;
                        nextVariation[index] = input[i];
                        Variation(index + 1, input);
                        used[i] = false;
                    }
                }
            }
        }
    }
}

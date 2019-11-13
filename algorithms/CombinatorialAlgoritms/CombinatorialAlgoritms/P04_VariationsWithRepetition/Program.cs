using System;
using System.Linq;

namespace P04_VariationsWithRepetition
{
    class Program
    {
        private static int varLength;
        private static char[] nextVariation;

        static void Main(string[] args)
        {
            char[] input = Console.ReadLine().Split().Select(char.Parse).ToArray();
            varLength = int.Parse(Console.ReadLine());
            nextVariation = new char[varLength];

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
                    nextVariation[index] = input[i];
                    Variation(index + 1, input);
                }
            }
        }
    }
}

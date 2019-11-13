using System;
using System.Linq;

namespace LegoBlocks
{
    class Program
    {
        static void Main(string[] args)
        {
            int cells = int.Parse(Console.ReadLine());

            int[][] firstLego = new int[cells][];
            int[][] secondLego = new int[cells][];

            for (int i = 0; i < cells; i++)
            {
                firstLego[i] = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            }
            for (int i = 0; i < cells; i++)
            {
                secondLego[i] = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Reverse().ToArray();
            }

            if (MatchedArrays(firstLego, secondLego))
            {
                for (int i = 0; i < cells; i++)
                {
                    Console.WriteLine("[{0}, {1}]", String.Join(", ", firstLego[i]), String.Join(", ", secondLego[i]));
                }
            }
            else
            {
                int totalElements = 0;
                for (int i = 0; i < cells; i++)
                {
                    totalElements += firstLego[i].Length;
                    totalElements += secondLego[i].Length;
                }
                Console.WriteLine("The total number of cells is: {0}", totalElements);
            }

        }

        public static bool MatchedArrays(int[][] first, int[][] second)
        {
            bool result = true;
            int length = first[0].Length + second[0].Length;
            for (int i = 1; i < first.Length; i++)
            {
                if (first[i].Length + second[i].Length != length)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}

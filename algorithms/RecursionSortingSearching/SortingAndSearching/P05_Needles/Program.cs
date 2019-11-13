using System;
using System.Collections.Generic;
using System.Linq;

namespace P05_Needles
{
    class Program
    {
        public static void Main()
        {
            int[] arraySizes = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var sequence = Console.ReadLine().Split().Select(int.Parse).ToList();
            int[] needles = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int prev = sequence[arraySizes[0] - 1];
            for (int i = arraySizes[0] - 1; i >= 0; i--)
            {
                if (sequence[i] == 0)
                {
                    sequence[i] = prev;
                }

                prev = sequence[i];
            }

            var result = new List<int>();
            for (int i = 0; i < arraySizes[1]; i++)
            {
                bool isIn = false;
                for (int j = 0; j < sequence.Count; j++)
                {
                    if (sequence[j] >= needles[i])
                    {
                        result.Add(j);
                        isIn = true;
                        break;
                    }
                }

                if (!isIn)
                {
                    int index = sequence.Count - 1;
                    while (index > 0 && sequence[index] == 0)
                    {
                        index--;
                    }
                    if (sequence[index] == 0)
                    {
                        index--;
                    }
                    result.Add(index + 1);
                }
            }

            Console.WriteLine(string.Join(" ", result));
        }
    }
}

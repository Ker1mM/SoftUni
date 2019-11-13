using System;
using System.Linq;

namespace GroupNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();

            int[][] numberSet = new int[3][];

            for (int i = 0; i < 3; i++)
            {
                numberSet[i] = numbers.Where(x => Math.Abs(x % 3) == i).ToArray();
            }

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(String.Join(" ", numberSet[i]));
            }
        }
    }
}

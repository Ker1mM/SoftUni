using System;
using System.Linq;

namespace BunkerBuster
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dimensions = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            int[][] area = new int[dimensions[0]][];
            for (int i = 0; i < dimensions[0]; i++)
            {
                area[i] = new int[dimensions[1]];
                area[i] = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            }

            string input;
            while ((input = Console.ReadLine()) != "cease fire!")
            {
                string[] tokens = input.Split();
                int row = int.Parse(tokens[0]);
                int col = int.Parse(tokens[1]);
                int power = tokens[2][0];

                Bomb(area, row, col, power);
            }

            double destroyedBunkers = 0;
            foreach (int[] bunker in area)
            {
                destroyedBunkers += bunker.Where(x => x <= 0).Count();
            }

            Console.WriteLine($"Destroyed bunkers: {destroyedBunkers}");
            Console.WriteLine("Damage done: {0:F1} %", (destroyedBunkers / (dimensions[0] * dimensions[1])) * 100);
        }

        public static void Bomb(int[][] area, int impactRow, int impactCol, int pow)
        {
            int row = impactRow - 1;
            int col = impactCol - 1;
            int dimRow = area.Length;
            int dimCol = area[0].Length;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (IsInside(dimRow, dimCol, row + i, col + j) && area[row + i][col + j] > 0)
                    {
                        if (impactCol == col + j && impactRow == row + i)
                        {
                            area[row + i][col + j] -= pow;
                        }
                        else
                        {
                            area[row + i][col + j] -= (int)Math.Ceiling(pow / 2.0);
                        }
                    }
                }
            }
        }

        public static bool IsInside(int dimRow, int dimCol, int row, int col)
        {
            return row >= 0 && row < dimRow && col >= 0 && col < dimCol;
        }
    }
}

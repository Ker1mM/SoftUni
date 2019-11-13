using System;
using System.Linq;
using System.Numerics;

namespace Zerg
{
    class Program
    {
        static int routesToBase;
        static BigInteger[,] grid;
        static void Main(string[] args)
        {
            int[] baseSize = Console.ReadLine().Split().Select(int.Parse).ToArray();
            grid = new BigInteger[baseSize[0], baseSize[1]];

            int[] mainBaseLocation = Console.ReadLine().Split().Select(int.Parse).ToArray();
            grid[mainBaseLocation[0], mainBaseLocation[1]] = 2;
            int enemyCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < enemyCount; i++)
            {
                int[] enemyLocation = Console.ReadLine().Split().Select(int.Parse).ToArray();
                grid[enemyLocation[0], enemyLocation[1]] = -1;
            }

            int num = 1;
            for (int i = 0; i < baseSize[0]; i++)
            {
                if (num == 1 && grid[i, 0] == -1)
                {
                    num = 0;
                }

                grid[i, 0] = num;
            }

            num = 1;
            for (int i = 0; i < baseSize[1]; i++)
            {
                if (num == 1 && grid[0, i] == -1)
                {
                    num = 0;
                }

                grid[0, i] = num;
            }

            for (int row = 1; row <= mainBaseLocation[0]; row++)
            {
                for (int col = 1; col <= mainBaseLocation[1]; col++)
                {
                    if (grid[row, col] == -1)
                    {
                        grid[row, col] = 0;
                    }
                    else
                    {
                        grid[row, col] = grid[row - 1, col] + grid[row, col - 1];
                    }
                }
            }

            Console.WriteLine(grid[mainBaseLocation[0], mainBaseLocation[1]]);

            ;
        }

    }
}

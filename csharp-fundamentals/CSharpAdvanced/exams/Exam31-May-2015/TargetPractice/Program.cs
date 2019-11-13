using System;
using System.Linq;

namespace TargetPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rowsAndColumns = Console.ReadLine().Split().Select(int.Parse).ToArray();
            string snakeSymbols = Console.ReadLine();
            char[,] matrix = new char[rowsAndColumns[0], rowsAndColumns[1]];

            int[] shotTokens = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int counter = 0;
            int direction = -1;
            int indexer = matrix.GetLength(1) - 1;
            for (int r = matrix.GetLength(0) - 1; r >= 0; r--)
            {
                while (true)
                {
                    if (counter >= snakeSymbols.Length)
                    {
                        counter = 0;
                    }
                    matrix[r, indexer] = snakeSymbols[counter];
                    counter++;

                    indexer += direction;

                    if (indexer < 0)
                    {
                        direction = 1;
                        indexer += direction;
                        break;
                    }
                    else if (indexer >= matrix.GetLength(1))
                    {
                        direction = -1;
                        indexer += direction;
                        break;
                    }
                }
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (IsInside(shotTokens[1], shotTokens[0], j, i, shotTokens[2]))
                    {
                        matrix[i, j] = ' ';
                    }
                }
            }

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                int empty = EmptyIndexes(matrix, i);
                if (empty > 0 && empty < matrix.GetLength(0))
                {
                    for (int j = matrix.GetLength(0) - 1; j > 0; j--)
                    {
                        if (matrix[j, i] == ' ')
                        {
                            if (j - empty >= 0)
                            {
                                matrix[j, i] = matrix[j - empty, i];
                                matrix[j - empty, i] = ' ';
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static int EmptyIndexes(char[,] matrix, int column)
        {
            int count = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, column] == ' ')
                {
                    count++;
                }
            }
            return count;
        }

        public static bool IsInside(int centerX, int centerY, int x, int y, int radius)
        {
            x -= centerX;
            y -= centerY;

            double distance = Math.Sqrt(x * x + y * y);
            if (radius >= distance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

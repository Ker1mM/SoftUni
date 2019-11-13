using System;
using System.Linq;

namespace _2x2SquaresInMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rowsAndColumns = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

            string[,] matrix = new string[rowsAndColumns[0], rowsAndColumns[1]];

            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                string[] currentRow = Console.ReadLine().Split();
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    matrix[r, c] = currentRow[c];
                }
            }

            int equalSquaresCount = 0;

            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    if (matrix[i, j] == matrix[i, j + 1] &&
                       matrix[i, j + 1] == matrix[i + 1, j] &&
                       matrix[i + 1, j] == matrix[i + 1, j + 1])
                    {
                        equalSquaresCount++;
                    }
                }
            }

            Console.WriteLine(equalSquaresCount);
        }
    }
}

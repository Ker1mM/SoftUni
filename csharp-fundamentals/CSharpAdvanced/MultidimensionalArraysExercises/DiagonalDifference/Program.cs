using System;
using System.Linq;

namespace DiagonalDifference
{
    class Program
    {
        static void Main(string[] args)
        {
            int matrixSize = int.Parse(Console.ReadLine());

            int[,] matrix = new int[matrixSize, matrixSize];

            for (int i = 0; i < matrixSize; i++)
            {
                int[] currentRow = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                for (int j = 0; j < matrixSize; j++)
                {
                    matrix[i, j] = currentRow[j];
                }
            }
            Console.WriteLine(DiagonalDifference(matrix));
        }

        public static int DiagonalDifference(int[,] matrix)
        {
            int matrixSize = matrix.GetLength(0);
            int primaryDiagonalSum = 0;
            int secondaryDiagonalSum = 0;

            for (int i = 0; i < matrixSize; i++)
            {
                primaryDiagonalSum += matrix[i, i];
                secondaryDiagonalSum += matrix[i, matrixSize - i - 1];
            }

            return Math.Abs(primaryDiagonalSum - secondaryDiagonalSum);
        }
    }
}

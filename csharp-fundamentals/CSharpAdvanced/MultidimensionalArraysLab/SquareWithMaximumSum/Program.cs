using System;
using System.Linq;

namespace SquareWithMaximumSum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rowColumnCount = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            int[,] matrix = new int[rowColumnCount[0], rowColumnCount[1]];

            for (int i = 0; i < rowColumnCount[0]; i++)
            {
                int[] currentRow = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
                SetRow(matrix, currentRow, i);
            }

            int[,] maxMatrix = new int[2, 2];
            int maxSum = 0;
            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    int currentSum = matrix[i, j] + matrix[i, j + 1] + matrix[i + 1, j] + matrix[i + 1, j + 1];
                    if (currentSum > maxSum)
                    {
                        maxSum = currentSum;
                        maxMatrix[0, 0] = matrix[i, j];
                        maxMatrix[0, 1] = matrix[i, j + 1];
                        maxMatrix[1, 0] = matrix[i + 1, j];
                        maxMatrix[1, 1] = matrix[i + 1, j + 1];
                    }

                }
            }

            Console.WriteLine("{0} {1}", maxMatrix[0, 0], maxMatrix[0, 1]);
            Console.WriteLine("{0} {1}", maxMatrix[1, 0], maxMatrix[1, 1]);
            Console.WriteLine(maxSum);
        }

        public static void SetRow(int[,] matrix, int[] row, int rowNumber)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                matrix[rowNumber, i] = row[i];
            }
        }
    }
}

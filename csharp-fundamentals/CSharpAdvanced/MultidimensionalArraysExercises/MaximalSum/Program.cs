using System;
using System.Linq;

namespace MaximalSum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rowsAndColumns = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

            int[,] matrix = new int[rowsAndColumns[0], rowsAndColumns[1]];

            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                string[] currentRow = Console.ReadLine().Split();
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    matrix[r, c] = int.Parse(currentRow[c]);
                }
            }

            int[,] maxMatrix = new int[3, 3];
            int maxSum = 0;

            for (int i = 0; i < matrix.GetLength(0) - 2; i++)
            {
                for (int j = 0; j < matrix.GetLength(1) - 2; j++)
                {
                    int currentSum = PartialSum(matrix, i, j);

                    if (currentSum > maxSum)
                    {
                        maxSum = currentSum;

                        for (int r = 0; r < 3; r++)
                        {
                            for (int c = 0; c < 3; c++)
                            {
                                maxMatrix[r, c] = matrix[i + r, j + c];
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Sum = {0}", maxSum);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("{0} ", maxMatrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static int PartialSum(int[,] matrix, int startRowIndex, int startColumnIndex)
        {
            int totalSum = 0;
            for (int i = startRowIndex; i < startRowIndex + 3; i++)
            {
                for (int j = startColumnIndex; j < startColumnIndex + 3; j++)
                {
                    totalSum += matrix[i, j];
                }
            }

            return totalSum;
        }
    }
}

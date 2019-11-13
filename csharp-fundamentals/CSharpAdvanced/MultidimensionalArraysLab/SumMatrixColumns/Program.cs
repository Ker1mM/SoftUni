using System;
using System.Linq;

namespace SumMatrixColumns
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rowColumnCount = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            int[,] matrix = new int[rowColumnCount[0], rowColumnCount[1]];

            for (int i = 0; i < rowColumnCount[0]; i++)
            {
                int[] currentRow = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
                SetRow(matrix, currentRow, i);
            }

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                int columnSum = 0;
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    columnSum += matrix[j, i];
                }
                Console.WriteLine(columnSum);
            }
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

using System;
using System.Linq;

namespace PrimaryDiagonal
{
    class Program
    {
        static void Main(string[] args)
        {
            int rowColumnCount = int.Parse(Console.ReadLine());
            int[,] matrix = new int[rowColumnCount, rowColumnCount];

            for (int i = 0; i < rowColumnCount; i++)
            {
                int[] currentRow = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
                SetRow(matrix, currentRow, i);
            }

            int totalSum = 0;
            for (int i = 0; i < rowColumnCount; i++)
            {
                totalSum += matrix[i, i];
            }
            Console.WriteLine(totalSum);
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

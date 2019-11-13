using System;
using System.Linq;

namespace SumMatrixElements
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

            Console.WriteLine(matrix.GetLength(0));
            Console.WriteLine(matrix.GetLength(1));

            int totalSum = 0;
            foreach (var currentElement in matrix)
            {
                totalSum += currentElement;
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

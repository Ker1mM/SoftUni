using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoveDownOrRight
{
    class Program
    {
        static void Main(string[] args)
        {
            int rowCount = int.Parse(Console.ReadLine());
            int colCount = int.Parse(Console.ReadLine());

            var matrix = GetMatrix(rowCount);

            CalculateSum(matrix);
            string result = GetBestRoute(matrix);
            Console.WriteLine(result);
        }

        private static string GetBestRoute(int[][] matrix)
        {
            var result = new List<string>();

            int row = matrix.GetLength(0) - 1;
            int col = row;
            result.Add($"[{row}, {col}]");

            while (row > 0 || col > 0)
            {
                if (col != 0 && (row == 0 || matrix[row][col - 1] >= matrix[row - 1][col]))
                {
                    col--;
                }
                else
                {
                    row--;
                }
                result.Add($"[{row}, {col}]");
            }

            result.Reverse();
            return string.Join(" ", result);
        }

        private static void CalculateSum(int[][] matrix)
        {
            CalculateFirstCol(matrix);
            CalculateFirstRow(matrix);

            for (int row = 1; row < matrix.GetLength(0); row++)
            {
                for (int col = 1; col < matrix[0].Length; col++)
                {
                    if (matrix[row][col - 1] < matrix[row - 1][col])
                    {
                        matrix[row][col] += matrix[row - 1][col];
                    }
                    else
                    {
                        matrix[row][col] += matrix[row][col - 1];
                    }
                }
            }
        }

        private static void CalculateFirstRow(int[][] matrix)
        {
            for (int i = 1; i < matrix[0].Length; i++)
            {
                matrix[0][i] += matrix[0][i - 1];
            }
        }

        private static void CalculateFirstCol(int[][] matrix)
        {
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                matrix[i][0] += matrix[i - 1][0];
            }
        }

        private static int[][] GetMatrix(int rows)
        {
            int[][] matrix = new int[rows][];

            for (int i = 0; i < rows; i++)
            {
                matrix[i] = Console.ReadLine().Split().Select(int.Parse).ToArray();
            }

            return matrix;
        }
    }
}

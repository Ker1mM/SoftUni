using System;
using System.Collections.Generic;
using System.Linq;

namespace ZigZagMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = ZigZag.FindMaxSum();
            Console.WriteLine(result);
        }
    }

    static class ZigZag
    {
        private static int rowCount;
        private static int colCount;
        private static int[,] matrix;
        private static long[,] maxMatrix;

        public static string FindMaxSum()
        {
            ReadMatrix();
            FindMaxMatrix();

            List<int> result = new List<int>();
            long maxValue = 0;
            int maxValueIndex = -1;

            for (int row = 0; row < rowCount; row++)
            {
                if (maxMatrix[row, colCount - 1] > maxValue)
                {
                    maxValue = maxMatrix[row, colCount - 1];
                    maxValueIndex = row;
                }
            }

            result.Add(matrix[maxValueIndex, colCount - 1]);
            for (int col = colCount - 1; col > 0; col--)
            {
                var value = FindMaxValue(col, maxValueIndex);
                maxValueIndex = value.Key;
                result.Add(matrix[maxValueIndex, col - 1]);
            }

            result.Reverse();
            return $"{result.Sum()} = {string.Join(" + ", result)}";
        }

        private static void ReadMatrix()
        {
            rowCount = int.Parse(Console.ReadLine());
            colCount = int.Parse(Console.ReadLine());
            matrix = new int[rowCount, colCount];
            maxMatrix = new long[rowCount, colCount];

            for (int row = 0; row < rowCount; row++)
            {
                var inputArgs = Console.ReadLine().Split(',');
                for (int col = 0; col < colCount; col++)
                {
                    matrix[row, col] = int.Parse(inputArgs[col]);
                }
            }
        }

        private static void FindMaxMatrix()
        {
            for (int i = 0; i < rowCount; i++)
            {
                maxMatrix[i, 0] = matrix[i, 0];
            }

            for (int col = 1; col < colCount; col++)
            {
                for (int row = 0; row < rowCount; row++)
                {
                    maxMatrix[row, col] = FindMaxValue(col, row).Value + matrix[row, col];
                }
            }
        }

        private static KeyValuePair<int, long> FindMaxValue(int col, int rowIndex)
        {
            var maxValue = new KeyValuePair<int, long>(-1, 0);
            int start = 0;
            int end = rowIndex;

            if (col % 2 != 0)
            {
                start = rowIndex + 1;
                end = rowCount;
            }

            for (int i = start; i < end; i++)
            {
                if (maxMatrix[i, col - 1] > maxValue.Value)
                {
                    maxValue = new KeyValuePair<int, long>(i, maxMatrix[i, col - 1]);
                }
            }

            return maxValue;
        }
    }
}

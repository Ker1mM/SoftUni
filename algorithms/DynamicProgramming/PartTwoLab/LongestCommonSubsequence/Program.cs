using System;

namespace LongestCommonSubsequence
{
    class Program
    {
        private static int[][] matrix;
        private static int maxSequenceLength;

        static void Main(string[] args)
        {
            string stringOne = Console.ReadLine();
            string stringTwo = Console.ReadLine();

            matrix = new int[stringOne.Length + 1][];
            maxSequenceLength = 0;
            FindLCS(stringOne, stringTwo);
            Console.WriteLine(maxSequenceLength);
        }

        private static void FindLCS(string one, string two)
        {
            matrix[0] = new int[two.Length + 1];
            for (int row = 1; row < one.Length + 1; row++)
            {
                matrix[row] = new int[two.Length + 1];
                for (int col = 1; col < two.Length + 1; col++)
                {
                    if (one[row - 1] == two[col - 1])
                    {
                        var number = matrix[row - 1][col - 1] + 1;
                        matrix[row][col] = number;
                        if (number > maxSequenceLength)
                        {
                            maxSequenceLength = number;
                        }

                    }
                    else
                    {
                        matrix[row][col] = GetBiggerValue(row, col);
                    }
                }
            }
        }

        private static int GetBiggerValue(int row, int col)
        {
            int topValue = matrix[row - 1][col];
            int leftValue = matrix[row][col - 1];

            return Math.Max(topValue, leftValue);
        }
    }
}

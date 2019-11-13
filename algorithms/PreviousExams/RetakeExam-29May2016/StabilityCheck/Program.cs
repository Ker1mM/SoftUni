using System;
using System.Linq;

namespace StabilityCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            int side = int.Parse(Console.ReadLine());
            int matrixSize = int.Parse(Console.ReadLine());
            long[][] matrix = new long[matrixSize][];

            int sumMatrxSize = matrixSize - side + 1;
            long[][] sumMatrix = new long[matrixSize][];
            bool[][] visited = new bool[matrixSize][];
            for (int i = 0; i < matrixSize; i++)
            {
                sumMatrix[i] = new long[sumMatrxSize];
                visited[i] = new bool[sumMatrxSize];
                matrix[i] = Console.ReadLine().Split().Select(long.Parse).ToArray();
            }

            long max = long.MinValue;

            for (int row = 0; row < sumMatrxSize; row++)
            {
                for (int col = 0; col < sumMatrxSize; col++)
                {
                    long currentSum = 0;
                    for (int i = 0; i < side; i++)
                    {
                        if (!visited[row + i][col])
                        {
                            sumMatrix[row + i][col] = GetSum(matrix, row + i, col, side);
                            visited[row + i][col] = true;
                        }

                        currentSum += sumMatrix[row + i][col];
                    }

                    if (currentSum > max)
                    {
                        max = currentSum;
                    }
                }
            }

            Console.WriteLine(max);
        }

        static long GetSum(long[][] matrix, int row, int col, int size)
        {
            long result = 0;
            for (int i = col; i < col + size; i++)
            {
                result += matrix[row][i];
            }

            return result;
        }
    }
}

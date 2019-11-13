using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RodCutting
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] points = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int rodLength = int.Parse(Console.ReadLine());

            points = points.Take(rodLength + 1).ToArray();
            var result = GetOptimalCutLengths(points, rodLength);
            Console.WriteLine(result);
        }

        private static string GetOptimalCutLengths(int[] points, int rodLength)
        {
            int[,] maxPoints = new int[rodLength + 1, points.Length];

            int[] maxPerLength = new int[rodLength + 1];

            for (int col = 1; col < maxPoints.GetLength(1); col++)
            {
                for (int row = 1; row < maxPoints.GetLength(0); row++)
                {
                    int currentPoint = 0;
                    if (row == col)
                    {
                        currentPoint = points[col];
                    }
                    else if (row > col)
                    {
                        currentPoint = points[col] + maxPerLength[row - col];
                    }

                    if (currentPoint > maxPerLength[row])
                    {
                        maxPerLength[row] = currentPoint;
                    }

                    maxPoints[row, col] = currentPoint;
                }
            }
            string result = GetBestWayCut(maxPoints, maxPerLength, rodLength);
            return result;
        }

        private static string GetBestWayCut(int[,] maxPoints, int[] maxPerLength, int rodLength)
        {
            var sb = new StringBuilder();
            var cutLengths = new List<int>();

            int maxPoint = maxPerLength[rodLength];
            sb.AppendLine($"{maxPoint}");

            while (rodLength > 0)
            {
                int index = GetRow(maxPoints, rodLength).IndexOf(maxPoint);
                cutLengths.Add(index);
                rodLength -= index;
                maxPoint = maxPerLength[rodLength];
            }

            cutLengths.Reverse();
            sb.AppendLine(string.Join(" ", cutLengths));
            return sb.ToString().TrimEnd();

        }

        private static List<int> GetRow(int[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToList();
        }
    }
}

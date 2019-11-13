using System;
using System.Collections.Generic;
using System.Linq;

namespace AreasInMatrix
{
    class Program
    {

        private static char[][] matrix;
        private static bool[][] visited;
        static void Main(string[] args)
        {
            int input = int.Parse(Console.ReadLine());

            matrix = new char[input][];
            visited = new bool[input][];

            for (int i = 0; i < input; i++)
            {
                var row = Console.ReadLine().ToCharArray();
                matrix[i] = row;
                visited[i] = new bool[row.Length];
            }

            var areas = new Dictionary<char, int>();

            for (int i = 0; i < input; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (!visited[i][j])
                    {
                        char areaSymbol = matrix[i][j];
                        FindArea(i, j, areaSymbol);

                        if (!areas.ContainsKey(areaSymbol))
                        {
                            areas.Add(areaSymbol, 0);
                        }

                        areas[areaSymbol]++;
                    }
                }
            }

            areas = areas
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);

            var totalAreas = areas.Sum(x => x.Value);
            Console.WriteLine($"Areas: {totalAreas}");

            foreach (var area in areas)
            {
                Console.WriteLine($"Letter '{area.Key}' -> {area.Value}");
            }
        }

        private static int FindArea(int row, int col, char letter)
        {
            if (CorrectPosition(row, col, letter))
            {
                visited[row][col] = true;

                int left = FindArea(row, col - 1, letter);
                int right = FindArea(row, col + 1, letter);
                int top = FindArea(row - 1, col, letter);
                int bottom = FindArea(row + 1, col, letter);

                return left + right + top + bottom + 1;
            }
            else
            {
                return 0;
            }
        }

        private static bool CorrectPosition(int row, int col, char letter)
        {
            int rowLength = matrix.GetLength(0);
            int colLength = matrix[0].Length;

            bool correctCol = col >= 0 && col < colLength;
            bool correctRow = row >= 0 && row < rowLength;

            if (correctRow && correctCol)
            {
                return matrix[row][col] == letter && !visited[row][col];
            }

            return false;
        }
    }
}

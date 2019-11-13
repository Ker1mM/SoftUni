using System;
using System.Collections.Generic;
using System.Linq;

namespace L07_PathsInLabyrinth
{
    class Program
    {
        static void Main(string[] args)
        {
            Labyrinth.ReadLab();
            Labyrinth.FindPaths(0, 0, 'T');
        }
    }

    static class Labyrinth
    {
        static List<char> path = new List<char>();
        private static int rows = 0;
        private static int cols = 0;
        private static char[][] lab;

        internal static void ReadLab()
        {
            rows = int.Parse(Console.ReadLine());
            cols = int.Parse(Console.ReadLine());
            lab = new char[rows][];
            for (int row = 0; row < rows; row++)
            {
                lab[row] = Console.ReadLine().ToCharArray();
            }
        }

        internal static void FindPaths(int row, int col, char direction)
        {
            if (!IsInBound(row, col))
            {
                return;
            }

            path.Add(direction);

            if (IsExit(row, col))
            {
                PrintPath();
            }
            else if (!IsVisited(col, row) && IsFree(row, col))
            {
                Mark(row, col);
                FindPaths(row, col + 1, 'R');
                FindPaths(row + 1, col, 'D');
                FindPaths(row, col - 1, 'L');
                FindPaths(row - 1, col, 'U');
                Unmark(row, col);
            }

            path.RemoveAt(path.Count - 1);
        }

        private static void Unmark(int row, int col)
        {
            lab[row][col] = '-';
        }

        private static void Mark(int row, int col)
        {
            lab[row][col] = 'V';
        }

        private static bool IsFree(int row, int col)
        {
            return lab[row][col] == '-';
        }

        private static bool IsVisited(int col, int row)
        {
            return lab[row][col] == 'V';
        }

        private static void PrintPath()
        {
            Console.WriteLine(string.Join("", path.Skip(1)));
        }

        private static bool IsExit(int row, int col)
        {
            return lab[row][col] == 'e';
        }

        private static bool IsInBound(int row, int col)
        {
            return (row >= 0 && row < rows) && (col >= 0 && col < cols);
        }


    }
}

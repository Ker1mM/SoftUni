using System;
using System.Collections.Generic;
using System.Linq;

namespace P06_ConnectedAreasInAMatrix
{
    class Program
    {
        static char[][] matrix;
        static int rows;
        static int cols;
        static int counter;
        static List<Area> foundAreas;

        static void Main(string[] args)
        {
            ReadMatrix();
            foundAreas = new List<Area>();
            int[] freeArea = FindEmptyArea();
            while (freeArea[0] != -1)
            {
                counter = 0;
                FindConnectedArea(freeArea[0], freeArea[1]);
                var area = new Area(freeArea[0], freeArea[1], counter);
                foundAreas.Add(area);
                freeArea = FindEmptyArea();
            }

            foundAreas = foundAreas
                 .OrderByDescending(x => x.Size)
                 .ThenBy(x => x.X)
                 .ThenBy(x => x.Y)
                 .ToList();

            int areaCount = foundAreas.Count();
            Console.WriteLine($"Total areas found: {areaCount}");
            for (int i = 0; i < areaCount; i++)
            {
                var current = foundAreas[i];
                Console.WriteLine($"Area #{i + 1} at ({current.X}, {current.Y}), size: {current.Size}");
            }
        }


        static void FindConnectedArea(int x, int y)
        {
            if (!IsOutOfBounds(x, y))
            {
                counter++;
                Mark(x, y);
                FindConnectedArea(x, y + 1);
                FindConnectedArea(x, y - 1);
                FindConnectedArea(x + 1, y);
                FindConnectedArea(x - 1, y);
            }
        }

        static void Mark(int x, int y)
        {
            matrix[x][y] = 'V';
        }

        static void Unmark(int x, int y)
        {
            matrix[x][y] = '-';
        }

        static bool IsOutOfBounds(int x, int y)
        {
            bool result =
                (y >= cols || y < 0) ||
                (x >= rows || x < 0) ||
                (matrix[x][y] == '*') ||
                (matrix[x][y] == 'V');

            return result;
        }

        static int[] FindEmptyArea()
        {
            int[] result = new int[] { -1, -1 };

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (matrix[row][col] == '-')
                    {
                        result[0] = row;
                        result[1] = col;
                        return result;
                    }
                }
            }

            return result;
        }

        static void ReadMatrix()
        {
            rows = int.Parse(Console.ReadLine());
            cols = int.Parse(Console.ReadLine());
            matrix = new char[rows][];
            for (int i = 0; i < rows; i++)
            {
                matrix[i] = Console.ReadLine().ToCharArray();
            }

        }
    }

    class Area
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public Area(int x, int y, int size)
        {
            this.X = x;
            this.Y = y;
            this.Size = size;
        }
    }
}

using System;
using System.Linq;

namespace GalacticBeacons
{
    class Program
    {

        static char[][] matrix;
        static long crosswayCount = 0;
        static void Main(string[] args)
        {
            int len = int.Parse(Console.ReadLine());

            matrix = new char[len][];

            int startRow = -1;

            int startCol = -1;

            for (int i = 0; i < len; i++)
            {
                var input = Console.ReadLine().ToCharArray();

                if (startRow == -1)
                {
                    int indexOfStart = input.ToList().IndexOf('3');
                    if (indexOfStart != -1)
                    {
                        startRow = i;
                        startCol = indexOfStart;
                    }
                }

                matrix[i] = input;
            }

            try
            {
                FindWayt(startRow, startCol);
            }
            catch (Exception)
            {

            }
            Console.WriteLine(
                crosswayCount++);
        }

        private static void FindWayt(int row, int col)
        {

            int wayCounter = 0;

            if (row > 0 && matrix[row - 1][col] != '1')
            {
                wayCounter++;
                if (matrix[row - 1][col] != '5')
                {
                    matrix[row][col] = '1';
                    FindWayt(row - 1, col);
                    matrix[row][col] = '0';
                }
            }

            if (row < matrix.GetLength(0) - 1 && matrix[row + 1][col] != '1')
            {
                wayCounter++;
                if (matrix[row + 1][col] != '5')
                {
                    matrix[row][col] = '1';
                    FindWayt(row + 1, col);
                    matrix[row][col] = '0';
                }
            }

            if (col > 0 && matrix[row][col - 1] != '1')
            {
                wayCounter++;
                if (matrix[row][col - 1] != '5')
                {
                    matrix[row][col] = '1';
                    FindWayt(row, col - 1);
                    matrix[row][col] = '0';
                }
            }

            if (col < matrix[row].Length - 1 && matrix[row][col + 1] != '1')
            {
                wayCounter++;
                if (matrix[row][col + 1] != '5')
                {
                    matrix[row][col] = '1';
                    FindWayt(row, col + 1);
                    matrix[row][col] = '0';
                }
            }

            if (wayCounter > 1)
            {
                crosswayCount++;
            }

        }
    }
}

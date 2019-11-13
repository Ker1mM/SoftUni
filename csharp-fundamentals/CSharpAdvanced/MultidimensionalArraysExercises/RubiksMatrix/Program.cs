using System;
using System.Linq;

namespace RubiksMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rowsAndColumns = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            int commandCount = int.Parse(Console.ReadLine());

            int[,] matrix = new int[rowsAndColumns[0], rowsAndColumns[1]];
            int counter = 0;
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    counter++;
                    matrix[r, c] = counter;
                }
            }

            for (int i = 0; i < commandCount; i++)
            {
                string command = Console.ReadLine();
                string[] tokens = command.Split(" ");
                int count = int.Parse(tokens[2]);

                int position = int.Parse(tokens[0]);

                switch (tokens[1])
                {
                    case "up":
                        Up(matrix, position, count % matrix.GetLength(0));

                        break;
                    case "down":
                        Down(matrix, position, count % matrix.GetLength(0));

                        break;
                    case "left":
                        Left(matrix, position, count % matrix.GetLength(1));

                        break;
                    case "right":
                        Right(matrix, position, count % matrix.GetLength(1));

                        break;
                    default:
                        break;
                }
            }

            counter = 1;
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (matrix[r, c] == counter)
                    {
                        Console.WriteLine("No swap required");
                    }
                    else
                    {
                        int[] indexes = FindReplacementIndexes(matrix, counter, r, c);
                        int temp = matrix[r, c];
                        matrix[r, c] = matrix[indexes[0], indexes[1]];
                        matrix[indexes[0], indexes[1]] = temp;
                        Console.WriteLine("Swap ({0}, {1}) with ({2}, {3})", r, c, indexes[0], indexes[1]);
                    }
                    counter++;
                }
            }
        }

        public static int[] FindReplacementIndexes(int[,] matrix, int number, int currentRow, int currentColumn)
        {
            int[] indexes = new int[2];

            for (int i = currentRow; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == number)
                    {
                        indexes[0] = i;
                        indexes[1] = j;
                        return indexes;
                    }
                }
            }
            return indexes;
        }

        public static void Up(int[,] matrix, int column, int count)
        {
            for (int counter = 0; counter < count; counter++)
            {
                int temp = matrix[0, column];

                for (int i = 0; i < matrix.GetLength(0) - 1; i++)
                {
                    matrix[i, column] = matrix[i + 1, column];
                }
                matrix[matrix.GetLength(0) - 1, column] = temp;
            }
        }

        public static void Down(int[,] matrix, int column, int count)
        {
            for (int counter = 0; counter < count; counter++)
            {
                int temp = matrix[matrix.GetLength(0) - 1, column];

                for (int i = matrix.GetLength(0) - 1; i > 0; i--)
                {
                    matrix[i, column] = matrix[i - 1, column];
                }
                matrix[0, column] = temp;
            }
        }

        public static void Left(int[,] matrix, int row, int count)
        {
            for (int counter = 0; counter < count; counter++)
            {
                int temp = matrix[row, 0];

                for (int i = 0; i < matrix.GetLength(1) - 1; i++)
                {
                    matrix[row, i] = matrix[row, i + 1];
                }
                matrix[row, matrix.GetLength(1) - 1] = temp;
            }
        }

        public static void Right(int[,] matrix, int row, int count)
        {
            for (int counter = 0; counter < count; counter++)
            {
                int temp = matrix[row, matrix.GetLength(1) - 1];

                for (int i = matrix.GetLength(1) - 1; i > 0; i--)
                {
                    matrix[row, i] = matrix[row, i - 1];
                }
                matrix[row, 0] = temp;
            }
        }
    }
}

using System;
using System.Linq;

namespace Crossfire
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dimensions = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

            string[][] matrix = new string[dimensions[0]][];

            for (int i = 0; i < dimensions[0]; i++)
            {
                matrix[i] = new string[dimensions[1]];
                for (int j = 0; j < dimensions[1]; j++)
                {
                    matrix[i][j] = (i * dimensions[1] + (j + 1)).ToString();
                }
            }

            string command = Console.ReadLine();

            while (command.Equals("Nuke it from orbit") == false)
            {
                int[] tokens = command.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                int row = tokens[0];
                int column = tokens[1];
                int radius = tokens[2];

                Destroy(matrix, row, column, radius);
                matrix = Arrange(matrix);
                command = Console.ReadLine();
            }

            for (int i = 0; i < matrix.Length; i++)
            {
                if (matrix[i].Length > 0)
                {
                    Console.WriteLine(String.Join(" ", matrix[i]));
                }
            }
        }


        public static string[][] Arrange(string[][] matrix)
        {
            int emptyRows = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                if (matrix[i].Any(x => x == ""))
                {
                    matrix[i] = matrix[i].Where(x => x != "").ToArray(); ;
                }

                if (matrix[i].Length == 0)
                {
                    emptyRows++;
                }
            }
            if (emptyRows > 0)
            {
                string[][] tempMatrix = new string[matrix.Length - emptyRows][];

                int counter = 0;
                for (int k = 0; k < matrix.Length; k++)
                {
                    if (matrix[k].Length > 0)
                    {
                        tempMatrix[counter] = matrix[k];
                        counter++;
                    }
                }
                matrix = tempMatrix;
            }
            return matrix;
        }

        public static void Destroy(string[][] matrix, int row, int column, int radius)
        {

            for (int i = row - radius; i <= row + radius; i++)
            {
                if (i >= 0 && i < matrix.Length)
                {
                    if (column >= 0 && column < matrix[i].Length)
                    {
                        matrix[i][column] = "";
                    }
                }
            }
            for (int i = column - radius; i <= column + radius; i++)
            {
                if (row >= 0 && row < matrix.Length)
                {
                    if (i >= 0 && i < matrix[row].Length)
                    {
                        matrix[row][i] = "";
                    }
                }
            }
        }
    }
}

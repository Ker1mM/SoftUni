using System;
using System.Collections.Generic;

namespace P10.ClearingCommands
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            List<char[]> matrix = new List<char[]>();
            while ((input = Console.ReadLine()) != "END")
            {
                matrix.Add(input.ToCharArray());
            }

            int rows = matrix.Count;
            for (int i = 0; i < rows; i++)
            {
                int cols = matrix[i].Length;
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i][j] == '<' ||
                            matrix[i][j] == '>' ||
                            matrix[i][j] == '^' ||
                            matrix[i][j] == 'v')
                    {
                        ExecuteCommand(matrix, i, j, matrix[i][j]);
                    }
                }
            }

            foreach (var line in matrix)
            {
                Console.Write("<p>");
                foreach (var symbol in line)
                {
                    Console.Write(System.Security.SecurityElement.Escape(symbol.ToString()));
                }
                Console.WriteLine("</p>");
            }
        }

        public static void ExecuteCommand(List<char[]> matrix, int row, int col, char command)
        {
            switch (command)
            {
                case '<':
                    while (--col >= 0 && col < matrix[row].Length)
                    {
                        if (matrix[row][col] != '<' &&
                            matrix[row][col] != '>' &&
                            matrix[row][col] != '^' &&
                            matrix[row][col] != 'v')
                        {
                            matrix[row][col] = ' ';
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                case '>':
                    while (++col >= 0 && col < matrix[row].Length)
                    {
                        if (matrix[row][col] != '<' &&
                            matrix[row][col] != '>' &&
                            matrix[row][col] != '^' &&
                            matrix[row][col] != 'v')
                        {
                            matrix[row][col] = ' ';
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                case '^':
                    while (--row >= 0 && row < matrix.Count && col < matrix[row].Length)
                    {
                        if (matrix[row][col] != '<' &&
                            matrix[row][col] != '>' &&
                            matrix[row][col] != '^' &&
                            matrix[row][col] != 'v')
                        {
                            matrix[row][col] = ' ';
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                case 'v':
                    while (++row >= 0 && row < matrix.Count && col < matrix[row].Length)
                    {
                        if (matrix[row][col] != '<' &&
                            matrix[row][col] != '>' &&
                            matrix[row][col] != '^' &&
                            matrix[row][col] != 'v')
                        {
                            matrix[row][col] = ' ';
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}

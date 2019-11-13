using System;
using System.Collections.Generic;
using System.Linq;

namespace P02.StringMatrixRotation
{
    class Program
    {
        static void Main(string[] args)
        {
            int rotations = int.Parse(String.Join("", Console.ReadLine().ToCharArray().Where(x => Char.IsDigit(x)).ToArray()));

            int maxLength = 0;

            List<string> rows = new List<string>();
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                if (input.Length > maxLength)
                {
                    maxLength = input.Length;
                }
                rows.Add(input);
            }

            char[][] matrix = new char[rows.Count][];
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new char[maxLength];
                matrix[i] = (rows[i] + new string(' ', maxLength - rows[i].Length)).ToCharArray();

            }

            int rotationCode = (rotations / 90) % 4;

            switch (rotationCode)
            {
                case 1:
                    for (int i = 0; i < maxLength; i++)
                    {
                        for (int j = matrix.Length - 1; j >= 0; j--)
                        {
                            Console.Write(matrix[j][i]);
                        }
                        Console.WriteLine();
                    }
                    break;
                case 2:
                    for (int i = matrix.Length - 1; i >= 0; i--)
                    {
                        for (int j = maxLength - 1; j >= 0; j--)
                        {
                            Console.Write(matrix[i][j]);
                        }
                        Console.WriteLine();
                    }
                    break;
                case 3:
                    for (int i = maxLength - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < matrix.Length; j++)
                        {
                            Console.Write(matrix[j][i]);
                        }
                        Console.WriteLine();
                    }
                    break;
                default:
                    for (int i = 0; i < matrix.Length; i++)
                    {
                        for (int j = 0; j < maxLength; j++)
                        {
                            Console.Write(matrix[i][j]);
                        }
                        Console.WriteLine();
                    }
                    break;

            }
        }
    }
}

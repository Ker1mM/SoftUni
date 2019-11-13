using System;
using System.Collections.Generic;

namespace AbaspaBasapa
{
    class Program
    {
        static string firstString;
        static string secondString;
        static void Main(string[] args)
        {
            firstString = Console.ReadLine();
            secondString = Console.ReadLine();

            var table = new bool[firstString.Length, secondString.Length];

            for (int row = 0; row < firstString.Length; row++)
            {
                for (int col = 0; col < secondString.Length; col++)
                {
                    if (firstString[row] == secondString[col])
                    {
                        table[row, col] = true;
                    }
                }
            }

            var locs = new List<char>();

            for (int row = 0; row < firstString.Length; row++)
            {
                for (int col = 0; col < secondString.Length; col++)
                {
                    if (table[row, col])
                    {
                        var current = GetLength(table, row, col);
                        if (current.Count > locs.Count)
                        {
                            locs = current;
                        }
                    }
                }
            }

            Console.WriteLine(new string(locs.ToArray()));
        }

        private static List<char> GetLength(bool[,] table, int row, int col)
        {
            var current = new List<char>();

            while (row < table.GetLength(0) && col < table.GetLength(1))
            {
                if (table[row, col])
                {
                    current.Add(firstString[row]);
                    row++;
                    col++;
                }
                else
                {
                    break;
                }
            }

            return current;
        }
    }
}

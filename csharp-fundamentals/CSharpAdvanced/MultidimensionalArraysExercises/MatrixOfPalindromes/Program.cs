using System;
using System.Linq;

namespace MatrixOfPalindromes
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            int[] tokens = input.Split(" ").Select(int.Parse).ToArray();

            int rows = tokens[0];
            int columns = tokens[1];

            string[,] palindromMatrix = new string[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    palindromMatrix[i, j] = ((char)('a' + i)).ToString();
                    palindromMatrix[i, j] += ((char)('a' + j + i)).ToString();
                    palindromMatrix[i, j] += ((char)('a' + i)).ToString();
                }
            }

            for (int i = 0; i < palindromMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < palindromMatrix.GetLength(1); j++)
                {
                    Console.Write("{0} ", palindromMatrix[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}

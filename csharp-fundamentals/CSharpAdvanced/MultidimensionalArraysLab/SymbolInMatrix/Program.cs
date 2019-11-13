using System;
using System.Linq;

namespace SymbolInMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int rowColumnCount = int.Parse(Console.ReadLine());
            char[,] matrix = new char[rowColumnCount, rowColumnCount];

            for (int i = 0; i < rowColumnCount; i++)
            {
                char[] currentRow = Console.ReadLine().ToCharArray();
                SetRow(matrix, currentRow, i);
            }

            char elementToFind = Console.ReadLine().ToCharArray()[0];

            int rowNumber = -1;
            int columnNumber = -1;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j].Equals(elementToFind))
                    {
                        rowNumber = i;
                        columnNumber = j;
                        break;
                    }
                }

                if (rowNumber >= 0)
                {
                    break;
                }
            }

            if (rowNumber >= 0)
            {
                Console.WriteLine("({0}, {1})", rowNumber, columnNumber);
            }
            else
            {
                Console.WriteLine("{0} does not occur in the matrix", elementToFind);
            }
        }

        public static void SetRow(char[,] matrix, char[] row, int rowNumber)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                matrix[rowNumber, i] = row[i];
            }
        }
    }
}

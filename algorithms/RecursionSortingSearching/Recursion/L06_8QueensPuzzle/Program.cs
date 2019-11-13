using System;
using System.Collections.Generic;

namespace L06_8QueensPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            EightQueens.PutQueens(0);
        }
    }

    internal class EightQueens
    {
        const int Size = 8;
        static bool[,] chessboard = new bool[Size, Size];
        internal static int solutionsFound = 0;

        static HashSet<int> attackedRows = new HashSet<int>();
        static HashSet<int> attackedColums = new HashSet<int>();
        static HashSet<int> attackedLeftDiagonals = new HashSet<int>();
        static HashSet<int> attackedRightDiagonals = new HashSet<int>();

        internal static void PutQueens(int row)
        {
            if (row == Size)
            {
                PrintSolution();
            }
            else
            {
                for (int col = 0; col < Size; col++)
                {
                    if (CanPlaceQueen(row, col))
                    {
                        MarkAllAttackedPositions(row, col);
                        PutQueens(row + 1);
                        UnmarkAllAttackedPositions(row, col);
                    }
                }
            }
        }

        static void PrintSolution()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    char result = '-';
                    if (chessboard[row, col])
                    {
                        result = '*';
                    }

                    Console.Write(result + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            solutionsFound++;
        }

        static bool CanPlaceQueen(int row, int col)
        {
            var positionOccupied =
                attackedRows.Contains(row) ||
                attackedColums.Contains(col) ||
                attackedLeftDiagonals.Contains(col - row) ||
                attackedRightDiagonals.Contains(row + col);

            return !positionOccupied;
        }

        static void MarkAllAttackedPositions(int row, int col)
        {
            attackedRows.Add(row);
            attackedColums.Add(col);
            attackedLeftDiagonals.Add(col - row);
            attackedRightDiagonals.Add(row + col);
            chessboard[row, col] = true;
        }

        static void UnmarkAllAttackedPositions(int row, int col)
        {
            attackedRows.Remove(row);
            attackedColums.Remove(col);
            attackedLeftDiagonals.Remove(col - row);
            attackedRightDiagonals.Remove(row + col);
            chessboard[row, col] = false;
        }
    }
}

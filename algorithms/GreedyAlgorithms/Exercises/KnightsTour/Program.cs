using System;

namespace KnightsTour
{
    class Program
    {
        private static int[,] board;

        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());
            board = new int[size, size];

            int row = 0;
            int col = 0;
            var nextPos = FindNextPosition(board, row, col);
            int counter = 1;
            while (nextPos[0] >= 0 && nextPos[1] >= 0)
            {
                board[row, col] = counter++;
                row = nextPos[0];
                col = nextPos[1];
                nextPos = FindNextPosition(board, row, col);
            }
            board[row, col] = counter++;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(board[i, j].ToString().PadLeft(4));
                }
                Console.WriteLine();
            }
        }

        private static int[] FindNextPosition(int[,] board, int row, int col)
        {

            int minOnwardMoves = int.MaxValue;
            int[] nextPos = new int[] { -1, -1 };

            var moves = GetMoves(row, col);
            int size = board.GetLength(0);

            foreach (var pos in moves)
            {
                if (IsValid(size, pos) && board[pos[0], pos[1]] == 0)
                {
                    int moveCount = GetPossibleMovesCount(size, pos[0], pos[1]);
                    if (moveCount < minOnwardMoves)
                    {
                        minOnwardMoves = moveCount;
                        nextPos[0] = pos[0];
                        nextPos[1] = pos[1];
                    }
                }
            }

            return nextPos;
        }

        private static int GetPossibleMovesCount(int size, int row, int col)
        {
            int[][] moves = GetMoves(row, col);

            int result = 0;
            foreach (var pos in moves)
            {
                if (IsValid(size, pos) && board[pos[0], pos[1]] == 0)
                {
                    result++;
                }
            }

            return result;
        }

        private static bool IsValid(int size, int[] pos)
        {
            int row = pos[0];
            int col = pos[1];
            bool result = row >= 0 && col >= 0 && row < size && col < size;
            return result;
        }

        private static int[][] GetMoves(int row, int col)
        {
            int[][] moves = new int[][]
            {
                new int [] {row + 1, col + 2 },
                new int [] {row - 1, col + 2 },
                new int [] {row + 1, col - 2 },
                new int [] {row - 1, col - 2 },
                new int [] {row + 2, col + 1 },
                new int [] {row + 2, col - 1 },
                new int [] {row - 2, col + 1 },
                new int [] {row - 2, col - 1 },
            };

            return moves;
        }
    }
}

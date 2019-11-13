using System;

namespace KnightGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());
            char[][] board = new char[size][];
            for (int i = 0; i < size; i++)
            {
                board[i] = Console.ReadLine().ToCharArray();
            }

            int[] indexes = GetIndex(board);
            int removed = 0;
            while (indexes[0] >= 0 && indexes[1] >= 0)
            {
                board[indexes[0]][indexes[1]] = '0';
                removed++;
                indexes = GetIndex(board);
            }

            Console.WriteLine(removed);
        }

        public static int CanAttack(char[][] board, int row, int col)
        {
            int count = 0;
            if (col + 2 < board.Length)
            {
                if (row - 1 >= 0 && board[row - 1][col + 2] == 'K')
                {
                    count++;
                }
                if (row + 1 < board.Length && board[row + 1][col + 2] == 'K')
                {
                    count++;
                }
            }
            if (col - 2 >= 0)
            {
                if (row - 1 >= 0 && board[row - 1][col - 2] == 'K')
                {
                    count++;
                }
                if (row + 1 < board.Length && board[row + 1][col - 2] == 'K')
                {
                    count++;
                }
            }

            if (row + 2 < board.Length)
            {
                if (col - 1 >= 0 && board[row + 2][col - 1] == 'K')
                {
                    count++;
                }
                if (col + 1 < board.Length && board[row + 2][col + 1] == 'K')
                {
                    count++;
                }
            }
            if (row - 2 >= 0)
            {
                if (col - 1 >= 0 && board[row - 2][col - 1] == 'K')
                {
                    count++;
                }
                if (col + 1 < board.Length && board[row - 2][col + 1] == 'K')
                {
                    count++;
                }
            }

            return count;
        }

        public static int[] GetIndex(char[][] board)
        {
            int[] result = new int[] { -1, -1 };
            int maxAttack = 0;
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board.Length; j++)
                {
                    if (board[i][j] == 'K' && CanAttack(board, i, j) > maxAttack)
                    {
                        maxAttack = CanAttack(board, i, j);
                        result[0] = i;
                        result[1] = j;
                    }
                }
            }
            return result;
        }
    }
}

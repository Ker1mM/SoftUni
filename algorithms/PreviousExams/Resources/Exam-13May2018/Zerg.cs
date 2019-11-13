using System;
using System.Numerics;
using System.Text;

public class Program
{
    public static void Main()
    {
        var lineParts = Console.ReadLine().Split(' ');
        int N = int.Parse(lineParts[0]);
        int M = int.Parse(lineParts[1]);
        lineParts = Console.ReadLine().Split(' ');
        int Fx = int.Parse(lineParts[0]);
        int Fy = int.Parse(lineParts[1]);
        int K = int.Parse(Console.ReadLine());

        BigInteger[,] board = new BigInteger[N, M];
        for (int i = 0; i < K; i++)
        {
            lineParts = Console.ReadLine().Split(' ');
            int x = int.Parse(lineParts[0]);
            int y = int.Parse(lineParts[1]);
            board[x, y] = -1;
        }

        bool doge = false;
        for (int i = 0; i < N; i++)
        {
            if (board[i, 0] != -1 && !doge)
            {
                board[i, 0] = 1;
            }
            else
            {
                doge = true;
                board[i, 0] = 0;
            }
        }

        doge = false;
        for (int i = 0; i < M; i++)
        {
            if (board[0, i] != -1 && !doge)
            {
                board[0, i] = 1;
            }
            else
            {
                doge = true;
                board[0, i] = 0;
            }
        }

        for (int i = 1; i < N; i++)
        {
            for (int j = 1; j < M; j++)
            {
                if (board[i, j] != -1)
                {
                    board[i, j] = board[i - 1, j] + board[i, j - 1];
                }
                else
                {
                    board[i, j] = 0;
                }
            }
        }

        Console.WriteLine(board[Fx, Fy]);
    }
}
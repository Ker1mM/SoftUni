using System;

namespace P06_Sneaking
{
    public class StartUp
    {
        static char[][] room;
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            room = new char[n][];

            for (int row = 0; row < n; row++)
            {
                room[row] = Console.ReadLine().ToCharArray();
            }

            var moves = Console.ReadLine().ToCharArray();
            int[] samPosition = Methods.GetSamPosition(room);

            for (int i = 0; i < moves.Length; i++)
            {
                Methods.MoveEnemies(room);

                if (Methods.CheckIfSamIsDead(room, samPosition))
                {
                    Environment.Exit(0);
                }

                Methods.MoveSam(room, samPosition, moves[i]);
                if (Methods.CheckForNikoladze(room, samPosition))
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}

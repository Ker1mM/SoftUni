using System;

namespace P06_Sneaking
{
    public static class Methods
    {
        public static int[] GetSamPosition(char[][] room)
        {
            int[] samPosition = new int[2];
            for (int row = 0; row < room.Length; row++)
            {
                for (int col = 0; col < room[row].Length; col++)
                {
                    if (room[row][col] == 'S')
                    {
                        samPosition[0] = row;
                        samPosition[1] = col;
                    }
                }
            }
            return samPosition;
        }

        public static void PrintRoom(char[][] room)
        {
            for (int row = 0; row < room.Length; row++)
            {
                for (int col = 0; col < room[row].Length; col++)
                {
                    Console.Write(room[row][col]);
                }
                Console.WriteLine();
            }
        }

        public static int[] GetEnemyPosition(char[][] room, int[] samPosition)
        {
            int[] getEnemy = new int[2];
            for (int j = 0; j < room[samPosition[0]].Length; j++)
            {
                if (room[samPosition[0]][j] != '.' && room[samPosition[0]][j] != 'S')
                {
                    getEnemy[0] = samPosition[0];
                    getEnemy[1] = j;
                }
            }
            return getEnemy;
        }

        public static bool CheckIfSamIsDead(char[][] room, int[] samPosition)
        {
            bool result = false;
            int[] getEnemy = GetEnemyPosition(room, samPosition);

            if (
               ((samPosition[1] < getEnemy[1] &&
                room[getEnemy[0]][getEnemy[1]] == 'd') ||
                (getEnemy[1] < samPosition[1] &&
                room[getEnemy[0]][getEnemy[1]] == 'b')) &&
                getEnemy[0] == samPosition[0])
            {
                room[samPosition[0]][samPosition[1]] = 'X';
                Console.WriteLine($"Sam died at {samPosition[0]}, {samPosition[1]}");
                PrintRoom(room);
                result = true;
            }

            return result;
        }

        public static void MoveSam(char[][] room, int[] samPosition, char move)
        {
            room[samPosition[0]][samPosition[1]] = '.';
            switch (move)
            {
                case 'U':
                    samPosition[0]--;
                    break;
                case 'D':
                    samPosition[0]++;
                    break;
                case 'L':
                    samPosition[1]--;
                    break;
                case 'R':
                    samPosition[1]++;
                    break;
                default:
                    break;
            }
            room[samPosition[0]][samPosition[1]] = 'S';
        }

        public static bool CheckForNikoladze(char[][] room, int[] samPosition)
        {
            bool result = false;
            int[] getEnemy = GetEnemyPosition(room, samPosition);

            if (room[getEnemy[0]][getEnemy[1]] == 'N' && samPosition[0] == getEnemy[0])
            {
                room[getEnemy[0]][getEnemy[1]] = 'X';
                Console.WriteLine("Nikoladze killed!");
                PrintRoom(room);
                result = true;
            }

            return result;
        }

        public static void MoveEnemies(char[][] room)
        {
            for (int row = 0; row < room.Length; row++)
            {
                for (int col = 0; col < room[row].Length; col++)
                {
                    if (room[row][col] == 'b')
                    {
                        if (row >= 0 && row < room.Length && col + 1 >= 0 && col + 1 < room[row].Length)
                        {
                            room[row][col] = '.';
                            room[row][col + 1] = 'b';
                            col++;
                        }
                        else
                        {
                            room[row][col] = 'd';
                        }
                    }
                    else if (room[row][col] == 'd')
                    {
                        if (row >= 0 && row < room.Length && col - 1 >= 0 && col - 1 < room[row].Length)
                        {
                            room[row][col] = '.';
                            room[row][col - 1] = 'd';
                        }
                        else
                        {
                            room[row][col] = 'b';
                        }
                    }
                }
            }
        }
    }
}

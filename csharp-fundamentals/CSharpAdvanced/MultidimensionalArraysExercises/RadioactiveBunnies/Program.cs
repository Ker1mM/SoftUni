using System;
using System.Linq;

namespace RadioactiveBunnies
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            int[] rowsAndColumns = input.Split(" ").Select(int.Parse).ToArray();
            int rows = rowsAndColumns[0];
            int columns = rowsAndColumns[1];

            char[][] lair = new char[rows][];

            for (int i = 0; i < rows; i++)
            {
                lair[i] = Console.ReadLine().ToCharArray();
            }

            char[] commands = Console.ReadLine().ToCharArray();

            int[] playerCoordinates = FindPlayerCoordinates(lair);
            bool hasEscaped = false;
            bool isDead = false;
            for (int i = 0; i < commands.Length; i++)
            {
                switch (commands[i])
                {
                    case 'U':
                        if (playerCoordinates[0] == 0)
                        {
                            hasEscaped = true;
                            lair[playerCoordinates[0]][playerCoordinates[1]] = '.';
                        }
                        else if (lair[playerCoordinates[0] - 1][playerCoordinates[1]] == 'B')
                        {
                            playerCoordinates[0] -= 1;
                            isDead = true;
                        }
                        else
                        {
                            lair[playerCoordinates[0]][playerCoordinates[1]] = '.';
                            playerCoordinates[0] -= 1;
                            lair[playerCoordinates[0]][playerCoordinates[1]] = 'P';
                        }
                        break;
                    case 'D':
                        if (playerCoordinates[0] >= lair.Length - 1)
                        {
                            hasEscaped = true;
                            lair[playerCoordinates[0]][playerCoordinates[1]] = '.';
                        }
                        else if (lair[playerCoordinates[0] + 1][playerCoordinates[1]] == 'B')
                        {
                            playerCoordinates[0] += 1;
                            isDead = true;
                        }
                        else
                        {
                            lair[playerCoordinates[0]][playerCoordinates[1]] = '.';
                            playerCoordinates[0] += 1;
                            lair[playerCoordinates[0]][playerCoordinates[1]] = 'P';
                        }
                        break;
                    case 'L':
                        if (playerCoordinates[1] == 0)
                        {
                            hasEscaped = true;
                            lair[playerCoordinates[0]][playerCoordinates[1]] = '.';
                        }
                        else if (lair[playerCoordinates[0]][playerCoordinates[1] - 1] == 'B')
                        {
                            playerCoordinates[1] -= 1;
                            isDead = true;
                        }
                        else
                        {
                            lair[playerCoordinates[0]][playerCoordinates[1]] = '.';
                            playerCoordinates[1] -= 1;
                            lair[playerCoordinates[0]][playerCoordinates[1]] = 'P';
                        }
                        break;
                    case 'R':
                        if (playerCoordinates[1] >= lair[playerCoordinates[0]].Length - 1)
                        {
                            hasEscaped = true;
                            lair[playerCoordinates[0]][playerCoordinates[1]] = '.';
                        }
                        else if (lair[playerCoordinates[0]][playerCoordinates[1] + 1] == 'B')
                        {
                            playerCoordinates[1] += 1;
                            isDead = true;
                        }
                        else
                        {
                            lair[playerCoordinates[0]][playerCoordinates[1]] = '.';
                            playerCoordinates[1] += 1;
                            lair[playerCoordinates[0]][playerCoordinates[1]] = 'P';
                        }
                        break;
                    default:
                        break;
                }
                isDead = SpreadTheBunnies(lair);
                if (isDead || hasEscaped)
                {
                    break;
                }
            }
            for (int j = 0; j < lair.Length; j++)
            {
                Console.WriteLine(String.Join("", lair[j]));
            }
            if (isDead)
            {
                Console.WriteLine("dead: {0} {1}", playerCoordinates[0], playerCoordinates[1]);
            }
            else
            {
                Console.WriteLine("won: {0} {1}", playerCoordinates[0], playerCoordinates[1]);
            }
        }

        public static int[] FindPlayerCoordinates(char[][] lair)
        {
            int[] coordinates = new int[2];

            for (int i = 0; i < lair.Length; i++)
            {
                for (int j = 0; j < lair[i].Length; j++)
                {
                    if (lair[i][j] == 'P')
                    {
                        coordinates[0] = i;
                        coordinates[1] = j;
                        return coordinates;
                    }
                }
            }

            return coordinates;
        }

        public static bool SpreadTheBunnies(char[][] lair)
        {
            bool result = false;

            for (int i = 0; i < lair.Length; i++)
            {
                for (int j = 0; j < lair[i].Length; j++)
                {
                    if (lair[i][j] == 'B')
                    {
                        lair[i][j] = 'O';
                    }
                }
            }

            for (int i = 0; i < lair.Length; i++)
            {
                for (int j = 0; j < lair[i].Length; j++)
                {
                    if (lair[i][j] == 'O')
                    {
                        if (j - 1 >= 0 && lair[i][j - 1] != 'O')
                        {
                            if (lair[i][j - 1] == 'P')
                            {
                                lair[i][j - 1] = 'B';
                                result = true;
                            }
                            else
                            {
                                lair[i][j - 1] = 'B';
                            }
                        }
                        if (i - 1 >= 0 && lair[i - 1][j] != 'O')
                        {
                            if (lair[i - 1][j] == 'P')
                            {
                                lair[i - 1][j] = 'B';
                                result = true;
                            }
                            else
                            {
                                lair[i - 1][j] = 'B';
                            }
                        }
                        if (j + 1 < lair[i].Length && lair[i][j + 1] != 'O')
                        {
                            if (lair[i][j + 1] == 'P')
                            {
                                lair[i][j + 1] = 'B';
                                result = true;
                            }
                            else
                            {
                                lair[i][j + 1] = 'B';
                            }
                        }
                        if (i + 1 < lair.Length && lair[i + 1][j] != 'O')
                        {
                            if (lair[i + 1][j] == 'P')
                            {
                                lair[i + 1][j] = 'B';
                                result = true;
                            }
                            else
                            {
                                lair[i + 1][j] = 'B';
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < lair.Length; i++)
            {
                for (int j = 0; j < lair[i].Length; j++)
                {
                    if (lair[i][j] == 'O')
                    {
                        lair[i][j] = 'B';
                    }
                }
            }

            return result;
        }
    }
}

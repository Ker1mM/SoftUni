using System;

namespace TheHeiganDance
{
    class Program
    {
        static void Main(string[] args)
        {
            double damage = double.Parse(Console.ReadLine());

            int playerHP = 18500;
            double heiganHP = 3000000;

            int[] playerPosition = new int[] { 7, 7 };

            string lastSpell = "";
            bool hasCloudDOT = false;
            while (true)
            {
                string input = Console.ReadLine();
                string[] tokens = input.Split();
                int row = int.Parse(tokens[1]);
                int col = int.Parse(tokens[2]);

                heiganHP -= damage;

                if (hasCloudDOT)
                {
                    playerHP -= 3500;
                    hasCloudDOT = false;
                    if (playerHP <= 0)
                    {
                        lastSpell = "Plague Cloud";
                        break;
                    }
                }

                if (heiganHP <= 0)
                {
                    break;
                }

                int[] playerNewPosition = PlayerCanDodge(playerPosition, row, col);

                if (playerNewPosition[2] == 0)
                {

                    if (tokens[0].Equals("Cloud"))
                    {
                        playerHP -= 3500;
                        hasCloudDOT = true;
                        if (playerHP <= 0)
                        {
                            lastSpell = "Plague Cloud";
                            break;
                        }
                    }
                    else if (tokens[0].Equals("Eruption"))
                    {
                        playerHP -= 6000;
                        if (playerHP <= 0)
                        {
                            lastSpell = "Eruption";
                            break;
                        }
                    }
                }
                else
                {
                    playerPosition[0] = playerNewPosition[0];
                    playerPosition[1] = playerNewPosition[1];
                }
            }

            if (heiganHP <= 0)
            {
                Console.WriteLine("Heigan: Defeated!");
            }
            else
            {
                Console.WriteLine("Heigan: {0:f2}", heiganHP);
            }

            if (playerHP <= 0)
            {
                Console.WriteLine("Player: Killed by {0}", lastSpell);
            }
            else
            {
                Console.WriteLine("Player: {0}", playerHP);
            }
            Console.WriteLine("Final position: {0}, {1}", playerPosition[0], playerPosition[1]);


        }

        public static int[] PlayerCanDodge(int[] playerPos, int row, int col)
        {
            int[] playerNewPos = new int[3];
            playerNewPos[0] = playerPos[0];
            playerNewPos[1] = playerPos[1];
            playerNewPos[2] = 1;
            if (IsInRadius(playerPos[0], playerPos[1], row, col))
            {
                playerNewPos[2] = 0;
                if (playerPos[0] > 0 && IsInRadius(playerPos[0] - 1, playerPos[1], row, col) == false)
                {
                    playerNewPos[0] -= 1;
                    playerNewPos[2] = 1;
                }
                else if (playerPos[1] < 14 && IsInRadius(playerPos[0], playerPos[1] + 1, row, col) == false)
                {
                    playerNewPos[1] += 1;
                    playerNewPos[2] = 1;
                }
                else if (playerPos[0] < 14 && IsInRadius(playerPos[0] + 1, playerPos[1], row, col) == false)
                {
                    playerNewPos[0] += 1;
                    playerNewPos[2] = 1;
                }
                else if (playerPos[1] > 0 && IsInRadius(playerPos[0], playerPos[1] - 1, row, col) == false)
                {
                    playerNewPos[1] -= 1;
                    playerNewPos[2] = 1;
                }
            }

            return playerNewPos;
        }

        public static bool IsInRadius(int playerRow, int playerCol, int row, int col)
        {
            bool result = false;

            if (playerRow == row || playerRow == row + 1 || playerRow == row - 1)
            {
                if (playerCol == col || playerCol == col + 1 || playerCol == col - 1)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}

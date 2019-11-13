using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace P21.ITVillage
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine().Replace(" ", "");
            string[] playboard = Regex.Split(input, @"\|");
            int[] position = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] dice = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int coins = 50;
            int totalInns = input.ToCharArray().Where(x => x == 'I').Count();
            int playerInns = 0;

            bool playerWon = false;
            bool nakovFound = false;
            for (int i = 0; i < dice.Length; i++)
            {
                int diceRoll = dice[i];
                coins += playerInns * 20;
                position = RollTheDice(diceRoll, position);

                switch (playboard[position[0] - 1][position[1] - 1])
                {
                    case 'P':
                        coins -= 5;
                        break;
                    case 'I':
                        if (coins >= 100)
                        {
                            coins -= 100;
                            playerInns++;
                        }
                        else
                        {
                            coins -= 10;
                        }
                        break;
                    case 'F':
                        coins += 20;
                        break;
                    case 'S':
                        i += 2;
                        break;
                    case 'V':
                        coins *= 10;
                        break;
                    case 'N':
                        nakovFound = true;
                        break;
                    default:
                        break;
                }

                if (nakovFound || playerInns >= totalInns)
                {
                    playerWon = true;
                    break;
                }
                else if (coins < 0)
                {
                    break;
                }
            }

            if (playerWon)
            {
                if (nakovFound)
                {
                    Console.WriteLine("<p>You won! Nakov's force was with you!<p>");
                }
                else if (playerInns >= totalInns)
                {
                    Console.WriteLine("<p>You won! You own the village now! You have {0} coins!<p>", coins);
                }
            }
            else
            {
                if (coins < 0)
                {
                    Console.WriteLine("<p>You lost! You ran out of money!<p>");
                }
                else
                {
                    Console.WriteLine("<p>You lost! No more moves! You have {0} coins!<p>", coins);
                }
            }
        }

        public static int[] RollTheDice(int diceRoll, int[] position)
        {
            int row = position[0];
            int col = position[1];
            while (diceRoll-- > 0)
            {
                if ((row == 1 || row == 2 || row == 3) && col == 4)
                {
                    row++;
                }
                else if ((row == 4 || row == 3 | row == 2) && col == 1)
                {
                    row--;
                }
                else if ((col == 1 || col == 2 || col == 3) && row == 1)
                {
                    col++;
                }
                else if ((col == 3 || col == 2 || col == 4) && row == 4)
                {
                    col--;
                }
            }
            return new int[] { row, col };
        }
    }
}

using System;
using System.Linq;

namespace Monopoly
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dimensions = Console.ReadLine().Split().Select(int.Parse).ToArray();

            char[][] field = new char[dimensions[0]][];
            for (int i = 0; i < dimensions[0]; i++)
            {
                field[i] = Console.ReadLine().ToCharArray();
            }

            int totalMoney = 50;
            int hotelCount = 0;
            int turnCounter = 0;
            for (int i = 0; i < dimensions[0]; i++)
            {
                int col = i % 2 == 0 ? 0 : dimensions[1] - 1;
                int iterator = i % 2 == 0 ? 1 : -1;
                int counter = 0;
                while (counter++ < dimensions[1])
                {
                    switch (field[i][col])
                    {
                        case 'H':
                            hotelCount++;
                            Console.WriteLine("Bought a hotel for {0}. Total hotels: {1}.", totalMoney, hotelCount);
                            totalMoney = 0; ;
                            break;
                        case 'J':
                            Console.WriteLine("Gone to jail at turn {0}.", turnCounter);
                            totalMoney += hotelCount * 20;
                            turnCounter += 2;
                            break;
                        case 'S':
                            int moneySpent = totalMoney; ;
                            if ((i + 1) * (col + 1) < totalMoney)
                            {
                                moneySpent = (i + 1) * (col + 1);
                            }
                            Console.WriteLine("Spent {0} money at the shop.", moneySpent);
                            totalMoney -= moneySpent;
                            break;
                        case 'F':
                        default:
                            break;
                    }
                    totalMoney += hotelCount * 10;
                    col += iterator;
                    turnCounter++;
                }
            }

            Console.WriteLine("Turns {0}", turnCounter);
            Console.WriteLine("Money {0}", totalMoney);
        }
    }
}

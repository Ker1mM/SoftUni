using System;
using System.Collections.Generic;
using System.Linq;

namespace P16.Parachute
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> map = new List<string>();

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                map.Add(input);
            }


            int col = -1;
            int row = -1;

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == 'o')
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
                if (col > -1)
                {
                    break;
                }
            }

            for (row += 1; row < map.Count; row++)
            {
                int power = WindPower(map[row]);
                col += power;

                //col = col < 0 ? 0 : col;

                char landingSpot = map[row][col];

                if (landingSpot == '/' || landingSpot == '\\' || landingSpot == '|')
                {
                    Console.WriteLine("Got smacked on the rock like a dog!");
                    break;
                }
                else if (landingSpot == '~')
                {
                    Console.WriteLine("Drowned in the water like a cat!");
                    break;
                }
                else if (landingSpot == '_')
                {
                    Console.WriteLine("Landed on the ground like a boss!");
                    break;
                }
            }
            Console.WriteLine($"{row} {col}");
        }

        public static int WindPower(string windRow)
        {
            int power = 0;
            foreach (var symbol in windRow)
            {
                if (symbol == '>')
                {
                    power++;
                }
                else if (symbol == '<')
                {
                    power--;
                }
            }
            return power;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Wardrobe
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputCount = int.Parse(Console.ReadLine());

            Dictionary<string, Dictionary<string, int>> wardrobe = new Dictionary<string, Dictionary<string, int>>();

            while (inputCount-- > 0)
            {
                string input = Console.ReadLine();
                string[] tokens = input.Split(" -> ");
                string color = tokens[0];
                if (!wardrobe.ContainsKey(color))
                {
                    wardrobe.Add(color, new Dictionary<string, int>());
                }

                string[] clothes = tokens[1].Split(",");
                foreach (var cloth in clothes)
                {
                    if (!wardrobe[color].ContainsKey(cloth))
                    {
                        wardrobe[color].Add(cloth, 0);
                    }
                    wardrobe[color][cloth]++;
                }
            }

            string wantedInput = Console.ReadLine();
            string[] wantedTokens = wantedInput.Split(" ");

            string wantedColor = wantedTokens[0];
            string wantedCloth = wantedTokens[1];

            foreach (var color in wardrobe)
            {
                Console.WriteLine("{0} clothes:", color.Key);
                foreach (var clothes in color.Value)
                {
                    Console.Write("* {0} - {1}", clothes.Key, clothes.Value);
                    if (color.Key == wantedColor && clothes.Key == wantedCloth)
                    {
                        Console.Write(" (found!)");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}

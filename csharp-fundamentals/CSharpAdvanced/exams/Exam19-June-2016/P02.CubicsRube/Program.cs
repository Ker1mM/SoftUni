using System;
using System.Collections.Generic;

namespace P02.CubicsRube
{
    class Program
    {
        static void Main(string[] args)
        {
            int dimension = int.Parse(Console.ReadLine());

            string input;
            long sum = 0;
            int unaffectedCells = dimension * dimension * dimension;
            List<string> affectedCells = new List<string>();
            while ((input = Console.ReadLine()) != "Analyze")
            {
                string[] tokens = input.Split();
                int x = int.Parse(tokens[0]);
                int y = int.Parse(tokens[1]);
                int z = int.Parse(tokens[2]);
                int particles = int.Parse(tokens[3]);

                if (IsInside(dimension, x, y, z) && particles != 0)
                {
                    sum += particles;
                    unaffectedCells--;
                }
            }
            Console.WriteLine($"{sum}");
            Console.WriteLine($"{unaffectedCells}");
        }

        public static bool IsInside(int dimension, int x, int y, int z)
        {
            return (x >= 0 && x < dimension) && (y >= 0 && y < dimension) && (z >= 0 && z < dimension);
        }
    }
}

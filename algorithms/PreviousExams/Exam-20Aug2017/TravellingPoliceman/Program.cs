using System;
using System.Collections.Generic;
using System.Linq;

namespace TravellingPoliceman
{
    class Program
    {
        static int fuelLeft;
        static List<Street> streets;

        static void Main(string[] args)
        {
            fuelLeft = int.Parse(Console.ReadLine());
            var input = Console.ReadLine();

            streets = new List<Street>();
            while (input != "End")
            {
                var inputArgs = input.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                var street = new Street(inputArgs);
                if (street.Value >= 0)
                {
                    streets.Add(street);
                }
                input = Console.ReadLine();
            }

            streets = streets.Where(x => x.Value > 0).ToList();

            int[,] mat = new int[streets.Count + 1, fuelLeft + 1];

            var visitedStreets = new List<Street>();
            FillMatrix(mat);
            GetVisitedStreets(mat, visitedStreets);
            visitedStreets.Reverse();

            Console.WriteLine(string.Join(" -> ", visitedStreets));
            Console.WriteLine($"Total pokemons caught -> {visitedStreets.Sum(x => x.PokemonCount)}");
            Console.WriteLine($"Total car damage -> {visitedStreets.Sum(x => x.CarDamage)}");
            Console.WriteLine($"Fuel Left -> {fuelLeft - visitedStreets.Sum(x => x.Length)}");
        }

        private static void GetVisitedStreets(int[,] mat, List<Street> visited)
        {
            int cols = mat.GetLength(1) - 1;
            int rows = mat.GetLength(0) - 1;

            int res = mat[rows, cols];

            int startRow = rows;
            int startCol = cols;

            for (int i = rows; i > 0 && res > 0; i--)
            {
                if (res == mat[i - 1, startCol])
                {
                    continue;
                }
                else
                {
                    var street = streets[i - 1];
                    visited.Add(street);
                    res -= street.Value;
                    startCol -= street.Length;

                }
            }
        }

        private static void FillMatrix(int[,] matrix)
        {
            int cols = matrix.GetLength(1) - 1;
            int rows = matrix.GetLength(0) - 1;

            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    if (streets[i - 1].Length <= j)
                    {
                        matrix[i, j] = Math.Max(streets[i - 1].Value + matrix[i - 1, j - streets[i - 1].Length], matrix[i - 1, j]);
                    }
                    else
                    {
                        matrix[i, j] = matrix[i - 1, j];
                    }
                }
            }
        }
    }

    class Street
    {
        public string Name { get; set; }
        public int CarDamage { get; set; }
        public int PokemonCount { get; set; }
        public int Length { get; set; }

        public int Value => (PokemonCount * 10) - CarDamage;

        public Street(string[] inputArgs)
        {
            this.Name = inputArgs[0];
            this.CarDamage = int.Parse(inputArgs[1]);
            this.PokemonCount = int.Parse(inputArgs[2]);
            this.Length = int.Parse(inputArgs[3]);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

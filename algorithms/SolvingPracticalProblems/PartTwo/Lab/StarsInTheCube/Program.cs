using System;
using System.Collections.Generic;
using System.Linq;

namespace StarsInTheCube
{
    class Program
    {
        static char[][][] cube;
        static Dictionary<char, int> stars;

        static void Main(string[] args)
        {
            int cubeSide = int.Parse(Console.ReadLine());
            cube = new char[cubeSide][][];

            for (int i = 0; i < cubeSide; i++)
            {
                cube[i] = new char[cubeSide][];
            }

            for (int i = 0; i < cubeSide; i++)
            {
                var inputArgs = Console.ReadLine().Replace(" ", "").Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                for (int k = 0; k < cubeSide; k++)
                {
                    cube[k][i] = inputArgs[k].ToCharArray();

                }
            }

            stars = new Dictionary<char, int>();
            for (int z = 1; z < cubeSide - 1; z++)
            {
                for (int y = 1; y < cubeSide - 1; y++)
                {
                    for (int x = 1; x < cubeSide - 1; x++)
                    {
                        AddIfStar(x, y, z);
                    }
                }
            }

            Print();
        }

        private static void Print()
        {
            Console.WriteLine(stars.Sum(x => x.Value));
            foreach (var star in stars.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{star.Key} -> {star.Value}");
            }
        }

        private static void AddIfStar(int x, int y, int z)
        {
            char middle = cube[z][y][x];

            char left = cube[z][y][x - 1];
            char right = cube[z][y][x + 1];
            char front = cube[z][y - 1][x];
            char back = cube[z][y + 1][x];
            char top = cube[z + 1][y][x];
            char bottom = cube[z - 1][y][x];

            bool isStar =
                middle == left &&
                left == right &&
                right == front &&
                front == back &&
                back == top &&
                top == bottom;

            if (isStar)
            {
                if (!stars.ContainsKey(middle))
                {
                    stars.Add(middle, 0);
                }
                stars[middle]++;
            }
        }
    }
}

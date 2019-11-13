using System;
using System.Collections.Generic;

namespace ShortestPath
{
    class Program
    {
        static char[] map;
        static List<string> maps;

        static void Main(string[] args)
        {
            map = Console.ReadLine().ToCharArray();
            maps = new List<string>();

            char[] vector = new char[map.Length];
            ChooseDirection(0, vector);
            Console.WriteLine(maps.Count);
            Console.WriteLine(string.Join(Environment.NewLine, maps));
        }

        private static void ChooseDirection(int index, char[] vector)
        {
            if (index == map.Length)
            {
                maps.Add(string.Join("", vector));
                return;
            }

            if (map[index] == '*')
            {
                vector[index] = 'L';
                ChooseDirection(index + 1, vector);
                vector[index] = 'R';
                ChooseDirection(index + 1, vector);
                vector[index] = 'S';
                ChooseDirection(index + 1, vector);
            }
            else
            {
                vector[index] = map[index];
                ChooseDirection(index + 1, vector);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Miner
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());
            string[] commands = Console.ReadLine().Split();

            List<KeyValuePair<int, int>> coals = new List<KeyValuePair<int, int>>();
            List<KeyValuePair<int, int>> ends = new List<KeyValuePair<int, int>>();
            var miner = new long[2];

            for (int i = 0; i < size; i++)
            {
                string input = Console.ReadLine().Replace(" ", "");
                int minerIndex = input.IndexOf('s');
                if (minerIndex != -1)
                {
                    miner[0] = i;
                    miner[1] = minerIndex;

                }
                foreach (var coal in GetCoals(input))
                {
                    coals.Add(new KeyValuePair<int, int>(i, coal));
                }
                foreach (var end in GetEnds(input))
                {
                    ends.Add(new KeyValuePair<int, int>(i, end));
                }
            }

            long totalCoals = 0;
            bool gameOver = false;
            foreach (var command in commands)
            {
                switch (command)
                {
                    case "left":
                        if (miner[1] - 1 >= 0)
                        {
                            miner[1]--;
                        }
                        break;
                    case "right":
                        if (miner[1] + 1 < size)
                        {
                            miner[1]++;
                        }
                        break;
                    case "up":
                        if (miner[0] - 1 >= 0)
                        {
                            miner[0]--;
                        }
                        break;
                    case "down":
                        if (miner[0] + 1 < size)
                        {
                            miner[0]++;
                        }
                        break;
                    default:
                        break;
                }

                int index = ends.FindIndex(x => x.Key == miner[0] && x.Value == miner[1]);
                if (index != -1)
                {
                    Console.WriteLine($"Game over! ({miner[0]}, {miner[1]})");
                    gameOver = true;
                    break;
                }
                index = coals.FindIndex(x => x.Key == miner[0] && x.Value == miner[1]);
                if (index != -1)
                {
                    coals.RemoveAt(index);
                    totalCoals++;
                }
                if (coals.Count == 0)
                {
                    Console.WriteLine($"You collected all coals! ({miner[0]}, {miner[1]})");
                    gameOver = true;
                    break;
                }
            }
            if (!gameOver)
            {
                Console.WriteLine($"{coals.Count} coals left. ({miner[0]}, {miner[1]})");
            }
        }

        public static List<int> GetCoals(string line)
        {
            var result = new List<int>();
            int index;
            while ((index = line.IndexOf('c')) != -1)
            {
                result.Add(index);
                Regex rgx = new Regex("c");
                line = rgx.Replace(line, "*", 1);
            }

            return result;
        }

        public static List<int> GetEnds(string line)
        {
            var result = new List<int>();
            int index;
            while ((index = line.IndexOf('e')) != -1)
            {
                result.Add(index);
                Regex rgx = new Regex("e");
                line = rgx.Replace(line, "*", 1);
            }

            return result;
        }
    }
}

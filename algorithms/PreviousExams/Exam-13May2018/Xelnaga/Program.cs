using System;
using System.Collections.Generic;
using System.Linq;

namespace Xelnaga
{
    class Program
    {
        static void Main(string[] args)
        {
            var answers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            var asked = new Dictionary<int, int>();
            int speciesCount = 0;

            for (int i = 0; i < answers.Length - 1; i++)
            {
                int current = answers[i];

                if (!asked.ContainsKey(current))
                {
                    speciesCount += current + 1;
                    asked.Add(current, current);
                }
                else if (asked[current] == 0)
                {
                    speciesCount += current + 1;
                    asked[current] = current;
                }
                else
                {
                    asked[current]--;
                }
            }

            Console.WriteLine(speciesCount);
        }
    }
}

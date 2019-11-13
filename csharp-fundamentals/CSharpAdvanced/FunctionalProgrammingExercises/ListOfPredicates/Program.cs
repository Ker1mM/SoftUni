using System;
using System.Collections.Generic;
using System.Linq;

namespace ListOfPredicates
{
    class Program
    {
        static void Main(string[] args)
        {
            int rangeEnd = int.Parse(Console.ReadLine());
            int[] dividers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Func<int, bool> conditionChecker = x =>
            {
                bool result = true;
                foreach (var number in dividers)
                {
                    if (x % number != 0)
                    {
                        result = false;
                        break;
                    }
                }
                return result;

            };

            Action<int, Func<int, bool>> Printer = (x, y) =>
            {
                List<int> result = new List<int>();
                for (int i = 1; i <= x; i++)
                {
                    if (y(i))
                    {
                        result.Add(i);
                    }
                }
                Console.WriteLine(String.Join(" ", result));
            };

            Printer(rangeEnd, conditionChecker);
        }
    }
}

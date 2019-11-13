using System;
using System.Collections.Generic;
using System.Linq;

namespace EvenTimes
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputCount = int.Parse(Console.ReadLine());

            Dictionary<int, int> count = new Dictionary<int, int>();

            while (inputCount-- > 0)
            {
                int input = int.Parse(Console.ReadLine());

                if (!count.ContainsKey(input))
                {
                    count.Add(input, 0);
                }
                count[input]++;
            }

            int result = count
                .FirstOrDefault(x => x.Value % 2 == 0)
                .Key;

            Console.WriteLine(result);
        }
    }
}

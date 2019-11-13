using System;
using System.Collections.Generic;

namespace Graph
{
    class Program
    {
        static long[] results;
        private static long CountHandshakes(int num)
        {
            long res = 0;
            for (int i = 1; i < num; i += 2)
            {
                res += results[i - 1] * results[num - 1 - i];
            }

            return res;
        }

        static void Main(string[] args)
        {
            int ppl = int.Parse(Console.ReadLine());
            results = new long[ppl + 1];
            results[2] = 1;
            results[0] = 1;

            for (int i = 4; i <= ppl; i += 2)
            {
                results[i] = CountHandshakes(i);
            }
            Console.WriteLine(results[ppl]);

        }
    }
}

using System;
using System.Collections.Generic;

namespace CountSymbols
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            char[] symbols = input.ToCharArray();

            SortedDictionary<char, int> count = new SortedDictionary<char, int>();

            foreach (var symbol in symbols)
            {
                if (!count.ContainsKey(symbol))
                {
                    count.Add(symbol, 0);
                }
                count[symbol]++;
            }

            foreach (var symbol in count)
            {
                Console.WriteLine("{0}: {1} time/s", symbol.Key, symbol.Value);
            }
        }
    }
}

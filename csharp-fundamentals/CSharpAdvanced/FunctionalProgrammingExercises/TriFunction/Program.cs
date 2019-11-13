using System;
using System.Collections.Generic;
using System.Linq;

namespace TriFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            int limit = int.Parse(Console.ReadLine());
            List<string> names = Console.ReadLine().Split().ToList();

            Func<string, int> SymbolSum = x =>
            {
                int result = 0;
                for (int i = 0; i < x.Length; i++)
                {
                    result += x[i];
                }
                return result;
            };

            Predicate<string> IsValidName = x =>
            {
                return SymbolSum(x) >= limit;
            };

            PrintCorrectName(names, IsValidName);
        }

        public static void PrintCorrectName(List<string> names, Predicate<string> Checker)
        {
            foreach (var name in names)
            {
                if (Checker(name))
                {
                    Console.WriteLine(name);
                    break;
                }
            }
        }
    }
}

using System;
using System.Linq;

namespace PredicateForNames
{
    class Program
    {
        static void Main(string[] args)
        {
            int condition = int.Parse(Console.ReadLine());
            Predicate<string> Sorter = n => n.Length <= condition;
            var names = Console.ReadLine().Split().Where(x => Sorter(x)).ToList();
            Console.WriteLine(String.Join("\n", names));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace ReverseAndExclude
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> myCollection = Console.ReadLine().Split().Select(int.Parse).ToList();
            int specialNumber = int.Parse(Console.ReadLine());

            Predicate<int> Sorter = CreateSorter(specialNumber);
            SortAndPrint(myCollection, Sorter);
        }

        public static Predicate<int> CreateSorter(int specialNumber)
        {
            return n => n % specialNumber != 0;
        }

        public static void SortAndPrint(List<int> numbers, Predicate<int> Sorter)
        {
            numbers = numbers.Where(x => Sorter(x)).Reverse().ToList();
            Console.WriteLine(String.Join(" ", numbers));
        }
    }
}

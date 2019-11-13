using System;
using System.Linq;

namespace FindEvensOrOdds
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split();
            string condition = Console.ReadLine();

            int start = int.Parse(input[0]);
            int end = int.Parse(input[1]);

            Predicate<int> ConditionMet = ConditionTester(condition);

            var numbers = Enumerable.Range(start, end - start + 1).Where(n => ConditionMet(n)).ToList();
            Console.WriteLine(String.Join(" ", numbers));
        }

        public static Predicate<int> ConditionTester(string condition)
        {
            if (condition.Equals("odd"))
            {
                return n => n % 2 != 0;
            }
            else if (condition.Equals("even"))
            {
                return n => n % 2 == 0;
            }
            else
            {
                return null;
            }
        }
    }
}

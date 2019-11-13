using System;
using System.Collections.Generic;

namespace FilterByAge
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputCount = int.Parse(Console.ReadLine());
            Dictionary<string, int> people = new Dictionary<string, int>();

            while (inputCount-- > 0)
            {
                string[] tokens = Console.ReadLine().Split(", ");
                if (!people.ContainsKey(tokens[0]))
                {
                    people.Add(tokens[0], int.Parse(tokens[1]));
                }
            }

            string condition = Console.ReadLine();
            int conditionAge = int.Parse(Console.ReadLine());
            string printCondition = Console.ReadLine();

            Func<int, bool> ConditionTester = CreateTest(condition, conditionAge);
            Action<KeyValuePair<string, int>> Printer = CreatePrint(printCondition);

            PrintFiltered(people, ConditionTester, Printer);
        }

        public static void PrintFiltered(Dictionary<string, int> ppl, Func<int, bool> ConditionTester, Action<KeyValuePair<string, int>> Printer)
        {
            foreach (var person in ppl)
            {
                if (ConditionTester(person.Value))
                {
                    Printer(person);
                }
            }
        }

        public static Action<KeyValuePair<string, int>> CreatePrint(string condition)
        {
            switch (condition)
            {
                case "name age":
                    return p => Console.WriteLine("{0} - {1}", p.Key, p.Value);
                case "name":
                    return p => Console.WriteLine("{0}", p.Key);
                case "age":
                    return p => Console.WriteLine("{0}", p.Value);
                default:
                    return null;
            }
        }

        public static Func<int, bool> CreateTest(string condition, int age)
        {
            switch (condition)
            {
                case "older":
                    return x => x >= age;
                case "younger":
                    return x => x < age;
                default:
                    return null;
            }
        }
    }
}

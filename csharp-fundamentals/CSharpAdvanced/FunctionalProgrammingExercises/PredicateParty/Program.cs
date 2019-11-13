using System;
using System.Collections.Generic;
using System.Linq;

namespace PredicateParty
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> people = Console.ReadLine().Split().ToList();

            string input;
            while ((input = Console.ReadLine()) != "Party!")
            {
                string[] tokens = input.Split();
                string command = tokens[0];
                string condition = tokens[1];
                string conditionVar = tokens[2];
                Predicate<string> ConditionChecker = ConditionCreator(condition, conditionVar);

                Action<Predicate<string>> Remove = x => people.RemoveAll(n => x(n));
                Action<Predicate<string>> Double = x =>
                {
                    for (int i = 0; i < people.Count; i++)
                    {
                        if (x(people[i]))
                        {
                            people.Insert(i, people[i]);
                            i++;
                        }
                    }
                };

                switch (command)
                {
                    case "Remove":
                        Remove(ConditionChecker);
                        break;
                    case "Double":
                        Double(ConditionChecker);
                        break;
                    default:
                        break;
                }
            }

            if (people.Count == 0)
            {
                Console.WriteLine("Nobody is going to the party!");
            }
            else
            {
                Console.WriteLine("{0} are going to the party!", String.Join(", ", people));
            }
        }

        public static Predicate<string> ConditionCreator(string condition, string conditionVar)
        {
            switch (condition)
            {
                case "StartsWith":
                    return x => x.StartsWith(conditionVar);
                case "EndsWith":
                    return x => x.EndsWith(conditionVar);
                case "Length":
                    return x => x.Length == int.Parse(conditionVar);
                default:
                    return null;
            }
        }
    }
}

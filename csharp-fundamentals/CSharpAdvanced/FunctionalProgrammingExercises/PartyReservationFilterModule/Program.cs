using System;
using System.Collections.Generic;
using System.Linq;

namespace PartyReservationFilterModule
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> people = Console.ReadLine().Split().ToList();

            List<KeyValuePair<string, string>> filters = new List<KeyValuePair<string, string>>();

            string input;
            while ((input = Console.ReadLine()) != "Print")
            {
                string[] tokens = input.Split(";");
                string command = tokens[0];
                string condition = tokens[1];
                string conditionVar = tokens[2];
                KeyValuePair<string, string> filter = new KeyValuePair<string, string>(condition, conditionVar);

                if (command == "Add filter")
                {
                    filters.Add(filter);
                }
                else if (command == "Remove filter")
                {
                    filters.RemoveAll(x => x.Equals(filter));
                }
            }

            foreach (var filter in filters)
            {
                Predicate<string> CurrentFilter = ConditionCreator(filter.Key, filter.Value);
                people.RemoveAll(x => CurrentFilter(x));
            }

            Console.WriteLine(String.Join(" ", people));
        }

        public static Predicate<string> ConditionCreator(string condition, string conditionVar)
        {
            switch (condition)
            {
                case "Starts with":
                    return x => x.StartsWith(conditionVar);
                case "Ends with":
                    return x => x.EndsWith(conditionVar);
                case "Length":
                    return x => x.Length == int.Parse(conditionVar);
                case "Contains":
                    return x => x.Contains(conditionVar);
                default:
                    return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace InfernoIII
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> gems = Console.ReadLine().Split().Select(int.Parse).ToList();

            string input;

            List<KeyValuePair<string, int>> filters = new List<KeyValuePair<string, int>>();
            while ((input = Console.ReadLine()) != "Forge")
            {
                string[] tokens = input.Split(";");
                string command = tokens[0];
                string condition = tokens[1];
                int conditionVar = int.Parse(tokens[2]);
                KeyValuePair<string, int> filter = new KeyValuePair<string, int>(condition, conditionVar);

                if (command == "Exclude")
                {
                    filters.Add(filter);
                }
                else if (command == "Reverse")
                {
                    if (filters.Count > 0)
                    {
                        filters.RemoveAll(x => x.Equals(filter));
                    }
                }
            }
            if (filters.Count > 0)
            {
                HashSet<int> result = new HashSet<int>();
                foreach (var currentFilter in filters)
                {
                    Predicate<int> Checker = CreateFilter(currentFilter, gems);
                    for (int i = 0; i < gems.Count; i++)
                    {
                        if (Checker(i))
                        {
                            result.Add(i);
                        }
                    }

                }

                for (int i = 0; i < gems.Count; i++)
                {
                    if (!result.Contains(i))
                    {
                        Console.Write(gems[i] + " ");
                    }
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(String.Join(" ", gems));
            }

        }

        public static Predicate<int> CreateFilter(KeyValuePair<string, int> filter, List<int> gems)
        {
            switch (filter.Key)
            {
                case "Sum Left":
                    return x =>
                    {
                        int leftGem = x == 0 ? 0 : gems[x - 1];
                        return gems[x] + leftGem == filter.Value;
                    };
                case "Sum Right":
                    return x =>
                    {
                        int rightGem = x == gems.Count - 1 ? 0 : gems[x + 1];
                        return gems[x] + rightGem == filter.Value;
                    };
                case "Sum Left Right":
                    return x =>
                    {
                        int leftGem = x == 0 ? 0 : gems[x - 1];
                        int rightGem = x == gems.Count - 1 ? 0 : gems[x + 1];
                        return gems[x] + leftGem + rightGem == filter.Value;
                    };
                default:
                    return null;
            }
        }
    }
}

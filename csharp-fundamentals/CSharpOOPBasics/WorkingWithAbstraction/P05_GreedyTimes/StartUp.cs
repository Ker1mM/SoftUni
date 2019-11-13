using System;
using System.Collections.Generic;
using System.Linq;

namespace P05_GreedyTimes
{

    public class StartUp
    {
        static void Main(string[] args)
        {
            long bagCapacity = long.Parse(Console.ReadLine());
            string[] safe =
                Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var bag = new Dictionary<string, Dictionary<string, long>>();
            long gold = 0;
            long gems = 0;
            long cash = 0;

            for (int i = 0; i < safe.Length; i += 2)
            {
                string nextItem = safe[i];
                long count = long.Parse(safe[i + 1]);

                string type = Methods.GetType(nextItem);

                if (type == "")
                {
                    continue;
                }
                else if (bagCapacity < bag.Values.Select(x => x.Values.Sum()).Sum() + count)
                {
                    break;
                }

                if (!Methods.CheckCase(type, bag, count))
                {
                    continue;
                }

                if (!bag.ContainsKey(type))
                {
                    bag[type] = new Dictionary<string, long>();
                }

                if (!bag[type].ContainsKey(nextItem))
                {
                    bag[type][nextItem] = 0;
                }

                bag[type][nextItem] += count;

                if (type == "Gold")
                {
                    gold += count;
                }
                else if (type == "Gem")
                {
                    gems += count;
                }
                else if (type == "Cash")
                {
                    cash += count;
                }
            }

            foreach (var x in bag)
            {
                Console.WriteLine($"<{x.Key}> ${x.Value.Values.Sum()}");
                foreach (var item2 in x.Value.OrderByDescending(y => y.Key).ThenBy(y => y.Value))
                {
                    Console.WriteLine($"##{item2.Key} - {item2.Value}");
                }
            }
        }
    }
}
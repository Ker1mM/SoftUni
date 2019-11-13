using System;
using System.Collections.Generic;
using System.Linq;

namespace Knapsack
{
    class Program
    {
        static void Main(string[] args)
        {
            int capacity = int.Parse(Console.ReadLine());

            var items = Item.GetItems().OrderByDescending(x => x.Proportion).ToList();

            var addedItems = new List<Item>();

            foreach (var item in items)
            {
                if (capacity <= 0)
                {
                    break;
                }

                if (capacity - item.Weight >= 0)
                {
                    addedItems.Add(item);
                    capacity -= item.Weight;
                }

            }

            Console.WriteLine($"Total Weight: {addedItems.Sum(x => x.Weight)}");
            Console.WriteLine($"Total Value: {addedItems.Sum(x => x.Value)}");
            Console.WriteLine(string.Join(Environment.NewLine, addedItems.OrderBy(x => x.Name)));

        }
    }

    class Item
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Value { get; set; }

        public double Proportion => (double)this.Value / this.Weight;

        public Item(string name, int weight, int value)
        {
            this.Name = name;
            this.Weight = weight;
            this.Value = value;
        }

        public static List<Item> GetItems()
        {
            string input = Console.ReadLine();
            var result = new List<Item>();

            while (input != "end")
            {
                var inputArgs = input.Split().ToArray();
                string name = inputArgs[0];
                int weight = int.Parse(inputArgs[1]);
                int value = int.Parse(inputArgs[2]);

                result.Add(new Item(name, weight, value));
                input = Console.ReadLine();
            }

            return result;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

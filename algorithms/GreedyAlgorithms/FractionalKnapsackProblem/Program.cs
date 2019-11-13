using System;
using System.Linq;

namespace FractionalKnapsackProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            double capacity = double.Parse(Console.ReadLine().Split()[1]);
            int itemsCount = int.Parse(Console.ReadLine().Split()[1]);

            var items = new Item[itemsCount];

            for (int count = 0; count < itemsCount; count++)
            {
                var itemProperties = Console.ReadLine().Split();
                var itemPrice = double.Parse(itemProperties[0]);
                var itemWeight = double.Parse(itemProperties[2]);
                items[count] = new Item(itemPrice, itemWeight);
            }

            items = items.OrderByDescending(i => i.Price / i.Weight).ToArray();

            double totalPrice = 0;
            int index = 0;
            while (capacity > 0 && index < items.Length)
            {
                var nextItem = items[index++];
                double percentage = capacity / nextItem.Weight;

                if (percentage >= 1)
                {
                    capacity -= nextItem.Weight;
                    totalPrice += nextItem.Price;
                    percentage = 1;
                }
                else
                {
                    totalPrice += percentage * nextItem.Price;
                    capacity = 0;
                }

                string percentageString = $"{percentage * 100:f2}";
                if (percentage == 1)
                {
                    percentageString = "100";
                }

                Console.WriteLine($"Take {percentageString}% of item with price {nextItem.Price:f2} and weight {nextItem.Weight:f2}");
            }

            Console.WriteLine($"Total price: {totalPrice:f2}");

        }

    }

    internal class Item
    {
        public double Price { get; set; }
        public double Weight { get; set; }

        public Item(double price, double weight)
        {
            this.Price = price;
            this.Weight = weight;
        }
    }
}

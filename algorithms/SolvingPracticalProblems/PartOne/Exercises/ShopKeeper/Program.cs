using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopKeeper
{
    class Program
    {
        private static List<int> inStock;
        private static Dictionary<int, List<int>> ordersList;

        static void Main(string[] args)
        {

            //var stringInput = System.IO.File.ReadLines(@"..\..\..\..\..\..\Resources\in.txt").ToArray();
            //;

            ////inStock = new HashSet<int>(stringInput[0].Split().Select(int.Parse).ToList());
            //inStock = stringInput[0].Split().Select(int.Parse).ToList();

            //var orders = stringInput[1].Split().Select(int.Parse).ToList();

            inStock = Console.ReadLine().Split().Select(int.Parse).ToList();
            var orders = Console.ReadLine().Split().Select(int.Parse).ToList();

            ordersList = new Dictionary<int, List<int>>();
            for (int i = 0; i < orders.Count; i++)
            {
                if (!ordersList.ContainsKey(orders[i]))
                {
                    ordersList.Add(orders[i], new List<int>());
                }

                ordersList[orders[i]].Add(i);
            }

            foreach (var item in orders)
            {
                ordersList[item] = ordersList[item].OrderBy(x => x).ToList();
            }

            bool result = true;

            int count = 0;

            if (!inStock.Contains(orders[0]))
            {
                result = false;
            }

            for (int i = 0; i < orders.Count; i++)
            {

                if (!result)
                { break; }

                int item = orders[i];
                if (!inStock.Contains(item))
                {
                    int last = FindLast();
                    inStock.Remove(last);
                    inStock.Add(item);
                    count++;
                }

                ordersList[item].Remove(i);
            }

            Console.WriteLine(result ? count.ToString() : "impossible");
        }

        private static int FindLast()
        {
            int last = 0;
            int bestItem = -1;
            foreach (var item in inStock)
            {
                if (!ordersList.ContainsKey(item) || ordersList[item].Count == 0)
                {
                    bestItem = item;
                    break;
                }
                else
                {
                    int current = ordersList[item][0];
                    if (current > last)
                    {
                        last = current;
                        bestItem = item;
                    }
                }
            }

            return bestItem;
        }
    }
}

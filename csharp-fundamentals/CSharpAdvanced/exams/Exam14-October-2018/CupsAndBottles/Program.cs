using System;
using System.Collections.Generic;
using System.Linq;

namespace CupsAndBottles
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> cups = new Queue<int>(Console.ReadLine().Split().Select(int.Parse));
            Stack<int> bottles = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));

            int wastedWater = 0;
            while (cups.Count > 0 && bottles.Count > 0)
            {
                int currentCup = cups.Peek();
                int currentBottle = 0;
                while (currentCup > 0)
                {
                    currentBottle = bottles.Pop();
                    if (currentBottle >= currentCup)
                    {
                        currentBottle -= currentCup;
                        cups.Dequeue();
                        break;
                    }

                    currentCup -= currentBottle;
                }
                wastedWater += currentBottle;
            }
            if (cups.Count > 0)
            {
                Console.Write($"Cups: ");
                Console.WriteLine(String.Join(" ", cups));
            }
            else if (bottles.Count > 0)
            {
                Console.Write($"Bottles: ");
                Console.WriteLine(String.Join(" ", bottles));
            }
            Console.WriteLine($"Wasted litters of water: {wastedWater}");
        }
    }
}

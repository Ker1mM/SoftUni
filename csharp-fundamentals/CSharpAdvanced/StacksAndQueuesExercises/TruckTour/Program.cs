using System;
using System.Collections.Generic;
using System.Linq;

namespace TruckTour
{
    class Program
    {
        static void Main(string[] args)
        {
            int pumpsCount = int.Parse(Console.ReadLine());

            Queue<int[]> circle = new Queue<int[]>();

            for (int i = 0; i < pumpsCount; i++)
            {
                string input = Console.ReadLine();
                int[] tokens = input.Split(" ").Select(int.Parse).ToArray();

                circle.Enqueue(tokens);
            }

            for (int i = 0; i < circle.Count; i++)
            {
                bool result = true;
                long totalFuel = 0;
                foreach (var currentCircle in circle)
                {
                    totalFuel += currentCircle[0] - currentCircle[1];
                    if (totalFuel < 0)
                    {
                        result = false;
                        break;
                    }
                }

                if (result == true)
                {
                    Console.WriteLine(i);
                    break;
                }
                circle.Enqueue(circle.Dequeue());
            }
        }
    }
}

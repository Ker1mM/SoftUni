using System;
using System.Collections.Generic;

namespace TrafficLight
{
    class Program
    {
        static void Main(string[] args)
        {
            int carsCanPass = int.Parse(Console.ReadLine());

            string input = Console.ReadLine();

            Queue<string> trafficQueue = new Queue<string>();

            int totalCarsPassed = 0;

            while (input != "end")
            {
                if (input == "green")
                {
                    int counter = 0;
                    while (counter < carsCanPass && trafficQueue.TryDequeue(out string passedCar))
                    {
                        Console.WriteLine(passedCar + " passed!");
                        totalCarsPassed++;
                        counter++;
                    }
                }
                else
                {
                    trafficQueue.Enqueue(input);
                }

                input = Console.ReadLine();
            }

            Console.WriteLine(totalCarsPassed + " cars passed the crossroads.");
        }
    }
}

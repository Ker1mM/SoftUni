using System;
using System.Collections.Generic;
using System.Linq;

namespace PoisonousPlants
{
    class Program
    {
        static void Main(string[] args)
        {
            int initialPlantsCount = int.Parse(Console.ReadLine());

            Queue<int> plants = new Queue<int>(Console.ReadLine().Split(" ").Select(int.Parse));

            int daysCount = 0;

            bool noPlantHasDied = false;
            while (!noPlantHasDied)
            {
                noPlantHasDied = true;

                int plantsCount = plants.Count();
                int currentPlant = plants.Dequeue();
                plants.Enqueue(currentPlant);

                for (int i = 0; i < plantsCount - 1; i++)
                {
                    if (currentPlant < plants.Peek())
                    {
                        currentPlant = plants.Dequeue();
                        noPlantHasDied = false;
                    }
                    else
                    {
                        currentPlant = plants.Dequeue();
                        plants.Enqueue(currentPlant);
                    }

                }

                if (!noPlantHasDied)
                {
                    daysCount++;
                }
            }
            Console.WriteLine(daysCount);
        }
    }
}

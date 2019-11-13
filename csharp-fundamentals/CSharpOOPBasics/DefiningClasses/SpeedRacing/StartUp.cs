using System;
using System.Collections.Generic;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            List<Car> cars = new List<Car>();
            string input;
            while (count-- > 0)
            {
                input = Console.ReadLine();
                string[] tokens = input.Split();
                string model = tokens[0];
                double fuel = double.Parse(tokens[1]);
                double fuelPerKm = double.Parse(tokens[2]);

                cars.Add(new Car(model, fuel, fuelPerKm));
            }

            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split();
                string model = tokens[1];
                double distance = double.Parse(tokens[2]);
                int index = cars.FindIndex(x => x.Model == model);
                if (index >= 0)
                {
                    cars[index].Drive(distance);
                }
            }

            foreach (var car in cars)
            {
                Console.WriteLine(car.ToString());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace CarSalesman
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            List<Engine> engines = new List<Engine>();
            List<Car> cars = new List<Car>();
            while (count-- > 0)
            {
                string[] tokens =
                    Console.ReadLine()
                    .Trim()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                engines.Add(Engine.Parse(tokens));
            }

            count = int.Parse(Console.ReadLine());
            while (count-- > 0)
            {
                string[] tokens =
                    Console.ReadLine()
                    .Trim()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string model = tokens[0];
                Engine engine = engines.FirstOrDefault(x => x.Model == tokens[1]);
                Car tempCar = new Car(model, engine);
                tempCar.ParseOptionals(tokens);
                cars.Add(tempCar);
            }

            foreach (var car in cars)
            {
                Console.WriteLine(car.ToString());
            }

        }
    }
}

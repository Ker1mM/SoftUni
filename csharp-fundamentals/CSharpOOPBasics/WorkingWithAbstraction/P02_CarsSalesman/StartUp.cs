using System;
using System.Collections.Generic;

namespace P02_CarsSalesman
{
    class CarSalesman
    {
        static void Main(string[] args)
        {
            List<Car> cars = new List<Car>();
            List<Engine> engines = new List<Engine>();

            int engineCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < engineCount; i++)
            {
                string[] parameters = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Engine.AddEngine(engines, parameters);
            }

            int carCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < carCount; i++)
            {
                string[] parameters = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Car.AddCar(cars, engines, parameters);
            }

            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
        }
    }

}

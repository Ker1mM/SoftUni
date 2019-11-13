using System;
using System.Collections.Generic;
using System.Linq;

namespace RawData
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            List<Car> cars = new List<Car>();
            while (count-- > 0)
            {
                string[] tokens = Console.ReadLine().Split();
                string model = tokens[0];
                int engineSpeed = int.Parse(tokens[1]);
                int enginePower = int.Parse(tokens[2]);
                Engine engine = new Engine(engineSpeed, enginePower);
                int cargoWeight = int.Parse(tokens[3]);
                string cargoType = tokens[4];
                Cargo cargo = new Cargo(cargoWeight, cargoType);
                Tire tire1 = new Tire(double.Parse(tokens[5]), int.Parse(tokens[6]));
                Tire tire2 = new Tire(double.Parse(tokens[7]), int.Parse(tokens[8]));
                Tire tire3 = new Tire(double.Parse(tokens[9]), int.Parse(tokens[10]));
                Tire tire4 = new Tire(double.Parse(tokens[11]), int.Parse(tokens[12]));
                cars.Add(new Car(model, engine, cargo, tire1, tire2, tire3, tire4));
            }

            string command = Console.ReadLine();

            foreach (var car in cars.Where(x => x.Cargo.Type == command && x.Check()))
            {
                Console.WriteLine(car.Model);
            }
        }
    }
}

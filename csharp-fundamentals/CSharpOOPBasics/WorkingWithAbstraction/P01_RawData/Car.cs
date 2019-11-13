using System;
using System.Collections.Generic;
using System.Linq;

namespace P01_RawData
{
    public class Car
    {
        public string model;
        public int engineSpeed;
        public int enginePower;
        public int cargoWeight;
        public string cargoType;
        public KeyValuePair<double, int>[] tires;

        public Car(
            string model, int engineSpeed,
            int enginePower,
            int cargoWeight,
            string cargoType,
            double tire1Pressure,
            int tire1Age,
            double tire2Pressure,
            int tire2Age,
            double tire3Pressure,
            int tire3age,
            double tire4Pressure,
            int tire4age)
        {
            this.model = model;
            this.engineSpeed = engineSpeed;
            this.enginePower = enginePower;
            this.cargoWeight = cargoWeight;
            this.cargoType = cargoType;
            this.tires =
                new KeyValuePair<double, int>[] {
                    KeyValuePair.Create(tire1Pressure, tire1Age),
                    KeyValuePair.Create(tire2Pressure, tire2Age),
                    KeyValuePair.Create(tire3Pressure, tire3age),
                    KeyValuePair.Create(tire4Pressure, tire4age) };
        }

        public static void Print(string command, List<Car> cars)
        {
            if (command == "fragile")
            {
                List<string> fragile = cars
                    .Where(x => x.cargoType == "fragile" && x.tires.Any(y => y.Key < 1))
                    .Select(x => x.model)
                    .ToList();

                Console.WriteLine(string.Join(Environment.NewLine, fragile));
            }
            else
            {
                List<string> flamable = cars
                    .Where(x => x.cargoType == "flamable" && x.enginePower > 250)
                    .Select(x => x.model)
                    .ToList();

                Console.WriteLine(string.Join(Environment.NewLine, flamable));
            }
        }
    }
}

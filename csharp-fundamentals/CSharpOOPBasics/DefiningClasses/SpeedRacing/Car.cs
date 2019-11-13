using System;
using System.Collections.Generic;
using System.Text;

namespace DefiningClasses
{
    public class Car
    {
        private string model;
        private double fuel;
        private double fuelPerKm;
        private double distance;

        public string Model
        {
            get { return this.model; }
            set { this.model = value; }
        }

        public double Fuel
        {
            get { return this.fuel; }
            set { this.fuel = value; }
        }

        public double FuelPerKm
        {
            get { return this.fuelPerKm; }
            set { this.fuelPerKm = value; }
        }

        public double Distance
        {
            get { return this.distance; }
            set { this.distance = value; }
        }

        public Car(string model, double fuel, double fuelPerKm)
        {
            this.Model = model;
            this.Fuel = fuel;
            this.FuelPerKm = fuelPerKm;
            this.Distance = 0;
        }

        public void Drive(double distance)
        {
            double fuelNeeded = distance * this.FuelPerKm;
            if (fuelNeeded <= this.Fuel)
            {
                this.Fuel -= fuelNeeded;
                this.Distance += distance;
            }
            else
            {
                Console.WriteLine("Insufficient fuel for the drive");
            }
        }

        public override string ToString()
        {
            return $"{this.Model} {this.Fuel:f2} {this.Distance}";
        }
    }
}

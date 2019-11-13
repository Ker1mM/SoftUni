using System;
using System.Collections.Generic;
using System.Text;

namespace RawData
{
    public class Tire
    {
        private double pressure;
        private int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }


        public double Pressure
        {
            get { return pressure; }
            set { pressure = value; }
        }

        public Tire(double pressure, int age)
        {
            this.Pressure = pressure;
            this.Age = age;
        }
    }
}

using System;

namespace P5_PizzaCalories
{
    public class Topping
    {
        private string type;
        private double weight;

        private double Weight
        {
            get => this.weight;
            set
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{this.Type} weight should be in the range [1..50].");
                }
                this.weight = value;
            }
        }


        public double CaloriesPerGram
        {
            get
            {
                return 2 * this.Weight * GetTypeModifier();
            }
        }

        private string Type
        {
            get => this.type;
            set
            {
                if (value.ToLower() != "meat" && value.ToLower() != "veggies" && value.ToLower() != "cheese" && value.ToLower() != "sauce")
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }
                this.type = value;
            }
        }

        public Topping(string type, double weight)
        {
            this.Type = type;
            this.Weight = weight;
        }

        private double GetTypeModifier()
        {
            switch (this.type.ToLower())
            {
                case "meat":
                    return 1.2;
                case "veggies":
                    return 0.8;
                case "cheese":
                    return 1.1;
                case "sauce":
                default:
                    return 0.9;
            }
        }
    }
}

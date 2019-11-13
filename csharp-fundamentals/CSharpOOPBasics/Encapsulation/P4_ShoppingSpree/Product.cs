using System;

namespace P4_ShoppingSpree
{
    public class Product
    {
        private string name;
        private decimal cost;

        public string Name
        {
            get => this.name;
            set
            {
                if (value == "" || value == null || value == " ")
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                this.name = value;
            }
        }

        public decimal Cost
        {
            get => this.cost;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                this.cost = value;
            }
        }

        public Product(string name, decimal cost)
        {
            this.Name = name;
            this.Cost = cost;
        }

        public static Product Parse(string nameAndCost)
        {
            string[] tokens = nameAndCost.Split("=", StringSplitOptions.RemoveEmptyEntries);
            string name = tokens[0];
            decimal cost = decimal.Parse(tokens[1]);

            return new Product(name, cost);
        }

        public override string ToString()
        {
            return $"{this.Name}";
        }
    }
}

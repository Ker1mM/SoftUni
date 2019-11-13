using System;
using System.Collections.Generic;

namespace P4_ShoppingSpree
{
    public class Person
    {
        private string name;
        private decimal money;
        private List<Product> productBag;


        public IReadOnlyCollection<Product> ProductBag
        {
            get
            {
                return this.productBag.AsReadOnly();
            }
        }

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

        public decimal Money
        {
            get => this.money;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                this.money = value;
            }
        }
        public Person()
        {
            this.productBag = new List<Product>();
        }

        public Person(string name, decimal money) : this()
        {
            this.Name = name;
            this.Money = money;
        }

        public void AddProduct(Product product)
        {
            if (this.Money < product.Cost)
            {
                Console.WriteLine($"{this.Name} can't afford {product.Name}");
            }
            else
            {
                Console.WriteLine($"{this.Name} bought {product.Name}");
                this.Money -= product.Cost;
                this.productBag.Add(product);
            }
        }

        public static Person Parse(string nameAndMoney)
        {
            string[] tokens = nameAndMoney.Split("=", StringSplitOptions.RemoveEmptyEntries);
            string name = tokens[0];
            decimal money = decimal.Parse(tokens[1]);

            return new Person(name, money);
        }

        public override string ToString()
        {
            string products = string.Join(", ", this.productBag);
            if (this.productBag.Count == 0)
            {
                products = "Nothing bought";
            }

            return $"{this.Name} - {products}";
        }
    }
}

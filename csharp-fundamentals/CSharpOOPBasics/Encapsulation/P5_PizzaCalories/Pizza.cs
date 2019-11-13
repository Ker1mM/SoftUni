using System;
using System.Collections.Generic;

namespace P5_PizzaCalories
{
    public class Pizza
    {
        private string name;
        private List<Topping> toppings;
        private Dough dough;

        private Dough Dough
        {
            get => this.dough;
            set
            {
                this.dough = value;
            }
        }

        public IReadOnlyCollection<Topping> Toppings
        {
            get
            {
                return this.toppings.AsReadOnly();
            }
        }

        public double GetCalories
        {
            get
            {
                double totalCalories = 0;
                foreach (var topping in this.Toppings)
                {
                    totalCalories += topping.CaloriesPerGram;
                }
                totalCalories += this.Dough.CaloriesPerGram;
                return totalCalories;
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value == "" || value == null || value.Length > 15)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }
                this.name = value;
            }
        }

        public Pizza(string name)
        {
            this.Name = name;
            this.toppings = new List<Topping>();
        }

        public void AddTopping(Topping topping)
        {
            if (this.Toppings.Count == 10)
            {
                throw new ArgumentException("Number of toppings should be in range [0..10].");
            }
            this.toppings.Add(topping);
        }

        public void AddDough(Dough dough)
        {
            this.Dough = dough;
        }

    }
}

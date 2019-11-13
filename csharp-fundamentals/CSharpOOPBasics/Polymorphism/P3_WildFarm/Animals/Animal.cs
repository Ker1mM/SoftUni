using System;
using System.Collections.Generic;

namespace Animals
{

    public abstract class Animal
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public int FoodEaten { get; private set; }


        public Animal(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }

        public virtual double GetTotalWeight()
        {
            return Weight + (FoodEaten * 1);
        }

        protected virtual List<string> GetFavoredFood()
        {
            return new List<string> { "Fruit", "Meat", "Seeds", "Vegetable" };
        }

        public void Eat(Food food)
        {
            string foodType = food.GetType().FullName;
            List<string> favoredFood = GetFavoredFood();

            if (favoredFood.Any(x => x == foodType))
            {
                FoodEaten += food.Quantity;
            }
            else
            {
                throw new ArgumentException($"{this.GetType().FullName} does not eat {foodType}!");
            }
        }

        public abstract string MakeSound();

        public override string ToString()
        {
            return $"{this.GetType().FullName} ";
        }
    }
}


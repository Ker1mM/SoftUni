using System;

namespace P5_PizzaCalories
{
    public class Dough
    {
        private string flour;
        private string technique;
        private double weight;

        public double CaloriesPerGram
        {
            get
            {
                return GetFlourModifier() * GetTechniqueModifier() * this.Weight * 2;
            }
        }

        private double Weight
        {
            get => this.weight;
            set
            {
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }
                this.weight = value;
            }
        }


        private string Technique
        {
            get => this.technique;
            set
            {
                if (value.ToLower() != "crispy" && value.ToLower() != "chewy" && value.ToLower() != "homemade")
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                this.technique = value;
            }
        }

        private string Flour
        {
            get => this.flour;
            set
            {
                if (value.ToLower() != "white" && value.ToLower() != "wholegrain")
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                this.flour = value;
            }
        }

        private double GetFlourModifier()
        {
            switch (this.Flour.ToLower())
            {
                case "white":
                    return 1.5;
                case "wholegrain":
                default:
                    return 1;
            }
        }

        private double GetTechniqueModifier()
        {
            switch (this.Technique.ToLower())
            {
                case "crispy":
                    return 0.9;
                case "chewy":
                    return 1.1;
                case "homemade":
                default:
                    return 1;
            }
        }

        public Dough(string flourType, string bakingTechnique, double grams)
        {
            this.Flour = flourType;
            this.Technique = bakingTechnique;
            this.Weight = grams;
        }
    }
}

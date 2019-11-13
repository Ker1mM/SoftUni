﻿namespace AnimalCentre.Models.Entities.Animals
{
    public class Cat : Animal
    {
        public Cat(string name, int energy, int happiness, int procedureTime) : base(name, energy, happiness, procedureTime)
        {

        }

        public override string ToString()
        {
            string type = this.GetType().Name;
            return $"    Animal type: {type} - {this.Name} - Happiness: {this.Happiness} - Energy: {this.Energy}";
        }
    }
}

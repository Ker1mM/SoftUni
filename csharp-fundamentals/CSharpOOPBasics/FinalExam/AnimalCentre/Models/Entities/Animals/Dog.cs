namespace AnimalCentre.Models.Entities.Animals
{
    public class Dog : Animal
    {
        public Dog(string name, int energy, int happiness, int procedureTime) : base(name, energy, happiness, procedureTime)
        {

        }

        public override string ToString()
        {
            string type = this.GetType().Name;
            return $"    Animal type: {type} - {this.Name} - Happiness: {this.Happiness} - Energy: {this.Energy}";
        }
    }
}

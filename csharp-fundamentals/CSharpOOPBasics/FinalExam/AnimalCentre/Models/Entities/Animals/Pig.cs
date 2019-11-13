namespace AnimalCentre.Models.Entities.Animals
{
    public class Pig : Animal
    {
        public Pig(string name, int energy, int happiness, int procedureTime) : base(name, energy, happiness, procedureTime)
        {

        }

        public override string ToString()
        {
            string type = this.GetType().Name;
            return $"    Animal type: {type} - {this.Name} - Happiness: {this.Happiness} - Energy: {this.Energy}";
        }
    }
}

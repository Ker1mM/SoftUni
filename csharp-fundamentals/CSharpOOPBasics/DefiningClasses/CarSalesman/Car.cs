namespace CarSalesman
{
    public class Car
    {
        public string Model { get; set; }
        public Engine Engine { get; set; }
        public string Weight { get; set; }
        public string Color { get; set; }

        public Car(string model, Engine engine)
        {
            this.Model = model;
            this.Engine = engine;
            this.Weight = "n/a";
            this.Color = "n/a";
        }

        public void ParseOptionals(string[] tokens)
        {
            if (tokens.Length == 4)
            {
                this.Weight = tokens[2];
                this.Color = tokens[3];
            }
            else if (tokens.Length == 3)
            {
                if (int.TryParse(tokens[2], out int temp))
                {
                    this.Weight = tokens[2];
                }
                else
                {
                    this.Color = tokens[2];
                }
            }
        }

        public override string ToString()
        {
            return $"{this.Model}:\n" +
                $"  {this.Engine.Model}:\n" +
                $"{this.Engine.ToString()}\n" +
                $"  Weight: {this.Weight}\n" +
                $"  Color: {this.Color}";
        }
    }
}

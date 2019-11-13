namespace Google
{
    public class Car
    {
        public string Model { get; set; }
        public int Speed { get; set; }

        public Car()
        {
            this.Model = "";
            this.Speed = 0;
        }

        public Car(string model, string speed)
        {
            this.Model = model;
            this.Speed = int.Parse(speed);
        }

        public override string ToString()
        {
            string result = "Car:";
            if (this.Model != "")
            {
                result += $"\n{this.Model} {this.Speed}";
            }
            return result;
        }
    }
}

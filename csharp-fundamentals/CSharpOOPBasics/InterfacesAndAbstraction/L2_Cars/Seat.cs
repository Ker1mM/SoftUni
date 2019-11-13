using System.Text;

namespace Cars
{
    public class Seat : ICar
    {
        public string Model { get; private set; }
        public string Color { get; private set; }

        public string Start()
        {
            return "Engine start";
        }

        public string Stop()
        {
            return "Breaaak!";
        }

        public Seat(string model, string color)
        {
            Model = model;
            Color = color;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().Name + " " + Model + " " + Color + "\n");
            sb.Append(Start() + "\n");
            sb.Append(Stop());
            return sb.ToString();
        }
    }
}

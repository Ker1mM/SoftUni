using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P02_CarsSalesman
{
    public class Car
    {
        private const string offset = "  ";

        public string model;
        public Engine engine;
        public int weight;
        public string color;

        public Car(string model, Engine engine)
        {
            this.model = model;
            this.engine = engine;
            this.weight = -1;
            this.color = "n/a";
        }

        public Car(string model, Engine engine, int weight) : this(model, engine)
        {
            this.weight = weight;
            this.color = "n/a";
        }

        public Car(string model, Engine engine, string color) : this(model, engine)
        {
            this.weight = -1;
            this.color = color;
        }

        public Car(string model, Engine engine, int weight, string color) : this(model, engine)
        {
            this.weight = weight;
            this.color = color;
        }

        public static void AddCar(List<Car> cars, List<Engine> engines, string[] parameters)
        {
            string model = parameters[0];
            string engineModel = parameters[1];
            Engine engine = engines.FirstOrDefault(x => x.model == engineModel);

            int weight = -1;

            if (parameters.Length == 3 && int.TryParse(parameters[2], out weight))
            {
                cars.Add(new Car(model, engine, weight));
            }
            else if (parameters.Length == 3)
            {
                string color = parameters[2];
                cars.Add(new Car(model, engine, color));
            }
            else if (parameters.Length == 4)
            {
                string color = parameters[3];
                cars.Add(new Car(model, engine, int.Parse(parameters[2]), color));
            }
            else
            {
                cars.Add(new Car(model, engine));
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}:\n", this.model);
            sb.Append(this.engine.ToString());
            sb.AppendFormat("{0}Weight: {1}\n", offset, this.weight == -1 ? "n/a" : this.weight.ToString());
            sb.AppendFormat("{0}Color: {1}", offset, this.color);

            return sb.ToString();
        }
    }
}

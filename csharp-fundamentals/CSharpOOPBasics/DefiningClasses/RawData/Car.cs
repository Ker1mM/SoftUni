
using System.Linq;

namespace RawData
{
    public class Car
    {
        private string model;
        private Engine engine;
        private Cargo cargo;
        private Tire[] tire;

        public Cargo Cargo
        {
            get { return cargo; }
            set { cargo = value; }
        }

        public Engine Engine
        {
            get { return engine; }
            set { engine = value; }
        }


        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public Car(string model, Engine engine, Cargo cargo, Tire tire1, Tire tire2, Tire tire3, Tire tire4)
        {
            this.Model = model;
            this.Engine = engine;
            this.Cargo = cargo;
            this.tire = new Tire[4];
            this.tire[0] = tire1;
            this.tire[1] = tire2;
            this.tire[2] = tire3;
            this.tire[3] = tire4;
        }

        public bool Check()
        {
            if (this.Cargo.Type == "fragile")
            {
                return this.tire.Where(x => x.Pressure < 1).ToArray().Length > 0;
            }
            else if (this.Cargo.Type == "flamable")
            {
                return this.Engine.Power > 250;
            }
            else
            {
                return false;
            }
        }
    }
}

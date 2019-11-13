namespace CarSalesman
{
    public class Engine
    {
        public string Model { get; set; }
        public int Power { get; set; }
        public string Displacement { get; set; }
        public string Efficiency { get; set; }

        public Engine(string model, int power)
        {
            this.Model = model;
            this.Power = power;
            this.Displacement = "n/a";
            this.Efficiency = "n/a";
        }

        public static Engine Parse(string[] engineInfo)
        {
            string model = engineInfo[0];
            int power = int.Parse(engineInfo[1]);
            Engine tempEngine = new Engine(model, power);

            if (engineInfo.Length == 4)
            {
                tempEngine.Displacement = engineInfo[2];
                tempEngine.Efficiency = engineInfo[3];
            }
            else if (engineInfo.Length == 3)
            {
                if (int.TryParse(engineInfo[2], out int temp))
                {
                    tempEngine.Displacement = engineInfo[2];
                }
                else
                {
                    tempEngine.Efficiency = engineInfo[2];
                }
            }

            return tempEngine;
        }

        public override string ToString()
        {
            return $"    Power: {this.Power}\n" +
                $"    Displacement: {this.Displacement}\n" +
                $"    Efficiency: {this.Efficiency}";
        }

    }
}

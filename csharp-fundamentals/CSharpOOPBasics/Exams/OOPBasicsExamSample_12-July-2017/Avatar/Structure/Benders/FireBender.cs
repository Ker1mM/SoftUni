public class FireBender : Bender
{
    public double HeatAggression { get; set; }

    public FireBender(string name, int power, double heatAggression) : base(name, power)
    {
        HeatAggression = heatAggression;
    }

    public override string ToString()
    {
        return $"Fire Bender: {Name}, Power: {Power}, Heat Aggression: {HeatAggression:F2}";
    }

    public override double GetPower()
    {
        return Power * HeatAggression;
    }
}


public class WaterBender : Bender
{
    public double WaterClarity { get; set; }

    public WaterBender(string name, int power, double waterClarity) : base(name, power)
    {
        WaterClarity = waterClarity;
    }

    public override string ToString()
    {
        return $"Water Bender: {Name}, Power: {Power}, Water Clarity: {WaterClarity:F2}";
    }

    public override double GetPower()
    {
        return Power * WaterClarity;
    }

}


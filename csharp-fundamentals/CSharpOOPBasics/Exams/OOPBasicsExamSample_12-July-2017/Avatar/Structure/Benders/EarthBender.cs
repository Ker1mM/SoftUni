public class EarthBender : Bender
{
    public double GroundSaturation { get; set; }

    public EarthBender(string name, int power, double groundSaturation) : base(name, power)
    {
        GroundSaturation = groundSaturation;
    }

    public override string ToString()
    {
        return $"Earth Bender: {Name}, Power: {Power}, Ground Saturation: {GroundSaturation:F2}";
    }

    public override double GetPower()
    {
        return Power * GroundSaturation;
    }
}


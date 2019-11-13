public class AirBender : Bender
{
    public double AerialIntegrity { get; set; }

    public AirBender(string name, int power, double aerialIntegrity) : base(name, power)
    {
        AerialIntegrity = aerialIntegrity;
    }

    public override string ToString()
    {
        return $"Air Bender: {Name}, Power: {Power}, Aerial Integrity: {AerialIntegrity:F2}";
    }

    public override double GetPower()
    {
        return Power * AerialIntegrity;
    }
}


public class AirMonument : Monument
{
    public int AirAffinity { get; set; }

    public AirMonument(string name, int airAffinity) : base(name)
    {
        AirAffinity = airAffinity;
    }

    public override string ToString()
    {
        return $"Air Monument: {Name}, Air Affinity: {AirAffinity}";
    }

    public override int GetPower()
    {
        return AirAffinity;
    }
}


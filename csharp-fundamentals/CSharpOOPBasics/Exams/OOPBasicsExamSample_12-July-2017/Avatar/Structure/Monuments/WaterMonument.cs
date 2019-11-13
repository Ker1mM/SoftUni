public class WaterMonument : Monument
{
    public int WaterAffinity { get; set; }

    public WaterMonument(string name, int waterAffinity) : base(name)
    {
        WaterAffinity = waterAffinity;
    }

    public override string ToString()
    {
        return $"Water Monument: {Name}, Water Affinity: {WaterAffinity}";
    }

    public override int GetPower()
    {
        return WaterAffinity;
    }
}


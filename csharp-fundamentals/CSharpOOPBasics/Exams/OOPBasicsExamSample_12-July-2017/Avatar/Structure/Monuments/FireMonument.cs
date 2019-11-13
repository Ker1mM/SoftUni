public class FireMonument : Monument
{
    public int FireAffinity { get; set; }

    public FireMonument(string name, int fireAffinity) : base(name)
    {
        FireAffinity = fireAffinity;
    }

    public override string ToString()
    {
        return $"Fire Monument: {Name}, Fire Affinity: {FireAffinity}";
    }

    public override int GetPower()
    {
        return FireAffinity;
    }
}

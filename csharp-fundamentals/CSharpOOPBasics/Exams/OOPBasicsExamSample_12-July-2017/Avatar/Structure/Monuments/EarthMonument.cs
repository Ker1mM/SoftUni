public class EarthMonument : Monument
{
    public int EarthAffinity { get; set; }

    public EarthMonument(string name, int earthAffinity) : base(name)
    {
        EarthAffinity = earthAffinity;
    }

    public override string ToString()
    {
        return $"Earth Monument: {Name}, Earth Affinity: {EarthAffinity}";
    }

    public override int GetPower()
    {
        return EarthAffinity;
    }
}

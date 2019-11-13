public abstract class Software
{
    public string Name { get; protected set; }
    public int CapacityConsumption { get; protected set; }
    public int MemoryConsumption { get; protected set; }

    public Software(string name)
    {
        Name = name;
        CapacityConsumption = 0;
        MemoryConsumption = 0;
    }

    public override string ToString()
    {
        return Name;
    }
}

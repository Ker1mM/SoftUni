public class LightSoftware : Software
{
    public LightSoftware(string name, int capConsumption, int memConsumption) : base(name)
    {
        MemoryConsumption = (int)(memConsumption * 0.50);
        CapacityConsumption = (int)(capConsumption * 1.50);
    }
}


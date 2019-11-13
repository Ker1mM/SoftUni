public class ExpressSoftware : Software
{
    public ExpressSoftware(string name, int capConsumption, int memConsumption) : base(name)
    {
        MemoryConsumption = memConsumption * 2;
        CapacityConsumption = capConsumption;
    }
}

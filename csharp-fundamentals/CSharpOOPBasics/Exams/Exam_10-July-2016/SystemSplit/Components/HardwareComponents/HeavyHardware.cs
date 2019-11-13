public class HeavyHardware : Hardware
{
    public HeavyHardware(string name, long maxCap, long maxMem) : base(name)
    {
        MaxCapacity = maxCap * 2;
        MaxMemory = (long)(maxMem * 0.75);
    }
}
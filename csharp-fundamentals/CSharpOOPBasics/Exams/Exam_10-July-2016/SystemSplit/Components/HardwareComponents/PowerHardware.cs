public class PowerHardware : Hardware
{
    public PowerHardware(string name, long maxCap, long maxMem) : base(name)
    {
        MaxCapacity = (long)(maxCap * 0.25);
        MaxMemory = (long)(maxMem * 1.75);
    }

}

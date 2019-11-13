public class SonicHarvester : Harvester
{
    private int sonicFactor;

    public int SonicFactor
    {
        get { return sonicFactor; }
        private set { sonicFactor = value; }
    }

    public SonicHarvester(string id, double oreOutput, double energyReq, int sonicFactor) : base(id, oreOutput, energyReq)
    {
        this.SonicFactor = sonicFactor;
        base.SetEnergyReq(base.EnergyRequirement / this.SonicFactor);
    }
}

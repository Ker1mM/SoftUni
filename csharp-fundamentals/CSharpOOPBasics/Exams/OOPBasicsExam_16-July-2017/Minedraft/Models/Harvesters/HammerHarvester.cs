public class HammerHarvester : Harvester
{
    public HammerHarvester(string id, double oreOutput, double energyReq) : base(id, oreOutput, energyReq)
    {
        base.SetEnergyReq(base.EnergyRequirement * 2);
        base.SetOreOutput(base.OreOutput * 3);
    }
}


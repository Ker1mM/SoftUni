using System;
using System.Text;

public abstract class Harvester : Identity
{
    private double oreOutput;
    private double energyRequirement;

    protected Harvester(string id, double oreOutput, double energyReq) : base(id)
    {
        this.OreOutput = oreOutput;
        this.EnergyRequirement = energyReq;
    }

    public double EnergyRequirement
    {
        get { return energyRequirement; }
        protected set
        {
            if (value < 0 || value > 20000)
            {
                throw new ArgumentException("EnergyRequirement");
            }
            energyRequirement = value;
        }
    }


    public double OreOutput
    {
        get { return oreOutput; }
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentException("OreOutput");
            }
            oreOutput = value;
        }
    }

    protected void SetOreOutput(double output)
    {
        this.oreOutput = output;
    }

    protected void SetEnergyReq(double energyReq)
    {
        this.energyRequirement = energyReq;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        string type = this.GetType().Name.Replace("Harvester", "");
        sb.AppendLine($"{type} Harvester - {this.Id}");
        sb.AppendLine($"Ore Output: {this.oreOutput}");
        sb.AppendLine($"Energy Requirement: {this.energyRequirement}");
        return sb.ToString().TrimEnd();
    }
}

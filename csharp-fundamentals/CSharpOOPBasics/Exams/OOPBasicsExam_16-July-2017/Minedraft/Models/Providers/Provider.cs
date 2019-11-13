using System;
using System.Text;

public abstract class Provider : Identity
{
    private double energyOutput;

    protected Provider(string id, double energyOutput) : base(id)
    {
        this.EnergyOutput = energyOutput;
    }

    public double EnergyOutput
    {
        get { return energyOutput; }
        private set
        {
            if (value < 0 || value >= 10_000)
            {
                throw new ArgumentException("EnergyOutput");
            }
            energyOutput = value;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        string type = this.GetType().Name.Replace("Provider", "");
        sb.AppendLine($"{type} Provider - {base.Id}");
        sb.AppendLine($"Energy Output: {this.energyOutput}");
        return sb.ToString().TrimEnd();
    }
}

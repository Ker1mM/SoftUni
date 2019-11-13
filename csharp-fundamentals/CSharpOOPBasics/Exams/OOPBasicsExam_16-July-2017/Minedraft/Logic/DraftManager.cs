using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DraftManager
{

    private Modes mode;
    private double totalEnergyStored;
    private double totalMinedOre;
    private List<Harvester> harvesters;
    private List<Provider> providers;


    public DraftManager()
    {
        this.mode = Modes.Full;
        this.harvesters = new List<Harvester>();
        this.providers = new List<Provider>();
        this.totalEnergyStored = 0;
        this.totalMinedOre = 0;
    }

    public string RegisterHarvester(List<string> arguments)
    {
        string result = $"Successfully registered {arguments[0]} Harvester - {arguments[1]}";
        try
        {
            this.harvesters.Add(HarvesterFactory.CreateHarvester(arguments));
        }
        catch (ArgumentException ae)
        {
            result = $"Harvester is not registered, because of it's {ae.Message}";
        }

        return result;
    }
    public string RegisterProvider(List<string> arguments)
    {
        string result = $"Successfully registered {arguments[0]} Provider - {arguments[1]}";
        try
        {
            this.providers.Add(ProviderFactory.CreateProvider(arguments));
        }
        catch (ArgumentException ae)
        {
            result = $"Provider is not registered, because of it's {ae.Message}";
        }

        return result;
    }
    public string Day()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("A day has passed.");
        double producedEnergy = this.providers.Sum(x => x.EnergyOutput);
        totalEnergyStored += producedEnergy;
        sb.AppendLine($"Energy Provided: {producedEnergy}");

        double totalEnergyReq = this.harvesters.Sum(x => x.EnergyRequirement);
        double totalOreProduced = 1;

        if (this.mode == Modes.Half)
        {
            totalEnergyReq *= 0.6;
            totalOreProduced = 0.5;
        }

        if (this.mode == Modes.Energy || totalEnergyReq > this.totalEnergyStored)
        {
            totalOreProduced = 0;
        }
        else
        {
            totalOreProduced *= this.harvesters.Sum(x => x.OreOutput);
            this.totalEnergyStored -= totalEnergyReq;
            this.totalMinedOre += totalOreProduced;
        }

        sb.AppendLine($"Plumbus Ore Mined: {totalOreProduced}");
        return sb.ToString().TrimEnd();
    }
    public string Mode(List<string> arguments)
    {
        string newModeType = arguments[0];

        bool validMode = Enum.TryParse(typeof(Modes), newModeType, out object newMode);

        if (!validMode)
        {
            throw new ArgumentException();
        }

        this.mode = (Modes)newMode;

        return $"Successfully changed working mode to {this.mode} Mode";
    }
    public string Check(List<string> arguments)
    {
        string id = arguments[0];
        int index;
        if ((index = this.harvesters.FindIndex(x => x.Id == id)) >= 0)
        {
            return this.harvesters[index].ToString();
        }
        else if ((index = this.providers.FindIndex(x => x.Id == id)) >= 0)
        {
            return this.providers[index].ToString();
        }
        else
        {
            return $"No element found with id - {id}";
        }
    }
    public string ShutDown()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("System Shutdown");
        sb.AppendLine($"Total Energy Stored: {this.totalEnergyStored}");
        sb.AppendLine($"Total Mined Plumbus Ore: {this.totalMinedOre}");

        return sb.ToString().TrimEnd();
    }

}

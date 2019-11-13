using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Hardware
{
    public string Name { get; protected set; }
    public long MaxCapacity { get; protected set; }
    public long TakenCapacity { get; private set; }
    public long MaxMemory { get; protected set; }
    public long TakenMemory { get; private set; }
    private long AvailableCapacity { get { return MaxCapacity - TakenCapacity; } }
    private long AvailableMemory { get { return MaxMemory - TakenMemory; } }
    private List<Software> Softwares;

    public Hardware(string name)
    {
        Name = name;
        MaxMemory = 0;
        TakenMemory = 0;
        TakenCapacity = 0;
        MaxCapacity = 0;
        Softwares = new List<Software>();
    }

    public void AddSoftware(Software software)
    {
        if (AvailableMemory >= software.MemoryConsumption && AvailableCapacity >= software.CapacityConsumption)
        {
            Softwares.Add(software);
            TakenMemory += software.MemoryConsumption;
            TakenCapacity += software.CapacityConsumption;
        }
    }

    public void ReleaseSoftware(string softwareName)
    {
        Softwares.RemoveAll(x => x.Name == softwareName);
    }

    public int GetSoftwareCount()
    {
        return Softwares.Count;
    }

    private string GetTypeName()
    {
        string type = this.GetType().FullName;
        if (type == "HeavyHardware")
        {
            return "Heavy";
        }
        else
        {
            return "Power";
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append($"Hardware Component – {Name}");
        sb.Append(Environment.NewLine);
        sb.Append($"Express Software Components: {Softwares.Where(x => x.GetType().FullName == "ExpressSoftware").Count()}");
        sb.Append(Environment.NewLine);
        sb.Append($"Light Software Components: {Softwares.Where(x => x.GetType().FullName == "LightSoftware").Count()}");
        sb.Append(Environment.NewLine);
        sb.Append($"Memory Usage: {TakenMemory} / {MaxMemory}");
        sb.Append(Environment.NewLine);
        sb.Append($"Capacity Usage: {TakenCapacity} / {MaxCapacity}");
        sb.Append(Environment.NewLine);
        sb.Append($"Type: {GetTypeName()}");
        sb.Append(Environment.NewLine);
        if (Softwares.Count == 0)
        {
            sb.Append("None");
        }
        else
        {
            sb.Append($"Software Components: {string.Join(", ", Softwares)}");
        }

        return sb.ToString();
    }
}

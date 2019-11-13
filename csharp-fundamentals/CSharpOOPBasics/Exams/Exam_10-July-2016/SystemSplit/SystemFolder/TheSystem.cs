
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TheSystem
{
    private Dictionary<string, Hardware> Components;

    public TheSystem()
    {
        Components = new Dictionary<string, Hardware>();
    }

    public void RegisterHeavyHardware(string name, long capacity, long memory)
    {
        Components.Add(name, new HeavyHardware(name, capacity, memory));
    }

    public void RegisterPowerHardware(string name, long capacity, long memory)
    {
        Components.Add(name, new PowerHardware(name, capacity, memory));
    }

    public void RegisterExpressSoftware(string hardwareComponentName, string name, int capacity, int memory)
    {
        var software = new ExpressSoftware(name, capacity, memory);
        if (Components.ContainsKey(hardwareComponentName))
        {
            Components[hardwareComponentName].AddSoftware(software);
        }
    }

    public void RegisterLightSoftware(string hardwareComponentName, string name, int capacity, int memory)
    {
        var software = new LightSoftware(name, capacity, memory);
        if (Components.ContainsKey(hardwareComponentName))
        {
            Components[hardwareComponentName].AddSoftware(software);
        }
    }

    public void ReleaseSoftwareComponent(string hardwareComponentName, string softwareComponentName)
    {
        if (Components.ContainsKey(hardwareComponentName))
        {
            Components[hardwareComponentName].ReleaseSoftware(softwareComponentName);
        }
    }

    public void ExecuteCommand(string input)
    {
        string[] commandLine = input.Split("(", StringSplitOptions.RemoveEmptyEntries);
        string command = commandLine[0];
        string[] args = commandLine[1].Remove(commandLine[1].Length - 1).Trim().Split(", ", StringSplitOptions.RemoveEmptyEntries);

        switch (command)
        {
            case "RegisterPowerHardware":
                RegisterPowerHardware(args[0], long.Parse(args[1]), long.Parse(args[2]));
                break;
            case "RegisterHeavyHardware":
                RegisterHeavyHardware(args[0], long.Parse(args[1]), long.Parse(args[2]));
                break;
            case "Analyze":
                Console.WriteLine(Analyze());
                break;
            case "RegisterLightSoftware":
                RegisterLightSoftware(args[0], args[1], int.Parse(args[2]), int.Parse(args[3]));
                break;
            case "RegisterExpressSoftware":
                RegisterExpressSoftware(args[0], args[1], int.Parse(args[2]), int.Parse(args[3]));
                break;
            case "ReleaseSoftwareComponent":
                ReleaseSoftwareComponent(args[0], args[1]);
                break;
            default:
                break;
        }
    }

    public string Analyze()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("System Analysis");
        sb.Append(Environment.NewLine);
        sb.Append($"Hardware Components: {Components.Count}");
        sb.Append(Environment.NewLine);
        sb.Append($"Software Components: {Components.Values.Sum(x => x.GetSoftwareCount())}");
        sb.Append(Environment.NewLine);

        long memoryInUse = Components.Values.Sum(x => x.TakenMemory);
        long capacityInUse = Components.Values.Sum(x => x.TakenCapacity);
        long totalMemory = Components.Values.Sum(x => x.MaxMemory);
        long totalCapacity = Components.Values.Sum(x => x.MaxCapacity);

        sb.Append($"Total Operational Memory: {memoryInUse} / {totalMemory}");
        sb.Append(Environment.NewLine);
        sb.Append($"Total Capacity Taken: {capacityInUse} / {totalCapacity}");
        return sb.ToString();
    }

    public void End()
    {
        foreach (var component in Components)
        {
            Console.WriteLine(component.Value);
        }
    }
}


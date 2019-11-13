using System;
using System.Collections.Generic;

public class HarvesterFactory
{
    public static Harvester CreateHarvester(List<string> args)
    {
        string type = args[0];
        string id = args[1];
        double oreOutput = double.Parse(args[2]);
        double energyReq = double.Parse(args[3]);
        switch (type)
        {
            case "Sonic":
                int sonicFactor = int.Parse(args[4]);
                return new SonicHarvester(id, oreOutput, energyReq, sonicFactor);
            case "Hammer":
                return new HammerHarvester(id, oreOutput, energyReq);
            default:
                throw new ArgumentException("Type");
        }
    }
}

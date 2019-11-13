using System;
using System.Collections.Generic;

public static class BenderFactory
{
    public static Bender GetBender(List<string> args)
    {
        string type = args[1];
        string name = args[2];
        int power = int.Parse(args[3]);
        double specialty = double.Parse(args[4]);

        switch (type)
        {
            case "Air":
                return new AirBender(name, power, specialty);
            case "Water":
                return new WaterBender(name, power, specialty);
            case "Earth":
                return new EarthBender(name, power, specialty);
            case "Fire":
                return new FireBender(name, power, specialty);
            default:
                throw new ArgumentException("Invalid Bender Type");
        }
    }
}


using System;
using System.Collections.Generic;

public static class MonumentFactory
{
    public static Monument GetMonument(List<string> args)
    {
        string type = args[1];
        string name = args[2];
        int power = int.Parse(args[3]);

        switch (type)
        {
            case "Air":
                return new AirMonument(name, power);
            case "Water":
                return new WaterMonument(name, power);
            case "Earth":
                return new EarthMonument(name, power);
            case "Fire":
                return new FireMonument(name, power);
            default:
                throw new ArgumentException("Invalid Monument Type");
        }
    }
}

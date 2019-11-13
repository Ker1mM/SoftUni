using System;
using System.Collections.Generic;
using System.Linq;

public class DriverFactory
{
    public static Driver GetDriver(string type, string name, Car car)
    {
        switch (type)
        {
            case "Aggressive":
                return new AggressiveDriver(name, car);
            case "Endurance":
                return new EnduranceDriver(name, car);
            default:
                throw new ArgumentException("Invalid Driver Type!");
        }
    }
}

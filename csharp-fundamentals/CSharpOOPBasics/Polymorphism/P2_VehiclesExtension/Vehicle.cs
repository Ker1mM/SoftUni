using System;

public abstract class Vehicle
{
    private double fuelQuantity;
    private double fuelPerKm;
    protected double refuelConstant;
    private double tankCapacity;

    public double TankCapacity
    {
        get { return tankCapacity; }
        private set
        {
            tankCapacity = value;
        }
    }


    public double FuelPerKm
    {
        get { return fuelPerKm; }
        set { fuelPerKm = value; }
    }

    public double FuelQuantity
    {
        get => fuelQuantity;
        set
        {
            fuelQuantity = value;
        }
    }

    public Vehicle(double fuelQuantity, double literPerKm, double tankCap)
    {
        TankCapacity = tankCap;
        FuelQuantity = fuelQuantity > tankCap ? 0 : fuelQuantity;
        FuelPerKm = literPerKm;
    }

    public string Drive(double distance)
    {
        double neededFuel = distance * FuelPerKm;
        if (neededFuel > FuelQuantity)
        {
            return $"{this.GetType().FullName} needs refueling";
        }
        else
        {
            FuelQuantity -= neededFuel;
            return $"{this.GetType().FullName} travelled {distance} km";
        }
    }

    public virtual string DriveEmpty(double distance) { return "Not Implemented"; }

    public void Refuel(double fuel)
    {
        if (fuel <= 0)
        {
            throw new ArgumentException("Fuel must be a positive number");
        }
        else if (fuel + FuelQuantity > TankCapacity)
        {
            throw new ArgumentException($"Cannot fit {fuel} fuel in the tank");
        }
        FuelQuantity += fuel * refuelConstant;
    }

    public void ExecuteCommand(string command, double argument)
    {
        if (command == "Drive")
        {
            Console.WriteLine(this.Drive(argument));
        }
        else if (command == "Refuel")
        {
            this.Refuel(argument);
        }
        else if (command == "DriveEmpty")
        {
            Console.WriteLine(this.DriveEmpty(argument));
        }
    }

    public override string ToString()
    {
        return $"{this.GetType().FullName}: {FuelQuantity:F2}";
    }
}

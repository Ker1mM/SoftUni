public abstract class Vehicle
{
    private double fuelQuantity;
    private double fuelPerKm;
    protected double refuelConstant;

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

    public Vehicle(double fuelQuantity, double literPerKm)
    {
        FuelQuantity = fuelQuantity;
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

    public void Refuel(double fuel)
    {
        FuelQuantity += fuel * refuelConstant;
    }

    public void ExecuteCommand(string command, double argument)
    {
        if (command == "Drive")
        {
            System.Console.WriteLine(this.Drive(argument));
        }
        else if (command == "Refuel")
        {
            this.Refuel(argument);
        }
    }

    public override string ToString()
    {
        return $"{this.GetType().FullName}: {FuelQuantity:F2}";
    }
}

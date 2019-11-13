public class Bus : Vehicle
{
    public Bus(double fuelQuantity, double literPerKm, double tankCap) : base(fuelQuantity, literPerKm + 1.4, tankCap)
    {
        refuelConstant = 1;
    }

    public override string DriveEmpty(double distance)
    {
        double neededFuel = distance * (FuelPerKm - 1.4);
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
}

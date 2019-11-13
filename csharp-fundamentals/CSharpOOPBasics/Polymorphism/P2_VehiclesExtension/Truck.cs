public class Truck : Vehicle
{
    public Truck(double fuelQuantity, double fuelPerKm, double tankCap) : base(fuelQuantity, fuelPerKm + 1.6, tankCap)
    {
        refuelConstant = 0.95;
    }
}

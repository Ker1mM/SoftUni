public class Truck : Vehicle
{
    public Truck(double fuelQuantity, double fuelPerKm) : base(fuelQuantity, fuelPerKm + 1.6)
    {
        refuelConstant = 0.95;
    }
}

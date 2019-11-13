public class Car : Vehicle
{
    public Car(double fuelQuantity, double fuelPerKm, double tankCap) : base(fuelQuantity, fuelPerKm + 0.9, tankCap)
    {
        refuelConstant = 1;
    }
}

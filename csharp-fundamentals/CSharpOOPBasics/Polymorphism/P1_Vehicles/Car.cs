public class Car : Vehicle
{
    public Car(double fuelQuantity, double fuelPerKm) : base(fuelQuantity, fuelPerKm + 0.9)
    {
        refuelConstant = 1;
    }
}

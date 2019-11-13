using System;

namespace HotelReservation
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] inputArgs = Console.ReadLine().Split();
            decimal pricePerDay = decimal.Parse(inputArgs[0]);
            int days = int.Parse(inputArgs[1]);
            Season season = PriceCalculator.GetSeason(inputArgs[2]);
            Discount discount = PriceCalculator.GetDiscount(inputArgs);

            decimal result = PriceCalculator.Calculate(pricePerDay, days, season, discount);
            Console.WriteLine($"{result:f2}");
        }
    }
}

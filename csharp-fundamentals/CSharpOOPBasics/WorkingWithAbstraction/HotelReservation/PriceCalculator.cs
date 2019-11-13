namespace HotelReservation
{
    public static class PriceCalculator
    {
        public static decimal Calculate(
            decimal pricePerDay,
            int days,
            Season season,
            Discount discount)
        {
            pricePerDay *= (int)season;
            decimal totalPrice = days * pricePerDay;
            totalPrice *= (decimal)((100 - (int)discount) / 100.00);
            return totalPrice;
        }

        public static Season GetSeason(string season)
        {
            switch (season)
            {
                case "Autumn":
                    return Season.Autumn;
                case "Spring":
                    return Season.Spring;
                case "Winter":
                    return Season.Winter;
                case "Summer":
                    return Season.Summer;
                default:
                    return Season.None;
            }
        }

        public static Discount GetDiscount(string[] args)
        {
            string discount = "None";
            if (args.Length > 3)
            {
                discount = args[3];
            }
            switch (discount)
            {
                case "SecondVisit":
                    return Discount.SecondVisit;
                case "VIP":
                    return Discount.VIP;
                default:
                    return Discount.None;
            }
        }
    }


}

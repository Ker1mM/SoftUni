
using System;

public static class FoodFactory
{
    public static Food GetFood(string[] args)
    {
        string type = args[0];
        int quantity = int.Parse(args[1]);
        switch (type)
        {
            case "Fruit":
                return new Fruit(quantity);
            case "Meat":
                return new Meat(quantity);
            case "Seeds":
                return new Seeds(quantity);
            case "Vegetable":
                return new Vegetable(quantity);
            default:
                throw new ArgumentException("Invalid type!");
        }
    }
}


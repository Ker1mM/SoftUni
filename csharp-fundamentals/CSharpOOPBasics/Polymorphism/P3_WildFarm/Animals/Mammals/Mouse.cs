using System;
using System.Collections.Generic;

public class Mouse : Mammal
{
    public Mouse(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
    {
    }

    public override string MakeSound()
    {
        return "Squeak";
    }

    protected override List<string> GetFavoredFood()
    {
        return new List<string> { "Vegetable", "Fruit" };
    }

    public override double GetTotalWeight()
    {
        return Weight + (FoodEaten * 0.10);
    }
}


using System.Collections.Generic;

public class Owl : Bird
{
    public Owl(string name, double weight, double wingSize) : base(name, weight, wingSize)
    {

    }

    public override string MakeSound()
    {
        return "Hoot Hoot";
    }

    protected override List<string> GetFavoredFood()
    {
        return new List<string> { "Meat" };
    }

    public override double GetTotalWeight()
    {
        return Weight + (FoodEaten * 0.25);
    }
}

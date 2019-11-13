using System.Collections.Generic;

public class Cat : Feline
{
    public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
    {

    }

    public override string MakeSound()
    {
        return "Meow";
    }

    protected override List<string> GetFavoredFood()
    {
        return new List<string> { "Vegetable", "Meat" };
    }

    public override double GetTotalWeight()
    {
        return Weight + (FoodEaten * 0.30);
    }
}


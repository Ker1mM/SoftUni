using System.Collections.Generic;

public class Dog : Mammal
{
    public Dog(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
    {

    }

    public override string MakeSound()
    {
        return "Woof!";
    }

    protected override List<string> GetFavoredFood()
    {
        return new List<string> { "Meat" };
    }

    public override double GetTotalWeight()
    {
        return Weight + (FoodEaten * 0.40);
    }
}


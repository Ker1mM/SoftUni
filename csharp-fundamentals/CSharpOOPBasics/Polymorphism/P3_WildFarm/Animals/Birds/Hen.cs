public class Hen : Bird
{
    public Hen(string name, double weight, double wingSize) : base(name, weight, wingSize)
    {
    }

    public override string MakeSound()
    {
        return "Cluck";
    }

    public override double GetTotalWeight()
    {
        return Weight + (FoodEaten * 0.35);
    }
}
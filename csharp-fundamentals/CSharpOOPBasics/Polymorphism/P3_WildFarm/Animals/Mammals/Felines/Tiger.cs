using System.Collections.Generic;

public class Tiger : Feline
{
    public Tiger(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
    {

    }

    public override string MakeSound()
    {
        return "ROAR!!!";
    }

    protected override List<string> GetFavoredFood()
    {
        return new List<string> { "Meat" };
    }

}


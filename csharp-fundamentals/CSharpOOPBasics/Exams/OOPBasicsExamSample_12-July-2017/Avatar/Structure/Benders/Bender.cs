public abstract class Bender
{
    public string Name { get; set; }
    public int Power { get; set; }

    public Bender(string name, int power)
    {
        Name = name;
        Power = power;
    }

    public abstract double GetPower();
}


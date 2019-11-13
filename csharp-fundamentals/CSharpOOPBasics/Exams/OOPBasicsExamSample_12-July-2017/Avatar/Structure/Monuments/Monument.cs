public abstract class Monument
{
    public string Name { get; set; }

    public Monument(string name)
    {
        Name = name;
    }

    public abstract int GetPower();
}


using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class SoftUniAttribute : Attribute
{
    private string name;

    public string Name
    {
        get { return name; }
        private set { name = value; }
    }

    public SoftUniAttribute(string name)
    {
        this.Name = name;
    }
}



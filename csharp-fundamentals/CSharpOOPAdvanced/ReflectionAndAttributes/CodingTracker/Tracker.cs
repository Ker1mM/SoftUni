using System;
using System.Linq;
using System.Reflection;
using System.Text;

public class Tracker
{
    public void PrintMethodsByAuthor()
    {
        var type = Type.GetType("StartUp");
        var sb = new StringBuilder();

        MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

        foreach (var method in methods)
        {
            if (method.CustomAttributes.Any(x => x.AttributeType == typeof(SoftUniAttribute)))
            {
                var atts = method.GetCustomAttributes(false);
                foreach (SoftUniAttribute att in atts)
                {
                    sb.AppendLine($"{method.Name} is written by {att.Name}");
                }
            }
        }

        Console.WriteLine(sb.ToString().Trim());
    }
}

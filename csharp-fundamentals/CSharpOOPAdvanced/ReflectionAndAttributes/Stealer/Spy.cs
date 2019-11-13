using System;
using System.Linq;
using System.Reflection;
using System.Text;

public class Spy
{
    public string StealFieldInfo(string className, string username, string password)
    {
        Type type = Type.GetType(className);

        var sb = new StringBuilder();
        sb.AppendLine($"Class under investigation: {className}");

        FieldInfo[] allFields = type.GetFields
            (BindingFlags.Static |
            BindingFlags.NonPublic |
            BindingFlags.Public |
            BindingFlags.Instance);

        Object classInstance = Activator.CreateInstance(type, new object[] { });

        foreach (var field in allFields.Where(x => x.Name == username || x.Name == password))
        {
            sb.AppendLine($"{field.Name} = {field.GetValue(classInstance).ToString()}");
        }

        return sb.ToString().TrimEnd();
    }
}

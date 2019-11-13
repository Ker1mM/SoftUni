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

    public string CollectGettersAndSetters(string className)
    {
        Type type = Type.GetType(className);

        MethodInfo[] methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var sb = new StringBuilder();

        foreach (var getter in methods.Where(x => x.Name.StartsWith("get")))
        {
            sb.AppendLine($"{getter.Name} will return {getter.ReturnType}");
        }

        foreach (var setter in methods.Where(x => x.Name.StartsWith("set")))
        {
            sb.AppendLine($"{setter.Name} will set field of {setter.GetParameters().First().ParameterType}");
        }

        return sb.ToString().Trim();
    }

    public string RevealPrivateMethods(string className)
    {
        Type type = Type.GetType(className);
        var sb = new StringBuilder();

        sb.AppendLine($"All Private Methods of Class: {className}");
        sb.AppendLine($"Base Class: {type.BaseType.Name}");

        MethodInfo[] privateMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var method in privateMethods)
        {
            sb.AppendLine(method.Name);
        }


        return sb.ToString().TrimEnd();
    }

    public string AnalyzeAcessModifiers(string className)
    {
        Type type = Type.GetType(className);

        var sb = new StringBuilder();

        MethodInfo[] publicMethods = type.GetMethods
            (BindingFlags.Public |
            BindingFlags.Instance);

        MethodInfo[] nonPublicMethods = type.GetMethods
            (BindingFlags.NonPublic |
            BindingFlags.Instance);

        FieldInfo[] publicFields = type.GetFields
            (BindingFlags.Static |
            BindingFlags.Public |
            BindingFlags.Instance);

        foreach (var field in publicFields)
        {
            sb.AppendLine($"{field.Name} must be private!");
        }
        foreach (var method in nonPublicMethods.Where(x => x.Name.StartsWith("get")))
        {
            sb.AppendLine($"{method.Name} have to be public!");
        }
        foreach (var method in publicMethods.Where(x => x.Name.StartsWith("set")))
        {
            sb.AppendLine($"{method.Name} have to be private!");
        }

        return sb.ToString().TrimEnd();
    }
}

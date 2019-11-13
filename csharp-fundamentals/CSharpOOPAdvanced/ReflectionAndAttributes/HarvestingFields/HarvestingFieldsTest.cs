namespace P01_HarvestingFields
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class HarvestingFieldsTest
    {
        public static void Main()
        {
            string input;
            var sb = new StringBuilder();
            Type type = Type.GetType("P01_HarvestingFields.HarvestingFields");
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
            while ((input = Console.ReadLine()) != "HARVEST")
            {
                List<FieldInfo> result = fields.ToList();
                switch (input)
                {
                    case "private":
                        result = fields.Where(x => x.IsPrivate).ToList();
                        break;
                    case "protected":
                        result = fields.Where(x => x.IsFamily).ToList();
                        break;
                    case "public":
                        result = fields.Where(x => x.IsPublic).ToList();
                        break;
                    default:
                        break;
                }

                foreach (var field in result)
                {
                    sb.AppendLine($"{GetModifier(field)} {field.FieldType.Name} {field.Name}");
                }
            }

            Console.WriteLine(sb.ToString().Trim());
        }

        private static string GetModifier(FieldInfo field)
        {
            string result = "public";
            if (field.IsFamily)
            {
                result = "protected";
            }
            else if (field.IsPrivate)
            {
                result = "private";
            }

            return result;
        }

    }
}

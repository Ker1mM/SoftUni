using SoftUni.Data;
using System;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (var softUniContext = new SoftUniContext())
            {
                string result = GetAddressesByTown(softUniContext);
                Console.Write(result);
            }
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();


            var addresses = context.Addresses
                .OrderByDescending(x => x.Employees.Count)
                .ThenBy(x => x.Town.Name)
                .Take(10)
                .Select(x => new
                {
                    x.Employees,
                    x.AddressText,
                    TownName = x.Town.Name,
                    ResidentCount = x.Employees.Count
                });

            foreach (var item in addresses)
            {
                sb.AppendLine($"{item.AddressText}, {item.TownName} - {item.ResidentCount} employees");
            }

            return sb.ToString();
        }
    }
}

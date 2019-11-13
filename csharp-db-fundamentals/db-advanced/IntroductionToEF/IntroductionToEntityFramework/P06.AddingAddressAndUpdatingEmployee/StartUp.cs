using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var softUniContext = new SoftUniContext())
            {
                string result = AddNewAddressToEmployee(softUniContext);
                Console.Write(result);
            }
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            var employee = context.Employees
                .FirstOrDefault(x => x.LastName == "Nakov");

            employee.Address = address;

            context.SaveChanges();

            var employees = context.Employees
                .OrderByDescending(x => x.AddressId)
                .Take(10)
                .Select(e => new
                {
                    Address = e.Address.AddressText
                })
                .ToArray();

            foreach (var employeeAddress in employees)
            {
                sb.AppendLine($"{employeeAddress.Address}");
            }

            return sb.ToString();
        }
    }
}

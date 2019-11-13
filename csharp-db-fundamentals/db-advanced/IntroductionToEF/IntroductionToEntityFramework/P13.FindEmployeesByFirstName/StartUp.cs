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
                string result = GetEmployeesByFirstNameStartingWithSa(softUniContext);
                Console.Write(result);
            }
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(x => x.FirstName.StartsWith("Sa"))
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName);

            foreach (var employee in employees)
            {
                string fullName = employee.FirstName + " " + employee.LastName;
                sb.AppendLine($"{fullName} - {employee.JobTitle} - (${employee.Salary:f2})");
            }

            return sb.ToString();
        }
    }
}

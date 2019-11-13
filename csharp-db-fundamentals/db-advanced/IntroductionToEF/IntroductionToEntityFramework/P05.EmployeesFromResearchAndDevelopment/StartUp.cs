using SoftUni.Data;
using System;
using System.Globalization;
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
                string result = GetEmployeesFromResearchAndDevelopment(softUniContext);
                Console.Write(result);
            }
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepName = e.Department.Name,
                    e.Salary
                })
                .Where(x => x.DepName == "Research and Development")
                .OrderBy(x => x.Salary)
                .ThenByDescending(x => x.FirstName)
                .ToArray();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.DepName} - ${employee.Salary:f2}");
            }

            return sb.ToString();
        }
    }
}

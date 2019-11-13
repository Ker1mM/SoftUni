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
                string result = IncreaseSalaries(softUniContext);
                Console.Write(result);
            }
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(x => IsInCorrectDepartment(x.Department.Name))
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName);

            foreach (var employee in employees)
            {
                employee.Salary *= 1.12m;
                sb.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
            }

            return sb.ToString();
        }

        private static bool IsInCorrectDepartment(string department)
        {
            bool result = false;

            if (department == "Engineering" ||
                department == "Tool Design" ||
                department == "Marketing" ||
                department == "Information Services")
            {
                result = true;
            }

            return result;
        }
    }
}

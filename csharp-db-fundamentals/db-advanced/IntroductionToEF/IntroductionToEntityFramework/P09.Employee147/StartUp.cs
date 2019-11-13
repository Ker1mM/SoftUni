using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Globalization;
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
                string result = GetEmployee147(softUniContext);
                Console.Write(result);
            }
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employee = context.Employees
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.JobTitle,
                    x.EmployeeId,
                    Projects = x.EmployeesProjects
                   .Select(y => new
                   {
                       y.Project.Name
                   })
                    .OrderBy(y => y.Name)
                })
                .FirstOrDefault(x => x.EmployeeId == 147);

            sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            foreach (var project in employee.Projects)
            {
                sb.AppendLine($"{project.Name}");
            }

            return sb.ToString();
        }
    }
}

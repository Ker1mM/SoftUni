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
                string result = GetEmployeesInPeriod(softUniContext);
                Console.Write(result);
            }
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(x => x.EmployeesProjects
                    .Any(y => y.Project.StartDate.Year >= 2001 && y.Project.StartDate.Year <= 2003))
                .Select(x => new
                {
                    EmployeeName = x.FirstName + " " + x.LastName,
                    ManagerName = x.Manager.FirstName + " " + x.Manager.LastName,
                    Projects = x.EmployeesProjects.Select(y => new
                    {
                        ProjectName = y.Project.Name,
                        ProjectStartDate = y.Project.StartDate,
                        ProjectEndDate = y.Project.EndDate
                    })
                })
                .Take(10)
                .ToArray();


            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.EmployeeName} - Manager: {employee.ManagerName}");
                foreach (var project in employee.Projects)
                {
                    string startDate = project.ProjectStartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                    string endDate = project.ProjectEndDate.HasValue ?
                        project.ProjectEndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) : "not finished";
                    sb.AppendLine($"--{project.ProjectName} - {startDate} - {endDate}");
                }
            }

            return sb.ToString();
        }
    }
}

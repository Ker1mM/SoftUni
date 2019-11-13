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
                string result = GetDepartmentsWithMoreThan5Employees(softUniContext);
                Console.Write(result);
            }
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var deps = context.Departments
                .Where(x => x.Employees.Count > 5)
                .OrderBy(x => x.Employees.Count)
                .ThenBy(x => x.Name)
                .Select(x => new
                {
                    DepartmentName = x.Name,
                    ManagerName = x.Manager.FirstName + " " + x.Manager.LastName,
                    Employees = x.Employees
                    .OrderBy(y => y.FirstName)
                    .ThenBy(y => y.LastName)
                    .Select(y => new
                    {
                        EmployeeName = y.FirstName + " " + y.LastName,
                        y.JobTitle
                    })
                });

            foreach (var department in deps)
            {
                sb.AppendLine($"{department.DepartmentName} - {department.ManagerName}");
                foreach (var employee in department.Employees)
                {
                    sb.AppendLine($"{employee.EmployeeName} - {employee.JobTitle}");
                }
            }

            return sb.ToString();
        }
    }
}

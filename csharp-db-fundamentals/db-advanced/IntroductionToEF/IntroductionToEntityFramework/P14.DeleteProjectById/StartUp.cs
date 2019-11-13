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
                string result = DeleteProjectById(softUniContext);
                Console.Write(result);
            }
        }

        public static string DeleteProjectById(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employeeProjects = context.EmployeesProjects
                .Where(x => x.ProjectId == 2);

            context.EmployeesProjects.RemoveRange(employeeProjects);

            var project = context.Projects.Find(2);
            context.Projects.Remove(project);

            context.SaveChanges();

            var projects = context.Projects.Take(10);

            foreach (var item in projects)
            {
                sb.AppendLine(item.Name);
            }

            return sb.ToString();
        }
    }
}

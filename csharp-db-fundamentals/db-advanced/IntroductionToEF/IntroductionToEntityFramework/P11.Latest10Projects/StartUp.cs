using SoftUni.Data;
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
                string result = GetLatestProjects(softUniContext);
                Console.Write(result);
            }
        }

        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var lastTenProjects = context.Projects
                .OrderByDescending(x => x.StartDate)
                .Take(10)
                .OrderBy(x => x.Name);

            foreach (var project in lastTenProjects)
            {
                string startDate = project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                sb.AppendLine($"{project.Name}");
                sb.AppendLine($"{project.Description}");
                sb.AppendLine($"{startDate}");
            }

            return sb.ToString();
        }
    }
}

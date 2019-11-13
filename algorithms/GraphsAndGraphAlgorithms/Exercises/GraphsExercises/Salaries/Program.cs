using System;
using System.Collections.Generic;
using System.Linq;

namespace Salaries
{
    class Program
    {
        private static Dictionary<int, List<int>> graph;
        private static long[] salaries;

        static void Main(string[] args)
        {
            graph = new Dictionary<int, List<int>>();
            int employeeCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < employeeCount; i++)
            {
                var input = Console.ReadLine().ToCharArray().ToList();
                graph.Add(i, new List<int>());

                while (input.Contains('Y'))
                {
                    int index = input.IndexOf('Y');
                    input[index] = 'N';
                    graph[i].Add(index);
                }
            }
            graph = graph
                .OrderBy(x => x.Value.Count)
                .ToDictionary(x => x.Key, x => x.Value);

            salaries = new long[employeeCount];

            foreach (var employee in graph.Keys)
            {
                if (salaries[employee] == 0)
                {
                    FindSalary(employee);
                }
            }

            var result = salaries.Sum();
            Console.WriteLine(result);
        }

        private static void FindSalary(int employee)
        {
            if (graph[employee].Count == 0)
            {
                salaries[employee] = 1;
            }
            else
            {
                long salary = 0;
                foreach (var managedEmployee in graph[employee])
                {
                    if (salaries[managedEmployee] == 0)
                    {
                        FindSalary(managedEmployee);
                    }

                    salary += salaries[managedEmployee];
                }
                salaries[employee] = salary;
            }
        }
    }
}

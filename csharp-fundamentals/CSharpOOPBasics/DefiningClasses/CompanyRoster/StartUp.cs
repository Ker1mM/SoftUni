using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            Dictionary<string, List<Employee>> departments = new Dictionary<string, List<Employee>>();
            for (int i = 0; i < count; i++)
            {
                Employee current = new Employee(Console.ReadLine());
                if (!departments.ContainsKey(current.Department))
                {
                    departments.Add(current.Department, new List<Employee>());
                }
                departments[current.Department].Add(current);
            }

            var highestSalaryDepartment = departments
                .OrderByDescending(x => x.Value.Sum(y => y.Salary))
                .First();

            Console.WriteLine($"Highest Average Salary: {highestSalaryDepartment.Key}");
            foreach (var employee in highestSalaryDepartment.Value.OrderByDescending(x => x.Salary))
            {
                Console.WriteLine($"{employee.Name} {employee.Salary:f2} {employee.Email} {employee.Age}");
            }
        }
    }
}

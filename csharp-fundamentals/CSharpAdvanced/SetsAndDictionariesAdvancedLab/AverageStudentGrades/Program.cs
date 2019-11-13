using System;
using System.Collections.Generic;
using System.Linq;

namespace AverageStudentGrades
{
    class Program
    {
        static void Main(string[] args)
        {
            int gradesCount = int.Parse(Console.ReadLine());

            Dictionary<string, List<double>> grades = new Dictionary<string, List<double>>();
            while (gradesCount-- > 0)
            {
                string input = Console.ReadLine();
                string[] tokens = input.Split(" ");
                string name = tokens[0];
                double grade = double.Parse(tokens[1]);

                if (grades.ContainsKey(name) == false)
                {
                    grades.Add(name, new List<double>());
                }
                grades[name].Add(grade);
            }

            foreach (var student in grades)
            {
                Console.Write("{0} -> ", student.Key);
                foreach (var mark in student.Value)
                {
                    Console.Write("{0:f2} ", mark);
                }
                Console.WriteLine("(avg: {0:f2})", student.Value.Average());

            }
        }
    }
}

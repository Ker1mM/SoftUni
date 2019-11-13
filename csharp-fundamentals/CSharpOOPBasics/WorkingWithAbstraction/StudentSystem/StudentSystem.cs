using System;
using System.Collections.Generic;

namespace StudentSystem
{
    public static class StudentSystem
    {
        public static bool TryGetInfo(Dictionary<string, Student> repo, string input, out string result)
        {
            bool print = false;
            result = "";
            string[] args = input.Split();

            if (args[0] == "Create")
            {
                Create(repo, args);
            }
            else if (args[0] == "Show")
            {
                result = Show(repo, args);
                if (result != "")
                {
                    print = true;
                }
            }
            return print;
        }

        private static void Create(Dictionary<string, Student> repo, string[] args)
        {
            var name = args[1];
            var age = int.Parse(args[2]);
            var grade = double.Parse(args[3]);
            if (!repo.ContainsKey(name))
            {
                var student = new Student(name, age, grade);
                repo[name] = student;
            }
        }

        private static string Show(Dictionary<string, Student> repo, string[] args)
        {
            var name = args[1];
            string view = "";
            if (repo.ContainsKey(name))
            {
                var student = repo[name];
                view = $"{student.Name} is {student.Age} years old.";

                if (student.Grade >= 5.00)
                {
                    view += " Excellent student.";
                }
                else if (student.Grade < 5.00 && student.Grade >= 3.50)
                {
                    view += " Average student.";
                }
                else
                {
                    view += " Very nice person.";
                }
            }
            return view;
        }
    }
}

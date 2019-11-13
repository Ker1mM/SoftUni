using System;
using System.Collections.Generic;
using System.Text;

namespace DefiningClasses
{
    public class Employee
    {
        private string name;
        private decimal salary;
        private string position;
        private string department;
        private string email;
        private int age;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public decimal Salary
        {
            get { return this.salary; }
            set { this.salary = value; }
        }

        public string Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public string Department
        {
            get { return this.department; }
            set { this.department = value; }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        public int Age
        {
            get { return this.age; }
            set { this.age = value; }
        }

        public Employee(string employeeInfo)
        {
            SetEmployee(employeeInfo);
        }

        private void SetEmployee(string employeeInfo)
        {
            string[] tokens = employeeInfo.Split();
            this.Name = tokens[0];
            this.Salary = decimal.Parse(tokens[1]);
            this.Position = tokens[2];
            this.Department = tokens[3];
            this.Email = "n/a";
            this.Age = -1;

            if (tokens.Length == 6)
            {
                this.Email = tokens[4];
                this.Age = int.Parse(tokens[5]);
            }
            else if (tokens.Length == 5)
            {
                if (int.TryParse(tokens[4], out int age))
                {
                    this.Age = age;
                }
                else
                {
                    this.Email = tokens[4];
                }
            }
        }
    }
}

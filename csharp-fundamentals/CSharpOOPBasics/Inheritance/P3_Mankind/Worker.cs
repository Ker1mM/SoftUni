using System;
using System.Text;

namespace P3_Mankind
{
    public class Worker : Human
    {
        private decimal weekSalary;
        private decimal workHours;

        public decimal SalaryPerHour
        {
            get
            {
                return WeekSalary / (WorkHours * 5m);
            }
        }

        public decimal WorkHours
        {
            get { return workHours; }
            set
            {
                if (value < 1 || value > 12)
                {
                    throw new ArgumentException($"Expected value mismatch! Argument: workHoursPerDay");
                }
                workHours = value;
            }
        }


        public decimal WeekSalary
        {
            get { return weekSalary; }
            set
            {
                if (value <= 10)
                {
                    throw new ArgumentException($"Expected value mismatch! Argument: weekSalary");
                }
                weekSalary = value;
            }
        }

        public Worker(string firstName, string lastName, decimal salary, decimal workHours) : base(firstName, lastName)
        {
            WorkHours = workHours;
            WeekSalary = salary;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append($"Week Salary: {WeekSalary:F2}\n");
            sb.Append($"Hours per day: {WorkHours:F2}\n");
            sb.Append($"Salary per hour: {SalaryPerHour:F2}");
            return sb.ToString();
        }
    }
}

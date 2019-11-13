namespace Google
{
    public class Company
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }

        public Company()
        {
            this.Name = "";
            this.Department = "";
            this.Salary = 0;
        }

        public Company(string name, string department, string salary)
        {
            this.Name = name;
            this.Department = department;
            this.Salary = decimal.Parse(salary);
        }

        public override string ToString()
        {
            string result = "Company:";
            if (this.Name != "")
            {
                result += $"\n{this.Name} {this.Department} {this.Salary:f2}";
            }
            return result;
        }
    }
}

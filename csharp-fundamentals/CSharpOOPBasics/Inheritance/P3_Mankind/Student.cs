using System;
using System.Text;

namespace P3_Mankind
{
    public class Student : Human
    {
        private string facultyNumber;

        public string FacultyNumber
        {
            get { return facultyNumber; }
            set
            {
                if (value.Length < 5 || value.Length > 10 || !CheckFacultyNumber(value))
                {
                    throw new ArgumentException("Invalid faculty number!");
                }
                facultyNumber = value;
            }
        }

        private bool CheckFacultyNumber(string number)
        {
            bool result = true;
            foreach (var symbol in number)
            {
                if (!Char.IsLetterOrDigit(symbol))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public Student(string firstName, string lastName, string facultyNumber) : base(firstName, lastName)
        {
            FacultyNumber = facultyNumber;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append($"Faculty number: {FacultyNumber}");
            return sb.ToString();
        }

    }
}

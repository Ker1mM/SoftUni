using System;
using System.Text;

namespace P3_Mankind
{
    public class Human
    {
        private string firstName;
        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value.Length <= 2)
                {
                    throw new ArgumentException($"Expected length at least 3 symbols! Argument: lastName");
                }
                else if (!char.IsUpper(value.Trim()[0]))
                {
                    throw new ArgumentException($"Expected upper case letter! Argument: lastName");
                }
                lastName = value;
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (value.Length <= 3)
                {
                    throw new ArgumentException($"Expected length at least 4 symbols! Argument: firstName");
                }
                else if (!char.IsUpper(value.Trim()[0]))
                {
                    throw new ArgumentException($"Expected upper case letter! Argument: firstName");
                }
                firstName = value;
            }
        }

        public Human(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"First Name: {FirstName}\n");
            sb.Append($"Last Name: {LastName}\n");
            return sb.ToString();
        }
    }
}

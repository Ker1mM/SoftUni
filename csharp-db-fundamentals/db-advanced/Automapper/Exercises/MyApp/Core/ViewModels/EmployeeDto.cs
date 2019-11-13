

namespace MyApp.Core.ViewModels
{
    using MyApp.Models;
    using System;

    public class EmployeeDto
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }
    }
}


namespace MyApp.Core.Commands
{
    using AutoMapper;
    using MyApp.Core.Contracts;
    using MyApp.Core.ViewModels;
    using MyApp.Data;
    using MyApp.Models;
    using System;

    public class AddEmployeeCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public AddEmployeeCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            if (inputArgs.Length < 3)
            {
                throw new ArgumentOutOfRangeException(null, "Provided arguments are incorrect!");
            }

            string firstName = inputArgs[0];
            string lastName = inputArgs[1];
            decimal salary = decimal.Parse(inputArgs[2]);

            if (salary < 0)
            {
                throw new ArgumentException("Salary cannot be negative!");
            }

            var employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary
            };

            this.context.Employees.Add(employee);
            this.context.SaveChanges();

            var employeeDto = this.mapper.CreateMappedObject<EmployeeDto>(employee);

            string result = $"Registered successfully: {employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:f2}";

            return result;
        }
    }
}


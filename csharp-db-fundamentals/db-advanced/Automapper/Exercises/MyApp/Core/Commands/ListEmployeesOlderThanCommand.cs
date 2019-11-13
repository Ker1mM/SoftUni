
namespace MyApp.Core.Commands
{
    using AutoMapper;
    using MyApp.Core.Contracts;
    using MyApp.Core.ViewModels;
    using MyApp.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public ListEmployeesOlderThanCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            if (inputArgs.Length != 1)
            {
                throw new ArgumentOutOfRangeException(null, "Provided arguments are incorrect!");
            }

            int age = int.Parse(inputArgs[0]);

            var birthday = DateTime.Now.AddYears((age + 1) * -1).Date;

            var employees = context.Employees
                .Where(e => e.Birthday < birthday)
                .ToList();

            if (employees.Count() == 0)
            {
                throw new ArgumentNullException(null, $"There are no employees older than {age}");
            }


            var sb = new StringBuilder();
            foreach (var employee in employees.OrderByDescending(x => x.Salary))
            {
                var employeeDto = this.mapper.CreateMappedObject<EmployeeDto>(employee);
                var employeeManagerDto = this.mapper.CreateMappedObject<EmployeeManagerDto>(employee);
                var managerInfo = "[no manager]";
                if (employeeManagerDto.Manager != null)
                {
                    managerInfo = employeeManagerDto.Manager.LastName;
                }

                sb.AppendLine($"{employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:f2} - Manager: {managerInfo}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}

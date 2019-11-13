
namespace MyApp.Core.Commands
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using MyApp.Core.Contracts;
    using MyApp.Core.ViewModels;
    using MyApp.Data;
    using System;
    using System.Linq;
    using System.Text;

    public class ManagerInfoCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public ManagerInfoCommand(MyAppContext context, Mapper mapper)
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

            int managerId = int.Parse(inputArgs[0]);

            var manager = context.Employees
                .Include(m => m.ManagedEmployees)
                .FirstOrDefault(x => x.Id == managerId);

            if (manager == null)
            {
                throw new ArgumentNullException(null, $"Employee with ID: {managerId} cannot be found!");
            }

            var managerDto = this.mapper.CreateMappedObject<ManagerDto>(manager);

            var sb = new StringBuilder();

            sb.AppendLine($"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.ManagedEmployees.Count}");
            foreach (var employee in managerDto.ManagedEmployees)
            {
                sb.AppendLine($"    - {employee.FirstName} {employee.LastName} - ${employee.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}

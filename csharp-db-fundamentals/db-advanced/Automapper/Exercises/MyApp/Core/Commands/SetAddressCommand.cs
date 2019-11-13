using AutoMapper;

namespace MyApp.Core.Commands
{
    using MyApp.Core.Contracts;
    using MyApp.Core.ViewModels;
    using MyApp.Data;
    using System;
    using System.Linq;

    class SetAddressCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public SetAddressCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            if (inputArgs.Length < 2)
            {
                throw new ArgumentOutOfRangeException(null, "Provided arguments are incorrect!");
            }

            int employeeId = int.Parse(inputArgs[0]);

            var address = string.Join(" ", inputArgs.Skip(1));
            var employee = context.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentNullException(null, $"Employee with ID: {employeeId} cannot be found!");
            }

            employee.Address = address;

            context.SaveChanges();

            var employeeDto = this.mapper.CreateMappedObject<EmployeeDto>(employee);

            string result = $"Address of {employeeDto.FirstName} {employeeDto.LastName} is set to {employeeDto.Address}";
            return result;
        }
    }
}
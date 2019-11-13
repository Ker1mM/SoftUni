
namespace MyApp.Core.Commands
{
    using AutoMapper;
    using MyApp.Core.Contracts;
    using MyApp.Core.ViewModels;
    using MyApp.Data;
    using System;
    using System.Globalization;
    using System.Text;

    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public EmployeePersonalInfoCommand(MyAppContext context, Mapper mapper)
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

            int employeeId = int.Parse(inputArgs[0]);

            var employee = context.Employees
                .Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentNullException(null, $"Employee with ID: {employeeId} cannot be found!");
            }

            var employeeDto = this.mapper.CreateMappedObject<EmployeeDto>(employee);

            var sb = new StringBuilder();

            string birthday = "N/A";
            if (employeeDto.Birthday != null)
            {
                birthday = ((DateTime)employeeDto.Birthday).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            }

            string address = "N/A";
            if (employeeDto.Address != null)
            {
                address = employeeDto.Address;
            }

            sb.AppendLine($"ID: {employeeId} - {employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:f2}");
            sb.AppendLine($"Birthday: {birthday}");
            sb.AppendLine($"Address: {address}");

            return sb.ToString().TrimEnd();
        }
    }
}

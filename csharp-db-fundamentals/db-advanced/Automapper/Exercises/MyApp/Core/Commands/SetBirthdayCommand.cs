
namespace MyApp.Core.Commands
{
    using AutoMapper;
    using MyApp.Core.Contracts;
    using MyApp.Core.ViewModels;
    using MyApp.Data;
    using System;
    using System.Globalization;

    public class SetBirthdayCommand : ICommand
    {
        private readonly MyAppContext context;
        private readonly Mapper mapper;

        public SetBirthdayCommand(MyAppContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            if (inputArgs.Length != 2)
            {
                throw new ArgumentOutOfRangeException(null, "Provided arguments are incorrect!");
            }

            int employeeId = int.Parse(inputArgs[0]);
            if (!DateTime.TryParseExact(inputArgs[1], "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthday))
            {
                throw new ArgumentException("Incorrect date format!");
            }

            var minAge = DateTime.Now.AddYears(-18);
            var employee = context.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentNullException(null, $"Employee with ID: {employeeId} cannot be found!");
            }

            if (birthday.Date > minAge.Date)
            {
                throw new ArgumentException("Employee must be at least 18-years-old!");
            }



            employee.Birthday = birthday;

            context.SaveChanges();

            var employeeDto = this.mapper.CreateMappedObject<EmployeeDto>(employee);

            string date = birthday.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            string result = $"Birthday of {employeeDto.FirstName} {employeeDto.LastName} is set to {birthday.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)}";
            return result;
        }
    }
}

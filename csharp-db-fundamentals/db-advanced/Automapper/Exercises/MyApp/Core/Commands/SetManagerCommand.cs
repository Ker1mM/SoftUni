
namespace MyApp.Core.Commands
{
    using MyApp.Core.Contracts;
    using MyApp.Data;
    using System;

    public class SetManagerCommand : ICommand
    {
        private readonly MyAppContext context;

        public SetManagerCommand(MyAppContext context)
        {
            this.context = context;
        }

        public string Execute(string[] inputArgs)
        {
            if (inputArgs.Length != 2)
            {
                throw new ArgumentOutOfRangeException(null, "Provided arguments are incorrect!");
            }

            int employeeId = int.Parse(inputArgs[0]);
            int managerId = int.Parse(inputArgs[1]);

            var employee = this.context.Employees.Find(employeeId);
            var manager = this.context.Employees.Find(managerId);

            if (employee == null)
            {
                throw new ArgumentNullException(null, $"Employee with ID: {employeeId} cannot be found!");
            }

            employee.Manager = manager ?? throw new ArgumentNullException(null, $"Manager with ID: {managerId} cannot be found!");

            this.context.SaveChanges();

            string result = $"{manager.LastName} is now manager of {employee.LastName}.";
            return result;
        }
    }
}

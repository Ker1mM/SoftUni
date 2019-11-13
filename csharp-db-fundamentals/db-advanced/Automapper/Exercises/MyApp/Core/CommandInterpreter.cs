namespace MyApp.Core
{
    using MyApp.Core.Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

    public class CommandInterpreter : ICommandInterpreter
    {
        private const string Suffix = "Command";
        private readonly IServiceProvider provider;

        public CommandInterpreter(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public string Read(string[] inputArgs)
        {
            string commandName = inputArgs[0] + Suffix;
            var args = inputArgs.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(n => n.Name == commandName);

            if (type == null)
            {
                throw new ArgumentNullException(null, "Command not found!");
            }

            var ctor = type.GetConstructors().FirstOrDefault();

            var ctorParams = ctor
                .GetParameters()
                .Select(x => x.ParameterType)
                .ToArray();

            var services = ctorParams
                .Select(this.provider.GetService)
                .ToArray();

            var command = (ICommand)ctor.Invoke(services);

            string result = command.Execute(args);

            return result;
        }
    }
}

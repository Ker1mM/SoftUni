
namespace MyApp.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using MyApp.Core.Contracts;
    using System;
    using System.Linq;

    public class Engine : IEngine
    {
        private readonly IServiceProvider provider;

        public Engine(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public void Run()
        {

            while (true)
            {
                try
                {
                    string[] inputArgs = Console.ReadLine()
                                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                .ToArray();

                    var commandInterpreter = this.provider.GetService<ICommandInterpreter>();

                    string result = commandInterpreter.Read(inputArgs);
                    Console.WriteLine(result);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }

            }
        }
    }
}

namespace TheTankGame.Core
{
    using Contracts;
    using System;
    using System.Collections.Generic;

    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IManager tankManager;

        public CommandInterpreter(IManager tankManager)
        {
            this.tankManager = tankManager;
        }

        public string ProcessInput(IList<string> inputParameters)
        {
            string command = inputParameters[0];
            inputParameters.RemoveAt(0);

            string result = string.Empty;

            if (command == "Part")
            {
                command = "Add" + command;
            }
            else if (command == "Vehicle")
            {
                command = "Add" + command;
            }

            var type = tankManager.GetType();

            var method = type.GetMethod(command, new Type[] { typeof(IList<string>) });

            result = (string)method.Invoke(tankManager, new object[] { inputParameters });

            return result;
        }
    }
}
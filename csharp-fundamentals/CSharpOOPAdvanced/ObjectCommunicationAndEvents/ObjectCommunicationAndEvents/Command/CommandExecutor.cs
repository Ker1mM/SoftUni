using System;
using System.Collections.Generic;
using System.Text;

namespace Command
{
    class CommandExecutor : IExecutor
    {
        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }
    }
}

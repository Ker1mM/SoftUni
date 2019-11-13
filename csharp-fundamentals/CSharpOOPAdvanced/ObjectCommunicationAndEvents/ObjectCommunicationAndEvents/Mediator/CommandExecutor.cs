using Heroes.Contracts;

namespace Heroes
{
    class CommandExecutor : IExecutor
    {
        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }
    }
}

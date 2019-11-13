namespace Command
{
    public interface IExecutor
    {
        void ExecuteCommand(ICommand command);
    }
}

namespace Logger.Architecture.Interfaces
{
    public interface ILogFile
    {
        int Size { get; }

        void Write(string content);
    }
}

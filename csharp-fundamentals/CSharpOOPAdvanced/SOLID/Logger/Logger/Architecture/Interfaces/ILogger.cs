using System.Collections.Generic;

namespace Logger.Architecture.Interfaces
{
    public interface ILogger
    {
        ICollection<IAppender> Appenders { get; }

        void Warn(string date, string message);

        void Info(string date, string message);

        void Error(string date, string message);

        void Critical(string date, string message);

        void Fatal(string date, string message);
    }
}

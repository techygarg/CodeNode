using System;

namespace CodeNode.Logging
{
    /// <summary>
    /// Defines the contract for Logger.
    /// </summary>
    public interface ILogger
    {
        void Debug(object message);
        void Error(string message);
        void Error(Exception exceptionToLog);
        void Error(object message, Exception exception);
        void Info(object message);
        void Warning(object message);
        void Fatal(string message);
        void Fatal(Exception exceptionToLog);
        void Fatal(object message,Exception exceptionToLog);
    }
}
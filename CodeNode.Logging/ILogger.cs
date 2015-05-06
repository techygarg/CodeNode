using System;

namespace CodeNode.Logging
{
    /// <summary>
    /// Defines the contract for Logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// The debug information.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(object message);

        /// <summary>
        /// The error message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(string message);

        /// <summary>
        /// Errors the specified exception.
        /// </summary>
        /// <param name="exceptionToLog">The exception to log.</param>
        void Error(Exception exceptionToLog);

        /// <summary>
        /// The error message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Error(object message, Exception exception);

        /// <summary>
        /// The information method.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(object message);

        /// <summary>
        /// The Warning method.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warning(object message);
    }
}
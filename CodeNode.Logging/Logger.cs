using System;
using System.Reflection;
using log4net;
using log4net.Config;

namespace CodeNode.Logging
{
    /// <summary>
    ///     Represents logger implementation for Log 4 Net.
    /// </summary>
    public sealed class Logger : ILogger
    {
        #region Variables

        private static readonly ILog LoggerObject = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Lazy<Logger> LoggerInstance = new Lazy<Logger>(() => new Logger());
        private const string ExceptionName = "Exception";
        private const string InnerExceptionName = "Inner Exception";
        private const string ExceptionMessageWithoutInnerException = "{0}{1}: {2}Message: {3}{4}StackTrace: {5}.";
        private const string ExceptionMessageWithInnerException = "{0}{1}{2}";

        #endregion

        #region Constructor

        private Logger()
        {
            XmlConfigurator.Configure();
        }

        #endregion

        #region Properties


        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Logger Instance
        {
            get { return LoggerInstance.Value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Gets the complete message and stack trace.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="exceptionString">The exception string.</param>
        /// <returns></returns>
        private static string GeDetailFromExceptipon(Exception ex, string exceptionString)
        {
            var mesgAndStackTrace = string.Format(ExceptionMessageWithoutInnerException, Environment.NewLine,
                exceptionString, Environment.NewLine, ex.Message, Environment.NewLine, ex.StackTrace);

            if (ex.InnerException != null)
            {
                mesgAndStackTrace = string.Format(ExceptionMessageWithInnerException, mesgAndStackTrace,
                    Environment.NewLine,
                    GeDetailFromExceptipon(ex.InnerException, InnerExceptionName));
            }

            return mesgAndStackTrace + Environment.NewLine;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///    Log a message object with the log4net.Core.Level.Debug level.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(object message)
        {
            LoggerObject.Debug(message);
        }

        /// <summary>
        ///  Logs a message object with the log4net.Core.Level.Info level.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(object message)
        {
            LoggerObject.Info(message);
        }

        /// <summary>
        ///  Logs a message object with the log4net.Core.Level.Info Warning.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warning(object message)
        {
            LoggerObject.Warn(message);
        }

        /// <summary>
        ///  Logs a message object with the log4net.Core.Level.Error level.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            LoggerObject.Error(message);
        }

        /// <summary>
        ///    Log a exception with the log4net.Core.Level.Fatal level.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void Error(Exception ex)
        {
            LoggerObject.Error(GeDetailFromExceptipon(ex, ExceptionName));
        }

        /// <summary>
        ///  Log a message object with the log4net.Core.Level.Error level including the
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Error(object message, Exception exception)
        {
            LoggerObject.Error(message, exception);
        }

        /// <summary>
        /// Log a message object with the log4net.Core.Level.Fatal level.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Fatal(string message)
        {
            LoggerObject.Fatal(message);
        }

        /// <summary>
        /// Log a exception with the log4net.Core.Level.Fatal level.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void Fatal(Exception ex)
        {
            LoggerObject.Fatal(GeDetailFromExceptipon(ex, ExceptionName));
        }

        /// <summary>
        /// Log a message object with the log4net.Core.Level.Fatal level including the
        //  stack trace of the System.Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Fatal(object message, Exception exception)
        {
            LoggerObject.Fatal(message, exception);
        }
        #endregion
    }
}
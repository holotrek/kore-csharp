// ***********************************************************************
// <copyright file="Log4NetProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Diagnostics;
using System.Linq;

using Kore.Providers.Messages;

namespace Kore.Providers.Logging
{
    /// <summary>
    /// An implementation of the <see cref="Kore.Providers.Logging.ILoggingProvider" /> that utilizes
    /// the NLog library and uses a static instance of the log manager.
    /// </summary>
    /// <seealso cref="Kore.Providers.Logging.ILoggingProvider" />
    public class Log4NetProvider : ILoggingProvider
    {
        #region Private/Protected Fields

        /// <summary>
        /// The singleton logger instance.
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Public Methods

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="param">The optional parameters.</param>
        public virtual void Log(string message, Severity severity, params object[] param)
        {
            if (severity == Severity.Error || severity == Severity.Fatal)
            {
                log4net.GlobalContext.Properties["StackTrace"] = this.GetCallingMethodStackTrace();
            }

            string formatted = param.Count() > 0 ? string.Format(message, param) : message;
            switch (severity)
            {
                case Severity.Trace:
                case Severity.Debug:
                    Log4NetProvider.Logger.Debug(formatted);
                    break;
                case Severity.Info:
                    Log4NetProvider.Logger.Info(formatted);
                    break;
                case Severity.Warn:
                    Log4NetProvider.Logger.Warn(formatted);
                    break;
                case Severity.Error:
                    Log4NetProvider.Logger.Error(formatted);
                    break;
                case Severity.Fatal:
                    Log4NetProvider.Logger.Fatal(formatted);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="referenceId">The reference identifier.</param>
        /// <param name="param">The optional parameters.</param>
        public virtual void Log(string message, Severity severity, Guid referenceId, params object[] param)
        {
            log4net.GlobalContext.Properties["ReferenceId"] = referenceId.ToString();
            this.Log(message, severity, param);
        }

        /// <summary>
        /// Logs the specified message and infers the severity of the log from the type of message that was
        /// also sent down to the consuming program (example: A message type of Info could log Info, but should
        /// never log an Error/Fatal).
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="param">The optional parameters.</param>
        public virtual void Log(string message, MessageType messageType, params object[] param)
        {
            Severity s = Severity.Trace;
            switch (messageType)
            {
                case MessageType.Info:
                    s = Severity.Info;
                    break;
                case MessageType.Success:
                    s = Severity.Debug;
                    break;
                case MessageType.Warning:
                    s = Severity.Warn;
                    break;
                case MessageType.Error:
                    s = Severity.Error;
                    break;
            }

            this.Log(message, s, param);
        }

        /// <summary>
        /// Logs the specified message and infers the severity of the log from the type of message that was
        /// also sent down to the consuming program (example: A message type of Info could log Info, but should
        /// never log an Error/Fatal).
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="referenceId">The reference identifier.</param>
        /// <param name="param">The optional parameters.</param>
        public virtual void Log(string message, MessageType messageType, Guid referenceId, params object[] param)
        {
            log4net.GlobalContext.Properties["ReferenceId"] = referenceId.ToString();
            this.Log(message, messageType, param);
        }

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public virtual void Log(Exception exception)
        {
            log4net.GlobalContext.Properties["StackTrace"] = this.GetCallingMethodStackTrace();
            Log4NetProvider.Logger.Fatal(exception);
        }

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="referenceId">The reference identifier.</param>
        public virtual void Log(Exception exception, Guid referenceId)
        {
            log4net.GlobalContext.Properties["ReferenceId"] = referenceId.ToString();
            this.Log(exception);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the calling method stack trace.
        /// </summary>
        /// <returns>The calling method stack trace.</returns>
        private string GetCallingMethodStackTrace()
        {
            return new StackTrace(2, true).ToString();
        }

        #endregion
    }
}

// <copyright file="NLogProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>

using System;
using System.Diagnostics;
using System.Linq;
using Kore.Providers.Messages;
using NLog;

namespace Kore.Providers.Logging
{
    /// <summary>
    /// An implementation of the <see cref="Kore.Providers.Logging.ILoggingProvider"/> that utilizes
    /// the NLog library and uses a static instance of the log manager.
    /// </summary>
    /// <seealso cref="Kore.Providers.Logging.ILoggingProvider" />
    public class NLogProvider : ILoggingProvider
    {
        #region Private/Protected Fields

        /// <summary>
        /// The singleton logger instance.
        /// </summary>
        protected static readonly Logger Logger = LogManager.GetLogger(LOGKEY);

        /// <summary>
        /// A key to use for the singleton logger instance.
        /// </summary>
        private const string LOGKEY = "NLog";

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
            GlobalDiagnosticsContext.Set("StackTrace", this.GetCallingMethodStackTrace());
            NLogProvider.Logger.Log(LogLevel.FromOrdinal((int)severity), param.Count() > 0 ? string.Format(message, param) : message);
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
            GlobalDiagnosticsContext.Set("ReferenceId", referenceId.ToString());
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
            GlobalDiagnosticsContext.Set("ReferenceId", referenceId.ToString());
            this.Log(message, messageType, param);
        }

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public virtual void Log(Exception exception)
        {
            GlobalDiagnosticsContext.Set("StackTrace", this.GetCallingMethodStackTrace());
            NLogProvider.Logger.Log(LogLevel.Fatal, exception);
        }

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="referenceId">The reference identifier.</param>
        public virtual void Log(Exception exception, Guid referenceId)
        {
            GlobalDiagnosticsContext.Set("ReferenceId", referenceId.ToString());
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

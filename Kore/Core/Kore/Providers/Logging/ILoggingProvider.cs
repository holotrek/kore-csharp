// ***********************************************************************
// <copyright file="ILoggingProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using Kore.Providers.Messages;

namespace Kore.Providers.Logging
{
    /// <summary>
    /// A contract for logging messages so that a chosen implementation can be injected into the service.
    /// </summary>
    public interface ILoggingProvider
    {
        #region Methods

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="param">The optional parameters.</param>
        void Log(string message, Severity severity, params object[] param);

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="referenceId">The reference identifier.</param>
        /// <param name="param">The optional parameters.</param>
        void Log(string message, Severity severity, Guid referenceId, params object[] param);

        /// <summary>
        /// Logs the specified message and infers the severity of the log from the type of message that was
        /// also sent down to the consuming program (example: A message type of Info could log Info, but should
        /// never log an Error/Fatal).
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="param">The optional parameters.</param>
        void Log(string message, MessageType messageType, params object[] param);

        /// <summary>
        /// Logs the specified message and infers the severity of the log from the type of message that was
        /// also sent down to the consuming program (example: A message type of Info could log Info, but should
        /// never log an Error/Fatal).
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="referenceId">The reference identifier.</param>
        /// <param name="param">The optional parameters.</param>
        void Log(string message, MessageType messageType, Guid referenceId, params object[] param);

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void Log(Exception exception);

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="referenceId">The reference identifier.</param>
        void Log(Exception exception, Guid referenceId);

        #endregion
    }

    /// <summary>
    /// The severity of the logged message. Implementation was borrowed from NLog as it is assumed
    /// that it will be the most likely <see cref="ILoggingProvider" /> candidate to be injected into the service.
    /// </summary>
    public enum Severity
    {
        /// <summary>
        /// Begin method X, end method X etc
        /// </summary>
        Trace,

        /// <summary>
        /// Executed queries, user authenticated, session expired
        /// </summary>
        Debug,

        /// <summary>
        /// Normal behavior like mail sent, user updated profile etc.
        /// </summary>
        Info,

        /// <summary>
        /// Incorrect behavior but the application can continue
        /// </summary>
        Warn,

        /// <summary>
        /// For example application crashes / exceptions.
        /// </summary>
        Error,

        /// <summary>
        /// Highest level: important stuff down
        /// </summary>
        Fatal
    }
}

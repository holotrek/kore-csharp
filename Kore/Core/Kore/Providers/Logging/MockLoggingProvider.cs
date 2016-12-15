// ***********************************************************************
// <copyright file="MockLoggingProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Kore.Extensions;
using Kore.Providers.Messages;

namespace Kore.Providers.Logging
{
    /// <summary>
    /// A logging provider for tests.
    /// </summary>
    /// <seealso cref="Kore.Providers.Logging.ILoggingProvider" />
    public class MockLoggingProvider : ILoggingProvider
    {
        #region Private Fields

        /// <summary>
        /// The logs
        /// </summary>
        private List<MockLog> _logs;

        /// <summary>
        /// The logs
        /// </summary>
        private Dictionary<Guid, MockLog> _logsByReference;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MockLoggingProvider" /> class.
        /// </summary>
        public MockLoggingProvider()
        {
            this._logs = new List<MockLog>();
            this._logsByReference = new Dictionary<Guid, MockLog>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the logs.
        /// </summary>
        /// <value>The logs.</value>
        public List<MockLog> Logs
        {
            get
            {
                return this._logs.Union(this._logsByReference.Select(x => x.Value)).ToList();
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void Log(Exception exception)
        {
            this._logs.Add(new MockLog
            {
                Level = Severity.Fatal,
                Message = exception.GetMostInner().Message
            });
        }

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="referenceId">The reference identifier.</param>
        public void Log(Exception exception, Guid referenceId)
        {
            this._logsByReference.Add(
                referenceId,
                new MockLog
                {
                    Level = Severity.Fatal,
                    Message = exception.GetMostInner().Message
                });
        }

        /// <summary>
        /// Logs the specified message and infers the severity of the log from the type of message that was
        /// also sent down to the consuming program (example: A message type of Info could log Info, but should
        /// never log an Error/Fatal).
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="param">The optional parameters.</param>
        public void Log(string message, MessageType messageType, params object[] param)
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
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="param">The optional parameters.</param>
        public void Log(string message, Severity severity, params object[] param)
        {
            this._logs.Add(new MockLog
            {
                Level = severity,
                Message = string.Format(message, param)
            });
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
        public void Log(string message, MessageType messageType, Guid referenceId, params object[] param)
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

            this.Log(message, s, referenceId, param);
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="referenceId">The reference identifier.</param>
        /// <param name="param">The optional parameters.</param>
        public void Log(string message, Severity severity, Guid referenceId, params object[] param)
        {
            this._logsByReference.Add(
                referenceId,
                new MockLog
                {
                    Level = severity,
                    Message = string.Format(message, param)
                });
        }

        #endregion
    }
}

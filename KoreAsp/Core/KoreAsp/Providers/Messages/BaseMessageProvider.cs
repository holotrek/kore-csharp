// ***********************************************************************
// <copyright file="BaseMessageProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using KoreAsp.Extensions;
using KoreAsp.Providers.Logging;

namespace KoreAsp.Providers.Messages
{
    /// <summary>
    /// Provides a base implementation of the message provider.
    /// </summary>
    /// <seealso cref="KoreAsp.Providers.Messages.IMessageProvider" />
    public class BaseMessageProvider : IMessageProvider
    {
        #region Private Fields
        
        /// <summary>
        /// The collection of messages.
        /// </summary>
        private List<Message> _messages;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMessageProvider" /> class.
        /// </summary>
        public BaseMessageProvider()
        {
            this._messages = new List<Message>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMessageProvider" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public BaseMessageProvider(ILoggingProvider logger)
            : this()
        {
            this.Logger = logger;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the logger.
        /// </summary>
        public virtual ILoggingProvider Logger { get; private set; }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        public virtual IEnumerable<Message> Messages
        {
            get
            {
                return this._messages;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has errors.
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return this.GetMessages(MessageType.Error).Count() > 0;
            }
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets or sets the message to show by default when an exception occurred and the application is not in debug mode.
        /// </summary>
        /// <value>The message to show by default when an exception occurred and the application is not in debug mode.</value>
        protected string ExceptionMessage { get; set; } = "A fatal application error occurred. Please use Reference ID {0} when contacting support.";

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a collection of just the message texts of a specific type.
        /// </summary>
        /// <param name="type">The type of the message.</param>
        /// <returns>The collection of message texts.</returns>
        public virtual IEnumerable<string> GetMessages(MessageType type)
        {
            return this._messages.Where(x => x.Type == type).Select(x => x.Text);
        }

        /// <summary>
        /// Adds the exception message if debugging. If not in debug mode, it simply indicates that there
        /// was an application error and provides the user with a reference ID to use when contacting support
        /// (within Message) that will hopefully be logged along with the real exception.
        /// </summary>
        /// <param name="exception">The exception message.</param>
        public virtual void AddException(Exception exception)
        {
            Exception mostInner = exception;
            while (mostInner.InnerException != null)
            {
                mostInner = mostInner.InnerException;
            }

            Message m = new Message
            {
                Type = MessageType.Error
            };

#if DEBUG
            m.Text = string.Format("Exception occurred. Message: {0}; Stack Trace: {1}.", mostInner.Message, exception.StackTrace);
#else
            m.Text = string.Format(this.ExceptionMessage, m.ReferenceId);
#endif

            if (this.Logger != null)
            {
                this.Logger.Log(exception, m.ReferenceId);
            }

            this._messages.Add(m);
        }

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        public virtual Message AddMessage(MessageType type, string message, params object[] param)
        {
            return this.AddMessage(type, message, null, 0, null, param);
        }

        /// <summary>
        /// Adds the message and identifies the property it relates to in order to assist in marking the property in the UI.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <param name="property">The property.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        public virtual Message AddMessageByProperty(MessageType type, string message, string property, params object[] param)
        {
            return this.AddMessage(type, message, property, 0, null, param);
        }

        /// <summary>
        /// Adds the message and identifies the property it relates to in order to assist in marking the property in the UI.
        /// </summary>
        /// <typeparam name="T">The type of the model that contains the property to select.</typeparam>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        public virtual Message AddMessageByProperty<T>(MessageType type, string message, Expression<Func<T, object>> propertySelector, params object[] param)
        {
            return this.AddMessage(type, message, propertySelector.GetPropertyName(), 0, null, param);
        }

        /// <summary>
        /// Adds the message and identifies the property and record unique ID it relates to in order to assist in marking the property in the UI.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <param name="property">The property.</param>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        public virtual Message AddMessageByPropertyAndUniqueId(MessageType type, string message, string property, string uniqueId, params object[] param)
        {
            return this.AddMessage(type, message, property, 0, uniqueId, param);
        }

        /// <summary>
        /// Adds the message and identifies the property and record unique ID it relates to in order to assist in marking the property in the UI.
        /// </summary>
        /// <typeparam name="T">The type of the model that contains the property to select.</typeparam>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        public virtual Message AddMessageByPropertyAndUniqueId<T>(MessageType type, string message, Expression<Func<T, object>> propertySelector, string uniqueId, params object[] param)
        {
            return this.AddMessage(type, message, propertySelector.GetPropertyName(), 0, uniqueId, param);
        }

        /// <summary>
        /// Removes the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Whether the message was removed.</returns>
        public virtual bool RemoveMessage(Message message)
        {
            if (this._messages.Contains(message))
            {
                this._messages.Remove(message);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Clears all messages.
        /// </summary>
        public void ClearAllMessages()
        {
            this._messages.Clear();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <param name="property">The property.</param>
        /// <param name="recordId">The record identifier.</param>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        private Message AddMessage(MessageType type, string message, string property, int recordId, string uniqueId, params object[] param)
        {
            Message m = new Message
            {
                Id = recordId,
                Property = property,
                Text = param.Count() > 0 ? string.Format(message, param) : message,
                Type = type,
                UniqueId = uniqueId
            };

            this._messages.Add(m);

            if (this.Logger != null)
            {
                this.Logger.Log(message, type, m.ReferenceId, param);
            }

            return m;
        }

        #endregion
    }
}

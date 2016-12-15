// ***********************************************************************
// <copyright file="ResourceMessageProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Resources;
using Kore.Providers.Logging;

namespace Kore.Providers.Messages
{
    /// <summary>
    /// Provides a base implementation of the message provider using the current culture and a resource file where message keys
    /// from the resource are passed to the add message methods rather than the actual message itself.
    /// </summary>
    /// <seealso cref="Kore.Providers.Messages.IMessageProvider" />
    public class ResourceMessageProvider : BaseMessageProvider, IMessageProvider
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceMessageProvider" /> class.
        /// </summary>
        /// <param name="currentCulture">The current culture.</param>
        /// <param name="resourceManager">The resource manager.</param>
        /// <param name="logger">The logger.</param>
        public ResourceMessageProvider(string currentCulture, ResourceManager resourceManager, ILoggingProvider logger)
            : base(logger)
        {
            this.CurrentCulture = new CultureInfo(currentCulture);
            this.ResourceManager = resourceManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceMessageProvider" /> class.
        /// </summary>
        /// <param name="currentCulture">The current culture.</param>
        /// <param name="resourceManager">The resource manager.</param>
        public ResourceMessageProvider(string currentCulture, ResourceManager resourceManager)
            : this(currentCulture, resourceManager, null)
        {
        }
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the current culture.
        /// </summary>
        public CultureInfo CurrentCulture { get; private set; }

        /// <summary>
        /// Gets the resource manager.
        /// </summary>
        public ResourceManager ResourceManager { get; private set; }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Adds the message, where message is a resource key.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The resource key of the message to add.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        public override Message AddMessage(MessageType type, string message, params object[] param)
        {
            return base.AddMessage(type, this.ResourceManager.GetString(message, this.CurrentCulture));
        }

        /// <summary>
        /// Adds the message, where message is a resource key, and identifies the property it relates to in order to assist in marking the property in the UI.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The resource key of the message to add.</param>
        /// <param name="property">The property.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        public override Message AddMessageByProperty(MessageType type, string message, string property, params object[] param)
        {
            return base.AddMessageByProperty(type, this.ResourceManager.GetString(message, this.CurrentCulture), property, param);
        }

        /// <summary>
        /// Adds the message, where message is a resource key, and identifies the property it relates to in order to assist in marking the property in the UI.
        /// </summary>
        /// <typeparam name="T">The type of the model that contains the property to select.</typeparam>
        /// <param name="type">The type.</param>
        /// <param name="message">The resource key of the message to add.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        public override Message AddMessageByProperty<T>(MessageType type, string message, Expression<Func<T, object>> propertySelector, params object[] param)
        {
            return base.AddMessageByProperty<T>(type, this.ResourceManager.GetString(message, this.CurrentCulture), propertySelector, param);
        }

        /// <summary>
        /// Adds the message, where message is a resource key, and identifies the property and record unique ID it relates to in order to assist in marking the property in the UI.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The resource key of the message to add.</param>
        /// <param name="property">The property.</param>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        public override Message AddMessageByPropertyAndUniqueId(MessageType type, string message, string property, string uniqueId, params object[] param)
        {
            return base.AddMessageByPropertyAndUniqueId(type, this.ResourceManager.GetString(message, this.CurrentCulture), property, uniqueId, param);
        }

        /// <summary>
        /// Adds the message, where message is a resource key, and identifies the property and record unique ID it relates to in order to assist in marking the property in the UI.
        /// </summary>
        /// <typeparam name="T">The type of the model that contains the property to select.</typeparam>
        /// <param name="type">The type.</param>
        /// <param name="message">The resource key of the message to add.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <param name="param">The optional parameters.</param>
        /// <returns>The message that was added.</returns>
        public override Message AddMessageByPropertyAndUniqueId<T>(MessageType type, string message, Expression<Func<T, object>> propertySelector, string uniqueId, params object[] param)
        {
            return base.AddMessageByPropertyAndUniqueId<T>(type, this.ResourceManager.GetString(message, this.CurrentCulture), propertySelector, uniqueId, param);
        }

        #endregion
    }
}

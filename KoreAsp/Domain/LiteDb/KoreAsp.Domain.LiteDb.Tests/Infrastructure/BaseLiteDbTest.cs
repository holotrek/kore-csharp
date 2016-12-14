// ***********************************************************************
// <copyright file="BaseLiteDbTest.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using KoreAsp.Domain.Events;
using KoreAsp.Providers.Authentication;
using KoreAsp.Providers.Logging;
using KoreAsp.Providers.Messages;

namespace KoreAsp.Domain.LiteDb.Tests.Infrastructure
{
    /// <summary>
    /// Handles the base functionality for all the EF tests.
    /// </summary>
    public abstract class BaseLiteDbTest
    {
        /// <summary>
        /// Initializes the test.
        /// </summary>
        public virtual void InitializeTest()
        {
            this.AuthenticationProvider = new MockAuthenticationProvider();
            this.Logger = new MockLoggingProvider();
            this.MessageProvider = new BaseMessageProvider(this.Logger);
            this.EventDispatcher = new DomainEventDispatcher();
            this.DatabaseFileName = "TestLite.db";
        }

        /// <summary>
        /// Gets the authentication provider
        /// </summary>
        protected MockAuthenticationProvider AuthenticationProvider { get; private set; }

        /// <summary>
        /// Gets the message provider.
        /// </summary>
        protected BaseMessageProvider MessageProvider { get; private set; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected MockLoggingProvider Logger { get; private set; }

        /// <summary>
        /// Gets the event dispatcher
        /// </summary>
        protected DomainEventDispatcher EventDispatcher { get; private set; }

        /// <summary>
        /// Gets the connection
        /// </summary>
        protected string DatabaseFileName { get; private set; }
    }
}

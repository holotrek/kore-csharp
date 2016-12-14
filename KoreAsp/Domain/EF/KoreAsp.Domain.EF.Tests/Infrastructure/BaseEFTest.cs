// ***********************************************************************
// <copyright file="BaseEFTest.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Data.Common;
using System.IO;
using System.Reflection;
using KoreAsp.Domain.Events;
using KoreAsp.Providers.Authentication;
using KoreAsp.Providers.Logging;
using KoreAsp.Providers.Messages;
using Effort;
using Effort.DataLoaders;

namespace KoreAsp.Domain.EF.Tests.Infrastructure
{
    /// <summary>
    /// Handles the base functionality for all the EF tests.
    /// </summary>
    public abstract class BaseEFTest
    {
        /// <summary>
        /// The CSV folder
        /// </summary>
        private const string CSVFolder = @"..\..\FakeDatabase";

        /// <summary>
        /// Initializes the test.
        /// </summary>
        public virtual void InitializeTest()
        {
            this.AuthenticationProvider = new MockAuthenticationProvider();
            this.Logger = new MockLoggingProvider();
            this.MessageProvider = new BaseMessageProvider(this.Logger);
            this.EventDispatcher = new DomainEventDispatcher();
            var assembly = Assembly.GetExecutingAssembly();
            var basePath = Path.GetDirectoryName(assembly.Location);
            var csvPath = Path.Combine(basePath, CSVFolder);
            this.Connection = DbConnectionFactory.CreateTransient(new CsvDataLoader(csvPath));
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
        protected DbConnection Connection { get; private set; }
    }
}

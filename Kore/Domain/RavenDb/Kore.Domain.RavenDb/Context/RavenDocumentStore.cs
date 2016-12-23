// ***********************************************************************
// <copyright file="RavenDocumentStore.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using Raven.Client;
using Raven.Client.Embedded;

namespace Kore.Domain.RavenDb.Context
{
    /// <summary>
    /// The class used to initialize and contain the static document store
    /// </summary>
    public class RavenDocumentStore
    {
        /// <summary>
        /// The static document store instance
        /// </summary>
        private static EmbeddableDocumentStore instance;

        /// <summary>
        /// Gets the document store instance.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">IDocumentStore has not been initialized.</exception>
        public static IDocumentStore Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new InvalidOperationException("IDocumentStore has not been initialized.");
                }

                return instance;
            }
        }

        /// <summary>
        /// Initializes the document store.
        /// </summary>
        /// <param name="runInMemory">if set to <c>true</c> [run in memory].</param>
        /// <param name="connectionStringName">Name of the connection string.</param>
        public static void Initialize(bool runInMemory, string connectionStringName = null)
        {
            if (runInMemory)
            {
                instance = new EmbeddableDocumentStore
                {
                    RunInMemory = true,
                    EnlistInDistributedTransactions = false
                };
            }
            else
            {
                instance = new EmbeddableDocumentStore
                {
                    ConnectionStringName = connectionStringName
                };
            }

            instance.Configuration.Storage.Voron.AllowOn32Bits = true;
            instance.Conventions.IdentityPartsSeparator = "-";
            instance.Initialize();
        }
    }
}

// ***********************************************************************
// <copyright file="MockUnitOfWork.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Kore.Domain.Context;
using Kore.Domain.Events;
using Kore.Providers.Authentication;
using Kore.Providers.Messages;

namespace Kore.Domain.Tests
{
    /// <summary>
    /// An implementation of Unit of Work that can be used in tests.
    /// </summary>
    /// <seealso cref="Kore.Domain.Context.BaseUnitOfWork" />
    /// <seealso cref="Kore.Domain.Context.IUnitOfWork{Kore.Domain.Tests.MockRepository}" />
    public class MockUnitOfWork : BaseUnitOfWork, IUnitOfWork<MockRepository>
    {
        #region Private Fields

        /// <summary>
        /// Whether the unit of work has been disposed.
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// The repositories
        /// </summary>
        private List<MockRepository> _repositories;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MockUnitOfWork"/> class.
        /// </summary>
        /// <param name="authenticationProvider">The authentication provider.</param>
        /// <param name="messageProvider">The message provider.</param>
        /// <param name="eventDispatcher">The event dispatcher.</param>
        public MockUnitOfWork(IAuthenticationProvider authenticationProvider, IMessageProvider messageProvider, IDomainEventDispatcher eventDispatcher)
            : base(authenticationProvider, messageProvider, eventDispatcher)
        {
            this._repositories = new List<MockRepository>();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="MockUnitOfWork"/> class from being created.
        /// </summary>
        private MockUnitOfWork()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the repositories within this unit of work.
        /// </summary>
        public IEnumerable<MockRepository> Repositories
        {
            get
            {
                return this._repositories;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the repository to this unit of work so that transactions within all the repositories will use the same transaction.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public void AddRepository(MockRepository repository)
        {
            this._repositories.Add(repository);
        }

        /// <summary>
        /// Gets the specific repository from the list of repositories managed by this Unit of Work.
        /// </summary>
        /// <typeparam name="TSpecificRepository">The type of the specific repository.</typeparam>
        /// <returns>The specific typed repository.</returns>
        public MockRepository GetRepository<TSpecificRepository>()
            where TSpecificRepository : MockRepository
        {
            return this._repositories.OfType<TSpecificRepository>().FirstOrDefault();
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public override void BeginTransaction()
        {
        }

        /// <summary>
        /// Commits the changes to the repositories in this unit of work.
        /// </summary>
        public override void Commit()
        {
            foreach (MockRepository repo in this.Repositories)
            {
                repo.WriteChanges();
            }
        }

        /// <summary>
        /// Rolls back the transaction.
        /// </summary>
        public override void Rollback()
        {
            foreach (MockRepository repo in this.Repositories)
            {
                repo.DiscardChanges();
            }
        }

        /// <summary>
        /// Disposes the unit of work and all corresponding repositories.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this.Rollback();
                    foreach (MockRepository r in this.Repositories)
                    {
                        r.Dispose();
                    }
                }
            }

            this._disposed = true;
        }

        #endregion
    }
}

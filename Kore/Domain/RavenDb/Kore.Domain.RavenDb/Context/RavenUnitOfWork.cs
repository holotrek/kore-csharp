// ***********************************************************************
// <copyright file="RavenUnitOfWork.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Transactions;
using Kore.Domain.Context;
using Kore.Domain.Events;
using Kore.Providers.Authentication;
using Kore.Providers.Messages;
using Raven.Client;

namespace Kore.Domain.RavenDb.Context
{
    /// <summary>
    /// An implementation of the Core Unit of Work for Raven DB.
    /// </summary>
    /// <seealso cref="Kore.Domain.Context.BaseUnitOfWork" />
    /// <seealso cref="Kore.Domain.Context.IUnitOfWork" />
    public class RavenUnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        #region Private Fields

        /// <summary>
        /// Whether the unit of work has been disposed.
        /// </summary>
        private bool _disposed = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RavenUnitOfWork" /> class.
        /// </summary>
        /// <param name="authenticationProvider">The authentication provider.</param>
        /// <param name="messageProvider">The message provider.</param>
        /// <param name="eventDispatcher">The event dispatcher.</param>
        /// <param name="documentStore">The document store.</param>
        public RavenUnitOfWork(IAuthenticationProvider authenticationProvider, IMessageProvider messageProvider, IDomainEventDispatcher eventDispatcher, IDocumentStore documentStore)
            : base(authenticationProvider, messageProvider, eventDispatcher)
        {
            this.DocumentStore = documentStore;
            this.BeginTransaction();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="RavenUnitOfWork"/> class from being created.
        /// </summary>
        private RavenUnitOfWork()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the document store.
        /// </summary>
        public virtual IDocumentStore DocumentStore { get; private set; }

        /// <summary>
        /// Gets the document session.
        /// </summary>
        public virtual IDocumentSession DocumentSession { get; private set; }

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        public virtual TransactionScope Transaction { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public override void BeginTransaction()
        {
            if (this.Transaction == null)
            {
                this.DocumentSession = this.DocumentStore.OpenSession();
                this.Transaction = new TransactionScope();
            }
        }

        /// <summary>
        /// Rolls back the transaction.
        /// </summary>
        public override void Rollback()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Dispose();
                this.Transaction = null;
            }

            this.DocumentSession.Dispose();
        }

        /// <summary>
        /// Commits the changes to the repositories in this unit of work.
        /// </summary>
        public override void Commit()
        {
            this.DispatchEvents(this.Repositories);

            try
            {
                if (this.Transaction == null)
                {
                    this.BeginTransaction();
                }

                this.DocumentSession.SaveChanges();

                if (this.Transaction != null)
                {
                    this.Transaction.Complete();
                    this.Transaction.Dispose();
                    this.Transaction = null;
                }
            }
            catch (Exception ex)
            {
                if (this.MessageProvider.Logger != null)
                {
                    this.MessageProvider.Logger.Log(ex);
                }

                this.Rollback();
                throw;
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

        #region Protected Properties

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
                    foreach (RavenRepository r in this.Repositories)
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

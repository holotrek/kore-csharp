// ***********************************************************************
// <copyright file="LiteDbUnitOfWork.cs" company="Holotrek">
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
using LiteDB;

namespace Kore.Domain.LiteDb.Context
{
    /// <summary>
    /// A contract that will allow for multiple ORM repositories to be transactional.
    /// </summary>
    public class LiteDbUnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        #region Private Fields

        /// <summary>
        /// Whether the unit of work has been disposed.
        /// </summary>
        private bool _disposed = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteDbUnitOfWork" /> class.
        /// </summary>
        /// <param name="authenticationProvider">The authentication provider.</param>
        /// <param name="messageProvider">The message provider.</param>
        /// <param name="eventDispatcher">The event dispatcher.</param>
        /// <param name="databaseFileName">Name of the database file.</param>
        public LiteDbUnitOfWork(IAuthenticationProvider authenticationProvider, IMessageProvider messageProvider, IDomainEventDispatcher eventDispatcher, string databaseFileName)
            : base(authenticationProvider, messageProvider, eventDispatcher)
        {
            this.Connection = new LiteDatabase(databaseFileName);
            this.BeginTransaction();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="LiteDbUnitOfWork"/> class from being created.
        /// </summary>
        private LiteDbUnitOfWork()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public virtual LiteDatabase Connection { get; set; }

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        public virtual LiteTransaction Transaction { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public override void BeginTransaction()
        {
            if (this.Transaction == null)
            {
                this.Transaction = this.Connection.BeginTrans();
            }
        }

        /// <summary>
        /// Rolls back the transaction.
        /// </summary>
        public override void Rollback()
        {
            if (this.Transaction != null)
            {
                if (this.Transaction.State == TransactionState.Started)
                {
                    this.Transaction.Rollback();
                }

                this.Transaction.Dispose();
                this.Transaction = null;
            }
        }

        /// <summary>
        /// Commits the changes to the repositories in this unit of work.
        /// </summary>
        public override void Commit()
        {
            this.DispatchEvents(this.Repositories);

            if (this.Transaction != null)
            {
                if (this.Transaction.State == TransactionState.Started)
                {
                    this.Transaction.Commit();
                }

                this.Transaction.Dispose();
                this.Transaction = null;
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
                    foreach (LiteDbRepository r in this.Repositories)
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

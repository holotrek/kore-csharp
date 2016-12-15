// ***********************************************************************
// <copyright file="EFUnitOfWork.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Transactions;
using Kore.Domain.Context;
using Kore.Domain.Events;
using Kore.Providers.Authentication;
using Kore.Providers.Messages;

namespace Kore.Domain.EF.Context
{
    /// <summary>
    /// A contract that will allow for multiple ORM repositories to be transactional.
    /// </summary>
    public class EFUnitOfWork : BaseUnitOfWork, IUnitOfWork<EFRepository>
    {
        #region Private Fields

        /// <summary>
        /// Whether the unit of work has been disposed.
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// The repositories
        /// </summary>
        private List<EFRepository> _repositories;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EFUnitOfWork" /> class.
        /// </summary>
        /// <param name="authenticationProvider">The authentication provider.</param>
        /// <param name="messageProvider">The message provider.</param>
        /// <param name="eventDispatcher">The event dispatcher.</param>
        /// <param name="connection">The connection.</param>
        public EFUnitOfWork(IAuthenticationProvider authenticationProvider, IMessageProvider messageProvider, IDomainEventDispatcher eventDispatcher, DbConnection connection)
            : base(authenticationProvider, messageProvider, eventDispatcher)
        {
            this.Connection = connection;
            this._repositories = new List<EFRepository>();
            this.BeginTransaction();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="EFUnitOfWork"/> class from being created.
        /// </summary>
        private EFUnitOfWork()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the repositories within this unit of work.
        /// </summary>
        public virtual IEnumerable<EFRepository> Repositories
        {
            get
            {
                return this._repositories;
            }
        }

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public virtual DbConnection Connection { get; set; }

        /// <summary>
        /// Gets the transaction.
        /// </summary>
        public virtual TransactionScope Transaction { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the repository to this unit of work so that transactions within all the repositories will use the same transaction.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public virtual void AddRepository(EFRepository repository)
        {
            this._repositories.Add(repository);
        }

        /// <summary>
        /// Gets the specific repository from the list of repositories managed by this Unit of Work.
        /// </summary>
        /// <typeparam name="TSpecificRepository">The type of the specific repository.</typeparam>
        /// <returns>The specific typed repository.</returns>
        public EFRepository GetRepository<TSpecificRepository>()
            where TSpecificRepository : EFRepository
        {
            return this._repositories.OfType<TSpecificRepository>().FirstOrDefault();
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public override void BeginTransaction()
        {
            if (this.Transaction == null)
            {
                if (this.Connection.State == ConnectionState.Closed)
                {
                    this.Connection.Open();
                }

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

            if (this.Connection.State == ConnectionState.Open)
            {
                this.Connection.Close();
            }
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

                foreach (EFRepository r in this.Repositories)
                {
                    if (r is DbContext)
                    {
                        r.SaveChanges();
                    }
                }

                if (this.Transaction != null)
                {
                    this.Transaction.Complete();
                    this.Transaction.Dispose();
                    this.Transaction = null;
                }

                if (this.Connection.State == ConnectionState.Open)
                {
                    this.Connection.Close();
                }
            }
            catch (DbEntityValidationException entEx)
            {
                this.Rollback();
                foreach (var validationErrors in entEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        if (this.MessageProvider.Logger != null)
                        {
                            this.MessageProvider.Logger.Log("{0}; Property: {1}, Error: {2}", Providers.Logging.Severity.Fatal, entEx.Message, validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }

                throw;
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
                    foreach (EFRepository r in this.Repositories)
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

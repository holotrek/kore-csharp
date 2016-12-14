// ***********************************************************************
// <copyright file="IUnitOfWork.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using KoreAsp.Domain.Events;
using KoreAsp.Providers.Authentication;
using KoreAsp.Providers.Messages;

namespace KoreAsp.Domain.Context
{
    /// <summary>
    /// A contract that will allow for multiple ORM repositories to be transactional.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the authentication provider.
        /// </summary>
        IAuthenticationProvider AuthenticationProvider { get; }

        /// <summary>
        /// Gets the message provider.
        /// </summary>
        IMessageProvider MessageProvider { get; }

        /// <summary>
        /// Gets the event dispatcher.
        /// </summary>
        IDomainEventDispatcher EventDispatcher { get; }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Rolls back the transaction.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Commits the changes to the repositories in this unit of work.
        /// </summary>
        void Commit();
    }

    /// <summary>
    /// A contract that will allow for multiple ORM repositories to be transactional.
    /// </summary>
    /// <typeparam name="TRepository">The type of the repository.</typeparam>
    /// <seealso cref="KoreAsp.Domain.Context.IUnitOfWork" />
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWork<TRepository> : IUnitOfWork
        where TRepository : IRepository
    {
        /// <summary>
        /// Gets the repositories within this unit of work.
        /// </summary>
        IEnumerable<TRepository> Repositories { get; }

        /// <summary>
        /// Adds the repository to this unit of work so that transactions within all the repositories will use the same transaction.
        /// </summary>
        /// <param name="repository">The repository.</param>
        void AddRepository(TRepository repository);

        /// <summary>
        /// Gets the specific repository from the list of repositories managed by this Unit of Work.
        /// </summary>
        /// <typeparam name="TSpecificRepository">The type of the specific repository.</typeparam>
        /// <returns>The specific typed repository.</returns>
        TRepository GetRepository<TSpecificRepository>()
            where TSpecificRepository : TRepository;
    }
}

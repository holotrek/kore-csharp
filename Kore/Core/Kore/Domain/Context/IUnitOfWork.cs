// ***********************************************************************
// <copyright file="IUnitOfWork.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Kore.Domain.Events;
using Kore.Providers.Authentication;
using Kore.Providers.Messages;

namespace Kore.Domain.Context
{
    /// <summary>
    /// A contract that will allow for multiple ORM repositories to be transactional.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the repositories within this unit of work.
        /// </summary>
        IEnumerable<IRepository> Repositories { get; }

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
        /// Adds the repository to this unit of work so that transactions within all the repositories will use the same transaction.
        /// </summary>
        /// <param name="repository">The repository.</param>
        void AddRepository(IRepository repository);

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="TRepository">The type of the repository.</typeparam>
        /// <returns>The repository.</returns>
        TRepository GetRepository<TRepository>()
            where TRepository : IRepository;

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
}

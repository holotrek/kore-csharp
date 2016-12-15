// ***********************************************************************
// <copyright file="BaseUnitOfWork.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using Kore.Domain.Events;
using Kore.Providers.Authentication;
using Kore.Providers.Messages;

namespace Kore.Domain.Context
{
    /// <summary>
    /// A contract that will allow for multiple ORM repositories to be transactional.
    /// </summary>
    public abstract class BaseUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUnitOfWork" /> class.
        /// </summary>
        /// <param name="authenticationProvider">The authentication provider.</param>
        /// <param name="messageProvider">The message provider.</param>
        /// <param name="eventDispatcher">The event dispatcher.</param>
        public BaseUnitOfWork(IAuthenticationProvider authenticationProvider, IMessageProvider messageProvider, IDomainEventDispatcher eventDispatcher)
        {
            this.AuthenticationProvider = authenticationProvider;
            this.MessageProvider = messageProvider;
            this.EventDispatcher = eventDispatcher;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUnitOfWork"/> class.
        /// </summary>
        protected BaseUnitOfWork()
        {
        }

        /// <summary>
        /// Gets the authentication provider.
        /// </summary>
        public virtual IAuthenticationProvider AuthenticationProvider { get; private set; }

        /// <summary>
        /// Gets the message provider.
        /// </summary>
        public virtual IMessageProvider MessageProvider { get; }

        /// <summary>
        /// Gets the event dispatcher.
        /// </summary>
        public virtual IDomainEventDispatcher EventDispatcher { get; private set; }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public abstract void BeginTransaction();

        /// <summary>
        /// Rolls back the transaction.
        /// </summary>
        public abstract void Rollback();

        /// <summary>
        /// Commits the changes to the repositories in this unit of work.
        /// </summary>
        public abstract void Commit();

        /// <summary>
        /// Disposes the unit of work and all corresponding repositories.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Dispatches the events.
        /// </summary>
        /// <param name="repositories">The repositories.</param>
        protected void DispatchEvents(IEnumerable<IRepository> repositories)
        {
            if (this.EventDispatcher != null)
            {
                // The following delayed event dispatch pattern borrowed from Jimmy Bogard: https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/
                foreach (IRepository r in repositories)
                {
                    foreach (var entity in r.GetEntitiesWithEvents())
                    {
                        var events = entity.Events.ToArray();
                        entity.Events.Clear();
                        foreach (var domainEvent in events)
                        {
                            this.EventDispatcher.Raise(domainEvent);
                        }
                    }
                }
            }
        }
    }
}

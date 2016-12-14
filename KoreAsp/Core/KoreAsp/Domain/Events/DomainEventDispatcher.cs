// ***********************************************************************
// <copyright file="DomainEventDispatcher.cs" company="Holotrek">
//     Copyright © Holotrek 2016
//     Original Source: http://udidahan.com/2009/06/14/domain-events-salvation/
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using KoreAsp.Providers.Containers;

namespace KoreAsp.Domain.Events
{
    /// <summary>
    /// Collects domain events and then dispatches them, allowing whatever handlers are stored in the container or registered
    /// as a callback action to call their particular Handle method.
    /// </summary>
    /// <seealso cref="KoreAsp.Domain.Events.IDomainEventDispatcher" />
    /// <seealso cref="System.IDisposable" />
    public class DomainEventDispatcher : IDomainEventDispatcher, IDisposable
    {
        /// <summary>
        /// The registered domain event handlers.
        /// </summary>
        private List<Delegate> _actions;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventDispatcher" /> class.
        /// </summary>
        public DomainEventDispatcher()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventDispatcher" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public DomainEventDispatcher(IContainerProvider container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Gets or sets the unity container that can collect registered handlers by registering the handler as a type or instance.
        /// </summary>
        /// <value>
        /// The unity container.
        /// </value>
        public IContainerProvider Container { get; set; }

        /// <summary>
        /// Registers the specified callback.
        /// </summary>
        /// <typeparam name="T">The type of the domain event.</typeparam>
        /// <param name="callback">The callback.</param>
        public void Register<T>(Action<T> callback)
            where T : IDomainEvent
        {
            if (this._actions == null)
            {
                this._actions = new List<Delegate>();
            }

            this._actions.Add(callback);
        }

        /// <summary>
        /// Raises the specified domain event with the provided arguments.
        /// </summary>
        /// <typeparam name="T">The type of the domain event.</typeparam>
        /// <param name="args">The arguments.</param>
        public void Raise<T>(T args)
            where T : IDomainEvent
        {
            if (this.Container != null)
            {
                Type specificHandlerType = typeof(IHandler<>).MakeGenericType(args.GetType());
                foreach (IHandler handler in this.Container.ResolveAll(specificHandlerType))
                {
                    handler.Handle(args);
                }
            }

            if (this._actions != null)
            {
                foreach (var action in this._actions)
                {
                    if (action is Action<T>)
                    {
                        ((Action<T>)action)(args);
                    }
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this._actions = null;
        }
    }
}

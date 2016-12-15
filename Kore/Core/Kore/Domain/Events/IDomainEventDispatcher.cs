// ***********************************************************************
// <copyright file="IDomainEventDispatcher.cs" company="Holotrek">
//     Copyright © Holotrek 2016
//     Original Source: http://udidahan.com/2009/06/14/domain-events-salvation/
// </copyright>
// ***********************************************************************

using System;
using Kore.Providers.Containers;

namespace Kore.Domain.Events
{
    /// <summary>
    /// Contract for a domain event dispatcher that can collect raised events and handle them.
    /// </summary>
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        IContainerProvider Container { get; set; }

        /// <summary>
        /// Registers the specified callback.
        /// </summary>
        /// <typeparam name="T">The type of the domain event.</typeparam>
        /// <param name="callback">The callback.</param>
        void Register<T>(Action<T> callback)
            where T : IDomainEvent;

        /// <summary>
        /// Raises the specified domain event with the provided arguments.
        /// </summary>
        /// <typeparam name="T">The type of the domain event.</typeparam>
        /// <param name="args">The arguments.</param>
        void Raise<T>(T args)
            where T : IDomainEvent;
    }
}

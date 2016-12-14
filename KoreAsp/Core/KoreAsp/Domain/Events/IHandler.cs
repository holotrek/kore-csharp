// ***********************************************************************
// <copyright file="IHandler.cs" company="Holotrek">
//     Copyright © Holotrek 2016
//     Original Source: http://udidahan.com/2009/06/14/domain-events-salvation/
// </copyright>
// ***********************************************************************

namespace KoreAsp.Domain.Events
{
    /// <summary>
    /// Allows the implementing class to handle the domain event identified by the type parameter.
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Handles the domain event using the provided arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        void Handle(object args);
    }

    /// <summary>
    /// Allows the implementing class to handle the domain event identified by the type parameter.
    /// </summary>
    /// <typeparam name="T">The type of the domain event.</typeparam>
    public interface IHandler<T> : IHandler
        where T : IDomainEvent
    {
        /// <summary>
        /// Handles the domain event using the provided arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        void Handle(T args);
    }
}
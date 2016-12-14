// ***********************************************************************
// <copyright file="IService.cs" company="Holotrek">
//     Copyright (c) Holotrek. All rights reserved.
// </copyright>
// ***********************************************************************

using KoreAsp.Domain.Context;
using KoreAsp.Providers.Authentication;
using KoreAsp.Providers.Caching;
using KoreAsp.Providers.Logging;
using KoreAsp.Providers.Messages;
using KoreAsp.Providers.Serialization;

namespace KoreAsp.Service.Infrastructure
{
    /// <summary>
    /// A contract for a service-layer class that performs discrete Unit of Work actions and holds containers for cross-cutting concerns.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Gets the authentication provider.
        /// </summary>
        IAuthenticationProvider AuthenticationProvider { get; }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        IMessageProvider MessageProvider { get; }

        /// <summary>
        /// Gets the caching provider.
        /// </summary>
        ICachingProvider CachingProvider { get; }

        /// <summary>
        /// Gets the serialization provider.
        /// </summary>
        ISerializationProvider SerializationProvider { get; }

        /// <summary>
        /// Gets the logging provider.
        /// </summary>
        ILoggingProvider LoggingProvider { get; }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        IUser CurrentUser { get; }
    }
}

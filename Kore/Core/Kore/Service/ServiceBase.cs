// ***********************************************************************
// <copyright file="ServiceBase.cs" company="Holotrek">
//     Copyright (c) Holotrek. All rights reserved.
// </copyright>
// ***********************************************************************

using Kore.Domain.Context;
using Kore.Providers.Authentication;
using Kore.Providers.Caching;
using Kore.Providers.Logging;
using Kore.Providers.Messages;
using Kore.Providers.Serialization;

namespace Kore.Service.Infrastructure
{
    /// <summary>
    /// A base implementation for a service-layer class that performs discrete Unit of Work actions and holds containers for cross-cutting concerns.
    /// </summary>
    /// <seealso cref="Kore.Service.Infrastructure.IService" />
    public abstract class ServiceBase : IService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="authenticationProvider">The authentication provider.</param>
        /// <param name="messageProvider">The message provider.</param>
        /// <param name="cachingProvider">The caching provider.</param>
        /// <param name="serializationProvider">The serialization provider.</param>
        public ServiceBase(
            IUnitOfWork unitOfWork,
            IAuthenticationProvider authenticationProvider,
            IMessageProvider messageProvider, 
            ICachingProvider cachingProvider, 
            ISerializationProvider serializationProvider)
        {
            this.UnitOfWork = unitOfWork;
            this.AuthenticationProvider = authenticationProvider;
            this.MessageProvider = messageProvider;
            this.CachingProvider = cachingProvider;
            this.SerializationProvider = serializationProvider;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ServiceBase"/> class from being created.
        /// </summary>
        private ServiceBase()
        {
        }

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        public virtual IUnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        /// Gets the authentication provider.
        /// </summary>
        public virtual IAuthenticationProvider AuthenticationProvider { get; private set; }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        public virtual IMessageProvider MessageProvider { get; private set; }

        /// <summary>
        /// Gets the caching provider.
        /// </summary>
        public virtual ICachingProvider CachingProvider { get; private set; }

        /// <summary>
        /// Gets the serialization provider.
        /// </summary>
        public virtual ISerializationProvider SerializationProvider { get; private set; }

        /// <summary>
        /// Gets the logging provider.
        /// </summary>
        public virtual ILoggingProvider LoggingProvider
        {
            get
            {
                return this.MessageProvider.Logger;
            }
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        public virtual IUser CurrentUser
        {
            get
            {
                return this.AuthenticationProvider.CurrentUser;
            }
        }
    }
}

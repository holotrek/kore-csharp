// ***********************************************************************
// <copyright file="ServiceValidatorBase.cs" company="Holotrek">
//     Copyright (c) Holotrek. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KoreAsp.Domain.Context;
using KoreAsp.Providers.Authentication;
using KoreAsp.Providers.Caching;
using KoreAsp.Providers.Messages;
using KoreAsp.Providers.Serialization;

namespace KoreAsp.Service.Infrastructure
{
    /// <summary>
    /// A base implementation of the validation within a service layer.
    /// </summary>
    /// <seealso cref="psice.Service.Infrastructure.ServiceBase" />
    /// <seealso cref="psice.Service.Infrastructure.IService" />
    /// <seealso cref="psice.Service.Infrastructure.IServiceValidator" />
    public abstract class ServiceValidatorBase : ServiceBase, IService, IServiceValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceValidatorBase" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="authenticationProvider">The authentication provider.</param>
        /// <param name="messageProvider">The message provider.</param>
        /// <param name="cachingProvider">The caching provider.</param>
        /// <param name="serializationProvider">The serialization provider.</param>
        public ServiceValidatorBase(
            IUnitOfWork unitOfWork,
            IAuthenticationProvider authenticationProvider,
            IMessageProvider messageProvider,
            ICachingProvider cachingProvider,
            ISerializationProvider serializationProvider)
            : base(unitOfWork, authenticationProvider, messageProvider, cachingProvider, serializationProvider)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the service is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public virtual bool IsValid
        {
            get
            {
                return !this.MessageProvider.HasErrors;
            }
        }

        /// <summary>
        /// Validates the object by checking attribute annotations.
        /// </summary>
        /// <typeparam name="T">The type of the model being validated.</typeparam>
        /// <param name="item">The item.</param>
        /// <param name="uniqueId">The unique identifier.</param>
        /// <returns>Whether the item is valid.</returns>
        public virtual bool ValidateAnnotations<T>(T item, string uniqueId = null)
        {
            ValidationContext context = new ValidationContext(item, serviceProvider: null, items: null);
            List<ValidationResult> results = new List<ValidationResult>();

            // Use metadata if provided
            MetadataTypeAttribute mta = typeof(T).GetCustomAttributes(typeof(MetadataTypeAttribute), false).FirstOrDefault() as MetadataTypeAttribute;
            if (mta != null)
            {
                Type metadataType = mta.MetadataClassType;
                TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(T), metadataType), typeof(T));
            }

            if (!Validator.TryValidateObject(item, context, results, validateAllProperties: true))
            {
                foreach (ValidationResult vr in results)
                {
                    string memberName = vr.MemberNames.Count() > 0 ? vr.MemberNames.First() : null;
                    if (memberName == null)
                    {
                        this.MessageProvider.AddMessage(MessageType.Error, vr.ErrorMessage);
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(uniqueId))
                        {
                            this.MessageProvider.AddMessageByProperty(MessageType.Error, vr.ErrorMessage, memberName);
                        }
                        else
                        {
                            this.MessageProvider.AddMessageByPropertyAndUniqueId(MessageType.Error, vr.ErrorMessage, memberName, uniqueId);
                        }
                    }
                }

                return false;
            }

            return true;
        }
    }
}

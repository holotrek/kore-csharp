// ***********************************************************************
// <copyright file="IServiceValidator.cs" company="Holotrek">
//     Copyright (c) Holotrek. All rights reserved.
// </copyright>
// ***********************************************************************

namespace Kore.Service.Infrastructure
{
    /// <summary>
    /// A contract for the validation within a service layer.
    /// </summary>
    /// <seealso cref="psice.Service.Infrastructure.IService" />
    public interface IServiceValidator : IService
    {
        /// <summary>
        /// Gets a value indicating whether the service is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        bool IsValid { get; }
    }
}

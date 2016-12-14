// ***********************************************************************
// <copyright file="IUser.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using KoreAsp.Domain.Context;

namespace KoreAsp.Providers.Authentication
{
    /// <summary>
    /// Represents a user entity with a UserName
    /// </summary>
    /// <seealso cref="KoreAsp.Domain.Context.IEntity" />
    public interface IUser : IEntity
    {
        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        string UserName { get; }
    }
}

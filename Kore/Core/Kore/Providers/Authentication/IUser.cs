// ***********************************************************************
// <copyright file="IUser.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using Kore.Domain.Context;

namespace Kore.Providers.Authentication
{
    /// <summary>
    /// Represents a user entity with a UserName
    /// </summary>
    /// <seealso cref="Kore.Domain.Context.IEntity" />
    public interface IUser : IEntity
    {
        /// <summary>
        /// Gets the username of the user.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets the first name of the user.
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// Gets the last name of the user.
        /// </summary>
        string LastName { get; }

        /// <summary>
        /// Gets the middle name of the user.
        /// </summary>
        string MiddleName { get; }
    }
}

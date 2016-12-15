// ***********************************************************************
// <copyright file="MockUser.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using Kore.Domain.Context;

namespace Kore.Providers.Authentication
{
    /// <summary>
    /// Class MockUser.
    /// </summary>
    /// <seealso cref="Kore.Domain.Context.BaseEntity" />
    /// <seealso cref="Kore.Domain.Context.IEntity" />
    /// <seealso cref="Kore.Providers.Authentication.IUser" />
    public class MockUser : BaseEntity, IUser, IEntity
    {
        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public override string UniqueId
        {
            get
            {
                return this.UserId;
            }

            protected set
            {
                this.UserId = value;
            }
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
    }
}

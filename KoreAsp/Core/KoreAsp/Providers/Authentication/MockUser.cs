// ***********************************************************************
// <copyright file="MockUser.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using KoreAsp.Domain.Context;

namespace KoreAsp.Providers.Authentication
{
    /// <summary>
    /// Class MockUser.
    /// </summary>
    /// <seealso cref="KoreAsp.Domain.Context.BaseEntity" />
    /// <seealso cref="KoreAsp.Domain.Context.IEntity" />
    /// <seealso cref="KoreAsp.Providers.Authentication.IUser" />
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

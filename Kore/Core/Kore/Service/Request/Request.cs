// ***********************************************************************
// <copyright file="Request.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

namespace Kore.Service.Request
{
    /// <summary>
    /// Encapsulates the contents of all service requests
    /// </summary>
    /// <seealso cref="Kore.Service.Request.IRequest" />
    public abstract class Request : IRequest
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
    }
}

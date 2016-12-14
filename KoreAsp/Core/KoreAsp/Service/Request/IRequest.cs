// ***********************************************************************
// <copyright file="IRequest.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

namespace KoreAsp.Service.Request
{
    /// <summary>
    /// Contract for all service requests
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        string Token { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        string UserName { get; set; }
    }
}

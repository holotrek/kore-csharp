// ***********************************************************************
// <copyright file="IResponse.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Net;
using Kore.Providers.Messages;

namespace Kore.Response
{
    /// <summary>
    /// Contract for the response of all service requests.
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>The status code.</value>
        int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>The messages.</value>
        IMessageProvider Messages { get; set; }
    }
}

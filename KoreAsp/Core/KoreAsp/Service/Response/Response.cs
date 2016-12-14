// ***********************************************************************
// <copyright file="Response.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Net;
using KoreAsp.Providers.Messages;

namespace KoreAsp.Response
{
    /// <summary>
    /// Encapsulates the response of all service requests.
    /// </summary>
    /// <seealso cref="KoreAsp.Response.IResponse" />
    public abstract class Response : IResponse
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>The status code.</value>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>The messages.</value>
        public IMessageProvider Messages { get; set; }
    }
}

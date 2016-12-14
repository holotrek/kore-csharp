// ***********************************************************************
// <copyright file="StatusResponse.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Net;

namespace KoreAsp.Response
{
    /// <summary>
    /// Encapsulates the response of a service get request that returns a status with no model.
    /// </summary>
    /// <seealso cref="KoreAsp.Response.Response" />
    /// <seealso cref="KoreAsp.Response.IResponse" />
    public class StatusResponse : Response, IResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusResponse" /> class.
        /// </summary>
        public StatusResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusResponse" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        public StatusResponse(HttpStatusCode status)
        {
            this.StatusCode = (int)status;
        }
    }
}

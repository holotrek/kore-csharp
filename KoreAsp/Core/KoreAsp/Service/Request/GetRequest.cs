// ***********************************************************************
// <copyright file="GetRequest.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

namespace KoreAsp.Service.Request
{
    /// <summary>
    /// Encapsulates the contents of a service get request that is expected to return a single result
    /// </summary>
    /// <seealso cref="KoreAsp.Service.Request.Request" />
    /// <seealso cref="KoreAsp.Service.Request.IRequest" />
    public class GetRequest : Request, IRequest
    {
    }

    /// <summary>
    /// Encapsulates the contents of a service get request that is expected to return a single result
    /// </summary>
    /// <typeparam name="T">The model type</typeparam>
    /// <seealso cref="KoreAsp.Service.Request.Request" />
    /// <seealso cref="KoreAsp.Service.Request.IRequest" />
    public class GetRequest<T> : Request, IRequest
    {
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public T Model { get; set; }
    }
}

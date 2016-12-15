// ***********************************************************************
// <copyright file="UpdateRequest.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

namespace Kore.Service.Request
{
    /// <summary>
    /// Encapsulates the contents of a service update request which updates a record
    /// </summary>
    /// <seealso cref="Kore.Service.Request.Request" />
    /// <seealso cref="Kore.Service.Request.IRequest" />
    public class UpdateRequest : Request, IRequest
    {
        /// <summary>
        /// Gets or sets the record identifier.
        /// </summary>
        /// <value>The record identifier.</value>
        public int? RecordId { get; set; }
    }

    /// <summary>
    /// Encapsulates the contents of a service update request which updates a record
    /// </summary>
    /// <typeparam name="T">The model type</typeparam>
    /// <seealso cref="Kore.Service.Request.Request" />
    /// <seealso cref="Kore.Service.Request.IRequest" />
    public class UpdateRequest<T> : Request, IRequest
    {
        /// <summary>
        /// Gets or sets the record identifier.
        /// </summary>
        /// <value>The record identifier.</value>
        public int? RecordId { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public T Model { get; set; }
    }
}

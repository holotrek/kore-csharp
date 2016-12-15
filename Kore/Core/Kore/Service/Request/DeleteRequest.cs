// ***********************************************************************
// <copyright file="DeleteRequest.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

namespace Kore.Service.Request
{
    /// <summary>
    /// Encapsulates the contents of a service delete request
    /// </summary>
    /// <seealso cref="Kore.Service.Request.Request" />
    /// <seealso cref="Kore.Service.Request.IRequest" />
    public class DeleteRequest : Request, IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRequest" /> class.
        /// </summary>
        public DeleteRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRequest" /> class.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        public DeleteRequest(int recordId)
        {
            this.RecordId = recordId;
        }

        /// <summary>
        /// Gets or sets the record identifier.
        /// </summary>
        /// <value>The record identifier.</value>
        public int? RecordId { get; set; }
    }
}

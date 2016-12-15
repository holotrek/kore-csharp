// ***********************************************************************
// <copyright file="CollectionResponse.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Collections.Generic;

namespace Kore.Response
{
    /// <summary>
    /// Encapsulates the response of a service request, which returns a collection of models.
    /// </summary>
    /// <typeparam name="T">The model type</typeparam>
    /// <seealso cref="Kore.Response.Response" />
    /// <seealso cref="Kore.Response.IResponse" />
    public class CollectionResponse<T> : Response, IResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionResponse{T}" /> class.
        /// </summary>
        public CollectionResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionResponse{T}" /> class.
        /// </summary>
        /// <param name="models">The models.</param>
        public CollectionResponse(IEnumerable<T> models)
            : this()
        {
            this.Models = models;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionResponse{T}" /> class.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <param name="totalRecords">The total records.</param>
        public CollectionResponse(IEnumerable<T> models, int totalRecords)
            : this(models)
        {
            this.TotalRecords = totalRecords;
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public IEnumerable<T> Models { get; set; }

        /// <summary>
        /// Gets or sets the total records.
        /// </summary>
        /// <value>The total records.</value>
        public int TotalRecords { get; set; }
    }
}

// ***********************************************************************
// <copyright file="GetCollectionRequest.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Collections.Generic;

namespace KoreAsp.Service.Request
{
    /// <summary>
    /// Encapsulates the contents of a service get request that is expected to return multiple results
    /// </summary>
    /// <seealso cref="KoreAsp.Service.Request.Request" />
    /// <seealso cref="KoreAsp.Service.Request.IRequest" />
    public class GetCollectionRequest : Request, IRequest
    {
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets the custom filter.
        /// </summary>
        /// <value>
        /// The custom filter.
        /// </value>
        public string CustomFilter { get; set; }

        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>The sort.</value>
        public IEnumerable<string> Sort { get; set; }

        /// <summary>
        /// Gets or sets the take value (how many per page).
        /// </summary>
        /// <value>The take value.</value>
        public int Take { get; set; }

        /// <summary>
        /// Gets or sets the skip value (which page).
        /// </summary>
        /// <value>The skip value.</value>
        public int Skip { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
        public int Page { get; set; }
    }
}

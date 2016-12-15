// ***********************************************************************
// <copyright file="OrderCategory.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using Kore.Domain.Context;

namespace Kore.Domain.LiteDb.Tests.Domain.Orders.Models
{
    /// <summary>
    /// A test Category model for the Core.EF Testing Suite
    /// </summary>
    /// <seealso cref="Kore.Domain.Context.BaseEntity" />
    /// <seealso cref="Kore.Domain.Context.IEntity" />
    public class OrderCategory : BaseEntity, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderCategory"/> class.
        /// </summary>
        public OrderCategory()
        {
        }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>The category identifier.</value>
        public string CategoryId
        {
            get
            {
                return this.UniqueId;
            }

            set
            {
                this.UniqueId = value;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
    }
}

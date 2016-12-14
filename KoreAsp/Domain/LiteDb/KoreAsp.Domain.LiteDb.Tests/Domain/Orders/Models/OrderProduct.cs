// ***********************************************************************
// <copyright file="OrderProduct.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using KoreAsp.Domain.Context;

namespace KoreAsp.Domain.LiteDb.Tests.Domain.Orders.Models
{
    /// <summary>
    /// A test Product model for the Core.EF Testing Suite
    /// </summary>
    /// <seealso cref="KoreAsp.Domain.Context.BaseEntity" />
    /// <seealso cref="KoreAsp.Domain.Context.IEntity" />
    public class OrderProduct : BaseEntity, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderProduct"/> class.
        /// </summary>
        public OrderProduct()
        {
        }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>The product identifier.</value>
        public string ProductId
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
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>The category identifier.</value>
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public virtual OrderCategory Category { get; set; }

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

// ***********************************************************************
// <copyright file="Product.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.ComponentModel.DataAnnotations;
using Kore.Domain.Context;

namespace Kore.Domain.EF.Tests.Data.Models
{
    /// <summary>
    /// A test Product model for the Core.EF Testing Suite
    /// </summary>
    /// <seealso cref="Kore.Domain.Context.BaseEntity" />
    /// <seealso cref="Kore.Domain.Context.IEntity" />
    public class Product : BaseEntity, IEntity
    {
        /// <summary>
        /// Gets the product identifier.
        /// </summary>
        public string ProductId
        {
            get
            {
                return this.UniqueId;
            }

            private set
            {
                this.UniqueId = value;
            }
        }

        /// <summary>
        /// Gets the category identifier.
        /// </summary>
        [Required]
        public string CategoryId { get; private set; }

        /// <summary>
        /// Gets the category.
        /// </summary>
        [Required]
        public virtual Category Category { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [Required]
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the display order.
        /// </summary>
        public int DisplayOrder { get; private set; }
    }
}

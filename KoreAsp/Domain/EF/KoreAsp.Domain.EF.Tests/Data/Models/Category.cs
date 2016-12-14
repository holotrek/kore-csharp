// ***********************************************************************
// <copyright file="Category.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.ComponentModel.DataAnnotations;
using KoreAsp.Domain.Context;

namespace KoreAsp.Domain.EF.Tests.Data.Models
{
    /// <summary>
    /// A test Category model for the Core.EF Testing Suite
    /// </summary>
    /// <seealso cref="KoreAsp.Domain.Context.BaseEntity" />
    /// <seealso cref="KoreAsp.Domain.Context.IEntity" />
    public class Category : BaseEntity, IEntity
    {
        /// <summary>
        /// Gets the category identifier.
        /// </summary>
        public string CategoryId
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

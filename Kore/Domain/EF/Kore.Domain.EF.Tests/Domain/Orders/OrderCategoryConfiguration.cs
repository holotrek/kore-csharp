// ***********************************************************************
// <copyright file="OrderCategoryConfiguration.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Data.Entity.ModelConfiguration;
using Kore.Domain.EF.Tests.Domain.Orders.Models;

namespace Kore.Domain.EF.Tests.Domain.Orders
{
    /// <summary>
    /// The EF configuration class for the Category entity
    /// </summary>
    public class OrderCategoryConfiguration : EntityTypeConfiguration<OrderCategory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderCategoryConfiguration" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public OrderCategoryConfiguration(OrderRepository repository)
        {
            repository.ConfigureEntity(this);
            this.ToTable("Categories").HasKey(x => x.CategoryId);
        }
    }
}

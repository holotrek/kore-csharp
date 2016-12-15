// ***********************************************************************
// <copyright file="OrderProductConfiguration.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Data.Entity.ModelConfiguration;
using Kore.Domain.EF.Tests.Domain.Orders.Models;

namespace Kore.Domain.EF.Tests.Domain.Orders
{
    /// <summary>
    /// The EF configuration class for the Product entity
    /// </summary>
    public class OrderProductConfiguration : EntityTypeConfiguration<OrderProduct>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderProductConfiguration" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public OrderProductConfiguration(OrderRepository repository)
        {
            repository.ConfigureEntity(this);
            this.ToTable("Products").HasKey(x => x.ProductId);
        }
    }
}

// ***********************************************************************
// <copyright file="ProductConfiguration.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Data.Entity.ModelConfiguration;
using Kore.Domain.EF.Tests.Domain.Products.Models;

namespace Kore.Domain.EF.Tests.Domain.Products
{
    /// <summary>
    /// The EF configuration class for the Product entity
    /// </summary>
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductConfiguration" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ProductConfiguration(ProductRepository repository)
        {
            repository.ConfigureEntity(this);
            this.Ignore(x => x.TestSomePropertyNotInDatabase);
        }
    }
}

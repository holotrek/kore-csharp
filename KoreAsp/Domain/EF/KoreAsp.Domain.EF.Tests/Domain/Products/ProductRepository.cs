// ***********************************************************************
// <copyright file="ProductRepository.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Data.Entity;
using KoreAsp.Domain.Context;
using KoreAsp.Domain.EF.Context;
using KoreAsp.Domain.EF.Tests.Domain.Products.Models;

namespace KoreAsp.Domain.EF.Tests.Domain.Products
{
    /// <summary>
    /// The repository for the Product Domain in the Core.EF Testing Suite.
    /// </summary>
    /// <seealso cref="KoreAsp.Domain.EF.Context.EFRepository{KoreAsp.Domain.EF.Tests.Domain.Products.ProductRepository}" />
    /// <seealso cref="KoreAsp.Domain.Context.IRepository" />
    public class ProductRepository : EFRepository<ProductRepository>, IRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="configuration">The configuration.</param>
        public ProductRepository(EFUnitOfWork unitOfWork, ContextConfiguration<ProductRepository> configuration)
            : base(unitOfWork, configuration)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public ProductRepository(EFUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Configures the repository using the specified model builder.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public override void Configure(DbModelBuilder modelBuilder)
        {
            // Example of how to configure entity that has no custom configuration beyond the defaults set up by the repository's Custom Configuration
            this.ConfigureEntity<Category>(modelBuilder);

            // Example of how to configure entity with extra custom configuration using a separate class (more proper Separation of Concerns)
            modelBuilder.Configurations.Add(new ProductConfiguration(this));
        }
    }
}

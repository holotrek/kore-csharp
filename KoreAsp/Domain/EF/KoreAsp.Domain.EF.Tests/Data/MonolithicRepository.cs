// ***********************************************************************
// <copyright file="MonolithicRepository.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Data.Entity;
using KoreAsp.Domain.Context;
using KoreAsp.Domain.EF.Context;
using KoreAsp.Domain.EF.Tests.Data.Models;

namespace KoreAsp.Domain.EF.Tests.Data
{
    /// <summary>
    /// The repository used for the database initialization strategy since individual domains will only have parts of the entire database.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class MonolithicRepository : EFRepository<MonolithicRepository>, IRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonolithicRepository" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public MonolithicRepository(EFUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<MonolithicRepository>());
        }

        /// <summary>
        /// Configures the repository using the specified model builder.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public override void Configure(DbModelBuilder modelBuilder)
        {
            this.ConfigureEntity<Category>(modelBuilder);
            this.ConfigureEntity<Order>(modelBuilder);
            this.ConfigureEntity<Product>(modelBuilder);
        }
    }
}

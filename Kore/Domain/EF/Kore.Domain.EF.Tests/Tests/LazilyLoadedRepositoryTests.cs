// ***********************************************************************
// <copyright file="LazilyLoadedRepositoryTests.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Linq;
using Kore.Domain.EF.Context;
using Kore.Domain.EF.Tests.Data;
using Kore.Domain.EF.Tests.Domain.Products;
using Kore.Domain.EF.Tests.Domain.Products.Models;
using Kore.Domain.EF.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Domain.EF.Tests.Tests
{
    /// <summary>
    /// Tests the EF Unit of Work and Repository with all the default configuration settings, except with Lazy Loading enabled.
    /// </summary>
    [TestClass]
    public class LazilyLoadedRepositoryTests : BaseEFTest
    {
        /// <summary>
        /// The context configuration
        /// </summary>
        private ContextConfiguration<ProductRepository> _configuration;

        /// <summary>
        /// Initializes the test.
        /// </summary>
        [TestInitialize]
        public override void InitializeTest()
        {
            base.InitializeTest();
            this._configuration = new ContextConfiguration<ProductRepository>
            {
                // https://msdn.microsoft.com/en-us/data/jj574232(v=vs.113).aspx
                LazyLoadingEnabled = true
            };
        }

        /// <summary>
        /// Tests that a product can be queried with a lazy loaded navigation property
        /// </summary>
        [TestMethod]
        public void EF_ProductsAssociatedCategoryIsLoadedOnAccess()
        {
            using (var uow = new EFUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.Connection))
            {
                // For testing purposes, the in-memory Effort database has to be initialized at the beginning of each test
                (new MonolithicRepository(uow)).Database.Initialize(true);

                var repo = new ProductRepository(uow, this._configuration);

                Product p = repo.Get<Product>().Where(x => x.Name == "Dove").FirstOrDefault();

                Assert.IsNotNull(p.Category);
                Assert.AreEqual("Soap", p.Category.Name);
            }
        }

        /// <summary>
        /// Tests that without the Auto-Detect Changes option, changing the category ID will make the associated category out of sync
        /// </summary>
        [TestMethod]
        public void EF_ChangingCategoryIdDoesNotAutomaticallyChangeCategoryEntity()
        {
            using (var uow = new EFUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.Connection))
            {
                // For testing purposes, the in-memory Effort database has to be initialized at the beginning of each test
                (new MonolithicRepository(uow)).Database.Initialize(true);

                var repo = new ProductRepository(uow, this._configuration);

                Product p = repo.Get<Product>().Where(x => x.Name == "Dove").FirstOrDefault();
                Category catNew = repo.Get<Category>().Where(x => x.Name == "Towels").FirstOrDefault();

                Assert.AreEqual("Soap", p.Category.Name);

                p.CategoryId = catNew.CategoryId;
                repo.Update(p);

                // Without auto-detect changes, category is still Soap
                Assert.AreEqual("Soap", p.Category.Name);

                // Now call detect changes manually to have it update the associated Category
                repo.ChangeTracker.DetectChanges();

                // Detect changes updated the category entity to the new Towels category
                Assert.AreEqual("Towels", p.Category.Name);
            }
        }
    }
}

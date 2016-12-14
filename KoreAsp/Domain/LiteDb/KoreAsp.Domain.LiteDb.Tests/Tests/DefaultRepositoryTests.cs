// ***********************************************************************
// <copyright file="DefaultRepositoryTests.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using KoreAsp.Domain.LiteDb.Context;
using KoreAsp.Domain.LiteDb.Tests.Domain.Products;
using KoreAsp.Domain.LiteDb.Tests.Domain.Products.Models;
using KoreAsp.Domain.LiteDb.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoreAsp.Domain.LiteDb.Tests.Tests
{
    /// <summary>
    /// Tests the EF Unit of Work and Repository with all the default configuration settings.
    /// </summary>
    [TestClass]
    public class DefaultRepositoryTests : BaseLiteDbTest
    {
        /// <summary>
        /// Initializes the test.
        /// </summary>
        [TestInitialize]
        public override void InitializeTest()
        {
            base.InitializeTest();
        }

        /// <summary>
        /// Tests that a repository can be obtained from the Unit of Work.
        /// </summary>
        [TestMethod]
        public void LiteDb_CanGetRepositoryBackFromUnitOfWork()
        {
            using (var uow = new LiteDbUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.DatabaseFileName))
            {
                var repo = new ProductRepository(uow);
                var repoAgain = uow.GetRepository<ProductRepository>();
                Assert.IsNotNull(repoAgain);
                Assert.AreEqual(typeof(ProductRepository), repoAgain.GetType());
            }
        }

        /// <summary>
        /// Tests that a category can be inserted into the fake context and then queried
        /// </summary>
        [TestMethod]
        public void LiteDb_CategoryCanBeInsertedAndQueried()
        {
            using (var uow = new LiteDbUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.DatabaseFileName))
            {
                var repo = new ProductRepository(uow);

                string userId = uow.AuthenticationProvider.CurrentUser.UniqueId;

                // 2 existing records
                Assert.AreEqual(2, repo.Get<Category>().Count(), "Initial Count");

                // Adds the new category simply by instantiating it
                new Category(repo, "Dishes", "Plates, Bowels, etc");

                // Now there should be 3 records, 2 existing, plus one new one
                ////NOTE: LiteDb is a little different from EF and will commit each change as you perform it, therefore there's a new record already
                Assert.AreEqual(3, repo.Get<Category>().Count(), "After Commit");

                Category c = repo.Get<Category>().Where(x => x.Name == "Dishes").FirstOrDefault();
                Assert.IsNotNull(c);
                Assert.AreEqual("Plates, Bowels, etc", c.Description);

                // Test user has created it and no one has updated it
                Assert.AreEqual(userId, c.CreatedBy);
                Assert.IsNull(c.UpdatedBy);
            }
        }

        /// <summary>
        /// Tests that a product can be updated in the fake context and then re-queried with updated data
        /// </summary>
        [TestMethod]
        public void LiteDb_ProductCanBeUpdated()
        {
            using (var uow = new LiteDbUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.DatabaseFileName))
            {
                var repo = new ProductRepository(uow);

                string userId = uow.AuthenticationProvider.CurrentUser.UniqueId;

                Product initial = repo.Get<Product>().Where(x => x.Name == "Dove").FirstOrDefault();
                string createdBy = initial.CreatedBy;
                Assert.IsNotNull(initial);
                Assert.AreEqual("Dove Bar Soap", initial.Description);

                initial.UpdateDescription(repo, "Dove Hand Soap");

                // Now it should have new value
                Product p = repo.Get<Product>().Where(x => x.Name == "Dove").FirstOrDefault();
                Assert.IsNotNull(p);
                Assert.AreEqual("Dove Hand Soap", p.Description);

                // Test user has updated it and initial created by has not changed
                Assert.AreEqual(createdBy, p.CreatedBy);
                Assert.AreEqual(userId, p.UpdatedBy);
            }
        }

        /// <summary>
        /// Tests that a set of products can be queried by a specific category and then have the category navigation property set
        /// </summary>
        [TestMethod]
        public void LiteDb_ProductsCanBeFetchedForCategory()
        {
            using (var uow = new LiteDbUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.DatabaseFileName))
            {
                var repo = new ProductRepository(uow);

                List<Product> prods = (from p in repo.Get<Product>()
                                       join c in repo.Get<Category>() on p.CategoryId equals c.CategoryId
                                       where c.Name == "Soap"
                                       select p).ToList();

                Assert.AreEqual(3, prods.Count());

                // Even without lazy loading a join will result in the navigation property being set
                Assert.IsNotNull(prods[0].Category);
            }
        }

        /// <summary>
        /// Tests that a set of products can be queried by a specific category and then have the category navigation property set
        /// </summary>
        [TestMethod]
        public void LiteDb_ProductsCanBeFetchedWithCategoryNavigationSetUsingExplicitLoad()
        {
            using (var uow = new LiteDbUnitOfWork(this.AuthenticationProvider, this.MessageProvider, this.EventDispatcher, this.DatabaseFileName))
            {
                var repo = new ProductRepository(uow);

                List<Product> prods = (from p in repo.Get<Product>()
                                       select p).ToList();

                Assert.AreEqual(5, prods.Count());
                Assert.IsNotNull(prods[0].Category);
            }
        }
    }
}

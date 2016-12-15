// ***********************************************************************
// <copyright file="Category.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.ComponentModel.DataAnnotations;
using System.Linq;
using Kore.Domain.Context;

namespace Kore.Domain.EF.Tests.Domain.Products.Models
{
    /// <summary>
    /// A test Category model for the Core.EF Testing Suite
    /// </summary>
    /// <seealso cref="Kore.Domain.Context.BaseEntity" />
    /// <seealso cref="Kore.Domain.Context.IEntity" />
    public class Category : BaseEntity, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        public Category()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Category"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public Category(IRepository repository, string name, string description)
        {
            this.Name = name;
            this.Description = description;
            this.EntityState = DomainState.Added;
            this.DisplayOrder = repository.Get<Category>().Select(x => x.DisplayOrder).DefaultIfEmpty(0).Max() + 1;
            repository.Add(this);
        }

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

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public void Update(IRepository repository)
        {
            this.EntityState = DomainState.Modified;
            repository.Update(this);
        }

        /// <summary>
        /// Updates the description.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="description">The description.</param>
        public void UpdateDescription(IRepository repository, string description)
        {
            this.Description = description;
            this.Update(repository);
        }

        /// <summary>
        /// Moves up.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public void MoveUp(IRepository repository)
        {
            Category prev = repository.Get<Category>().Where(x => x.DisplayOrder < this.DisplayOrder).OrderByDescending(x => x.DisplayOrder).FirstOrDefault();
            if (prev != null)
            {
                int prevOrder = prev.DisplayOrder;
                prev.DisplayOrder = this.DisplayOrder;
                this.DisplayOrder = prevOrder;
                this.Update(repository);
                repository.Update(prev);
            }
        }

        /// <summary>
        /// Moves down.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public void MoveDown(IRepository repository)
        {
            Category next = repository.Get<Category>().Where(x => x.DisplayOrder > this.DisplayOrder).OrderBy(x => x.DisplayOrder).FirstOrDefault();
            if (next != null)
            {
                int nextOrder = next.DisplayOrder;
                next.DisplayOrder = this.DisplayOrder;
                this.DisplayOrder = nextOrder;
                this.Update(repository);
                repository.Update(next);
            }
        }
    }
}

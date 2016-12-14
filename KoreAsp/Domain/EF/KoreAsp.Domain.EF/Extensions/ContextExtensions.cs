// ***********************************************************************
// <copyright file="ContextExtensions.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System.Data.Entity;
using KoreAsp.Domain.Context;

namespace KoreAsp.Domain.EF.Extensions
{
    /// <summary>
    /// Extensions for the domain context.
    /// </summary>
    public static class ContextExtensions
    {
        /// <summary>
        /// To the entity state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The entity state.</returns>
        public static EntityState ToEntityState(this DomainState state)
        {
            switch (state)
            {
                case DomainState.Added:
                    return EntityState.Added;
                case DomainState.Deleted:
                    return EntityState.Deleted;
                case DomainState.Detached:
                    return EntityState.Detached;
                case DomainState.Modified:
                    return EntityState.Modified;
                case DomainState.Unchanged:
                default:
                    return EntityState.Unchanged;
            }
        }

        /// <summary>
        /// To the domain state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The domain state.</returns>
        public static DomainState ToDomainState(this EntityState state)
        {
            switch (state)
            {
                case EntityState.Added:
                    return DomainState.Added;
                case EntityState.Deleted:
                    return DomainState.Deleted;
                case EntityState.Detached:
                    return DomainState.Detached;
                case EntityState.Modified:
                    return DomainState.Modified;
                case EntityState.Unchanged:
                default:
                    return DomainState.Unchanged;
            }
        }
    }
}

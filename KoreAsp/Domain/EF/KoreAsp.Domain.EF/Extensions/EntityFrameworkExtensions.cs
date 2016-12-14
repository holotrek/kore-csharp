// ***********************************************************************
// <copyright file="EntityFrameworkExtensions.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq.Expressions;
using KoreAsp.Extensions;

namespace KoreAsp.Domain.EF.Extensions
{
    /// <summary>
    /// Extensions to ease some of the development of entity framework code first configurations.
    /// </summary>
    public static class EntityFrameworkExtensions
    {
        /// <summary>
        /// Maps an association from a left table to a right table through the specified association table.
        /// </summary>
        /// <typeparam name="TRightEntity">The type of the right entity.</typeparam>
        /// <typeparam name="TLeftEntity">The type of the left entity.</typeparam>
        /// <param name="conf">The configuration.</param>
        /// <param name="associationTableName">Name of the association table.</param>
        /// <param name="rightCollection">The right collection.</param>
        /// <param name="rightPrimaryKey">The right primary key.</param>
        /// <param name="leftCollection">The left collection.</param>
        /// <param name="leftPrimaryKey">The left primary key.</param>
        public static void MapAssociation<TRightEntity, TLeftEntity>(
            this EntityTypeConfiguration<TLeftEntity> conf,
            string associationTableName,
            Expression<Func<TLeftEntity, ICollection<TRightEntity>>> rightCollection,
            Expression<Func<TLeftEntity, int>> rightPrimaryKey,
            Expression<Func<TRightEntity, ICollection<TLeftEntity>>> leftCollection,
            Expression<Func<TRightEntity, int>> leftPrimaryKey)
            where TLeftEntity : class
            where TRightEntity : class
        {
            conf.HasMany(rightCollection).WithMany(leftCollection).Map(m =>
            {
                m.MapLeftKey(leftPrimaryKey.GetPropertyName());
                m.MapRightKey(rightPrimaryKey.GetPropertyName());
                m.ToTable(associationTableName);
            });
        }
    }
}

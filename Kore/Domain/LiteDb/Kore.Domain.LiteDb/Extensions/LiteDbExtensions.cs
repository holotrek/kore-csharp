// ***********************************************************************
// <copyright file="LiteDbExtensions.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Kore.Domain.Context;
using LiteDB;

namespace Kore.Domain.LiteDb.Extensions
{
    /// <summary>
    /// Provides extension methods for more easily accessing collections and performing operations on them.
    /// </summary>
    public static class LiteDbExtensions
    {
        /// <summary>
        /// Gets the name of the collection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>Gets the collection from the LiteDB connection.</returns>
        public static string GetCollectionName<TEntity>()
            where TEntity : class, IEntity, new()
        {
            return typeof(TEntity).Name;
        }

        /// <summary>
        /// Seeds the provided data into the Lite DB connection, by dropping the existing collection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="connection">The connection.</param>
        /// <param name="data">The data.</param>
        public static void SeedData<TEntity>(this LiteDatabase connection, IEnumerable<TEntity> data)
            where TEntity : class, IEntity, new()
        {
            string entName = GetCollectionName<TEntity>();
            if (connection.CollectionExists(entName))
            {
                connection.DropCollection(entName);
            }

            connection.GetCollection<TEntity>(entName).Insert(data);
        }

        /// <summary>
        /// Determines if the collection exists using the provided entity type as the name of the collection.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="connection">The connection.</param>
        /// <returns><c>true</c> if the Lite DB collection exists, <c>false</c> otherwise.</returns>
        public static bool CollectionExists<TEntity>(this LiteDatabase connection)
            where TEntity : class, IEntity, new()
        {
            return connection.CollectionExists(GetCollectionName<TEntity>());
        }

        /// <summary>
        /// Ensures that the Lite DB collection has the provided index.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="connection">The connection.</param>
        /// <param name="property">An expression representing the entity property to add the index for.</param>
        public static void EnsureIndex<TEntity>(this LiteDatabase connection, Expression<Func<TEntity, object>> property)
            where TEntity : class, IEntity, new()
        {
            string entName = GetCollectionName<TEntity>();
            if (connection.CollectionExists(entName))
            {
                connection.GetCollection<TEntity>(entName).EnsureIndex(property);
            }
        }
    }
}

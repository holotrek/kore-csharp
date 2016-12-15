// ***********************************************************************
// <copyright file="IFormatterStrategy.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace Kore.Formatters
{
    /// <summary>
    /// Contract for a collection of different formatters in order to provide consistency for the application.
    /// </summary>
    public interface IFormatterStrategy
    {
        #region Properties

        /// <summary>
        /// Gets the formatters.
        /// </summary>
        Dictionary<Type, IFormatter> Formatters { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the formatter for the specified interface type.
        /// </summary>
        /// <param name="formatterInterface">The formatter interface type, for example <see cref="IDateFormatter"/>.</param>
        /// <param name="formatter">The formatter.</param>
        void SetFormatter(Type formatterInterface, IFormatter formatter);

        /// <summary>
        /// Formats the specified object.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="obj">The object.</param>
        void Format<T>(T obj);

        #endregion
    }
}

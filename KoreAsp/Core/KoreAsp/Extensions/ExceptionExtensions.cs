// ***********************************************************************
// <copyright file="ExceptionExtensions.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;

namespace KoreAsp.Extensions
{
    /// <summary>
    /// Extension methods for the Exception class.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Gets the most inner exception.
        /// </summary>
        /// <param name="exception">The original exception.</param>
        /// <returns>The most inner exception.</returns>
        public static Exception GetMostInner(this Exception exception)
        {
            Exception mostInner = exception;
            while (mostInner.InnerException != null)
            {
                mostInner = mostInner.InnerException;
            }

            return mostInner;
        }
    }
}

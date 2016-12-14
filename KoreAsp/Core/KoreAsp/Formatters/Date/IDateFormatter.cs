// ***********************************************************************
// <copyright file="IDateFormatter.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;

namespace KoreAsp.Formatters
{
    /// <summary>
    /// Contract for a formatter that takes a <see cref="DateTime?"/> and formats it based on the current culture
    /// </summary>
    public interface IDateFormatter : IFormatter
    {
        /// <summary>
        /// Gets a date and time based on the current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The formatted date and time.</returns>
        string ToDateAndTime(DateTime? date);

        /// <summary>
        /// Gets a short date based on the current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The formatted short date.</returns>
        string ToShortDate(DateTime? date);

        /// <summary>
        /// Gets a long date based on the current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The formatted long date.</returns>
        string ToLongDate(DateTime? date);

        /// <summary>
        /// Gets a short time based on the current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The formatted short time.</returns>
        string ToShortTime(DateTime? date);

        /// <summary>
        /// Gets a long time based on the current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The formatted long time.</returns>
        string ToLongTime(DateTime? date);

        /// <summary>
        /// Gets a sortable date and time based on the current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The formatted sortable date and time.</returns>
        string ToSortable(DateTime? date);

        /// <summary>
        /// Gets the date and time as ticks based on the current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The formatted sortable date and time.</returns>
        string ToTicks(DateTime? date);
    }
}

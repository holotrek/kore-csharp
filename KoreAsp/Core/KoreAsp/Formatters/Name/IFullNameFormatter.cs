// ***********************************************************************
// <copyright file="IFullNameFormatter.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using KoreAsp.Composites;

namespace KoreAsp.Formatters
{
    /// <summary>
    /// A formatter that takes a set of name properties and creates a name using the implementation set by the current culture.
    /// </summary>
    public interface IFullNameFormatter : IFormatter
    {
        /// <summary>
        /// Gets the full name based on the current culture.
        /// </summary>
        /// <param name="fullName">The full name object.</param>
        /// <returns>
        /// The full name.
        /// </returns>
        string ToFullName(FullName fullName);

        /// <summary>
        /// Gets the full name based on the current culture.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">Name of the middle.</param>
        /// <returns>
        /// The full name.
        /// </returns>
        string ToFullName(string lastName, string firstName, string middleName = "");
    }
}

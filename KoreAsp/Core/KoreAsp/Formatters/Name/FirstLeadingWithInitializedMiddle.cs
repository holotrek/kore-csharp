// ***********************************************************************
// <copyright file="FirstLeadingWithInitializedMiddle.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using KoreAsp.Composites;

namespace KoreAsp.Formatters
{
    /// <summary>
    /// A full name formatter that returns a name in the format "First M. Last".
    /// </summary>
    /// <seealso cref="KoreAsp.Formatters.Name.IFullNameFormatter" />
    public class FirstLeadingWithInitializedMiddle : IFullNameFormatter
    {
        /// <summary>
        /// Gets the full name based on the current culture.
        /// </summary>
        /// <param name="fullName">The full name object.</param>
        /// <returns>
        /// The full name.
        /// </returns>
        public string ToFullName(FullName fullName)
        {
            return this.ToFullName(fullName.LastName, fullName.FirstName, fullName.MiddleName);
        }

        /// <summary>
        /// Gets the full name based on the current culture.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">Name of the middle.</param>
        /// <returns>
        /// The full name.
        /// </returns>
        public string ToFullName(string lastName, string firstName, string middleName = "")
        {
            return string.Format("{0}{1} {2}", firstName, string.IsNullOrWhiteSpace(middleName) ? string.Empty : " " + middleName[0] + ".", lastName);
        }
    }
}

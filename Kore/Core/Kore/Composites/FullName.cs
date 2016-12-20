// ***********************************************************************
// <copyright file="FullName.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using Kore.Domain.Context;

namespace Kore.Composites
{
    /// <summary>
    /// Represents a full name that can be formatted based on the current culture info.
    /// </summary>
    public class FullName : ValueObject<FullName>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FullName"/> class.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">Name of the middle.</param>
        public FullName(string lastName, string firstName, string middleName)
        {
            this.LastName = lastName;
            this.FirstName = firstName;
            this.MiddleName = middleName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the last name.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the middle name.
        /// </summary>
        public string MiddleName { get; private set; }

        #endregion
    }
}

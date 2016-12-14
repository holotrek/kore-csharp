// ***********************************************************************
// <copyright file="FullName.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using KoreAsp.Domain.Context;

namespace KoreAsp.Composites
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
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        public string MiddleName { get; set; }

        #endregion
    }
}

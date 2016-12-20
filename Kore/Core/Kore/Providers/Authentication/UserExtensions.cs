// ***********************************************************************
// <copyright file="UserExtensions.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using Kore.Composites;
using Kore.Domain.Context;
using Kore.Formatters;

namespace Kore.Providers.Authentication
{
    /// <summary>
    /// Extension methods for an <see cref="IUser"/>.
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Gets the identifier used to mark the CreatedBy and UpdatedBy properties of an entity.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="mode">The record stub mode.</param>
        /// <returns>The identifier string generated.</returns>
        public static string GetRecordIdentifier(this IUser user, RecordStubMode mode)
        {
            switch (mode)
            {
                case RecordStubMode.LastFirstUserName:
                    var fn = new FullName(user.LastName, user.FirstName, user.MiddleName);
                    return string.Format("{0} ({1})", new LastLeadingWithNoMiddle().ToFullName(fn), user.UserName);
                case RecordStubMode.UserName:
                    return user.UserName;
                default:
                case RecordStubMode.UniqueId:
                    return user.UniqueId;
            }
        }
    }
}

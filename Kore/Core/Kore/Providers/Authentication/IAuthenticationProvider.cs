// ***********************************************************************
// <copyright file="IAuthenticationProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

namespace Kore.Providers.Authentication
{
    /// <summary>
    /// A contract for an authentication provider so that the appropriate authentication can be injected into the service.
    /// </summary>
    public interface IAuthenticationProvider
    {
        #region Properties

        /// <summary>
        /// Gets the current user.
        /// </summary>
        IUser CurrentUser { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>
        /// The registered user or null if it failed.
        /// </returns>
        IUser Register(IUser userData);

        /// <summary>
        /// Logs in the specified user.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>
        /// The token of the logged in user.
        /// </returns>
        string Login(IUser userData);

        /// <summary>
        /// Logs out the user.
        /// </summary>
        /// <param name="authToken">The authentication token.</param>
        void LogOut(string authToken);

        /// <summary>
        /// Checks the token to see if the user is still logged in.
        /// </summary>
        /// <param name="authToken">The authentication token.</param>
        /// <returns>
        /// The user that is logged in, or null if the token is no longer valid.
        /// </returns>
        IUser CheckToken(string authToken);

        #endregion
    }
}

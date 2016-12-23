// ***********************************************************************
// <copyright file="MockAuthenticationProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;

namespace Kore.Providers.Authentication
{
    /// <summary>
    /// An authentication provider for testing that will always allow all actions.
    /// </summary>
    public class MockAuthenticationProvider : IAuthenticationProvider
    {
        /// <summary>
        /// The current user
        /// </summary>
        private static IUser _currentUser;

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>The current user.</value>
        public IUser CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = new MockUser
                    {
                        UserId = Guid.NewGuid().ToString(),
                        UserName = "Test",
                        FirstName = "Test",
                        LastName = "User",
                        MiddleName = "A"
                    };
                }

                return _currentUser;
            }
        }

        /// <summary>
        /// Checks the token to see if the user is still logged in.
        /// </summary>
        /// <param name="authToken">The authentication token.</param>
        /// <returns>The user that is logged in, or null if the token is no longer valid.</returns>
        public IUser CheckToken(string authToken)
        {
            return this.CurrentUser;
        }

        /// <summary>
        /// Logs in the specified user.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>The token of the logged in user.</returns>
        public string Login(IUser userData)
        {
            return this.CurrentUser.UniqueId;
        }

        /// <summary>
        /// Logs out the user.
        /// </summary>
        /// <param name="authToken">The authentication token.</param>
        public void LogOut(string authToken)
        {
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>The registered user or null if it failed.</returns>
        public IUser Register(IUser userData)
        {
            return userData;
        }
    }
}

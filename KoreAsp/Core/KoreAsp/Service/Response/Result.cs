// ***********************************************************************
// <copyright file="Result.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using System;

namespace KoreAsp.Response
{
    /// <summary>
    /// Class Result.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets a value indicating whether this instance is successful.
        /// </summary>
        /// <value><c>true</c> if this instance is successful; otherwise, <c>false</c>.</value>
        public bool IsSuccessful { get; private set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Result"/> is failure.
        /// </summary>
        /// <value><c>true</c> if failure; otherwise, <c>false</c>.</value>
        public bool Failure
        {
            get
            {
                return !IsSuccessful;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="errorMessage">The error message.</param>
        protected Result(bool success, string errorMessage)
        {
            this.IsSuccessful = success;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Fails the specified error message.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The result of the failed request.</returns>
        public static Result Fail(string errorMessage)
        {
            return new Result(false, errorMessage);
        }

        /// <summary>
        /// Fails the specified message.
        /// </summary>
        /// <typeparam name="T">The type of the object returned from the method.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns>The result of the failed request.</returns>
        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        }

        /// <summary>
        /// Oks this instance.
        /// </summary>
        /// <returns>The result of the OK request.</returns>
        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        /// <summary>
        /// Oks the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the object returned from the method.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The result of the OK request.</returns>
        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        /// <summary>
        /// Combines the specified results.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <returns>The combined requests.</returns>
        public static Result Combine(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.Failure)
                {
                    return result;
                }
            }

            return Ok();
        }

        /// <summary>
        /// Called when [success].
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The result of the successful request.</returns>
        public Result OnSuccess(Func<Result> func)
        {
            if (Failure)
            {
                return this;
            }

            return func();
        }

        /// <summary>
        /// Called when [success].
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>The result of the successful request.</returns>
        public Result OnSuccess(Action action)
        {
            if (Failure)
            {
                return this;
            }

            action();
            return Ok();
        }

        /// <summary>
        /// Called when [failure].
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>The result of the failed request.</returns>
        public Result OnFailure(Action action)
        {
            if (Failure)
            {
                action();
            }

            return this;
        }

        /// <summary>
        /// Called when [failure].
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The result of the failed request.</returns>
        public Result OnFailure(Func<Result> func)
        {
            if (this.Failure)
            {
                return func();
            }

            return this;
        }

        /// <summary>
        /// Called when [both].
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>The result of the request.</returns>
        public Result OnBoth(Action<Result> action)
        {
            action(this);

            return this;
        }
    }

    /// <summary>
    /// Class Result.
    /// </summary>
    /// <typeparam name="T">The type of the object returned from the method.</typeparam>
    /// <seealso cref="KoreAsp.Response.Result" />
    public class Result<T> : Result
    {
        /// <summary>
        /// The value
        /// </summary>
        private T _value;

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public T Value
        {
            get
            {   
                return _value;
            }            

            private set
            {
                _value = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="error">The error.</param>
        protected internal Result(T value, bool success, string error)
            : base(success, error)
        {
            Value = value;
        }

        /// <summary>
        /// Called when [success].
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>The result of the successful request.</returns>
        public Result OnSuccess(Action<T> action)
        {
            if (Failure)
            {
                return this;
            }

            action(Value);

            return Ok();
        }

        /// <summary>
        /// Called when [success].
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The result of the successful request.</returns>
        public Result<T> OnSuccess(Func<T> func)
        {
            if (Failure)
            {
                return Result.Fail<T>(ErrorMessage);
            }

            return Result.Ok(func());
        }

        /// <summary>
        /// Called when [success].
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The result of the successful request.</returns>
        public Result<T> OnSuccess(Func<Result<T>> func)
        {
            if (Failure)
            {
                return Result.Fail<T>(ErrorMessage);
            }

            return func();
        }

        /// <summary>
        /// Called when [success].
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The result of the successful request.</returns>
        public Result OnSuccess(Func<T, Result> func)
        {
            if (Failure)
            {
                return this;
            }

            return func(Value);
        }

        /// <summary>
        /// Called when [both].
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The result of the request.</returns>
        public T OnBoth(Func<Result, T> func)
        {
            return func(this);
        }
    }
}

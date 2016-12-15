// ***********************************************************************
// <copyright file="ModelResponse.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

namespace Kore.Response
{
    /// <summary>
    /// Encapsulates the response of a service request, which returns a model.
    /// </summary>
    /// <typeparam name="T">The model type</typeparam>
    /// <seealso cref="Kore.Response.Response" />
    /// <seealso cref="Kore.Response.IResponse" />
    public class ModelResponse<T> : Response, IResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelResponse{T}" /> class.
        /// </summary>
        public ModelResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelResponse{T}" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public ModelResponse(T model)
        {
            this.Model = model;
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public T Model { get; set; }
    }
}

// ***********************************************************************
// <copyright file="ISerializationProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

namespace Kore.Providers.Serialization
{
    /// <summary>
    /// A contract that provides basic serialization and deserialization.
    /// </summary>
    public interface ISerializationProvider
    {
        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The serialized string.</returns>
        string SerializeObject(object value);

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The serialized string.</returns>
        string SerializeObject<T>(T value);

        /// <summary>
        /// Deserializes the object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The resulting object.</returns>
        object DeserializeObject(string value);

        /// <summary>
        /// Deserializes the object.
        /// </summary>
        /// <typeparam name="T">The type of the object that the string is being deserialized into.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The resulting object.</returns>
        T DeserializeObject<T>(string value);
    }
}

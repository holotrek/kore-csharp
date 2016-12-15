// ***********************************************************************
// <copyright file="NewtonsoftSerializationProvider.cs" company="Holotrek">
//     Copyright © Holotrek 2016
// </copyright>
// ***********************************************************************

using Newtonsoft.Json;

namespace Kore.Providers.Serialization.Newtonsoft
{
    /// <summary>
    /// An implementation of the <see cref="Kore.Providers.Serialization.ISerializationProvider" />
    /// that utilizes the NEWTONSOFT JSON library.
    /// </summary>
    /// <seealso cref="Kore.Providers.Serialization.ISerializationProvider" />
    public class NewtonsoftSerializationProvider : ISerializationProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewtonsoftSerializationProvider" /> class.
        /// </summary>
        public NewtonsoftSerializationProvider()
        {
            this.Settings = new JsonSerializerSettings();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewtonsoftSerializationProvider" /> class.
        /// </summary>
        /// <param name="settings">The serializer settings.</param>
        public NewtonsoftSerializationProvider(JsonSerializerSettings settings)
        {
            this.Settings = settings;
        }

        /// <summary>
        /// Gets or sets the serializer settings.
        /// </summary>
        /// <value>The serializer settings.</value>
        public JsonSerializerSettings Settings { get; set; }

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The serialized string.</returns>
        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, this.Settings);
        }

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <typeparam name="T">The type of the object being serialized.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The serialized string.</returns>
        public string SerializeObject<T>(T value)
        {
            return JsonConvert.SerializeObject(value, typeof(T), this.Settings);
        }

        /// <summary>
        /// Deserializes the object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The resulting object.</returns>
        public object DeserializeObject(string value)
        {
            return JsonConvert.DeserializeObject(value, this.Settings);
        }

        /// <summary>
        /// Deserializes the object.
        /// </summary>
        /// <typeparam name="T">The type of the object that the string is being deserialized into.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The resulting object.</returns>
        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, this.Settings);
        }
    }
}

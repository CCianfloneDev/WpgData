using System.Text.Json;

namespace Finance.Components.Services
{
    /// <summary>
    /// Provides helper methods for serializing and deserializing JSON data.
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Shared instance of <see cref="JsonSerializerOptions"/> to improve performance and avoid CA1869 warnings.
        /// </summary>
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        /// <summary>
        /// Deserializes a JSON string into a list of objects of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of objects to deserialize.</typeparam>
        /// <param name="json">The JSON string representing a list of objects.</param>
        /// <returns>A list of deserialized objects. Returns an empty list if deserialization fails.</returns>
        public static List<T> DeserializeList<T>(string json)
        {
            return JsonSerializer.Deserialize<List<T>>(json, _jsonOptions) ?? [];
        }

        /// <summary>
        /// Serializes a list of objects into a JSON string.
        /// </summary>
        /// <typeparam name="T">The type of objects to serialize.</typeparam>
        /// <param name="data">The list of objects to serialize.</param>
        /// <returns>A formatted JSON string representing the list of objects.</returns>
        public static string SerializeList<T>(List<T> data)
        {
            return JsonSerializer.Serialize(data, _jsonOptions);
        }
    }
}

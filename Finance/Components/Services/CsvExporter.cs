using System.Reflection;
using System.Text;

namespace Finance.Components.Services
{
    /// <summary>
    /// Provides functionality to export a list of objects as a CSV file.
    /// </summary>
    public class CsvExporter
    {
        /// <summary>
        /// Converts a list of objects into a CSV-formatted string.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="data">The list of objects to convert.</param>
        /// <returns>A CSV string representing the list.</returns>
        public static string ConvertToCsv<T>(List<T> data)
        {
            if (data == null || data.Count == 0)
                return string.Empty;

            var csvBuilder = new StringBuilder();
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Write CSV Header
            csvBuilder.AppendLine(string.Join(",", properties.Select(p => p.Name)));

            // Write CSV Rows
            foreach (var item in data)
            {
                var values = properties.Select(p =>
                    p.GetValue(item, null)?.ToString()?.Replace(",", " ") ?? ""); // Avoid CSV format breaking
                csvBuilder.AppendLine(string.Join(",", values));
            }

            return csvBuilder.ToString();
        }
    }
}

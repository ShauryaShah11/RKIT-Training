using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace AdvanceC_FinalDemo.Extensions
{
    /// <summary>
    /// Provides extension methods for the List<T> class.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Converts a list of objects of type T to a DataTable.
        /// Each object in the list is represented as a row in the DataTable,
        /// and each property of the object is represented as a column.
        /// </summary>
        /// <typeparam name="T">The type of the objects in the list.</typeparam>
        /// <param name="data">The list of objects to be converted to a DataTable.</param>
        /// <returns>A DataTable that contains the data from the list.</returns>
        public static DataTable ConvertToDataTable<T>(this List<T> data)
        {
            var dataTable = new DataTable(typeof(T).Name);
            var properties = typeof(T).GetProperties();

            // Add columns
            foreach (var prop in properties)
            {
                Type propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                // Handle unsupported types by converting them to a suitable type
                if (propType == typeof(Type))
                {
                    propType = typeof(string); // Convert System.Type to string
                }
                else if (propType == typeof(Guid))
                {
                    propType = typeof(string); // Convert Guid to string, or handle it as needed
                }
                else if (propType == typeof(DBNull))
                {
                    propType = typeof(object); // DBNull to object (or handle as needed)
                }

                // Additional logging to verify what types are being processed
                Debug.WriteLine($"Property: {prop.Name}, Type: {propType}");

                // Add the column to the DataTable
                try
                {
                    dataTable.Columns.Add(prop.Name, propType);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error adding column {prop.Name}: {ex.Message}");
                }
            }

            // Add rows
            foreach (var item in data)
            {
                var row = dataTable.NewRow();
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(item);

                    // Debugging: Output the value of each property using Debug.WriteLine
                    Debug.WriteLine($"Property: {prop.Name}, Value: {value}");

                    if (value is Type typeValue)
                    {
                        // Convert System.Type to string if it's of Type
                        row[prop.Name] = typeValue.ToString();
                    }
                    else
                    {
                        row[prop.Name] = value ?? DBNull.Value;
                    }
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}
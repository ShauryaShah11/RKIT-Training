using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace AdvanceC_FinalDemo.Extension_Methods
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Converts an IEnumerable of any type to a DataTable.
        /// </summary>
        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            var dataTable = new DataTable(typeof(T).Name);

            // Get all properties of the object
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Add columns to the DataTable based on the properties of the class
            foreach (var prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            // Populate rows of the DataTable based on the data
            foreach (var item in data)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
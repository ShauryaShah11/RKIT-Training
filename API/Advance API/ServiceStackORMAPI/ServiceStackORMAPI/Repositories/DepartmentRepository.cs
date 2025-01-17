using ServiceStack.OrmLite;
using ServiceStackORMAPI.Models;
using ServiceStackORMAPI.Models.DTO;
using ServiceStackORMAPI.Models.POCO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace ServiceStackORMAPI.Repositories
{
    /// <summary>
    /// The DepartmentRepository class provides methods to manage department data within the database.
    /// It handles CRUD operations (Create, Read, Update, Delete) for departments and performs validation
    /// to ensure data integrity before performing these operations.
    /// </summary>
    public class DepartmentRepository
    {
        private readonly IDbConnection _db;

        // Get the connection string from web.config
        static string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        // Initialize the OrmLiteConnectionFactory with the connection string and provider
        OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

        // Open the connection

        /// <summary>
        /// Initializes a new instance of the DepartmentService class with an injected database connection.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        public DepartmentRepository()
        {
            _db = dbFactory.Open();

            // Ensure the table exists in the database
            _db.CreateTableIfNotExists<YMD01>();
        }

        /// <summary>
        /// Handles department operations: Add, Edit, or Delete.
        /// </summary>
        /// <param name="dto">The DTO containing department data.</param>
        /// <param name="entryType">The operation type: 1 (Add), 2 (Edit), 3 (Delete).</param>
        /// <returns>Returns a Response object.</returns>
        public Response HandleOperation(DTOYMD01 dto, int entryType)
        {
            try
            {
                // Convert DTO to POCO
                YMD01 poco = PreSave(dto);

                // Validate the operation based on entry type
                Response validationResponse = ValidateOnSave(poco, entryType);

                // If validation fails, return the error response
                if (validationResponse.IsError)
                {
                    return validationResponse;
                }

                // Proceed with the database operation based on entry type (Add, Edit, Delete)
                return Save(poco, entryType);
            }
            catch (Exception ex)
            {
                // Catch and return any unexpected errors
                return new Response { IsError = true, Message = ex.Message };
            }
        }

        /// <summary>
        /// Converts DTO object into POCO object.
        /// </summary>
        private YMD01 PreSave(DTOYMD01 dto)
        {
            return new YMD01
            {
                D01F01 = dto.D01101,
                D02F02 = dto.D02102,
                D03F03 = dto.D03103,
                D04F04 = dto.D04104
            };
        }

        /// <summary>
        /// Validates the department data before saving it.
        /// </summary>
        private Response ValidateOnSave(YMD01 poco, int entryType)
        {
            // Check if the department already exists for Add operation
            if (entryType == 1 && _db.Exists<YMD01>(d => d.D02F02 == poco.D02F02))
            {
                return new Response { IsError = true, Message = "Department already exists." };
            }

            // Check if the department exists for Edit or Delete operation
            if ((entryType == 2 || entryType == 3) && !_db.Exists<YMD01>(d => d.D02F02 == poco.D02F02))
            {
                return new Response { IsError = true, Message = "Department does not exist." };
            }

            return new Response { Message = "Validation successful." };
        }

        /// <summary>
        /// Saves the department data to the database based on the operation type.
        /// </summary>
        private Response Save(YMD01 poco, int entryType)
        {
            switch (entryType)
            {
                // Add operation (Insert new department)
                case 1:
                    _db.Insert(poco); // Insert the new department into the database
                    return new Response { IsError = false, Message = "Department added successfully." };

                // Edit operation (Update existing department)
                case 2:
                    _db.Update(poco); // Update the department in the database
                    return new Response { IsError = false, Message = "Department updated successfully." };

                // Delete operation (Delete department)
                case 3:
                    _db.Delete<YMD01>(x => x.D02F02 == poco.D02F02); // Delete the department based on its ID or code
                    return new Response { IsError = false, Message = "Department deleted successfully." };

                // Default case for invalid entry type
                default:
                    return new Response { IsError = true, Message = "Invalid entry type." };
            }
        }

        /// <summary>
        /// Retrieves all department data.
        /// </summary>
        public Response GetAllDepartments()
        {
            try
            {
                List<YMD01> data = _db.Select<YMD01>();
                return new Response
                {
                    IsError = false,
                    Message = "Data retrieved successfully.",
                    Data = ConvertToDataTable(data)
                };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }

        /// <summary>
        /// Converts a list of objects to a DataTable.
        /// </summary>
        private DataTable ConvertToDataTable<T>(List<T> data)
        {
            var dataTable = new DataTable(typeof(T).Name);

            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (var item in data)
            {
                var row = dataTable.NewRow();
                foreach (var prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}

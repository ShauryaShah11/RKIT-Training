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
    /// The EmployeeRepository class provides methods to manage employee data within the database.
    /// It handles CRUD operations (Create, Read, Update, Delete) for employee and performs validation
    /// to ensure data integrity before performing these operations.
    /// </summary>
    public class EmployeeRepository
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
        public EmployeeRepository()
        {
            _db = dbFactory.Open();

            // Ensure the table exists in the database
            _db.CreateTableIfNotExists<YME01>();
        }

        /// <summary>
        /// Handles employee operations: Add, Edit, or Delete.
        /// </summary>
        /// <param name="dto">The DTO containing employee data.</param>
        /// <param name="entryType">The operation type: 1 (Add), 2 (Edit), 3 (Delete).</param>
        /// <returns>A Response object indicating the outcome.</returns>
        public Response HandleOperation(DTOYME01 dto, int entryType)
        {
            try
            {
                var poco = PreSave(dto);

                // Validation
                var validationResponse = ValidateOnSave(poco, entryType);
                if (validationResponse.IsError)
                    return validationResponse;

                // Proceed with the database operation based on entry type (Add, Edit, Delete)
                return Save(poco, entryType);
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }

        /// <summary>
        /// Converts DTO object into POCO object.
        /// </summary>
        private YME01 PreSave(DTOYME01 dto)
        {
            return new YME01
            {
                E01F01 = dto.E01101,
                E02F02 = dto.E02102,
                E04F04 = dto.E04104,
                E03F03 = DateTime.Now
            };
        }

        /// <summary>
        /// Validates the employee data based on the operation type.
        /// </summary>
        private Response ValidateOnSave(YME01 poco, int entryType)
        {
            if (entryType == 1 && _db.Exists<YME01>(x => x.E01F01 == poco.E01F01))
            {
                return new Response { IsError = true, Message = "Employee already exists." };
            }
            if (entryType == 1 && !_db.Exists<YMD01>(d => d.D01F01 == poco.E04F04))
            {
                return new Response { IsError = true, Message = "Department does not exist." };
            }
            if ((entryType == 2 || entryType == 3) && !_db.Exists<YME01>(x => x.E01F01 == poco.E01F01))
            {
                return new Response { IsError = true, Message = "Employee not found." };
            }

            return new Response { Message = "Validation successful." };
        }

        /// <summary>
        /// Saves the employee data to the database based on the operation type.
        /// </summary>
        private Response Save(YME01 poco, int entryType)
        {
            switch (entryType)
            {
                case 1: // Add operation (Insert new employee)
                    _db.Insert(poco);
                    return new Response { IsError = false, Message = "Employee added successfully." };

                case 2: // Edit operation (Update existing employee)
                    _db.Update(poco);
                    return new Response { IsError = false, Message = "Employee updated successfully." };

                case 3: // Delete operation (Delete employee)
                    _db.Delete<YME01>(x => x.E01F01 == poco.E01F01);
                    return new Response { IsError = false, Message = "Employee deleted successfully." };

                default:
                    return new Response { IsError = true, Message = "Invalid operation type." };
            }
        }

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        public Response GetAllEmployees()
        {
            try
            {
                var data = _db.Select<dynamic>("SELECT e.E01F01, e.E02F02, e.E03F03, e.E04F04, d.D02F02 AS DepartmentName " +
                                               "FROM YME01 e " +
                                               "INNER JOIN YMD01 d ON e.E04F04 = d.D01F01");
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

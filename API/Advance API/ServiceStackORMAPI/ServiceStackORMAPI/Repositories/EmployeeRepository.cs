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
    /// It handles CRUD operations (Create, Read, Update, Delete) for employees and performs validation
    /// to ensure data integrity before performing these operations.
    /// </summary>
    public class EmployeeRepository
    {
        /// <summary>
        /// The database connection instance used for all database operations.
        /// </summary>
        private readonly IDbConnection _db;

        /// <summary>
        /// The connection string retrieved from the web.config file.
        /// </summary>
        static string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        /// <summary>
        /// The OrmLite connection factory instance configured with MySQL provider.
        /// </summary>
        OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

        /// <summary>
        /// Initializes a new instance of the EmployeeRepository class.
        /// Creates a database connection and ensures the required table exists.
        /// </summary>
        public EmployeeRepository()
        {
            _db = dbFactory.Open();
            _db.CreateTableIfNotExists<YME01>();
        }

        /// <summary>
        /// Handles all employee operations including Add, Edit, and Delete.
        /// Routes the operation to appropriate methods based on the entry type.
        /// </summary>
        /// <param name="dto">The Data Transfer Object containing employee information.</param>
        /// <param name="entryType">The type of operation: 1 (Add), 2 (Edit), 3 (Delete).</param>
        /// <returns>A Response object indicating the success or failure of the operation.</returns>
        public Response HandleOperation(DTOYME01 dto, int entryType)
        {
            try
            {
                switch (entryType)
                {
                    case 1: // Add
                        var addPoco = PreSave(dto);
                        var addValidation = ValidateOnSave(addPoco, entryType);
                        if (addValidation.IsError) return addValidation;
                        return Save(addPoco, entryType);

                    case 2: // Edit
                        var editPoco = PreSave(dto);
                        var editValidation = ValidateOnSave(editPoco, entryType);
                        if (editValidation.IsError) return editValidation;
                        return Save(editPoco, entryType);

                    case 3: // Delete
                        var deletePoco = PreDelete(dto);
                        var deleteValidation = ValidateOnDelete(deletePoco);
                        if (deleteValidation.IsError) return deleteValidation;
                        return Delete(deletePoco);

                    default:
                        return new Response { IsError = true, Message = "Invalid operation type." };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }

        /// <summary>
        /// Converts a DTO object into a POCO object for save operations.
        /// </summary>
        /// <param name="dto">The Data Transfer Object containing employee data.</param>
        /// <returns>A POCO object ready for database operations.</returns>
        private YME01 PreSave(DTOYME01 dto)
        {
            return new YME01
            {
                E01F01 = dto.E01101,
                E01F02 = dto.E01102,
                E01F04 = dto.E01104,
                E01F03 = DateTime.Now
            };
        }

        /// <summary>
        /// Validates employee data before save operations.
        /// Checks for duplicate employees, valid department references, and existence for updates.
        /// </summary>
        /// <param name="poco">The POCO object to validate.</param>
        /// <param name="entryType">The type of operation being performed.</param>
        /// <returns>A Response object indicating validation success or failure.</returns>
        private Response ValidateOnSave(YME01 poco, int entryType)
        {
            if (entryType == 1 && _db.Exists<YME01>(x => x.E01F01 == poco.E01F01))
            {
                return new Response { IsError = true, Message = "Employee already exists." };
            }
            if (entryType == 1 && !_db.Exists<YMD01>(d => d.D01F01 == poco.E01F04))
            {
                return new Response { IsError = true, Message = "Department does not exist." };
            }
            if (entryType == 2 && !_db.Exists<YME01>(x => x.E01F01 == poco.E01F01))
            {
                return new Response { IsError = true, Message = "Employee not found." };
            }

            return new Response { Message = "Validation successful." };
        }

        /// <summary>
        /// Performs the actual save operation in the database.
        /// Handles both insert and update operations.
        /// </summary>
        /// <param name="poco">The POCO object to save.</param>
        /// <param name="entryType">The type of save operation (1 for Add, 2 for Edit).</param>
        /// <returns>A Response object indicating the operation result.</returns>
        private Response Save(YME01 poco, int entryType)
        {
            switch (entryType)
            {
                case 1: // Add operation
                    _db.Insert(poco);
                    return new Response { IsError = false, Message = "Employee added successfully." };

                case 2: // Edit operation
                    _db.Update(poco);
                    return new Response { IsError = false, Message = "Employee updated successfully." };

                default:
                    return new Response { IsError = true, Message = "Invalid operation type for Save." };
            }
        }

        /// <summary>
        /// Prepares employee data for deletion by creating a POCO object with the employee ID.
        /// </summary>
        /// <param name="dto">The Data Transfer Object containing the employee ID.</param>
        /// <returns>A POCO object containing the employee ID for deletion.</returns>
        private YME01 PreDelete(DTOYME01 dto)
        {
            return new YME01
            {
                E01F01 = dto.E01101  // Only need the ID for deletion
            };
        }

        /// <summary>
        /// Validates whether an employee can be deleted.
        /// Checks if the employee exists and any other deletion constraints.
        /// </summary>
        /// <param name="poco">The POCO object containing the employee ID.</param>
        /// <returns>A Response object indicating whether deletion is allowed.</returns>
        private Response ValidateOnDelete(YME01 poco)
        {
            if (!_db.Exists<YME01>(x => x.E01F01 == poco.E01F01))
            {
                return new Response { IsError = true, Message = "Employee not found for deletion." };
            }

            return new Response { Message = "Validation successful for deletion." };
        }

        /// <summary>
        /// Performs the actual deletion of an employee from the database.
        /// </summary>
        /// <param name="poco">The POCO object containing the employee ID to delete.</param>
        /// <returns>A Response object indicating the success or failure of the deletion.</returns>
        private Response Delete(YME01 poco)
        {
            try
            {
                _db.Delete<YME01>(x => x.E01F01 == poco.E01F01);
                return new Response { IsError = false, Message = "Employee deleted successfully." };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = $"Error deleting employee: {ex.Message}" };
            }
        }

        /// <summary>
        /// Retrieves all employees from the database along with their department information.
        /// Performs a JOIN operation between employee and department tables.
        /// </summary>
        /// <returns>A Response object containing a DataTable with all employee records.</returns>
        public Response GetAllEmployees()
        {
            try
            {
                List<DTOYME02> employees = _db.Select<DTOYME02>(@"
                    SELECT 
                        e.E01F01, 
                        e.E01F02, 
                        e.E01F03, 
                        e.E01F04, 
                        d.D01F02 AS DepartmentName
                    FROM YME01 e
                    INNER JOIN YMD01 d ON e.E01F04 = d.D01F01");

                DataTable data = ConvertToDataTable(employees);
                return new Response
                {
                    IsError = false,
                    Message = "Data retrieved successfully.",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }

        /// <summary>
        /// Converts a generic List to a DataTable.
        /// Uses reflection to get properties and values from the objects.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="data">The list of objects to convert.</param>
        /// <returns>A DataTable containing the data from the list.</returns>
        private DataTable ConvertToDataTable<T>(List<T> data)
        {
            var dataTable = new DataTable(typeof(T).Name);
            var properties = typeof(T).GetProperties();

            // Add columns
            foreach (var prop in properties)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            // Add rows
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
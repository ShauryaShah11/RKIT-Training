using ServiceStack.OrmLite;
using StackOrmConsole.DTO;
using StackOrmConsole.Models;
using StackOrmConsole.POCO;
using System;
using System.Configuration;
using System.Data;

namespace StackOrmConsole.Services
{
    /// <summary>
    /// The EMP01Service class provides services to manage EMP01 entities in the database. 
    /// Handles operations like adding, updating, and deleting employee records.
    /// </summary>
    public class EMP01Service
    {
        private readonly IDbConnection _db;

        /// <summary>
        /// Initializes a new instance of the EMP01Service class.
        /// Establishes a database connection and ensures the EMP01 table exists.
        /// </summary>
        public EMP01Service()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
            OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
            _db = dbFactory.Open();

            // Automatically create the table based on the POCO class
            _db.CreateTableIfNotExists<EMP01>();
        }

        /// <summary>
        /// Handles employee operations (Add, Edit, or Delete) based on user input and DTO data.
        /// </summary>
        /// <param name="dto">The DTO containing employee data.</param>
        /// <returns>A response object indicating the outcome of the operation.</returns>
        public Response Post(DTOEMP01 dto)
        {
            int entryType;
            Console.WriteLine("Enter Entry Type: \n1.ADD\n2.EDIT\n3.DELETE");
            entryType = int.Parse(Console.ReadLine());
            try
            {
                // Convert DTO to POCO object
                EMP01 poco = PreSave(dto);

                // Validate the data before saving
                Response validationResponse = ValidateOnSave(poco, entryType);
                if (validationResponse.IsError)
                {
                    return validationResponse;
                }

                // Perform the save operation
                return Save(poco, entryType);
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }

        /// <summary>
        /// Converts a DTO object into a POCO object.
        /// </summary>
        /// <param name="dto">The DTO containing employee data.</param>
        /// <returns>A POCO object of type EMP01.</returns>
        public EMP01 PreSave(DTOEMP01 dto)
        {
            return new EMP01
            {
                P01F01 = dto.P01101,
                P02F02 = dto.P02102,
                P04F04 = dto.P04104,
                P03F03 = DateTime.Now
            };
        }

        /// <summary>
        /// Validates the EMP01 data before saving it to the database.
        /// </summary>
        /// <param name="poco">The EMP01 POCO object to validate.</param>
        /// <param name="entryType">The operation type (Add, Edit, or Delete).</param>
        /// <returns>A response object indicating the validation result.</returns>
        public Response ValidateOnSave(EMP01 poco, int entryType)
        {
            if (entryType == 1 && _db.Exists<EMP01>(x => x.P01F01 == poco.P01F01))
            {
                return new Response { IsError = true, Message = "Employee already exists." };
            }
            if (entryType == 1 && !_db.Exists<DEPT01>(d => d.P01F01 == poco.P04F04))
            {
                return new Response { IsError = true, Message = "Department does not exist." };
            }
            if ((entryType == 2 || entryType == 3) && !_db.Exists<EMP01>(x => x.P01F01 == poco.P01F01))
            {
                return new Response { IsError = true, Message = "Employee not found." };
            }

            return new Response { Message = "Validation successful." };
        }

        /// <summary>
        /// Performs the requested database operation (Add, Edit, or Delete) for the employee.
        /// </summary>
        /// <param name="poco">The EMP01 POCO object to save.</param>
        /// <param name="entryType">The operation type (Add, Edit, or Delete).</param>
        /// <returns>A response object indicating the outcome of the operation.</returns>
        public Response Save(EMP01 poco, int entryType)
        {
            switch (entryType)
            {
                case 1:
                    _db.Insert(poco);
                    return new Response { IsError = false, Message = "Employee added successfully." };

                case 2:
                    _db.Update(poco);
                    return new Response { IsError = false, Message = "Employee updated successfully." };

                case 3:
                    _db.Delete<EMP01>(x => x.P01F01 == poco.P01F01);
                    return new Response { IsError = false, Message = "Employee deleted successfully." };

                default:
                    return new Response { IsError = true, Message = "Invalid entry type." };
            }
        }
    }
}

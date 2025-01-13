using ServiceStack.OrmLite;
using StackOrmConsole.DTO;
using StackOrmConsole.Models;
using StackOrmConsole.POCO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace StackOrmConsole.Services
{
    /// <summary>
    /// The DEPT01Service class Provides services to manage DEPT01 entities in the database. 
    /// Handles operations like adding, updating, and deleting department records.
    /// </summary>
    public class DEPT01Service
    {

        private readonly IDbConnection _db;
        /// <summary>
        /// Initializes new Instance of DEPT01Service Class.
        /// It Establish connection and make sure table exist in database.
        /// </summary>
        public DEPT01Service()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
            OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
            _db = dbFactory.Open();

            // Automatically create the table based on the POCO class
            _db.CreateTableIfNotExists<DEPT01>();
        }

        /// <summary>
        /// Handles the department operation (Add, Edit, or Delete) based on user input and DTO data.
        /// </summary>
        /// <param name="dto">The DTO containing department data.</param>
        /// <returns>Returns Reponse object.</returns>
        public Response Post(DTODEPT01 dto)
        {
            int entryType;
            Console.WriteLine("Enter Entry Type: \n1.ADD\n2.EDIT\n3.DELETE");
            entryType = int.Parse(Console.ReadLine());
            try
            {
                DEPT01 poco = PreSave(dto);
                Response validationResponse = ValidateOnSave(poco, entryType);
                if (validationResponse.IsError)
                {
                    return validationResponse;
                }
                return Save(poco, entryType);

            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }        

        /// <summary>
        /// Convert DTO object into POCO object.
        /// </summary>
        /// <param name="dto">The DTO Containing department data.</param>
        /// <returns>Returns POCO object of DEPT01 object.</returns>
        public DEPT01 PreSave(DTODEPT01 dto)
        {
            return new DEPT01
            {
                P01F01 = dto.P01101,
                P02F02 = dto.P02102,
                P03F03 = dto.P03103,
                P04F04 = dto.P04104
            };
        }

        /// <summary>
        /// Validates the DEPT01 data before saving it to the database.
        /// </summary>
        /// <param name="poco">The POCO Object DEPT01 to validate.</param>
        /// <param name="entryType">The Operation type.</param>
        /// <returns>indicating the validation result.</returns>
        public Response ValidateOnSave(DEPT01 poco, int entryType)
        {
            if (entryType == 1 && _db.Exists<DEPT01>(d => d.P02F02 == poco.P02F02 || d.P01F01 == poco.P01F01))
            {
                return new Response { IsError = true, Message = "Department already exist." };
            }
            if ((entryType == 2 || entryType == 3) && !_db.Exists<DEPT01>(d => d.P01F01 == poco.P01F01))
            {
                return new Response { IsError = true, Message = "Department not exist." };
            }
            return new Response { Message = "Validation successful." };
        }

        /// <summary>
        /// Performs the requested database operation (Add, Edit, or Delete) for the department.
        /// </summary>
        /// <param name="poco">The POCO Object DEPT01 to save.</param>
        /// <param name="entryType">The Operation type.</param>
        /// <returns>indicating validation result.</returns>
        public Response Save(DEPT01 poco, int entryType)
        {
            switch (entryType)
            {
                case 1:
                    _db.Insert(poco);
                    return new Response { IsError = false, Message = "Department added succesfully." };

                case 2:
                    _db.Update(poco);
                    return new Response { IsError = false, Message = "Department updated succesfully." };

                case 3:
                    _db.Delete<DEPT01>(x => x.P01F01 == poco.P01F01);
                    return new Response { IsError = false, Message = "Department deleted succesfully." };

                default:
                    return new Response { IsError = true, Message = "Invalid entry type." };
            }
        }

        public Response GetData()
        {
            var data = _db.Select<DEPT01>();

            return new Response
            {
                IsError = false,
                Message = "Data retrieved successfully.",
                Data = ConvertToDataTable(data)
            };
            
        }

        /// <summary>
        /// Converts a list of objects to a DataTable.
        /// </summary>
        /// <typeparam name="T">The type of the objects in the list.</typeparam>
        /// <param name="data">The list of objects to convert.</param>
        /// <returns>A DataTable representation of the list.</returns>
        private DataTable ConvertToDataTable<T>(List<T> data)
        {
            var dataTable = new DataTable(typeof(T).Name);

            // Get all the properties of the type T
            var properties = typeof(T).GetProperties();

            // Create a column for each property
            foreach (var prop in properties)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            // Add rows for each object in the list
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

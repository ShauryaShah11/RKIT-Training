using AdvanceC_FinalDemo.Enums;
using AdvanceC_FinalDemo.Extensions;
using AdvanceC_FinalDemo.Models;
using AdvanceC_FinalDemo.Models.DTO;
using AdvanceC_FinalDemo.Models.POCO;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Configuration;
using System.Data;
using System.Linq;

namespace AdvanceC_FinalDemo.Repositories
{
    /// <summary>
    /// Repository class for performing CRUD operations on books in the database.
    /// Uses ServiceStack ORM Lite for database operations.
    /// </summary>
    public class BookRepository
    {
        private readonly IDbConnection _db;

        public EnmOperationType type;

        static string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        // Initialize the OrmLiteConnectionFactory with the connection string and provider
        OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

        /// <summary>
        /// Initializes the BookRepository class and creates the book table if it doesn't exist.
        /// </summary>
        public BookRepository()
        {
            _db = dbFactory.Open();

            // Creates the table if it doesn't exist
            _db.CreateTableIfNotExists<YMM01>();
            _db.CreateTableIfNotExists<YMB01>();
            _db.CreateTableIfNotExists<YMH01>();
        }

        /// <summary>
        /// Retrieves all books from the database.
        /// </summary>
        /// <returns>A response containing a DataTable of all books.</returns>
        public Response GetAllBooks()
        {
            try
            {
                var booksList = _db.Select<YMB01>();
                DataTable bookDataTable = booksList.ToDataTable();

                return new Response { IsError = false, Data = bookDataTable, Message = "Book retrieved successfully." };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }

        /// <summary>
        /// Retrieves a specific book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book.</param>
        /// <returns>A response containing the DataTable for the specified book.</returns>
        public Response GetBookById(int id)
        {
            try
            {
                // Fetch a book by its ID and convert to DataTable
                var books = _db.Select<YMB01>().Where(b => b.B01F01 == id);
                DataTable bookDataTable = books.ToDataTable();
                return new Response { IsError = false, Data = bookDataTable, Message = "Book retrieved successfully." };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }

        /// <summary>
        /// Retrieves books by category.
        /// </summary>
        /// <param name="category">The category of books.</param>
        /// <returns>A response containing a DataTable of books in the specified category.</returns>
        public Response GetBookByCategory(string category)
        {
            try
            {
                // Fetch books by category and convert to DataTable
                var books = _db.Select<YMB01>().Where(b => b.B01F04.Contains(category));
                DataTable bookDataTable = books.ToDataTable();
                return new Response { IsError = false, Data = bookDataTable, Message = "Book retrieved successfully." };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }

        /// <summary>
        /// Retrieves the total count of records (books) in the YMM01 table.
        /// The method executes a SQL query to count all rows in the YMM01 table and returns the count in the response.
        /// </summary>
        /// <returns>A Response object containing the total count of books or a default message in case of an error.</returns>
        public Response GetBookCount()
        {
            try
            {
                // The query returns a single value: the count of records in the table YMM01
                int count = _db.Scalar<int>("SELECT COUNT(*) FROM YMB01");

                return new Response { IsError = false, Message = $"Total Book count is : {count}" }; // Return the count of rows
            }
            catch (Exception ex)
            {
                // Handle any potential exceptions (e.g., database connectivity issues)
                return new Response { IsError = false, Message = $"Total Book count is : {0}" }; // Return the count of rows
            }
        }

        /// <summary>
        /// Prepares a YMB01 object for saving by mapping DTO data to the entity.
        /// </summary>
        /// <param name="dto">The DTO containing book data.</param>
        /// <returns>A YMB01 object to be saved.</returns>
        public YMB01 PreSave(DTOYMB01 dto)
        {
            return dto.ToPoco<YMB01>();
        }

        /// <summary>
        /// Prepares a YMB01 object for saving by mapping DTO data to the entity.
        /// </summary>
        /// <param name="dto">The DTO containing book data.</param>
        /// <returns>A YMB01 object to be saved.</returns>
        public YMB01 PreDelete(DTOYMB01 dto)
        {
            return dto.ToPoco<YMB01>();
        }

        /// <summary>
        /// Validates the data before saving the book based on the operation type.
        /// </summary>
        /// <param name="poco">The YMB01 object containing the book data.</param>
        /// <param name="type">The operation type (ADD, UPDATE, DELETE).</param>
        /// <returns>A response indicating if validation passed or failed.</returns>
        public Response ValidateOnSave(YMB01 poco)
        {
            if (type == EnmOperationType.ADD && _db.Exists<YMB01>(b => b.B01F01 == poco.B01F01))
            {
                return new Response { IsError = true, Message = "Book already exists." };
            }
            else if (type == EnmOperationType.UPDATE && !_db.Exists<YMB01>(b => b.B01F01 == poco.B01F01))
            {
                return new Response { IsError = true, Message = "Book not found." };
            }            

            return new Response { IsError = false };
        }

        /// <summary>
        /// Validates the data before saving the book based on the operation type.
        /// </summary>
        /// <param name="poco">The YMB01 object containing the book data.</param>
        /// <param name="type">The operation type (ADD, UPDATE, DELETE).</param>
        /// <returns>A response indicating if validation passed or failed.</returns>
        public Response ValidateOnDelete(YMB01 poco)
        {          
            bool isExist = _db.Exists<YMB01>(b => b.B01F01 == poco.B01F01);
            if(!isExist)
            {
                return new Response { IsError = true, Message = "Book not found." };
            }
            return new Response { IsError = false };
        }

        /// <summary>
        /// Saves a new, updated book record in the database.
        /// </summary>
        /// <param name="poco">The YMB01 object containing the book data.</param>
        /// <param name="type">The operation type (ADD, UPDATE, DELETE).</param>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Save(YMB01 poco)
        {
            try
            {
                if (type == EnmOperationType.ADD)
                {
                    // InsertOnly ensures a record is added only if a record with the same B01F01 doesn't already exist.
                    long result = _db.InsertOnly(poco, x => x.B01F01);

                    // Check if the insertion was successful
                    if (result > 0)
                    {
                        return new Response { IsError = false, Message = "Book added successfully." };
                    }
                    else
                    {
                        return new Response { IsError = true, Message = "Book already exists." };
                    }
                }
                else
                {                    
                    int result = _db.UpdateOnly(
                        () => new YMB01 { B01F02 = poco.B01F02, B01F03 = poco.B01F03, B01F04 = poco.B01F04, B01F05 = poco.B01F05},
                        where: x => x.B01F01 == poco.B01F01
                    );

                    // Check if the update was successful
                    if (result > 0)
                    {
                        return new Response { IsError = false, Message = "Book updated successfully." };
                    }
                    else
                    {
                        return new Response { IsError = true, Message = "Book not found to update." };
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the operation
                return new Response { IsError = true, Message = $"An error occurred: {ex.Message}" };
            }
        }


        /// <summary>
        /// Saves a new, updated, or deleted book record in the database.
        /// </summary>
        /// <param name="poco">The YMB01 object containing the book data.</param>
        /// <param name="type">The operation type (ADD, UPDATE, DELETE).</param>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Delete(YMB01 poco)
        {
            try
            {
                _db.Delete<YMB01>(b => b.B01F01 == poco.B01F01);
                return new Response { IsError = false, Message = "Book deleted successfully." };
                 
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = $"An error occurred: {ex.Message}" };
            }
        }
    }
}

using AdvanceC_FinalDemo.Enums;
using AdvanceC_FinalDemo.Extension_Methods;
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
                var books = _db.Select<YMB01>().Where(b => b.B04F04.Contains(category));
                DataTable bookDataTable = books.ToDataTable();
                return new Response { IsError = false, Data = bookDataTable, Message = "Book retrieved successfully." };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }

        /// <summary>
        /// Handles the book operation (add, update, delete) based on the provided operation type.
        /// </summary>
        /// <param name="dto">The DTO containing book data.</param>
        /// <param name="type">The type of operation to perform (ADD, UPDATE, DELETE).</param>
        /// <returns>A response with the result of the operation.</returns>
        public Response HandleOperation(DTOYMB01 dto, OperationType type)
        {
            var poco = PreSave(dto);
            var validationResponse = ValidateOnSave(poco, type);
            if (validationResponse.IsError)
            {
                return validationResponse;
            }
            return Save(poco, type);
        }

        /// <summary>
        /// Prepares a YMB01 object for saving by mapping DTO data to the entity.
        /// </summary>
        /// <param name="dto">The DTO containing book data.</param>
        /// <returns>A YMB01 object to be saved.</returns>
        public YMB01 PreSave(DTOYMB01 dto)
        {
            return new YMB01
            {
                B01F01 = dto.B01101,
                B02F02 = dto.B02102,
                B03F03 = dto.B03103,
                B04F04 = dto.B04104,
                B05F05 = dto.B05105,
                B06F06 = dto.B06106
            };
        }

        /// <summary>
        /// Validates the data before saving the book based on the operation type.
        /// </summary>
        /// <param name="poco">The YMB01 object containing the book data.</param>
        /// <param name="type">The operation type (ADD, UPDATE, DELETE).</param>
        /// <returns>A response indicating if validation passed or failed.</returns>
        public Response ValidateOnSave(YMB01 poco, OperationType type)
        {
            if (type == OperationType.ADD && _db.Exists<YMB01>(b => b.B01F01 == poco.B01F01))
            {
                return new Response { IsError = true, Message = "Book already exists." };
            }
            else if (type == OperationType.UPDATE && !_db.Exists<YMB01>(b => b.B01F01 == poco.B01F01))
            {
                return new Response { IsError = true, Message = "Book not found." };
            }
            else if (type == OperationType.DELETE && !_db.Exists<YMB01>(b => b.B01F01 == poco.B01F01))
            {
                return new Response { IsError = true, Message = "Book not found." };
            }

            return new Response { IsError = false };
        }

        /// <summary>
        /// Saves a new, updated, or deleted book record in the database.
        /// </summary>
        /// <param name="poco">The YMB01 object containing the book data.</param>
        /// <param name="type">The operation type (ADD, UPDATE, DELETE).</param>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Save(YMB01 poco, OperationType type)
        {
            try
            {
                if (poco == null)
                    return new Response { IsError = true, Message = "Invalid data provided." };

                switch (type)
                {
                    case OperationType.ADD:
                        _db.Insert(poco);
                        return new Response { IsError = false, Message = "Book added successfully." };

                    case OperationType.UPDATE:
                        _db.Update(poco);
                        return new Response { IsError = false, Message = "Book updated successfully." };

                    case OperationType.DELETE:
                        _db.Delete<YMB01>(b => b.B01F01 == poco.B01F01);
                        return new Response { IsError = false, Message = "Book deleted successfully." };

                    default:
                        return new Response { IsError = true, Message = "Invalid operation type." };
                }
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = $"An error occurred: {ex.Message}" };
            }
        }
    }
}

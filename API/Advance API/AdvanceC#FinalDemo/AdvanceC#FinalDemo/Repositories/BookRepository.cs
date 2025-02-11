using AdvanceC_FinalDemo.Enums;
using AdvanceC_FinalDemo.Extensions;
using AdvanceC_FinalDemo.Models;
using AdvanceC_FinalDemo.Models.DTO;
using AdvanceC_FinalDemo.Models.POCO;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Legacy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace AdvanceC_FinalDemo.Repositories
{
    /// <summary>
    /// Repository class for managing book-related CRUD operations using ServiceStack ORM Lite.
    /// </summary>
    public class BookRepository
    {
        /// <summary>
        /// Database connection instance.
        /// </summary>
        private readonly IDbConnection _db;

        /// <summary>
        /// Operation type for identifying the current CRUD operation (ADD, UPDATE, DELETE).
        /// </summary>
        public EnmOperationType type;

        /// <summary>
        /// Connection string to the database.
        /// </summary>
        static string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        /// <summary>
        /// OrmLiteConnectionFactory for creating and managing database connections.
        /// </summary>
        OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// Ensures that necessary tables are created if they do not exist.
        /// </summary>
        public BookRepository()
        {
            _db = dbFactory.Open();
            _db.CreateTableIfNotExists<YMM01>();
            _db.CreateTableIfNotExists<YMB01>();
            _db.CreateTableIfNotExists<YMH01>();
        }

        /// <summary>
        /// Retrieves all books from the database and returns them as a DataTable.
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
        /// <param name="id">The ID of the book to retrieve.</param>
        /// <returns>A response containing the book details in a DataTable.</returns>
        public Response GetBookById(int id)
        {
            try
            {
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
        /// Retrieves books by their category.
        /// </summary>
        /// <param name="category">The category of books to retrieve.</param>
        /// <returns>A response containing a DataTable of books in the specified category.</returns>
        public Response GetBookByCategory(string category)
        {
            try
            {
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
        /// Gets the total count of books in the database.
        /// </summary>
        /// <returns>A response containing the total book count.</returns>
        public Response GetBookCount()
        {
            try
            {
                int count = _db.Scalar<int>("SELECT COUNT(*) FROM YMB01");
                return new Response { IsError = false, Message = $"Total Book count is : {count}" };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = $"Total Book count is : {0}" };
            }
        }

        /// <summary>
        /// Converts a DTO object to a POCO object for saving.
        /// </summary>
        /// <param name="dto">The DTO object representing the book.</param>
        /// <returns>A POCO object for saving to the database.</returns>
        public YMB01 PreSave(DTOYMB01 dto)
        {
            return dto.ToPoco<YMB01>();
        }

        /// <summary>
        /// Converts a DTO object to a POCO object for deletion.
        /// </summary>
        /// <param name="dto">The DTO object representing the book.</param>
        /// <returns>A POCO object for deletion.</returns>
        public YMB01 PreDelete(DTOYMB01 dto)
        {
            return dto.ToPoco<YMB01>();
        }

        /// <summary>
        /// Validates the book data before performing a save operation based on the specified type.
        /// </summary>
        /// <param name="poco">The book object to validate.</param>
        /// <returns>A response indicating the validation result.</returns>
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
        /// Validates the book data before performing a delete operation.
        /// </summary>
        /// <param name="poco">The book object to validate.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response ValidateOnDelete(YMB01 poco)
        {
            bool isExist = _db.Exists<YMB01>(b => b.B01F01 == poco.B01F01);
            if (!isExist)
            {
                return new Response { IsError = true, Message = "Book not found." };
            }
            return new Response { IsError = false };
        }

        /// <summary>
        /// Saves a new or updated book record to the database.
        /// </summary>
        /// <param name="poco">The book object to save.</param>
        /// <returns>A response indicating the result of the save operation.</returns>
        public Response Save(YMB01 poco)
        {
            try
            {
                if (type == EnmOperationType.ADD)
                {
                    long result = _db.InsertOnly(poco, x => x.B01F01);
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
                        () => new YMB01 { B01F02 = poco.B01F02, B01F03 = poco.B01F03, B01F04 = poco.B01F04, B01F05 = poco.B01F05 },
                        where: x => x.B01F01 == poco.B01F01
                    );
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
                return new Response { IsError = true, Message = $"An error occurred: {ex.Message}" };
            }
        }

        /// <summary>
        /// Deletes a book record from the database.
        /// </summary>
        /// <param name="poco">The book object to delete.</param>
        /// <returns>A response indicating the result of the delete operation.</returns>
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

        /// <summary>
        /// Demonstrates various ORM Select operations for the Book entity
        /// </summary>
        public Response TestORMSelectCommand(DTOYMB01 dto)
        {
            try
            {
                YMB01 poco = PreSave(dto);
                var results = new DataTable();

                // 1. Select All Books
                var allBooks = _db.Select<YMB01>();

                // 2. Select Single Book by ID
                var bookById = _db.SingleById<YMB01>(poco.B01F01);

                // 3. Select Book with Lambda Expression
                var booksByAuthor = _db.Select<YMB01>(x => x.B01F02 == poco.B01F02);

                // 4. Select with Multiple Conditions
                var specificBooks = _db.Select<YMB01>(x =>
                    x.B01F04 == poco.B01F04 &&
                    x.B01F06 >= 2000);

                // 5. Select with LIKE Operations
                var booksWithNamePattern = _db.Select<YMB01>(x => x.B01F03.Contains(poco.B01F03));
                var booksStartWith = _db.Select<YMB01>(x => x.B01F03.StartsWith("The"));

                // 6. Select with IN Clause
                var categories = new[] { "Fiction", "Science", "History" };
                var booksInCategories = _db.Select<YMB01>(x => Sql.In(x.B01F04, categories));

                // 7. Select with Ordering
                var q = _db.From<YMB01>()
                         .OrderByDescending(x => x.B01F06)
                         .Take(5);
                var latestBooks = _db.Select(q);

                // 8. Select with Aggregates
                var totalBooks = _db.Count<YMB01>(x => x.B01F05 > 0);
                var distinctCategories = _db.ColumnDistinct<string>(
                    _db.From<YMB01>()
                       .Select(x => x.B01F04)
                );

                // 9. Select with GroupBy
                var booksByCategory = _db.Dictionary<string, int>(
                    _db.From<YMB01>()
                       .GroupBy(x => x.B01F04)
                       .Select(x => new
                       {
                           Category = x.B01F04,
                           Count = Sql.Count("*")
                       })
                );

                // 10. Select with Complex Conditions
                var complexQuery = _db.From<YMB01>()
                    .Where(x => x.B01F06 >= 2000)
                    .And(x => x.B01F05 > 0)
                    .OrderBy(x => x.B01F06)
                    .ThenBy(x => x.B01F03)
                    .Select(x => new
                    {
                        x.B01F03,
                        x.B01F02,
                        x.B01F04,
                        x.B01F06
                    });
                var complexResults = _db.Select(complexQuery);

                // 11. Scalar Operations
                var oldestBook = _db.Scalar<YMB01, int>(x => Sql.Min(x.B01F06));
                var totalCopies = _db.Scalar<YMB01, int>(x => Sql.Sum(x.B01F05));

                // 12. Existence Check
                var hasRareBooks = _db.Exists<YMB01>(x => x.B01F05 == 1);

                // 13. Lazy Loading
                var lazyBooks = _db.SelectLazy<YMB01>();

                // 14. Column Selection
                var bookTitles = _db.Column<string>(
                    _db.From<YMB01>()
                       .Select(x => x.B01F03)
                );

                // 15. Lookup (Dictionary with grouped values)
                var booksByYear = _db.Lookup<int, string>(
                    _db.From<YMB01>()
                       .Select(x => new { x.B01F06, x.B01F03 })
                );

                // Combine results into a DataTable for return
                results = allBooks.ToDataTable();

                return new Response
                {
                    Data = results,
                    IsError = false,
                    Message = "ORM Select operations completed successfully."
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsError = true,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Demonstrates various ORM Insert operations for the Book entity (YMB01).
        /// </summary>
        public Response TestORMInsertCommand(DTOYMB01 dto)
        {
            try
            {
                YMB01 poco = PreSave(dto); // Pre-process DTO to POCO

                // 1. Basic Insert - Inserts the POCO and retrieves the inserted ID.
                var insertedId = _db.Insert(poco, selectIdentity: true);

                // 2. Insert multiple books at once using params.
                _db.Insert(
                    new YMB01 { B01F02 = "Author 1", B01F03 = "Book 1", B01F04 = "Fiction", B01F05 = 5, B01F06 = 2024 },
                    new YMB01 { B01F02 = "Author 2", B01F03 = "Book 2", B01F04 = "Non-Fiction", B01F05 = 3, B01F06 = 2023 }
                );

                // 3. Insert multiple books using InsertAll with a list of YMB01 objects.
                var booksToInsert = new List<YMB01>
            {
                new YMB01 { B01F02 = "Author 3", B01F03 = "Book 3", B01F04 = "Science", B01F05 = 10, B01F06 = 2024 },
                new YMB01 { B01F02 = "Author 4", B01F03 = "Book 4", B01F04 = "History", B01F05 = 7, B01F06 = 2022 }
            };
                _db.InsertAll(booksToInsert);

                // 4. InsertOnly - Insert specific fields only.
                _db.InsertOnly(() => new YMB01 { B01F02 = "Author 5", B01F03 = "Book 5" });

                // 5. InsertOnly with explicit field selection.
                _db.InsertOnly(new YMB01 { B01F02 = "Author 6", B01F03 = "Book 6", B01F04 = "Biography", B01F05 = 4, B01F06 = 2024 },
                              x => new { x.B01F02, x.B01F03, x.B01F04 });

                // 6. Insert using SqlExpression to insert specific fields.
                var q = _db.From<YMB01>().Insert(p => new { p.B01F02, p.B01F03, p.B01F04 });
                _db.InsertOnly(new YMB01 { B01F02 = "Author 7", B01F03 = "Book 7", B01F04 = "Mystery" }, onlyFields: q);

                //// 7. Insert by Dictionary - Convert POCO to a dictionary and modify it before inserting.
                //var bookDict = poco.ToObjectDictionary();
                //bookDict["B01F05"] = 1; // Set available copies to 1.
                //var dictInsertId = _db.Insert<YMB01>(bookDict, selectIdentity: true);

                // 8. Save API - Handles both insert and update operations.
                var newBook = new YMB01 { B01F02 = "Author 8", B01F03 = "Book 8", B01F04 = "Fantasy", B01F05 = 6, B01F06 = 2024 };
                _db.Save(newBook); // Performs insert since it's a new record.

                // 9. Bulk Insert with a loop to add multiple records.
                var bulkBooks = new List<YMB01>();
                for (int i = 9; i <= 11; i++)
                {
                    bulkBooks.Add(new YMB01 { B01F02 = $"Author {i}", B01F03 = $"Book {i}", B01F04 = "Fiction", B01F05 = 5, B01F06 = 2024 });
                }
                _db.InsertAll(bulkBooks);


                // Return a  response with details of inserted records.
                return new Response
                {
                    IsError = false,
                    Message = "All insert operations completed successfully.",
                };
            }
            catch (Exception ex)
            {
                // Return an error response with the exception message.
                return new Response
                {
                    IsError = true,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Demonstrates various ORM Update operations for the Book entity (YMB01).
        /// </summary>
        public Response TestORMUpdateCommand(DTOYMB01 dto)
        {
            try
            {
                YMB01 poco = PreSave(dto); // Pre-process DTO to POCO

                // 1. Update the entire object (excluding the Id, which is used as the filter).
                poco.B01F02 = "Updated Author";
                poco.B01F03 = "Updated Book";
                poco.B01F04 = "Updated Genre";
                _db.Update(poco); // This updates all fields except the Id

                // 2. Update only specific fields using an anonymous type.
                _db.Update<YMB01>(new { B01F02 = "New Author", B01F03 = "New Book" }, p => p.B01F01 == poco.B01F01);

                // 3. Update with filters using `UpdateOnly` for partial updates.
                _db.UpdateOnly(() => new YMB01 { B01F05 = 10 }, where: x => x.B01F01 == poco.B01F01);

                // 4. Update multiple records (bulk update).
                var booksToUpdate = _db.Select<YMB01>(x => x.B01F06 < 2024); // Get books from previous years
                booksToUpdate.Each(x => x.B01F05 = 3); // Update the number of copies to 3 for each record
                _db.Update(booksToUpdate[0], booksToUpdate[1]); // Update by params

                // 5. Update using UpdateAdd to increment a field (example for increasing B01F05).
                _db.UpdateAdd(() => new YMB01 { B01F05 = 2 }, x => x.B01F06 == 2024); // Increment B01F05 by 2 for all books in 2024

                // 6. Update by Dictionary - Convert POCO to dictionary and update.
                var bookDict = poco.ToObjectDictionary();
                bookDict[nameof(YMB01.B01F03)] = "Updated Book Name"; // Update the book name
                _db.Update<YMB01>(bookDict, p => p.B01F01 == poco.B01F01); // Update using dictionary

                // 7. Update using a typed SQL Expression for more control.
                var q = _db.From<YMB01>()
                    .Where(x => x.B01F02 == "Author 1")
                    .Update(p => p.B01F03); // Update only the B01F03 field for matching records
                _db.UpdateOnlyFields(new YMB01 { B01F03 = "Updated Book Name" }, onlyFields: q);

                // 8. Save API - Handles both insert and update.
                var existingBook = _db.SingleById<YMB01>(poco.B01F01); // Find the existing record
                if (existingBook != null)
                {
                    existingBook.B01F02 = "Updated Author Name";
                    _db.Save(existingBook); // This performs the update operation
                }

                // Return a successful response
                return new Response
                {
                    IsError = false,
                    Message = "All update operations completed successfully.",
                };
            }
            catch (Exception ex)
            {
                // Return an error response with the exception message.
                return new Response
                {
                    IsError = true,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }


        //public Response TestORMDeleteCommand(DTOYMB01 dto)
        //{
        //    try
        //    {
        //        YMB01 poco = PreSave(dto);

        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response { IsError = true, Message = $"An error occurred: {ex.Message}" };
        //    }
        //}


    }
}

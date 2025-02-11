using System;
using System.Data;
using System.Linq;
using System.Configuration;
using MySql.Data.MySqlClient;
using ServiceStack.OrmLite;
using AdvanceC_FinalDemo.Models.DTO;
using AdvanceC_FinalDemo.Models.POCO;
using AdvanceC_FinalDemo.Models;
using AdvanceC_FinalDemo.Extensions;

namespace AdvanceC_FinalDemo.Repositories
{
    /// <summary>
    /// Represents a book history record containing information about book loans to members
    /// </summary>
    public class BookHistory
    {
        public string BookName { get; set; }
        public string MemberName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; } // Nullable DateTime for the return date
    }

    /// <summary>
    /// Repository class for managing book history records in the library system.
    /// Handles CRUD operations for book loans and returns.
    /// </summary>
    public class BookHistoryRepository : IDisposable
    {
        private readonly IDbConnection _db;
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the BookHistoryRepository class.
        /// Sets up the database connection using the connection string from configuration.
        /// </summary>
        public BookHistoryRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
            var dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
            _db = dbFactory.Open();
        }

        /// <summary>
        /// Retrieves all book history records from the database.
        /// </summary>
        /// <returns>A Response object containing a DataTable with all book history records or error information if the operation fails.</returns>
        public Response GetAllBookHistory()
        {
            try
            {
                string query = string.Format(@"
                                    SELECT 
                                        ymb.B01F03 AS BookName, 
                                        ymm.M01F02 AS MemberName, 
                                        ymh.H01F03 AS IssueDate, 
                                        ymh.H01F04 AS ReturnDate
                                    FROM 
                                        YMH01 ymh
                                    JOIN 
                                        YMM01 ymm ON ymm.M01F01 = ymh.H01F02
                                    JOIN 
                                        YMB01 ymb ON ymb.B01F01 = ymh.H01F01");

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = query;

                        DataTable dataTable = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }

                        return new Response
                        {
                            Data = dataTable,
                            IsError = false,
                            Message = dataTable.Rows.Count > 0 ? "Data retrieved successfully." : "No records found."
                        };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new Response { Data = null, IsError = true, Message = $"Database error: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new Response { Data = null, IsError = true, Message = $"Error: {ex.Message}" };
            }
        }

        /// <summary>
        /// Retrieves book history records for a specific book and member combination.
        /// </summary>
        /// <param name="bookId">The ID of the book to search for.</param>
        /// <param name="memberId">The ID of the member to search for.</param>
        /// <returns>A Response object containing the matching book history records or error information if the operation fails.</returns>
        public Response GetBookHistoryById(int bookId, int memberId)
        {
            if (bookId <= 0 || memberId <= 0)
            {
                return new Response
                {
                    Data = null,
                    IsError = true,
                    Message = "Invalid book ID or member ID provided."
                };
            }

            try
            {
                var query = _db.Select(_db.From<YMH01>()
                    .Join<YMH01, YMM01>((ymh, ymm) => ymh.H01F02 == ymm.M01F01)
                    .Join<YMH01, YMB01>((ymh, ymb) => ymh.H01F01 == ymb.B01F01)
                    .Where<YMH01>(ymh => ymh.H01F01 == bookId && ymh.H01F02 == memberId)
                    .Select<YMH01, YMM01, YMB01>((ymh, ymm, ymb) => new
                    {
                        BookName = ymb.B01F03,
                        MemberName = ymm.M01F02,
                        IssueDate = ymh.H01F03,
                        ReturnDate = ymh.H01F04
                    }));

                return new Response
                {
                    Data = query.ConvertToDataTable(),
                    IsError = false,
                    Message = "Data retrieved successfully."
                };
            }
            catch (MySqlException ex)
            {
                return new Response
                {
                    Data = null,
                    IsError = true,
                    Message = $"Database error: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Data = null,
                    IsError = true,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Retrieves all book history records where the book has not been returned yet.
        /// </summary>
        /// <returns>A Response object containing unreturned book records or error information if the operation fails.</returns>
        public Response GetUnreturnedBookHistory()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    string query = string.Format(@"
                            SELECT 
                                ymb.B01F03 AS BookName, 
                                ymm.M01F02 AS MemberName, 
                                ymh.H01F03 AS IssueDate, 
                                ymh.H01F04 AS ReturnDate
                            FROM 
                                YMH01 ymh
                            JOIN 
                                YMM01 ymm ON ymm.M01F01 = ymh.H01F02
                            JOIN 
                                YMB01 ymb ON ymb.B01F01 = ymh.H01F01
                            WHERE 
                                ymh.H01F04 IS NULL");
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = query;

                        var dataTable = new DataTable();
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }

                        return new Response
                        {
                            Data = dataTable,
                            IsError = false,
                            Message = dataTable.Rows.Count > 0 ? "Data retrieved successfully." : "No unreturned books found."
                        };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new Response { Data = null, IsError = true, Message = $"Database error: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new Response { Data = null, IsError = true, Message = $"Error: {ex.Message}" };
            }
        }

        /// <summary>
        /// Adds a new book history record to the database.
        /// </summary>
        /// <param name="dto">The data transfer object containing the book history information to add.</param>
        /// <returns>A Response object indicating success or failure of the operation.</returns>
        public Response AddBookHistoryRecord(DTOYMH01 dto)
        {
            try
            {
                YMH01 poco = PreSave(dto);

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = @"
                            INSERT INTO YMH01
                            (H01F01, H01F02, H01F03, H01F04) 
                            VALUES 
                            (@bookId, @memberId, @issueDate, @returnDate)";

                        cmd.Parameters.AddWithValue("@bookId", poco.H01F01);
                        cmd.Parameters.AddWithValue("@memberId", poco.H01F02);
                        cmd.Parameters.AddWithValue("@issueDate", poco.H01F03);
                        cmd.Parameters.AddWithValue("@returnDate",
                            poco.H01F04.HasValue ? (object)poco.H01F04.Value : DBNull.Value);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return new Response
                        {
                            Message = $"{rowsAffected} row(s) inserted successfully.",
                            IsError = false
                        };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new Response { Message = $"Database error: {ex.Message}", IsError = true };
            }
            catch (Exception ex)
            {
                return new Response { Message = $"Error: {ex.Message}", IsError = true };
            }
        }

        /// <summary>
        /// Updates an existing book history record in the database.
        /// </summary>
        /// <param name="dto">The data transfer object containing the updated book history information.</param>
        /// <param name="bookId">The ID of the book to update.</param>
        /// <param name="memberId">The ID of the member to update.</param>
        /// <returns>A Response object indicating success or failure of the operation.</returns>
        public Response UpdateBookHistoryRecord(DTOYMH01 dto, int bookId, int memberId)
        {
            if (bookId <= 0 || memberId <= 0)
            {
                return new Response
                {
                    Message = "Invalid book ID or member ID provided.",
                    IsError = true
                };
            }
            try
            {
                YMH01 poco = PreSave(dto);

                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = @"
                            UPDATE YMH01 
                            SET 
                                H01F03 = @issueDate, 
                                H01F04 = @returnDate
                            WHERE 
                                H01F01 = @bookId AND H01F02 = @memberId";

                        cmd.Parameters.AddWithValue("@bookId", bookId);
                        cmd.Parameters.AddWithValue("@memberId", memberId);
                        cmd.Parameters.AddWithValue("@issueDate", poco.H01F03);
                        cmd.Parameters.AddWithValue("@returnDate",
                            poco.H01F04.HasValue ? (object)poco.H01F04.Value : DBNull.Value);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return new Response
                        {
                            Message = $"{rowsAffected} row(s) updated successfully.",
                            IsError = false
                        };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new Response { Message = $"Database error: {ex.Message}", IsError = true };
            }
            catch (Exception ex)
            {
                return new Response { Message = $"Error: {ex.Message}", IsError = true };
            }
        }

        /// <summary>
        /// Deletes a book history record from the database.
        /// </summary>
        /// <param name="bookId">The ID of the book to delete.</param>
        /// <param name="memberId">The ID of the member to delete.</param>
        /// <returns>A Response object indicating success or failure of the operation.</returns>
        public Response DeleteBookHistoryRecord(int bookId, int memberId)
        {
            if (bookId <= 0 || memberId <= 0)
            {
                return new Response
                {
                    Message = "Invalid book ID or member ID provided.",
                    IsError = true
                };
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = @"
                            DELETE FROM YMH01
                            WHERE H01F01 = @bookId AND H01F02 = @memberId";

                        cmd.Parameters.AddWithValue("@bookId", bookId);
                        cmd.Parameters.AddWithValue("@memberId", memberId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return new Response
                        {
                            Message = $"{rowsAffected} row(s) deleted successfully.",
                            IsError = false
                        };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new Response { Message = $"Database error: {ex.Message}", IsError = true };
            }
            catch (Exception ex)
            {
                return new Response { Message = $"Error: {ex.Message}", IsError = true };
            }
        }

        /// <summary>
        /// Converts a DTO to a POCO before saving to the database.
        /// </summary>
        /// <param name="dto">The DTO to convert.</param>
        /// <returns>The converted POCO object.</returns>
        private YMH01 PreSave(DTOYMH01 dto)
        {
            return dto.ToPoco<YMH01>();
        }

        /// <summary>
        /// Disposes of the database connection.
        /// </summary>
        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
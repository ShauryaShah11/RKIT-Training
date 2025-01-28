using AdvanceC_FinalDemo.Models;
using AdvanceC_FinalDemo.Models.DTO;
using AdvanceC_FinalDemo.Models.POCO;
using MySql.Data.MySqlClient;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.Linq;

namespace AdvanceC_FinalDemo.Repositories
{
    public class BookHistoryRepository
    {
        /// <summary>
        /// Database Connection String.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookHistoryRepository"/> class
        /// and sets the database connection string.
        /// </summary>
        public BookHistoryRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
        }

        /// <summary>
        /// Retrieves all book history records.
        /// </summary>
        public Response GetAllBookHistory()
        {
            List<dynamic> histories = new List<dynamic>();

            const string sql = @"
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
                                YMB01 ymb ON ymb.B01F01 = ymh.H01F01";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    using (MySqlDataReader dbReader = cmd.ExecuteReader())
                    {
                        while (dbReader.Read())
                        {
                            dynamic obj = new ExpandoObject(); // Use ExpandoObject for dynamic property assignment
                            obj.BookName = dbReader.GetString("BookName");
                            obj.MemberName = dbReader.GetString("MemberName");
                            obj.IssueDate = dbReader.GetDateTime("IssueDate");
                            obj.ReturnDate = dbReader.GetDateTime("ReturnDate");
                            histories.Add(obj);
                        }
                    }
                }

                // Convert List<dynamic> to DataTable
                DataTable historyTable = ConvertToDataTable(histories);
                return new Response { Data = historyTable, IsError = false, Message = "Data retrieved successfully." };
            }
            catch (MySqlException ex)
            {
                return new Response { Data = null, IsError = true, Message = $"MySQL Error: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new Response { Data = null, IsError = true, Message = $"Error: {ex.Message}" };
            }
        }

        /// <summary>
        /// Retrieves book history for a specific book and member based on their IDs.
        /// </summary>
        public Response GetBookHistoryById(int bookId, int memberId)
        {
            List<dynamic> histories = new List<dynamic>();

            const string sql = @"
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
                            ymh.H01F01 = @bookId
                                AND ymh.H01F02 = @memberId";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@bookId", bookId);
                        cmd.Parameters.AddWithValue("@memberId", memberId);

                        using (MySqlDataReader dbReader = cmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                dynamic obj = new ExpandoObject(); // Use ExpandoObject for dynamic property assignment
                                obj.BookName = dbReader.GetString(0);
                                obj.MemberName = dbReader.GetString(1);
                                obj.IssueDate = dbReader.GetDateTime(2);
                                obj.ReturnDate = dbReader.IsDBNull(3) ? (DateTime?)null : dbReader.GetDateTime(3);
                                histories.Add(obj);
                            }
                        }
                    }
                }

                // Convert List<dynamic> to DataTable
                DataTable historyTable = ConvertToDataTable(histories);
                return new Response { Data = historyTable, IsError = false, Message = "Data retrieved successfully." };
            }
            catch (MySqlException ex)
            {
                return new Response { Data = null, IsError = true, Message = $"MySQL Error: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new Response { Data = null, IsError = true, Message = $"Error: {ex.Message}" };
            }
        }

        /// <summary>
        /// Retrieves book history for a specific book and member where the return date is not set.
        /// </summary>
        public Response GetUnreturnedBookHistory()
        {
            List<dynamic> histories = new List<dynamic>();

            const string sql = @"
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
                         WHERE ymh.H01F04 IS NULL";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader dbReader = cmd.ExecuteReader())
                        {
                            while (dbReader.Read())
                            {
                                dynamic obj = new ExpandoObject(); // Use ExpandoObject for dynamic property assignment
                                obj.BookName = dbReader["BookName"]?.ToString();
                                obj.MemberName = dbReader["MemberName"]?.ToString();
                                obj.IssueDate = dbReader["IssueDate"] != DBNull.Value ? dbReader.GetDateTime("IssueDate") : (DateTime?)null;
                                obj.ReturnDate = dbReader["ReturnDate"] != DBNull.Value ? dbReader.GetDateTime("ReturnDate") : (DateTime?)null;
                                histories.Add(obj);
                            }
                        }
                    }
                }

                // Convert List<dynamic> to DataTable
                DataTable historyTable = ConvertToDataTable(histories);
                return new Response { Data = historyTable, IsError = false, Message = "Data retrieved successfully." };
            }
            catch (MySqlException ex)
            {
                return new Response { Data = null, IsError = true, Message = $"MySQL Error: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new Response { Data = null, IsError = true, Message = $"Error: {ex.Message}" };
            }
        }

        /// <summary>
        /// Adds a new book history record to the database.
        /// </summary>
        public Response AddBookHistoryRecord(DTOYMH01 dto)
        {
            YMH01 poco = PreSave(dto);
            const string sql = "INSERT INTO YMH01(H01F01, H01F02, H01F03, H01F04) VALUES (@bookId, @memberId, @issueDate, @returnDate)";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@bookId", poco.H01F01);
                        cmd.Parameters.AddWithValue("@memberId", poco.H01F02);
                        cmd.Parameters.AddWithValue("@issueDate", poco.H01F03);
                        cmd.Parameters.AddWithValue("@returnDate", poco.H01F04);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        string message = $"{rowsAffected} row(s) inserted successfully.";
                        return new Response { Message = message, IsError = false };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new Response { Message = $"Database error: {ex.Message}", IsError = true };
            }
            catch (Exception ex)
            {
                return new Response { Message = ex.Message, IsError = true };
            }
        }

        /// <summary>
        /// Updates an existing book history record in the database.
        /// </summary>
        public Response UpdateBookHistoryRecord(DTOYMH01 dto, int bookId, int memberId)
        {
            YMH01 poco = PreSave(dto); // Assuming PreSave is creating a YMH01 object from DTO.

            // Define the UPDATE SQL query.
            const string sql = @"
                    UPDATE YMH01 
                    SET 
                        H01F03 = @issueDate, 
                        H01F04 = @returnDate
                    WHERE 
                        H01F01 = @bookId AND H01F02 = @memberId";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Add parameters to the query.
                        cmd.Parameters.AddWithValue("@bookId", bookId);  // Book ID (part of composite PK)
                        cmd.Parameters.AddWithValue("@memberId", memberId); // Member ID (part of composite PK)
                        cmd.Parameters.AddWithValue("@issueDate", poco.H01F03);  // Issue Date
                        cmd.Parameters.AddWithValue("@returnDate", poco.H01F04 ?? (object)DBNull.Value);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        string message = $"{rowsAffected} row(s) updated successfully.";
                        return new Response { Message = message, IsError = false };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new Response { Message = $"MySQL Error: {ex.Message}", IsError = true };
            }
            catch (Exception ex)
            {
                return new Response { Message = $"Error: {ex.Message}", IsError = true };
            }
        }

        /// <summary>
        /// Deletes a book history record from the database.
        /// </summary>
        public Response DeleteBookHistoryRecord(int bookId, int memberId)
        {
            const string sql = @"
                    DELETE FROM YMH01
                    WHERE 
                        H01F01 = @bookId AND H01F02 = @memberId";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@bookId", bookId);
                        cmd.Parameters.AddWithValue("@memberId", memberId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        string message = $"{rowsAffected} row(s) deleted successfully.";
                        return new Response { Message = message, IsError = false };
                    }
                }
            }
            catch (MySqlException ex)
            {
                return new Response { Message = $"MySQL Error: {ex.Message}", IsError = true };
            }
            catch (Exception ex)
            {
                return new Response { Message = $"Error: {ex.Message}", IsError = true };
            }
        }

        /// <summary>
        /// Converts a list of dynamic objects into a DataTable.
        /// </summary>
        private DataTable ConvertToDataTable(IEnumerable<dynamic> items)
        {
            DataTable dt = new DataTable();

            // Add columns dynamically
            if (items.Any())
            {
                var firstItem = items.First();
                foreach (var property in (IDictionary<string, object>)firstItem)
                {
                    dt.Columns.Add(property.Key);
                }

                // Add rows to DataTable
                foreach (var item in items)
                {
                    DataRow row = dt.NewRow();
                    foreach (var property in (IDictionary<string, object>)item)
                    {
                        row[property.Key] = property.Value ?? DBNull.Value;
                    }
                    dt.Rows.Add(row);
                }
            }

            return dt;
        }

        /// <summary>
        /// Prepares the BookHistory object before saving it.
        /// </summary>
        private YMH01 PreSave(DTOYMH01 dto)
        {
            return new YMH01
            {
                H01F01 = dto.H01101, // Book ID
                H01F02 = dto.H01102, // Member ID
                H01F03 = dto.H01103, // Issue Date
                H01F04 = dto.H01104
            };
        }
    }
}

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
    public class BookHistory
    {
        public string BookName { get; set; }
        public string MemberName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; } // Nullable DateTime for the return date
    }

    public class BookHistoryRepository : IDisposable
    {
        private readonly IDbConnection _db;
        private readonly string _connectionString;

        public BookHistoryRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
            var dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
            _db = dbFactory.Open();
        }

        public Response GetAllBookHistory()
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = @"
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

                        var dataTable = new DataTable();
                        using (var adapter = new MySqlDataAdapter(cmd))
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
                var query = _db.Select(_db.From<dynamic>()
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


        public Response GetUnreturnedBookHistory()
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = @"
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
                                ymh.H01F04 IS NULL";

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

        public Response AddBookHistoryRecord(DTOYMH01 dto)
        {
            try
            {
                var poco = PreSave(dto);

                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand())
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
                var poco = PreSave(dto);

                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand())
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
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand())
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

        private YMH01 PreSave(DTOYMH01 dto)
        {
            return dto.ToPoco<YMH01>();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
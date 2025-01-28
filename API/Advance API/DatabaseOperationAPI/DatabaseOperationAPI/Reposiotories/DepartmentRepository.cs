using DatabaseOperationAPI.Models;
using DatabaseOperationAPI.Models.POCO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace DatabaseOperationAPI.Repositories
{
    /// <summary>
    /// The DepartmentRepository contains methods for performing CRUD operation on department table.
    /// </summary>
    public class DepartmentRepository
    {
        /// <summary>
        /// Database Connection String.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentRepository"/> class
        /// and sets the database connection string.
        /// </summary>
        public DepartmentRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
        }

        /// <summary>
        /// Retrieves all department data from the database.
        /// </summary>
        /// <returns>A <see cref="Response"/> object containing department data or error details.</returns>
        public Response GetDepartmentData()
        {
            List<YMD01> departments = new List<YMD01>();
            const string sql = "SELECT * FROM YMD01";

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
                            YMD01 obj = new YMD01
                            {
                                D01F01 = dbReader.GetInt32(0),
                                D01F02 = dbReader.GetString(1),
                                D01F03 = dbReader.GetString(2),
                                D01F04 = dbReader.GetString(3)
                            };
                            departments.Add(obj);
                        }
                    }
                }

                DataTable departmentTable = ConvertToDataTable(departments);
                return new Response { data = departmentTable, IsError = false };
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("MySQL Error: " + ex.Message);
                return new Response { data = null, IsError = true, Message = ex.Message };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return new Response { data = null, IsError = true, Message = ex.Message };
            }
        }

        /// <summary>
        /// Retrieves department data for a specific ID.
        /// </summary>
        /// <param name="id">The unique identifier of the department.</param>
        /// <returns>A <see cref="Response"/> object containing department data or error details.</returns>
        public Response GetDepartmentById(int id)
        {
            List<YMD01> departments = new List<YMD01>();
            const string sql = "SELECT * FROM YMD01 WHERE D01F01 = @id";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Assign the parameter value
                        cmd.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader dbReader = cmd.ExecuteReader())
                        {
                            // Fetch and populate the list of departments
                            while (dbReader.Read())
                            {
                                YMD01 obj = new YMD01
                                {
                                    D01F01 = dbReader.GetInt32(0),
                                    D01F02 = dbReader.GetString(1),
                                    D01F03 = dbReader.GetString(2),
                                    D01F04 = dbReader.GetString(3)
                                };
                                departments.Add(obj);
                            }
                        }
                    }
                }

                // Check if any data was found
                if (departments.Count == 0)
                {
                    return new Response
                    {
                        data = null,
                        IsError = false,
                        Message = "No department found with the specified ID."
                    };
                }

                // Convert List to DataTable
                DataTable departmentTable = ConvertToDataTable(departments);

                return new Response
                {
                    data = departmentTable,
                    IsError = false
                };
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("MySQL Error: " + ex.Message);
                return new Response
                {
                    data = null,
                    IsError = true,
                    Message = $"Database error: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return new Response
                {
                    data = null,
                    IsError = true,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Adds a new department to the database.
        /// </summary>
        /// <param name="dept">An object representing the new department data.</param>
        public Response AddDepartment(YMD01 dept)
        {
            const string sql = "INSERT INTO YMD01 (D01F01, D01F02, D01F03, D01F04) VALUES (@id, @shortName, @deptName, @deptHead)";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", dept.D01F01);
                        cmd.Parameters.AddWithValue("@shortName", dept.D01F02);
                        cmd.Parameters.AddWithValue("@deptName", dept.D01F03);
                        cmd.Parameters.AddWithValue("@deptHead", dept.D01F04);

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
        /// Updates an existing department in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the department to update.</param>
        /// <param name="dept">An object representing the updated department data.</param>
        /// <returns>A <see cref="Response"/> object indicating success or failure of the operation.</returns>
        public Response UpdateDepartment(int id, YMD01 dept)
        {
            const string sql = "UPDATE YMD01 SET D01F02 = @shortName, D01F03 = @deptName, D01F04 = @deptHead WHERE D01F01 = @id";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@shortName", dept.D01F02);
                        cmd.Parameters.AddWithValue("@deptName", dept.D01F03);
                        cmd.Parameters.AddWithValue("@deptHead", dept.D01F04);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        string message = $"{rowsAffected} row(s) updated successfully.";
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
        /// Deletes a department from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the department to delete.</param>
        /// <returns>A <see cref="Response"/> object indicating success or failure of the operation.</returns>
        public Response DeleteDepartment(int id)
        {
            try
            {
                const string sql = "DELETE from YMD01 where D01F01 = @id";
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        string message = $"{rowsAffected} row(s) deleted successfully.";
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
        /// convert list of departments into data table format.
        /// </summary>
        /// <param name="departments">list of department object.</param>
        /// <returns>containing department data.</returns>
        private DataTable ConvertToDataTable(List<YMD01> departments)
        {
            DataTable table = new DataTable("YMD01");

            table.Columns.Add("D01F01", typeof(int));
            table.Columns.Add("D01F02", typeof(string));
            table.Columns.Add("D01F03", typeof(string));
            table.Columns.Add("D01F04", typeof(string));

            foreach (var dept in departments)
            {
                table.Rows.Add(dept.D01F01, dept.D01F02, dept.D01F03, dept.D01F04);
            }

            return table;
        }
    }
}

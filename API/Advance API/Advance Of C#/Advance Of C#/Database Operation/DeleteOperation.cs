using MySql.Data.MySqlClient;
using System.Configuration;
using System;

namespace Advance_Of_C_.Database_Operation
{
    public class DeleteOperation
    {
        // Connection string to connect to the MySQL database
        private readonly string _connectionString;

        // Constructor to initialize the connection string from the configuration file
        public DeleteOperation()
        {
            // Fetching the connection string from app settings (web.config or app.config)
            _connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
        }

        /// <summary>
        /// This method delete the department record based on the department ID.
        /// </summary>
        public void DeleteDemo()
        {
            int id = 2; // Department ID for which the name is being updated

            // SQL query with parameterized query to prevent SQL injection
            string sql = "DELETE FROM DEPT01 WHERE id = @id";

            try
            {
                // Establish a connection to the database
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // Create a MySQL command object with the query and connection
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Add parameters to the query to avoid SQL injection
                        cmd.Parameters.AddWithValue("@id", id);

                        // Execute the DELETE command and capture the number of affected rows
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check if any rows were affected and print a message
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"{rowsAffected} row(s) deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No rows were deleted.");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Catch and log any MySQL-specific exceptions
                Console.WriteLine($"MySQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Catch and log any general exceptions
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

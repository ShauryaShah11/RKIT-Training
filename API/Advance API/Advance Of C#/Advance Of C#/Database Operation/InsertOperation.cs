using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace Advance_Of_C_.Database_Operation
{
    /// <summary>
    /// This class performs database insert operations and demonstrates safe and unsafe SQL practices.
    /// </summary>
    public class InsertOperation
    {
        private readonly string _connectionString;

        /// <summary>
        /// Constructor initializes the database connection string.
        /// </summary>
        public InsertOperation()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
        }

        /// <summary>
        /// Safely inserts data into the DEPT01 table using parameterized queries to prevent SQL injection.
        /// </summary>
        public void SafeInsertDemo()
        {
            string deptShortName = "HR", deptName = "Human Resources", head = "John Doe";
            int id = 2;

            // SQL query with parameterized values to prevent SQL injection
            string sql = "INSERT INTO DEPT01 (P01F01, P02F02, P03F03, P04F04) VALUES (@id, @deptShortName, @deptName, @head)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Add parameters to the query
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@deptShortName", deptShortName);
                        cmd.Parameters.AddWithValue("@deptName", deptName);
                        cmd.Parameters.AddWithValue("@head", head);

                        // Execute the INSERT command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        Console.WriteLine($"{rowsAffected} row(s) inserted successfully using SafeInsertDemo.");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Inserts data into the DEPT01 table using direct string concatenation, which is vulnerable to SQL injection.
        /// </summary>
        public void UnsafeInsertDemo()
        {
            string deptShortName = "HR", deptName = "Human Resources", head = "John Doe";
            int id = 2;

            // Vulnerable SQL query with direct string concatenation
            string sql = $"INSERT INTO DEPT01 (P01F01, P02F02, P03F03, P04F04) " +
                         $"VALUES ({id}, {deptShortName}', '{deptName}', '{head}')";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Execute the INSERT command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        Console.WriteLine($"{rowsAffected} row(s) inserted successfully using UnsafeInsertDemo.");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

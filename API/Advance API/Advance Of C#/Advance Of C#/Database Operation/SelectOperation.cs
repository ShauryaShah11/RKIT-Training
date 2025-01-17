using System;
using System.Configuration;
using MySql.Data.MySqlClient; // Add MySQL namespace

namespace Advance_Of_C_.Database_Operation
{
    public class SelectOperation
    {
        private readonly string _connectionString;

        // Constructor to initialize the connection string
        public SelectOperation()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
        }

        public void SelectDemo()
        {
            const string sql = "SELECT * FROM dept01";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_connectionString)) // Use MySqlConnection
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn)) // Use MySqlCommand
                    using (MySqlDataReader dbReader = cmd.ExecuteReader()) // Use MySqlDataReader
                    {
                        PrintReaderData(dbReader);
                    }
                }
            }
            catch (MySqlException ex) // Catch MySQL-specific exceptions
            {
                Console.WriteLine("MySQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void PrintReaderData(MySqlDataReader dbReader)
        {
            Console.WriteLine("{0,-5} {1,-10} {2,-20} {3,-20}", "ID", "Short Name", "Long Name", "Head");
            while (dbReader.Read())
            {
                Console.WriteLine(
                    $"{dbReader.GetInt32(0),-5} {dbReader.GetString(1),-10} {dbReader.GetString(2),-20} {dbReader.GetString(3),-20}"
                );
            }

        }
    }
}

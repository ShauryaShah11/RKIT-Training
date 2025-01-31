using FinalDemo.Interfaces;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace FinalDemo.Services
{
    /// <summary>
    /// OrmLiteDbFactory is a class that implements the IOrmLiteDbFactory interface
    /// for managing database connections using the OrmLite ORM framework.
    /// It initializes a connection factory based on the configuration provided,
    /// and opens a connection to the database for interacting with the data.
    /// </summary>
    public class OrmLiteDbFactory : IOrmLiteDbFactory
    {
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Constructor that initializes the OrmLiteDbFactory with the connection string
        /// obtained from the application's configuration.
        /// </summary>
        /// <param name="configuration">The IConfiguration object used to access connection settings.</param>
        public OrmLiteDbFactory(IConfiguration configuration)
        {
            // Retrieving the connection string from the configuration
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // Initializing the OrmLite connection factory with MySQL dialect
            _dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);
        }

        /// <summary>
        /// Opens and returns a new database connection using the OrmLite connection factory.
        /// </summary>
        /// <returns>A database connection (IDbConnection) for interacting with the database.</returns>
        public IDbConnection OpenConnection()
        {
            // Opening and returning the database connection
            return _dbFactory.OpenDbConnection();
        }
    }
}

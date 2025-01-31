using System.Data;

namespace FinalDemo.Interfaces
{
    /// <summary>
    /// Provides a factory interface for managing database connections using OrmLite.
    /// </summary>
    public interface IOrmLiteDbFactory
    {
        /// <summary>
        /// Opens and returns a new database connection.
        /// </summary>
        /// <returns>An instance of <see cref="IDbConnection"/> representing the open connection.</returns>
        IDbConnection OpenConnection();
    }
}

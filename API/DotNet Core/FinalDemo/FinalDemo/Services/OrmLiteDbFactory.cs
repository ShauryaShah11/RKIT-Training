using FinalDemo.Interfaces;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace FinalDemo.Services
{
    public class OrmLiteDbFactory : IOrmLiteDbFactory
    {
        private readonly IDbConnectionFactory _dbFactory;

        public OrmLiteDbFactory(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            _dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

        }
        public IDbConnection OpenConnection()
        {
            return _dbFactory.OpenDbConnection();
        }
    }
}

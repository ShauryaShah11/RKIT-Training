using System.Data;

namespace FinalDemo.Interfaces
{
    public interface IOrmLiteDbFactory
    {
        IDbConnection OpenConnection();
    }
}

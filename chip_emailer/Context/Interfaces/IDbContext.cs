using System.Data;

namespace SnpCaller.Contexts.Interfaces
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
    }
}

using System.Data;

namespace ChipEmailer.Contexts
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
    }
}

using SnpCaller.Contexts.Interfaces;

namespace SnpCaller.Contexts
{
    public class SnpCallerDbContext : DbContext, ISnpCallerDbContext
    {
        public SnpCallerDbContext(string connectionString)
            : base(DatabaseType.SqlServer, connectionString)
        {
        }
    }
}

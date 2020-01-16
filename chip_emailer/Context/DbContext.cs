using Npgsql;
using System.Data;
using System.Data.SqlClient;

namespace ChipEmailer.Contexts
{
    public class DbContext : IDbContext
    {
        protected DatabaseType DatabaseType { get; }
        protected string ConnectionString { get; }

        public DbContext(DatabaseType databaseType, string connectionString)
        {
            DatabaseType = databaseType;
            ConnectionString = connectionString;
        }

        public DbContext(DatabaseType databaseType)
        {
            DatabaseType = databaseType;
        }

        public IDbConnection CreateConnection()
        {
            if (DatabaseType == DatabaseType.Postgres)
            {
                var conn = new NpgsqlConnection(ConnectionString);
                return conn;
            }
            else
            {
                var conn = new SqlConnection(ConnectionString);
                return conn;
            }
        }
    }
}

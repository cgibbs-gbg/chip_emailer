namespace ChipEmailer
{
    public class DatabaseConfiguration
    {
        public DatabaseConfiguration(
            string connectionString,
            DatabaseType databaseType,
            int commandTimeout)
        {
            ConnectionString = connectionString;
            DatabaseType = databaseType;
            CommandTimeout = commandTimeout;
        }

        public string ConnectionString { get; }
        public DatabaseType DatabaseType { get; }
        public int CommandTimeout { get; }
    }
}

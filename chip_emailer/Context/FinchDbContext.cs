namespace ChipEmailer.Contexts
{
    public class FinchDbContext : DbContext, IFinchDbContext
    {
        public FinchDbContext(string connectionString)
            : base(DatabaseType.Postgres, connectionString)
        {
        }
    }
}

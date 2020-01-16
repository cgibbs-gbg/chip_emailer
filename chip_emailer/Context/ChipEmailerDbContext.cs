namespace ChipEmailer.Contexts
{
    public class ChipEmailerDbContext : DbContext, IChipEmailerDbContext
    {
        public ChipEmailerDbContext(string connectionString)
            : base(DatabaseType.Postgres, connectionString)
        {
        }
    }
}

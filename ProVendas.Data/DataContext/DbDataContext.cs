using Microsoft.EntityFrameworkCore;

namespace ProVendas.Data.DataContext
{
    public class DbDataContext : DbContext
    {
        public DbDataContext(DbContextOptions<DbDataContext> options) : base(options) { }
    }
}

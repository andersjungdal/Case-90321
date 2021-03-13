using Microsoft.EntityFrameworkCore;

namespace Billedstyring.Db
{
    public class BilledstyringDbContext : DbContext
    {
        public DbSet<Billedstyring> Billedstyring { get; set; }

        public BilledstyringDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
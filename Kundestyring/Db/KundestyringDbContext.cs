using Microsoft.EntityFrameworkCore;

namespace Kundestyring.Db
{
    public class KundestyringDbContext : DbContext
    {
        public DbSet<Kundestyring> Kundestyring { get; set; }

        public KundestyringDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
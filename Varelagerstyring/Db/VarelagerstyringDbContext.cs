using Microsoft.EntityFrameworkCore;

namespace Varelagerstyring.Db
{
    public class VarelagerstyringDbContext : DbContext
    {
        public DbSet<global::Varelagerstyring.Db.Varelagerstyring> Varelagerstyring { get; set; }

        public VarelagerstyringDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}
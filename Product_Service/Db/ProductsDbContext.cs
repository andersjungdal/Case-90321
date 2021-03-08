using Microsoft.EntityFrameworkCore;

namespace Product_Service.Db
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<Product> Product { get; set; }

        public ProductsDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
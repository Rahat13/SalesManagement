using Microsoft.EntityFrameworkCore;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Sale> Sales => Set<Sale>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasMany(c => c.Sales).WithOne(s => s.Customer).HasForeignKey(s => s.CustomerId);
            modelBuilder.Entity<Product>().HasMany(p => p.Sales).WithOne(s => s.Product).HasForeignKey(s => s.ProductId);

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "iPhone 10", Price = 100 }
            );
        }
    }
}

using Microsoft.EntityFrameworkCore;
using BeerTap.Models;

namespace BeerTap.Data
{
    public class BarDbContext : DbContext
    {
        public BarDbContext(DbContextOptions<BarDbContext> options) : base(options)
        {
        }

        public DbSet<Beverage> Beverages { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Tab> Tabs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure BeverageId has a unique constraint
            modelBuilder.Entity<Beverage>()
                .HasIndex(b => b.BeverageId)
                .IsUnique();

            modelBuilder.Entity<Tab>()
                .HasKey(t => t.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}

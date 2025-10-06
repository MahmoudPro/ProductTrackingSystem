using ProductTrackingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductTrackingSystem.Infrastructure.Contexts
{
    public class PTSContext: DbContext
    {
        public PTSContext(DbContextOptions<PTSContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTrackingLog> ProductTrackingLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.SKU)
                .IsUnique();
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderLines)
                .WithOne(ol => ol.Order)
                .HasForeignKey(ol => ol.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderLines)
                .WithOne(ol => ol.Product)
                .HasForeignKey(ol => ol.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
          



        }

    }
}

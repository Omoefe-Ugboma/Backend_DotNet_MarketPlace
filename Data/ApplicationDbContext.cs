namespace Backend.Data;

using Backend.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public int CurrentTenantId { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Global query filters by TenantId
        modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == CurrentTenantId);
        modelBuilder.Entity<Product>().HasQueryFilter(p => p.TenantId == CurrentTenantId);
        modelBuilder.Entity<Order>().HasQueryFilter(o => o.TenantId == CurrentTenantId);
        modelBuilder.Entity<OrderItem>().HasQueryFilter(oi => oi.Order.TenantId == CurrentTenantId);

        // Fix decimal precision warning
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        // Fix cascade delete issues - change ALL cascade deletes to Restrict
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany()
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}
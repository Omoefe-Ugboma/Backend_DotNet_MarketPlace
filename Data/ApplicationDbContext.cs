using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Global TenantId filter
            modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == GetTenantId());
            modelBuilder.Entity<Product>().HasQueryFilter(p => p.TenantId == GetTenantId());
            modelBuilder.Entity<Order>().HasQueryFilter(o => o.TenantId == GetTenantId());
            modelBuilder.Entity<OrderItem>().HasQueryFilter(oi => oi.TenantId == GetTenantId());

            // Fix decimal precision warning (from Day 2)
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // Fix cascade delete issues (from Day 2)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Add TenantId to OrderItem for better filtering (Day 3 improvement)
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.TenantId)
                .IsRequired();
        }

        private int GetTenantId()
        {
            var tenantIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("TenantId")?.Value;
            return tenantIdClaim != null ? int.Parse(tenantIdClaim) : 0;
        }

        // Override SaveChanges to automatically set TenantId where needed
        public override int SaveChanges()
        {
            SetTenantIds();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTenantIds();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetTenantIds()
        {
            var tenantId = GetTenantId();
            if (tenantId == 0) return;

            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added))
            {
                if (entry.Entity is ITenantEntity tenantEntity)
                {
                    tenantEntity.TenantId = tenantId;
                }
            }
        }
    }
}
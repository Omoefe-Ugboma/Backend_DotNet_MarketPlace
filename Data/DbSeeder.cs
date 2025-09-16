namespace Backend.Data;



using Backend.Models;

public static class DbSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.Tenants.Any())
        {
            var tenant1 = new Tenant { Name = "Tenant A" };
            var tenant2 = new Tenant { Name = "Tenant B" };

            context.Tenants.AddRange(tenant1, tenant2);
            context.SaveChanges();

            var user1 = new User { Name = "Alice", Email = "alice@a.com", TenantId = tenant1.Id };
            var user2 = new User { Name = "Bob", Email = "bob@b.com", TenantId = tenant2.Id };

            var product1 = new Product { Name = "Product 1", Price = 100, TenantId = tenant1.Id };
            var product2 = new Product { Name = "Product 2", Price = 200, TenantId = tenant2.Id };

            context.Users.AddRange(user1, user2);
            context.Products.AddRange(product1, product2);
            context.SaveChanges();
        }
    }
}

using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class TenantService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public TenantService(ApplicationDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<Tenant> RegisterTenantAsync(RegisterTenantRequest request)
        {
            var tenant = new Tenant
            {
                Name = request.Name,
                Identifier = request.Identifier,
                Host = request.Host,
                SubscriptionPlan = request.SubscriptionPlan,
                Subdomain = request.Subdomain,
                Users = request.Users.Select(u => new User
                {
                    Name = u.Name,
                    Email = u.Email,
                    PasswordHash = _passwordHasher.HashPassword(null!, u.PasswordHash),
                    Role = u.Role
                }).ToList(),
                Products = request.Products.Select(p => new Product
                {
                    Name = p.Name,
                    Price = p.Price
                }).ToList()
            };

            _dbContext.Tenants.Add(tenant);
            await _dbContext.SaveChangesAsync();

            return tenant; // ✅ Never void, always return Tenant
        }
    }
}

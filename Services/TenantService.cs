using Backend.DTOs;
using Backend.Models;
using Backend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class TenantService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public TenantService(ApplicationDbContext context,
                             IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Tenant> RegisterTenantAsync(TenantRegisterRequest request)
        {
            // Normalize email
            var adminEmail = request.AdminUser.Email.Trim().ToLower();

            // Check if admin already exists in any tenant
            var existingUser = await _context.Users
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Email == adminEmail);

            if (existingUser != null)
                throw new Exception("Admin user already exists");

            var tenant = new Tenant
            {
                Name = request.Name,
                Identifier = request.Identifier,
                Host = request.Host,
                Subdomain = request.Subdomain,
                SubscriptionPlan = request.SubscriptionPlan,
                Users = new List<User>()
            };

            // Create Admin user
            var adminUser = new User
            {
                Name = request.AdminUser.Name,
                Email = adminEmail,
                Role = request.AdminUser.Role,
                Tenant = tenant
            };

            // Hash password using PBKDF2 (Microsoft default)
            adminUser.PasswordHash = _passwordHasher.HashPassword(adminUser, request.AdminUser.Password);

            tenant.Users.Add(adminUser);

            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();

            Console.WriteLine($"[TENANT REGISTER] Tenant={tenant.Name}, Admin={adminUser.Email}, Hash={adminUser.PasswordHash.Substring(0, 20)}...");

            return tenant;
        }
    }
}

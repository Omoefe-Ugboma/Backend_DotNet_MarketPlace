using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly TokenService _tokenService;

        public AuthService(
            ApplicationDbContext context,
            IPasswordHasher<User> passwordHasher,
            TokenService tokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            email = email.Trim().ToLower();

            // Ignore tenant filters, find user across all tenants
            var user = await _context.Users
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                Console.WriteLine($"[LOGIN FAIL] User '{email}' not found in DB.");
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (result != PasswordVerificationResult.Success)
            {
                Console.WriteLine($"[LOGIN FAIL] Password mismatch for {email}");
                return null;
            }

            Console.WriteLine($"[LOGIN OK] {user.Email}");

            // Generate JWT token
            return _tokenService.GenerateToken(user);
        }
    }
}

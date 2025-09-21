using Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Required]
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "Admin"; // Admin, User, etc.
    }
}

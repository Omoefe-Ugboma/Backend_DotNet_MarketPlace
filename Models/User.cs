namespace Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Role { get; set; } = "User";

        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = default!;
    }
}

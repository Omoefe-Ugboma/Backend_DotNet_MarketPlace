namespace Backend.Dtos
{
    public class TenantRegisterDto
    {
        public string Name { get; set; } = null!;
        public string Identifier { get; set; } = null!;
        public string Host { get; set; } = null!;
        public string Subdomain { get; set; } = null!; // 🔹 required
        public string SubscriptionPlan { get; set; } = null!;
        public UserRegisterDto AdminUser { get; set; } = null!;
    }

    public class UserRegisterDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}

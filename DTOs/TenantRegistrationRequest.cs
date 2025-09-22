namespace Backend.DTOs
{
    public class TenantRegistrationRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string Subdomain { get; set; } = string.Empty;
        public string SubscriptionPlan { get; set; } = string.Empty;
        public AdminUserDto AdminUser { get; set; } = new AdminUserDto();
    }

    public class AdminUserDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
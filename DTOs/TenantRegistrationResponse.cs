namespace Backend.DTOs
{
    public class TenantRegistrationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
        public string Subdomain { get; set; } = string.Empty;
        public string SubscriptionPlan { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public UserResponse AdminUser { get; set; } = new UserResponse();
    }

    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
// DTOs/TenantRegisterRequest.cs
namespace Backend.DTOs
{
    public class TenantRegisterRequest
    {
        public string Name { get; set; }
        public string Identifier { get; set; }
        public string Host { get; set; }
        public string Subdomain { get; set; }
        public string SubscriptionPlan { get; set; }
        public AdminUserRequest AdminUser { get; set; }
    }

    public class AdminUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

// DTOs/TenantDto.cs
namespace Backend.DTOs
{
    public class TenantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public string Host { get; set; }
        public string Subdomain { get; set; }
        public string SubscriptionPlan { get; set; }
        public List<UserDto> Users { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}

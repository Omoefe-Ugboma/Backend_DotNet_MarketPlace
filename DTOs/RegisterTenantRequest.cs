namespace Backend.DTOs
{
    public class RegisterTenantRequest
    {
        public string Name { get; set; } = default!;
        public string Identifier { get; set; } = default!;
        public string Host { get; set; } = default!;
        public string SubscriptionPlan { get; set; } = default!;
        public string Subdomain { get; set; } = default!;
        
        public List<UserRequest> Users { get; set; } = new();
        public List<ProductRequest> Products { get; set; } = new();
    }
}

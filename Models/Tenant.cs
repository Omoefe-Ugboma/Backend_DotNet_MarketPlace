namespace Backend.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Identifier { get; set; } = default!;
        public string Host { get; set; } = default!;
        public string? Subdomain { get; set; }
        public string SubscriptionPlan { get; set; } = "Free";

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

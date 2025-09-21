using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Tenant
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Identifier { get; set; } = string.Empty; // slug

        public string? Host { get; set; }

        public string? SubscriptionPlan { get; set; } = "Free";

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public string Subdomain { get; internal set; }
    }
}

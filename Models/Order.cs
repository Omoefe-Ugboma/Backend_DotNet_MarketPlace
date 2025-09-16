using Backend.Models;
public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
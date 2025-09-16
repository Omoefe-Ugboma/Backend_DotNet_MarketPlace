namespace Backend.Models
{
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; }
}
}
 
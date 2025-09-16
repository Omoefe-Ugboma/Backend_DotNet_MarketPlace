using Backend.Models;

// In Models/OrderItem.cs
// In Models/OrderItem.cs
public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }       // Foreign key
    public Order Order { get; set; }       // Navigation property ← ADD THIS
    public int ProductId { get; set; }     // Foreign key  
    public Product Product { get; set; }   // Navigation property ← ADD THIS
    public int Quantity { get; set; }
}
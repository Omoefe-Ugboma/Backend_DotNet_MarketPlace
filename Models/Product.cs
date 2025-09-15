namespace Backend.Models
{
    public class Product
    {
        public int Id { get; set; }              // Primary Key
        public string Name { get; set; } = "";   // Product name
        public decimal Price { get; set; }       // Product price
        public string Description { get; set; } = "";
        
        // Multi-tenancy support
        public string TenantId { get; set; } = "";  
    }
}

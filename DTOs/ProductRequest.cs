namespace Backend.DTOs
{
    public class ProductRequest
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
    }
}

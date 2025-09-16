using Backend.Models;

public class Tenant
{
    public Tenant()
    {
        Users = new List<User>();
        Products = new List<Product>();
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public List<User> Users { get; set; }
    public List<Product> Products { get; set; }
}
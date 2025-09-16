using Backend.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public ProductController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public IActionResult GetProducts() => Ok(_context.Products.ToList());
}

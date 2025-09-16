using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public OrderController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public IActionResult GetOrders() => Ok(_context.Orders.Include(o => o.OrderItems).ToList());
}

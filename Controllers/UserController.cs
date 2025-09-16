using Backend.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public UserController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public IActionResult GetUsers() => Ok(_context.Users.ToList());
}

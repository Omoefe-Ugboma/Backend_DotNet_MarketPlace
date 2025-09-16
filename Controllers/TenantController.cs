using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TenantController : ControllerBase
{
    private readonly Backend.Data.ApplicationDbContext _context;
    public TenantController(Backend.Data.ApplicationDbContext context) => _context = context;

    [HttpGet]
    public IActionResult GetTenants() => Ok(_context.Tenants.ToList());
}

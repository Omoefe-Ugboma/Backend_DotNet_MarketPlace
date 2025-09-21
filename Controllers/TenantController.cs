using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantController : ControllerBase
    {
        private readonly TenantService _tenantService;

        public TenantController(TenantService tenantService)
        {
            _tenantService = tenantService;
        }

         [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DTOs.RegisterTenantRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tenant = await _tenantService.RegisterTenantAsync(request);
            return Ok(tenant);
        }
    }
}

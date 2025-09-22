// Controllers/TenantController.cs
using Backend.DTOs;
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
        public async Task<ActionResult<TenantDto>> RegisterTenant([FromBody] TenantRegisterRequest request)
        {
            var tenant = await _tenantService.RegisterTenantAsync(request);

            var dto = new TenantDto
            {
                Id = tenant.Id,
                Name = tenant.Name,
                Identifier = tenant.Identifier,
                Host = tenant.Host,
                Subdomain = tenant.Subdomain,
                SubscriptionPlan = tenant.SubscriptionPlan,
                Users = tenant.Users.Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role
                }).ToList()
            };

            return Ok(dto);
        }
    }
}

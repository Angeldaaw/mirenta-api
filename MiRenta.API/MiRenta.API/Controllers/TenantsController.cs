using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiRenta.Application.Common.Models;
using MiRenta.Application.Tenants.DTOs;
using MiRenta.Application.Tenants.Interfaces;

namespace MiRenta.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/tenants")]
    public class TenantsController : BaseApiController
    {

        private readonly ITenantService _tenantService;

        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Result<List<TenantResponseDto>> result = await _tenantService.GetAllAsync(UserId);
            return ToActionResult(result);
        }

    }
}

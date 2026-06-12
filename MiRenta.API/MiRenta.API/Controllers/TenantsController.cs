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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Result<TenantResponseDto> result = await _tenantService.GetByIdAsync(id, UserId);
            return ToActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTenantDto request)
        {
            Result<TenantResponseDto> result = await _tenantService.CreateAsync(request, UserId);
            return ToActionResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTenantDto request)
        {
            Result<TenantResponseDto> result = await _tenantService.UpdateAsync(id, request, UserId);
            return ToActionResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Result result = await _tenantService.DeleteAsync(id, UserId);

            if (result.Success)
                return NoContent();

            return ToActionResult(result);
        }

    }
}

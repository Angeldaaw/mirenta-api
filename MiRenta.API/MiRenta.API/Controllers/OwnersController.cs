using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiRenta.Application.Common.Models;
using MiRenta.Application.Owners.DTOs;
using MiRenta.Application.Owners.Interfaces;

namespace MiRenta.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/owners")]
    public class OwnersController : BaseApiController
    {
        private readonly IOwnerService _ownerService;

        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Result<List<OwnerResponseDto>> result = await _ownerService.GetAllAsync(UserId);

            return ToActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Result<OwnerResponseDto> result = await _ownerService.GetByIdAsync(id, UserId);

            return ToActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOwnerDto request)
        {
            Result<OwnerResponseDto> result = await _ownerService.CreateAsync(request, UserId);

            if (!result.Success)
                return ToActionResult(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateOwnerDto request)
        {
            Result<OwnerResponseDto> result = await _ownerService.UpdateAsync(id, request, UserId);

            return ToActionResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Result result = await _ownerService.DeleteAsync(id, UserId);

            if (result.Success)
                return NoContent();

            return ToActionResult(result);
        }
    }
}

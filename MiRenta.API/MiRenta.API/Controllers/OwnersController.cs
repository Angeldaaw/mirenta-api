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
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Guid userId = GetUserId();
            Result<List<OwnerResponseDto>> result = await _ownerService.GetAllAsync(userId);

            return ToActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Guid userId = GetUserId();
            Result<OwnerResponseDto> result = await _ownerService.GetByIdAsync(id, userId);

            return ToActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOwnerDto request)
        {
            Guid userId = GetUserId();
            Result<OwnerResponseDto> result = await _ownerService.CreateAsync(request, userId);

            if (!result.Success)
                return ToActionResult(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateOwnerDto request)
        {
            Guid userId = GetUserId();
            Result<OwnerResponseDto> result = await _ownerService.UpdateAsync(id, request, userId);

            return ToActionResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Guid userId = GetUserId();
            Result result = await _ownerService.DeleteAsync(id, userId);

            if (result.Success)
                return NoContent();

            return ToActionResult(result);
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        private IActionResult ToActionResult<T>(Result<T> result)
        {
            if (result.Success)
                return Ok(result.Data);

            return result.ErrorType switch
            {
                ErrorType.NotFound => NotFound(new { error = result.Error }),
                ErrorType.Conflict => Conflict(new { error = result.Error }),
                _ => BadRequest(new { error = result.Error })
            };
        }

        private IActionResult ToActionResult(Result result)
        {
            return result.ErrorType switch
            {
                ErrorType.NotFound => NotFound(new { error = result.Error }),
                ErrorType.Conflict => Conflict(new { error = result.Error }),
                _ => BadRequest(new { error = result.Error })
            };
        }
    }
}

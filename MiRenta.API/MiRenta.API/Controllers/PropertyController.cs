using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiRenta.Application.Common.Models;
using MiRenta.Application.Properties.DTOs;
using MiRenta.Application.Properties.Interfaces;

namespace MiRenta.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/properties")]
    public class PropertyController : BaseApiController
    {
        private readonly IPropertiesService _propertiesService;

        public PropertyController(IPropertiesService propertiesService)
        {
            _propertiesService = propertiesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Result<List<PropertyResponseDto>> result = await _propertiesService.GetAllAsync(UserId);

            return ToActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Result<PropertyResponseDto> result = await _propertiesService.GetByIdAsync(id, UserId);

            return ToActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePropertyDto request)
        {
            Result<PropertyResponseDto> result = await _propertiesService.CreateAsync(request, UserId);

            if (!result.Success)
                return ToActionResult(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePropertyDto request)
        {
            Result<PropertyResponseDto> result = await _propertiesService.UpdateAsync(id, request, UserId);

            return ToActionResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Result result = await _propertiesService.DeleteAsync(id, UserId);

            if (!result.Success)
                return ToActionResult(result);

            return NoContent();
        }
    }
}

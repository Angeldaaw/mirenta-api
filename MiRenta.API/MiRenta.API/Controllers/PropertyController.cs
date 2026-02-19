using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiRenta.Application.Authentication.Interfaces;
using MiRenta.Application.Properties.DTOs;
using MiRenta.Domain.Entities;
using System.Security.Claims;

namespace MiRenta.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/properties")]
    public class PropertyController : ControllerBase
    {
        private readonly IApplicationDbContext _context;

        public PropertyController(IApplicationDbContext context)
        {
            _context = context;
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        // GET: api/properties
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();

            var properties = await _context.Properties
                .Where(p => p.UserId == userId)
                .Select(p => new PropertyResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Address = p.Address,
                    MonthlyRent = p.MonthlyRent
                })
                .ToListAsync();

            return Ok(properties);
        }

        // POST: api/properties
        [HttpPost]
        public async Task<IActionResult> Create(CreatePropertyDto request)
        {
            var userId = GetUserId();

            var property = new Property
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address,
                MonthlyRent = request.MonthlyRent,
                UserId = userId
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            var response = new PropertyResponseDto
            {
                Id = property.Id,
                Name = property.Name,
                Address = property.Address,
                MonthlyRent = property.MonthlyRent
            };

            return Ok(response);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MiRenta.Application.Authentication.Interfaces;
using MiRenta.Application.Common.Models;
using MiRenta.Application.Properties.DTOs;
using MiRenta.Application.Properties.Interfaces;
using MiRenta.Domain.Entities;

namespace MiRenta.Application.Properties.Services
{
    public class PropertiesService : IPropertiesService
    {
        private readonly IApplicationDbContext _context;

        public PropertiesService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<PropertyResponseDto>>> GetAllAsync(Guid userId)
        {
            List<PropertyResponseDto> properties = await _context.Properties
                .Where(p => p.UserId == userId && !p.IsDeleted)
                .OrderBy(p => p.Name)
                .Select(p => ToResponse(p))
                .ToListAsync();

            return Result<List<PropertyResponseDto>>.Ok(properties);
        }

        public async Task<Result<PropertyResponseDto>> GetByIdAsync(Guid id, Guid userId)
        {
            Property? property = await _context.Properties
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId && !p.IsDeleted);

            if (property == null)
                return Result<PropertyResponseDto>.Fail("Property not found.", ErrorType.NotFound);

            return Result<PropertyResponseDto>.Ok(ToResponse(property));
        }

        public async Task<Result<PropertyResponseDto>> CreateAsync(CreatePropertyDto request, Guid userId)
        {
            string? validationError = Validate(request.Name, request.Address, request.MonthlyRent);
            if (validationError != null)
                return Result<PropertyResponseDto>.Fail(validationError);

            var property = new Property
            {
                Id = Guid.NewGuid(),
                Name = request.Name.Trim(),
                Address = request.Address.Trim(),
                MonthlyRent = request.MonthlyRent,
                UserId = userId
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            return Result<PropertyResponseDto>.Ok(ToResponse(property));
        }

        public async Task<Result<PropertyResponseDto>> UpdateAsync(Guid id, UpdatePropertyDto request, Guid userId)
        {
            string? validationError = Validate(request.Name, request.Address, request.MonthlyRent);
            if (validationError != null)
                return Result<PropertyResponseDto>.Fail(validationError);

            Property? property = await _context.Properties
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId && !p.IsDeleted);

            if (property == null)
                return Result<PropertyResponseDto>.Fail("Property not found.", ErrorType.NotFound);

            property.Name = request.Name.Trim();
            property.Address = request.Address.Trim();
            property.MonthlyRent = request.MonthlyRent;
            property.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Result<PropertyResponseDto>.Ok(ToResponse(property));
        }

        public async Task<Result> DeleteAsync(Guid id, Guid userId)
        {
            Property? property = await _context.Properties
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId && !p.IsDeleted);

            if (property == null)
                return Result.Fail("Property not found.", ErrorType.NotFound);

            property.IsDeleted = true;
            property.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Result.Ok();
        }

        private static PropertyResponseDto ToResponse(Property property)
        {
            return new PropertyResponseDto
            {
                Id = property.Id,
                Name = property.Name,
                Address = property.Address,
                MonthlyRent = property.MonthlyRent
            };
        }

        private static string? Validate(string name, string address, decimal monthlyRent)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Name is required.";

            if (name.Trim().Length > 150)
                return "Name cannot be longer than 150 characters.";

            if (string.IsNullOrWhiteSpace(address))
                return "Address is required.";

            if (address.Trim().Length > 300)
                return "Address cannot be longer than 300 characters.";

            if (monthlyRent <= 0)
                return "Monthly rent must be greater than zero.";

            return null;
        }
    }
}

using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using MiRenta.Application.Authentication.Interfaces;
using MiRenta.Application.Common.Models;
using MiRenta.Application.Owners.DTOs;
using MiRenta.Application.Tenants.DTOs;
using MiRenta.Application.Tenants.Interfaces;
using MiRenta.Domain.Entities;

namespace MiRenta.Application.Tenants.Services
{
    public class TenantService : ITenantService
    {

        private readonly IApplicationDbContext _context;

        public TenantService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<TenantResponseDto>>> GetAllAsync(Guid userId)
        {
            var tenants = await _context.Tenants
                .Where(t => t.UserId == userId && t.IsActive)
                .OrderBy(t => t.Name)
                .Select(t => ToResponse(t))
                .ToListAsync();


            return Result<List<TenantResponseDto>>.Ok(tenants);
        }

        public async Task<Result<TenantResponseDto>> GetByIdAsync(Guid id, Guid userId)
        {
            Tenant? tenant = await _context.Tenants
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId && t.IsActive);

            if (tenant == null)
                return Result<TenantResponseDto>.Fail("Tenant not found", ErrorType.NotFound);

            return Result<TenantResponseDto>.Ok(ToResponse(tenant));
        }

        public async Task<Result<TenantResponseDto>> CreateAsync(CreateTenantDto request, Guid userId)
        {
            string? validationError = Validate(request);
            if (validationError != null)
                return Result<TenantResponseDto>.Fail(validationError, ErrorType.Validation);

            string email = NormalizeEmail(request.Email);
            bool emailExists = await _context.Tenants.Where(t => t.Email == email && t.IsActive).AnyAsync();

            if (emailExists)
                return Result<TenantResponseDto>.Fail("El correo electrónico ya está registrado.", ErrorType.Conflict);

            bool identificationNumberExists = await _context.Tenants
                .Where(t => t.IdentificationNumber == request.IdentificationNumber && t.IsActive).AnyAsync();

            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = request.Name.Trim(),
                LastName = request.LastName,
                Email = request.Email.Trim(),
                Phone = request.Phone.Trim(),
                IdentificationNumber = request.IdentificationNumber?.Trim(),
                UserId = userId
            };

            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();

            return Result<TenantResponseDto>.Ok(ToResponse(tenant));
        }

        public async Task<Result<TenantResponseDto>> UpdateAsync(Guid id, UpdateTenantDto request, Guid userId)
        {
            string? validationError = Validate(request);
            if (validationError != null)
                return Result<TenantResponseDto>.Fail(validationError, ErrorType.Validation);

            Tenant? tenant = await _context.Tenants
                .Where(t => t.Id == id && t.UserId == userId && t.IsActive)
                .FirstOrDefaultAsync();

            if (tenant == null)
                return Result<TenantResponseDto>.Fail("Inquilino no encontrado", ErrorType.NotFound);

            string email = NormalizeEmail(request.Email);
            bool emailExists = await _context.Tenants
                .Where(t => t.Id != id && t.UserId == userId && t.Email == email && t.IsActive)
                .AnyAsync();

            if (emailExists)
                return Result<TenantResponseDto>.Fail("El correo electrónico ya está registrado.", ErrorType.Conflict);

            tenant.Name = request.Name.Trim();
            tenant.LastName = request.LastName.Trim();
            tenant.Email = email;
            tenant.Phone = request.Phone.Trim();
            tenant.IdentificationNumber = request.IdentificationNumber?.Trim();
            tenant.IsActive = request.IsActive;

            await _context.SaveChangesAsync();
            return Result<TenantResponseDto>.Ok(ToResponse(tenant));
        }

        public async Task<Result> DeleteAsync(Guid id, Guid userId)
        {
            Tenant? tenant = await _context.Tenants
                .Where(t => t.Id == id && t.UserId == userId && t.IsActive)
                .FirstOrDefaultAsync();

            if (tenant == null)
                return Result.Fail("Inquilino no encontrado");

            bool hasLeases = await _context.Leases
                .Where(l => l.TenantId == id && l.EndDate > DateTime.UtcNow)
                .AnyAsync();

            if (hasLeases)
                return Result.Fail("El inquilino no puede ser eliminado porque tiene arrendamientos activos.", ErrorType.Conflict);

            tenant.IsActive = false;
            await _context.SaveChangesAsync();

            return Result.Ok();
        }

        private static TenantResponseDto ToResponse(Tenant tenant)
        {
            return new TenantResponseDto
            {
                Id = tenant.Id,
                Name = tenant.Name,
                LastName = tenant.LastName,
                Email = tenant.Email,
                Phone = tenant.Phone,
                IsActive = tenant.IsActive,
                CreatedAt = tenant.CreatedAt,
                IdentificationNumber = tenant.IdentificationNumber
            };

        }

        private static string? Validate(CreateTenantDto tenant)
        {
            return ValidateTenant(
                tenant.Name,
                tenant.LastName,
                tenant.Email,
                tenant.Phone,
                tenant.IdentificationNumber);
        }

        private static string? Validate(UpdateTenantDto tenant)
        {
            return ValidateTenant(
                tenant.Name,
                tenant.LastName,
                tenant.Email,
                tenant.Phone,
                tenant.IdentificationNumber);
        }

        private static string? ValidateTenant(
            string name,
            string lastName,
            string email,
            string phone,
            string? identificationNumber)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "El nombre es obligatorio.";
            }

            if (name.Length > 50)
            {
                return "El nombre no puede tener más de 50 caracteres.";
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                return "El apellido es obligatorio.";
            }

            if (lastName.Length > 50)
            {
                return "El apellido no puede tener más de 50 caracteres.";
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return "El email es obligatorio.";
            }

            if (email.Length > 100)
            {
                return "El email no puede tener más de 100 caracteres.";
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                return "El número de teléfono es obligatorio.";
            }

            if (phone.Length > 30)
            {
                return "El número de teléfono no puede tener más de 30 caracteres.";
            }

            if (identificationNumber?.Length > 50)
            {
                return "El número de identificación no puede tener más de 50 caracteres.";
            }

            return null;
        }

        private static string NormalizeEmail(string email)
        {
            return email.Trim().ToLowerInvariant();
        }
    }
}

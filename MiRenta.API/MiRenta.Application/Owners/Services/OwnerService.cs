using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using MiRenta.Application.Authentication.Interfaces;
using MiRenta.Application.Common.Models;
using MiRenta.Application.Owners.DTOs;
using MiRenta.Application.Owners.Interfaces;
using MiRenta.Domain.Entities;

namespace MiRenta.Application.Owners.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IApplicationDbContext _context;

        public OwnerService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<OwnerResponseDto>>> GetAllAsync(Guid userId)
        {
            var owners = await _context.Owners
                .Where(o => o.UserId == userId && o.IsActive)
                .OrderBy(o => o.Name)
                .Select(o => ToResponse(o))
                .ToListAsync();

            return Result<List<OwnerResponseDto>>.Ok(owners);
        }

        public async Task<Result<OwnerResponseDto>> GetByIdAsync(Guid id, Guid userId)
        {
            Owner? owner = await _context.Owners
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId && o.IsActive);

            if (owner == null)
                return Result<OwnerResponseDto>.Fail("Owner not found.", ErrorType.NotFound);

            return Result<OwnerResponseDto>.Ok(ToResponse(owner));
        }

        public async Task<Result<OwnerResponseDto>> CreateAsync(CreateOwnerDto request, Guid userId)
        {
            string? validationError = Validate(request.Name, request.Email, request.Phone);
            if (validationError != null)
                return Result<OwnerResponseDto>.Fail(validationError);

            string email = NormalizeEmail(request.Email);
            bool emailExists = await _context.Owners
                .AnyAsync(o => o.UserId == userId && o.Email == email && o.IsActive);

            if (emailExists)
                return Result<OwnerResponseDto>.Fail("An active owner with this email already exists.", ErrorType.Conflict);

            var owner = new Owner
            {
                Id = Guid.NewGuid(),
                Name = request.Name.Trim(),
                Email = email,
                Phone = request.Phone.Trim(),
                IsActive = true,
                UserId = userId
            };

            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            return Result<OwnerResponseDto>.Ok(ToResponse(owner));
        }

        public async Task<Result<OwnerResponseDto>> UpdateAsync(Guid id, UpdateOwnerDto request, Guid userId)
        {
            string? validationError = Validate(request.Name, request.Email, request.Phone);
            if (validationError != null)
                return Result<OwnerResponseDto>.Fail(validationError);

            Owner? owner = await _context.Owners
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId && o.IsActive);

            if (owner == null)
                return Result<OwnerResponseDto>.Fail("Owner not found.", ErrorType.NotFound);

            string email = NormalizeEmail(request.Email);
            bool emailExists = await _context.Owners
                .AnyAsync(o => o.Id != id && o.UserId == userId && o.Email == email && o.IsActive);

            if (emailExists)
                return Result<OwnerResponseDto>.Fail("An active owner with this email already exists.", ErrorType.Conflict);

            owner.Name = request.Name.Trim();
            owner.Email = email;
            owner.Phone = request.Phone.Trim();

            await _context.SaveChangesAsync();

            return Result<OwnerResponseDto>.Ok(ToResponse(owner));
        }

        public async Task<Result> DeleteAsync(Guid id, Guid userId)
        {
            Owner? owner = await _context.Owners
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId && o.IsActive);

            if (owner == null)
                return Result.Fail("Owner not found.", ErrorType.NotFound);

            bool hasProperties = await _context.Properties
                .AnyAsync(p => p.OwnerId == id && p.UserId == userId && !p.IsDeleted);

            if (hasProperties)
                return Result.Fail("Owner cannot be deleted because it has active properties.", ErrorType.Conflict);

            owner.IsActive = false;
            await _context.SaveChangesAsync();

            return Result.Ok();
        }

        private static OwnerResponseDto ToResponse(Owner owner)
        {
            return new OwnerResponseDto
            {
                Id = owner.Id,
                Name = owner.Name,
                Email = owner.Email,
                Phone = owner.Phone,
                IsActive = owner.IsActive,
                CreatedAt = owner.CreatedAt
            };
        }

        private static string? Validate(string name, string email, string phone)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Name is required.";

            if (name.Trim().Length > 100)
                return "Name cannot be longer than 100 characters.";

            if (string.IsNullOrWhiteSpace(email))
                return "Email is required.";

            if (email.Trim().Length > 100)
                return "Email cannot be longer than 100 characters.";

            if (!IsValidEmail(email))
                return "Email format is invalid.";

            if (string.IsNullOrWhiteSpace(phone))
                return "Phone is required.";

            if (phone.Trim().Length > 30)
                return "Phone cannot be longer than 30 characters.";

            return null;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                _ = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string NormalizeEmail(string email)
        {
            return email.Trim().ToLowerInvariant();
        }
    }
}

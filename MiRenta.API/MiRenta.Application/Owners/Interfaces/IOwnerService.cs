using MiRenta.Application.Common.Models;
using MiRenta.Application.Owners.DTOs;

namespace MiRenta.Application.Owners.Interfaces
{
    public interface IOwnerService
    {
        Task<Result<List<OwnerResponseDto>>> GetAllAsync(Guid userId);
        Task<Result<OwnerResponseDto>> GetByIdAsync(Guid id, Guid userId);
        Task<Result<OwnerResponseDto>> CreateAsync(CreateOwnerDto request, Guid userId);
        Task<Result<OwnerResponseDto>> UpdateAsync(Guid id, UpdateOwnerDto request, Guid userId);
        Task<Result> DeleteAsync(Guid id, Guid userId);
    }
}

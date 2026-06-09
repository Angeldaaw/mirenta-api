using MiRenta.Application.Common.Models;
using MiRenta.Application.Tenants.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRenta.Application.Tenants.Interfaces
{
    public interface ITenantService
    {
        Task<Result<List<TenantResponseDto>>> GetAllAsync(Guid userId);
        Task<Result<TenantResponseDto>> GetByIdAsync(Guid id, Guid userId);
        Task<Result<TenantResponseDto>> CreateAsync(CreateTenantDto request, Guid userId);
        Task<Result<TenantResponseDto>> UpdateAsync(Guid id, UpdateTenantDto request, Guid userId);
        Task<Result> DeleteAsync(Guid id, Guid userId);
    }
}

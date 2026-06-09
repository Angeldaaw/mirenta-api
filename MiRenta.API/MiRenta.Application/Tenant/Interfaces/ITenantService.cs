using MiRenta.Application.Common.Models;
using MiRenta.Application.Tenant.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRenta.Application.Tenant.Interfaces
{
    public interface ITenantService
    {
        Task<Result<List<TenantResponseDto>>> GetAllAsync(Guid userId);
        Task<Result<TenantResponseDto>> GetByIdAsync(Guid userId);
        Task<Result<TenantResponseDto>> CreateAsync(CreateTenantDto request, Guid userId);
    }
}

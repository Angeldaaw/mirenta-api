using MiRenta.Application.Common.Models;
using MiRenta.Application.Properties.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRenta.Application.Properties.Interfaces
{
    public interface IPropertiesService
    {
        Task<Result<List<PropertyResponseDto>>> GetAllAsync();
        Task<Result<PropertyResponseDto>> GetByIdAsync(Guid id, Guid userId);
        Task<Result<PropertyResponseDto>> CreateAync(CreatePropertyDto request, Guid userId);
        Task<Result<PropertyResponseDto>> UpdateAsync(Guid id, UpdatePropertyDto request, Guid userId);
        Task<Result> DeleteAsync(Guid id, Guid userId);
    }
}

using MiRenta.Application.Authentication.Interfaces;
using MiRenta.Application.Common.Models;
using MiRenta.Application.Properties.DTOs;
using MiRenta.Application.Properties.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRenta.Application.Properties.Services
{
    public class PropertiesService(IApplicationDbContext context) : IPropertiesService
    {
        private readonly IApplicationDbContext _context = context;

        public Task<Result<PropertyResponseDto>> CreateAync(CreatePropertyDto request, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteAsync(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<PropertyResponseDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<PropertyResponseDto>> GetByIdAsync(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<PropertyResponseDto>> UpdateAsync(Guid id, UpdatePropertyDto request, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}

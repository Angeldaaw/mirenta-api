using MiRenta.Application.Authentication.DTOs;
using MiRenta.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRenta.Application.Authentication.Interfaces
{
    public interface IAuthService
    {
        Task<Result<AuthResponseDto>> RegisterAsync(RegisterRequestDto request);
        Task<Result<AuthResponseDto>> LoginAsync(LoginRequestDto request);
    }
}

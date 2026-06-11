using Microsoft.AspNetCore.Mvc;
using MiRenta.Application.Authentication.DTOs;
using MiRenta.Application.Authentication.Interfaces;
using MiRenta.Application.Common;
using MiRenta.Application.Common.Models;

namespace MiRenta.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            Result<AuthResponseDto> result = await _authService.RegisterAsync(request);
            return ToActionResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            Result<AuthResponseDto> result = await _authService.LoginAsync(request);
            return ToActionResult(result);
        }

    }
}

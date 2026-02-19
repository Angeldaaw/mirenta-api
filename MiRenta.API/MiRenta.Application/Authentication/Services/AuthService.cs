using Microsoft.EntityFrameworkCore;
using MiRenta.Application.Authentication.DTOs;
using MiRenta.Application.Authentication.Interfaces;
using MiRenta.Domain.Entities;

namespace MiRenta.Application.Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public AuthService(
            IApplicationDbContext context,
            IPasswordHasher passwordHasher,
            IJwtService jwtService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            // Find user by email
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            // Validate credentials
            if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
                throw new Exception("Invalid credentials.");

            // Generate JWT token
            var token = GetToken(user.Id, user.Email);
            return new AuthResponseDto { Token = token };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            // Check if email is already registered
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                throw new Exception("Email already in registered.");

            // Create new user
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = _passwordHasher.Hash(request.Password)
            };

            // Save user to database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generate JWT token
            var token =  GetToken(user.Id, user.Email);
            return new AuthResponseDto { Token = token };
        }

        private string GetToken(Guid userId, string email)
        {
            return _jwtService.GenerateToken(userId, email);
        }
    }
}

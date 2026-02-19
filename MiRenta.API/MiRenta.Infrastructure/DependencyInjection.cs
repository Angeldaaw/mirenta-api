using Microsoft.Extensions.DependencyInjection;
using MiRenta.Application.Authentication.Interfaces;
using MiRenta.Application.Authentication.Services;
using MiRenta.Infrastructure.Persistence;
using MiRenta.Infrastructure.Security;

namespace MiRenta.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<AppDbContext>());

            return services;
        }

    }
}

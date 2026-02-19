using MiRenta.Application.Authentication.Interfaces;

namespace MiRenta.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {

        public string Hash(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string password, string hash)
            => BCrypt.Net.BCrypt.Verify(password, hash);

    }
}

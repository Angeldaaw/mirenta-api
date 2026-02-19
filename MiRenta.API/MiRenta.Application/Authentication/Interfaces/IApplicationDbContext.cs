using Microsoft.EntityFrameworkCore;
using MiRenta.Domain.Entities;

namespace MiRenta.Application.Authentication.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Property> Properties { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

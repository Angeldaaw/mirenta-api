using Microsoft.EntityFrameworkCore;
using MiRenta.Domain.Entities;

namespace MiRenta.Application.Authentication.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Property> Properties { get; }
        DbSet<Tenant> Tenants { get; }
        DbSet<Owner> Owners { get; }
        DbSet<Unit> Units { get; }
        DbSet<Lease> Leases { get; }
        DbSet<Payment> Payments { get; }
        DbSet<PaymentReceipt> PaymentReceipts { get; }
        DbSet<MaintenanceRequest> MaintenanceRequests { get; }
        DbSet<PropertyExpense> PropertyExpenses { get; }
        DbSet<PropertyImage> PropertyImages { get; }
        DbSet<Document> Documents { get; }
        DbSet<Notification> Notifications { get; }
        DbSet<Role> Roles { get; }
        DbSet<UserRole> UserRoles { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

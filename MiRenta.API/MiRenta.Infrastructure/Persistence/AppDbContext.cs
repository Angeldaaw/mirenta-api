using Microsoft.EntityFrameworkCore;
using MiRenta.Application.Authentication.Interfaces;
using MiRenta.Domain.Entities;

namespace MiRenta.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Lease> Leases { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentReceipt> PaymentReceipts { get; set; }
        public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
        public DbSet<PropertyExpense> PropertyExpenses { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(u => u.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(u => u.Email)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(u => u.PasswordHash)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(u => u.Activo)
                    .HasDefaultValue(true);

                entity.HasIndex(u => u.Email)
                    .IsUnique();
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.Property(p => p.Name)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(p => p.Address)
                    .HasMaxLength(300)
                    .IsRequired();

                entity.Property(p => p.MonthlyRent)
                    .HasPrecision(18, 2);

                entity.Property(p => p.Status)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(p => p.IsDeleted)
                    .HasDefaultValue(false);

                entity.Property(p => p.CreatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.Property(p => p.UserId)
                    .IsRequired();

                entity.HasOne(p => p.User)
                    .WithMany(u => u.Properties)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.Owner)
                    .WithMany(o => o.Properties)
                    .HasForeignKey(p => p.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.Property(t => t.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(t => t.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(t => t.Email)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(t => t.Phone)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(t => t.IdentificationNumber)
                    .HasMaxLength(50);

                entity.Property(t => t.IsActive)
                    .HasDefaultValue(true);

                entity.Property(t => t.CreatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.HasOne(t => t.User)
                    .WithMany(u => u.Tenants)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(t => new { t.UserId, t.Email });
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.Property(o => o.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(o => o.Email)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(o => o.Phone)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(o => o.IsActive)
                    .HasDefaultValue(true);

                entity.Property(o => o.CreatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.HasOne(o => o.User)
                    .WithMany(u => u.Owners)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(o => new { o.UserId, o.Email })
                    .IsUnique()
                    .HasFilter("[IsActive] = 1");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(u => u.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(u => u.MonthlyRent)
                    .HasPrecision(18, 2);

                entity.Property(u => u.Status)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(u => u.IsDeleted)
                    .HasDefaultValue(false);

                entity.HasOne(u => u.Property)
                    .WithMany(p => p.Units)
                    .HasForeignKey(u => u.PropertyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Lease>(entity =>
            {
                entity.Property(l => l.MonthlyRent)
                    .HasPrecision(18, 2);

                entity.Property(l => l.DepositAmount)
                    .HasPrecision(18, 2);

                entity.Property(l => l.Status)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(l => l.CreatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.HasOne(l => l.Property)
                    .WithMany(p => p.Leases)
                    .HasForeignKey(l => l.PropertyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.Unit)
                    .WithMany(u => u.Leases)
                    .HasForeignKey(l => l.UnitId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.Tenant)
                    .WithMany(t => t.Leases)
                    .HasForeignKey(l => l.TenantId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(p => p.Amount)
                    .HasPrecision(18, 2);

                entity.Property(p => p.Status)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(p => p.Method)
                    .HasMaxLength(50);

                entity.Property(p => p.Reference)
                    .HasMaxLength(100);

                entity.Property(p => p.CreatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.HasOne(p => p.Lease)
                    .WithMany(l => l.Payments)
                    .HasForeignKey(p => p.LeaseId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PaymentReceipt>(entity =>
            {
                entity.Property(r => r.ReceiptNumber)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(r => r.FileUrl)
                    .HasMaxLength(500);

                entity.Property(r => r.IssuedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.HasOne(r => r.Payment)
                    .WithOne(p => p.Receipt)
                    .HasForeignKey<PaymentReceipt>(r => r.PaymentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(r => r.PaymentId)
                    .IsUnique();
            });

            modelBuilder.Entity<MaintenanceRequest>(entity =>
            {
                entity.Property(m => m.Title)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(m => m.Description)
                    .HasMaxLength(1000)
                    .IsRequired();

                entity.Property(m => m.Status)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(m => m.Priority)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(m => m.Cost)
                    .HasPrecision(18, 2);

                entity.Property(m => m.ReportedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.HasOne(m => m.Property)
                    .WithMany(p => p.MaintenanceRequests)
                    .HasForeignKey(m => m.PropertyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Unit)
                    .WithMany(u => u.MaintenanceRequests)
                    .HasForeignKey(m => m.UnitId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Tenant)
                    .WithMany(t => t.MaintenanceRequests)
                    .HasForeignKey(m => m.TenantId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PropertyExpense>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsRequired();

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Amount)
                    .HasPrecision(18, 2);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.HasOne(e => e.Property)
                    .WithMany(p => p.Expenses)
                    .HasForeignKey(e => e.PropertyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PropertyImage>(entity =>
            {
                entity.Property(i => i.Url)
                    .HasMaxLength(500)
                    .IsRequired();

                entity.Property(i => i.Caption)
                    .HasMaxLength(200);

                entity.Property(i => i.CreatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.HasOne(i => i.Property)
                    .WithMany(p => p.Images)
                    .HasForeignKey(i => i.PropertyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.Property(d => d.Name)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(d => d.DocumentType)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(d => d.FileUrl)
                    .HasMaxLength(500)
                    .IsRequired();

                entity.Property(d => d.UploadedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.HasOne(d => d.User)
                    .WithMany(u => u.Documents)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Tenant)
                    .WithMany(t => t.Documents)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Lease)
                    .WithMany(l => l.Documents)
                    .HasForeignKey(d => d.LeaseId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(n => n.Title)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(n => n.Message)
                    .HasMaxLength(1000)
                    .IsRequired();

                entity.Property(n => n.IsRead)
                    .HasDefaultValue(false);

                entity.Property(n => n.CreatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.HasOne(n => n.User)
                    .WithMany(u => u.Notifications)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(r => r.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(r => r.Description)
                    .HasMaxLength(200);

                entity.HasIndex(r => r.Name)
                    .IsUnique();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

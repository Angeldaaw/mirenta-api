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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(u => u.Email)
                    .HasMaxLength(100)
                    .IsRequired();


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
                    .

            });
        }
    }
}

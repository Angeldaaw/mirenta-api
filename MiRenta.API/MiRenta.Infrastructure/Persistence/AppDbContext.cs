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
            modelBuilder.Entity<Property>()
                .Property(p => p.MonthlyRent)
                .HasPrecision(18, 2);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRenta.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public ICollection<Property> Properties { get; set; } = new List<Property>();
        public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
        public ICollection<Owner> Owners { get; set; } = new List<Owner>();
        public ICollection<Document> Documents { get; set; } = new List<Document>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}

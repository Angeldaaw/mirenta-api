using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRenta.Domain.Entities
{
    public class Property
    { 

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal MonthlyRent { get; set; }
        public string Status { get; set; } = "Available";
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public Guid? OwnerId { get; set; }

        // Navigation property
        public User User { get; set; } = null!;
        public Owner? Owner { get; set; }
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
        public ICollection<Lease> Leases { get; set; } = new List<Lease>();
        public ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();
        public ICollection<PropertyExpense> Expenses { get; set; } = new List<PropertyExpense>();
        public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
        public ICollection<Document> Documents { get; set; } = new List<Document>();

    }
}

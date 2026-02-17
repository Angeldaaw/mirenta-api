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
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }

        // Navigation property
        public User User { get; set; } = null!;

    }
}

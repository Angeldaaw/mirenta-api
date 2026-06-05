namespace MiRenta.Domain.Entities
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? IdentificationNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Lease> Leases { get; set; } = new List<Lease>();
        public ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();
        public ICollection<Document> Documents { get; set; } = new List<Document>();
    }
}

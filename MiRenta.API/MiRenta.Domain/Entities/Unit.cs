namespace MiRenta.Domain.Entities
{
    public class Unit
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal MonthlyRent { get; set; }
        public string Status { get; set; } = "Available";
        public bool IsDeleted { get; set; }
        public Guid PropertyId { get; set; }

        public Property Property { get; set; } = null!;
        public ICollection<Lease> Leases { get; set; } = new List<Lease>();
        public ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();
    }
}

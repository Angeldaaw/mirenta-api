namespace MiRenta.Domain.Entities
{
    public class MaintenanceRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Open";
        public string Priority { get; set; } = "Medium";
        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ResolvedAt { get; set; }
        public decimal? Cost { get; set; }
        public Guid PropertyId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? TenantId { get; set; }

        public Property Property { get; set; } = null!;
        public Unit? Unit { get; set; }
        public Tenant? Tenant { get; set; }
    }
}

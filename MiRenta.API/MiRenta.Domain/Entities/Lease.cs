namespace MiRenta.Domain.Entities
{
    public class Lease
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal DepositAmount { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Guid PropertyId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid TenantId { get; set; }

        public Property Property { get; set; } = null!;
        public Unit? Unit { get; set; }
        public Tenant Tenant { get; set; } = null!;
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Document> Documents { get; set; } = new List<Document>();
    }
}

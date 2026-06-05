namespace MiRenta.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public Guid? PropertyId { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? LeaseId { get; set; }

        public User User { get; set; } = null!;
        public Property? Property { get; set; }
        public Tenant? Tenant { get; set; }
        public Lease? Lease { get; set; }
    }
}

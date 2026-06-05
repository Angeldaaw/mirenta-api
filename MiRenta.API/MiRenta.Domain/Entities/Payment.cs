namespace MiRenta.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaidAt { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending";
        public string? Method { get; set; }
        public string? Reference { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid LeaseId { get; set; }

        public Lease Lease { get; set; } = null!;
        public PaymentReceipt? Receipt { get; set; }
    }
}

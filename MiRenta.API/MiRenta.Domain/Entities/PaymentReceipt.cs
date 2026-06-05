namespace MiRenta.Domain.Entities
{
    public class PaymentReceipt
    {
        public Guid Id { get; set; }
        public string ReceiptNumber { get; set; } = string.Empty;
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public string? FileUrl { get; set; }
        public Guid PaymentId { get; set; }

        public Payment Payment { get; set; } = null!;
    }
}

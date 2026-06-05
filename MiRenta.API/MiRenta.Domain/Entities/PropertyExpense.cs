namespace MiRenta.Domain.Entities
{
    public class PropertyExpense
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid PropertyId { get; set; }

        public Property Property { get; set; } = null!;
    }
}

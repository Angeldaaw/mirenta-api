namespace MiRenta.Domain.Entities
{
    public class PropertyImage
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string? Caption { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid PropertyId { get; set; }

        public Property Property { get; set; } = null!;
    }
}

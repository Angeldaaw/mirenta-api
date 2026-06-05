namespace MiRenta.Domain.Entities
{
    public class Owner
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}

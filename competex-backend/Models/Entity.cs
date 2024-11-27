using System.ComponentModel.DataAnnotations;

namespace competex_backend.Models
{
    public class Entity : Identifiable
    {
        public EntityType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public Level Level { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
    }
}
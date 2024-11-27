using competex_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace competex_backend.API.DTOs
{
    public class EntityDTO
    {
        public Guid Id { get; init; }
        public EntityType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public Level Level { get; set; }
        [Required]
        public Member Owner { get; set; }
    }
}

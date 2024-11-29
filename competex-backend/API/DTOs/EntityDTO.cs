using competex_backend.Models;
using System.ComponentModel.DataAnnotations;

namespace competex_backend.API.DTOs
{
    public class EntityDTO : Identifiable
    {
        public EntityType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public Level Level { get; set; }
        public MemberDTO? Owner { get; set; }
        public Guid? OwnerId { get; set; }
    }
}

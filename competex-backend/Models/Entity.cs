using System.ComponentModel.DataAnnotations;

namespace competex_backend.Models
{
    public class Entity : IIdentifiable
    {
        public Guid Id { get; init; }
        public EntityType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public EntityLevel Level { get; set; }
        [Required]
        public Member Owner { get; set; }


        public Entity(Member owner)
        {
            Owner = owner;
            Id = new Guid();
        }

        public Entity(Member owner, Guid entityId)
            : this(owner)
        {
            Id = entityId;
        }
    }
}
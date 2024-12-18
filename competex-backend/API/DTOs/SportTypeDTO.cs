using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class SportTypeDTO : Identifiable
    {
        public string Name { get; set; } = string.Empty;
        public EntityType EntityType { get; set; }
    }
}
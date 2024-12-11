
namespace competex_backend.Models
{
    public class SportType : Identifiable
    {
        public string Name { get; set; } = string.Empty;
        public EntityType EntityType { get; set; }
    }
}

using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    //@APIModel
    public class SportTypeDTO : Identifiable
    {
        public string Name { get; set; } = string.Empty;
        //public List<string> EventAttributes { get; set; } = new List<string>();
        public EntityType EntityType { get; set; }
    }
}
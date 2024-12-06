using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    //@APIModel
    public class SportTypeDTO : Identifiable
    {
        public string Name { get; set; } = string.Empty;
        public List<string> EventAttributes { get; set; } = new List<string>();
        //public required IEnumerable<Club> Clubs { get; set; }
        //public required IEnumerable<Admin> Admins { get; set; }
        //public IEnumerable<Event>? Events { get; set; }
        //public required IEnumerable<CompetitionType> CompetitionTypes { get; set; }
        public EntityType? EntityType { get; set; }
    }
}
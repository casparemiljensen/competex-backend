
namespace competex_backend.Models
{
    public class SportType : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string> EventAttributes { get; set; } = new List<string>();
        // TODO: Investigate if need need these properties
        //public required IEnumerable<Club> Clubs { get; set; }
        //public required IEnumerable<Admin> Admins { get; set; }
        //public IEnumerable<Event>? Events { get; set; }
        //public required IEnumerable<CompetitionType> CompetitionTypes { get; set; }
        public EntityType EntityType { get; set; }
    }
}

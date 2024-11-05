
namespace competex_backend.Models
{
    public class Competition : IIdentifiable
    {
        public Guid Id { get; init; }
        public required IEnumerable<CompetitionType> CompetitionType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Level level { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public int MinParticipants { get; set; }
        public int MaxParticipants { get; set; }
        
    }
}

using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class CompetitionDTO
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

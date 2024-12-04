using competex_backend.API.DTOs;

namespace competex_backend.Models
{
    public class ScoreResult : Identifiable
    {
        public Guid CompetitionId { get; set; }
        public Guid ParticipantId { get; set; }
        public int Faults { get; set; }
        public TimeSpan Time { get; set; }
    }
}

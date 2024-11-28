using competex_backend.Models;

namespace competex_backend.Models
{
    public class Match : Identifiable
    {
        public Guid RoundId { get; set; }
        public List<Guid>? ParticipantIds { get; set; }
        public MatchStatus Status { get; set; } = MatchStatus.Pending;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid? FieldId { get; set; }
        public Guid? JudgeId { get; set; }
        public List<Guid>? ScoreIds { get; set; } = new List<Guid>();
    }
}

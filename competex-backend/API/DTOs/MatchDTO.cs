using competex_backend.Models;

namespace competex_backend.API.DTOs
{
    public class MatchDTO : Identifiable
    {
        public Guid RoundId { get; set; }
        public List<Participant>? Participants { get; set; }
        public MatchStatus Status { get; set; } = MatchStatus.Pending;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Field? Field { get; set; }
        public Judge? Judge { get; set; }
        public Score? Score { get; set; }
    }
}

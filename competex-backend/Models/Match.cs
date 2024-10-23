using competex_backend.Models;

namespace competex_backend.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public int RoundId { get; set; }
        public List<Participant>? Participants { get; set; }
        public MatchStatus Status { get; set; } = MatchStatus.pending;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Field? Field { get; set; }
        public Judge? Judge { get; set; }
        public Score? Score { get; set; }
    }
}

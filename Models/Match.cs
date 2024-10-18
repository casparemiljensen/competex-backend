using competex_backend.Models;

namespace competex_backend.Models
{
    internal class Match
    {
        public int MatchId { get; set; }
        public int RoundId { get; set; }
        public List<Participant> Participants { get; set; }
        public MatchStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int FieldId { get; set; }
        public int JudgeId { get; set; }
        public Score Score { get; set; }
    }
}
